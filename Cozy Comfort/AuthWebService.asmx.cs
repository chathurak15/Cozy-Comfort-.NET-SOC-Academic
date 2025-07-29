using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace Cozy_Comfort
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
  
    public class AuthWebService : System.Web.Services.WebService
    {
        SqlConnection con;

        //get db connection
        public SqlConnection getConnection()
        {
            try
            {
                con = new SqlConnection("data source=localhost\\SQLEXPRESS; initial catalog=CozyComfort; Integrated Security=True");
                con.Open();
            }catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Db" + ex);
            }
            return con;
        }

        //user login method (username and password)
        [WebMethod]
        public string Login(string username, string password)
        {
            getConnection();
            try
            {
                SqlCommand cmd = new SqlCommand
                    ("Select * from Users Where Username = '" + username + "' AND Password = '" + password + "'", con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    con.Close();
                    return "Login Sucees!";
                }
                else
                {
                    con.Close();
                    return "Invalid Username or Password";
                }
            }
            catch (Exception ex)
            {
               return "Error User Login" + ex;

            }
            
        }

        //get User role by user name
        [WebMethod]
        public string getRole(string username)
        {
            getConnection ();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Role FROM Users WHERE Username = '"+username+"'", con);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return "user not found";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        //get All Manufacturer,Distributor and seller name list
        [WebMethod]
        public DataSet getUserName(string table)
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand ($"SELECT Name FROM {table} ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, table);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error "+ table+" Name" + ex);
            }
            con.Close();
            return ds;
        }

        //get Manufacturer, Distributor and seller by name
        [WebMethod]
        public string getUserId(string userName, string table)
        {
            string userId = "";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand($"SELECT Id FROM {table} WHERE Name = '" + userName + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();

                bool records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        userId = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error "+ table + ex);
            }
            con.Close();
            return userId;
        }
    }
}
