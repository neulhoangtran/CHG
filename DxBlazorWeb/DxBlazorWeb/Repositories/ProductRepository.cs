using DxBlazorWeb.Model;
using DxBlazorWeb.Repositories.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DxBlazorWeb.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connStr;
        public ProductRepository(IConfiguration cfg)
        {
            _connStr = cfg.GetConnectionString("DefaultConnection")!;
        }

        public async Task<bool> ExistsAsync(string maSp)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("SELECT 1 FROM SAN_PHAM WHERE MA_SP = @ID", conn);
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);
            await conn.OpenAsync();
            var obj = await cmd.ExecuteScalarAsync();
            return obj != null;
        }

        public async Task<bool> AddNewProductAsync(SanPham p)
        {
            const string sql = @"
INSERT INTO SAN_PHAM
(MA_SP, TEN_SAN_PHAM, MA_KHACH_HANG, MA_LO_HANG, MA_NHOM_HANG, MA_HANG, CREATED_AT)
VALUES (@MA_SP, @TEN_SAN_PHAM, @MA_KHACH_HANG, @MA_LO_HANG, @MA_NHOM_HANG, @MA_HANG, GETDATE());";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@MA_SP", SqlDbType.UniqueIdentifier).Value = Guid.Parse(p.MA_SP);
            cmd.Parameters.AddWithValue("@TEN_SAN_PHAM", p.TEN_SAN_PHAM ?? "");
            cmd.Parameters.AddWithValue("@MA_KHACH_HANG", p.MA_KHACH_HANG ?? "");
            cmd.Parameters.AddWithValue("@MA_LO_HANG", p.MA_LO_HANG ?? "");
            cmd.Parameters.AddWithValue("@MA_NHOM_HANG", p.MA_NHOM_HANG ?? "");
            cmd.Parameters.AddWithValue("@MA_HANG", p.MA_HANG ?? "");

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateProductIdentityAsync(SanPham p)
        {
            const string sql = @"
UPDATE SAN_PHAM SET
    TEN_SAN_PHAM   = @TEN_SAN_PHAM,
    MA_KHACH_HANG  = @MA_KHACH_HANG,
    MA_LO_HANG     = @MA_LO_HANG,
    MA_NHOM_HANG   = @MA_NHOM_HANG,
    MA_HANG        = @MA_HANG
WHERE MA_SP = @MA_SP;";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@MA_SP", SqlDbType.UniqueIdentifier).Value = Guid.Parse(p.MA_SP);
            cmd.Parameters.AddWithValue("@TEN_SAN_PHAM", p.TEN_SAN_PHAM ?? "");
            cmd.Parameters.AddWithValue("@MA_KHACH_HANG", p.MA_KHACH_HANG ?? "");
            cmd.Parameters.AddWithValue("@MA_LO_HANG", p.MA_LO_HANG ?? "");
            cmd.Parameters.AddWithValue("@MA_NHOM_HANG", p.MA_NHOM_HANG ?? "");
            cmd.Parameters.AddWithValue("@MA_HANG", p.MA_HANG ?? "");

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> AddProductInformationAsync(string maSp, ProductInfoModel info)
            => await UpdateProductInformationAsync(maSp, info); // cùng logic update phần

        public async Task<bool> UpdateProductInformationAsync(string maSp, ProductInfoModel info)
        {
            const string sql = @"
UPDATE SAN_PHAM SET
    LOAI_BAO_THAN_CHAC=@A, LOAI_BAO_THAN_DAI=@B, LOAI_BAO_ONG_CHAC=@C, LOAI_BAO_ONG_DAI=@D,
    LOAI_BAO_TRON=@E, LOAI_BAO_4_TAM=@F, LOAI_BAO_KHAC=@G,
    CHIEU_DAI_DAY=@H, CHIEU_RONG_DAY=@I, CHIEU_CAO=@J,
    DUONG_KINH_MIENG=@K, DUONG_KINH_DAY=@L, TAI_TRONG_YC=@M,
    DO_DAY_LINER=@N, CHIEU_RONG_LINER=@O, CHIEU_DAI_LINER=@P,
    CO_TRAN=@Q, KIEU_TRANG=@R, MAU_VAI=@S,
    CO_DAI_DAY=@T, DAI_DAY=@U, CO_CHONG_PHINH=@V, RONG_CHONG_PHINH=@W, SO_LUONG_PHINH=@X,
    CO_CHONG_XI=@Y, SO_LUONG_XI=@Z, CO_DAY_CHONG_XI=@AA, SO_LUONG_DAY_XI=@AB,
    CO_MEX=@AC, CO_NHAM=@AD, CO_PE_DAY=@AE, CO_PALLET=@AF, GHI_CHU=@AG
WHERE MA_SP=@ID;";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);

            cmd.Parameters.AddWithValue("@A", info.LoaiBaoThanChac ?? "");
            cmd.Parameters.AddWithValue("@B", info.LoaiBaoThanDai ?? "");
            cmd.Parameters.AddWithValue("@C", info.LoaiBaoOngChac ?? "");
            cmd.Parameters.AddWithValue("@D", info.LoaiBaoOngDai ?? "");
            cmd.Parameters.AddWithValue("@E", info.LoaiBaoTron ?? "");
            cmd.Parameters.AddWithValue("@F", info.LoaiBao4Tam ?? "");
            cmd.Parameters.AddWithValue("@G", info.LoaiBaoKhac ?? "");

            cmd.Parameters.AddWithValue("@H", info.ChieuDaiDay ?? "");
            cmd.Parameters.AddWithValue("@I", info.ChieuRongDay ?? "");
            cmd.Parameters.AddWithValue("@J", info.ChieuCao ?? "");
            cmd.Parameters.AddWithValue("@K", info.DuongKinhMieng ?? "");
            cmd.Parameters.AddWithValue("@L", info.DuongKinhDay ?? "");
            cmd.Parameters.AddWithValue("@M", info.TaiTrongYC ?? "");

            cmd.Parameters.AddWithValue("@N", info.DoDayLiner ?? "");
            cmd.Parameters.AddWithValue("@O", info.ChieuRongLiner ?? "");
            cmd.Parameters.AddWithValue("@P", info.ChieuDaiLiner ?? "");

            cmd.Parameters.AddWithValue("@Q", info.CoTran ?? "");
            cmd.Parameters.AddWithValue("@R", info.KieuTrang ?? "");
            cmd.Parameters.AddWithValue("@S", info.MauVai ?? "");

            cmd.Parameters.AddWithValue("@T", info.CoDaiDay ?? "");
            cmd.Parameters.AddWithValue("@U", info.DaiDay ?? "");
            cmd.Parameters.AddWithValue("@V", info.CoChongPhinh ?? "");
            cmd.Parameters.AddWithValue("@W", info.RongChongPhinh ?? "");
            cmd.Parameters.AddWithValue("@X", info.SoLuongPhinh ?? "");
            cmd.Parameters.AddWithValue("@Y", info.CoChongXi ?? "");
            cmd.Parameters.AddWithValue("@Z", info.SoLuongXi ?? "");
            cmd.Parameters.AddWithValue("@AA", info.CoDayChongXi ?? "");
            cmd.Parameters.AddWithValue("@AB", info.SoLuongDayXi ?? "");
            cmd.Parameters.AddWithValue("@AC", info.CoMex ?? "");
            cmd.Parameters.AddWithValue("@AD", info.CoNham ?? "");
            cmd.Parameters.AddWithValue("@AE", info.CoPeDay ?? "");
            cmd.Parameters.AddWithValue("@AF", info.CoPallet ?? "");
            cmd.Parameters.AddWithValue("@AG", info.GhiChu ?? "");

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> AddProductDetailInformationAsync(string maSp, ProductDetailModel d)
            => await UpdateProductDetailInformationAsync(maSp, d);

        public async Task<bool> UpdateProductDetailInformationAsync(string maSp, ProductDetailModel d)
        {
            const string sql = @"
UPDATE SAN_PHAM SET
  VAI_THAN_ONG=@a1, VAI_THAN_DAI=@a2, VAI_THAN_NGAN=@a3, VAI_MIENG=@a4, VAI_DAY=@a5, VAI_BOCANG=@a6,
  VAI_TAM_GIA_CUONG=@a7, VAI_DEM_10x10=@a8, VAI_XA_CHEO_MIENG=@a9, VAI_XA_CHEO_DAY=@a10,
  VAI_TUONG_MIENG=@a11, VAI_TUONG_DAY=@a12,
  PE_TARP_TC_MIENG=@b1, PE_TARP_TC_DAY=@b2, PE_TARP_TC_DUFFLE=@b3, PE_TARP_PE_DAY=@b4, PE_TARP_CHONG_BAN=@b5,
  TAPE_DAY_TC_MIENG=@c1, TAPE_DAY_MOC=@c2,
  BEFT_DAI_NANG=@d1, BEFT_DAI_NGANG=@d2, BEFT_DAI_BOCANG_MIENG=@d3, BEFT_DAI_BOCANG_THAN=@d4, BEFT_DAI_BOCANG_DAY=@d5, BEFT_DAI_MOC=@d6,
  ROPE_CHAC_MIENG=@e1, ROPE_CHAC_DAY=@e2, ROPE_CHAC_NANG=@e3, ROPE_CHAC_NGANG=@e4,
  LABEL=@f1, POCKET=@f2, CHI_MAY=@f3, ONG_PVC=@f4, KHOA_CHAC=@f5
WHERE MA_SP=@ID;";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);

            cmd.Parameters.AddWithValue("@a1", d.VaiThanOng ?? ""); cmd.Parameters.AddWithValue("@a2", d.VaiThanDai ?? "");
            cmd.Parameters.AddWithValue("@a3", d.VaiThanNgan ?? ""); cmd.Parameters.AddWithValue("@a4", d.VaiMieng ?? "");
            cmd.Parameters.AddWithValue("@a5", d.VaiDay ?? ""); cmd.Parameters.AddWithValue("@a6", d.VaiBocang ?? "");
            cmd.Parameters.AddWithValue("@a7", d.VaiTamGiaCuong ?? ""); cmd.Parameters.AddWithValue("@a8", d.VaiDem10x10 ?? "");
            cmd.Parameters.AddWithValue("@a9", d.VaiXaCheoMieng ?? ""); cmd.Parameters.AddWithValue("@a10", d.VaiXaCheoDay ?? "");
            cmd.Parameters.AddWithValue("@a11", d.VaiTuongMieng ?? ""); cmd.Parameters.AddWithValue("@a12", d.VaiTuongDay ?? "");

            cmd.Parameters.AddWithValue("@b1", d.PeTarpTcMieng ?? ""); cmd.Parameters.AddWithValue("@b2", d.PeTarpTcDay ?? "");
            cmd.Parameters.AddWithValue("@b3", d.PeTarpTcDuffle ?? ""); cmd.Parameters.AddWithValue("@b4", d.PeTarpPeDay ?? "");
            cmd.Parameters.AddWithValue("@b5", d.PeTarpChongBan ?? "");

            cmd.Parameters.AddWithValue("@c1", d.TapeDayTcMieng ?? ""); cmd.Parameters.AddWithValue("@c2", d.TapeDayMoc ?? "");

            cmd.Parameters.AddWithValue("@d1", d.BeftDaiNang ?? ""); cmd.Parameters.AddWithValue("@d2", d.BeftDaiNgang ?? "");
            cmd.Parameters.AddWithValue("@d3", d.BeftDaiBocangMieng ?? ""); cmd.Parameters.AddWithValue("@d4", d.BeftDaiBocangThan ?? "");
            cmd.Parameters.AddWithValue("@d5", d.BeftDaiBocangDay ?? ""); cmd.Parameters.AddWithValue("@d6", d.BeftDaiMoc ?? "");

            cmd.Parameters.AddWithValue("@e1", d.RopeChacMieng ?? ""); cmd.Parameters.AddWithValue("@e2", d.RopeChacDay ?? "");
            cmd.Parameters.AddWithValue("@e3", d.RopeChacNang ?? ""); cmd.Parameters.AddWithValue("@e4", d.RopeChacNgang ?? "");

            cmd.Parameters.AddWithValue("@f1", d.Label ?? ""); cmd.Parameters.AddWithValue("@f2", d.Pocket ?? "");
            cmd.Parameters.AddWithValue("@f3", d.ChiMay ?? ""); cmd.Parameters.AddWithValue("@f4", d.OngPvc ?? "");
            cmd.Parameters.AddWithValue("@f5", d.KhoaChac ?? "");

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }

}
