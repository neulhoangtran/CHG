using DxBlazorApplication.Model;
using DxBlazorApplication.Repositories.Interface;
using DxBlazorApplication.Services.Interface;

public class AppService : IAppService
{
    private readonly IAppRepository _repo;

    public AppService(IAppRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        try
        {
            return await _repo.GetAllCustomersAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi lấy danh sách khách hàng: {ex.Message}");
            return new List<Customer>(); // Trả về danh sách rỗng nếu lỗi
        }
    }
    //public Task<Customer?> GetCustomerByIdAsync(string id) => _repo.GetCustomerByIdAsync(id);
    public async Task<bool> AddCustomerAsync(Customer customer)
    {
        try
        {
            await _repo.AddCustomerAsync(customer);
            return true;
        }
        catch (Exception ex)
        {
            // Bạn có thể log lỗi tại đây nếu cần
            Console.WriteLine($"Lỗi khi thêm khách hàng: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            await _repo.UpdateCustomerAsync(customer);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi cập nhật khách hàng: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteCustomerAsync(string id)
    {
        try
        {
            await _repo.DeleteCustomerAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi xóa khách hàng: {ex.Message}");
            return false;
        }
    }
    public async Task<List<CustomerCare>> GetAllCustomerCareAsync()
    {
        try { return await _repo.GetAllCustomerCareAsync(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); return new(); }
    }

    public async Task<bool> UpdateCustomerCareAsync(CustomerCare item)
    {
        try { await _repo.UpdateCustomerCareAsync(item); return true; }
        catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
    }

    public async Task<List<OrderView>> GetAllOrdersAsync()
    {
        try { return await _repo.GetAllOrdersAsync(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); return new(); }
    }
    public async Task<bool> AddOrderAsync(Order item)
    {
        try { await _repo.AddOrderAsync(item); return true; }
        catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
    }
    public async Task<bool> UpdateOrderAsync(Order item)
    {
        try { await _repo.UpdateOrderAsync(item); return true; }
        catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
    }
    public async Task<bool> DeleteOrderAsync(string id)
    {
        try { await _repo.DeleteOrderAsync(id); return true; }
        catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
    }






    public async Task<List<Output>> GetAllOutputAsync()
    {
        try
        {
            return await _repo.GetAllOutputAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi GetAllOutput: " + ex.Message);
            return new List<Output>();
        }
    }

    public async Task<bool> AddOutputAsync(Output output)
    {
        try
        {
            await _repo.AddOutputAsync(output);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi AddOutput: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> UpdateOutputAsync(Output output)
    {
        try
        {
            await _repo.UpdateOutputAsync(output);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi UpdateOutput: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteOutputAsync(string id)
    {
        try
        {
            await _repo.DeleteOutputAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi DeleteOutput: " + ex.Message);
            return false;
        }
    }

    public async Task<List<ShippingConditional>> GetAllShippingConditionalsAsync()
    {
        try { return await _repo.GetAllShippingConditionalsAsync(); }
        catch (Exception ex) { Console.WriteLine(ex.Message); return new(); }
    }

    public async Task<bool> AddShippingConditionalAsync(ShippingConditional item)
    {
        try { await _repo.AddShippingConditionalAsync(item); return true; }
        catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
    }

    public async Task<bool> UpdateShippingConditionalAsync(ShippingConditional item)
    {
        try { await _repo.UpdateShippingConditionalAsync(item); return true; }
        catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
    }

    public async Task<bool> DeleteShippingConditionalAsync(string id)
    {
        try { await _repo.DeleteShippingConditionalAsync(id); return true; }
        catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
    }

    //public Task<List<Product>> GetAllProductsAsync() => _repo.GetAllProductsAsync();
    //public Task AddProductAsync(Product product) => _repo.AddProductAsync(product);

    // ... tiếp tục cho các entity khác
}
