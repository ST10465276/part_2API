using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using part_2API.backend;
using System.Text.RegularExpressions;


namespace part_2API.Controllers
{//start of namespace
    [Route("api/[controller]")]
    [ApiController]
    public class register_and_login : ControllerBase
    {//start of class

        //creating list of users type options , using enum
        public enum Roles {

            user, organizer

        }

        //creating a register Request EndPoint method using
        //HttpPost , to test on storing users info
        //which is fullname , email , role and password
        [HttpPost("{register_fullname}&{register_email}&{role}&{register_phoneNumber}&{register_password}")]
        public string register_user(string register_fullname, string register_email, Roles role, string register_password)
        {//start of method register

            //check for the fullname if valid or not
            if (!Regex.IsMatch(register_fullname, @"^\p{Lu}\p{L}+([ '\-]\p{L}+)*$")) 
            {
                //return the error message of invlid fullname
                return "Please enter valid fullname...";
            
            }

            //check for the email if valid or not
            if (!Regex.IsMatch(register_email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")  )
            {
                //return the error message of invalid email
                return "Please enter valid email address...";
            }

            //check if the password contains a capital letter
            bool hasCapital = Regex.IsMatch(register_password, @"[A-Z]");

            //check if the password contains a small letter
            bool hasSmall = Regex.IsMatch(register_password, @"[a-z]");

            //check if the password contains a number
            bool hasNumber = Regex.IsMatch(register_password, @"[0-9]");

            //check if the password contains a special character
            bool hasSpecial = Regex.IsMatch(
                register_password, @"[^a-zA-Z0-9\s]");

            //check if the password has at least 8 characters
            bool hasLength = register_password.Length >= 8;

            //validate the password
            if (!hasCapital || !hasSmall || !hasNumber ||!hasSpecial || !hasLength)
            {
                return "Password must contain at least 8 characters, " +
                       "one capital letter, one small letter, " +
                       "one number and one special character.";
            }

            //creating an instance for the class register_backend
            //with an object name store
            register_backend store = new register_backend();

            //temp variable to hold the results
            string results = store.store_user(register_fullname, register_email, role.ToString(), register_password);


            //return the results
            return results;




        }//end of method register


        //Creating a login Request method to allow the users to login and manage their details
        //using the HttpGet request method, by Email And Password
        [HttpGet("{Login_email}&{Login_password}")]
        public string login_users(string Login_email , string Login_password  )
        {//start of login user's request method

            //creating an instanc for the class login_backend
            //with an object name login_user
            login_backend login_user = new login_backend();

            //then collect the results
            string results = login_user.login( Login_email , Login_password , HttpContext );

            //return the results
            return results;


        }//end of the login users request method










    }//end of class


}//end of namespace
