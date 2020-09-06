using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class CatalogIndexViewModel
    {
        public int? BrandFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
        public List<SelectListItem> Brands { get; set; }
    }
}
