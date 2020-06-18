using System;
using System.Collections.Concurrent;


namespace Lab15
{
    public class ExaminationRoom
    {
        public event EventHandler PermissionEnter;

        private readonly int _countSeats;
        private int _countDoctors;
        private bool _infected;

        ConcurrentQueue<Patient> _queuePatients;
        ConcurrentQueue<Patient> _queueViewingRoom;
        ConcurrentDictionary<int, Patient> _queueViewingRoomInService;
        ConcurrentQueue<Doctor> _listFreeDoctors;


        public int CountInQueue
        {
            get { return _queuePatients.Count; }
        }


        public ExaminationRoom(uint countSeats = 0, uint countDoctors = 0)
        {
            _countSeats = (int)countSeats;
            _countDoctors = (int)countDoctors;
            PermissionEnter += Enter;

            _queuePatients = new ConcurrentQueue<Patient>();
            _queueViewingRoom = new ConcurrentQueue<Patient>();
            _queueViewingRoomInService = new ConcurrentDictionary<int, Patient>();
            _listFreeDoctors = new ConcurrentQueue<Doctor>();

            for (int i = 0; i < _countDoctors; i++)
            {
                var doc = new Doctor();
                doc.ID = i;
                _listFreeDoctors.Enqueue(doc);
            }
        }
        

        public void PushPatinentInQueue(Patient patient)
        {
            _queuePatients.Enqueue(patient);
            Debug.Write($"Пациент {patient.Id} ({(patient.IsInfected ? "заражен" : "не заражен")}) встал в очередь (Пациентов в очереди: {CountInQueue})");

            if (CountPatient() < _countSeats)
            { 
                PermissionEnter?.Invoke(this, new EventArgs());
            }
        }


        public void PushPateintInViewingRoom()
        {
            Patient patient;

            do
            {
                if (_queuePatients.Count != 0 && CountPatient() < _countSeats)
                {
                    if (!_queuePatients.TryPeek(out patient))
                        return;

                    if (IsInfected() == patient.IsInfected || IsClear())
                    {
                        if (!_queuePatients.TryDequeue(out patient))
                            return;

                        _infected = patient.IsInfected;
                        _queueViewingRoom.Enqueue(patient);

                        Debug.Write($"Пациент {patient.Id} ({(patient.IsInfected ? "заражен" : "не заражен")}) зашел в смотровую " +
                            $"(Пациентов в смотровой: {CountPatient()} / {Capacity()})");
                        if (_listFreeDoctors.Count != 0)
                            Viewing();
                    }
                    else
                        break;
                }
                else
                    break;
            }
            while (_queuePatients.Count != 0);
        }


        private void Viewing()
        {
            Patient patient;
            Doctor doc;

            if (!_queueViewingRoom.TryDequeue(out patient) || !_queueViewingRoomInService.TryAdd(patient.Id, patient) || !_listFreeDoctors.TryDequeue(out doc))
                return;

            doc.EndWork += EndWork; 

            Debug.Write($"Доктор {doc.ID} смотрит пациента {patient.Id} ");

            doc.Work(patient);

            Doctor docCons;
            if (new Random().Next(1, 10) >= 8 && _listFreeDoctors.Count != 0)
            {
                if (!_listFreeDoctors.TryDequeue(out docCons))
                    return; 
                doc.Consultant = docCons;
                docCons.Consultant = doc;
                
                Debug.Write($"Доктор {doc.ID} консультируется с доктором {docCons.ID}");
                docCons.EndWork += EndWork;

                doc.Consulting(docCons, patient);
            }
        }


        public bool IsClear()
        {
            return CountPatient() == 0;
        }


        public bool IsInfected()
        {
            return _infected;
        }


        public int CountPatient()
        {
            return _queueViewingRoom.Count + _queueViewingRoomInService.Count;
        }


        public int Capacity()
        {
            return _countSeats;
        }


        private void EndWork(object obj, EventArgs eventArgs)
        {
            Doctor doc = (Doctor)obj;
            Patient patient;

            if (!_queueViewingRoomInService.TryRemove(doc.GetPatient().Id, out patient))
                return;
            
            if(doc.Consultant == null)
                Debug.Write($"Доктор {doc.ID} осмотрел пациента {doc.GetPatient().Id}");
            else
                Debug.Write($"Доктор {doc.ID} и {doc.Consultant.ID} осмотрели пациента {doc.GetPatient().Id} ");
            
            if (doc.Consultant != null)
            {
                doc.Consultant.Consultant = null;
                doc.Consultant.EndWork -= EndWork;
                _listFreeDoctors.Enqueue(doc.Consultant);
            }

            doc.EndWork -= EndWork;
            doc.Consultant = null;
            _listFreeDoctors.Enqueue(doc);

            Viewing();
            PermissionEnter?.Invoke(this, new EventArgs());
        }


        public void InfectedQueue()
        {
            var tmpArray = _queuePatients.ToArray();

            for (int i = 0; i < tmpArray.Length; i++)
                tmpArray[i].IsInfected = true;

            _queuePatients = new ConcurrentQueue<Patient>(tmpArray);
            Debug.Write("Вся очередь инфицирована");
        }


        public bool IsInfectedInQueue()
        {
            foreach (var item in _queuePatients)
            {
                if (item.IsInfected) return true;
            }

            return false;
        }


        private void Enter(object obj, EventArgs eventArgs)
        {
            PushPateintInViewingRoom();
        }
    }
}
