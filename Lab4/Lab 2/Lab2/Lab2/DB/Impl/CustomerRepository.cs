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
    class CustomerRepository : IRepository<Customer>
    {
        public SqlConnection connection;

        public CustomerRepository(SqlConnection connection)
        {
            this.connection = connection;

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
