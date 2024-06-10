namespace SmileMate.Common.Entities
{
    public class Doctor : User
    {
        public List<Reception>? Receptions { get; set; } = new();
    }
}