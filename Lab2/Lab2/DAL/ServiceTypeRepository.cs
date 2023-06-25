using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Lab2.DAL
{
    public class ServiceTypeRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Model1"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddServiceType(ServiceType obj)
        {

            connection();
            string query = "Insert into ServiceType(Id,ServiceName) values(@Id,@ServiceName)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@ServiceName", obj.ServiceName);



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
        public List<ServiceType> GetAllServiceTypes()
        {
            connection();
            List<ServiceType> ServiceTypeList = new List<ServiceType>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from ServiceType", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                ServiceTypeList.Add(

                    new ServiceType
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        ServiceName = Convert.ToString(dr["ServiceName"])
                    }
                    );
            }
            return ServiceTypeList;
        }


        public bool UpdateServiceType(ServiceType obj)
        {

            connection();
            con.Open();
            string query = "Update ServiceType SET ServiceName=@ServiceName where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@ServiceName", obj.ServiceName);
         

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

        public bool DeleteServiceType(int Id)
        {

            connection();
            string query = "Delete from ServiceType where Id=@Id";
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