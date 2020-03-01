using Web.Models;

namespace Web.Services
{
    public interface IOrderService
    {
        #region Public Methods

        void CreateOrder(OrderInputModel orderInputModel);

        #endregion Public Methods
    }
}