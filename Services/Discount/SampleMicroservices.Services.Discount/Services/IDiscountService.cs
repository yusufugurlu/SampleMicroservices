﻿using SampleMicroservices.Services.Discount.Models;
using SampleMicroservices.Shared.Dtos;

namespace SampleMicroservices.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Models.Discount>>> GetAll();
        Task<Response<Models.Discount>> GetById(int id);

        Task<Response<NoContent>> Save(Models.Discount discount);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<NoContent>> Update(Models.Discount discount);

        Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId);

    }
}
