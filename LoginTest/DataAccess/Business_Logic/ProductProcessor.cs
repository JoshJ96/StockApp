using DataAccessLibrary.Data_Access;
using LoginTest.Models;
using System.Collections.Generic;

namespace LoginTest.DataAccess.Business_Logic
{
    public class ProductProcessor
    {
        //Read a list of products
        public static List<Product> LoadProducts()
        {
            string sql = @"
                SELECT TOP 25 * 
                FROM [dbo].[Product]
                ORDER BY [Name]";

            return SqlDataAccess.LoadData<Product>(sql);
        }

        //Read a list of products matching an ID
        public static List<Product> GetProductFromID(int id)
        {
            string sql = @"SELECT * FROM [DBO].[PRODUCT]
                    WHERE
                    Number = '"+id+"'";

            return SqlDataAccess.LoadData<Product>(sql);
        }

        //Get one product from an ID search
        public static Product GetByID(int id)
        {
            string sql = @"SELECT * FROM [DBO].[PRODUCT]
                    WHERE
                    Number = '" + id + "'";

            var data = SqlDataAccess.LoadData<Product>(sql);

            //If we found data, return it
            if (data.Count != 0)
                return data[0];

            //If we didn't find data, return an empty product
            return new Product();

        }

        //Modify product
        public static int ModifyProduct(Product product)
        {
            string sql = @"
                       UPDATE [dbo].[Product]
                       SET
                        [Name] = '" + product.Name + "' ," +
                       "[Price] = '" + product.Price + "'," +
                       "[Location] = '" + product.Location + "'," +
                       "[Quantity] = '" + product.Quantity + "'," +
                       "[Brand] = '" + product.Brand + "'" +
                       "WHERE [Number] = '" + product.Number + "'";

            return SqlDataAccess.Query(sql);
        }
    }
}