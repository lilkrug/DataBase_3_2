using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Lab2.DAL
{
    public class CustomerRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Model1"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddCustomer(Customer obj)
        {

            connection();
            string query = "Insert into Customer(Id,CustomerName) values(@Id,@CustomerName)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@CustomerName", obj.CustomerName);

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
        public List<Customer> GetAllCustomers()
        {
            connection();
            List<Customer> CustomerList = new List<Customer>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Customer", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                CustomerList.Add(

                    new Customer
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        CustomerName = Convert.ToString(dr["CustomerName"])

                    }
                    );
            }
            return CustomerList;
        }


        public bool UpdateCustomer(Customer obj)
        {

            connection();
            con.Open();
            string query = "Update Customer SET CustomerName=@CustomerName where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@CustomerName", obj.CustomerName);

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

        public bool DeleteCustomer(int Id)
        {

            connection();
            string query = "Delete from Customer where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", Id);

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