using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    using System.Data;
    using Models;

    public class OrderService
    {
        public List<Order> GetOrdersForCompany(int CompanyId)
        {
            var database = new Database();

            List<Order> Orders = new List<Order>();
            List<OrderProduct> Product = new List<OrderProduct>();

            try
            {
                Orders = database.GetCompanysByID(CompanyId);
                Product = database.GetProductsByCompanyID(CompanyId);

                foreach (var order in Orders)
                {
                    foreach (var orderproduct in Product)
                    {
                        if (orderproduct.OrderId != order.OrderId)
                            continue;

                        order.OrderProducts.Add(orderproduct);
                        order.OrderTotal = order.OrderTotal + (orderproduct.Price * orderproduct.Quantity);
                    }
                }
            }
            catch (Exception e)
            {
                //log error through here
            }

            return Orders;
        }
    }
}