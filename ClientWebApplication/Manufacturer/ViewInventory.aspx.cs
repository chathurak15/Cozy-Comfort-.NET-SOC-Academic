using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Manufacturer
{
    public partial class ViewInventory : System.Web.UI.Page
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
            DataSet ds = manufacturer.getBlankets();
            gvInventory.DataSource = ds;
            gvInventory.DataBind();
        }

        protected void gvInventory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                string modelId = e.CommandArgument.ToString();
                string result = manufacturer.deleteBlanket(modelId);

                if (result == "1")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Blanket deleted successfully!');", true);
                    LoadInventory();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Delete failed: " + result + "');", true);
                }
            }
        }

    }
}