using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Lab2.DAL
{
    public class CityRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Model1"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddCity(City obj)
        {

            connection();
            string query = "Insert into City(Id,CityName) values(@Id,@CityName)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@CityName", obj.CityName);

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

        public List<City> GetAllCitys()
        {
            connection();
            List<City> CityList = new List<City>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from City", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                CityList.Add(

                    new City
                    {

                        Id = Convert.ToInt32(dr["Id"]) ,
                        CityName = Convert.ToString(dr["CityName"])

                    }
                    ) ;
            }
            return CityList;
        }

        public bool UpdateCity(City obj)
        {

            connection();
            con.Open();
            string query = "Update City SET CityName=@CityName where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@CityName", obj.CityName);

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

        public bool DeleteCity(int Id)
        {

            connection();
            string query = "Delete from City where Id=@Id";
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