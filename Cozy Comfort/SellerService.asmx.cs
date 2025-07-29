
using System;
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
   
    public class SellerService : System.Web.Services.WebService
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

        // Add a new customer order and reduce seller stock if enough is available
        [WebMethod]
        public string addCustomerOrder(int blanketId, string customerName, string phone, int qty, int sellerId)
        {
            string status = "Pending";
            DateTime orderDate = DateTime.Now;

            try
            {
                if (!checkStockAvaialbilty(blanketId, qty))
                {
                    return "Not enough stock";
                }

                getConnection();
                SqlCommand insertCmd = new SqlCommand(
                    "INSERT INTO CustomerOrders (CustomerName, Phone, SellerId, ModelId, Quantity, OrderDate, Status) " +
                    "VALUES ('" + customerName + "', '" + phone + "', " + sellerId + ", " + blanketId + ", " +
                    qty + ", '" + orderDate + "', '" + status + "')", con);
                
                insertCmd.ExecuteNonQuery();

                // Reduce stock from SellerStock
                SqlCommand getStockCmd = new SqlCommand("SELECT Quantity FROM SellerStock WHERE ModelId = " + blanketId, con);
                int currentStock = Convert.ToInt32(getStockCmd.ExecuteScalar());
                int newStock = currentStock - qty;

                SqlCommand updateStockCmd = new SqlCommand("UPDATE SellerStock SET Quantity = " + newStock + ", " +
                    "LastUpdated = '" + DateTime.Now + "' WHERE ModelId = " + blanketId, con);
                updateStockCmd.ExecuteNonQuery();

                con.Close();
                return "1";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        // Check if seller has enough stock for a specific blanket model and quantity
        private bool checkStockAvaialbilty(int modelId, int qty)
        {
            bool available = false;
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT Quantity FROM SellerStock WHERE ModelId = " + modelId, con);
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

        //get he full inventory for the Seller including blanket model and current stock info
        [WebMethod]
        public DataSet getSellerInventry()
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("SELECT b.*, s.Quantity, s.LastUpdated" +
                    " FROM SellerStock s " +
                    "JOIN BlanketModels b on s.ModelId = b.ModelId ", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Seller Inventry");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error geting Seller Inventry " + ex);
            }
            con.Close();
            return ds;
        }

        /// Search Blanket info by ID including blanket model and current stock info
        [WebMethod]
        public DataSet serachBlanket(int modelId)
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand(
                    "SELECT b.*, s.Quantity, s.LastUpdated " +
                    "FROM SellerStock s " +
                    "JOIN BlanketModels b ON s.ModelId = b.ModelId " +
                    "WHERE b.ModelId = "+ modelId, con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "SellerInventory");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting Seller Inventory: " + ex.Message);
            }
            con.Close();
            return ds;
        }

        //get all seller orders for a given Seller including seller contact and order status
        [WebMethod]
        public DataSet getCustomerOrder()
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand(
                    "SELECT c.OrderId, c.CustomerName, c.Quantity, s.Name AS Seller, c.ModelId, " +
                    "b.Name AS ModelName, c.OrderDate AS Date, c.Status, c.Phone " +
                    "FROM CustomerOrders c " +
                    "JOIN Sellers s ON c.SellerId = s.Id " +
                    "JOIN BlanketModels b ON c.ModelId = b.ModelId ", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "SellerDistributorRequests");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting SellerDistributorRequests" + ex);
            }

            con.Close();
            return ds;
        }

        //get seller's own orders for stock from Distributor, including blanket details
        [WebMethod]
        public DataSet getSellerDistributorRequests()
        {
            DataSet ds = new DataSet();
            try
            {
                getConnection();

                SqlCommand cmd = new SqlCommand(
                    "SELECT s.RequestId, s.ModelId, b.Name AS ModelName, d.Name AS Distributor, d.Phone, " +
                    "s.Quantity, s.RequestDate AS Date, s.Status " +
                    "FROM SellerDistributorRequests s " +
                    "JOIN Distributors d ON s.DistributorId = d.Id " +
                    "JOIN BlanketModels b ON s.ModelId = b.ModelId", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "SellerRequests");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting seller requests: " + ex.Message);
            }

            con.Close();
            return ds;
        }

        // Add a new seller's stock request to a distributor and mark it as Pending
        [WebMethod]
        public string addSellerDistributorRequest(int sellerId, int distributorId, int modelId, int quantity)
        {
            int noRow = 0;
            DateTime requestDate = DateTime.Now;
            string status = "Pending";

            try
            {
                getConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO SellerDistributorRequests " +
                    "(SellerId, DistributorId, ModelId, Quantity, RequestDate, Status) " +
                    "VALUES (" + sellerId + ", " + distributorId + ", " + modelId + ", " + quantity + ", '" + requestDate + "', '" + status + "')", con);

                noRow = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            con.Close();
            return noRow.ToString();
        }

        // Complete a seller's order by updating stock or inserting new stock record, then mark as Completed
        [WebMethod]
        public string updateSellerOrder(int requestId)
        {
            int noRow = 0;
            try
            {
                getConnection();

                // Get seller request details
                SqlCommand getDetails = new SqlCommand("SELECT ModelId, Quantity, Status, SellerId FROM SellerDistributorRequests " +
                    "WHERE RequestId = " + requestId, con);
                SqlDataReader dr = getDetails.ExecuteReader();

                int modelId = 0;
                int qty = 0;
                int sellerId = 0;
                string status = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        modelId = Convert.ToInt32(dr["ModelId"]);
                        qty = Convert.ToInt32(dr["Quantity"]);
                        status = dr["Status"].ToString();
                        sellerId = Convert.ToInt32(dr["SellerId"]);
                    }
                }
                dr.Close();

                if (status == "Approved")
                {
                    // Update request as Completed
                    SqlCommand updateStatusCmd = new SqlCommand(
                        "UPDATE SellerDistributorRequests SET Status = 'Completed', RequestDate = '" + DateTime.Now + "' " +
                        "WHERE RequestId = " + requestId, con);
                    noRow = updateStatusCmd.ExecuteNonQuery();

                    // Check if stock already exists
                    SqlCommand checkStock = new SqlCommand("SELECT COUNT(*) FROM SellerStock " +
                        "WHERE ModelId = " + modelId + " AND SellerId = " + sellerId, con);
                    int exists = (int)checkStock.ExecuteScalar();

                    if (exists > 0)
                    {
                        // Update existing stock
                        SqlCommand getQtyCmd = new SqlCommand("SELECT Quantity FROM SellerStock " +
                            "WHERE ModelId = " + modelId + " AND SellerId = " + sellerId, con);
                        int currentStock = (int)getQtyCmd.ExecuteScalar();

                        int newStock = currentStock + qty;
                        SqlCommand updateStockCmd = new SqlCommand("UPDATE SellerStock SET Quantity = " + newStock + ", " +
                            "LastUpdated = '" + DateTime.Now + "' WHERE ModelId = " + modelId + " AND SellerId = " + sellerId, con);
                        updateStockCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Insert new stock record
                        SqlCommand insertCmd = new SqlCommand("INSERT INTO SellerStock (SellerId, ModelId, Quantity, LastUpdated) " +
                            "VALUES (" + sellerId + ", " + modelId + ", " + qty + ", '" + DateTime.Now + "')", con);
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

        // Delete Seller order by requestId (only Pending orders)
        [WebMethod]
        public string deleteSellerOrder(int requestId)
        {
            int noRow = 0;
            try
            {
                getConnection();

                SqlCommand checkCmd = new SqlCommand("SELECT Status FROM SellerDistributorRequests WHERE RequestId = " + requestId, con);
                string status = (string)checkCmd.ExecuteScalar();

                if (status != "Pending")
                {
                    return "Cannot delete an Approved or Completed order.";
                }

                SqlCommand deleteCmd = new SqlCommand("DELETE FROM SellerDistributorRequests WHERE RequestId = " + requestId, con);
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
