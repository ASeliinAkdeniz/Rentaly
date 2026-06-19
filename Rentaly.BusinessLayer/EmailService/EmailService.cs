using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Rentaly.BusinessLayer.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendRentalApprovalEmailAsync(string toEmail, string customerName, string carInfo,
            DateTime pickupDate, DateTime returnDate, string pickupBranch, string returnBranch,
            decimal totalPrice, string discountCode)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var senderEmail = smtpSettings["SenderEmail"];
            var senderPassword = smtpSettings["SenderPassword"];
            var senderName = smtpSettings["SenderName"] ?? "RentCar";

            var htmlBody = BuildEmailBody(customerName, carInfo, pickupDate, returnDate,
                pickupBranch, returnBranch, totalPrice, discountCode);

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = "Rezervasyonunuz Onaylandı! 🎉",
                Body = htmlBody,
                IsBodyHtml = true
            };
            mail.To.Add(toEmail);

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }

        private string BuildEmailBody(string customerName, string carInfo, DateTime pickupDate,
            DateTime returnDate, string pickupBranch, string returnBranch, decimal totalPrice, string discountCode)
        {
            return $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
<meta charset='UTF-8' />
</head>
<body style='margin:0;padding:0;background-color:#e8f0eb;font-family:Segoe UI, Arial, sans-serif;'>
<table width='100%' cellpadding='0' cellspacing='0' style='background-color:#e8f0eb;padding:30px 0;'>
<tr>
<td align='center'>
<table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff;border-radius:14px;overflow:hidden;box-shadow:0 4px 20px rgba(0,0,0,0.08);'>

    <!-- Header -->
    <tr>
        <td style='background-color:#080f1e;padding:30px 40px;text-align:center;'>
            <div style='display:inline-block;width:48px;height:48px;background-color:#0d7a45;border-radius:12px;line-height:48px;font-size:22px;color:#ffffff;margin-bottom:10px;'>🚗</div>
            <h1 style='color:#ffffff;font-size:20px;margin:8px 0 0;font-weight:700;'>RentCar</h1>
            <p style='color:#7a9cc8;font-size:12px;margin:2px 0 0;'>Araç Kiralama Hizmetleri</p>
        </td>
    </tr>

    <!-- Success banner -->
    <tr>
        <td style='background-color:#d1fae5;padding:18px 40px;text-align:center;'>
            <span style='font-size:24px;'>✅</span>
            <p style='color:#065f46;font-size:16px;font-weight:700;margin:6px 0 0;'>Rezervasyonunuz Onaylandı!</p>
        </td>
    </tr>

    <!-- Greeting -->
    <tr>
        <td style='padding:30px 40px 10px;'>
            <p style='font-size:15px;color:#0f2044;margin:0;'>Merhaba <strong>{customerName}</strong>,</p>
            <p style='font-size:14px;color:#3d5a80;line-height:1.6;margin-top:10px;'>
                Rezervasyon talebiniz incelendi ve onaylandı. Aracınız sizin için hazırlanıyor.
                Aşağıda rezervasyon detaylarınızı bulabilirsiniz.
            </p>
        </td>
    </tr>

    <!-- Rental details card -->
    <tr>
        <td style='padding:10px 40px;'>
            <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#e8f0eb;border-radius:12px;padding:20px;'>
                <tr>
                    <td style='padding:8px 16px;'>
                        <table width='100%' cellpadding='6' cellspacing='0'>
                            <tr>
                                <td style='font-size:13px;color:#3d5a80;font-weight:600;width:140px;'>🚘 Araç</td>
                                <td style='font-size:13px;color:#0f2044;font-weight:700;'>{carInfo}</td>
                            </tr>
                            <tr>
                                <td style='font-size:13px;color:#3d5a80;font-weight:600;'>📅 Alış Tarihi</td>
                                <td style='font-size:13px;color:#0f2044;font-weight:700;'>{pickupDate:dd.MM.yyyy}</td>
                            </tr>
                            <tr>
                                <td style='font-size:13px;color:#3d5a80;font-weight:600;'>📅 Teslim Tarihi</td>
                                <td style='font-size:13px;color:#0f2044;font-weight:700;'>{returnDate:dd.MM.yyyy}</td>
                            </tr>
                            <tr>
                                <td style='font-size:13px;color:#3d5a80;font-weight:600;'>📍 Alış Şubesi</td>
                                <td style='font-size:13px;color:#0f2044;font-weight:700;'>{pickupBranch}</td>
                            </tr>
                            <tr>
                                <td style='font-size:13px;color:#3d5a80;font-weight:600;'>📍 Teslim Şubesi</td>
                                <td style='font-size:13px;color:#0f2044;font-weight:700;'>{returnBranch}</td>
                            </tr>
                            <tr>
                                <td style='font-size:13px;color:#3d5a80;font-weight:600;border-top:1px solid #cdddd4;padding-top:12px;'>💰 Toplam Ücret</td>
                                <td style='font-size:16px;color:#0a5c34;font-weight:800;border-top:1px solid #cdddd4;padding-top:12px;'>{totalPrice:N0} ₺</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

    <!-- Discount coupon - görsel olarak -->
    <tr>
        <td style='padding:24px 40px 10px;'>
            <p style='font-size:13px;color:#3d5a80;text-align:center;margin-bottom:12px;'>Size özel bir hediyemiz var! 🎁</p>
            <table width='100%' cellpadding='0' cellspacing='0' style='background:linear-gradient(135deg,#0a5c34,#16c172);border-radius:14px;'>
                <tr>
                    <td style='padding:24px;text-align:center;'>
                        <p style='color:#ffffff;font-size:12px;letter-spacing:1px;text-transform:uppercase;margin:0;opacity:0.85;'>Sonraki Rezervasyonunuzda</p>
                        <p style='color:#ffffff;font-size:30px;font-weight:800;margin:6px 0;'>%15 İNDİRİM</p>
                        <table cellpadding='0' cellspacing='0' style='margin:14px auto 6px;'>
                            <tr>
                                <td style='background-color:#ffffff;border-radius:8px;padding:10px 28px;'>
                                    <span style='color:#0a5c34;font-size:18px;font-weight:800;letter-spacing:3px;'>{discountCode}</span>
                                </td>
                            </tr>
                        </table>
                        <p style='color:#e8f0fe;font-size:11px;margin:10px 0 0;'>Bu kodu bir sonraki rezervasyonunuzda kullanabilirsiniz.</p>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

    <!-- Footer note -->
    <tr>
        <td style='padding:24px 40px 10px;'>
            <p style='font-size:13px;color:#3d5a80;line-height:1.6;margin:0;'>
                Herhangi bir sorunuz olursa bizimle iletişime geçmekten çekinmeyin.
                İyi yolculuklar dileriz! 🚗💨
            </p>
        </td>
    </tr>

    <!-- Signature -->
    <tr>
        <td style='padding:20px 40px 30px;border-top:1px solid #e8edf5;margin-top:10px;'>
            <table cellpadding='0' cellspacing='0'>
                <tr>
                    <td style='padding-top:16px;'>
                        <p style='font-size:14px;color:#0f2044;font-weight:700;margin:0;'>RentCar Müşteri Hizmetleri</p>
                        <p style='font-size:12px;color:#3d5a80;margin:4px 0 0;'>📧 destek@rentcar.com &nbsp;|&nbsp; 📞 0850 123 45 67</p>
                        <p style='font-size:11px;color:#7a9cc8;margin:8px 0 0;'>Bu e-posta otomatik olarak gönderilmiştir.</p>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

    <!-- Bottom bar -->
    <tr>
        <td style='background-color:#080f1e;padding:16px 40px;text-align:center;'>
            <p style='color:#7a9cc8;font-size:11px;margin:0;'>© 2026 RentCar — Tüm Hakları Saklıdır</p>
        </td>
    </tr>

</table>
</td>
</tr>
</table>
</body>
</html>";
        }
    }
}
