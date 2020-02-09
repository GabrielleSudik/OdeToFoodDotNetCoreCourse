using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

//this is created for the third module on editing data

namespace OdeToFood
{
    public class EditModel : PageModel
    {
        //the usual setup of fields and properties
        private readonly IRestaurantData restaurantData;

        //this is new -- htmlHelper will go along with the property Cuisines,
        //which will be a drop-down list when users edit restaurants.
        private readonly IHtmlHelper htmlHelper;

        [BindProperty] //default is for Post, so no need to specify that.
                        //we added this attribute so the html can use this as both and input and output property.
        public Restaurant Restaurant { get; set; }

        //this next one is new. it's for the drop-down selection of CuisineTypes, 
        //based on the enum. More code below.
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        //and remember, with asp-for in your html,
        //you can bind against any properties, including this one and Restaurant, above.

        //the constructor, injecting the restaurant data AND the htmlHelper, which is for the Cuisines drop-down.
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        //and like before, edit OnGet as necessary,
        //including changing void to IActionResult
        public IActionResult OnGet(int? restaurantId)
        {
            //this is new, it sets Cuisines to be a list of the Cuisine enums in the html drop-down.
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            //this part is sort of like the details page.
            //added the if to check if it's null (ie, when creating a new one, not checking an existing one)
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurantById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        //OnGet() gets data that already exists
        //OnPost() - created by us - updates the data.
        //It needs a data source to work from, so let's start with ours, which is IRestaurantData.cs.
        //Later we add logic to allow this method to also create new restaurants.
        //you commented out the logic from just Edit, new code does Edit and Add
        public IActionResult OnPost()
        {
            ////we need model validation, don't trust the user.
            ////we'll start it in the data model with some attributes -- Restaurant.cs
            ////but we also need to use ModelState to check them:
            //if (ModelState.IsValid)
            //{
            //    restaurantData.Update(Restaurant);
            //    restaurantData.Commit();
            //    return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            //    //restaurantId comes from the html for List page: asp-route-restaurantId attribute.
            //    //you've seen that id used elsewhere to id a type of page.
            //}
            ////and finally, in the html, add some error messages for the user.
            ////asp-validation-for then which field you're checking.
            ////asp has some built in error messages.

            //Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            //return Page();

            //revised code for edit and add:
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0) //if it already exists, update.
            {
                restaurantData.Update(Restaurant);
            }
            else //if not, add.
            {
                restaurantData.Add(Restaurant);
            }

            restaurantData.Commit();
            //return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            //the "new" part is what other info is being passed. It will turn out like /Detail/5 or whatever.
            //you can pass more info along that way too to the /Detail page.
            //how?
            TempData["Message"] = "Restaurant saved."; //temp data is an asp.net thing.
                //it only hangs out briefly.
                //to use it wisely, check out Detail.cshtml.cs and the Message stuff there.
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}