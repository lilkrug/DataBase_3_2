using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Lab2.DAL
{
    public class ServiceRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Model1"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddService(Service obj)
        {

            connection();
            string query = "Insert into Service(Id,ServiceType,RouteName) values(@Id,@ServiceType,@RouteName)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@ServiceType", obj.ServiceType);
            cmd.Parameters.AddWithValue("@RouteName", obj.RouteName);


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
        public List<Service> GetAllServices()
        {
            connection();
            List<Service> ServiceList = new List<Service>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Service", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                ServiceList.Add(

                    new Service
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        ServiceType = Convert.ToString(dr["ServiceType"]),
                        RouteName = Convert.ToString(dr["RouteName"])
                    }
                    );
            }
            return ServiceList;
        }


        public bool UpdateService(Service obj)
        {

            connection();
            con.Open();
            string query = "Update Service SET ServiceType=@ServiceType, RouteName=@RouteName  where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@ServiceType", obj.ServiceType);
            cmd.Parameters.AddWithValue("@RouteName", obj.RouteName);

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

        public bool DeleteService(int Id)
        {

            connection();
            string query = "Delete from Service where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", Id);

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