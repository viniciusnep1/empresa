using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace core.seedwork
{
    public abstract class DapperRepository
    {
        private readonly IConfiguration config;

        protected DapperRepository(IConfiguration config)
        {
            this.config = config;
        }

        protected IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(config.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
