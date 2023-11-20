namespace BookStoreApp.Models
{
   public partial class Book
   {
       public int BookId{ get; set; }
        public string BookTitle { get; set; } = "";
        public string BookAuthor { get; set; } = "";
        public string BookImg { get; set; } = "";
        public string PublishedYear { get; set; }
   } 
}