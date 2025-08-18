using DxBlazorWeb.Model;

namespace DxBlazorWeb.Services.Interface
{
    public interface IProductService
    {
        Task<bool> AddNewProductAsync(SanPham identityPart);
        Task<bool> AddProductInformationAsync(string maSp, ProductInfoModel infoPart);
        Task<bool> AddProductDetailInformationAsync(string maSp, ProductDetailModel detailPart);
        Task<bool> UpdateProductIdentityAsync(SanPham identityPart);
        Task<bool> UpdateProductInformationAsync(string maSp, ProductInfoModel infoPart);
        Task<bool> UpdateProductDetailInformationAsync(string maSp, ProductDetailModel detailPart);
        Task<bool> ExistsAsync(string maSp);
    }
}
