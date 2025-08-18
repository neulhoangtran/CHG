using System.ComponentModel.DataAnnotations;

namespace DxBlazorWeb.Model
{
    // 1) Nhận diện sản phẩm
    public class ProductIdentityModel
    {
        public string ID = string.Empty;
        [Required(ErrorMessage = "Vui lòng nhập Tên sản phẩm")]
        public string TenSanPham { get; set; } = "";
        [Required(ErrorMessage = "Chọn Mã khách hàng")]
        public string MaKhachHang { get; set; } = "";  // chọn từ CUSTOMER_CODE
        public string MaLoHang { get; set; } = "";
        public string MaNhomHang { get; set; } = "";
        [Required(ErrorMessage = "Vui lòng nhập Mã hàng")]
        public string MaHang { get; set; } = "";
    }

    // 2) Thông tin sản phẩm (kích thước, liner, tráng, màu, options)
    public class ProductInfoModel
    {
        // Loại bao (1–7) → string ("1"/"0" hoặc "Có"/"Không")
        public string LoaiBaoThanChac { get; set; } = "";
        public string LoaiBaoThanDai { get; set; } = "";
        public string LoaiBaoOngChac { get; set; } = "";
        public string LoaiBaoOngDai { get; set; } = "";
        public string LoaiBaoTron { get; set; } = "";
        public string LoaiBao4Tam { get; set; } = "";
        public string LoaiBaoKhac { get; set; } = "";

        // Kích thước / tải trọng
        public string ChieuDaiDay { get; set; } = "";
        public string ChieuRongDay { get; set; } = "";
        public string ChieuCao { get; set; } = "";
        public string DuongKinhMieng { get; set; } = "";
        public string DuongKinhDay { get; set; } = "";
        public string TaiTrongYC { get; set; } = "";

        // Liner
        public string DoDayLiner { get; set; } = "";
        public string ChieuRongLiner { get; set; } = "";
        public string ChieuDaiLiner { get; set; } = "";

        // Tráng & Màu
        public string CoTran { get; set; } = "";
        public string KieuTrang { get; set; } = "";
        public string MauVai { get; set; } = "";

        // Tùy chọn khác
        public string CoDaiDay { get; set; } = "";
        public string DaiDay { get; set; } = "";
        public string CoChongPhinh { get; set; } = "";
        public string RongChongPhinh { get; set; } = "";
        public string SoLuongPhinh { get; set; } = "";
        public string CoChongXi { get; set; } = "";
        public string SoLuongXi { get; set; } = "";
        public string CoDayChongXi { get; set; } = "";
        public string SoLuongDayXi { get; set; } = "";
        public string CoMex { get; set; } = "";
        public string CoNham { get; set; } = "";
        public string CoPeDay { get; set; } = "";
        public string CoPallet { get; set; } = "";
        public string GhiChu { get; set; } = "";
    }

    // 3) Thông tin chi tiết (thành phần vật liệu)
    public class ProductDetailModel
    {
        public string VaiThanOng { get; set; } = "";
        public string VaiThanDai { get; set; } = "";
        public string VaiThanNgan { get; set; } = "";
        public string VaiMieng { get; set; } = "";
        public string VaiDay { get; set; } = "";
        public string VaiBocang { get; set; } = "";
        public string VaiTamGiaCuong { get; set; } = "";
        public string VaiDem10x10 { get; set; } = "";
        public string VaiXaCheoMieng { get; set; } = "";
        public string VaiXaCheoDay { get; set; } = "";
        public string VaiTuongMieng { get; set; } = "";
        public string VaiTuongDay { get; set; } = "";

        public string PeTarpTcMieng { get; set; } = "";
        public string PeTarpTcDay { get; set; } = "";
        public string PeTarpTcDuffle { get; set; } = "";
        public string PeTarpPeDay { get; set; } = "";
        public string PeTarpChongBan { get; set; } = "";

        public string TapeDayTcMieng { get; set; } = "";
        public string TapeDayMoc { get; set; } = "";

        public string BeftDaiNang { get; set; } = "";
        public string BeftDaiNgang { get; set; } = "";
        public string BeftDaiBocangMieng { get; set; } = "";
        public string BeftDaiBocangThan { get; set; } = "";
        public string BeftDaiBocangDay { get; set; } = "";
        public string BeftDaiMoc { get; set; } = "";

        public string RopeChacMieng { get; set; } = "";
        public string RopeChacDay { get; set; } = "";
        public string RopeChacNang { get; set; } = "";
        public string RopeChacNgang { get; set; } = "";

        public string Label { get; set; } = "";
        public string Pocket { get; set; } = "";
        public string ChiMay { get; set; } = "";
        public string OngPvc { get; set; } = "";
        public string KhoaChac { get; set; } = "";
    }

    // Model lưu DB (map 1-1 bảng SAN_PHAM)
    public class SanPham
    {
        public string MA_SP { get; set; } = Guid.NewGuid().ToString();
        public string TEN_SAN_PHAM { get; set; } = "";
        public string MA_KHACH_HANG { get; set; } = "";
        public string MA_LO_HANG { get; set; } = "";
        public string MA_NHOM_HANG { get; set; } = "";
        public string MA_HANG { get; set; } = "";

        public string LOAI_BAO_THAN_CHAC { get; set; } = "";
        public string LOAI_BAO_THAN_DAI { get; set; } = "";
        public string LOAI_BAO_ONG_CHAC { get; set; } = "";
        public string LOAI_BAO_ONG_DAI { get; set; } = "";
        public string LOAI_BAO_TRON { get; set; } = "";
        public string LOAI_BAO_4_TAM { get; set; } = "";
        public string LOAI_BAO_KHAC { get; set; } = "";

        public string CHIEU_DAI_DAY { get; set; } = "";
        public string CHIEU_RONG_DAY { get; set; } = "";
        public string CHIEU_CAO { get; set; } = "";
        public string DUONG_KINH_MIENG { get; set; } = "";
        public string DUONG_KINH_DAY { get; set; } = "";
        public string TAI_TRONG_YC { get; set; } = "";

        public string DO_DAY_LINER { get; set; } = "";
        public string CHIEU_RONG_LINER { get; set; } = "";
        public string CHIEU_DAI_LINER { get; set; } = "";

        public string CO_TRAN { get; set; } = "";
        public string KIEU_TRANG { get; set; } = "";
        public string MAU_VAI { get; set; } = "";

        public string CO_DAI_DAY { get; set; } = "";
        public string DAI_DAY { get; set; } = "";
        public string CO_CHONG_PHINH { get; set; } = "";
        public string RONG_CHONG_PHINH { get; set; } = "";
        public string SO_LUONG_PHINH { get; set; } = "";
        public string CO_CHONG_XI { get; set; } = "";
        public string SO_LUONG_XI { get; set; } = "";
        public string CO_DAY_CHONG_XI { get; set; } = "";
        public string SO_LUONG_DAY_XI { get; set; } = "";
        public string CO_MEX { get; set; } = "";
        public string CO_NHAM { get; set; } = "";
        public string CO_PE_DAY { get; set; } = "";
        public string CO_PALLET { get; set; } = "";

        public string VAI_THAN_ONG { get; set; } = "";
        public string VAI_THAN_DAI { get; set; } = "";
        public string VAI_THAN_NGAN { get; set; } = "";
        public string VAI_MIENG { get; set; } = "";
        public string VAI_DAY { get; set; } = "";
        public string VAI_BOCANG { get; set; } = "";
        public string VAI_TAM_GIA_CUONG { get; set; } = "";
        public string VAI_DEM_10x10 { get; set; } = "";
        public string VAI_XA_CHEO_MIENG { get; set; } = "";
        public string VAI_XA_CHEO_DAY { get; set; } = "";
        public string VAI_TUONG_MIENG { get; set; } = "";
        public string VAI_TUONG_DAY { get; set; } = "";

        public string PE_TARP_TC_MIENG { get; set; } = "";
        public string PE_TARP_TC_DAY { get; set; } = "";
        public string PE_TARP_TC_DUFFLE { get; set; } = "";
        public string PE_TARP_PE_DAY { get; set; } = "";
        public string PE_TARP_CHONG_BAN { get; set; } = "";

        public string TAPE_DAY_TC_MIENG { get; set; } = "";
        public string TAPE_DAY_MOC { get; set; } = "";

        public string BEFT_DAI_NANG { get; set; } = "";
        public string BEFT_DAI_NGANG { get; set; } = "";
        public string BEFT_DAI_BOCANG_MIENG { get; set; } = "";
        public string BEFT_DAI_BOCANG_THAN { get; set; } = "";
        public string BEFT_DAI_BOCANG_DAY { get; set; } = "";
        public string BEFT_DAI_MOC { get; set; } = "";

        public string ROPE_CHAC_MIENG { get; set; } = "";
        public string ROPE_CHAC_DAY { get; set; } = "";
        public string ROPE_CHAC_NANG { get; set; } = "";
        public string ROPE_CHAC_NGANG { get; set; } = "";

        public string LABEL { get; set; } = "";
        public string POCKET { get; set; } = "";
        public string CHI_MAY { get; set; } = "";
        public string ONG_PVC { get; set; } = "";
        public string KHOA_CHAC { get; set; } = "";

        public string GHI_CHU { get; set; } = "";
        public DateTime CREATED_AT { get; set; } = DateTime.Now;
    }
}
