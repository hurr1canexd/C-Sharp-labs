using System;
using System.Threading;


namespace Lab15
{
    class Program
    {
        private static ExaminationRoom room;
        const int t = 10;

        static void Main(string[] args)
        {
            room = new ExaminationRoom(10, 2);

            int id = 0;

            while (true)
            {
                bool isInfected = (uint)new Random().Next(1, 10) >= 5;
                uint timing = (uint)new Random().Next(1, t);

                Patient patient = new Patient(isInfected, timing, id++);
                room.PushPatinentInQueue(patient);

                if (room.IsInfectedInQueue())
                {
                    if (new Random().Next(1, 10) >= 7)
                    { 
                        room.InfectedQueue();
                    }
                }

                int a = new Random().Next(1, 3);
                Thread.Sleep(a * Debug.UnitTime);
            }
        }
    }
}

