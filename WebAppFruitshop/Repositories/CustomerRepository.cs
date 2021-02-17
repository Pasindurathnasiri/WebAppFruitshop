using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFruitshop.Models;

namespace WebAppFruitshop.Repositories
{
    public class CustomerRepository
    {
        private FruitShopDBEntities objfruitShopDBEntities;
        public CustomerRepository()
        {
            objfruitShopDBEntities = new FruitShopDBEntities();
        }

        public IEnumerable<SelectListItem> GetAllCustomers()
        {
            var objselectListItems = new List<SelectListItem>();
            objselectListItems = (from obj in objfruitShopDBEntities.Customers
                                  select new SelectListItem()
                                  {
                                      Text = obj.CustomerName,
                                      Value = obj.CustomerId.ToString(),
                                      Selected = true
                                  }).ToList();
            return objselectListItems;

        }
    }
}