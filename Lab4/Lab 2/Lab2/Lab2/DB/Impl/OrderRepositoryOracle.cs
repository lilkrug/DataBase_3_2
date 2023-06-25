using Lab2.DB.Interfaces;
using Lab2.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DB.Impl
{
    class OrderRepositoryOracle : IRepository<Order>
    {
        public OracleConnection connection;

        public OrderRepositoryOracle(OracleConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(int id)
        {
            connection.Open();

            string sql = $"Delete from Order_ where Id = '{id}' ";

            // Создать объект Command
            OracleCommand cmd = new OracleCommand();

            // Сочетать с Connection
            cmd.Connection = connection;

            // Command Text.
            cmd.CommandText = sql;


            // Выполнить Command (Используется для delete,insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Row Count affected = " + rowCount);
        }

        public Order Get(int id)
        {
            connection.Open();

            OracleCommand command = new OracleCommand();
            command.CommandText = "SELECT * FROM Order_ where Id = " + id;
          
            command.Connection = connection;
            DbDataReader reader = command.ExecuteReader();
            Order order = new Order();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    order.Id = (int)(long)reader.GetValue(0);
                    order.CustomerName = (string)reader.GetValue(1);
                    order.ServiceId = (int)(long)reader.GetValue(2);
                    order.UnitsAmount = (decimal)reader.GetValue(3);
                    order.OrderDate = (DateTime)reader.GetValue(4);
                    order.OrderExec = (DateTime)reader.GetValue(5);
                }
            }
            reader.Close();

            connection.Close();
            return order.Id == 0 ? null : order;
        }

        public List<Order> GetAll()
        {
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.CommandText = "Select * from Order_";
            command.Connection = connection;
            DbDataReader reader = command.ExecuteReader();
            List<Order> orders = new();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    Order order = new Order((int)(long)reader.GetValue(0), (string)reader.GetValue(1), (int)(long)reader.GetValue(2),
                        (decimal)reader.GetValue(3), (DateTime)reader.GetValue(4), (DateTime)reader.GetValue(5));
                    orders.Add(order);
                }
            }

            connection.Close();
            return orders;
        }

        public int Insert(Order order)
        {
            connection.Open();

            OracleCommand command = new OracleCommand();


            command.CommandText = @$"INSERT INTO Order_(CustomerName,ServiceId,UnitsAmount,OrderDate,OrderExec) values('{order.CustomerName}',{order.ServiceId},{order.UnitsAmount},'{order.OrderDate}','{order.OrderExec}')";
            command.Connection = connection;


            // Выполнить Command (Используется для delete,insert, update).
            int rowCount = command.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("Row Count affected = " + rowCount);
            return rowCount;
        }

        public int Update(Order order)
        {
            connection.Open();

            OracleCommand command = new OracleCommand();


            command.CommandText = @$"UPDATE Order_ set 
            CustomerName = '{order.CustomerName}', ServiceId = '{order.ServiceId}',
            UnitsAmount = '{order.UnitsAmount}',OrderDate = '{order.OrderDate}', OrderExec = '{order.OrderExec}' where Id = '{order.Id}'";
            command.Connection = connection;


            // Выполнить Command (Используется для delete,insert, update).
            int rowCount = command.ExecuteNonQuery();

            Console.WriteLine("Row Count affected = " + rowCount);
            connection.Close();

            return rowCount;
        }

        public void PrintTableHeader()
        {
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.CommandText = "Select * from Order_";
            command.Connection = connection;
            DbDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n{0,5}{1,25}{2,15}{3,15}{4,25}{5,25}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4), reader.GetName(5));
            connection.Close();
        }

        public void PrintRow(Order order)
        {
            if (order != null)
                Console.WriteLine("{0,5}{1,25}{2,15}{3,15}{4,25}{5,25}", order.Id, order.CustomerName, order.ServiceId, order.UnitsAmount, order.OrderDate, order.OrderExec);
        }
    }
}
