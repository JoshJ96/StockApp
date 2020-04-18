using DataAccessLibrary.Data_Access;
using LoginTest.Models;
using System.Collections.Generic;

namespace LoginTest.DataAccess.Business_Logic
{
    public static class UserProcessor
    {
        public static UserRoles GetUserInfoFromEmail(string userName)
        {
            string sql = @"
                SELECT * FROM [DBO].[AspNetUsers]
                WHERE [Email] = '"+userName+"'";


            var data = SqlDataAccess.LoadData<UserRoles>(sql);

            //If we found data, return it
            if (data.Count != 0)
                return data[0];

            //If we didn't find data, return an empty object
            return new UserRoles();
        }

        public static UserRoles GetUserRole(string userName)
        {
            string sql = @"
                SELECT * FROM [DBO].[AspNetUsers]
                WHERE [Email] = '" + userName + "'";


            var data = SqlDataAccess.LoadData<UserRoles>(sql);

            //If we user found data, continue otherwise return null
            if (data.Count == 0)
            {
                return new UserRoles {Id = userName, Name = "Guest"};
            }

            //Check role in aspnetroles DB
            sql = @"SELECT * FROM [dbo].[AspNetRoles]
                WHERE Id = '"+data[0].Id+"'; ";

            var roleData = SqlDataAccess.LoadData<UserRoles>(sql);
            if (roleData.Count != 0)
            {
                return roleData[0];
            }

            return new UserRoles { Id = userName, Name = "Guest" };
        }
    }
}