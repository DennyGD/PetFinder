namespace PetFinder.Services.Web
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Data.Models;

    public interface IDropdownListService
    {
        IEnumerable<SelectListItem> RegionsForSearch(List<Region> regions, string defaultSelectedRegion);

        IEnumerable<SelectListItem> RegionsForAddition(List<Region> regions);

        IEnumerable<SelectListItem> PostCategories(List<PostCategory> postCategories);
    }
}
