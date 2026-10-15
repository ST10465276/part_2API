using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using part_2API.backend;

namespace part_2API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class z_Organizer_manage_results : ControllerBase
    {


        //request method to view all enrolments for one of the organizer's events
        //using the HttpGet
        [HttpGet("View all enrolments in an event you own")]
        public string view_enrolments_for_event(int? event_id)
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

            //creating an instance for the class manage_results
            //with an object name get_enrolments
            manage_results get_enrolments = new manage_results();
            //get the results
            string results = get_enrolments.view_enrolments_by_event(event_id, user_id, HttpContext);

            //then return the results
            return results;
        }//end of method view enrolments for event


        //request method to record a result for an enrolment
        //using the HttpPost
        [HttpPost("Record a result by enrolment id")]
        public string record_result(int? enrolment_id, TimeSpan? finishTime, int? finishPosition)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";
            }

            //check if the enrolment id is provided
            if (enrolment_id == null || enrolment_id <= 0)
            {
                //return the error message
                return "Please provide a valid enrolment id, check it from view enrolments...";
            }

            //check that finish time and position are provided
            if (finishTime == null || finishPosition == null)
            {
                //return the error message
                return "Both finish time and finish position are required.....";
            }

            //creating an instance for the class manage_results
            //with an object name store_result
            manage_results store_result = new manage_results();
            //get the results
            string results = store_result.store_result(enrolment_id, finishTime, finishPosition, user_id, HttpContext);

            //then return the results
            return results;
        }//end of method record result


        //request method to update a result
        //using the HttpPut
        [HttpPut("Update a result by result id, leave fields empty for what you dont want to UPDATE")]
        public string update_result(int? result_id, TimeSpan? finishTime, int? finishPosition)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";
            }

            //check if the result id is provided
            if (result_id == null || result_id <= 0)
            {
                //return the error message
                return "Please provide a valid result id...";
            }

            //check that at least one field is provided
            if (finishTime == null && finishPosition == null)
            {
                //return the error message
                return "At least one field is required..........";
            }

            //creating an instance for the class manage_results
            //with an object name update_result
            manage_results update_result = new manage_results();
            //get the results
            string results = update_result.updating_results(result_id, finishTime, finishPosition, user_id, HttpContext);

            //then return the results
            return results;
        }//end of method update result


        //request method to delete a result
        //using the HttpDelete
        [HttpDelete("Delete a result by result id")]
        public string delete_result(int? result_id)
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string? role = HttpContext.Session.GetString("user_role");
            if (user_id == null || role?.ToLower().ToString() != "organizer")
            {
                //return the error message 
                return "Login first as an organizer.....";
            }

            //check if the result id is provided
            if (result_id == null || result_id <= 0)
            {
                //return the error message
                return "Please provide a valid result id...";
            }

            //creating an instance for the class manage_results
            //with an object name delete_result
            manage_results delete_result = new manage_results();
            //get the results
            string results = delete_result.deleting_results(result_id, user_id, HttpContext);

            //then return the results
            return results;
        }//end of method delete result


    }//end of class

}//end of namespace