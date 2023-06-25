using Lab2.DB.Interfaces;
using Lab2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DB.Impl
{
    class CityRepository : IRepository<City>
    {
        public SqlConnection connection;

        public CityRepository(SqlConnection connection)
        {
            this.connection = connection;

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public City Get(int id)
        {
            bool isOpened = false;
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                isOpened = true;
            }

            SqlCommand command = new ();
            command.CommandText = "SELECT * FROM [City] where Id = @Id";
            command.Parameters.AddWithValue("@Id", id);
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            City city = new City();
            int p = 0;
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    city.Id = (int)reader.GetValue(0);
                    city.CityName = (string)reader.GetValue(1);
                    p = (int)reader.GetValue(2);
                }
            }
            reader.Close();

            Console.WriteLine($"{city.Id,15}{city.CityName,15}{p,5}");
            if(isOpened)
                connection.Close();
            return city.Id == 0 ? null : city;
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();

        }

        public int Insert(City customer)
        {
            throw new NotImplementedException();
        }

        public int Update(City customer)
        {
            throw new NotImplementedException();
        }

        public void PrintRowNearest((string cityName, double dist) item)
        {
                Console.WriteLine($"{item.Item1,20}{item.Item2,20}");
        }

        public void getDistance(int idCity,int idCityT)
        {
            string sqlExpression = "SELECT dbo.getDistanse(@idCity,@idCityT) [Расстояние]";
            connection.Open();

            Console.WriteLine("\nРезультат выполнения запроса:\n");


            SqlCommand command = new SqlCommand(sqlExpression, connection);


            SqlParameter idoneParam = new SqlParameter
            {
                ParameterName = "@idCity",
                Value = idCity
            };
            command.Parameters.Add(idoneParam);

            SqlParameter idtwoParam = new SqlParameter
            {
                ParameterName = "@idCityT",
                Value = idCityT
            };
            command.Parameters.Add(idtwoParam);
            City first = Get(idCity);
            City second = Get(idCityT);
            SqlDataReader result = command.ExecuteReader();

            

            result.Read();
            Console.WriteLine($"Расстояние между городами {first.CityName} и {second.CityName}: {result.GetValue(0):F3}");

            connection.Close();
            
        }

        public void findNearest(float latitude, float longitude)
        {
            connection.Open();
            string sqlExpression = "SELECT * from dbo.findNearest(@latitude,@longitude)";
            Console.WriteLine("\nРезультат выполнения запроса:\n");


            SqlCommand command = new SqlCommand(sqlExpression, connection);

            SqlParameter latitudeParam = new SqlParameter
            {
                ParameterName = "@latitude",
                Value = latitude
            };
            command.Parameters.Add(latitudeParam);

            SqlParameter longitudeParam = new SqlParameter
            {
                ParameterName = "@longitude",
                Value = longitude
            };
            command.Parameters.Add(longitudeParam);

            SqlDataReader result = command.ExecuteReader();

            Console.WriteLine("\n{0,20}{1,20}", result.GetName(0), result.GetName(1));
            if(result.HasRows)
            {
                while (result.Read())
                {
                    PrintRowNearest(((string)result.GetValue(0), (double)result.GetValue(1)));
                }
            }
            
            connection.Close();

        }

        public void getIntersect(int idCityOne, int idCityTwo, int idCityThree, int idCityFour)
        {
            connection.Open();
            string sqlExpression = "SELECT dbo.getIntersect(@idCityOne,@idCityTwo,@idCityThree,@idCityFour) [Пересекается]";
            Console.WriteLine("\nРезультат выполнения запроса:\n");


            SqlCommand command = new SqlCommand(sqlExpression, connection);



            SqlParameter idCityOneP = new SqlParameter
            {
                ParameterName = "@idCityOne",
                Value = idCityOne
            };
            command.Parameters.Add(idCityOneP);

            SqlParameter idCityTwoP = new SqlParameter
            {
                ParameterName = "@idCityTwo",
                Value = idCityTwo
            };
            command.Parameters.Add(idCityTwoP);

            SqlParameter idCityThreeP = new SqlParameter
            {
                ParameterName = "@idCityThree",
                Value = idCityThree
            };
            command.Parameters.Add(idCityThreeP);

            SqlParameter idCityFourP = new SqlParameter
            {
                ParameterName = "@idCityFour",
                Value = idCityFour
            };
            command.Parameters.Add(idCityFourP);

            

            City first = Get(idCityOne);
            City second = Get(idCityTwo);
            City third = Get(idCityThree);
            City fourth = Get(idCityFour);

            SqlDataReader result = command.ExecuteReader();

            result.Read();
            bool isIn = (bool)result.GetValue(0);

            if (isIn)
            {
                Console.WriteLine($"Маршруты {first.CityName}-{second.CityName} и {third.CityName}-{fourth.CityName} пересекаются");
            }
            else
            {
                Console.WriteLine($"Маршруты {first.CityName}-{second.CityName} и {third.CityName}-{fourth.CityName} не пересекаются");
            }

            connection.Close();

        }

    }
}
