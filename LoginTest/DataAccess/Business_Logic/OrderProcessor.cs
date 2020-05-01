using DataAccessLibrary.Data_Access;
using LoginTest.Models;
using System.Collections.Generic;

namespace LoginTest.DataAccess.Business_Logic
{
    public class OrderProcessor
    {
        //Create order record
        public static int CreateOrder(int customerNumber, decimal price)
        {
            string sql = @"
            INSERT INTO [dbo].[Orders]
           ([CustomerNumber],[Price])
             VALUES
           ('" + customerNumber + "', '" + price + "')";

            return SqlDataAccess.Query(sql);
        }

        //Get most recent order
        public static Order GetOrderDetails()
        {
            string sql = @"SELECT max(OrderNumber) FROM [dbo].[Orders]";

            var data = SqlDataAccess.LoadData<Order>(sql);

            if (data.Count != 0)
                return data[0];

            return new Order();
        }

        //Add line items to records
        public static int AddLineItems(List<Product> products)
        {
            //Get our recently created order number
            Order order = GetOrderDetails();

            string sql = "";

            foreach (Product line in products)
            {
                sql = @"
                    INSERT INTO [dbo].[LineItems]
                   ([CustomerNumber],[LineItem],[Price])
                     VALUES
                   ('" + order.customerInfo.ID + "', '" + line.Number + "', '" +line.Quantity+ ")";

                SqlDataAccess.Query(sql);
            }

            return SqlDataAccess.Query(sql);
        }
    }
}