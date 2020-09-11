using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Context;
using Order.Infrastructure.Domain.Services.Base;
using Order.Infrastructure.Domain.Services.Interface;
using Order.Infrastructure.Models.Result;
using CardType = Order.Infrastructure.Domain.AggregatesModel.Entities.CardType;

namespace Order.Infrastructure.Domain.Services.Implementation
{
    public class OrderService: IOrderService
    {
        private readonly OrderDbContext _context;
        private string _connectionString = string.Empty;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public OrderService(OrderDbContext context, string constr)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public AggregatesModel.Entities.Order Add(AggregatesModel.Entities.Order order)
        {
            return _context.Orders.Add(order).Entity;

        }

        public async Task<AggregatesModel.Entities.Order> GetAsync(int orderId)
        {
            var order = await _context
                .Orders
                .Include(x => x.Address)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                order = _context
                    .Orders
                    .Local
                    .FirstOrDefault(o => o.Id == orderId);
            }
            if (order != null)
            {
                await _context.Entry(order)
                    .Collection(i => i.OrderItems).LoadAsync();
                await _context.Entry(order)
                    .Reference(i => i.OrderStatus).LoadAsync();
            }

            return order;
        }


        public void Update(AggregatesModel.Entities.Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }


        public async Task<Models.Result.Order> GetOrderAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                   @"select o.[Id] as ordernumber,o.OrderDate as date, o.Description as description,
                        o.Address_City as city, o.Address_Country as country, o.Address_State as state, o.Address_Street as street, o.Address_ZipCode as zipcode,
                        os.Name as status, 
                        oi.ProductName as productname, oi.Units as units, oi.UnitPrice as unitprice, oi.PictureUrl as pictureurl
                        FROM order.Orders o
                        LEFT JOIN order.Orderitems oi ON o.Id = oi.orderid 
                        LEFT JOIN order.orderstatus os on o.OrderStatusId = os.Id
                        WHERE o.Id=@id"
                        , new { id }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapOrderItems(result);
            }
        }

        public async Task<IEnumerable<OrderSummary>> GetOrdersFromUserAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<OrderSummary>(@"SELECT o.[Id] as ordernumber,o.[OrderDate] as [date],os.[Name] as [status], SUM(oi.units*oi.unitprice) as total
                     FROM [order].[Orders] o
                     LEFT JOIN[order].[orderitems] oi ON  o.Id = oi.orderid 
                     LEFT JOIN[order].[orderstatus] os on o.OrderStatusId = os.Id                     
                     LEFT JOIN[order].[buyers] ob on o.BuyerId = ob.Id
                     WHERE ob.IdentityGuid = @userId
                     GROUP BY o.[Id], o.[OrderDate], os.[Name] 
                     ORDER BY o.[Id]", new { userId });
            }
        }

        public async Task<IEnumerable<CardType>> GetCardTypesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<CardType>("SELECT * FROM order.cardtypes");
            }
        }

        private Models.Result.Order MapOrderItems(dynamic result)
        {
            var order = new Models.Result.Order
            {
                OrderNumber = result[0].ordernumber,
                Date = result[0].date,
                Status = result[0].status,
                Description = result[0].description,
                Street = result[0].street,
                City = result[0].city,
                ZipCode = result[0].zipcode,
                Country = result[0].country,
                Total = 0
            };

            foreach (dynamic item in result)
            {
                var orderItem = new OrderItem
                {
                    ProductName = item.productname,
                    Units = item.units,
                    UnitPrice = (double)item.unitprice,
                    PictureUrl = item.pictureurl
                };

                order.Total += item.units * item.unitprice;
                order.OrderItems.Add(orderItem);
            }

            return order;
        }
    }
}
