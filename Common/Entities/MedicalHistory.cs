namespace SmileMate.Common.Entities
{
    public class MedicalHistory
    {
        public long Id { get; set; }
        public string Diagnosis { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }
        public string PhotoPath { get; set; }

        public Reception Reception { get; set; }
    }
}