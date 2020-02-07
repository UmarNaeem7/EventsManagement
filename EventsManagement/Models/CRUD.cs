using System;

using System.Data.SqlClient;
using System.Data;

namespace EventsManagement.Models
{
    public class CRUD
    {
        public static string connectionString = "Data Source=DESKTOP-C7RHKE6;Initial Catalog=events;Persist Security Info=False;Integrated Security=SSPI;";
      


        public static int UserSignUpProc(string email, string password, string fullname)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 1;

            try
            {
                cmd = new SqlCommand("UserSignupProc", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = email;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 100).Value = password;
                cmd.Parameters.Add("@fullname", SqlDbType.VarChar, 100).Value = fullname;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error" + ex.Message.ToString());
                result = 0; //0 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        public static int UserLoginProc(string email, string password)
        {
            

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result;

            try
            {
                cmd = new SqlCommand("UserLoginProc", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = email;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 100).Value = password;

                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
               

             
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = 0; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }

            return result;
        }

        public static string Getfullname(string email, string password)
        {


            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            string name;

            try
            {
                cmd = new SqlCommand("getfullname", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = email;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 100).Value = password;

                cmd.Parameters.Add("@fullname", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        
                cmd.ExecuteNonQuery();

                name = (cmd.Parameters["@fullname"].Value).ToString();
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                name = "";
            }
            finally
            {
                con.Close();
            }

            return name;
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table class = \"myTable\">";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        public static string ConvertDataTableToHTML1(DataTable dt)
        {
            string html = "<table class = \"myTable\">";
            //add header row
            string a = "Edit Event";
            string b = "Delete Event";
            
           
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "<td>" + a + "</td>";
            html += "<td>" + b + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";

                html += "<td>" + "<button class=\"btn btn-info btn - lg\" data-toggle=\"modal\" data-target=\"#myModal\" onclick=\"test('" + dt.Rows[i][0].ToString() + "'" + "," + "'" + dt.Rows[i][1].ToString() + "'" + "," + "'" + dt.Rows[i][2].ToString() + "'" + "," + "'" + dt.Rows[i][3].ToString() + "'" + "," + "'" + dt.Rows[i][4].ToString() + "'" + "," + "'" + dt.Rows[i][5].ToString() + "'" + "," + "'" + dt.Rows[i][6].ToString() + "'" + "," + "'" + dt.Rows[i][7].ToString() + "')\">Edit</ button > " + " </td>";

                //html += "<td>" + "<button class=\"btn btn-info btn - lg\" data-toggle=\"modal\" data-target=\"#myModal\" onclick=\"test('" + dt.Rows[i][0].ToString() + "'" + "," + "'" + dt.Rows[i][1].ToString() + "'" + "," + "'" + dt.Rows[i][2] + "'" + "," + "'"  + dt.Rows[i][3] +  "')\">Edit</ button > " + " </td>";

                //html += "<td>" + "<button class=\"btn btn-info btn - lg\" data-toggle=\"modal\" data-target=\"#myModal\" onclick=\"test('" + dt.Rows[i][0].ToString() +  "')\">Edit</ button > " + " </td>";

                html += "<td>" + "<button class=\"btn btn-info btn - lg\" data-toggle=\"modal\" data-target=\"#myModal1\" onclick=\"del('" + dt.Rows[i][0].ToString() + "'" + "," + "'" + dt.Rows[i][1].ToString() + "')\" >Delete</ button > " + " </td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        public static DataTable GetAllEvents()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            string query = "SELECT * from [event]";
            DataTable dt = new DataTable();

            cmd = new SqlCommand(query, con);
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        public static DataTable GetUpcomingPublicEventsList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            string query = "SELECT [title], [startdate], [startTime], [duration], [description], [location], [author] " +
                "FROM [event]" +
                "where [upcoming] = 1 AND [isPublic] = 1";
            DataTable dt = new DataTable();

            cmd = new SqlCommand(query, con);
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        public static DataTable GetPassedPublicEventsList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            string query = "SELECT [title], [startdate], [startTime], [duration], [description], [location], [author] " +
                "FROM [event]" +
                "where [upcoming] = 0 AND [isPublic] = 1";
            DataTable dt = new DataTable();

            cmd = new SqlCommand(query, con);
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        public static DataTable GetUpcomingPrivateEventsList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            string query = "SELECT [title], [startdate], [startTime], [duration], [description], [location], [author] " +
                "FROM [event]" +
                "where [upcoming] = 1";
            DataTable dt = new DataTable();

            cmd = new SqlCommand(query, con);
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        public static DataTable GetPassedPrivateEventsList()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            string query = "SELECT [title], [startdate], [startTime], [duration], [description], [location], [author] " +
                "FROM [event]" +
                "where [upcoming] = 0";
            DataTable dt = new DataTable();

            cmd = new SqlCommand(query, con);
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        public static DataTable GetUserEventsList(string name)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            string query = "select [title], [startdate], [startTime], [duration], [description], [location], [isPublic], [upcoming]" +
                " from [event] where [author] = " + "'" + name + "'";
            DataTable dt = new DataTable();

            cmd = new SqlCommand(query, con);
            dt.Load(cmd.ExecuteReader());
            con.Close();
            return dt;
        }

        public static int AddEventToDB(string title, string date, string time, string duration, string description, string location, int isPublic, string author, int upcoming)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 1;
            
            try
            {
                cmd = new SqlCommand("AddEventToDB", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = title;
                cmd.Parameters.Add("@startdate", SqlDbType.VarChar, 30).Value = date;
                cmd.Parameters.Add("@startTime", SqlDbType.VarChar, 30).Value = time;
                cmd.Parameters.Add("@duration", SqlDbType.VarChar, 30).Value = duration;
                cmd.Parameters.Add("@description", SqlDbType.VarChar, 200).Value = description;
                cmd.Parameters.Add("@location", SqlDbType.VarChar, 100).Value = location;
                cmd.Parameters.Add("@isPublic", SqlDbType.Int).Value = isPublic;
                cmd.Parameters.Add("@author", SqlDbType.VarChar, 100).Value = author;
                cmd.Parameters.Add("@upcoming", SqlDbType.Int).Value = upcoming;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
               
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error" + ex.Message.ToString());
                result = 0; //0 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static int EditEvent(string title, string date, string time, string duration, string description, string location, int isPublic, string author, int upcoming)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 1;

            try
            {
                cmd = new SqlCommand("EditEvent", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = title;
                cmd.Parameters.Add("@startdate", SqlDbType.VarChar, 30).Value = date;
                cmd.Parameters.Add("@startTime", SqlDbType.VarChar, 30).Value = time;
                cmd.Parameters.Add("@duration", SqlDbType.VarChar, 30).Value = duration;
                cmd.Parameters.Add("@description", SqlDbType.VarChar, 200).Value = description;
                cmd.Parameters.Add("@location", SqlDbType.VarChar, 100).Value = location;
                cmd.Parameters.Add("@isPublic", SqlDbType.Int).Value = isPublic;
                cmd.Parameters.Add("@author", SqlDbType.VarChar, 100).Value = author;
                cmd.Parameters.Add("@upcoming", SqlDbType.Int).Value = upcoming;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error" + ex.Message.ToString());
                result = 0; //0 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static int EditAdminEvent(string title, string date, string time, string duration, string description, string location, int isPublic, int upcoming)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 1;

            try
            {
                cmd = new SqlCommand("EditAdminEvent", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = title;
                cmd.Parameters.Add("@startdate", SqlDbType.VarChar, 30).Value = date;
                cmd.Parameters.Add("@startTime", SqlDbType.VarChar, 30).Value = time;
                cmd.Parameters.Add("@duration", SqlDbType.VarChar, 30).Value = duration;
                cmd.Parameters.Add("@description", SqlDbType.VarChar, 200).Value = description;
                cmd.Parameters.Add("@location", SqlDbType.VarChar, 100).Value = location;
                cmd.Parameters.Add("@isPublic", SqlDbType.Int).Value = isPublic;
                cmd.Parameters.Add("@upcoming", SqlDbType.Int).Value = upcoming;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error" + ex.Message.ToString());
                result = 0; //0 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static int DeleteAdminEvent(string title, string date)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 1;

            try
            {
                cmd = new SqlCommand("DeleteAdminEvent", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = title;
                cmd.Parameters.Add("@startdate", SqlDbType.VarChar, 30).Value = date;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error" + ex.Message.ToString());
                result = 0; //0 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public static int DeleteEvent(string title, string date, string author)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 1;

            try
            {
                cmd = new SqlCommand("DeleteUserEvent", con)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@title", SqlDbType.VarChar, 50).Value = title;
                cmd.Parameters.Add("@startdate", SqlDbType.VarChar, 30).Value = date;
                cmd.Parameters.Add("@author", SqlDbType.VarChar, 100).Value = author;
                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                result = Convert.ToInt32(cmd.Parameters["@output"].Value);
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error" + ex.Message.ToString());
                result = 0; //0 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}