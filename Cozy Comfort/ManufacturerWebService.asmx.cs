using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace Cozy_Comfort
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class ManufacturerWebService : System.Web.Services.WebService
    {
        SqlConnection con;
        public SqlConnection getConnection()
        {
            try
            {
                con = new SqlConnection("data source=localhost\\SQLEXPRESS; initial catalog=CozyComfort; Integrated Security=True");
                con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Db" + ex);
            }
            return con;
        }

        //Add new blanket
        [WebMethod]
        public string insertBlanketInfo(string name, string material, int price,
            string size, string color)
        {
            int noRow = 0;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO BlanketModels values(" +
                    "'" + name + "','" + material + "','" + size + "','"
                    + color + "','" + price + "')", con);
                noRow = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "error" + ex;
            }
            return noRow.ToString();
        }

        //add Manufacturer Stock
        [WebMethod]
        public string insertBlanketStock(int ManufacturerId, int ModelId, int stock)
        {
            int noRow = 0;
            DateTime dateTime = DateTime.Now;

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO ManufacturerStock values(" +
                    "'" + ManufacturerId + "','" + ModelId + "','" + stock + "','" + dateTime + "')", con);
                noRow = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "error" + ex;
            }
            con.Close();
            return noRow.ToString();
        }

        //get All Blankets name
        [WebMethod]
        public DataSet getBlanketName()
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Name FROM BlanketModels", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "BlanketModels");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Blanket Name" + ex);
            }
            con.Close();
            return ds;
        }

        //get BlanketId by Blanketname
        [WebMethod]
        public string getBlanketId(string blanketName)
        {
            string blanketId = "";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT ModelId FROM BlanketModels " +
                    "WHERE Name = '" + blanketName + "'", con); ;
                SqlDataReader dr = cmd.ExecuteReader();

                bool records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        blanketId = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error blanketId " + ex);
            }
            con.Close ();
            return blanketId;
        }

        //get Manufacturer inventry
        [WebMethod]
        public DataSet getBlankets()
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT b.*, bs.Quantity, bs.LastUpdated" +
                    " FROM BlanketModels b " +
                    "JOIN ManufacturerStock bs on b.ModelId = bs.ModelId ", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ManufacturerInventory");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getTING Bkankets" + ex);
            }
            con.Close () ;
            return ds;
        }

        // Get all distributor stock requests (to manufacturer) including distributor contact and blanket model info
        [WebMethod]
        public DataSet getDistributorOrder()
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand(
                    "SELECT dr.RequestId, d.Name AS Distributor, dr.ModelId, " +
                    "b.Name AS ModelName, dr.RequestedQty, dr.RequestedDate AS Date, d.Phone, d.Location, dr.Status " +
                    "FROM DistributorRequests dr " +
                    "JOIN Distributors d ON dr.DistributorId = d.Id " +
                    "JOIN BlanketModels b ON dr.ModelId = b.ModelId", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "DistributorOrderRequest");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting Distributor Order Request: " + ex.Message);
            }
            con.Close();
            return ds;
        }


        [WebMethod]
        public string getcurrentStock(string modelId, string manufacturerId)
        {
            string stock = "";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand
                ("SELECT Quantity FROM ManufacturerStock WHERE ModelId = " + modelId + 
                " AND ManufacturerId = " + manufacturerId, con);
                SqlDataReader dr = cmd.ExecuteReader();

                bool records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        stock = dr["Quantity"].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error stock " + ex);
            }
            con.Close();
            return stock;
        }

        //Update stock
        [WebMethod]
        public string updateStock(string modelId, string manufacturerId, int newStock)
        {
            int noRow = 0;
            DateTime dateTime = DateTime.Now;

            try
            {
                getConnection();
                string query = "UPDATE ManufacturerStock SET Quantity = '" + newStock +
                       "', LastUpdated = '" + dateTime +
                       "' WHERE ModelId = " + modelId + " AND ManufacturerId = " + manufacturerId;

                SqlCommand cmd = new SqlCommand(query, con);
                noRow = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "error" + ex;
            }
            con.Close();
            return noRow.ToString();
        }

        // Update Distributor order status (e.g., Approved/Rejected) and update manufacturer stock if approved
        [WebMethod]
        public string updateDisributorRequestStatus(int requestId, string status)
        {
            if (requestId <= 0 || string.IsNullOrEmpty(status))
            {
                return "Invalid input";
            }

            string rowsAffected = "0";
            int modelId, qty;

            try
            {
                (modelId, qty) = getDistributorRequestDetails(requestId);

                if (status == "Approved")
                {
                    if (checkManufacturerStockAvailability(modelId, qty))
                    {
                        rowsAffected = updateDistributorRequestStatusInternal(requestId, status);
                        if (rowsAffected == "1")
                        {
                            getConnection();
                            SqlCommand cmd = new SqlCommand("SELECT Quantity FROM ManufacturerStock  WHERE ModelId = " + modelId, con);
                            int currentQty = Convert.ToInt32(cmd.ExecuteScalar());
                            
                            int newStock = currentQty - qty;

                            SqlCommand updateStockCmd = new SqlCommand("UPDATE ManufacturerStock SET Quantity = " + newStock + ", " +
                                "LastUpdated = '" + DateTime.Now + "' WHERE ModelId = " + modelId, con);
                            updateStockCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        return "Not enough manufacturer stock";
                    }
                }
                else
                {
                    rowsAffected = updateDistributorRequestStatusInternal(requestId, status);
                }

            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            con.Close();
            return rowsAffected;
        }

        // Update Distributor request status (used internally)
        private string updateDistributorRequestStatusInternal(int requestId, string status)
        {
            string rowsAffected = "0";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("UPDATE DistributorRequests SET Status = '" + status + "' " +
                    "WHERE RequestId = " + requestId, con);

                rowsAffected = cmd.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }

            con.Close();
            return rowsAffected;
        }

        // Get modelId and quantity from a DistributorRequest
        private (int modelId, int qty) getDistributorRequestDetails(int requestId)
        {
            int modelId = 0, qty = 0;
            getConnection();
            SqlCommand cmd = new SqlCommand("SELECT ModelId, RequestedQty FROM DistributorRequests WHERE RequestId = " + requestId, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    modelId = Convert.ToInt32(dr["ModelId"]);
                    qty = Convert.ToInt32(dr["RequestedQty"]);
                }
            }
            dr.Close();
            con.Close();
            return (modelId, qty);
        }

        // Check if Manufacturer has enough stock for a specific blanket model and quantity
        [WebMethod]
        public bool checkManufacturerStockAvailability(int modelId, int qty)
        {
            bool available = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Quantity FROM ManufacturerStock " +
                    "WHERE ModelId = " + modelId, con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int currentQty = Convert.ToInt32(dr["Quantity"]);
                        available = currentQty >= qty;
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking manufacturer stock: " + ex.Message);
            }
            con.Close();
            return available;
        }

        //delete Blanket stock and blanket
        [WebMethod]
        public string deleteBlanket(string modelId)
        {
            int noRow = 0;
            try
            {
                getConnection();

                // First delete from ManufacturerStock
                string deleteStockQuery = "DELETE FROM ManufacturerStock WHERE ModelId = " + modelId;
                SqlCommand deleteStockCmd = new SqlCommand(deleteStockQuery, con);
                deleteStockCmd.ExecuteNonQuery();

                // Then delete from BlanketModels
                string deleteModelQuery = "DELETE FROM BlanketModels WHERE ModelId = " + modelId;
                SqlCommand deleteModelCmd = new SqlCommand(deleteModelQuery, con);
                noRow = deleteModelCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
            return noRow.ToString();
        }
    }
}
