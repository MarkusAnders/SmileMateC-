    using Microsoft.AspNetCore.SignalR;

namespace SmileMate.Common.Entities
{
    public class Reception
    {
        public long Id { get; set; }
        public Patient Client { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime ReceptionDate { get; set; }

        public long MedicalHistoryId { get; set; }
        public MedicalHistory MedicalHistory { get; set; }
    }
}