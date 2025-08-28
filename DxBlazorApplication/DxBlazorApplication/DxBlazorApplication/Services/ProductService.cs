using DxBlazorApplication.Model;
using DxBlazorApplication.Repositories.Interface;
using DxBlazorApplication.Services.Interface;

namespace DxBlazorApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public Task<List<SanPham>> GetAllProductsAsync( ProductFilterModel filter)
            => _repo.GetAllProductsAsync(filter);

        public Task<bool> ExistsAsync(string maSp)
            => _repo.ExistsAsync(maSp);

        public Task<bool> AddNewProductAsync(ProductIdentityModel product)
            => _repo.AddNewProductAsync(product);

        public Task<bool> UpdateProductIdentityAsync(ProductIdentityModel identityPart)
            => _repo.UpdateProductIdentityAsync(identityPart);

        public Task<bool> AddProductInformationAsync(string maSp, ProductInfoModel infoPart)
            => _repo.AddProductInformationAsync(maSp, infoPart);

        public Task<bool> UpdateProductInformationAsync(string maSp, ProductInfoModel infoPart)
            => _repo.UpdateProductInformationAsync(maSp, infoPart);

        public Task<bool> AddProductDetailInformationAsync(string maSp, ProductDetailModel detailPart)
            => _repo.AddProductDetailInformationAsync(maSp, detailPart);

        public Task<bool> UpdateProductDetailInformationAsync(string maSp, ProductDetailModel detailPart)
            => _repo.UpdateProductDetailInformationAsync(maSp, detailPart);

        public Task<bool> DeleteProductAsync(string maSp)
            => _repo.DeleteProductAsync(maSp);

    }
}
    