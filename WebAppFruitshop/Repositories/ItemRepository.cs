using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFruitshop.Models;

namespace WebAppFruitshop.Repositories
{
    public class ItemRepository
    {
        private FruitShopDBEntities objfruitShopDBEntities;
        public ItemRepository()
        {
            objfruitShopDBEntities = new FruitShopDBEntities();
        }

        public IEnumerable<SelectListItem> GetAllItems()
        {
            var objselectListItems = new List<SelectListItem>();
            objselectListItems = (from obj in objfruitShopDBEntities.Items
                                  select new SelectListItem()
                                  {
                                      Text = obj.ItemName,
                                      Value = obj.ItemId .ToString(),
                                      Selected = false
                                  }).ToList();
            return objselectListItems;

        }
    }
}