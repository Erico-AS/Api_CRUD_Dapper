namespace ApiCruDap.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;  
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }
}