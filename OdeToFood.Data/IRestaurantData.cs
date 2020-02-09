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
        Restaurant Update(Restaurant updatedRestaurant); //module 3 updating data
        int Commit(); //module 3 we'll use this to track changes to the data
        Restaurant Add(Restaurant newRestaurant); //module 3, to add new ones.
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

        //module three - updating data:
        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            //ie, check the range of all restaurants and find the matching item.
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
                //just a simple re-naming of relevant data.
            }

            return restaurant;
        }

        public int Commit()
        {
            return 0; //for now, so intellisense doesn't complain.
        }

        //module 3. add a new restaurant to the list of them,
        //assign the next available Id to its id.
        //WARNING! the Id assignment line should ONLY be used while we supply our data here in the code.
        //a real DB has its own way of assigning Ids, we don't want to mess with that.
        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
    }
}


