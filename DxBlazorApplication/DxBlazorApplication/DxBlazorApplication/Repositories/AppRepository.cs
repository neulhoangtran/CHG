using System.Data;
using DxBlazorApplication.Model;
using DxBlazorApplication.Repositories.Interface;
using Microsoft.Data.SqlClient;

public class AppRepository : IAppRepository
{
    private readonly string _connStr;

    public AppRepository(IConfiguration config)
    {
        _connStr = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        var result = new List<Customer>();
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        SELECT ID, CUSTOMER_NAME, CUSTOMER_CODE,
               CUSTOMER_MANAGER, CUSTOMER_POSITION, CUSTOMER_EMAIL, CUSTOMER_PHONE,
               CUSTOMER_ADDRESS, CUSTOMER_KAKAO, CUSTOMER_ZALO, CUSTOMER_DETAIL
        FROM CUSTOMER
        ORDER BY CREATED_AT DESC", conn);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new Customer
            {
                ID = reader["ID"].ToString() ?? "",
                CustomerName = reader["CUSTOMER_NAME"].ToString() ?? "",
                CustomerCode = reader["CUSTOMER_CODE"].ToString() ?? "",
                CustomerManager = reader["CUSTOMER_MANAGER"].ToString() ?? "",
                CustomerPosition = reader["CUSTOMER_POSITION"].ToString() ?? "",
                CustomerEmail = reader["CUSTOMER_EMAIL"].ToString() ?? "",
                CustomerPhone = reader["CUSTOMER_PHONE"].ToString() ?? "",
                CustomerAddress = reader["CUSTOMER_ADDRESS"].ToString() ?? "",
                CustomerKakao = reader["CUSTOMER_KAKAO"].ToString() ?? "",
                CustomerZalo = reader["CUSTOMER_ZALO"].ToString() ?? "",
                CustomerDetail = reader["CUSTOMER_DETAIL"].ToString() ?? ""
            });
        }
        return result;
    }
    public async Task<List<Output>> GetAllOutputAsync()
    {
        var result = new List<Output>();

        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("SELECT * FROM OUTPUT ORDER BY CREATED_AT DESC", conn);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new Output
            {
                ID = reader["ID"].ToString() ?? "",
                OutputName = reader["OUTPUT_NAME"].ToString() ?? "",
                Level1 = reader.GetDecimal(reader.GetOrdinal("LEVEL1")),
                Level2 = reader.GetDecimal(reader.GetOrdinal("LEVEL2")),
                Level3 = reader.GetDecimal(reader.GetOrdinal("LEVEL3"))
            });
        }

        return result;
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        INSERT INTO CUSTOMER (
            ID, CUSTOMER_NAME, CUSTOMER_CODE,
            CUSTOMER_MANAGER, CUSTOMER_POSITION, CUSTOMER_EMAIL, CUSTOMER_PHONE,
            CUSTOMER_ADDRESS, CUSTOMER_KAKAO, CUSTOMER_ZALO, CUSTOMER_DETAIL, CREATED_AT
        ) VALUES (
            @ID, @NAME, @CODE,
            @MANAGER, @POSITION, @EMAIL, @PHONE,
            @ADDRESS, @KAKAO, @ZALO, @DETAIL, GETDATE()
        )", conn);

        cmd.Parameters.Add("@ID", System.Data.SqlDbType.UniqueIdentifier)
                      .Value = Guid.Parse(customer.ID);
        cmd.Parameters.AddWithValue("@NAME", customer.CustomerName ?? "");
        cmd.Parameters.AddWithValue("@CODE", customer.CustomerCode ?? "");
        cmd.Parameters.AddWithValue("@MANAGER", customer.CustomerManager ?? "");
        cmd.Parameters.AddWithValue("@POSITION", customer.CustomerPosition ?? "");
        cmd.Parameters.AddWithValue("@EMAIL", customer.CustomerEmail ?? "");
        cmd.Parameters.AddWithValue("@PHONE", customer.CustomerPhone ?? "");
        cmd.Parameters.AddWithValue("@ADDRESS", customer.CustomerAddress ?? "");
        cmd.Parameters.AddWithValue("@KAKAO", customer.CustomerKakao ?? "");
        cmd.Parameters.AddWithValue("@ZALO", customer.CustomerZalo ?? "");
        cmd.Parameters.AddWithValue("@DETAIL", customer.CustomerDetail ?? "");

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        UPDATE CUSTOMER SET
            CUSTOMER_NAME = @NAME,
            CUSTOMER_CODE = @CODE,
            CUSTOMER_MANAGER = @MANAGER,
            CUSTOMER_POSITION = @POSITION,
            CUSTOMER_EMAIL = @EMAIL,
            CUSTOMER_PHONE = @PHONE,
            CUSTOMER_ADDRESS = @ADDRESS,
            CUSTOMER_KAKAO = @KAKAO,
            CUSTOMER_ZALO = @ZALO,
            CUSTOMER_DETAIL = @DETAIL
        WHERE ID = @ID", conn);

        cmd.Parameters.Add("@ID", System.Data.SqlDbType.UniqueIdentifier)
                      .Value = Guid.Parse(customer.ID);
        cmd.Parameters.AddWithValue("@NAME", customer.CustomerName ?? "");
        cmd.Parameters.AddWithValue("@CODE", customer.CustomerCode ?? "");
        cmd.Parameters.AddWithValue("@MANAGER", customer.CustomerManager ?? "");
        cmd.Parameters.AddWithValue("@POSITION", customer.CustomerPosition ?? "");
        cmd.Parameters.AddWithValue("@EMAIL", customer.CustomerEmail ?? "");
        cmd.Parameters.AddWithValue("@PHONE", customer.CustomerPhone ?? "");
        cmd.Parameters.AddWithValue("@ADDRESS", customer.CustomerAddress ?? "");
        cmd.Parameters.AddWithValue("@KAKAO", customer.CustomerKakao ?? "");
        cmd.Parameters.AddWithValue("@ZALO", customer.CustomerZalo ?? "");
        cmd.Parameters.AddWithValue("@DETAIL", customer.CustomerDetail ?? "");

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteCustomerAsync(string id)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("DELETE FROM CUSTOMER WHERE ID = @ID", conn);
        cmd.Parameters.AddWithValue("@ID", id);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<CustomerCare>> GetAllCustomerCareAsync() {
        var result = new List<CustomerCare>();
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
            SELECT
                ID, CUSTOMER_NAME, CUSTOMER_CODE,
                CUSTOMER_CSQH, CUSTOMER_RISK, CUSTOMER_DKTT, CUSTOMER_REVIEW
            FROM CUSTOMER
            ORDER BY CREATED_AT DESC", conn);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync()) {
            result.Add(new CustomerCare {
                ID = reader["ID"].ToString() ?? "",
                CustomerName = reader["CUSTOMER_NAME"].ToString() ?? "",
                CustomerCode = reader["CUSTOMER_CODE"].ToString() ?? "",
                CustomerCSQH = reader["CUSTOMER_CSQH"].ToString() ?? "",
                CustomerRisk = reader["CUSTOMER_RISK"].ToString() ?? "",
                CustomerDKTT = reader["CUSTOMER_DKTT"].ToString() ?? "",
                CustomerReview = reader["CUSTOMER_REVIEW"] == DBNull.Value
                    ? (decimal?)null
                    : Convert.ToDecimal(reader["CUSTOMER_REVIEW"])
            });
        }
        return result;
    }

    public async Task UpdateCustomerCareAsync(CustomerCare item) {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
            UPDATE CUSTOMER SET
                CUSTOMER_CSQH = @CSQH,
                CUSTOMER_RISK = @RISK,
                CUSTOMER_DKTT = @DKTT,
                CUSTOMER_REVIEW = @REVIEW
            WHERE ID = @ID", conn);

        cmd.Parameters.Add("@ID", System.Data.SqlDbType.UniqueIdentifier)
                      .Value = Guid.Parse(item.ID);
        cmd.Parameters.AddWithValue("@CSQH",  (object?)item.CustomerCSQH ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@RISK",  (object?)item.CustomerRisk ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DKTT",  (object?)item.CustomerDKTT ?? DBNull.Value);
        // Nếu cột REVIEW là DECIMAL
        cmd.Parameters.Add("@REVIEW", System.Data.SqlDbType.Decimal).Value =
            (object?)item.CustomerReview ?? DBNull.Value;

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<OrderView>> GetAllOrdersAsync()
    {
        var list = new List<OrderView>();
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        SELECT O.ID, O.CUSTOMER_ID, C.CUSTOMER_NAME, C.CUSTOMER_CODE,
               O.ORDER_CODE, O.QUANTITY, O.ORDER_DATE, O.DELIVERY_DATE, O.STATUS
        FROM ORDERS O
        JOIN CUSTOMER C ON O.CUSTOMER_ID = C.ID
        ORDER BY O.CREATED_AT DESC", conn);
        await conn.OpenAsync();
        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
        {
            list.Add(new OrderView
            {
                ID = r["ID"].ToString() ?? "",
                CustomerID = r["CUSTOMER_ID"].ToString() ?? "",
                CustomerName = r["CUSTOMER_NAME"].ToString() ?? "",
                CustomerCode = r["CUSTOMER_CODE"].ToString() ?? "",
                OrderCode = r["ORDER_CODE"].ToString() ?? "",
                Quantity = r.GetInt32(r.GetOrdinal("QUANTITY")),
                OrderDate = r.GetDateTime(r.GetOrdinal("ORDER_DATE")),
                DeliveryDate = r.GetDateTime(r.GetOrdinal("DELIVERY_DATE")),
                Status = r["STATUS"].ToString() ?? ""
            });
        }
        return list;
    }

    public async Task AddOrderAsync(Order item)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        INSERT INTO ORDERS
        (ID, CUSTOMER_ID, ORDER_CODE, QUANTITY, ORDER_DATE, DELIVERY_DATE, STATUS, CREATED_AT)
        VALUES (@ID, @CID, @OCODE, @QTY, @ODATE, @DDATE, @STATUS, GETDATE())", conn);

        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(item.ID);
        cmd.Parameters.Add("@CID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(item.CustomerID);
        cmd.Parameters.AddWithValue("@OCODE", item.OrderCode);
        cmd.Parameters.AddWithValue("@QTY", item.Quantity);
        cmd.Parameters.Add("@ODATE", SqlDbType.Date).Value = item.OrderDate.Date;
        cmd.Parameters.Add("@DDATE", SqlDbType.Date).Value = item.DeliveryDate.Date;
        cmd.Parameters.AddWithValue("@STATUS", item.Status);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateOrderAsync(Order item)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        UPDATE ORDERS SET
            CUSTOMER_ID = @CID,
            ORDER_CODE = @OCODE,
            QUANTITY = @QTY,
            ORDER_DATE = @ODATE,
            DELIVERY_DATE = @DDATE,
            STATUS = @STATUS
        WHERE ID = @ID", conn);

        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(item.ID);
        cmd.Parameters.Add("@CID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(item.CustomerID);
        cmd.Parameters.AddWithValue("@OCODE", item.OrderCode);
        cmd.Parameters.AddWithValue("@QTY", item.Quantity);
        cmd.Parameters.Add("@ODATE", SqlDbType.Date).Value = item.OrderDate.Date;
        cmd.Parameters.Add("@DDATE", SqlDbType.Date).Value = item.DeliveryDate.Date;
        cmd.Parameters.AddWithValue("@STATUS", item.Status);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteOrderAsync(string id)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("DELETE FROM ORDERS WHERE ID = @ID", conn);
        cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(id);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }




    public async Task AddOutputAsync(Output output)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        INSERT INTO OUTPUT (ID, OUTPUT_NAME, LEVEL1, LEVEL2, LEVEL3, CREATED_AT)
        VALUES (@ID, @Name, @L1, @L2, @L3, GETDATE())", conn);

        cmd.Parameters.AddWithValue("@ID", output.ID);
        cmd.Parameters.AddWithValue("@Name", output.OutputName);
        cmd.Parameters.AddWithValue("@L1", output.Level1);
        cmd.Parameters.AddWithValue("@L2", output.Level2);
        cmd.Parameters.AddWithValue("@L3", output.Level3);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateOutputAsync(Output output)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        UPDATE OUTPUT
        SET OUTPUT_NAME = @Name,
            LEVEL1 = @L1,
            LEVEL2 = @L2,
            LEVEL3 = @L3
        WHERE ID = @ID", conn);

        cmd.Parameters.AddWithValue("@ID", output.ID);
        cmd.Parameters.AddWithValue("@Name", output.OutputName);
        cmd.Parameters.AddWithValue("@L1", output.Level1);
        cmd.Parameters.AddWithValue("@L2", output.Level2);
        cmd.Parameters.AddWithValue("@L3", output.Level3);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteOutputAsync(string id)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("DELETE FROM OUTPUT WHERE ID = @ID", conn);
        cmd.Parameters.AddWithValue("@ID", id);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<ShippingConditional>> GetAllShippingConditionalsAsync()
    {
        var result = new List<ShippingConditional>();
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("SELECT * FROM SHIPPING_CONDITIONAL ORDER BY CREATED_AT DESC", conn);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new ShippingConditional
            {
                ID = reader["ID"].ToString() ?? "",
                ConditionName = reader["SHIPPING_NAME"].ToString() ?? "",
                EffectPrice = reader.GetDecimal(reader.GetOrdinal("SHIPPING_EFFECT_PRICE")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CREATED_AT"))
            });
        }
        return result;
    }

    public async Task AddShippingConditionalAsync(ShippingConditional item)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        INSERT INTO SHIPPING_CONDITIONAL (ID, SHIPPING_NAME, SHIPPING_EFFECT_PRICE, CREATED_AT)
        VALUES (@ID, @NAME, @PRICE, GETDATE())", conn);
        cmd.Parameters.AddWithValue("@ID", item.ID);
        cmd.Parameters.AddWithValue("@NAME", item.ConditionName);
        cmd.Parameters.AddWithValue("@PRICE", item.EffectPrice);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateShippingConditionalAsync(ShippingConditional item)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(@"
        UPDATE SHIPPING_CONDITIONAL
        SET SHIPPING_NAME = @NAME, SHIPPING_EFFECT_PRICE = @PRICE
        WHERE ID = @ID", conn);
        cmd.Parameters.AddWithValue("@ID", item.ID);
        cmd.Parameters.AddWithValue("@NAME", item.ConditionName);
        cmd.Parameters.AddWithValue("@PRICE", item.EffectPrice);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteShippingConditionalAsync(string id)
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("DELETE FROM SHIPPING_CONDITIONAL WHERE ID = @ID", conn);
        cmd.Parameters.AddWithValue("@ID", id);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

}
