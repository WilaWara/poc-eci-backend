namespace API.DTOs
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CategoryId { get; set; }
    }
}
