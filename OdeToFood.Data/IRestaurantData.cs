using OdeToFood.Core;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name); 
        Restaurant GetRestaurantById(int id); 
        Restaurant Update(Restaurant updatedRestaurant); 
        int Commit(); 
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Delete(int id);
    }
}


