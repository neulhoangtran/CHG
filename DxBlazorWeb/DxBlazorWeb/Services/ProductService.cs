using DxBlazorWeb.Model;
using DxBlazorWeb.Repositories.Interface;
using DxBlazorWeb.Services.Interface;

namespace DxBlazorWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public Task<List<SanPham>> GetAllProductsAsync()
        => _repo.GetAllProductsAsync();
        public Task<bool> ExistsAsync(string maSp) => _repo.ExistsAsync(maSp);

        public async Task<bool> AddNewProductAsync(ProductIdentityModel product)
        {
            // nếu muốn: kiểm tra trùng MA_HANG, v.v… tại đây
            return await _repo.AddNewProductAsync(product);
        }

        public Task<bool> AddProductInformationAsync(string maSp, ProductInfoModel infoPart)
            => _repo.AddProductInformationAsync(maSp, infoPart);

        public Task<bool> AddProductDetailInformationAsync(string maSp, ProductDetailModel detailPart)
            => _repo.AddProductDetailInformationAsync(maSp, detailPart);

        public Task<bool> UpdateProductIdentityAsync(ProductIdentityModel identityPart)
            => _repo.UpdateProductIdentityAsync(identityPart);

        public Task<bool> UpdateProductInformationAsync(string maSp, ProductInfoModel infoPart)
            => _repo.UpdateProductInformationAsync(maSp, infoPart);

        public Task<bool> UpdateProductDetailInformationAsync(string maSp, ProductDetailModel detailPart)
            => _repo.UpdateProductDetailInformationAsync(maSp, detailPart);
    }
}
