namespace SmileMate.Common.Entities
{
    public class Patient : User
    {
        public DateTime BirthdayDate { get; set; }
        public string Address { get; set; }
        
        public long ReceptionId { get; set; }
        public Reception Reception { get; set; }
    }
}
