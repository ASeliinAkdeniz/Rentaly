namespace Rentaly.BusinessLayer.EmailService
{
    public interface IEmailService
    {
        Task SendRentalApprovalEmailAsync(string toEmail, string customerName, string carInfo,
            DateTime pickupDate, DateTime returnDate, string pickupBranch, string returnBranch,
            decimal totalPrice, string discountCode);
    }
}
