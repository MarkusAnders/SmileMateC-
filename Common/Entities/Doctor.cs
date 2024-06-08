namespace SmileMate.Common.Entities
{
    public class Doctor : User
    {
        public List<Schedule> Schedules { get; set; } = new();
        public long ReceptionId { get; set; }
        public Reception Reception { get; set; }
    }
}