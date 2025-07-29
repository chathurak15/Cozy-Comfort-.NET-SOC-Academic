using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Manufacturer
{
    
    public partial class DistributorRequests : System.Web.UI.Page
    {
        // Create a instance to access Manufacturer web service methods
        ManufacturerServiceReference.ManufacturerWebServiceSoapClient manufacturer = new ManufacturerServiceReference.ManufacturerWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInventory();
            }
        }

        protected void btnRefreshInventory_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void LoadInventory()
        {
            DataSet ds = manufacturer.getDistributorOrder();
            gvOrders.DataSource = ds;
            gvOrders.DataBind();
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve" || e.CommandName == "Reject")
            {
                int RequestId = Convert.ToInt32(e.CommandArgument);
                string status = e.CommandName == "Approve" ? "Approved" : "Rejected";

                // Call your reusable update method
                string result = manufacturer.updateDisributorRequestStatus(RequestId, status);

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
    }
}