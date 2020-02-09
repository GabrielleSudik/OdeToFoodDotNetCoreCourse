using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

//after you created a page with a list of all restaurants
//and the form to search them, you are creating a way to check details of each.
//don't forget the matching html page, which will display to the user.

//List.html contains some of the code to make this work: buttons to reach each detail page.

namespace OdeToFood
{
    public class DetailModel : PageModel
    {
        public Restaurant Restaurant { get; set; }
        public readonly IRestaurantData restaurantData;

        [TempData]//flags this property for the TempData called "Message" coming from Edit.cshtml.cs.
        public string Message { get; set; } 

        //make a constructor
        //you're injecting the restaurant data here,
        //to be used in OnGet() below.
        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        //again, publid void OnGet() comes standard and blank with all Razor Pages.
        //public void OnGet(int restaurantId) //commented out to return something: an IActionResult
        //we are returning IActionResult instead of void in case there is an error, like user trying to 
        //get a page that doesn't exist.
        public IActionResult OnGet(int restaurantId)
        {
            //Restaurant = new Restaurant();
            //Restaurant.Id = restaurantId;
            //that was before you had a constructor that injected the restaurant data. instead:
            //simply call the relevant method:
            Restaurant = restaurantData.GetRestaurantById(restaurantId);
            //now clicking the link to a restaurant's details page will show each data field properly.

            //if the page doesn't exist, return the NotFound page (which we create ourselves).
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page(); //this basically does the same as "returning" void -- it just returns the page.
            //the boilerplate "void OnGet()" method just returns whatever page you created.
        }
    }
}