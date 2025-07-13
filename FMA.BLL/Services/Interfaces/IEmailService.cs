using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        // 1. Thông báo khi tạo kèo
        Task SendMatchPostCreatedAsync(string fullName, string email);

        // 2. Thông báo khi có người request kèo – gửi cho chủ kèo
        Task SendNewMatchRequestAsync(string postOwnerName, string postOwnerEmail, string requesterName);

        // 3. Thông báo kết quả request – gửi cho người request
        Task SendMatchRequestResultAsync(string requesterName, string requesterEmail, bool isAccepted);

        // 4. Thông báo booking – gửi cho cả hai bên
        Task SendBookingCreatedAsync(string bookerName, string bookerEmail, Guid pitchId, DateTime bookingTime);

        // 5. Thông báo booking cho sân – gửi cho chủ sân
        Task SendBookingNotificationToStationAsync(string stationName, string stationEmail, Guid bookingId, Guid pitchId, DateTime bookingTime);
    }

}
