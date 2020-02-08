using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

//we're using this project to be our source of data (ie, no DB).
//basically we create a list of restaurants then call a method that will send them all to the app.
//not all apps will have something like this, it just takes the place of a real data source.

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        //IEnumerable<Restaurant> GetAll(); //this was the code for module 1
        IEnumerable<Restaurant> GetRestaurantsByName(string name); //module 2 model binding
        Restaurant GetRestaurantById(int id); //module 2 getting details by restaurant id
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        //create a constructor:
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location = "Lakewood", Cuisine = CuisineType.Italian},
                new Restaurant { Id = 2, Name = "Gino's Sushi", Location = "Cleveland", Cuisine = CuisineType.Japanese },
                new Restaurant { Id = 3, Name = "Nina's Burritos", Location = "Lakewood", Cuisine = CuisineType.Mexican },
                new Restaurant { Id = 4, Name = "Mei's Dumplings", Location = "Rocky River", Cuisine = CuisineType.Chinese }
            };
        }

        //the method to implement the interface's method:
        //you will call this method in List.cshtml.cs
        //commented out in module two while learning about model binding & form inputs
        //public IEnumerable<Restaurant> GetAll()
        //{
        //    //linq statement to "search the db" and return what we say.
        //    return from r in restaurants
        //           orderby r.Name
        //           select r;
        //}

        //for module two Model Binding:
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null) //if nothing entered, everything is returned.
        {
            //linq statement to "search the db" and return what we say.
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
        //next move to List.cshtml.cs to deal with the logic linked to the html

        //also for module two:
        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
            //that just searches the range of all restaurants,
            //looking for an Id that matches id.
        }
        //next move to Detail.cshtml.cs
    }
}


