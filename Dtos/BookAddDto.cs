namespace BookStoreApp.Dtos
{
   public partial class BookAddDto
   {
        public string BookTitle { get; set; } = "";
        public string BookAuthorFirstName { get; set; } = "";
        public string BookAuthorLastName { get; set; } = "";
        public string Genre { get; set; } = "";
        //public string BookImg { get; set; } = "";
        public string PublishedYear { get; set; } = "";
   } 
}