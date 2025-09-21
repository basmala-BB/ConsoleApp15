
using ConsoleApp15.Data;
using ConsoleApp15.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext _db = new();

            //1.
            var Customers = _db.Customers
              .Select(c => new { c.FirstName, c.LastName, c.Email });
            foreach (var customer in Customers)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Email}");
            }

            //2.
            var ordersByStaff3 = _db.Orders
                       .Where(o => o.StaffId == 3);

            foreach (var order in ordersByStaff3)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Order Date: {order.OrderDate}");
            }

            //3.
            var products = _db.Products
            .Join(_db.Categories,
                  p => p.CategoryId,
                  c => c.CategoryId,
                  (p, c) => new { p.ProductName, c.CategoryName })
            .Where(pc => pc.CategoryName == "Mountain Bikes");

            foreach (var item in products)
            {
                Console.WriteLine($"{item.ProductName} - {item.CategoryName}");

            }

            //4.
            var totalOrders = _db.Orders.GroupBy(o => o.StoreId)
                  .Select(e => new
                  {
                      StoreId = e.Key,
                      Count = e.Count()
                  });

            foreach (var item in totalOrders)
            {
                Console.WriteLine($"StoreId: {item.StoreId} - Count: {item.Count}");
            }

            //5.
            var notShippedOrders = _db.Orders.Where(o => o.ShippedDate == null);

            foreach (var order in notShippedOrders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Order Date: {order.OrderDate}");
            }

            //6.
            var customerOrders = _db.Customers
              .Select(c => new
              {

                  FullName = c.FirstName + " " + c.LastName,

                  OrdersCount = c.Orders.Count()

              });

            foreach (var item in customerOrders)
            {
                Console.WriteLine($"{item.FullName}: {item.OrdersCount} orders");
            }

            //7.
            var productsNeverOrdered = _db.Products
            .Where(p => !_db.OrderItems.Any(oi => oi.ProductId == p.ProductId));

            foreach (var p in productsNeverOrdered)
            {
                Console.WriteLine($"Product never ordered: {p.ProductName}");
            }

            //8.
            var lowStock = _db.Stocks
                .Where(s => s.Quantity < 5)
                  .Select(s => s.Product);

            foreach (var p in lowStock)
            {
                Console.WriteLine($"Low stock: {p.ProductName}");
            }

            //9.
            var firstProduct = _db.Products.FirstOrDefault();
            if (firstProduct != null)
                Console.WriteLine($"First product: {firstProduct.ProductName}");

            //10.
            int year = 2022;
            var productsByYear = _db.Products.Where(e => e.ModelYear == year);

            foreach (var e in productsByYear)
            {
                Console.WriteLine($"Product {e.ProductName}, Model Year: {e.ModelYear}");
            }

            //11.
            var productOrderCounts = _db.OrderItems.GroupBy(oi => oi.ProductId)
              .Select(e => new
              {
                  ProductId = e.Key,
       
                  TimesOrdered = e.Count()
     
              });

            foreach (var item in productOrderCounts)
            {
                Console.WriteLine($"ProductId {item.ProductId} ordered {item.TimesOrdered} times");
            }

            //12.
            var categoryGroups = _db.Products
             .GroupBy(p => p.Category.CategoryName)
              .Select(g => new
              {
                  CategoryName = g.Key,
                  Count = g.Count()
              });

            foreach (var item in categoryGroups)
            {
                Console.WriteLine($"{item.CategoryName}: {item.Count} products");
            }


            //13.
            var avgPrice = (from p in _db.Products
              select p.ListPrice).Average();
            Console.WriteLine(avgPrice);

            //14.
            int productId = 5;
            var product =
                (from p in _db.Products
                 where p.ProductId == productId
                 select p).FirstOrDefault();


            //15.
            var productsQtyGt3 =_db.OrderItems

            .Where(oi => oi.Quantity > 3)
            .Select(oi => oi.Product)
              .Distinct();

            foreach (var p in productsQtyGt3)
            {
                Console.WriteLine($"Product ordered with qty > 3: {p.ProductName}");
            }

            //16.
            var staffOrders = _db.Orders.GroupBy(o => o.StaffId).Join(
                    _db.Staffs,
                     g => g.Key,
                     s => s.StaffId,
                      (g, s) => new 
                      {
                       StaffName = s.FirstName + " " + s.LastName,
                         OrdersCount = g.Count()
                   
                      });

            foreach (var item in staffOrders)
            {
                Console.WriteLine($"{item.StaffName}: {item.OrdersCount} orders");
            }

            //17.
            var activeStaff = _db.Staffs.Where(s => s.Active == 1)
         .Select(s => new { s.FirstName, s.LastName, s.Phone });

            foreach (var s in activeStaff)
            {
                Console.WriteLine($"Active Staff: {s.FirstName} {s.LastName}, Phone: {s.Phone}");
            }

            //18.
            var productDetails = _db.Products.Select

               (p => new {
                  p.ProductName,
                   Brand = p.Brand.BrandName,
                  Category = p.Category.CategoryName
               });

            foreach (var p in productDetails)
            {
                Console.WriteLine($"{p.ProductName} - Brand: {p.Brand}, Category: {p.Category}");
            }

            //19.
            var completedOrders = _db.Orders.Where(o => o.OrderStatus == 2);

            foreach (var o in completedOrders)
            {
                Console.WriteLine($"Completed Order: {o.OrderId}");
            }

            //20.
            var totalQtySold = _db.OrderItems.GroupBy(oi => oi.ProductId).Select
                  (g => new {
                     ProductId = g.Key,
                     TotalQty = g.Sum(oi => oi.Quantity)
                  });

            foreach (var item in totalQtySold)
            {
                Console.WriteLine($"ProductId {item.ProductId} total sold: {item.TotalQty}");
            }

        }
    }
}
