namespace SmileMate.Common.Entities
{
    public class Schedule
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public TimeOnly Time { get; set; }

        public long DoctorId { get; set; }
        
        public Doctor Doctor { get; set; }
    }
}