using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Seller
{
    public partial class SellerInventory : System.Web.UI.Page
    {
        // Create a instance to access Manufacturer web service methods
        ManufacturerServiceReference.ManufacturerWebServiceSoapClient manufacturer = new ManufacturerServiceReference.ManufacturerWebServiceSoapClient();

        // Create a instance to access Seller web service methods
        SellerServiceReference.SellerServiceSoapClient seller = new SellerServiceReference.SellerServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dataSet = manufacturer.getBlanketName();
                ddlStockModel.DataSource = dataSet;
                ddlStockModel.DataBind();
                ddlStockModel.DataValueField = "Name";
                ddlStockModel.DataBind();

                LoadInventory();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int modelId = Int32.Parse(manufacturer.getBlanketId(ddlStockModel.SelectedValue));
            DataSet ds = seller.serachBlanket(modelId);
            gvSInventory.DataSource = ds;
            gvSInventory.DataBind();
        }
        private void LoadInventory()
        {
            DataSet ds = seller.getSellerInventry();
            gvSInventory.DataSource = ds;
            gvSInventory.DataBind();
        }
        protected void btnRefreshInventory_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }
    }
}