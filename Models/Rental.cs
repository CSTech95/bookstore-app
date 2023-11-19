namespace BookStoreApp.Models
{
   public partial class Rental
   {
       public int RentalId{ get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
   } 
}