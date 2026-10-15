using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using part_2API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testing_API_part2
{//start of namespace
    public class testing_profile_management
    {//start of class


        //helper to build a controller with a session that has a logged-in user
        //so HttpContext.Session reads the user id, name and email
        private users_profile_management buildControllerWithUser(string user_id, string user_name, string user_email)
        {//start of method buildControllerWithUser

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the user values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_name", user_name);
            context.Session.SetString("user_email", user_email);

            //creating the controller
            users_profile_management controller = new users_profile_management();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerWithUser


        //testing viewing profile info
        [Fact]
        public void view_profile_info_returns_data()
        {//start of method view_profile_info_returns_data

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the test fullname
            string user_name = "Tina";

            //temp variable to hold the test email
            string user_email = "tina@gmail.com";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, user_name, user_email);

            //get the results
            string results = controller.user_profileInfo();

            //check if the results return something
            Assert.False(string.IsNullOrWhiteSpace(results));

        }//end of method view_profile_info_returns_data


        //testing updating the fullname only
        [Fact]
        public void update_details_fullname_only()
        {//start of method update_details_fullname_only

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the old fullname
            string old_fullname = "Tina";

            //temp variable to hold the old email
            string old_email = "tina@gmail.com";

            //temp variable to hold the new fullname
            string new_fullname = "Tina Updated";

            //temp variable to hold none for the email side
            string none = "none";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, old_fullname, old_email);

            //call the update with the new fullname and none for the email
            string results = controller.update_details(new_fullname, none);

            //check if the results are not the same name message
            Assert.DoesNotContain("same as the old one", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_details_fullname_only


        //testing updating with both fields as none should fail
        [Fact]
        public void update_details_nothing_to_update()
        {//start of method update_details_nothing_to_update

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the old fullname
            string old_fullname = "Tina";

            //temp variable to hold the old email
            string old_email = "tina@gmail.com";

            //temp variable to hold none
            string none = "none";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, old_fullname, old_email);

            //call the update with none for both
            string results = controller.update_details(none, none);

            //check if the results contain the nothing to update message
            Assert.Contains("Nothing to update", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_details_nothing_to_update


        //testing updating with an invalid email should fail
        [Fact]
        public void update_details_invalid_email()
        {//start of method update_details_invalid_email

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the old fullname
            string old_fullname = "Tina";

            //temp variable to hold the old email
            string old_email = "tina@gmail.com";

            //temp variable to hold none for the fullname side
            string none = "none";

            //temp variable to hold an invalid email
            string bad_email = "tina";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, old_fullname, old_email);

            //call the update with the bad email
            string results = controller.update_details(none, bad_email);

            //check if the results contain the invalid email message
            Assert.Contains("valid email", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_details_invalid_email


        //testing updating the fullname to the same as the old one should fail
        [Fact]
        public void update_details_same_fullname_fails()
        {//start of method update_details_same_fullname_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the old fullname
            string old_fullname = "Tina";

            //temp variable to hold the old email
            string old_email = "tina@gmail.com";

            //temp variable to hold none
            string none = "none";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, old_fullname, old_email);

            //call the update with the same name
            string results = controller.update_details(old_fullname, none);

            //check if the results contain the same name message
            Assert.Contains("must not be the same as the old one", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_details_same_fullname_fails


        //testing updating the password with a weak one should fail
        [Fact]
        public void update_password_weak_fails()
        {//start of method update_password_weak_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the old fullname
            string user_name = "Tina";

            //temp variable to hold the old email
            string user_email = "tina@gmail.com";

            //temp variable to hold the old password
            string old_password = "Tina@1234";

            //temp variable to hold the weak new password
            string new_password = "weak";

            //temp variable to hold the confirm password
            string confirm_password = "weak";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, user_name, user_email);

            //call the update password with the weak password
            string results = controller.update_password(old_password, new_password, confirm_password);

            //check if the results contain the password rule message
            Assert.Contains("Password must contain", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_password_weak_fails


        //testing updating the password with a mismatch confirm should fail
        [Fact]
        public void update_password_mismatch_fails()
        {//start of method update_password_mismatch_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the old fullname
            string user_name = "Tina";

            //temp variable to hold the old email
            string user_email = "tina@gmail.com";

            //temp variable to hold the old password
            string old_password = "Tina@1234";

            //temp variable to hold a valid new password
            string new_password = "Tina@5678";

            //temp variable to hold a different confirm password
            string confirm_password = "Tina@9999";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, user_name, user_email);

            //call the update password with the mismatch
            string results = controller.update_password(old_password, new_password, confirm_password);

            //check if the results contain the mismatch message
            Assert.Contains("does not match", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_password_mismatch_fails


        //testing updating the password with empty fields should fail
        [Fact]
        public void update_password_empty_fields_fails()
        {//start of method update_password_empty_fields_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the old fullname
            string user_name = "Tina";

            //temp variable to hold the old email
            string user_email = "tina@gmail.com";

            //build a controller with the session user
            users_profile_management controller = buildControllerWithUser(user_id, user_name, user_email);

            //call the update password with empty fields
            string results = controller.update_password(null, null, null);

            //check if the results contain the required message
            Assert.Contains("required", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_password_empty_fields_fails


    }//end of class

}//end of namespace