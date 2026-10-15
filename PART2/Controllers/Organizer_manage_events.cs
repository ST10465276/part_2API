using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using part_2API.backend;

namespace part_2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class z_Organizer_manage_events : ControllerBase
    {


        //creating request method to create an event
        //using the HttpPost
        [HttpPost("Create new event")]
        public string create_event(string? eventName, string? description, DateTime? date, string? location, decimal? distance, string? eventType, int? age, TimeSpan? startTime, int? category_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";
            }

            //check if the required fields are empty or not
            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(description) || date == null || string.IsNullOrWhiteSpace(location) || distance == null || string.IsNullOrWhiteSpace(eventType) || age == null || startTime == null || category_id == null)
            {
                //return the error message
                return "All fields are required.....";
            }

            //creating an instance for the class manage events
            //with an object name store_event
            manage_events store_event = new manage_events();
            //get the results
            string results = store_event.store_event(eventName, description, date, location, distance, eventType, age, startTime, category_id, HttpContext);

            //then return the results
            return results;

        }//end of method create event


        //request method to see all events
        //using the Get request method
        [HttpGet("View all the events you have as an organizer")]
        public string get_allEvents()
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";
            }

            //creating an instance for the class manage events
            //with an object name get_events
            manage_events get_events = new manage_events();
            //get the results
            string results = get_events.view_events(HttpContext);

            //then return the results
            return results;
        }//end of method get all events


        //request to update the event
        //using the HttpPut endPoint
        [HttpPut("Update each event by id, first view events to get the ID AND Leave Field empty for what you dont want to UPDATE")]
        public string updates_event(int? event_id, string? eventName, string? description, DateTime? date, string? location, decimal? distance, string? eventType, int? age, TimeSpan? startTime, int? category_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";
            }

            //check if the id is empty
            if (event_id == null || event_id <= 0)
            {
                //return the error message
                return "Please before entering name or description provide event id, check it from view all events...";
            }

            //check that at least one field is provided
            if (string.IsNullOrWhiteSpace(eventName) && string.IsNullOrWhiteSpace(description) && date == null && string.IsNullOrWhiteSpace(location) && distance == null && string.IsNullOrWhiteSpace(eventType) && age == null && startTime == null && category_id == null)
            {
                //return the error message
                return "At least one field is required..........";
            }

            //creating an instance for the class manage events
            //with an object name update_event
            manage_events update_event = new manage_events();
            //get the results
            string results = update_event.updating_events(event_id, eventName, description, date, location, distance, eventType, age, startTime, category_id, user_id, HttpContext);

            //then return the results
            return results;

        }//end of update method


        //request to delete an event by id
        //using the HttpDelete endpoint
        [HttpDelete("Delete an event by its ID")]
        public string delete_event(int? event_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message
                return "Login first as an organizer.....";
            }

            //check if the event id is provided
            if (event_id == null || event_id <= 0)
            {
                //return the error message
                return "Please provide a valid event id, check it from view all events...";
            }

            //creating an instance for the class manage events
            //with an object name delete_event
            manage_events delete_event = new manage_events();

            //get the results
            string results = delete_event.deleting_events(event_id, user_id, HttpContext);

            //then return the results
            return results;

        }//end of delete method


    }//end of class

}//end of namespace