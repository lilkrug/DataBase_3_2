using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


namespace Lab2.DAL
{
    public class OrderRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Model1"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddOrder(Order obj)
        {

            connection();
            string query = "Insert into Order(Id,CustomerName,ServiceId,OrderDate,OrderExec) values(@Id,@CustomerName,@ServiceId,@OrderDate,@OrderExec)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@CustomerName", obj.CustomerName);
            cmd.Parameters.AddWithValue("@ServiceId", obj.ServiceId);
            cmd.Parameters.AddWithValue("@OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@OrderExec", obj.OrderExec);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Order> GetAllOrders()
        {
            connection();
            List<Order> OrderList = new List<Order>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Order", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                OrderList.Add(

                    new Order
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        CustomerName = Convert.ToString(dr["CustomerName"]),
                        ServiceId = Convert.ToInt32(dr["SeviceId"]),
                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                        OrderExec = Convert.ToDateTime(dr["OrderExec"])
                    }
                    );
            }
            return OrderList;
        }


        public bool UpdateOrder(Order obj)
        {

            connection();
            con.Open();
            string query = "Update Order SET CustomerName=@CustomerName , ServiceId=@ServiceId, OrderDate=@OrderDate, OrderExec=@OrderExec  where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@CustomerName", obj.CustomerName);
            cmd.Parameters.AddWithValue("@ServiceId", obj.ServiceId);
            cmd.Parameters.AddWithValue("@OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@OrderExec", obj.OrderExec);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteOrder(int Id, int ServiceId)
        {

            connection();
            string query = "Delete from Order where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@Serviceid", ServiceId);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}