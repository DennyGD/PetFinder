namespace PetFinder.Services.Web
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Data.Models;
    using PetFinder.Common.Constants;

    public class DropdownListService : IDropdownListService
    {
        public IEnumerable<SelectListItem> RegionsForSearch(List<Region> regions, string defaultSelectedRegion)
        {
            defaultSelectedRegion = defaultSelectedRegion ?? Others.AllRegions;

            var result = new List<SelectListItem>();
            result.Add(new SelectListItem()
            {
                Text = Others.AllRegions,
                Value = Others.AllRegions,
                Selected = defaultSelectedRegion == Others.AllRegions
            });

            regions.ForEach(x => result.Add(new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name,
                Selected = x.Name == defaultSelectedRegion
            }));

            return result;
        }

        public IEnumerable<SelectListItem> RegionsForAddition(List<Region> regions)
        {
            var result = new List<SelectListItem>();
            regions.ForEach(x => result.Add(new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }));

            return result;
        }

        public IEnumerable<SelectListItem> PostCategories(List<PostCategory> postCategories)
        {
            var result = new List<SelectListItem>();
            postCategories.ForEach(x => result.Add(new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }));

            return result;
        }
    }
}
