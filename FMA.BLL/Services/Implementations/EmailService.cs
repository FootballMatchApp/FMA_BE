using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.BLL.Services.Interfaces;
using FMA.Common.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FMA.BLL.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettingModel _emailSettings;
        private const string LogoUrl = "https://t3.ftcdn.net/jpg/02/90/25/24/360_F_290252407_4bDtD0cxOxCzVqvlNFHHmNxw9qKTuRup.jpg";
        private const string PrimaryColor = "#0052cc";    // xanh FMA
        private const string SecondaryColor = "#ffffff";  // nền trắng

        public EmailService(IOptions<EmailSettingModel> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, false);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendMatchPostCreatedAsync(string fullName, string email)
        {
            var subject = "🏟️ Kèo mới từ FMA đã lên sóng!";
            var body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{ margin:0; padding:0; background:#f0f2f5; font-family:Arial,sans-serif; }}
    .wrapper {{ max-width:600px; margin:30px auto; background:{SecondaryColor}; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.1); overflow:hidden; }}
    .header {{ background:{PrimaryColor}; padding:20px; text-align:center; }}
    .header img {{ max-height:40px; }}
    .content {{ padding:30px; color:#333; }}
    .content h2 {{ margin-top:0; color:{PrimaryColor}; font-size:24px; }}
    .content p {{ line-height:1.6; }}
    .button {{ display:inline-block; margin-top:20px; padding:12px 24px; background:{PrimaryColor}; color:#fff; text-decoration:none; border-radius:4px; }}
    .footer {{ text-align:center; font-size:12px; color:#999; padding:15px; }}
  </style>
</head>
<body>
  <div class='wrapper'>
    <div class='header'>
      <img src='{LogoUrl}' alt='FMA Logo'/>
    </div>
    <div class='content'>
      <h2>Xin chào, {fullName}!</h2>
      <p>Bạn vừa tạo thành công <strong>kèo mới</strong> trên nền tảng <strong>FMA</strong>. Hãy mời bạn bè và sẵn sàng cho trận đấu kịch tính sắp tới!</p>
      <a href='https://fma.com/myposts' class='button'>Xem kèo của tôi</a>
    </div>
    <div class='footer'>
      Bạn cần hỗ trợ? Liên hệ <a href='mailto:support@fma.com'>support@fma.com</a>
    </div>
  </div>
</body>
</html>";
            await SendEmailAsync(email, subject, body);
        }

        public async Task SendNewMatchRequestAsync(string postOwnerName, string postOwnerEmail, string requesterName)
        {
            var subject = "📩 Bạn có yêu cầu mới trên FMA!";
            var body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{ margin:0; padding:0; background:#eef1f6; font-family:Arial,sans-serif; }}
    .wrapper {{ max-width:600px; margin:30px auto; background:{SecondaryColor}; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.1); }}
    .header {{ background:{PrimaryColor}; padding:20px; text-align:center; }}
    .header img {{ max-height:40px; }}
    .content {{ padding:30px; color:#333; }}
    .content h2 {{ margin:0 0 10px; color:{PrimaryColor}; font-size:24px; }}
    .button {{ display:inline-block; margin-top:20px; padding:12px 24px; background:{PrimaryColor}; color:#fff; text-decoration:none; border-radius:4px; }}
    .footer {{ font-size:12px; color:#999; text-align:center; padding:15px; }}
  </style>
</head>
<body>
  <div class='wrapper'>
    <div class='header'>
      <img src='{LogoUrl}' alt='FMA Logo'/>
    </div>
    <div class='content'>
      <h2>Chào {postOwnerName},</h2>
      <p><strong>{requesterName}</strong> vừa gửi <em>yêu cầu tham gia kèo</em> của bạn trên FMA.</p>
      <a href='https://fma.com/requests' class='button'>Xem và duyệt yêu cầu</a>
    </div>
    <div class='footer'>
      FMA – Kết nối đam mê thể thao
    </div>
  </div>
</body>
</html>";
            await SendEmailAsync(postOwnerEmail, subject, body);
        }

        public async Task SendMatchRequestResultAsync(string requesterName, string requesterEmail, bool isAccepted)
        {
            var subject = isAccepted
                ? "✅ Yêu cầu tham gia kèo đã được chấp nhận!"
                : "❌ Yêu cầu tham gia kèo không thành công!";
            var emoji = isAccepted ? "🙌" : "😔";
            var message = isAccepted
                ? "Chúc mừng! Yêu cầu của bạn đã được chấp nhận. Hãy chuẩn bị cho trận đấu."
                : "Rất tiếc! Yêu cầu của bạn đã không được chấp nhận lần này.";

            var body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{ background:#f7f8fa; font-family:Arial,sans-serif; margin:0; }}
    .card {{ max-width:600px; margin:30px auto; background:{SecondaryColor}; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.1); overflow:hidden; }}
    .header {{ padding:20px; text-align:center; background:{PrimaryColor}; color:#fff; }}
    .content {{ padding:30px; color:#333; }}
    .content h2 {{ font-size:24px; margin-bottom:10px; }}
    .content p {{ line-height:1.6; }}
    .footer {{ padding:15px; text-align:center; font-size:12px; color:#666; }}
  </style>
</head>
<body>
  <div class='card'>
    <div class='header'>
      <h2>{emoji} Xin chào, {requesterName}!</h2>
    </div>
    <div class='content'>
      <p>{message}</p>
      <p><a href='https://fma.com/myrequests' style='color:{PrimaryColor}; text-decoration:none;'>Xem chi tiết yêu cầu</a></p>
    </div>
    <div class='footer'>
      FMA – Cầu nối của những trận cầu đỉnh cao
    </div>
  </div>
</body>
</html>";
            await SendEmailAsync(requesterEmail, subject, body);
        }

        public async Task SendBookingCreatedAsync(string bookerName, string bookerEmail, Guid pitchId, DateTime bookingTime)
        {
            var subject = "🏅 Đặt sân thành công với FMA!";
            var body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{ margin:0; padding:0; background:#edf1f7; font-family:Arial,sans-serif; }}
    .container {{ max-width:600px; margin:30px auto; background:{SecondaryColor}; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.1); }}
    .header {{ background:{PrimaryColor}; text-align:center; padding:20px; }}
    .header img {{ height:40px; }}
    .content {{ padding:30px; color:#333; }}
    .content h2 {{ margin:0 0 15px; font-size:24px; color:{PrimaryColor}; }}
    .details {{ background:#f9f9f9; padding:15px; border-radius:4px; }}
    .details p {{ margin:5px 0; }}
    .footer {{ padding:15px; text-align:center; font-size:12px; color:#888; }}
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>
      <img src='{LogoUrl}' alt='FMA Logo'/>
    </div>
    <div class='content'>
      <h2>Xin chào, {bookerName}!</h2>
      <p>Bạn đã đặt sân thành công trên FMA. Thông tin chi tiết:</p>
      <div class='details'>
        <p><strong>Pitch ID:</strong> {pitchId}</p>
        <p><strong>Thời gian:</strong> {bookingTime:HH:mm dd/MM/yyyy}</p>
      </div>
      <p>Chúc bạn có một trận đấu thật sôi động!</p>
    </div>
    <div class='footer'>
      FMA – đồng hành cùng đam mê thể thao
    </div>
  </div>
</body>
</html>";
            await SendEmailAsync(bookerEmail, subject, body);
        }
        public async Task SendBookingNotificationToStationAsync(string stationName, string stationEmail, Guid bookingId, Guid pitchId, DateTime bookingTime)
        {
            var subject = "🏟️ Có booking mới tại sân của bạn!";
            var body = $@"<!DOCTYPE html>
<html lang='vi'>
<head>
  <meta charset='UTF-8'>
  <style>
    body {{ margin:0; padding:0; background:#eef1f6; font-family:Arial,sans-serif; }}
    .wrapper {{ max-width:600px; margin:30px auto; background:{SecondaryColor}; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.1); }}
    .header {{ background:{PrimaryColor}; padding:20px; text-align:center; }}
    .header img {{ max-height:40px; }}
    .content {{ padding:30px; color:#333; }}
    .content h2 {{ margin:0 0 10px; color:{PrimaryColor}; font-size:24px; }}
    .details {{ background:#f9f9f9; padding:15px; border-radius:4px; }}
    .details p {{ margin:5px 0; }}
    .footer {{ font-size:12px; color:#999; text-align:center; padding:15px; }}
  </style>
</head>
<body>
  <div class='wrapper'>
    <div class='header'>
      <img src='{LogoUrl}' alt='FMA Logo'/>
    </div>
    <div class='content'>
      <h2>Xin chào, {stationName}!</h2>
      <p>Bạn vừa nhận được một booking mới tại sân (Pitch ID: <strong>{pitchId}</strong>).</p>
      <div class='details'>
        <p><strong>Booking ID:</strong> {bookingId}</p>
        <p><strong>Thời gian:</strong> {bookingTime:HH:mm dd/MM/yyyy}</p>
      </div>
      <p>Vui lòng kiểm tra và chuẩn bị đón tiếp khách.</p>
    </div>
    <div class='footer'>
      FMA – hỗ trợ sân cỏ chuyên nghiệp
    </div>
  </div>
</body>
</html>";
            await SendEmailAsync(stationEmail, subject, body);
        }
    }


}
    
