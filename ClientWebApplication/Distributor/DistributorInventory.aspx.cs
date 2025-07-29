using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Distributor
{
    public partial class DistributorInventory : System.Web.UI.Page
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
            DataSet ds = distributor.getDistributorInventry();
            gvDInventory.DataSource = ds;
            gvDInventory.DataBind();
        }

        protected void btnRefreshInventory_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }
        

    }
}