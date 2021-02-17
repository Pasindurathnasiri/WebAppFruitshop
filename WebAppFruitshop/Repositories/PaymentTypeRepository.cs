using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFruitshop.Models;

namespace WebAppFruitshop.Repositories
{
    public class PaymentTypeRepository
    {
        private FruitShopDBEntities objfruitShopDBEntities;
        public PaymentTypeRepository()
        {
            objfruitShopDBEntities = new FruitShopDBEntities();
        }

        public IEnumerable<SelectListItem> GetAllPaymentTypes()
        {
            var objselectListItems = new List<SelectListItem>();
            objselectListItems = (from obj in objfruitShopDBEntities.PaymentTypes
                                  select new SelectListItem()
                                  {
                                      Text = obj.PaymentTypeName,
                                      Value = obj.PaymentTypeId.ToString(),
                                      Selected = true
                                  }).ToList();
            return objselectListItems;

        }
    }
}