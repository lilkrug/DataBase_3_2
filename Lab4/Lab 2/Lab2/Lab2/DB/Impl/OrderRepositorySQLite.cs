using Lab2.DB.Interfaces;
using Lab2.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DB.Impl
{
    class OrderRepositorySQLite : IRepository<Order>
    {
        public SqliteConnection connection;

        public OrderRepositorySQLite(SqliteConnection connection)
        {
            this.connection = connection;

        }
        public void Delete(int id)
        {
            connection.Open();

            string sql = $"Delete from [Order] where Id = '{id}' ";

            // Создать объект Command
            SqliteCommand cmd = new SqliteCommand();

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

            SqliteCommand command = new SqliteCommand();
            command.CommandText = "SELECT * FROM [Order] where Id = " + id;

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
                    order.UnitsAmount = (decimal)(double)reader.GetValue(3);
                    order.OrderDate = DateTime.Parse((string)reader.GetValue(4));
                    order.OrderExec = DateTime.Parse((string)reader.GetValue(5));
                }
            }
            reader.Close();

            connection.Close();
            return order.Id == 0 ? null : order;
        }

        public List<Order> GetAll()
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.CommandText = "Select * from [Order]";
            command.Connection = connection;
            DbDataReader reader = command.ExecuteReader();
            List<Order> orders = new();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    Order order = new Order((int)(long)reader.GetValue(0), (string)reader.GetValue(1), (int)(long)reader.GetValue(2),
                        (decimal)(double)reader.GetValue(3), DateTime.Parse((string)reader.GetValue(4)), DateTime.Parse((string)reader.GetValue(5)));
                    orders.Add(order);
                }
            }

            connection.Close();
            return orders;
        }

        public int Insert(Order order)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();


            command.CommandText = $"INSERT INTO [Order](CustomerName,ServiceId,UnitsAmount,OrderDate,OrderExec) values('{order.CustomerName}',{order.ServiceId},{order.UnitsAmount},'{order.OrderDate}','{order.OrderExec}')";
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

            SqliteCommand command = new SqliteCommand();


            command.CommandText = @$"UPDATE [Order] set 
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
            SqliteCommand command = new SqliteCommand();
            command.CommandText = "Select * from [Order]";
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

        public void ExecuteTran()
        {
            connection.Open();

            Console.WriteLine("\nРезультат выполнения запроса:\n");

            SqliteTransaction transaction = connection.BeginTransaction();
            SqliteCommand command = new SqliteCommand("Select * from [Order] where id between 95 and 120", connection, transaction);
            

            DbDataReader reader = command.ExecuteReader();
            PrintFromReader(reader);
            reader.Close();
            try
            {
                command.CommandText = "delete from [Order] where id = (select id from [Order] where id between 95 and 120 limit 1)";
                int rowCount = command.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
                transaction.Rollback();
            }

            command.CommandText = "Select * from [Order] where id between 95 and 120";
            command.Transaction = transaction;

            reader = command.ExecuteReader();

            PrintFromReader(reader);
            reader.Close();

            transaction.Rollback();
            SqliteCommand command1 = new SqliteCommand("Select * from [Order] where id between 95 and 120", connection);
            Console.WriteLine("After rollback:");
            reader = command1.ExecuteReader();
            PrintFromReader(reader);
            reader.Close();

            connection.Close();
        }

        public void PrintFromReader(DbDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write("{0,20}", reader.GetName(i));
            }
            Console.WriteLine();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write("{0,20}", reader.GetValue(i).ToString());
                    }
                    Console.WriteLine();
                }
            }
        }

        public void Execute(string query)
        {
            connection.Open();

            Console.WriteLine("\nРезультат выполнения запроса:\n");


            SqliteCommand command = new SqliteCommand();
            command.CommandText = query;
            command.Connection = connection;
            DbDataReader reader = command.ExecuteReader();
            for(int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write("{0,27}", reader.GetName(i));
            }
            Console.WriteLine();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write("{0,27}", reader.GetValue(i).ToString());
                    }
                    Console.WriteLine();
                }
            }

            connection.Close();
        }


    }
}
