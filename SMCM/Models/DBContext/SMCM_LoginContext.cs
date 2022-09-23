using Microsoft.Data.SqlClient;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace SmartMeterConsumerManagement.Models.DBContext
{
    public partial class SMCM_LoginContext : SMCM_DBContext
    {
        public SMCM_LoginContext(DbContextOptions<SMCM_DBContext> options) : base(options)
        {

        }
        public User GetUser(string emailId)
        {
            SqlParameter parameter = new SqlParameter("@emailId", emailId ?? (object)DBNull.Value);

            return Users.FromSqlRaw("EXEC [dbo].[GetUserByEmailId] @emailId", parameter).AsEnumerable().FirstOrDefault();
        }
    }
}
