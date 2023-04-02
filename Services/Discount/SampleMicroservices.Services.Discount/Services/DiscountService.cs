using Dapper;
using Npgsql;
using SampleMicroservices.Services.Discount.Models;
using SampleMicroservices.Shared.Dtos;
using System.Data;

namespace SampleMicroservices.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var saveStatus = await _connection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("error", 500);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("select * from discount where code=@Code and userId=@UserId", new { Code = code, UserId = userId })).SingleOrDefault();
            if (discount == null)
            {
                return Response<Models.Discount>.Fail("", 400);
            }
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();
            if (discount == null)
            {
                return Response<Models.Discount>.Fail("", 400);
            }
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _connection.ExecuteAsync("insert into discount(userid,rate,code) values(@UserId, @Rate, @Code)", discount);
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("error", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var saveStatus = await _connection.ExecuteAsync("update discount set userId=@UserId, code=@Code, rate=@Rate where id=@Id", discount);
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("error", 500);
        }
    }
}
