namespace reviel.Models
{
    public enum ProductType
    {
        Krem,
        Serum,
        Sampuan
    }

    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ProductType Type { get; set; }
        public required string Description { get; set; }
        public string? ImagePath { get; set; } // Görsel yolu, null olabilir
        public string? Benefits { get; set; } // Egzamaya faydaları
        public string? KeyIngredients { get; set; } // Anahtar içerikleri ürünlerin
    }
}
