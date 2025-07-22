

using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;

namespace Ordering.Infrastructure.Extentions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("4c38df16-969b-414b-ad40-50c83fe233bd")), "mehmet", "mehmet@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("e9720450-ec3d-4a93-8f7a-ea8c1ec22759")), "john", "john@gmail.com"),
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("3fdbb6f9-bd8a-4628-b80b-94231b94d5f5")) , "IPhone" , 500),
                Product.Create(ProductId.Of(new Guid("79854db1-4d4c-4221-8462-b7249c13da5d")) , "Samsung" , 400),
                Product.Create(ProductId.Of(new Guid("2dd99450-3683-4215-9427-d7443cf6f774")) , "Huawei" , 650),
                Product.Create(ProductId.Of(new Guid("1b86a94a-9a12-4d8a-a066-2454b49daa8d")) , "Xiami" , 450),
            };

        public static IEnumerable<Order> OrderWithItems
        {
            get
            {
                var address1 = Address.Of("mehmet", "ozkaya", "mehmet@gmail.com", "Bahcelieveler No:4", "Turkey", "Istambul", "38050");
                var address2 = Address.Of("john", "doe", "john@gmail.com", "Brodway No:1", "Engalnd", "Nottingham", "08050");

                var payment1 = Payment.Of("mehmet", "5555555554444444", "12/28", "355", 1);
                var payment2 = Payment.Of("john", "8888888555554444", "06/30", "222", 2);

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("4c38df16-969b-414b-ad40-50c83fe233bd")),
                    OrderName.Of("ORD_1"),
                    shippingAddress: address1,
                    billingAddress : address2,
                    payment1
                    );

                order1.Add(ProductId.Of(new Guid("3fdbb6f9-bd8a-4628-b80b-94231b94d5f5")), 2, 500);
                order1.Add(ProductId.Of(new Guid("79854db1-4d4c-4221-8462-b7249c13da5d")), 1, 400);

                var order2 = Order.Create(
                  OrderId.Of(Guid.NewGuid()),
                  CustomerId.Of(new Guid("4c38df16-969b-414b-ad40-50c83fe233bd")),
                  OrderName.Of("ORD_2"),
                  shippingAddress: address2,
                  billingAddress: address2,
                  payment2
                  );

                order2.Add(ProductId.Of(new Guid("2dd99450-3683-4215-9427-d7443cf6f774")), 2, 500);
                order2.Add(ProductId.Of(new Guid("1b86a94a-9a12-4d8a-a066-2454b49daa8d")), 1, 400);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
