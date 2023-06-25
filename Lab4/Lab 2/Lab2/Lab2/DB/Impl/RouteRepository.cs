using Lab2.DB.Interfaces;
using Lab2.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DB.Impl
{
    class RouteRepository : IRepository<Route>
    {

        public SqlConnection connection;

        public RouteRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Route Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Route customer)
        {
            throw new NotImplementedException();
        }

        public int Update(Route customer)
        {
            throw new NotImplementedException();
        }
    }
}
