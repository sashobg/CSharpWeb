namespace PartsCatalog.Services.Shop.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Cart;
  
    using Models.Order;
    public class OrderService : IOrderService
    {
        private readonly IProductService _products;
        private readonly PartsCatalogDbContext db;
        public OrderService(IProductService products, PartsCatalogDbContext db)
        {
            this._products = products;
            this.db = db;
        }
        public IEnumerable<OrderListingServiceModel> All(string userId)
        {
            if (userId == null)
                userId = string.Empty;

            return this.db.Orders
                .Where(x => x.UserId
                    .Contains(userId.ToLower()))
                .OrderByDescending(x => x.Id)
                .ProjectTo<OrderListingServiceModel>()
                .ToList(); ;
        }

        public OrderDetailsServiceModel Details(int id)
            => this.db.Orders
                .Where(x => x.Id == id)
                .ProjectTo<OrderDetailsServiceModel>()
                .FirstOrDefault();

        public int Create(string address, string comment, IEnumerable<CartItem> items, string userId)
        {
            var itemsWithDetails = this._products.GetProducts(items);

            var order = new Order
            {

                UserId = userId,
                TotalPrice = itemsWithDetails.Sum(i => i.Price * i.Quantity),
                Address = address,
                Comment = comment,
                Date = DateTime.Now,
                OrderStatus = OrderStatus.Pending
            };

            foreach (var item in itemsWithDetails)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductPrice = item.Price,
                    Title = item.Title,
                    Quantity = item.Quantity

                });
            }
            this.db.Add(order);
            this.db.SaveChanges();
            return order.Id;
        }

        public bool IsItAuthor(int id, string userId)
        {
            var order = this.db.Orders.Find(id);

            if (order == null || order.UserId != userId)
            {
                return false;
            }
            return true;
        }

        public bool UpdateStatus(int id, int status)
        {
            var order = this.db.Orders.Find(id);

            if (order == null)
            {
                return false;
            }
           
            order.OrderStatus = (OrderStatus)status;
            this.db.Orders.Update(order);
            this.db.SaveChanges();
            return true;
        }

       

        public bool Delete(int id)
        {
            var order = this.db.Orders.Find(id);

            if (order == null)
            {
                return false;
            }

            this.db.Orders.Remove(order);
            this.db.SaveChanges();
            return true;
        }
    }
}
