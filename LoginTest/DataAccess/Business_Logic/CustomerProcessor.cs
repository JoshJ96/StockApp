using DataAccessLibrary.Data_Access;
using LoginTest.Models;
using System.Collections.Generic;

namespace LoginTest.DataAccess.Business_Logic
{
    public static class CustomerProcessor
    {
        //Read a list of products
        public static List<Customer> LoadCustomers()
        {
            string sql = @"
                SELECT TOP 25 * 
                FROM [dbo].[Customers]
                ORDER BY [Name]";

            return SqlDataAccess.LoadData<Customer>(sql);
        }

        public static List<Customer> CustomerSearch(string searchQuery)
        {
            bool isAnInteger = int.TryParse(searchQuery, out _);

            //By default, search by name, location, or brand
            string sql = @"
                  SELECT *
                  FROM [dbo].[Customers]
                  WHERE CHARINDEX('" + searchQuery + @"', [Name]) > 0
                  UNION
                  SELECT *
                  FROM [dbo].[Customers]
                  WHERE CHARINDEX('" + searchQuery + @"', [Address]) > 0
                  UNION
                  SELECT *
                  FROM [dbo].[Customers]
                  WHERE CHARINDEX('" + searchQuery + @"', [City]) > 0
                  UNION
                  SELECT*
                  FROM[dbo].[Customers]
                  WHERE CHARINDEX('" + searchQuery + @"', [State]) > 0";

            //If the search query has only numbers, search by product number
            if (isAnInteger)
            {
                sql = @"
                  SELECT *
                  FROM [dbo].[Customers]
                  WHERE [Id] = '" + searchQuery + @"'";
            }
            return SqlDataAccess.LoadData<Customer>(sql);
        }

        //Create Product
        public static int CreateCustomer(string name, string address, string city, string state)
        {
            string sql = @"
            INSERT INTO [dbo].[Customers]
           ([Name],[Address],[City],[State])
             VALUES
           ('" + name + "', '" + address + "', '" + city + "', '" + state + "')";

            return SqlDataAccess.Query(sql);
        }

        //Get ID of created product
        public static Customer GetByAllButNumber(string name, string address, string city, string state)
        {
            string sql = @"SELECT * FROM [DBO].[Customers]
                    WHERE(
                    [Name] = '" + name + @"'
                   AND [Address] = '" + address + @"'
                   AND [City] = '" + city + @"'
                   AND [State] = '" + state + "')";

            var data = SqlDataAccess.LoadData<Customer>(sql);

            //If we found data, return it
            if (data.Count != 0)
                return data[0];

            //If we didn't find data, return an empty product
            return new Customer();
        }

        public static Customer GetCustomerByID(int id)
        {
            string sql = @"SELECT * FROM [DBO].[CUSTOMERS]
                    WHERE
                    [Id] = '" + id + "'";

            var data = SqlDataAccess.LoadData<Customer>(sql);

            //If we found data, return it
            if (data.Count != 0)
                return data[0];

            //If we didn't find data, return an empty product
            return new Customer();

        }

        //Modify product
        public static int ModifyCustomer(Customer customer)
        {
            string sql = @"
                       UPDATE [dbo].[Customers]
                       SET
                        [Name] = '" + customer.Name + "' ," +
                       "[Address] = '" + customer.Address + "'," +
                       "[City] = '" + customer.City + "'," +
                       "[State] = '" + customer.State + "'" +
                       " WHERE [Id] = '" + customer.ID + "'";

            return SqlDataAccess.Query(sql);
        }
    }
}