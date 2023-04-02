using MediatR;
using SampleMicroservices.Services.Order.Application.Commands;
using SampleMicroservices.Services.Order.Application.Dtos;
using SampleMicroservices.Services.Order.Domain.OrderAggerate;
using SampleMicroservices.Services.Order.Instracture;
using SampleMicroservices.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Order.Application.Handler
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province, request.Address.Distrct, request.Address.Street, request.Address.ZipCode, request.Address.Line);

            var newOrder = new Domain.OrderAggerate.Order(request.BuyerId, newAddress);

            request.OrderItems.ForEach((item) =>
            {
                newOrder.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.PictureUrl);

            });
            await _orderDbContext.orders.AddAsync(newOrder);

            await _orderDbContext.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto() { OrderId = newOrder.Id }, 200);
        }
    }
}
