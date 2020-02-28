namespace Web.Models
{
    public class OrderInputModel
    {
        #region Public Properties

        public string Customer { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }

        #endregion Public Properties
    }
}