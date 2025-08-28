using System.ComponentModel.DataAnnotations;

namespace DxBlazorApplication.Model
{

    public class ProductFilterModel
    {
        public string TenSanPham { set; get; }
        public string MaKhacHang { set; get; }
        public string LoHang { set; get; }

    }
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
        [Required]
        public string LoaiBao { set; get; }  = "";

        // Kích thước 
        public int KTDenier { get; set; }
        public int KTSoiDoc { get; set; }
        public int KTSoiNgang { get; set; }
        public int KTChieuDai { get; set; }
        public int KTChieuRong { get; set; }
        public int KTChieuCao { get; set; }

        // TC 
        public int TCDenier { get; set; }
        public int TCSoiDoc { get; set; }
        public int TCSoiNgang { get; set; }
        public int TCDuongKinh { get; set; }
        public int TCChieuCao { get; set; }

        // TC Day
        public int TCDDenier { get; set; }
        public int TCDSoiDoc { get; set; }
        public int TCDSoiNgang { get; set; }
        public int TCDDuongKinh { get; set; }
        public int TCDChieuCao { get; set; }

        // tải trọng
        public int TrongTai { get; set; }

        // Liner
        public int LinerDoDay { get; set; }
        public int LinerKho { get; set; }  // Line khổ 
        public int LinerDai { get; set; }  // Line dai 
        public string DoDayLiner { get; set; } = "";
        public string ChieuRongLiner { get; set; } = "";
        public string ChieuDaiLiner { get; set; } = "";

        // Trang
        public string Trang { get; set; } = "";

        // Mau Vai
        public string MauVai { get; set; } = "";
        
        // Tráng & Màu
        public string CoTran { get; set; } = "";

        // Tùy chọn khác
        public string CoDaiDay { get; set; } = "";
        public string CoChongPhinh { get; set; } = "";
        public string RongChongPhinh { get; set; } = "";
        public string CoChongXi { get; set; } = "";
        public string CoDayChongXi { get; set; } = "";
        public string CoMex { get; set; } = "";
        public string CoNham { get; set; } = "";
        public string CoPeDay { get; set; } = "";
        public string CoPallet { get; set; } = "";
        public string GhiChu { get; set; } = "";
    }

    // 3) Thông tin chi tiết (thành phần vật liệu)
    public class ProductDetailModel
    {
        // Thân dài
        public int TDRong { set; get; }
        public int TDDai { set; get; }
        public int TDDienTich { set; get; }
        public int TDSoLuong { set; get; }
        public int TDTrongLuong { set; get; }

        // Thân ngắn
        public int TNRong { set; get; }
        public int TNDai { set; get; }
        public int TNDienTich { set; get; }
        public int TNSoLuong { set; get; }
        public int TNTrongLuong { set; get; }

        // Miệng
        public int MRong { set; get; }
        public int MDai { set; get; }
        public int MDienTich { set; get; }
        public int MSoLuong { set; get; }
        public int MTrongLuong { set; get; }

        // TC Miệng
        public int TCMRong { set; get; }
        public int TCMDai { set; get; }
        public int TCMDienTich { set; get; }
        public int TCMSoLuong { set; get; }
        public int TCMTrongLuong { set; get; }

        // Dây TC Miệng
        public int DTCMRong { set; get; }
        public int DTCMDai { set; get; }
        public int DTCMSoLuong { set; get; }
        public int DTCMTrongLuong { set; get; }

        // Chạc Miệng
        public int CMLoaiChac { set; get; }
        public int CMDai { set; get; }
        public int CMSoLuong { set; get; }
        public int CMTrongLuong { set; get; }

        // TC Đáy
        public int TCDRong { set; get; }
        public int TCDDai { set; get; }
        public int TCDDienTich { set; get; }
        public int TCDSoLuong { set; get; }
        public int TCDTrongLuong { set; get; }

        // Tucong miệng
        public int TMRong { set; get; }
        public int TMDai { set; get; }
        public int TMDienTich { set; get; }
        public int TMSoLuong { set; get; }
        public int TMTrongLuong { set; get; }

        // Dây TC Đáy
        public int DTDMRong { set; get; }
        public int DTCDDai { set; get; }
        public int DTCDSoLuong { set; get; }
        public int DTCDTrongLuong { set; get; }

        // --- Chạc ĐÁY ---
        public int CDLoaiChac { get; set; }        // loại chạc (3,5,8)
        public int CDDai { get; set; }             // chiều dài
        public int CDSoLuong { get; set; }         // số lượng
        public int CDTrongLuong { get; set; }      // trọng lượng (tính)

        // --- Tự công ĐÁY ---
        public int TUDRong { get; set; }           // rộng
        public int TUDDai { get; set; }            // dài
        public int TUDienTich { get; set; }        // diện tích (tính)
        public int TUDSoLuong { get; set; }        // số lượng
        public int TUDTrongLuong { get; set; }     // trọng lượng (tính)

        // --- Chạc NÂNG ---
        public int CNLoaiChac { get; set; }        // loại chạc (5,8,9,10,11,12,13,14,16,18)
        public string CNMauChac { get; set; } = ""; // WT/BLUE/GREEN
        public int CNRong { get; set; }            // rộng
        public int CNDai { get; set; }             // dài
        public int CNSoLuong { get; set; }         // số lượng
        public int CNTrongLuong { get; set; }      // trọng lượng (tính)

        // --- Đai NÂNG ---
        public int DNLoaiChac { get; set; }        // loại chạc (5,8,9,10,11,12,13,14,16,18)
        public string DNMauChac { get; set; } = ""; // WT/BLUE/GREEN
        public int DNRong { get; set; }            // rộng
        public int DNDai { get; set; }             // dài
        public int DNSoLuong { get; set; }         // số lượng
        public int DNTrongLuong { get; set; }      // trọng lượng (tính)

        // --- Đai Ngang ---
        public int DNGLoaiChac { get; set; }        // loại chạc (5,8,9,10,11,12,13,14,16,18)
        public string DNGMauChac { get; set; } = ""; // WT/BLUE/GREEN
        public int DNGRong { get; set; }            // rộng
        public int DNGDai { get; set; }             // dài
        public int DNGSoLuong { get; set; }         // số lượng
        public int DNGTrongLuong { get; set; }      // trọng lượng (tính)

        // --- POKET / POCKET ---
        public int PocketRong { get; set; }        // rộng
        public int PocketDai { get; set; }         // dài
        public int PocketSoLuong { get; set; }     // số lượng
        public int PocketTrongLuong { get; set; }  // trọng lượng (tính)

        // --- ĐẮP 10x10 ---
        public int Dap10x10SoLuong { get; set; }   // số lượng
        public int Dap10x10TrongLuong { get; set; } // trọng lượng (tính)

        // --- Chống BẨN ---
        public int ChongBanRong { get; set; }      // rộng
        public int ChongBanDai { get; set; }       // dài
        public int ChongBanDienTich { get; set; }  // diện tích (tính)
        public int ChongBanSoLuong { get; set; }   // số lượng
        public int ChongBanTrongLuong { get; set; } // trọng lượng (tính)

        // --- Liner (chi tiết vật tư nếu cần thể hiện ở tab này) ---
        public int LinerDoDayCt { get; set; }      // độ dày
        public int LinerKhoCt { get; set; }        // khổ
        public int LinerDaiCt { get; set; }        // dài
        public int LinerSoLuongCt { get; set; }    // số lượng
        public int LinerTrongLuongCt { get; set; } // trọng lượng (tính)

        // --- Chống PHÌNH ---
        public int ChongPhinhRong { get; set; }    // rộng
        public int ChongPhinhDai { get; set; }     // dài
        public int ChongPhinhDienTich { get; set; } // diện tích (tính)
        public int ChongPhinhSoLuong { get; set; } // số lượng
        public int ChongPhinhTrongLuong { get; set; } // trọng lượng (tính)

        // --- Chống XÌ ---
        public int ChongXiChieuDai { get; set; }   // chiều dài
        public int ChongXiSoLuong { get; set; }    // số lượng
        public int ChongXiTrongLuong { get; set; } // trọng lượng (tính)

    }

    // Model lưu DB (map 1-1 bảng SAN_PHAM)
    public class SanPham
    {
        public string MA_SP { get; set; } = Guid.NewGuid().ToString();

        // --- Tab 1: Nhận diện sản phẩm ---
        public string TEN_SAN_PHAM { get; set; } = "";
        public string MA_KHACH_HANG { get; set; } = "";
        public string MA_LO_HANG { get; set; } = "";
        public string MA_NHOM_HANG { get; set; } = "";
        public string MA_HANG { get; set; } = "";

        // --- Tab 2: Thông tin sản phẩm ---
        public string LOAI_BAO { get; set; } = "";       // GhepChac, GheDai, OngChac, ...

        // Kích thước
        public int KT_DENIER { get; set; }
        public int KT_SOI_DOC { get; set; }
        public int KT_SOI_NGANG { get; set; }
        public int KT_CHIEU_DAI { get; set; }
        public int KT_CHIEU_RONG { get; set; }
        public int KT_CHIEU_CAO { get; set; }

        // Tapchi miệng
        public int TC_DENIER { get; set; }
        public int TC_SOI_DOC { get; set; }
        public int TC_SOI_NGANG { get; set; }
        public int TC_DUONG_KINH { get; set; }
        public int TC_CHIEU_CAO { get; set; }

        // Tapchi đáy
        public int TCD_DENIER { get; set; }
        public int TCD_SOI_DOC { get; set; }
        public int TCD_SOI_NGANG { get; set; }
        public int TCD_DUONG_KINH { get; set; }
        public int TCD_CHIEU_CAO { get; set; }

        // Trọng tải
        public int TRONG_TAI { get; set; }

        // Liner
        public int LINER_DO_DAY { get; set; }
        public int LINER_KHO { get; set; }
        public int LINER_DAI { get; set; }
        public string LINER_DO_DAY_STR { get; set; } = "";
        public string LINER_CHIEU_RONG_STR { get; set; } = "";
        public string LINER_CHIEU_DAI_STR { get; set; } = "";

        // Màu & tráng
        public string TRANG { get; set; } = "";
        public string MAU_VAI { get; set; } = "";
        public string CO_TRAN { get; set; } = "";

        // Tùy chọn khác
        public string CO_DAI_DAY { get; set; } = "";
        public string CO_CHONG_PHINH { get; set; } = "";
        public string RONG_CHONG_PHINH { get; set; } = "";
        public string CO_CHONG_XI { get; set; } = "";
        public string CO_DAY_CHONG_XI { get; set; } = "";
        public string CO_MEX { get; set; } = "";
        public string CO_NHAM { get; set; } = "";
        public string CO_PE_DAY { get; set; } = "";
        public string CO_PALLET { get; set; } = "";
        public string GHI_CHU { get; set; } = "";

        // --- Tab 3: Thông tin chi tiết ---
        // Thân dài
        public int TD_RONG { get; set; }
        public int TD_DAI { get; set; }
        public int TD_DIEN_TICH { get; set; }
        public int TD_SO_LUONG { get; set; }
        public int TD_TRONG_LUONG { get; set; }

        // Thân ngắn
        public int TN_RONG { get; set; }
        public int TN_DAI { get; set; }
        public int TN_DIEN_TICH { get; set; }
        public int TN_SO_LUONG { get; set; }
        public int TN_TRONG_LUONG { get; set; }

        // Miệng
        public int M_RONG { get; set; }
        public int M_DAI { get; set; }
        public int M_DIEN_TICH { get; set; }
        public int M_SO_LUONG { get; set; }
        public int M_TRONG_LUONG { get; set; }

        // Tapchi miệng
        public int TCM_RONG { get; set; }
        public int TCM_DAI { get; set; }
        public int TCM_DIEN_TICH { get; set; }
        public int TCM_SO_LUONG { get; set; }
        public int TCM_TRONG_LUONG { get; set; }

        // Dây TC miệng
        public int DTCM_RONG { get; set; }
        public int DTCM_DAI { get; set; }
        public int DTCM_SO_LUONG { get; set; }
        public int DTCM_TRONG_LUONG { get; set; }

        // Chạc miệng
        public int CM_LOAI_CHAC { get; set; }
        public int CM_DAI { get; set; }
        public int CM_SO_LUONG { get; set; }
        public int CM_TRONG_LUONG { get; set; }

        // Tapchi đáy
        public int TCD_RONG { get; set; }
        public int TCD_DAI { get; set; }
        public int TCD_DIEN_TICH { get; set; }
        public int TCD_SO_LUONG { get; set; }
        public int TCD_TRONG_LUONG { get; set; }

        // Tự công miệng
        public int TM_RONG { get; set; }
        public int TM_DAI { get; set; }
        public int TM_DIEN_TICH { get; set; }
        public int TM_SO_LUONG { get; set; }
        public int TM_TRONG_LUONG { get; set; }

        // Dây TC đáy
        public int DTCD_RONG { get; set; }
        public int DTCD_DAI { get; set; }
        public int DTCD_SO_LUONG { get; set; }
        public int DTCD_TRONG_LUONG { get; set; }

        // Chạc đáy
        public int CD_LOAI_CHAC { get; set; }
        public int CD_DAI { get; set; }
        public int CD_SO_LUONG { get; set; }
        public int CD_TRONG_LUONG { get; set; }

        // Tự công đáy
        public int TUD_RONG { get; set; }
        public int TUD_DAI { get; set; }
        public int TUD_DIEN_TICH { get; set; }
        public int TUD_SO_LUONG { get; set; }
        public int TUD_TRONG_LUONG { get; set; }

        // Chạc nâng
        public int CN_LOAI_CHAC { get; set; }
        public string CN_MAU_CHAC { get; set; } = "";
        public int CN_RONG { get; set; }
        public int CN_DAI { get; set; }
        public int CN_SO_LUONG { get; set; }
        public int CN_TRONG_LUONG { get; set; }

        // Đai nâng
        public int DN_LOAI_CHAC { get; set; }
        public string DN_MAU_CHAC { get; set; } = "";
        public int DN_RONG { get; set; }
        public int DN_DAI { get; set; }
        public int DN_SO_LUONG { get; set; }
        public int DN_TRONG_LUONG { get; set; }

        // Đai ngang
        public int DNG_LOAI_CHAC { get; set; }
        public string DNG_MAU_CHAC { get; set; } = "";
        public int DNG_RONG { get; set; }
        public int DNG_DAI { get; set; }
        public int DNG_SO_LUONG { get; set; }
        public int DNG_TRONG_LUONG { get; set; }

        // Pocket
        public int POCKET_RONG { get; set; }
        public int POCKET_DAI { get; set; }
        public int POCKET_SO_LUONG { get; set; }
        public int POCKET_TRONG_LUONG { get; set; }

        // Đắp 10x10
        public int DAP10x10_SO_LUONG { get; set; }
        public int DAP10x10_TRONG_LUONG { get; set; }

        // Chống bẩn
        public int CB_RONG { get; set; }
        public int CB_DAI { get; set; }
        public int CB_DIEN_TICH { get; set; }
        public int CB_SO_LUONG { get; set; }
        public int CB_TRONG_LUONG { get; set; }

        // Liner chi tiết
        public int LINER_DODAY_CT { get; set; }
        public int LINER_KHO_CT { get; set; }
        public int LINER_DAI_CT { get; set; }
        public int LINER_SOLUONG_CT { get; set; }
        public int LINER_TRONGLUONG_CT { get; set; }

        // Chống phình
        public int CP_RONG { get; set; }
        public int CP_DAI { get; set; }
        public int CP_DIEN_TICH { get; set; }
        public int CP_SO_LUONG { get; set; }
        public int CP_TRONG_LUONG { get; set; }

        // Chống xì
        public int CX_CHIEU_DAI { get; set; }
        public int CX_SO_LUONG { get; set; }
        public int CX_TRONG_LUONG { get; set; }

        public DateTime CREATED_AT { get; set; } = DateTime.Now;
    }
}
