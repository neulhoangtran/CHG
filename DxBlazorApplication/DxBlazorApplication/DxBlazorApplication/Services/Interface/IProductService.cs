using DxBlazorApplication.Model;

namespace DxBlazorApplication.Services.Interface
{
    public interface IProductService
    {
        Task<List<SanPham>> GetAllProductsAsync(ProductFilterModel filter);
        Task<bool> AddNewProductAsync(ProductIdentityModel product);
        Task<bool> UpdateProductIdentityAsync(ProductIdentityModel product);

        Task<bool> AddProductInformationAsync(string maSp, ProductInfoModel infoPart);
        Task<bool> AddProductDetailInformationAsync(string maSp, ProductDetailModel detailPart);

        Task<bool> UpdateProductInformationAsync(string maSp, ProductInfoModel infoPart);
        Task<bool> UpdateProductDetailInformationAsync(string maSp, ProductDetailModel detailPart);
        Task<bool> ExistsAsync(string maSp);
        Task<bool> DeleteProductAsync(string maSp);
    }
}
