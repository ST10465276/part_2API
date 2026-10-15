using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using part_2API.Controllers;

namespace testing_API_part2
{//start of namespace
    public class testing_register
    {//start of class


        //testing a correct register should succeed
        [Fact]
        public void register_valid_user_returns_success()
        {//start of method register_valid_user_returns_success

            //temp variable to hold a unique email so tests dont collide
            string email = "test_" + Guid.NewGuid().ToString("N").Substring(0, 8) + "@gmail.com";

            //creating an instance for the controller register_and_login
            //with an object name controller
            register_and_login controller = new register_and_login();

            //temp variable to hold the results
            string results = controller.register_user("Tina", email, register_and_login.Roles.user, "Tina@1234");

            //check if the results contain success
            Assert.Contains("success", results, StringComparison.OrdinalIgnoreCase);

        }//end of method register_valid_user_returns_success


        //testing a wrong email should fail
        [Fact]
        public void register_invalid_email_returns_error()
        {//start of method register_invalid_email_returns_error

            //creating an instance for the controller register_and_login
            //with an object name controller
            register_and_login controller = new register_and_login();

            //temp variable to hold the results
            string results = controller.register_user("Tina", "tina", register_and_login.Roles.user, "Tina@1234");

            //check if the results contain the invalid email message
            Assert.Contains("valid email", results, StringComparison.OrdinalIgnoreCase);

        }//end of method register_invalid_email_returns_error


        //testing a weak password should fail
        [Fact]
        public void register_weak_password_returns_error()
        {//start of method register_weak_password_returns_error

            //creating an instance for the controller register_and_login
            //with an object name controller
            register_and_login controller = new register_and_login();

            //temp variable to hold the results
            string results = controller.register_user("Tina", "tina@gmail.com", register_and_login.Roles.user, "weak");

            //check if the results contain the password message
            Assert.Contains("Password must contain", results, StringComparison.OrdinalIgnoreCase);

        }//end of method register_weak_password_returns_error


        //testing a duplicate email should not register
        [Fact]
        public void register_duplicate_email_returns_error()
        {//start of method register_duplicate_email_returns_error

            //temp variable to hold a unique email so tests dont collide
            string email = "test_" + Guid.NewGuid().ToString("N").Substring(0, 8) + "@gmail.com";

            //creating an instance for the controller register_and_login
            //with an object name controller
            register_and_login controller = new register_and_login();

            //first register should succeed
            string first = controller.register_user("Tina", email, register_and_login.Roles.user, "Tina@1234");
            //check if the first register succeeded
            Assert.Contains("success", first, StringComparison.OrdinalIgnoreCase);

            //second register with the same email should not
            string second = controller.register_user("Tina Two", email, register_and_login.Roles.user, "Tina@1234");
            //check if the second register failed
            Assert.DoesNotContain("success", second, StringComparison.OrdinalIgnoreCase);

        }//end of method register_duplicate_email_returns_error


    }//end of class

}//end of namespace