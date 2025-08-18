using DxBlazorWeb.Model;

namespace DxBlazorWeb.Repositories.Interface
{
    public interface IProductRepository
    {
        // Create
        Task<bool> AddNewProductAsync(SanPham identityPart);              // chỉ tab 1
        Task<bool> AddProductInformationAsync(string maSp, ProductInfoModel infoPart);      // tab 2
        Task<bool> AddProductDetailInformationAsync(string maSp, ProductDetailModel detailPart); // tab 3

        // Update theo phần
        Task<bool> UpdateProductIdentityAsync(SanPham identityPart);      // tab 1
        Task<bool> UpdateProductInformationAsync(string maSp, ProductInfoModel infoPart);   // tab 2
        Task<bool> UpdateProductDetailInformationAsync(string maSp, ProductDetailModel detailPart); // tab 3

        // Utils (tuỳ chọn)
        Task<bool> ExistsAsync(string maSp);
    }

}
