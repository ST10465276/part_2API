using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using part_2API.backend;
using System.Text.RegularExpressions;
namespace part_2API.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class users_profile_management : ControllerBase
    {//start of class


        //creating a request method to display the user's profile info
        //using the HttpGet request method
        [HttpGet("View Your Profile Info")]
        public string user_profileInfo()
        {//start of method user profile info

            //creating an instance for the class profile info
            //with an object name user_data
            profile_info user_data = new profile_info();

            //get the results
            string results = user_data.get_profile_info(HttpContext);

            //then return the results or user's data
            return results;


        }//end of method user profile info


        //creating Put request method to update the user's fullname and email
        [HttpPut("Update Details Fullname and Email ( Remove none to be able to update a field.. )")]
        public string update_details(string update_fullname = "none", string update_email = "none")
        {
            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");
            string choice = "both";

            if (user_id == null)
            {
                //return the error message 
                return "Login first.....";

            }

            //check both
            if (update_fullname.ToLower().Equals("none") && update_email.ToLower().Equals("none"))
            {
                return "Nothing to update, please remove none and enter fullname or email, and make sure to not leave them empty\nnone means you dont want to update it...";

            }

            if (update_email == "" || update_fullname == "")
            {
                return "Nothing to update, please remove none and enter fullname or email, and make sure to not leave them empty\nnone means you dont want to update it...";

            }
            //check for the email if valid or not
            if (!Regex.IsMatch(update_email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") && update_email != "none")
            {

                //return the error message of invalid email
                return "Please enter valid email address...";

            }






            //if the user is active
            string? old_fullname = HttpContext.Session.GetString("user_name");
            string? old_email = HttpContext.Session.GetString("user_email");

            //then compare with the new one
            if (update_fullname.ToLower().Equals(old_fullname?.ToLower().ToString()))
            {

                //return the error message
                return "Please enter a new name, must not be the same as the old one " + old_fullname;


            }//end of if for old name


            //check both to know what the user is updating
            if (!update_fullname.ToLower().Equals("none") && update_email.ToLower().Equals("none"))
            {
                choice = "fullname";

            } else if (!update_email.ToLower().Equals("none") && update_fullname.ToLower().Equals("none"))
            {


                choice = "email";
            }


            //check for email
            if (update_email.ToLower().Equals(old_email?.ToLower().ToString()))
            {
                //return the error message
                return "Please the new email must be differ from the old one " + old_email;
            }




            //creating an instance for the class profile info
            profile_info update_details = new profile_info();

            //call the update method using the object name
            string results = update_details.update_info(update_fullname, update_email, choice, HttpContext);


            //return the results
            return results;
        }


        //request method to update the password
        //using put
        [HttpPut("Update Password from old one to new one")]
        public string update_password(string? old_password, string? new_password, string? confirm_password)
        {

            //check if the user is active or not
            string? user_id = HttpContext.Session.GetString("user_id");

            if (user_id == null)
            {
                //return the error message 
                return "Login first.....";

            }



            //validate the password
            if (string.IsNullOrEmpty(  old_password) || string.IsNullOrEmpty(new_password) || string.IsNullOrEmpty(confirm_password))
            {
                //return the error message
                return "All password fields are required....";

            }

            //check if the password contains a capital letter
            bool hasCapital = Regex.IsMatch(new_password, @"[A-Z]");

            //check if the password contains a small letter
            bool hasSmall = Regex.IsMatch(new_password, @"[a-z]");

            //check if the password contains a number
            bool hasNumber = Regex.IsMatch(new_password, @"[0-9]");

            //check if the password contains a special character
            bool hasSpecial = Regex.IsMatch(
                new_password, @"[^a-zA-Z0-9\s]");

            //check if the password has at least 8 characters
            bool hasLength = new_password.Length >= 8;

            //validate the password
            if (!hasCapital || !hasSmall || !hasNumber || !hasSpecial || !hasLength)
            {
                return "Password must contain at least 8 characters, " +
                       "one capital letter, one small letter, " +
                       "one number and one special character.";
            }


            //then confirm 
            if (!new_password.Equals(confirm_password))
            {
                //error message
                return "Password does not match, the new one and the confirm one....";

            }



            //creating an instance for the class profile info
            //with an object name update_password
            profile_info update_password = new profile_info();

            //call the update password method
            string reuslts = update_password.update_password(old_password,new_password,HttpContext);
            //then return results
            return reuslts;

         }



    
    }//end of class



}//end of namespace
