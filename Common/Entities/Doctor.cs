using SmileMate.Common.Enums;

namespace SmileMate.Common.Entities
{
    public class Doctor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public TimeOnly StartTime { get; set; } 
        public TimeOnly EndTime { get; set; }

        public long ReceptionId { get; set; }
        public Reception Reception { get; set; }
    }
}