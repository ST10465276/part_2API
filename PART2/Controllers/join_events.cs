using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using part_2API.backend;

namespace part_2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class z_join_events : ControllerBase
    {


        //request method to view all categories so the user can pick one
        //using the HttpGet
        [HttpGet("View all categories to choose from")]
        public string view_categories_for_user()
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "user")
            {
                //return the error message 
                return "Login first as a user.....";
            }

            //creating an instance for the class manage_join_events
            //with an object name get_categories
            manage_join_events get_categories = new manage_join_events();
            //get the results
            string results = get_categories.view_all_categories(HttpContext);

            //then return the results
            return results;
        }//end of method view categories for user


        //request method to view all events inside a chosen category
        //using the HttpGet
        [HttpGet("View all events in a category by category id")]
        public string view_events_in_category(int? category_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "user")
            {
                //return the error message 
                return "Login first as a user.....";
            }

            //check if the category id is provided
            if (category_id == null || category_id <= 0)
            {
                //return the error message
                return "Please provide a valid category id, check it from view all categories...";
            }

            //creating an instance for the class manage_join_events
            //with an object name get_events
            manage_join_events get_events = new manage_join_events();
            //get the results
            string results = get_events.view_events_by_category(category_id, HttpContext);

            //then return the results
            return results;
        }//end of method view events in category


        //request method to enroll in an event
        //using the HttpPost
        [HttpPost("Enroll or become a participant in an event by event id")]
        public string enroll_in_event(int? event_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "user")
            {
                //return the error message 
                return "Login first as a user.....";
            }

            //check if the event id is provided
            if (event_id == null || event_id <= 0)
            {
                //return the error message
                return "Please provide a valid event id, check it from view all events...";
            }

            //creating an instance for the class manage_join_events
            //with an object name join_event
            manage_join_events join_event = new manage_join_events();
            //get the results
            string results = join_event.store_enrolment(event_id, HttpContext);

            //then return the results
            return results;
        }//end of method enroll in event


        //request method to view all events the user has enrolled in
        //using the HttpGet
        [HttpGet("View all events you have enrolled in")]
        public string view_my_enrolments()
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "user")
            {
                //return the error message 
                return "Login first as a user.....";
            }

            //creating an instance for the class manage_join_events
            //with an object name get_enrolments
            manage_join_events get_enrolments = new manage_join_events();
            //get the results
            string results = get_enrolments.view_my_enrolments(HttpContext);

            //then return the results
            return results;
        }//end of method view my enrolments




        //request method to view your own results
        //using the HttpGet
        [HttpGet("View your own results")]
        public string view_my_results()
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "user")
            {
                //return the error message 
                return "Login first as a user.....";
            }

            //creating an instance for the class manage_join_events
            //with an object name get_results
            manage_join_events get_results = new manage_join_events();
            //get the results
            string results = get_results.view_my_results(HttpContext);

            //then return the results
            return results;
        }//end of method view my results



        //request method to cancel an enrolment
        //using the HttpDelete
        [HttpDelete("Cancel an enrolment by enrolment id")]
        public string cancel_enrolment(int? enrolment_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "user")
            {
                //return the error message 
                return "Login first as a user.....";
            }

            //check if the enrolment id is provided
            if (enrolment_id == null || enrolment_id <= 0)
            {
                //return the error message
                return "Please provide a valid enrolment id, check it from view my enrolments...";
            }

            //creating an instance for the class manage_join_events
            //with an object name cancel
            manage_join_events cancel = new manage_join_events();
            //get the results
            string results = cancel.cancel_enrolment(enrolment_id, HttpContext);

            //then return the results
            return results;
        }//end of method cancel enrolment


    }//end of class

}//end of namespace