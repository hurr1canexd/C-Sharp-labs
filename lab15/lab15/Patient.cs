namespace Lab15
{
    public class Patient
    {
        public int Id { get; set; }
        public bool IsInfected { get; set; }
        public uint Time { get; set; }

        public Patient(bool isInfected, uint time, int id)
        {
            Id = id;
            IsInfected = isInfected;
            Time = time;
        }
    }
}
