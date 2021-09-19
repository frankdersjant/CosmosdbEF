using ConsoleApp3.CosmosDAL;
using ConsoleApp3.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public static class CosmosDBEfDemo
    {
        public static async Task Run()
        {
            Console.WriteLine();

            using (var context = new OrderContext())
            {
                //NB - make sure understated code is executed
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                context.Add(
                    new Order
                    {
                        Id = 1,
                        ShippingAddress = new ShippingAddress { City = "London", Street = "221 B Baker St" },
                        PartitionKey = "1"
                    });

                await context.SaveChangesAsync();
            }

            using (var context = new OrderContext())
            {
                var order = await context.Orders.FirstAsync();
                Console.WriteLine($"First order will ship to: {order.ShippingAddress.Street}, {order.ShippingAddress.City}");
                Console.WriteLine();
            }

            using (var context = new OrderContext())
            {
                context.Add(
                    new Order
                    {
                        Id = 2,
                        ShippingAddress = new ShippingAddress { City = "New York", Street = "11 Wall Street" },
                        PartitionKey = "2"
                    });

                await context.SaveChangesAsync();
            }

            using (var context = new OrderContext())
            {
                var order = await context.Orders.WithPartitionKey("2").LastAsync();
                Console.WriteLine($"Last order will ship to: {order.ShippingAddress.Street}, {order.ShippingAddress.City}");
                Console.WriteLine();
            }
        }
    }
}
