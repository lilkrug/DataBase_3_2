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
    class ServiceTypeRepository : IRepository<ServiceType>
    {
        public SqlConnection connection;

        public ServiceTypeRepository(SqlConnection connection)
        {
            this.connection = connection;

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceType Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(ServiceType customer)
        {
            throw new NotImplementedException();
        }

        public int Update(ServiceType customer)
        {
            throw new NotImplementedException();
        }
    }
}
