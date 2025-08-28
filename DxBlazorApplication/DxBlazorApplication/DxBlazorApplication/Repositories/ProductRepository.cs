using DxBlazorApplication.Model;
using DxBlazorApplication.Repositories.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DxBlazorApplication.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connStr;
        public ProductRepository(IConfiguration cfg)
        {
            _connStr = cfg.GetConnectionString("DefaultConnection")!;
        }

        #region Helpers
        private static string ToStringSafe(object? v)
            => v == null || v == DBNull.Value ? "" : v.ToString() ?? "";

        private static int ToIntSafe(object? v)
        {
            if (v == null || v == DBNull.Value) return 0;
            if (v is int i) return i;
            int.TryParse(v.ToString(), out var x);
            return x;
        }
        #endregion

        public async Task<List<SanPham>> GetAllProductsAsync(ProductFilterModel filter)
        {
            var list = new List<SanPham>();

            // Dùng PRODUCT (không phải SAN_PHAM)
            var sql = @"
        SELECT *
        FROM PRODUCT
        WHERE 1=1";

            // chuẩn hóa NULL filter để đỡ phải check nhiều lần
            filter ??= new ProductFilterModel();

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand { Connection = conn };

            // Build điều kiện động + tham số
            if (!string.IsNullOrWhiteSpace(filter.TenSanPham))
            {
                sql += " AND TEN_SAN_PHAM LIKE @TEN_SAN_PHAM";
                cmd.Parameters.AddWithValue("@TEN_SAN_PHAM", "%" + filter.TenSanPham + "%");
            }

            if (!string.IsNullOrWhiteSpace(filter.LoHang))
            {
                sql += " AND MA_LO_HANG = @MA_LO_HANG";
                cmd.Parameters.AddWithValue("@MA_LO_HANG", filter.LoHang);
            }

            if (!string.IsNullOrWhiteSpace(filter.MaKhacHang))
            {
                sql += " AND MA_KHACH_HANG = @MA_KHACH_HANG";
                cmd.Parameters.AddWithValue("@MA_KHACH_HANG", filter.MaKhacHang);
            }

            sql += " ORDER BY CREATED_AT DESC";
            cmd.CommandText = sql;

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

                    LOAI_BAO = ToStringSafe(r["LOAI_BAO"]),

                    KT_DENIER = ToIntSafe(r["KT_DENIER"]),
                    KT_SOI_DOC = ToIntSafe(r["KT_SOI_DOC"]),
                    KT_SOI_NGANG = ToIntSafe(r["KT_SOI_NGANG"]),
                    KT_CHIEU_DAI = ToIntSafe(r["KT_CHIEU_DAI"]),
                    KT_CHIEU_RONG = ToIntSafe(r["KT_CHIEU_RONG"]),
                    KT_CHIEU_CAO = ToIntSafe(r["KT_CHIEU_CAO"]),

                    TC_DENIER = ToIntSafe(r["TC_DENIER"]),
                    TC_SOI_DOC = ToIntSafe(r["TC_SOI_DOC"]),
                    TC_SOI_NGANG = ToIntSafe(r["TC_SOI_NGANG"]),
                    TC_DUONG_KINH = ToIntSafe(r["TC_DUONG_KINH"]),
                    TC_CHIEU_CAO = ToIntSafe(r["TC_CHIEU_CAO"]),

                    TCD_DENIER = ToIntSafe(r["TCD_DENIER"]),
                    TCD_SOI_DOC = ToIntSafe(r["TCD_SOI_DOC"]),
                    TCD_SOI_NGANG = ToIntSafe(r["TCD_SOI_NGANG"]),
                    TCD_DUONG_KINH = ToIntSafe(r["TCD_DUONG_KINH"]),
                    TCD_CHIEU_CAO = ToIntSafe(r["TCD_CHIEU_CAO"]),

                    TRONG_TAI = ToIntSafe(r["TRONG_TAI"]),

                    LINER_DO_DAY = ToIntSafe(r["LINER_DO_DAY"]),
                    LINER_KHO = ToIntSafe(r["LINER_KHO"]),
                    LINER_DAI = ToIntSafe(r["LINER_DAI"]),
                    LINER_DO_DAY_STR = ToStringSafe(r["LINER_DO_DAY_STR"]),
                    LINER_CHIEU_RONG_STR = ToStringSafe(r["LINER_CHIEU_RONG_STR"]),
                    LINER_CHIEU_DAI_STR = ToStringSafe(r["LINER_CHIEU_DAI_STR"]),

                    TRANG = ToStringSafe(r["TRANG"]),
                    MAU_VAI = ToStringSafe(r["MAU_VAI"]),
                    CO_TRAN = ToStringSafe(r["CO_TRAN"]),

                    CO_DAI_DAY = ToStringSafe(r["CO_DAI_DAY"]),
                    CO_CHONG_PHINH = ToStringSafe(r["CO_CHONG_PHINH"]),
                    RONG_CHONG_PHINH = ToStringSafe(r["RONG_CHONG_PHINH"]),
                    CO_CHONG_XI = ToStringSafe(r["CO_CHONG_XI"]),
                    CO_DAY_CHONG_XI = ToStringSafe(r["CO_DAY_CHONG_XI"]),
                    CO_MEX = ToStringSafe(r["CO_MEX"]),
                    CO_NHAM = ToStringSafe(r["CO_NHAM"]),
                    CO_PE_DAY = ToStringSafe(r["CO_PE_DAY"]),
                    CO_PALLET = ToStringSafe(r["CO_PALLET"]),
                    GHI_CHU = ToStringSafe(r["GHI_CHU"]),

                    TD_RONG = ToIntSafe(r["TD_RONG"]),
                    TD_DAI = ToIntSafe(r["TD_DAI"]),
                    TD_DIEN_TICH = ToIntSafe(r["TD_DIEN_TICH"]),
                    TD_SO_LUONG = ToIntSafe(r["TD_SO_LUONG"]),
                    TD_TRONG_LUONG = ToIntSafe(r["TD_TRONG_LUONG"]),

                    TN_RONG = ToIntSafe(r["TN_RONG"]),
                    TN_DAI = ToIntSafe(r["TN_DAI"]),
                    TN_DIEN_TICH = ToIntSafe(r["TN_DIEN_TICH"]),
                    TN_SO_LUONG = ToIntSafe(r["TN_SO_LUONG"]),
                    TN_TRONG_LUONG = ToIntSafe(r["TN_TRONG_LUONG"]),

                    M_RONG = ToIntSafe(r["M_RONG"]),
                    M_DAI = ToIntSafe(r["M_DAI"]),
                    M_DIEN_TICH = ToIntSafe(r["M_DIEN_TICH"]),
                    M_SO_LUONG = ToIntSafe(r["M_SO_LUONG"]),
                    M_TRONG_LUONG = ToIntSafe(r["M_TRONG_LUONG"]),

                    TCM_RONG = ToIntSafe(r["TCM_RONG"]),
                    TCM_DAI = ToIntSafe(r["TCM_DAI"]),
                    TCM_DIEN_TICH = ToIntSafe(r["TCM_DIEN_TICH"]),
                    TCM_SO_LUONG = ToIntSafe(r["TCM_SO_LUONG"]),
                    TCM_TRONG_LUONG = ToIntSafe(r["TCM_TRONG_LUONG"]),

                    DTCM_RONG = ToIntSafe(r["DTCM_RONG"]),
                    DTCM_DAI = ToIntSafe(r["DTCM_DAI"]),
                    DTCM_SO_LUONG = ToIntSafe(r["DTCM_SO_LUONG"]),
                    DTCM_TRONG_LUONG = ToIntSafe(r["DTCM_TRONG_LUONG"]),

                    CM_LOAI_CHAC = ToIntSafe(r["CM_LOAI_CHAC"]),
                    CM_DAI = ToIntSafe(r["CM_DAI"]),
                    CM_SO_LUONG = ToIntSafe(r["CM_SO_LUONG"]),
                    CM_TRONG_LUONG = ToIntSafe(r["CM_TRONG_LUONG"]),

                    TCD_RONG = ToIntSafe(r["TCD_RONG"]),
                    TCD_DAI = ToIntSafe(r["TCD_DAI"]),
                    TCD_DIEN_TICH = ToIntSafe(r["TCD_DIEN_TICH"]),
                    TCD_SO_LUONG = ToIntSafe(r["TCD_SO_LUONG"]),
                    TCD_TRONG_LUONG = ToIntSafe(r["TCD_TRONG_LUONG"]),

                    TM_RONG = ToIntSafe(r["TM_RONG"]),
                    TM_DAI = ToIntSafe(r["TM_DAI"]),
                    TM_DIEN_TICH = ToIntSafe(r["TM_DIEN_TICH"]),
                    TM_SO_LUONG = ToIntSafe(r["TM_SO_LUONG"]),
                    TM_TRONG_LUONG = ToIntSafe(r["TM_TRONG_LUONG"]),

                    DTCD_RONG = ToIntSafe(r["DTCD_RONG"]),
                    DTCD_DAI = ToIntSafe(r["DTCD_DAI"]),
                    DTCD_SO_LUONG = ToIntSafe(r["DTCD_SO_LUONG"]),
                    DTCD_TRONG_LUONG = ToIntSafe(r["DTCD_TRONG_LUONG"]),

                    CD_LOAI_CHAC = ToIntSafe(r["CD_LOAI_CHAC"]),
                    CD_DAI = ToIntSafe(r["CD_DAI"]),
                    CD_SO_LUONG = ToIntSafe(r["CD_SO_LUONG"]),
                    CD_TRONG_LUONG = ToIntSafe(r["CD_TRONG_LUONG"]),

                    TUD_RONG = ToIntSafe(r["TUD_RONG"]),
                    TUD_DAI = ToIntSafe(r["TUD_DAI"]),
                    TUD_DIEN_TICH = ToIntSafe(r["TUD_DIEN_TICH"]),
                    TUD_SO_LUONG = ToIntSafe(r["TUD_SO_LUONG"]),
                    TUD_TRONG_LUONG = ToIntSafe(r["TUD_TRONG_LUONG"]),

                    CN_LOAI_CHAC = ToIntSafe(r["CN_LOAI_CHAC"]),
                    CN_MAU_CHAC = ToStringSafe(r["CN_MAU_CHAC"]),
                    CN_RONG = ToIntSafe(r["CN_RONG"]),
                    CN_DAI = ToIntSafe(r["CN_DAI"]),
                    CN_SO_LUONG = ToIntSafe(r["CN_SO_LUONG"]),
                    CN_TRONG_LUONG = ToIntSafe(r["CN_TRONG_LUONG"]),

                    DN_LOAI_CHAC = ToIntSafe(r["DN_LOAI_CHAC"]),
                    DN_MAU_CHAC = ToStringSafe(r["DN_MAU_CHAC"]),
                    DN_RONG = ToIntSafe(r["DN_RONG"]),
                    DN_DAI = ToIntSafe(r["DN_DAI"]),
                    DN_SO_LUONG = ToIntSafe(r["DN_SO_LUONG"]),
                    DN_TRONG_LUONG = ToIntSafe(r["DN_TRONG_LUONG"]),

                    DNG_LOAI_CHAC = ToIntSafe(r["DNG_LOAI_CHAC"]),
                    DNG_MAU_CHAC = ToStringSafe(r["DNG_MAU_CHAC"]),
                    DNG_RONG = ToIntSafe(r["DNG_RONG"]),
                    DNG_DAI = ToIntSafe(r["DNG_DAI"]),
                    DNG_SO_LUONG = ToIntSafe(r["DNG_SO_LUONG"]),
                    DNG_TRONG_LUONG = ToIntSafe(r["DNG_TRONG_LUONG"]),

                    POCKET_RONG = ToIntSafe(r["POCKET_RONG"]),
                    POCKET_DAI = ToIntSafe(r["POCKET_DAI"]),
                    POCKET_SO_LUONG = ToIntSafe(r["POCKET_SO_LUONG"]),
                    POCKET_TRONG_LUONG = ToIntSafe(r["POCKET_TRONG_LUONG"]),

                    DAP10x10_SO_LUONG = ToIntSafe(r["DAP10x10_SO_LUONG"]),
                    DAP10x10_TRONG_LUONG = ToIntSafe(r["DAP10x10_TRONG_LUONG"]),

                    CB_RONG = ToIntSafe(r["CB_RONG"]),
                    CB_DAI = ToIntSafe(r["CB_DAI"]),
                    CB_DIEN_TICH = ToIntSafe(r["CB_DIEN_TICH"]),
                    CB_SO_LUONG = ToIntSafe(r["CB_SO_LUONG"]),
                    CB_TRONG_LUONG = ToIntSafe(r["CB_TRONG_LUONG"]),

                    LINER_DODAY_CT = ToIntSafe(r["LINER_DODAY_CT"]),
                    LINER_KHO_CT = ToIntSafe(r["LINER_KHO_CT"]),
                    LINER_DAI_CT = ToIntSafe(r["LINER_DAI_CT"]),
                    LINER_SOLUONG_CT = ToIntSafe(r["LINER_SOLUONG_CT"]),
                    LINER_TRONGLUONG_CT = ToIntSafe(r["LINER_TRONGLUONG_CT"]),

                    CP_RONG = ToIntSafe(r["CP_RONG"]),
                    CP_DAI = ToIntSafe(r["CP_DAI"]),
                    CP_DIEN_TICH = ToIntSafe(r["CP_DIEN_TICH"]),
                    CP_SO_LUONG = ToIntSafe(r["CP_SO_LUONG"]),
                    CP_TRONG_LUONG = ToIntSafe(r["CP_TRONG_LUONG"]),

                    CX_CHIEU_DAI = ToIntSafe(r["CX_CHIEU_DAI"]),
                    CX_SO_LUONG = ToIntSafe(r["CX_SO_LUONG"]),
                    CX_TRONG_LUONG = ToIntSafe(r["CX_TRONG_LUONG"]),

                    CREATED_AT = r["CREATED_AT"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(r["CREATED_AT"])
                };

                list.Add(sp);
            }

            return list;
        }

        public async Task<bool> ExistsAsync(string maSp)
        {
            const string sql = "SELECT 1 FROM PRODUCT WHERE MA_SP = @ID";
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);
            await conn.OpenAsync();
            var obj = await cmd.ExecuteScalarAsync();
            return obj != null;
        }

        public async Task<bool> AddNewProductAsync(ProductIdentityModel p)
        {
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
    TEN_SAN_PHAM=@TEN_SAN_PHAM,
    MA_KHACH_HANG=@MA_KHACH_HANG,
    MA_LO_HANG=@MA_LO_HANG,
    MA_NHOM_HANG=@MA_NHOM_HANG,
    MA_HANG=@MA_HANG
WHERE MA_SP=@MA_SP;";

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
            => await UpdateProductInformationAsync(maSp, info);

        public async Task<bool> UpdateProductInformationAsync(string maSp, ProductInfoModel info)
        {
            const string sql = @"
UPDATE PRODUCT SET
    LOAI_BAO=@_LOAI_BAO,

    KT_DENIER=@KT_DENIER, KT_SOI_DOC=@KT_SOI_DOC, KT_SOI_NGANG=@KT_SOI_NGANG,
    KT_CHIEU_DAI=@KT_CHIEU_DAI, KT_CHIEU_RONG=@KT_CHIEU_RONG, KT_CHIEU_CAO=@KT_CHIEU_CAO,

    TC_DENIER=@TC_DENIER, TC_SOI_DOC=@TC_SOI_DOC, TC_SOI_NGANG=@TC_SOI_NGANG,
    TC_DUONG_KINH=@TC_DUONG_KINH, TC_CHIEU_CAO=@TC_CHIEU_CAO,

    TCD_DENIER=@TCD_DENIER, TCD_SOI_DOC=@TCD_SOI_DOC, TCD_SOI_NGANG=@TCD_SOI_NGANG,
    TCD_DUONG_KINH=@TCD_DUONG_KINH, TCD_CHIEU_CAO=@TCD_CHIEU_CAO,

    TRONG_TAI=@TRONG_TAI,

    LINER_DO_DAY=@LINER_DO_DAY, LINER_KHO=@LINER_KHO, LINER_DAI=@LINER_DAI,
    LINER_DO_DAY_STR=@LINER_DO_DAY_STR, LINER_CHIEU_RONG_STR=@LINER_CHIEU_RONG_STR, LINER_CHIEU_DAI_STR=@LINER_CHIEU_DAI_STR,

    TRANG=@TRANG, MAU_VAI=@MAU_VAI, CO_TRAN=@CO_TRAN,

    CO_DAI_DAY=@CO_DAI_DAY, CO_CHONG_PHINH=@CO_CHONG_PHINH, RONG_CHONG_PHINH=@RONG_CHONG_PHINH,
    CO_CHONG_XI=@CO_CHONG_XI, CO_DAY_CHONG_XI=@CO_DAY_CHONG_XI,
    CO_MEX=@CO_MEX, CO_NHAM=@CO_NHAM, CO_PE_DAY=@CO_PE_DAY, CO_PALLET=@CO_PALLET,

    GHI_CHU=@GHI_CHU
WHERE MA_SP=@ID;";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);

            cmd.Parameters.AddWithValue("@_LOAI_BAO", info.LoaiBao ?? "");

            cmd.Parameters.AddWithValue("@KT_DENIER", info.KTDenier);
            cmd.Parameters.AddWithValue("@KT_SOI_DOC", info.KTSoiDoc);
            cmd.Parameters.AddWithValue("@KT_SOI_NGANG", info.KTSoiNgang);
            cmd.Parameters.AddWithValue("@KT_CHIEU_DAI", info.KTChieuDai);
            cmd.Parameters.AddWithValue("@KT_CHIEU_RONG", info.KTChieuRong);
            cmd.Parameters.AddWithValue("@KT_CHIEU_CAO", info.KTChieuCao);

            cmd.Parameters.AddWithValue("@TC_DENIER", info.TCDenier);
            cmd.Parameters.AddWithValue("@TC_SOI_DOC", info.TCSoiDoc);
            cmd.Parameters.AddWithValue("@TC_SOI_NGANG", info.TCSoiNgang);
            cmd.Parameters.AddWithValue("@TC_DUONG_KINH", info.TCDuongKinh);
            cmd.Parameters.AddWithValue("@TC_CHIEU_CAO", info.TCChieuCao);

            cmd.Parameters.AddWithValue("@TCD_DENIER", info.TCDDenier);
            cmd.Parameters.AddWithValue("@TCD_SOI_DOC", info.TCDSoiDoc);
            cmd.Parameters.AddWithValue("@TCD_SOI_NGANG", info.TCDSoiNgang);
            cmd.Parameters.AddWithValue("@TCD_DUONG_KINH", info.TCDDuongKinh);
            cmd.Parameters.AddWithValue("@TCD_CHIEU_CAO", info.TCDChieuCao);

            cmd.Parameters.AddWithValue("@TRONG_TAI", info.TrongTai);

            cmd.Parameters.AddWithValue("@LINER_DO_DAY", info.LinerDoDay);
            cmd.Parameters.AddWithValue("@LINER_KHO", info.LinerKho);
            cmd.Parameters.AddWithValue("@LINER_DAI", info.LinerDai);

            cmd.Parameters.AddWithValue("@LINER_DO_DAY_STR", info.DoDayLiner ?? "");
            cmd.Parameters.AddWithValue("@LINER_CHIEU_RONG_STR", info.ChieuRongLiner ?? "");
            cmd.Parameters.AddWithValue("@LINER_CHIEU_DAI_STR", info.ChieuDaiLiner ?? "");

            cmd.Parameters.AddWithValue("@TRANG", info.Trang ?? "");
            cmd.Parameters.AddWithValue("@MAU_VAI", info.MauVai ?? "");
            cmd.Parameters.AddWithValue("@CO_TRAN", info.CoTran ?? "");

            cmd.Parameters.AddWithValue("@CO_DAI_DAY", info.CoDaiDay ?? "");
            cmd.Parameters.AddWithValue("@CO_CHONG_PHINH", info.CoChongPhinh ?? "");
            cmd.Parameters.AddWithValue("@RONG_CHONG_PHINH", info.RongChongPhinh ?? "");
            cmd.Parameters.AddWithValue("@CO_CHONG_XI", info.CoChongXi ?? "");
            cmd.Parameters.AddWithValue("@CO_DAY_CHONG_XI", info.CoDayChongXi ?? "");
            cmd.Parameters.AddWithValue("@CO_MEX", info.CoMex ?? "");
            cmd.Parameters.AddWithValue("@CO_NHAM", info.CoNham ?? "");
            cmd.Parameters.AddWithValue("@CO_PE_DAY", info.CoPeDay ?? "");
            cmd.Parameters.AddWithValue("@CO_PALLET", info.CoPallet ?? "");
            cmd.Parameters.AddWithValue("@GHI_CHU", info.GhiChu ?? "");

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> AddProductDetailInformationAsync(string maSp, ProductDetailModel d)
            => await UpdateProductDetailInformationAsync(maSp, d);

        public async Task<bool> UpdateProductDetailInformationAsync(string maSp, ProductDetailModel d)
        {
            const string sql = @"
UPDATE PRODUCT SET
    TD_RONG=@TD_RONG, TD_DAI=@TD_DAI, TD_DIEN_TICH=@TD_DIEN_TICH, TD_SO_LUONG=@TD_SO_LUONG, TD_TRONG_LUONG=@TD_TRONG_LUONG,
    TN_RONG=@TN_RONG, TN_DAI=@TN_DAI, TN_DIEN_TICH=@TN_DIEN_TICH, TN_SO_LUONG=@TN_SO_LUONG, TN_TRONG_LUONG=@TN_TRONG_LUONG,
    M_RONG=@M_RONG, M_DAI=@M_DAI, M_DIEN_TICH=@M_DIEN_TICH, M_SO_LUONG=@M_SO_LUONG, M_TRONG_LUONG=@M_TRONG_LUONG,
    TCM_RONG=@TCM_RONG, TCM_DAI=@TCM_DAI, TCM_DIEN_TICH=@TCM_DIEN_TICH, TCM_SO_LUONG=@TCM_SO_LUONG, TCM_TRONG_LUONG=@TCM_TRONG_LUONG,
    DTCM_RONG=@DTCM_RONG, DTCM_DAI=@DTCM_DAI, DTCM_SO_LUONG=@DTCM_SO_LUONG, DTCM_TRONG_LUONG=@DTCM_TRONG_LUONG,
    CM_LOAI_CHAC=@CM_LOAI_CHAC, CM_DAI=@CM_DAI, CM_SO_LUONG=@CM_SO_LUONG, CM_TRONG_LUONG=@CM_TRONG_LUONG,
    TCD_RONG=@TCD_RONG, TCD_DAI=@TCD_DAI, TCD_DIEN_TICH=@TCD_DIEN_TICH, TCD_SO_LUONG=@TCD_SO_LUONG, TCD_TRONG_LUONG=@TCD_TRONG_LUONG,
    TM_RONG=@TM_RONG, TM_DAI=@TM_DAI, TM_DIEN_TICH=@TM_DIEN_TICH, TM_SO_LUONG=@TM_SO_LUONG, TM_TRONG_LUONG=@TM_TRONG_LUONG,
    DTCD_RONG=@DTCD_RONG, DTCD_DAI=@DTCD_DAI, DTCD_SO_LUONG=@DTCD_SO_LUONG, DTCD_TRONG_LUONG=@DTCD_TRONG_LUONG,
    CD_LOAI_CHAC=@CD_LOAI_CHAC, CD_DAI=@CD_DAI, CD_SO_LUONG=@CD_SO_LUONG, CD_TRONG_LUONG=@CD_TRONG_LUONG,
    TUD_RONG=@TUD_RONG, TUD_DAI=@TUD_DAI, TUD_DIEN_TICH=@TUD_DIEN_TICH, TUD_SO_LUONG=@TUD_SO_LUONG, TUD_TRONG_LUONG=@TUD_TRONG_LUONG,
    CN_LOAI_CHAC=@CN_LOAI_CHAC, CN_MAU_CHAC=@CN_MAU_CHAC, CN_RONG=@CN_RONG, CN_DAI=@CN_DAI, CN_SO_LUONG=@CN_SO_LUONG, CN_TRONG_LUONG=@CN_TRONG_LUONG,
    DN_LOAI_CHAC=@DN_LOAI_CHAC, DN_MAU_CHAC=@DN_MAU_CHAC, DN_RONG=@DN_RONG, DN_DAI=@DN_DAI, DN_SO_LUONG=@DN_SO_LUONG, DN_TRONG_LUONG=@DN_TRONG_LUONG,
    DNG_LOAI_CHAC=@DNG_LOAI_CHAC, DNG_MAU_CHAC=@DNG_MAU_CHAC, DNG_RONG=@DNG_RONG, DNG_DAI=@DNG_DAI, DNG_SO_LUONG=@DNG_SO_LUONG, DNG_TRONG_LUONG=@DNG_TRONG_LUONG,
    POCKET_RONG=@POCKET_RONG, POCKET_DAI=@POCKET_DAI, POCKET_SO_LUONG=@POCKET_SO_LUONG, POCKET_TRONG_LUONG=@POCKET_TRONG_LUONG,
    DAP10x10_SO_LUONG=@DAP10x10_SO_LUONG, DAP10x10_TRONG_LUONG=@DAP10x10_TRONG_LUONG,
    CB_RONG=@CB_RONG, CB_DAI=@CB_DAI, CB_DIEN_TICH=@CB_DIEN_TICH, CB_SO_LUONG=@CB_SO_LUONG, CB_TRONG_LUONG=@CB_TRONG_LUONG,
    LINER_DODAY_CT=@LINER_DODAY_CT, LINER_KHO_CT=@LINER_KHO_CT, LINER_DAI_CT=@LINER_DAI_CT, LINER_SOLUONG_CT=@LINER_SOLUONG_CT, LINER_TRONGLUONG_CT=@LINER_TRONGLUONG_CT,
    CP_RONG=@CP_RONG, CP_DAI=@CP_DAI, CP_DIEN_TICH=@CP_DIEN_TICH, CP_SO_LUONG=@CP_SO_LUONG, CP_TRONG_LUONG=@CP_TRONG_LUONG,
    CX_CHIEU_DAI=@CX_CHIEU_DAI, CX_SO_LUONG=@CX_SO_LUONG, CX_TRONG_LUONG=@CX_TRONG_LUONG
WHERE MA_SP=@ID;";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);

            cmd.Parameters.AddWithValue("@TD_RONG", d.TDRong);
            cmd.Parameters.AddWithValue("@TD_DAI", d.TDDai);
            cmd.Parameters.AddWithValue("@TD_DIEN_TICH", d.TDDienTich);
            cmd.Parameters.AddWithValue("@TD_SO_LUONG", d.TDSoLuong);
            cmd.Parameters.AddWithValue("@TD_TRONG_LUONG", d.TDTrongLuong);

            cmd.Parameters.AddWithValue("@TN_RONG", d.TNRong);
            cmd.Parameters.AddWithValue("@TN_DAI", d.TNDai);
            cmd.Parameters.AddWithValue("@TN_DIEN_TICH", d.TNDienTich);
            cmd.Parameters.AddWithValue("@TN_SO_LUONG", d.TNSoLuong);
            cmd.Parameters.AddWithValue("@TN_TRONG_LUONG", d.TNTrongLuong);

            cmd.Parameters.AddWithValue("@M_RONG", d.MRong);
            cmd.Parameters.AddWithValue("@M_DAI", d.MDai);
            cmd.Parameters.AddWithValue("@M_DIEN_TICH", d.MDienTich);
            cmd.Parameters.AddWithValue("@M_SO_LUONG", d.MSoLuong);
            cmd.Parameters.AddWithValue("@M_TRONG_LUONG", d.MTrongLuong);

            cmd.Parameters.AddWithValue("@TCM_RONG", d.TCMRong);
            cmd.Parameters.AddWithValue("@TCM_DAI", d.TCMDai);
            cmd.Parameters.AddWithValue("@TCM_DIEN_TICH", d.TCMDienTich);
            cmd.Parameters.AddWithValue("@TCM_SO_LUONG", d.TCMSoLuong);
            cmd.Parameters.AddWithValue("@TCM_TRONG_LUONG", d.TCMTrongLuong);

            cmd.Parameters.AddWithValue("@DTCM_RONG", d.DTCMRong);
            cmd.Parameters.AddWithValue("@DTCM_DAI", d.DTCMDai);
            cmd.Parameters.AddWithValue("@DTCM_SO_LUONG", d.DTCMSoLuong);
            cmd.Parameters.AddWithValue("@DTCM_TRONG_LUONG", d.DTCMTrongLuong);

            cmd.Parameters.AddWithValue("@CM_LOAI_CHAC", d.CMLoaiChac);
            cmd.Parameters.AddWithValue("@CM_DAI", d.CMDai);
            cmd.Parameters.AddWithValue("@CM_SO_LUONG", d.CMSoLuong);
            cmd.Parameters.AddWithValue("@CM_TRONG_LUONG", d.CMTrongLuong);

            cmd.Parameters.AddWithValue("@TCD_RONG", d.TCDRong);
            cmd.Parameters.AddWithValue("@TCD_DAI", d.TCDDai);
            cmd.Parameters.AddWithValue("@TCD_DIEN_TICH", d.TCDDienTich);
            cmd.Parameters.AddWithValue("@TCD_SO_LUONG", d.TCDSoLuong);
            cmd.Parameters.AddWithValue("@TCD_TRONG_LUONG", d.TCDTrongLuong);

            cmd.Parameters.AddWithValue("@TM_RONG", d.TMRong);
            cmd.Parameters.AddWithValue("@TM_DAI", d.TMDai);
            cmd.Parameters.AddWithValue("@TM_DIEN_TICH", d.TMDienTich);
            cmd.Parameters.AddWithValue("@TM_SO_LUONG", d.TMSoLuong);
            cmd.Parameters.AddWithValue("@TM_TRONG_LUONG", d.TMTrongLuong);

            cmd.Parameters.AddWithValue("@DTCD_RONG", d.DTDMRong);
            cmd.Parameters.AddWithValue("@DTCD_DAI", d.DTCDDai);
            cmd.Parameters.AddWithValue("@DTCD_SO_LUONG", d.DTCDSoLuong);
            cmd.Parameters.AddWithValue("@DTCD_TRONG_LUONG", d.DTCDTrongLuong);

            cmd.Parameters.AddWithValue("@CD_LOAI_CHAC", d.CDLoaiChac);
            cmd.Parameters.AddWithValue("@CD_DAI", d.CDDai);
            cmd.Parameters.AddWithValue("@CD_SO_LUONG", d.CDSoLuong);
            cmd.Parameters.AddWithValue("@CD_TRONG_LUONG", d.CDTrongLuong);

            cmd.Parameters.AddWithValue("@TUD_RONG", d.TUDRong);
            cmd.Parameters.AddWithValue("@TUD_DAI", d.TUDDai);
            cmd.Parameters.AddWithValue("@TUD_DIEN_TICH", d.TUDienTich);
            cmd.Parameters.AddWithValue("@TUD_SO_LUONG", d.TUDSoLuong);
            cmd.Parameters.AddWithValue("@TUD_TRONG_LUONG", d.TUDTrongLuong);

            cmd.Parameters.AddWithValue("@CN_LOAI_CHAC", d.CNLoaiChac);
            cmd.Parameters.AddWithValue("@CN_MAU_CHAC", d.CNMauChac ?? "");
            cmd.Parameters.AddWithValue("@CN_RONG", d.CNRong);
            cmd.Parameters.AddWithValue("@CN_DAI", d.CNDai);
            cmd.Parameters.AddWithValue("@CN_SO_LUONG", d.CNSoLuong);
            cmd.Parameters.AddWithValue("@CN_TRONG_LUONG", d.CNTrongLuong);

            cmd.Parameters.AddWithValue("@DN_LOAI_CHAC", d.DNLoaiChac);
            cmd.Parameters.AddWithValue("@DN_MAU_CHAC", d.DNMauChac ?? "");
            cmd.Parameters.AddWithValue("@DN_RONG", d.DNRong);
            cmd.Parameters.AddWithValue("@DN_DAI", d.DNDai);
            cmd.Parameters.AddWithValue("@DN_SO_LUONG", d.DNSoLuong);
            cmd.Parameters.AddWithValue("@DN_TRONG_LUONG", d.DNTrongLuong);

            cmd.Parameters.AddWithValue("@DNG_LOAI_CHAC", d.DNGLoaiChac);
            cmd.Parameters.AddWithValue("@DNG_MAU_CHAC", d.DNGMauChac ?? "");
            cmd.Parameters.AddWithValue("@DNG_RONG", d.DNGRong);
            cmd.Parameters.AddWithValue("@DNG_DAI", d.DNGDai);
            cmd.Parameters.AddWithValue("@DNG_SO_LUONG", d.DNGSoLuong);
            cmd.Parameters.AddWithValue("@DNG_TRONG_LUONG", d.DNGTrongLuong);

            cmd.Parameters.AddWithValue("@POCKET_RONG", d.PocketRong);
            cmd.Parameters.AddWithValue("@POCKET_DAI", d.PocketDai);
            cmd.Parameters.AddWithValue("@POCKET_SO_LUONG", d.PocketSoLuong);
            cmd.Parameters.AddWithValue("@POCKET_TRONG_LUONG", d.PocketTrongLuong);

            cmd.Parameters.AddWithValue("@DAP10x10_SO_LUONG", d.Dap10x10SoLuong);
            cmd.Parameters.AddWithValue("@DAP10x10_TRONG_LUONG", d.Dap10x10TrongLuong);

            cmd.Parameters.AddWithValue("@CB_RONG", d.ChongBanRong);
            cmd.Parameters.AddWithValue("@CB_DAI", d.ChongBanDai);
            cmd.Parameters.AddWithValue("@CB_DIEN_TICH", d.ChongBanDienTich);
            cmd.Parameters.AddWithValue("@CB_SO_LUONG", d.ChongBanSoLuong);
            cmd.Parameters.AddWithValue("@CB_TRONG_LUONG", d.ChongBanTrongLuong);

            cmd.Parameters.AddWithValue("@LINER_DODAY_CT", d.LinerDoDayCt);
            cmd.Parameters.AddWithValue("@LINER_KHO_CT", d.LinerKhoCt);
            cmd.Parameters.AddWithValue("@LINER_DAI_CT", d.LinerDaiCt);
            cmd.Parameters.AddWithValue("@LINER_SOLUONG_CT", d.LinerSoLuongCt);
            cmd.Parameters.AddWithValue("@LINER_TRONGLUONG_CT", d.LinerTrongLuongCt);

            cmd.Parameters.AddWithValue("@CP_RONG", d.ChongPhinhRong);
            cmd.Parameters.AddWithValue("@CP_DAI", d.ChongPhinhDai);
            cmd.Parameters.AddWithValue("@CP_DIEN_TICH", d.ChongPhinhDienTich);
            cmd.Parameters.AddWithValue("@CP_SO_LUONG", d.ChongPhinhSoLuong);
            cmd.Parameters.AddWithValue("@CP_TRONG_LUONG", d.ChongPhinhTrongLuong);

            cmd.Parameters.AddWithValue("@CX_CHIEU_DAI", d.ChongXiChieuDai);
            cmd.Parameters.AddWithValue("@CX_SO_LUONG", d.ChongXiSoLuong);
            cmd.Parameters.AddWithValue("@CX_TRONG_LUONG", d.ChongXiTrongLuong);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(string maSp)
        {
            const string sql = @"DELETE FROM PRODUCT WHERE MA_SP = @ID;";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(maSp);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

    }
}
