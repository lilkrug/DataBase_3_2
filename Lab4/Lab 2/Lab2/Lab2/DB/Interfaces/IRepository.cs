using Lab2.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DB.Interfaces
{
    interface IRepository<T>
    {
        T Get(int id);
        List<Order> GetAll();
        void Delete(int id);
        int Update(T customer);
        int Insert(T customer);

    }
}
