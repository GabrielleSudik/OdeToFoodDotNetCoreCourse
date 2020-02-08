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
        
        [BindProperty(SupportsGet = true)] //turns the property from merely an output model to a dual input/output model
        public string SearchTerm { get; set; } //this is used in conjunction with an input model searchTerm
            //so it can be an output model as well.

        public void OnGet(string searchTerm) //empty OnGet() method was boilerplate.
            //we added a parameter in module two, to take in what data a user enters into a form.
            //"searchTerm" is what we named the data in the html's form.
            //it just has to be in both places.
        {
            Message = "Hello, Madames.";
            Message2 = config["Message"]; //checks appsettings.json, to see the value of "Message"
                                          //those two were just for testing/learning how this work.

            //Model Binding lesson:
            //HttpContext is the class(?) that handles data in http requests and responses.
            //But... We're not using it here for some reason??
            //HttpContext.Request....

            //our real goal in this app is showing a list of the restaurants.
            //so we set the Restaurants property (defined above) to something that will actually get the list:
            //restaurantData refers to the IRestaurantData interface (this looks familiar)
            //and GetAll() comes from the method that implements it -- InMemoryRestaurantData class.
            //Restaurants = restaurantData.GetAll(); //commented out/updated for module two

            //Restaurants = restaurantData.GetRestaurantsByName(searchTerm); //used when searchTerm was input only.
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm); //used when SearchTerm is input and output.

            //prof idea: searchTerm is an "input model" and Message and Restaurants are "output models"
            //what if you want searchTerm to be both (eg, to keep the searchTerm showing in the
            //search field even after the user hits the search button)?
            //A: add a new output model / property: SearchTerm and set [BindProperty] attribute.
            //in the html, check asp-for, which alerts the code it is both input and output.
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
