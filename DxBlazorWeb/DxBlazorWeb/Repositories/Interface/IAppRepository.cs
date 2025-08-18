using DxBlazorWeb.Model;

namespace DxBlazorWeb.Repositories.Interface
{
    public interface IAppRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<List<Output>> GetAllOutputAsync();
        //Task<Customer?> GetCustomerByIdAsync(string id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(string id);
        Task AddOutputAsync(Output output);
        Task UpdateOutputAsync(Output output);
        Task DeleteOutputAsync(string id);
        Task<List<CustomerCare>> GetAllCustomerCareAsync();
        Task UpdateCustomerCareAsync(CustomerCare item);
        Task<List<OrderView>> GetAllOrdersAsync();
        Task AddOrderAsync(Order item);
        Task UpdateOrderAsync(Order item);
        Task DeleteOrderAsync(string id);
        Task<List<ShippingConditional>> GetAllShippingConditionalsAsync();
        Task AddShippingConditionalAsync(ShippingConditional item);
        Task UpdateShippingConditionalAsync(ShippingConditional item);
        Task DeleteShippingConditionalAsync(string id);

        // ... tiếp tục với các entity khác như ShippingConditional
    }
}
