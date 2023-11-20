namespace BookStoreApp.Dtos
{
    public partial class RentalEditDto
   {
        public int RentalId { get; set; }
        public DateTime EndDate { get; set; }
        //TODO Add the below
        //public bool Active { get; set; }
        //public bool Returned { get; set; }
   }  
}