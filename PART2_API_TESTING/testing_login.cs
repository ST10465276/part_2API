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
    public class testing_login
    {//start of class


        //helper to build a controller with an HttpContext and session
        //so login_users does not crash on HttpContext.Session
        private register_and_login buildControllerWithSession()
        {//start of method buildControllerWithSession

            //creating an http context
            DefaultHttpContext context = new DefaultHttpContext();

            //giving it a session
            context.Session = new test_session();

            //creating the controller
            register_and_login controller = new register_and_login();

            //assigning the context to the controller
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            //return the controller
            return controller;

        }//end of method buildControllerWithSession




        //testing a correct login should succeed
        [Fact]
        public void login_valid_user_returns_success()
        {//start of method login_valid_user_returns_success

            //temp variable to hold a unique email so tests dont collide
            string email = "test_" + Guid.NewGuid().ToString("N").Substring(0, 8) + "@gmail.com";

            //temp variable to hold the test fullname
            string fullname = "Thina";

            //temp variable to hold the test password
            string password = "Thina@1234";

            //build a controller with session
            register_and_login controller = buildControllerWithSession();

            //register Tina first so we have someone to log in
            controller.register_user(fullname, email, register_and_login.Roles.user, password);

            //then try to log in with the same credentials
            string results = controller.login_users(email, password);

            //check if the results contain success
            Assert.Contains("success", results, StringComparison.OrdinalIgnoreCase);

        }//end of method login_valid_user_returns_success


        //testing a wrong password should fail
        [Fact]
        public void login_wrong_password_returns_error()
        {//start of method login_wrong_password_returns_error

            //temp variable to hold a unique email so tests dont collide
            string email = "test_" + Guid.NewGuid().ToString("N").Substring(0, 8) + "@gmail.com";

            //temp variable to hold the test fullname
            string fullname = "Tina";

            //temp variable to hold the correct password
            string password = "Tina@1234";

            //temp variable to hold the wrong password
            string wrong_password = "Wrong@1234";

            //build a controller with session
            register_and_login controller = buildControllerWithSession();

            //register Tina first
            controller.register_user(fullname, email, register_and_login.Roles.user, password);

            //then try to log in with the wrong password
            string results = controller.login_users(email, wrong_password);

            //check if the results do not contain success
            Assert.DoesNotContain("success", results, StringComparison.OrdinalIgnoreCase);

        }//end of method login_wrong_password_returns_error


        //testing an email that does not exist should fail
        [Fact]
        public void login_unknown_email_returns_error()
        {//start of method login_unknown_email_returns_error

            //temp variable to hold an email that was never registered
            string unknown_email = "nobody@gmail.com";
            string password = "Tina@1234";

            //build a controller with session
            register_and_login controller = buildControllerWithSession();

            //try to log in with an email that was never registered
            string results = controller.login_users(unknown_email, password);

            //check if the results do not contain success
            Assert.DoesNotContain("success", results, StringComparison.OrdinalIgnoreCase);

        }//end of method login_unknown_email_returns_error


    }//end of class


    //small helper class that implements ISession so HttpContext.Session works in tests
    public class test_session : ISession
    {//start of class test_session

        //temp dictionary to hold the session values
        private readonly Dictionary<string, byte[]> store = new Dictionary<string, byte[]>();

        //the session id
        public string Id => Guid.NewGuid().ToString();

        //check if the session is available
        public bool IsAvailable => true;

        //the keys in the session
        public IEnumerable<string> Keys => store.Keys;

        //clear the session
        public void Clear()
        {
            store.Clear();
        }

        //commit does nothing here
        public Task CommitAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        //load does nothing here
        public Task LoadAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        //remove a key
        public void Remove(string key)
        {
            store.Remove(key);
        }

        //set a value
        public void Set(string key, byte[] value)
        {
            store[key] = value;
        }

        //try to get a value
        public bool TryGetValue(string key, out byte[] value)
        {
            return store.TryGetValue(key, out value);
        }

    }//end of class test_session

}//end of namespace