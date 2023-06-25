using Lab2.DB.Interfaces;
using Lab2.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DB.Impl
{
    class OrderRepository : IRepository<Order>
    {
        public SqlConnection connection;

        public OrderRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Delete(int id)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "Delete [Order] where Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            command.Connection = connection;

            connection.Open();
            int number = command.ExecuteNonQuery() - 1;
            Console.WriteLine("\nУдалёно объектов: {0}", number == -1 ? 0 : number);
            connection.Close();

        }

        public Order Get(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Order] where Id = @Id";
            command.Parameters.AddWithValue("@Id", id);
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            Order order = new Order();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    order.Id = (int)reader.GetValue(0);
                    order.CustomerName = (string)reader.GetValue(1);
                    order.ServiceId = (int)reader.GetValue(2);
                    order.UnitsAmount = (decimal)reader.GetValue(3);
                    order.OrderDate = (DateTime)reader.GetValue(4);
                    order.OrderExec = (DateTime)reader.GetValue(5);
                }
            }
            reader.Close();

            connection.Close();
            return order.Id == 0 ? null : order ;
        }

        public List<Order> GetAll()
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [Order]";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            List<Order> orders = new();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    Order order = new Order((int)reader.GetValue(0), (string)reader.GetValue(1), (int)reader.GetValue(2),
                        (decimal)reader.GetValue(3), (DateTime)reader.GetValue(4), (DateTime)reader.GetValue(5));
                    orders.Add(order);
                }
            }

            connection.Close();
            return orders;
        }

        public int Insert(Order order)
        {

            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Order](CustomerName,ServiceId,UnitsAmount,OrderDate,OrderExec) values(@CustomerName,@ServiceId,@UnitsAmount,@OrderDate,@OrderExec)";
            command.Parameters.AddWithValue("@CustomerName", order.CustomerName);
            command.Parameters.AddWithValue("@ServiceId", order.ServiceId);
            command.Parameters.AddWithValue("@UnitsAmount", order.UnitsAmount);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@OrderExec", order.OrderExec);
            command.Connection = connection;
            
            connection.Open();

            int number = command.ExecuteNonQuery();
            Console.WriteLine("\nДобавлено объектов: {0}", number);

            connection.Close();
            return number;
        }

        public int Update(Order order)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Order] set CustomerName = @CustomerName, ServiceId = @ServiceId, UnitsAmount = @UnitsAmount,OrderDate = @OrderDate, OrderExec = @OrderExec where Id = @Id";
            command.Parameters.AddWithValue("@Id", order.Id);
            command.Parameters.AddWithValue("@CustomerName", order.CustomerName);
            command.Parameters.AddWithValue("@ServiceId", order.ServiceId);
            command.Parameters.AddWithValue("@UnitsAmount", order.UnitsAmount);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            command.Parameters.AddWithValue("@OrderExec", order.OrderExec);
            command.Connection = connection;

            int number = command.ExecuteNonQuery();
            Console.WriteLine("\nИзменено объектов: {0}", number);

            connection.Close();
            return number;

        }

        public void PrintTableHeader()
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from [Order]";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n{0,5}{1,25}{2,15}{3,15}{4,25}{5,25}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4), reader.GetName(5));
            connection.Close();
        }

        public void PrintRow(Order order)
        {
            if (order != null)
            Console.WriteLine("{0,5}{1,25}{2,15}{3,15}{4,25}{5,25}", order.Id, order.CustomerName, order.ServiceId, order.UnitsAmount, order.OrderDate, order.OrderExec);
        }

        public void Execute(string query)
        {
            connection.Open();

            Console.WriteLine("\nРезультат выполнения запроса:\n");


            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            foreach (DataColumn column in ds.Tables[0].Columns)
                Console.Write("{0,27}", column.ColumnName);
            Console.WriteLine();
            foreach (DataRow row in ds.Tables[0].Rows)
            {             
                var cells = row.ItemArray;
                foreach (object cell in cells)
                   Console.Write("{0,27}", cell);
                Console.WriteLine();
            }

            connection.Close();
        }

    }
}
