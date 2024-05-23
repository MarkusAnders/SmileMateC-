namespace SmileMate.Common.Entities
{
    public class Patient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        
        public long ReceptionId { get; set; }
        public Reception Reception { get; set; }
    }
}
