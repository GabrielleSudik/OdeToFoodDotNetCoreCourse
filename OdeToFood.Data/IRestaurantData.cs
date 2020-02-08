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
        IEnumerable<Restaurant> GetAll();
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
        public IEnumerable<Restaurant> GetAll()
        {
            //linq statement to "search the db" and return what we say.
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}


