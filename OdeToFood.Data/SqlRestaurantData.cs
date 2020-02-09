using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

//Tip: When you create a new implementing class for an interface
//you can save a lot of typing by ctrl-dot on the name of the interface
//select "implement interface" and you'll get the bare bones of each method.
//Then just replace with your logic.

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        //don't forget to pass your DbContext model to the constructor
        //so it gets injected here, and not where your logic needs it.
        private readonly OdeToFoodDbContext db;
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant); //THIS Add is a method built into DbContext class.
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges(); //SaveChanges is a method built into DbContext class.
            //your adds, edits and deletes don't actually happen until Commit() is called.
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant); //Remove is a method built into DbContext class.
            }
            return restaurant;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return db.Restaurants.Find(id); //not sure why "db.Find" doesn't work without Restaurants.
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            //attach is like "find an object that already exists and get ready to do something with it"
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //and I guess you just have to know about EntityState stuff. /shrug
            return updatedRestaurant;
        }
    }
}


