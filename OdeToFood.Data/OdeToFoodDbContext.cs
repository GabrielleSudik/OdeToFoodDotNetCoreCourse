using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

//We created this class to define what properties we want EF to send to a DB.
//OdeToFoodDbContext is ours. It inherits from DbContext,
//which is the MC class that handles EF and data.

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        //you wrote the constructor after setting up the Db ConnectionString
        //you need it as the final step of connecting this DbContext to the EF and DB
        //basically, you inject some options (whatever those are) for DbContexts
        //into this class. then in Startup.ConfigureServices, 
        //it checks the options until it finds the ConnectionString.

        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options) : base(options)
        {
                
        }

        //the property types are not what you'd think
        //they are all DbSet<T> where T is the type you wish to save in the DB.
        //DbSet is what allows you to CRUD all of this stuff.
        //(so maybe you can define the properties and regular types if you didn't care about CRUD?)

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
