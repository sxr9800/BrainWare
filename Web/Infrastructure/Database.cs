using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using Web.Models;

    public class Database
    {
        private readonly SqlConnection _connection;

        public Database()
        {
            try
            {
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["BrainWareConnectionString"].ConnectionString;

                _connection = new SqlConnection(connection);

                _connection.Open();
            }
            catch (Exception e)
            {
                //log error area
            }
        }

        public DbDataReader ExecuteReader(string query)
        {

            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteReader();
        }

        public int ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQuery();
        }

        public List<Order> GetCompanysByID(int CompanyID)
        {
            List<Order> Orders = new List<Order>();
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("GetCompanyOrdersByID", _connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            Orders.Add(new Order()
                            {
                                CompanyName = sqlDataReader["Name"].ToString(),
                                Description = sqlDataReader["description"].ToString(),
                                OrderId = Convert.ToInt32(sqlDataReader["order_id"]),
                                OrderProducts = new List<OrderProduct>()
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Some error handling to go here / log somewhere
            }

            return Orders;
        }

        public List<OrderProduct> GetProductsByCompanyID(int CompanyID)
        {
            List<OrderProduct> Products = new List<OrderProduct>();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand("GetCompanyProductsByCompanyID", _connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));

                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            Products.Add(new OrderProduct()
                            {
                                OrderId = Convert.ToInt32(sqlDataReader["order_id"]),
                                ProductId = Convert.ToInt32(sqlDataReader["product_id"]),
                                Price = Convert.ToDecimal(sqlDataReader["price"]),
                                Quantity = Convert.ToInt32(sqlDataReader["quantity"]),
                                Product = new Product()
                                {
                                    Name = sqlDataReader["name"].ToString(),
                                    Price = Convert.ToDecimal(sqlDataReader["productPrice"])
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Some error handling to go here / log somewhere
            }

            return Products;
        }



    }
}