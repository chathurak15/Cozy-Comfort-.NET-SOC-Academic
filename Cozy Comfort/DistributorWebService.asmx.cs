using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Cozy_Comfort
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class DistributorWebService : System.Web.Services.WebService
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

        //get he full inventory for the distributor including blanket model and current stock info
        [WebMethod]
        public DataSet getDistributorInventry()
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT b.*, bs.Quantity, bs.LastUpdated" +
                    " FROM BlanketModels b " +
                    "JOIN DistributorStock bs on b.ModelId = bs.ModelId ", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "DistributorInventory");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error geting Distributor Inventory " + ex);
            }
            con.Close();
            return ds;
        }

        //get all seller orders for a given distributor including seller contact and order status
        [WebMethod]
        public DataSet getSellerOrder(int distributorId)
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand(
                    "SELECT sd.RequestId AS OrderId, s.Name AS Seller, sd.ModelId, " +
                    "b.Name AS ModelName, sd.Quantity, sd.RequestDate AS Date, s.Phone, s.Address, sd.Status " +
                    "FROM SellerDistributorRequests sd " +
                    "JOIN Sellers s ON sd.SellerId = s.Id " +
                    "JOIN BlanketModels b ON sd.ModelId = b.ModelId " +
                    "WHERE sd.DistributorId = " + distributorId, con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "SellerDistributorRequests");

            }catch (Exception ex) 
            { 
            Console.WriteLine("Error getting SellerDistributorRequests" + ex);
            }

            con.Close();
            return ds;
        }

        //get distributor's own orders for stock from manufacturer, including blanket details
        [WebMethod]
        public DataSet getDistributorOrders(int distributorId)
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();

                // SQL query to join BlanketModels with DistributorRequests
                SqlCommand cmd = new SqlCommand(
                    "SELECT d.RequestId, d.ModelId,b.Name AS ModelName, d.RequestedQty AS Quantity, d.RequestedDate AS Date, d.Status " +
                    "FROM DistributorRequests d " +
                    "JOIN BlanketModels b ON d.ModelId = b.ModelId " +
                    "WHERE d.DistributorId = " + distributorId, con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "DistributorRequests");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting DistributorRequests: " + ex.Message);
            }
            con.Close();
            return ds;
        }

        // Update seller order status (e.g., Approved/Rejected) and update distributor stock if approved
        [WebMethod]
        public string updateSellerRequestStatus(int requestId, string status, int distributorId)
        {
            if (requestId <= 0 || string.IsNullOrEmpty(status)) {
                return "Invalid input";
            }
            string rowsAffected = "0";
            int modelId, qty;
            try
            {
                (modelId, qty) = getRequestDetails(requestId);

                if (status == "Approved")
                {
                    if (checkStockAvaialbilty(modelId, qty))
                    {
                        rowsAffected = updateRequestStatus(requestId, status);
                        if (rowsAffected == "1")
                        {
                            getConnection();
                            SqlCommand cmd = new SqlCommand("SELECT Quantity FROM DistributorStock WHERE ModelId = " + modelId, con);
                            int currentQty = Convert.ToInt32(cmd.ExecuteScalar());
                            int newStock = currentQty - qty;

                            SqlCommand updateStockCmd = new SqlCommand("UPDATE DistributorStock SET Quantity = " + newStock + ", " +
                                "LastUpdated = '" + DateTime.Now + "'" +
                                " WHERE ModelId = " + modelId + " AND DistributorId = " + distributorId, con);
                            updateStockCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        return "Not enough stock";
                    }
                }
                else
                {
                    rowsAffected = updateRequestStatus(requestId, status);
                }

            }catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            con.Close();
            return rowsAffected;
        }

        //Update the status of a seller-distributor request(Approved/Rejected)
        private string updateRequestStatus(int requestId, string status)
        {
            string rowsAffected = "0";
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE SellerDistributorRequests SET Status = '" + status +"'" +
                    " WHERE RequestId = "+ requestId, con);

                rowsAffected = cmd.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }

            con.Close();
            return rowsAffected;
        }

        //Get modelId and quantity for a given seller request ID
        private (int modelId, int qty) getRequestDetails(int requestId)
        {
            int modelId = 0, qty = 0;
            getConnection();
            SqlCommand cmd = new SqlCommand("SELECT ModelId, Quantity FROM SellerDistributorRequests " +
                "WHERE RequestId = " + requestId, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    modelId = Convert.ToInt32(dr["ModelId"]);
                    qty = Convert.ToInt32(dr["Quantity"]);
                }
            }
            dr.Close();
            con.Close();
            return (modelId, qty);
        }

        // Check if distributor has enough stock for a specific blanket model and quantity
        [WebMethod]
        public bool checkStockAvaialbilty(int modelId, int qty)
        {
            bool available = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Quantity FROM DistributorStock WHERE ModelId = " + modelId, con);
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
                Console.WriteLine("Error checking stock: " + ex.Message);
            }
            con.Close();
            return available;
        }

        //Add a new distributor stock request (to manufacturer) and mark as Pending
        [WebMethod]
        public string addDistributorStock(int modelId, int qty, int distributorId)
        {
            int noRow = 0;
            DateTime dateTime = DateTime.Now;
            string status = "Pending";

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO DistributorRequests " +
                    "(DistributorId, ModelId, RequestedQty, Status, RequestedDate) " +
                    "values('" + distributorId + "','" + modelId + "','" + qty + "', '"+ status + "' ,'" + dateTime + "')", con);
                noRow = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "error" + ex;
            }
            con.Close();
            return noRow.ToString();
        }

        //Complete a distributor's order by updating stock or inserting new stock record, then mark as Completed
        [WebMethod]
        public string updateOrder(int RequestId)
        {
            int noRow = 0;
            try
            {
                getConnection();

                // Get Disributor stock request order details by request id
                SqlCommand getDetails = new SqlCommand("SELECT ModelId, RequestedQty, Status, DistributorId FROM DistributorRequests " +
                    " WHERE RequestId = " + RequestId, con);
                SqlDataReader dr = getDetails.ExecuteReader();

                int modelId = 0;
                int qty = 0;
                int distributorId = 0;
                string status = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        modelId = Convert.ToInt32(dr["ModelId"]);
                        qty = Convert.ToInt32(dr["RequestedQty"]);
                        status = dr["Status"].ToString();
                        distributorId = Convert.ToInt32(dr["DistributorId"]);
                    }
                }
                dr.Close();

                if (status == "Approved")
                {

                    // update request as Completed
                    SqlCommand updateStatusCmd = new SqlCommand("UPDATE DistributorRequests SET Status = 'Completed', RequestedDate = '" + DateTime.Now + "' " +
                        "WHERE RequestId = " + RequestId, con);
                    noRow = updateStatusCmd.ExecuteNonQuery();

                    // Check if the model already exists in DistributorStock
                    SqlCommand checkStock = new SqlCommand("SELECT COUNT(*) FROM DistributorStock " +
                        "WHERE ModelId = " + modelId + " AND DistributorId = " + distributorId, con);
                    int exists = (int)checkStock.ExecuteScalar();

                    if (exists > 0)
                    {
                        // Get current stock
                        SqlCommand getQtyCmd = new SqlCommand("SELECT Quantity FROM DistributorStock " +
                            "WHERE ModelId = " + modelId + " AND DistributorId = " + distributorId, con);
                        int currentStock = (int)getQtyCmd.ExecuteScalar();

                        // Update stock
                        int newStock = currentStock + qty;
                        SqlCommand updateStockCmd = new SqlCommand("UPDATE DistributorStock SET Quantity = " + newStock + ", " +
                            "LastUpdated = '" + DateTime.Now + "' WHERE ModelId = " + modelId + " AND DistributorId = " + distributorId, con);
                        updateStockCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Insert new stock record
                        SqlCommand insertCmd = new SqlCommand("INSERT INTO DistributorStock (DistributorId, ModelId, Quantity, LastUpdated) " +
                            "VALUES (" + distributorId + ", " + modelId + ", " + qty + ", '" + DateTime.Now + "')", con);
                        insertCmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    return "Only Approved orders can be completed.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            con.Close();
            return noRow.ToString();
        }

        // Delete Distributor order by orderId (Distributor can't delete Manufacturer Approved Orders)
        [WebMethod]
        public string deleteDistributorOrder(int RequestId)
        {
            int noRow = 0;
            try
            {
                getConnection();

                // First, check the order status
                SqlCommand checkCmd = new SqlCommand("SELECT Status FROM DistributorRequests WHERE RequestId = " + RequestId, con);
                string status = (string)checkCmd.ExecuteScalar();

                if (status != "Pending")
                {
                    return "Cannot delete an Approved or Completed order.";
                }

                SqlCommand deleteCmd = new SqlCommand("DELETE FROM DistributorRequests WHERE RequestId = " + RequestId, con);
                noRow = deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            con.Close();
            return noRow.ToString();
        }
    }
}
