using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OdeToFood.Core
{
    public class Restaurant
    {
        //you created this class from scratch
        //now let's add the properties and other code
        //check out the validation check examples.
        //there are lots of choices, you can look them up.

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(60)]
        public string Location { get; set; }
        public CuisineType Cuisine { get; set; } //this is an enum class you created.

    }


    //tip: you first wrote the CuisineType enum here.
    //CTRL-dot on the name of the enum will give you a menu
    //choose "move to CuisineType.cs" to make it its own class.
    //I bet that works anytime you write more classes in one file.
}
