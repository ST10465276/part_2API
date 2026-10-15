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
    public class testing_Organizer_manage_categories_Controller
    {//start of class

        //helper to build a controller with a session that has a logged-in organizer
        //so HttpContext.Session reads the user id and role

        private z_Organizer_manage_categories buildControllerAsOrganizer(string user_id)
        {//start of method buildControllerAsOrganizer

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the organizer values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "organizer");

            //creating the controller
            z_Organizer_manage_categories controller = new z_Organizer_manage_categories();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsOrganizer


        //helper to build a controller with a session that has a logged-in user
        //so we can check the role check blocks non organizers
        private z_Organizer_manage_categories buildControllerAsUser(string user_id)
        {//start of method buildControllerAsUser

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //storing the user values in the session
            context.Session.SetString("user_id", user_id);
            context.Session.SetString("user_role", "user");

            //creating the controller
            z_Organizer_manage_categories controller = new z_Organizer_manage_categories();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerAsUser


        //testing create category when not logged in as an organizer should fail
        [Fact]
        public void create_category_not_organizer_fails()
        {//start of method create_category_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the test category name
            string name = "Tina Runs";

            //temp variable to hold the test category description
            string description = "All running events by Tina";

            //build a controller logged in as a normal user
            z_Organizer_manage_categories controller = buildControllerAsUser(user_id);

            //call the create
            string results = controller.create_category(name, description);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method create_category_not_organizer_fails


        //testing create category with missing fields should fail
        [Fact]
        public void create_category_missing_fields_fails()
        {//start of method create_category_missing_fields_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_Organizer_manage_categories controller = buildControllerAsOrganizer(user_id);

            //call the create with no fields
            string results = controller.create_category(null, null);

            //check if the results contain the required message
            Assert.Contains("Both fields are required", results, StringComparison.OrdinalIgnoreCase);

        }//end of method create_category_missing_fields_fails


        //testing view all categories when not logged in as an organizer should fail
        [Fact]
        public void get_all_categories_not_organizer_fails()
        {//start of method get_all_categories_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //build a controller logged in as a normal user
            z_Organizer_manage_categories controller = buildControllerAsUser(user_id);

            //call the view
            string results = controller.get_allCategories();

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method get_all_categories_not_organizer_fails


        //testing update category when not logged in as an organizer should fail
        [Fact]
        public void updates_category_not_organizer_fails()
        {//start of method updates_category_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the category id
            int category_id = 1;

            //temp variable to hold the test category name
            string name = "Tina Updated Runs";

            //build a controller logged in as a normal user
            z_Organizer_manage_categories controller = buildControllerAsUser(user_id);

            //call the update
            string results = controller.updates_category(category_id, name, null);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method updates_category_not_organizer_fails


        //testing update category with no category id should fail
        [Fact]
        public void updates_category_no_category_id_fails()
        {//start of method updates_category_no_category_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the test category name
            string name = "Tina Updated Runs";

            //build a controller logged in as an organizer
            z_Organizer_manage_categories controller = buildControllerAsOrganizer(user_id);

            //call the update with no category id
            string results = controller.updates_category(null, name, null);

            //check if the results contain the provide id message
            Assert.Contains("provide category id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method updates_category_no_category_id_fails


        //testing update category with nothing to update should fail
        [Fact]
        public void updates_category_nothing_to_update_fails()
        {//start of method updates_category_nothing_to_update_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //temp variable to hold the category id
            int category_id = 1;

            //build a controller logged in as an organizer
            z_Organizer_manage_categories controller = buildControllerAsOrganizer(user_id);

            //call the update with no fields
            string results = controller.updates_category(category_id, null, null);

            //check if the results contain the required message
            Assert.Contains("One of the Field is required", results, StringComparison.OrdinalIgnoreCase);

        }//end of method updates_category_nothing_to_update_fails


        //testing delete category when not logged in as an organizer should fail
        [Fact]
        public void delete_category_not_organizer_fails()
        {//start of method delete_category_not_organizer_fails

            //temp variable to hold the test user id
            string user_id = "1";

            //temp variable to hold the category id
            int category_id = 1;

            //build a controller logged in as a normal user
            z_Organizer_manage_categories controller = buildControllerAsUser(user_id);

            //call the delete
            string results = controller.delete_category(category_id);

            //check if the results contain the login message
            Assert.Contains("Login first as an organizer", results, StringComparison.OrdinalIgnoreCase);

        }//end of method delete_category_not_organizer_fails


        //testing delete category with no category id should fail
        [Fact]
        public void delete_category_no_category_id_fails()
        {//start of method delete_category_no_category_id_fails

            //temp variable to hold the test organizer id
            string user_id = "1";

            //build a controller logged in as an organizer
            z_Organizer_manage_categories controller = buildControllerAsOrganizer(user_id);

            //call the delete with no category id
            string results = controller.delete_category(null);

            //check if the results contain the invalid category id message
            Assert.Contains("valid category id", results, StringComparison.OrdinalIgnoreCase);

        }//end of method delete_category_no_category_id_fails


    }//end of class

}//end of namespace