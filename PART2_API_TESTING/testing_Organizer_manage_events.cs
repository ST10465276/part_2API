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
    public class testing_Organizer_manage_events
    {//start of class


        //helper to build a controller with a session that has a logged-in organizer
        //so HttpContext.Session reads the user id and role
        private z_Organizer_manage_events buildControllerAsOrganizer(string user_id)
        {//start of method buildControllerAsOrganizer

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the organizer values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "organizer");

            //creating the controller
            z_Organizer_manage_events controller = new z_Organizer_manage_events();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsOrganizer


        //helper to build a controller with a session that has a logged-in user
        //so we can check the role check blocks non organizers
        private z_Organizer_manage_events buildControllerAsUser(string user_id)
        {//start of method buildControllerAsUser

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the user values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "user");

            //creating the controller
            z_Organizer_manage_events controller = new z_Organizer_manage_events();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsUser


        //testing create event when not logged in as an organizer should fail
        [Fact]
        public void create_event_not_organizer_fails()
        {//start of method create_event_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the test event name
            string eventName = "Tina Fun Run";

            //temp variable to hold the test description
            string description = "A charity 5km run";

            //temp variable to hold the test date
            DateTime date = new DateTime(2026, 1, 15);

            //temp variable to hold the test location
            string location = "Park";

            //temp variable to hold the test distance
            decimal distance = 5.00m;

            //temp variable to hold the test event type
            string eventType = "Running";

            //temp variable to hold the test age
            int age = 18;

            //temp variable to hold the test start time
            TimeSpan startTime = new TimeSpan(7, 30, 0);

            //temp variable to hold the test category id
            int category_id = 1;

            //build a controller logged in as a normal user
            z_Organizer_manage_events controller = buildControllerAsUser(user_id);

            //call the create
            string results = controller.create_event(eventName, description, date, location, distance, eventType, age, startTime, category_id);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method create_event_not_organizer_fails


        //testing create event with missing fields should fail
        [Fact]
        public void create_event_missing_fields_fails()
        {//start of method create_event_missing_fields_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_Organizer_manage_events controller = buildControllerAsOrganizer(user_id);

            //call the create with no fields
            string results = controller.create_event(null, null, null, null, null, null, null, null, null);

            //check if the results contain the required message
            Assert.Contains("All fields are required", results, StringComparison.OrdinalIgnoreCase);

        }//end of method create_event_missing_fields_fails


        //testing view all events when not logged in as an organizer should fail
        [Fact]
        public void get_all_events_not_organizer_fails()
        {//start of method get_all_events_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //build a controller logged in as a normal user
            z_Organizer_manage_events controller = buildControllerAsUser(user_id);

            //call the view
            string results = controller.get_allEvents();

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method get_all_events_not_organizer_fails


        //testing update event when not logged in as an organizer should fail
        [Fact]
        public void updates_event_not_organizer_fails()
        {//start of method updates_event_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the event id
            int event_id = 1;

            //temp variable to hold the test event name
            string eventName = "Tina Updated Run";

            //build a controller logged in as a normal user
            z_Organizer_manage_events controller = buildControllerAsUser(user_id);

            //call the update
            string results = controller.updates_event(event_id, eventName, null, null, null, null, null, null, null, null);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method updates_event_not_organizer_fails


        //testing update event with no event id should fail
        [Fact]
        public void updates_event_no_event_id_fails()
        {//start of method updates_event_no_event_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the test event name
            string eventName = "Tina Updated Run";

            //build a controller logged in as an organizer
            z_Organizer_manage_events controller = buildControllerAsOrganizer(user_id);

            //call the update with no event id
            string results = controller.updates_event(null, eventName, null, null, null, null, null, null, null, null);

            //check if the results contain the invalid event id message
            Assert.Contains("provide event id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method updates_event_no_event_id_fails


        //testing update event with nothing to update should fail
        [Fact]
        public void updates_event_nothing_to_update_fails()
        {//start of method updates_event_nothing_to_update_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the event id
            int event_id = 1;

            //build a controller logged in as an organizer
            z_Organizer_manage_events controller = buildControllerAsOrganizer(user_id);

            //call the update with no fields
            string results = controller.updates_event(event_id, null, null, null, null, null, null, null, null, null);

            //check if the results contain the required message
            Assert.Contains("At least one field is required", results, StringComparison.OrdinalIgnoreCase);

        }//end of method updates_event_nothing_to_update_fails


        //testing delete event when not logged in as an organizer should fail
        [Fact]
        public void delete_event_not_organizer_fails()
        {//start of method delete_event_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the event id
            int event_id = 1;

            //build a controller logged in as a normal user
            z_Organizer_manage_events controller = buildControllerAsUser(user_id);

            //call the delete
            string results = controller.delete_event(event_id);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method delete_event_not_organizer_fails


        //testing delete event with no event id should fail
        [Fact]
        public void delete_event_no_event_id_fails()
        {//start of method delete_event_no_event_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_Organizer_manage_events controller = buildControllerAsOrganizer(user_id);

            //call the delete with no event id
            string results = controller.delete_event(null);

            //check if the results contain the invalid event id message
            Assert.Contains("valid event id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method delete_event_no_event_id_fails


    }//end of class

}//end of namespace