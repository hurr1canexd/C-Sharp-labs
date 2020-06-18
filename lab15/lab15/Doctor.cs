using System;
using System.Threading;
using System.Threading.Tasks;


namespace Lab15
{
    public class Doctor
    {
        public event EventHandler EndWork;

        public int ID { get; set; }
        private Patient _patient;


        public Patient GetPatient()
        {
            return _patient;
        }


        public Doctor Consultant { set; get; }


        public Doctor() { }


        public async void Work(Patient patient)
        {
            if (patient is null)
            { 
                throw new NullReferenceException();
            }

            _patient = patient;
            await Task.Run(() => Thread.Sleep((int)patient.Time * Debug.UnitTime));
            EndWork?.Invoke(this, new EventArgs());
        }


        public void Consulting(Doctor doc, Patient patient)
        {
            if (patient is null || doc is null)
            { 
                throw new NullReferenceException();
            }

            doc.Work(patient);
        }
    }
}
