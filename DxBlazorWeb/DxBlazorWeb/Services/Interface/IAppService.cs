using DxBlazorWeb.Model;

namespace DxBlazorWeb.Services.Interface
{
    public interface IAppService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        //Task<Customer?> GetCustomerByIdAsync(string id);
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(string id);
        Task<List<Output>> GetAllOutputAsync();
        Task<bool> AddOutputAsync(Output output);
        Task<bool> UpdateOutputAsync(Output output);
        Task<bool> DeleteOutputAsync(string id);
        Task<List<CustomerCare>> GetAllCustomerCareAsync();
        Task<bool> UpdateCustomerCareAsync(CustomerCare item);

        Task<List<OrderView>> GetAllOrdersAsync();
        Task<bool> AddOrderAsync(Order item);
        Task<bool> UpdateOrderAsync(Order item);
        Task<bool> DeleteOrderAsync(string id);



        Task<List<ShippingConditional>> GetAllShippingConditionalsAsync();
        Task<bool> AddShippingConditionalAsync(ShippingConditional item);
        Task<bool> UpdateShippingConditionalAsync(ShippingConditional item);
        Task<bool> DeleteShippingConditionalAsync(string id);
        
        //Task AddProductAsync(Product product);

        // ... tương tự cho các entity khác
    }
}
