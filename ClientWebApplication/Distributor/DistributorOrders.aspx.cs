using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Distributor
{
    public partial class DistributorOrders : System.Web.UI.Page
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
            int distributorId = 1;
            DataSet ds = distributor.getDistributorOrders(distributorId);
            gvMyOrders.DataSource = ds;
            gvMyOrders.DataBind();
        }

        protected void gvMyOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int RequestId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "complete")
            {
                string status = e.CommandName == "complete" ? "completed" : "pending";

                string result = distributor.updateOrder(RequestId);

                if (result == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Order {status}!');", true);
                    LoadInventory(); 
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Failed: {result}');", true);
                }
            }else if (e.CommandName == "deleteOrder")
            {
                string result = distributor.deleteDistributorOrder(RequestId);
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