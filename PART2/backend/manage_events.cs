using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace part_2API.backend
{
    public class manage_events
    {


        //method for storing/inserting a new event
        public string store_event(string eventName, string description, DateTime? date, string location, decimal? distance, string eventType, int? age, TimeSpan? startTime, int? category_id, HttpContext get_session)
        {
            //temp variables
            string message = "Failed to create event !!!!!!!!";
            string connection_string = @"Data source=(localdb)\part_2_api;database=part_2;";

            //try and catch
            try
            {
                //get the organizer id from the session
                string? organizer_id = get_session.Session.GetString("user_id");
                int owner_id = Int32.Parse(organizer_id);

                //query to insert a new event
                string dateFormated = ""+date?.ToString("yyyy-MM-dd");
                string starterTime = "" + startTime?.ToString(@"hh\:mm\:ss");
                string query = @"insert into Events (eventName, description, date, location, distance, eventType, age, startTime, user_id, category_id) values ('" + eventName + "', '" + description + "', '" + dateFormated+ "', '" + location + "', " + distance + ", '" + eventType + "', " + age + ", '" + starterTime + "', " + owner_id + ", " + category_id + ");";

                //connecting to the server and database
                SqlConnection connection = new SqlConnection(connection_string);
                //open the connection
                connection.Open();

                //Execute the command
                SqlCommand run_query = new SqlCommand(query, connection);
                //then execute
                int affected_rows = run_query.ExecuteNonQuery();

                if (affected_rows == 0)
                {
                    message = "Failed to create event !!!!!!!!";
                }
                else
                {
                    message = "Event is successfully created..";
                }

                //close the connection
                connection.Close();

            }
            catch (Exception error)
            {
                //assign the error message from catch
                message = error.Message;
            }//end of catch

            //then return
            return message;

        }//end of store_event method




        //method for viewing all events that belong to this organizer
        public string view_events(HttpContext get_session)
        {
            //temp variables
            string message = "Failed to load events !!!!!!!!";
            string connection_string = @"Data source=(localdb)\part_2_api;database=part_2;";
            bool data_found = false;
            int count_events = 0;

            //try and catch
            try
            {
                //get the organizer id from the session
                string? organizer_id = get_session.Session.GetString("user_id");
                int owner_id = Int32.Parse(organizer_id);

                //query to select only this organizer's events
                string query = @"select eventID, eventName, description, date, location, distance, eventType, age, startTime, category_id, user_id from Events where user_id=" + owner_id + ";";

                //connecting to the server and database
                SqlConnection connection = new SqlConnection(connection_string);
                //open the connection
                connection.Open();

                //Execute the command
                SqlCommand run_query = new SqlCommand(query, connection);
                //then execute
                SqlDataReader read_events = run_query.ExecuteReader();

                //clear the message so we start fresh
                message = "";

                //loop through the rows
                while (read_events.Read())
                {
                    data_found = true;
                    //get events by column names and append
                    message += "----------------------------------------------" +
                               "\n    [ Event no " + (count_events += 1) + " ]      \n" +
                               "----------------------------------------------\n";
                    //appending so message variable does not lose the last data
                    message += "Event   ID: " + read_events["eventID"].ToString() + "\n" +
                               "Event NAME: " + read_events["eventName"].ToString() + "\n" +
                               "Event Desc: " + read_events["description"].ToString() + "\n" +
                               "Event Date: " + Convert.ToDateTime(read_events["date"]).ToString("yyyy-MM-dd") + "\n" +
                               //"Event Loca: " + read_events["location"].ToString() + "\n" +
                               "Event Dist: " + read_events["distance"].ToString() + "\n" +
                               "Event Type: " + read_events["eventType"].ToString() + "\n" +
                               "Event  Age: " + read_events["age"].ToString() + "\n" +
                               "Event Start: " + read_events["startTime"].ToString() + "\n" +
                               "====================================================================\n" +
                               "Event Category ID: " + read_events["category_id"].ToString() + "\n" +
                               "Event Creator  ID: " + read_events["user_id"].ToString() + "\n" +
                               "=====================================================================\n";

                }//end of while loop

                //close the reader
                read_events.Close();

                //if no data was found, send a friendly message
                if (!data_found)
                {
                    message = "No events found for this organizer.....";
                }

                //close the connection
                connection.Close();

            }
            catch (Exception error)
            {
                //assign the error message from catch
                message = error.Message;
            }//end of catch

            //then return
            return message;

        }//end of view_events method





        //method for updating_events
        public string updating_events(int? event_id, string? eventName, string? description, DateTime? date, string? location, decimal? distance, string? eventType, int? age, TimeSpan? startTime, int? category_id, string organizer_id, HttpContext get_session)
        {
            //temp variables
            string message = "Failed to update event !!!!!!!!";
            string connection_string = @"Data source=(localdb)\part_2_api;database=part_2;";

            //try and catch
            try
            {
                //temp for query
                int owner_id = Int32.Parse(organizer_id);

                //building a SET of clause  based on which fields were provided
                List<string> sets = new List<string>();

                if (!string.IsNullOrWhiteSpace(eventName))
                {
                    sets.Add("eventName='" + eventName + "'");
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    sets.Add("description='" + description + "'");
                }

                if (date != null)
                {
                    sets.Add("date='" + date.Value.ToString("yyyy-MM-dd") + "'");
                }

                if (!string.IsNullOrWhiteSpace(location))
                {
                    sets.Add("location='" + location + "'");
                }

                if (distance != null)
                {
                    sets.Add("distance=" + distance.Value);
                }

                if (!string.IsNullOrWhiteSpace(eventType))
                {
                    sets.Add("eventType='" + eventType + "'");
                }

                if (age != null)
                {
                    sets.Add("age=" + age.Value);
                }

                if (startTime != null)
                {
                    sets.Add("startTime='" + startTime.Value.ToString(@"hh\:mm\:ss") + "'");
                }

                if (category_id != null)
                {
                    sets.Add("category_id=" + category_id.Value);
                }

                if (sets.Count == 0)
                {
                    return "Nothing to update, all fields were empty.....";
                }

                //query to update the event only if this organizer owns it
                string query = "update Events set " + string.Join(", ", sets) + " where eventID=" + event_id + " and user_id=" + owner_id + ";";

                //connecting to the server and database
                SqlConnection connection = new SqlConnection(connection_string);
                //open the connection
                connection.Open();

                //Execute the command
                SqlCommand run_query = new SqlCommand(query, connection);
                //then execute
                int affected_rows = run_query.ExecuteNonQuery();

                if (affected_rows == 0)
                {
                    message = "Failed to update event, either it does not exist or you do not own it !!!!!!!!";
                }
                else
                {
                    message = "Event with id " + event_id + " is successfully updated..";
                }

                //close the connection
                connection.Close();

            }
            catch (Exception error)
            {
                //assign the error message from catch
                message = error.Message;
            }//end of catch

            //then return
            return message;

        }//end of updating_events method


        //method for deleting_events
        public string deleting_events(int? event_id, string organizer_id, HttpContext get_session)
        {
            //temp variables
            string message = "Failed to delete event !!!!!!!!";
            string connection_string = @"Data source=(localdb)\part_2_api;database=part_2;";

            //try and catch
            try
            {
                //temp for query
                int owner_id = Int32.Parse(organizer_id);

                //query to delete only if this organizer owns the event
                string query = @"delete from Events where eventID=" + event_id + " and user_id=" + owner_id + ";";

                //connecting to the server and database
                SqlConnection connection = new SqlConnection(connection_string);
                //open the connection
                connection.Open();

                //Execute the command
                SqlCommand run_query = new SqlCommand(query, connection);
                //then execute
                int affected_rows = run_query.ExecuteNonQuery();

                if (affected_rows == 0)
                {
                    message = "Failed to delete event, either it does not exist or you do not own it !!!!!!!!";
                }
                else
                {
                    message = "Event with id " + event_id + " is successfully deleted..";
                }

                //close the connection
                connection.Close();

            }
            catch (Exception error)
            {
                //assign the error message from catch
                message = error.Message;
            }//end of catch

            //then return
            return message;

        }//end of deleting_events method


    }//end of class

}//end of namespace