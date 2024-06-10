namespace SmileMate.Common.Entities
{
    public class Patient : User
    {
        public DateTime BirthdayDate { get; set; }
        public string Address { get; set; }
        
        public List<Reception>? Receptions { get; set; }
    }
}
