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

        //Search
        public static List<Product> ProductSearch(string searchQuery)
        {
            bool isAnInteger = int.TryParse(searchQuery, out _);

            //By default, search by name, location, or brand
            string sql = @"
                  SELECT *
                  FROM [dbo].[Product]
                  WHERE CHARINDEX('" + searchQuery + @"', [Name]) > 0
                  UNION
                  SELECT *
                  FROM [dbo].[Product]
                  WHERE CHARINDEX('" + searchQuery + @"', [Location]) > 0
                  UNION
                  SELECT *
                  FROM [dbo].[Product]
                  WHERE CHARINDEX('" + searchQuery + @"', [Brand]) > 0";

            //If the search query has only numbers, search by product number
            if (isAnInteger)
            {
                sql = @"
                  SELECT *
                  FROM [dbo].[Product]
                  WHERE [Number] = '" + searchQuery + @"'";
            }
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

        //Create Product
        public static int CreateProduct(string name, string location, int quantity, decimal price, string brand)
        {
            string sql = @"
            INSERT INTO [dbo].[Product]
           ([Name],[Location],[Quantity],[Price],[Brand])
             VALUES
           ('" + name+"', '"+location+"', '"+quantity+"', '"+price+"','"+brand+"')";

            return SqlDataAccess.Query(sql);
        }

        //Get ID of created product
        public static Product GetByAllButNumber(string name, string location, int quantity, decimal price, string brand)
        {
            string sql = @"SELECT * FROM [DBO].[PRODUCT]
                    WHERE(
                    [Name] = '"+name+ @"'
                   AND [Location] = '" + location + @"'
                   AND [Quantity] = '" + quantity + @"'
                   AND [Price] = '" + price + @"'
                   AND [Brand] = '" + brand + "')";

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