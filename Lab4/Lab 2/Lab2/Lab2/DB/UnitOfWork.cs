using Lab2.DB.Impl;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DB
{


    class DBUtils
    {
        public static OracleConnection GetDBConnectionBuilder(string host, int port, String sid, String user, String password)
        {

            Console.WriteLine("Getting Connection ...");

            // Connection String для прямого подключения к Oracle.
            string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                 + host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                 + sid + ")));Password=" + password + ";User ID=" + user;


            OracleConnection conn = new OracleConnection();

            conn.ConnectionString = connString;

            return conn;
        }

        public static OracleConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 1521;
            string sid = "orcl";
            string user = "test";
            string password = "1111";

            return GetDBConnectionBuilder(host, port, sid, user, password);
        }
    }

    class UnitOfWork : IDisposable
    {
        private SqlConnection connection = new SqlConnection(@"Server=localhost;Database=TRANS;Trusted_Connection=True;");
        private OracleConnection connectionOracle = DBUtils.GetDBConnection();
        private SqliteConnection connectionSQLite = new SqliteConnection(@"Data Source=D:\Education\3 курс 2 сем\DB\Lab4\Lab 2\Lab2\TRANS.db;");
        

        private OrderRepository orderRepository;
        private CityRepository cityRepository;
        private CustomerRepository customerRepository;
        private RouteRepository routeRepository;
        private ServiceRepository serviceRepository;
        private ServiceTypeRepository serviceTypeRepository;

        private OrderRepositoryOracle orderRepositoryOracle;

        private OrderRepositorySQLite orderRepositorySQLLite;


        public OrderRepositorySQLite OrderRepositorySQLLite
        {
            get
            {
                if (orderRepositorySQLLite == null)
                    orderRepositorySQLLite = new OrderRepositorySQLite(connectionSQLite);
                return orderRepositorySQLLite;
            }
        }

        public OrderRepositoryOracle OrderRepositoryOracle
        {
            get
            {
                if (orderRepositoryOracle == null)
                    orderRepositoryOracle = new OrderRepositoryOracle(connectionOracle);
                return orderRepositoryOracle;
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(connection);
                return orderRepository;
            }
        }

        public CityRepository CityRepository
        {
            get
            {
                if (cityRepository == null)
                    cityRepository = new CityRepository(connection);
                return cityRepository;
            }
        }

        public CustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(connection);
                return customerRepository;
            }
        }

        public RouteRepository RouteRepository
        {
            get
            {
                if (routeRepository == null)
                    routeRepository = new RouteRepository(connection);
                return routeRepository;
            }
        }

        public ServiceRepository ServiceRepository
        {
            get
            {
                if (serviceRepository == null)
                    serviceRepository = new ServiceRepository(connection);
                return serviceRepository;
            }
        }

        public ServiceTypeRepository ServiceTypeRepository
        {
            get
            {
                if (serviceTypeRepository == null)
                    serviceTypeRepository = new ServiceTypeRepository(connection);
                return serviceTypeRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    connection.Close();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
