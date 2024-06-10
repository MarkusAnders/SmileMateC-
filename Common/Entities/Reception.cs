namespace SmileMate.Common.Entities
{
    public class Reception
    {
        public long Id { get; set; }
        public Patient? Client { get; set; }
        public long? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public List<MedicalHistory>? MedicalHistory { get; set; }
    }
}