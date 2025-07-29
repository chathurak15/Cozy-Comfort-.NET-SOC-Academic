using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Seller
{
    public partial class SellerRequests : System.Web.UI.Page
    {
        // Create a instance to access Seller web service methods
        SellerServiceReference.SellerServiceSoapClient seller = new SellerServiceReference.SellerServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInventory();
            }

        }
        private void LoadInventory()
        {
            DataSet ds = seller.getSellerDistributorRequests();
            gvMyOrders.DataSource = ds;
            gvMyOrders.DataBind();
        }

        protected void gvMyOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RequestId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "complete")
            {
                string status = e.CommandName == "complete" ? "completed" : "pending";

                string result = seller.updateSellerOrder(RequestId);

                if (result == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Order {status}!');", true);
                    LoadInventory();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Failed: {result}');", true);
                }
            }
            else if (e.CommandName == "deleteOrder")
            {
                string result = seller.deleteSellerOrder(RequestId);
                if (result == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Order Deletedzg" +
                        $"!');", true);
                    LoadInventory();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Failed: {result}');", true);
                }
            }
        }

        protected void btnRefreshInventory_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }
    }
}