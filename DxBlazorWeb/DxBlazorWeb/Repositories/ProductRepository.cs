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

        public async Task<List<SanPham>> GetAllProductsAsync()
        {
            const string sql = @"SELECT * FROM PRODUCT ORDER BY CREATED_AT DESC;";
            var list = new List<SanPham>();

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);
            await conn.OpenAsync();
            using var r = await cmd.ExecuteReaderAsync();

            while (await r.ReadAsync())
            {
                var sp = new SanPham
                {
                    MA_SP = ToStringSafe(r["MA_SP"]),
                    TEN_SAN_PHAM = ToStringSafe(r["TEN_SAN_PHAM"]),
                    MA_KHACH_HANG = ToStringSafe(r["MA_KHACH_HANG"]),
                    MA_LO_HANG = ToStringSafe(r["MA_LO_HANG"]),
                    MA_NHOM_HANG = ToStringSafe(r["MA_NHOM_HANG"]),
                    MA_HANG = ToStringSafe(r["MA_HANG"]),

                    LOAI_BAO_THAN_CHAC = ToStringSafe(r["LOAI_BAO_THAN_CHAC"]),
                    LOAI_BAO_THAN_DAI = ToStringSafe(r["LOAI_BAO_THAN_DAI"]),
                    LOAI_BAO_ONG_CHAC = ToStringSafe(r["LOAI_BAO_ONG_CHAC"]),
                    LOAI_BAO_ONG_DAI = ToStringSafe(r["LOAI_BAO_ONG_DAI"]),
                    LOAI_BAO_TRON = ToStringSafe(r["LOAI_BAO_TRON"]),
                    LOAI_BAO_4_TAM = ToStringSafe(r["LOAI_BAO_4_TAM"]),
                    LOAI_BAO_KHAC = ToStringSafe(r["LOAI_BAO_KHAC"]),

                    CHIEU_DAI_DAY = ToStringSafe(r["CHIEU_DAI_DAY"]),
                    CHIEU_RONG_DAY = ToStringSafe(r["CHIEU_RONG_DAY"]),
                    CHIEU_CAO = ToStringSafe(r["CHIEU_CAO"]),
                    DUONG_KINH_MIENG = ToStringSafe(r["DUONG_KINH_MIENG"]),
                    DUONG_KINH_DAY = ToStringSafe(r["DUONG_KINH_DAY"]),
                    TAI_TRONG_YC = ToStringSafe(r["TAI_TRONG_YC"]),

                    DO_DAY_LINER = ToStringSafe(r["DO_DAY_LINER"]),
                    CHIEU_RONG_LINER = ToStringSafe(r["CHIEU_RONG_LINER"]),
                    CHIEU_DAI_LINER = ToStringSafe(r["CHIEU_DAI_LINER"]),

                    CO_TRAN = ToStringSafe(r["CO_TRAN"]),
                    KIEU_TRANG = ToStringSafe(r["KIEU_TRANG"]),
                    MAU_VAI = ToStringSafe(r["MAU_VAI"]),

                    CO_DAI_DAY = ToStringSafe(r["CO_DAI_DAY"]),
                    DAI_DAY = ToStringSafe(r["DAI_DAY"]),
                    CO_CHONG_PHINH = ToStringSafe(r["CO_CHONG_PHINH"]),
                    RONG_CHONG_PHINH = ToStringSafe(r["RONG_CHONG_PHINH"]),
                    SO_LUONG_PHINH = ToStringSafe(r["SO_LUONG_PHINH"]),
                    CO_CHONG_XI = ToStringSafe(r["CO_CHONG_XI"]),
                    SO_LUONG_XI = ToStringSafe(r["SO_LUONG_XI"]),
                    CO_DAY_CHONG_XI = ToStringSafe(r["CO_DAY_CHONG_XI"]),
                    SO_LUONG_DAY_XI = ToStringSafe(r["SO_LUONG_DAY_XI"]),
                    CO_MEX = ToStringSafe(r["CO_MEX"]),
                    CO_NHAM = ToStringSafe(r["CO_NHAM"]),
                    CO_PE_DAY = ToStringSafe(r["CO_PE_DAY"]),
                    CO_PALLET = ToStringSafe(r["CO_PALLET"]),

                    VAI_THAN_ONG = ToStringSafe(r["VAI_THAN_ONG"]),
                    VAI_THAN_DAI = ToStringSafe(r["VAI_THAN_DAI"]),
                    VAI_THAN_NGAN = ToStringSafe(r["VAI_THAN_NGAN"]),
                    VAI_MIENG = ToStringSafe(r["VAI_MIENG"]),
                    VAI_DAY = ToStringSafe(r["VAI_DAY"]),
                    VAI_BOCANG = ToStringSafe(r["VAI_BOCANG"]),
                    VAI_TAM_GIA_CUONG = ToStringSafe(r["VAI_TAM_GIA_CUONG"]),
                    VAI_DEM_10x10 = ToStringSafe(r["VAI_DEM_10x10"]),
                    VAI_XA_CHEO_MIENG = ToStringSafe(r["VAI_XA_CHEO_MIENG"]),
                    VAI_XA_CHEO_DAY = ToStringSafe(r["VAI_XA_CHEO_DAY"]),
                    VAI_TUONG_MIENG = ToStringSafe(r["VAI_TUONG_MIENG"]),
                    VAI_TUONG_DAY = ToStringSafe(r["VAI_TUONG_DAY"]),

                    PE_TARP_TC_MIENG = ToStringSafe(r["PE_TARP_TC_MIENG"]),
                    PE_TARP_TC_DAY = ToStringSafe(r["PE_TARP_TC_DAY"]),
                    PE_TARP_TC_DUFFLE = ToStringSafe(r["PE_TARP_TC_DUFFLE"]),
                    PE_TARP_PE_DAY = ToStringSafe(r["PE_TARP_PE_DAY"]),
                    PE_TARP_CHONG_BAN = ToStringSafe(r["PE_TARP_CHONG_BAN"]),

                    TAPE_DAY_TC_MIENG = ToStringSafe(r["TAPE_DAY_TC_MIENG"]),
                    TAPE_DAY_MOC = ToStringSafe(r["TAPE_DAY_MOC"]),

                    BEFT_DAI_NANG = ToStringSafe(r["BEFT_DAI_NANG"]),
                    BEFT_DAI_NGANG = ToStringSafe(r["BEFT_DAI_NGANG"]),
                    BEFT_DAI_BOCANG_MIENG = ToStringSafe(r["BEFT_DAI_BOCANG_MIENG"]),
                    BEFT_DAI_BOCANG_THAN = ToStringSafe(r["BEFT_DAI_BOCANG_THAN"]),
                    BEFT_DAI_BOCANG_DAY = ToStringSafe(r["BEFT_DAI_BOCANG_DAY"]),
                    BEFT_DAI_MOC = ToStringSafe(r["BEFT_DAI_MOC"]),

                    ROPE_CHAC_MIENG = ToStringSafe(r["ROPE_CHAC_MIENG"]),
                    ROPE_CHAC_DAY = ToStringSafe(r["ROPE_CHAC_DAY"]),
                    ROPE_CHAC_NANG = ToStringSafe(r["ROPE_CHAC_NANG"]),
                    ROPE_CHAC_NGANG = ToStringSafe(r["ROPE_CHAC_NGANG"]),

                    LABEL = ToStringSafe(r["LABEL"]),
                    POCKET = ToStringSafe(r["POCKET"]),
                    CHI_MAY = ToStringSafe(r["CHI_MAY"]),
                    ONG_PVC = ToStringSafe(r["ONG_PVC"]),
                    KHOA_CHAC = ToStringSafe(r["KHOA_CHAC"]),

                    GHI_CHU = ToStringSafe(r["GHI_CHU"]),
                    CREATED_AT = r["CREATED_AT"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(r["CREATED_AT"])
                };

                list.Add(sp);
            }

            return list;
        }

        private static string ToStringSafe(object? v)
            => v == null || v == DBNull.Value ? "" : v.ToString() ?? "";

        public async Task<bool> ExistsAsync(string maSp)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("SELECT 1 FROM PRODUCT WHERE MA_SP = @ID", conn);
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);
            await conn.OpenAsync();
            var obj = await cmd.ExecuteScalarAsync();
            return obj != null;
        }

        public async Task<bool> AddNewProductAsync(ProductIdentityModel p)
        {
            Console.WriteLine("zzzzzzz");
            const string sql = @"
                INSERT INTO PRODUCT
                (MA_SP, TEN_SAN_PHAM, MA_KHACH_HANG, MA_LO_HANG, MA_NHOM_HANG, MA_HANG, CREATED_AT)
                VALUES (@MA_SP, @TEN_SAN_PHAM, @MA_KHACH_HANG, @MA_LO_HANG, @MA_NHOM_HANG, @MA_HANG, GETDATE());";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@MA_SP", SqlDbType.UniqueIdentifier).Value = Guid.Parse(p.ID);
            cmd.Parameters.AddWithValue("@TEN_SAN_PHAM", p.TenSanPham ?? "");
            cmd.Parameters.AddWithValue("@MA_KHACH_HANG", p.MaKhachHang ?? "");
            cmd.Parameters.AddWithValue("@MA_LO_HANG", p.MaLoHang ?? "");
            cmd.Parameters.AddWithValue("@MA_NHOM_HANG", p.MaNhomHang ?? "");
            cmd.Parameters.AddWithValue("@MA_HANG", p.MaHang ?? "");

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateProductIdentityAsync(ProductIdentityModel p)
        {
            const string sql = @"
                UPDATE PRODUCT SET
                    TEN_SAN_PHAM   = @TEN_SAN_PHAM,
                    MA_KHACH_HANG  = @MA_KHACH_HANG,
                    MA_LO_HANG     = @MA_LO_HANG,
                    MA_NHOM_HANG   = @MA_NHOM_HANG,
                    MA_HANG        = @MA_HANG
                WHERE MA_SP = @MA_SP;";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@MA_SP", SqlDbType.UniqueIdentifier).Value = Guid.Parse(p.ID);
            cmd.Parameters.AddWithValue("@TEN_SAN_PHAM", p.TenSanPham ?? "");
            cmd.Parameters.AddWithValue("@MA_KHACH_HANG", p.MaKhachHang ?? "");
            cmd.Parameters.AddWithValue("@MA_LO_HANG", p.MaLoHang ?? "");
            cmd.Parameters.AddWithValue("@MA_NHOM_HANG", p.MaNhomHang ?? "");
            cmd.Parameters.AddWithValue("@MA_HANG", p.MaHang ?? "");

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
