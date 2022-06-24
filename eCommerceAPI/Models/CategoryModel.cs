namespace eCommerceAPI.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string? CategoryName { get; set; }

        public Category( int iD, string? categoryName )
        {
            ID = iD;
            CategoryName = categoryName;
        }

     }
}