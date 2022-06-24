namespace eCommerceAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public int CategoryID { get; set; }
        public string? ImageURL { get; set; }

        public Product(
            int id,
            string? productName,
            double? price,
            string? description,
            int categoryID,
            string? imageURL
            )
        {
            ID = id;
            ProductName = productName;
            Price = price;
            Description = description;
            CategoryID = categoryID;
            ImageURL = imageURL;
        }

     }
}