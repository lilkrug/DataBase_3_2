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
    class ServiceRepository : IRepository<Service>
    {
        public SqlConnection connection;

        public ServiceRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Service Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Service customer)
        {
            throw new NotImplementedException();
        }

        public int Update(Service customer)
        {
            throw new NotImplementedException();
        }
    }
}
