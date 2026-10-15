using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using part_2API.backend;


namespace part_2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class z_Organizer_manage_categories : ControllerBase
    {

        //creating request method to create categories
        //using the HttpPost
        [HttpPost("Create new categories by name and descriptions")]
        public string create_category(string? name, string? description)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";

            }


            //check if empty or not
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {

                //return the error message
                return "Both fields are required.....";
            }


            //creating an instance for the class manage categories
            //with an object name store categories
            manage_categories store_categories = new manage_categories();
            //get the resulsts
            string results = store_categories.store_category(name, description, HttpContext);



            //then return the results
            return results;
        }//end of method create category



        //request method to see all categories
        //using the Get request method
        [HttpGet("View all the categories you have as an organizer")]
        public string get_allCategories()
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString()!= "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";

            }

            //creating an instance for the class manage categories
            //with an onject name get categories
            manage_categories get_categories = new manage_categories();
            //get the results
            string results = get_categories.view_categories(HttpContext);

            //then return the results
            return results;
        }//end of method get all categories


        //request to update the categories
        //using the HttpPut endPoint
        [HttpPut("Update each category by id, first view categories to get the ID AND Leave Field empty for what you dont want to UPDATE")]
        public string updates_category(int? category_id , string? name , string? description )
        {

            //temp variables
            string choice = "both";

            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";

            }


            //check if the id is empty
            string for_categoryID = "" + category_id;
            if (string.IsNullOrWhiteSpace(for_categoryID))
            {
                //return the error message
                return "Please before entering name or description provide category id, check it from view all categories...";

            }


            //check for the name and description
            if (string.IsNullOrWhiteSpace(name)   &&  string.IsNullOrWhiteSpace(description)  )
            {
                //return the error message
                return "One of the Field is required..........";
            }



            //check each
            if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description) )
            {
                choice = "name";

            }//end of if for name

            if (!string.IsNullOrWhiteSpace(description)  && string.IsNullOrWhiteSpace(name) )
            {
                choice = "description";
            }


            //creating an instance  for the class manage categories
            //with an object name update_category
            manage_categories update_category = new manage_categories();
            //get the results
            string results = update_category.updating_categories(category_id , name , description , user_id,choice , HttpContext );

            //then return the results
            return results;


        }//end of update method


        //request to delete a category by id
        //using the HttpDelete endpoint
        [HttpDelete("Delete a category by its ID")]
        public string delete_category(int? category_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message
                return "Login first as an organizer.....";
            }

            //check if the category id is provided
            if (category_id == null || category_id <= 0)
            {
                //return the error message
                return "Please provide a valid category id, check it from view all categories...";
            }

            //creating an instance for the class manage categories
            //with an object name delete_category
            manage_categories delete_category = new manage_categories();

            //get the results - note: this assumes your manage_categories class
            //has a deleting_categories method that takes category_id and user_id
            string results = delete_category.deleting_categories(category_id, user_id, HttpContext);

            //then return the results
            return results;

        }//end of delete method





    }//end of class


    }//end of namespace
