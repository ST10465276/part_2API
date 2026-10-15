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
    public class testing_join_events
    {//start of class

        //helper to build a controller with a session that has a logged-in user
        //so HttpContext.Session reads the user id and role
        private z_join_events buildControllerAsUser(string user_id)
        {//start of method buildControllerAsUser

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the user values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "user");

            //creating the controller
            z_join_events controller = new z_join_events();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsUser


        //helper to build a controller with a session that has a logged-in organizer
        //so we can check the role check blocks non users
        private z_join_events buildControllerAsOrganizer(string user_id)
        {//start of method buildControllerAsOrganizer

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the organizer values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "organizer");

            //creating the controller
            z_join_events controller = new z_join_events();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsOrganizer


        //testing view categories when not logged in as a user should fail
        [Fact]
        public void view_categories_not_user_fails()
        {//start of method view_categories_not_user_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_join_events controller = buildControllerAsOrganizer(user_id);

            //call the view
            string results = controller.view_categories_for_user();

            //check if the results contain the login message
            Assert.Contains("Login first as a user", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_categories_not_user_fails


        //testing view events in a category when not logged in as a user should fail
        [Fact]
        public void view_events_in_category_not_user_fails()
        {//start of method view_events_in_category_not_user_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the category id
            int category_id = 1;

            //build a controller logged in as an organizer
            z_join_events controller = buildControllerAsOrganizer(user_id);

            //call the view
            string results = controller.view_events_in_category(category_id);

            //check if the results contain the login message
            Assert.Contains("Login first as a user", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_events_in_category_not_user_fails


        //testing view events in a category with no category id should fail
        [Fact]
        public void view_events_in_category_no_category_id_fails()
        {//start of method view_events_in_category_no_category_id_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //build a controller logged in as a user
            z_join_events controller = buildControllerAsUser(user_id);

            //call the view with no category id
            string results = controller.view_events_in_category(null);

            //check if the results contain the invalid category id message
            Assert.Contains("valid category id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_events_in_category_no_category_id_fails


        //testing view events in a category with a bad category id should fail
        [Fact]
        public void view_events_in_category_bad_category_id_fails()
        {//start of method view_events_in_category_bad_category_id_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold a bad category id
            int bad_category_id = 0;

            //build a controller logged in as a user
            z_join_events controller = buildControllerAsUser(user_id);

            //call the view with a bad category id
            string results = controller.view_events_in_category(bad_category_id);

            //check if the results contain the invalid category id message
            Assert.Contains("valid category id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_events_in_category_bad_category_id_fails


        //testing enroll in event when not logged in as a user should fail
        [Fact]
        public void enroll_in_event_not_user_fails()
        {//start of method enroll_in_event_not_user_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the event id
            int event_id = 1;

            //build a controller logged in as an organizer
            z_join_events controller = buildControllerAsOrganizer(user_id);

            //call the enroll
            string results = controller.enroll_in_event(event_id);

            //check if the results contain the login message
            Assert.Contains("Login first as a user", results, StringComparison.OrdinalIgnoreCase);

        }//end of method enroll_in_event_not_user_fails


        //testing enroll in event with no event id should fail
        [Fact]
        public void enroll_in_event_no_event_id_fails()
        {//start of method enroll_in_event_no_event_id_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //build a controller logged in as a user
            z_join_events controller = buildControllerAsUser(user_id);

            //call the enroll with no event id
            string results = controller.enroll_in_event(null);

            //check if the results contain the invalid event id message
            Assert.Contains("valid event id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method enroll_in_event_no_event_id_fails


        //testing enroll in event with a bad event id should fail
        [Fact]
        public void enroll_in_event_bad_event_id_fails()
        {//start of method enroll_in_event_bad_event_id_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold a bad event id
            int bad_event_id = 0;

            //build a controller logged in as a user
            z_join_events controller = buildControllerAsUser(user_id);

            //call the enroll with a bad event id
            string results = controller.enroll_in_event(bad_event_id);

            //check if the results contain the invalid event id message
            Assert.Contains("valid event id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method enroll_in_event_bad_event_id_fails


        //testing view my enrolments when not logged in as a user should fail
        [Fact]
        public void view_my_enrolments_not_user_fails()
        {//start of method view_my_enrolments_not_user_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_join_events controller = buildControllerAsOrganizer(user_id);

            //call the view
            string results = controller.view_my_enrolments();

            //check if the results contain the login message
            Assert.Contains("Login first as a user", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_my_enrolments_not_user_fails


        //testing view my results when not logged in as a user should fail
        [Fact]
        public void view_my_results_not_user_fails()
        {//start of method view_my_results_not_user_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_join_events controller = buildControllerAsOrganizer(user_id);

            //call the view
            string results = controller.view_my_results();

            //check if the results contain the login message
            Assert.Contains("Login first as a user", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_my_results_not_user_fails


        //testing cancel enrolment when not logged in as a user should fail
        [Fact]
        public void cancel_enrolment_not_user_fails()
        {//start of method cancel_enrolment_not_user_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the enrolment id
            int enrolment_id = 1;

            //build a controller logged in as an organizer
            z_join_events controller = buildControllerAsOrganizer(user_id);

            //call the cancel
            string results = controller.cancel_enrolment(enrolment_id);

            //check if the results contain the login message
            Assert.Contains("Login first as a user", results, StringComparison.OrdinalIgnoreCase);

        }//end of method cancel_enrolment_not_user_fails


        //testing cancel enrolment with no enrolment id should fail
        [Fact]
        public void cancel_enrolment_no_enrolment_id_fails()
        {//start of method cancel_enrolment_no_enrolment_id_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //build a controller logged in as a user
            z_join_events controller = buildControllerAsUser(user_id);

            //call the cancel with no enrolment id
            string results = controller.cancel_enrolment(null);

            //check if the results contain the invalid enrolment id message
            Assert.Contains("valid enrolment id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method cancel_enrolment_no_enrolment_id_fails


        //testing cancel enrolment with a bad enrolment id should fail
        [Fact]
        public void cancel_enrolment_bad_enrolment_id_fails()
        {//start of method cancel_enrolment_bad_enrolment_id_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold a bad enrolment id
            int bad_enrolment_id = 0;

            //build a controller logged in as a user
            z_join_events controller = buildControllerAsUser(user_id);

            //call the cancel with a bad enrolment id
            string results = controller.cancel_enrolment(bad_enrolment_id);

            //check if the results contain the invalid enrolment id message
            Assert.Contains("valid enrolment id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method cancel_enrolment_bad_enrolment_id_fails


    }//end of class

}//end of namespace