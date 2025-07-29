using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Distributor
{
    public partial class SellerOrders : System.Web.UI.Page
    {
        // Create a instance to access Distributor web service methods
        DistributorServiceReference.DistributorWebServiceSoapClient distributor = new DistributorServiceReference.DistributorWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInventory();
            }
        }
        private void LoadInventory()
        {
            DataSet ds = distributor.getSellerOrder(1);
            gvOrders.DataSource = ds;
            gvOrders.DataBind();
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                string status = e.CommandName == "Approve" ? "Approved" : "Rejected";
                int distributorId = 1;

                // Call your reusable update method
                string result = distributor.updateSellerRequestStatus(orderId, status,distributorId);

                if (result == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Order {status}!');", true);
                    LoadInventory(); // reload grid data
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