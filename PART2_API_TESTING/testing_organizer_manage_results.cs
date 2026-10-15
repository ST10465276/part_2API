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
    public class testing_organizer_manage_results
    {//start of class


        //helper to build a controller with a session that has a logged-in organizer
        //so HttpContext.Session reads the user id and role
        private z_Organizer_manage_results buildControllerAsOrganizer(string user_id)
        {//start of method buildControllerAsOrganizer

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the organizer values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "organizer");

            //creating the controller
            z_Organizer_manage_results controller = new z_Organizer_manage_results();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsOrganizer


        //helper to build a controller with a session that has a logged-in user
        //so we can check the role check blocks non organizers
        private z_Organizer_manage_results buildControllerAsUser(string user_id)
        {//start of method buildControllerAsUser

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the user values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "user");

            //creating the controller
            z_Organizer_manage_results controller = new z_Organizer_manage_results();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsUser


        //testing view enrolments when not logged in as an organizer should fail
        [Fact]
        public void view_enrolments_not_organizer_fails()
        {//start of method view_enrolments_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the event id
            int event_id = 1;

            //build a controller logged in as a normal user
            z_Organizer_manage_results controller = buildControllerAsUser(user_id);

            //call the view
            string results = controller.view_enrolments_for_event(event_id);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_enrolments_not_organizer_fails


        //testing view enrolments with no event id should fail
        [Fact]
        public void view_enrolments_no_event_id_fails()
        {//start of method view_enrolments_no_event_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_Organizer_manage_results controller = buildControllerAsOrganizer(user_id);

            //call the view with no event id
            string results = controller.view_enrolments_for_event(null);

            //check if the results contain the invalid event id message
            Assert.Contains("valid event id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_enrolments_no_event_id_fails


        //testing view enrolments with a bad event id should fail
        [Fact]
        public void view_enrolments_bad_event_id_fails()
        {//start of method view_enrolments_bad_event_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold a bad event id
            int bad_event_id = 0;

            //build a controller logged in as an organizer
            z_Organizer_manage_results controller = buildControllerAsOrganizer(user_id);

            //call the view with a bad event id
            string results = controller.view_enrolments_for_event(bad_event_id);

            //check if the results contain the invalid event id message
            Assert.Contains("valid event id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method view_enrolments_bad_event_id_fails


        //testing record result when not logged in as an organizer should fail
        [Fact]
        public void record_result_not_organizer_fails()
        {//start of method record_result_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the enrolment id
            int enrolment_id = 1;

            //temp variable to hold the finish time
            TimeSpan finish_time = new TimeSpan(1, 23, 45);

            //temp variable to hold the finish position
            int finish_position = 1;

            //build a controller logged in as a normal user
            z_Organizer_manage_results controller = buildControllerAsUser(user_id);

            //call the record
            string results = controller.record_result(enrolment_id, finish_time, finish_position);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method record_result_not_organizer_fails


        //testing record result with no enrolment id should fail
        [Fact]
        public void record_result_no_enrolment_id_fails()
        {//start of method record_result_no_enrolment_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the finish time
            TimeSpan finish_time = new TimeSpan(1, 23, 45);

            //temp variable to hold the finish position
            int finish_position = 1;

            //build a controller logged in as an organizer
            z_Organizer_manage_results controller = buildControllerAsOrganizer(user_id);

            //call the record with no enrolment id
            string results = controller.record_result(null, finish_time, finish_position);

            //check if the results contain the invalid enrolment id message
            Assert.Contains("valid enrolment id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method record_result_no_enrolment_id_fails


        //testing record result with no finish time or position should fail
        [Fact]
        public void record_result_missing_fields_fails()
        {//start of method record_result_missing_fields_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the enrolment id
            int enrolment_id = 1;

            //build a controller logged in as an organizer
            z_Organizer_manage_results controller = buildControllerAsOrganizer(user_id);

            //call the record with no finish time or position
            string results = controller.record_result(enrolment_id, null, null);

            //check if the results contain the required message
            Assert.Contains("required", results, StringComparison.OrdinalIgnoreCase);

        }//end of method record_result_missing_fields_fails


        //testing update result when not logged in as an organizer should fail
        [Fact]
        public void update_result_not_organizer_fails()
        {//start of method update_result_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the result id
            int result_id = 1;

            //temp variable to hold the finish time
            TimeSpan finish_time = new TimeSpan(1, 23, 45);

            //temp variable to hold the finish position
            int finish_position = 1;

            //build a controller logged in as a normal user
            z_Organizer_manage_results controller = buildControllerAsUser(user_id);

            //call the update
            string results = controller.update_result(result_id, finish_time, finish_position);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_result_not_organizer_fails


        //testing update result with no result id should fail
        [Fact]
        public void update_result_no_result_id_fails()
        {//start of method update_result_no_result_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the finish time
            TimeSpan finish_time = new TimeSpan(1, 23, 45);

            //temp variable to hold the finish position
            int finish_position = 1;

            //build a controller logged in as an organizer
            z_Organizer_manage_results controller = buildControllerAsOrganizer(user_id);

            //call the update with no result id
            string results = controller.update_result(null, finish_time, finish_position);

            //check if the results contain the invalid result id message
            Assert.Contains("valid result id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_result_no_result_id_fails


        //testing update result with nothing to update should fail
        [Fact]
        public void update_result_nothing_to_update_fails()
        {//start of method update_result_nothing_to_update_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the result id
            int result_id = 1;

            //build a controller logged in as an organizer
            z_Organizer_manage_results controller = buildControllerAsOrganizer(user_id);

            //call the update with no fields
            string results = controller.update_result(result_id, null, null);

            //check if the results contain the required message
            Assert.Contains("At least one field is required", results, StringComparison.OrdinalIgnoreCase);

        }//end of method update_result_nothing_to_update_fails


        //testing delete result when not logged in as an organizer should fail
        [Fact]
        public void delete_result_not_organizer_fails()
        {//start of method delete_result_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the result id
            int result_id = 1;

            //build a controller logged in as a normal user
            z_Organizer_manage_results controller = buildControllerAsUser(user_id);

            //call the delete
            string results = controller.delete_result(result_id);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method delete_result_not_organizer_fails


        //testing delete result with no result id should fail
        [Fact]
        public void delete_result_no_result_id_fails()
        {//start of method delete_result_no_result_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_Organizer_manage_results controller = buildControllerAsOrganizer(user_id);

            //call the delete with no result id
            string results = controller.delete_result(null);

            //check if the results contain the invalid result id message
            Assert.Contains("valid result id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method delete_result_no_result_id_fails


    }//end of class

}//end of namespace