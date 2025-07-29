using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Seller
{
    public partial class CustomerOrders : System.Web.UI.Page
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
            DataSet ds = seller.getCustomerOrder();
            gvOrders.DataSource = ds;
            gvOrders.DataBind();
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Complete" || e.CommandName == "Delete")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                string status = e.CommandName == "Complete" ? "Completed" : "Delete";
    

            }
        }

        protected void btnRefreshInventory_Click(object sender, EventArgs e)
        {
            LoadInventory();

        }
    }
}