using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Lab2.DAL
{
    public class RouteRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Model1"].ToString();
            con = new SqlConnection(constr);

        }
        public bool AddRoute(Route obj)
        {

            connection();
            string query = "Insert into Route(RouteName,Distance,DeparturePoint,ArrivalPoint) values(@RouteName,@Distance,@DeparturePoint,@ArrivalPoint)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@RouteName", obj.RouteName);
            cmd.Parameters.AddWithValue("@Distance", obj.Distance);
            cmd.Parameters.AddWithValue("@DeparturePoint", obj.DeparturePoint);
            cmd.Parameters.AddWithValue("@ArrivalPoint", obj.ArrivalPoint);

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
        public List<Route> GetAllRoutes()
        {
            connection();
            List<Route> RouteList = new List<Route>();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Route", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                RouteList.Add(

                    new Route
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        RouteName = Convert.ToString(dr["RouteName"]),
                        Distance = Convert.ToDecimal(dr["Distance"]),
                        DeparturePoint = Convert.ToString(dr["DeparturePoint"]),
                        ArrivalPoint = Convert.ToString(dr["ArrivalPoint"])
                    }
                    );
            }
            return RouteList;
        }

       
        public bool UpdateRoute(Route obj)
        {

            connection();
            con.Open();
            string query = "Update Route SET RouteName=@RouteName , Distance=@Distance, DeparturePoint=@DeparturePoint, ArrivalPoint=@ArrivalPoint  where  Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@RouteName", obj.RouteName);
            cmd.Parameters.AddWithValue("@Distance", obj.Distance);
            cmd.Parameters.AddWithValue("@DeparturePoint", obj.DeparturePoint);
            cmd.Parameters.AddWithValue("@ArrivalPoint", obj.ArrivalPoint);
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

        public bool DeleteRoute(int Id)
        {

            connection();
            string query = "Delete from Route where Id=@Id";
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