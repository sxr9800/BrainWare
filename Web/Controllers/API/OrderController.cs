using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    using System.Web.Mvc;
    using Infrastructure;
    using Models;

    public class OrderController : ApiController
    {
        [HttpGet]
        public IEnumerable<Order> GetOrders(int id)
        {
            List<Order> orders = new List<Order>();
            try
            {
                var data = new OrderService();
                orders = data.GetOrdersForCompany(id);
            }
            catch (Exception e)
            {
                //log error here
            }
            return orders;
        }
    }
}
