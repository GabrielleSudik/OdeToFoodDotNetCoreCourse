using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood
{
    public class ListModel : PageModel
    {
        public string Message { get; set; } //we created this to be used in OnGet()
        //in the html page, @Model.Message will print the Message on the page.

        //create a constructor:
        //inject your configuration (in our case, that means appsettings.json)
        //later, we also added injecting the IRestaurantData because we know we need that too.
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;

        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }

        public string Message2 { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        public void OnGet() //empty OnGet() method was boilerplate.
        {
            Message = "Hello, Madames.";
            Message2 = config["Message"]; //checks appsettings.json, to see the value of "Message"
            //those two were just for testing/learning how this work.

            //our real goal in this app is showing a list of the restaurants.
            //so we set the Restaurants property (defined above) to something that will actually get the list:
            Restaurants = restaurantData.GetAll();
            //restaurantData refers to the IRestaurantData interface (this looks familiar)
            //and GetAll() comes from the method that implements it -- InMemoryRestaurantData class.
        }
    }
}

//Notes.
//Relationship between List.cshtml and List.cshtml.cs
//The heavy lifting happens in the .cs

//The primary purpose of the PageModel classes
//are to respond to http requests with the OnGet() method.
//ie, what happens when you link to a page.
//Later we will learn how to use pages to post, too.
