using System;
using System.Data;
using System.Data.SqlClient;


namespace Vervolmaking_ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] queries = new string[5];
            string query1 = @"SELECT [ProductID], ProductName,C.CategoryName
                            FROM Products p JOIN Categories c
                            ON p.CategoryID = c.CategoryID
                            ORDER BY c.CategoryID, p.ProductName";

            string query2 = @"SELECT TOP(10) ProductName as MostExpensiveProducts, UnitPrice
                            FROM Products
                            ORDER BY UnitPrice desc";

            string query3 = @"SELECT City, CompanyName, ContactName, 'Customer' as Type
                            FROM Customers c
                            UNION
                            SELECT City, CompanyName, ContactName, 'Supplier' as Type
                            FROM Suppliers s
                            ORDER BY City";

            string query4 = @"SELECT DISTINCT OrderID, subtotal =(select sum(UnitPrice * Quantity-(UnitPrice*Quantity*Discount)) FROM [Order Details] od where o.OrderID = od.OrderID)
                            FROM [dbo].[Order Details] o";

            string query5 = @"SELECT DISTINCT ShippedDate,o.OrderID, subtotal = (select sum(Unitprice * Quantity -(Unitprice * quantity * Discount)) from [Order Details] od2 where O.OrderID = od2.OrderID),
	                        Year(OrderDate) as Year
                            FROM Orders O JOIN [Order Details] OD 
                            ON o.OrderID = OD.OrderID
                            where o.ShippedDate between '1996-12-24' and '1997-09-30'
                            ORDER BY O.ShippedDate";

            queries[0] = query1;
            queries[1] = query2;
            queries[2] = query3;
            queries[3] = query4;
            queries[4] = query5;

            Queries q = new Queries();

            for (int i = 0; i < queries.Length; i++)
            {
                Console.WriteLine($"Query Commando:{queries[i]}");
                q.VoerQueryUit(queries[i]);
                Console.WriteLine($"================THE END===================");
            }
        }
    }
}
