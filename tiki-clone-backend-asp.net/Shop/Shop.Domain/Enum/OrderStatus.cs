using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Enum
{
    public enum OrderStatus
    {
        /// <summary>
        /// chờ xác nhận
        /// </summary>
        WaitAccess = 1,

        /// <summary>
        /// đã xác nhận
        /// </summary>
        Accessed,

        /// <summary>
        /// đang xử lý
        /// </summary>
        Processing,

        /// <summary>
        /// đã gửi đi
        /// </summary>
        Sent,

        /// <summary>
        /// đã nhận
        /// </summary>
        Received,

        /// <summary>
        /// đã hoàn thành
        /// </summary>
        Done,

        /// <summary>
        /// hủy
        /// </summary>
        Cancel,

        /// <summary>
        /// chờ thanh toán
        /// </summary>
        WaitForPay,

        /// <summary>
        /// đã trả lại
        /// </summary>
        Returned,
    }
}
