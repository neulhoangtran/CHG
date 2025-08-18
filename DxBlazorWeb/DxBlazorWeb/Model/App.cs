using System.ComponentModel.DataAnnotations;

namespace DxBlazorWeb.Model
{
    public class ProductShipment
    {
        public string ID { set; get; } = Guid.NewGuid().ToString();
        public string CustomerID { set; get; } = string.Empty;
        public string ProductID { set; get; } = string.Empty;
        public string LotNo { set; get; } = string.Empty;
        public string ShippingConditionalID { set; get; } = string.Empty;
        public DateTime CreatedAt { set; get; } = DateTime.Now;
    }

    public class Output
    { 
        public string ID { set; get; } = Guid.NewGuid().ToString();
        public string OutputName { set; get; } = string.Empty;
        public decimal Level1 { set; get; } 
        public decimal Level2 { set; get; } 
        public decimal Level3 { set; get; } 
    }
    public class Customer
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mã khách hàng")]
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerManager { get; set; } = "";   // Người phụ trách
        public string CustomerPosition { get; set; } = "";  // Chức vụ
        public string CustomerEmail { get; set; } = "";     // Email
        public string CustomerPhone { get; set; } = "";     // Số đt
        public string CustomerAddress { get; set; } = "";   // Địa chỉ
        public string CustomerKakao { get; set; } = "";     // Kakao
        public string CustomerZalo { get; set; } = "";      // Zalo
        public string CustomerDetail { get; set; } = "";    // Thông tin chi tiết
        public string CustomerCSQH { get; set; } = string.Empty;
        public string CustomerDKTT { get; set; } = string.Empty;
        public string CustomerRisk { get; set; } = string.Empty;
    }

    public class CustomerCare
    {
        public string ID { get; set; } = "";          // GUID
        public string CustomerName { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string CustomerCSQH { get; set; } = ""; // Tốt/Trung bình/Thấp
        public string CustomerRisk { get; set; } = ""; // Cao/Trung bình/Thấp
        public string CustomerDKTT { get; set; } = ""; // Tốt/Trung bình/Thấp
        public decimal? CustomerReview { get; set; }   // 1–10 (0.5)
    }

    public class Order
    {
        public string ID { get; set; } = "";
        public string CustomerID { get; set; } = "";  // chọn từ danh sách CUSTOMER
        public string OrderCode { get; set; } = "";
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; } = "";
    }

    // View model cho Grid (kèm Tên/Mã KH từ join)
    public class OrderView : Order
    {
        public string CustomerName { get; set; } = "";
        public string CustomerCode { get; set; } = "";
    }


    public class ShippingConditional
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Vui lòng nhập điều kiện")]
        public string ConditionName { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public decimal EffectPrice { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public class ProductShipmentView
    {
        public string ID { get; set; } = string.Empty;

        // Tên khách hàng từ bảng CUSTOMER
        public string CustomerName { get; set; } = string.Empty;

        // Mã sản phẩm từ bảng PRODUCT
        public string ProductCode { get; set; } = string.Empty;

        // Tên điều kiện giao hàng từ bảng SHIPPING_CONDITIONAL
        public string ShippingName { get; set; } = string.Empty;

        // Ngày tạo lô hàng (PRODUCT_SHIPMENT.CREATED_AT)
        public DateTime CreatedAt { get; set; }
    }

}
