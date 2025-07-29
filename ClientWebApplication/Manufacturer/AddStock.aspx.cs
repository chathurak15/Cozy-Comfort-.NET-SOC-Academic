using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Manufacturer
{
    public partial class AddStock : System.Web.UI.Page
    {
        // Create a instance to access Manufacturer web service methods
        ManufacturerServiceReference.ManufacturerWebServiceSoapClient manufacturer = new ManufacturerServiceReference.ManufacturerWebServiceSoapClient();
        
        // Create a instance to access Auth web service methods
        AuthServiceReference.AuthWebServiceSoapClient auth = new AuthServiceReference.AuthWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dataSet = manufacturer.getBlanketName();
                ddlStockModel.DataSource = dataSet;
                ddlStockModel.DataBind();
                ddlStockModel.DataValueField = "Name";
                ddlStockModel.DataBind();

                DataSet manufacturerds = auth.getUserName("Manufacturers");
                dlManufacturer.DataSource = manufacturerds;
                dlManufacturer.DataBind();
                dlManufacturer.DataValueField = "Name";
                dlManufacturer.DataBind();
            }
        }

        protected void btnAddModel_Click(object sender, EventArgs e)
        {
            int blanketId = Int32.Parse(manufacturer.getBlanketId(ddlStockModel.SelectedValue));
            int manufacturerId = Int32.Parse(auth.getUserId(dlManufacturer.SelectedValue, "Manufacturers"));
            int qty = Int32.Parse(txtStockQuantity.Text);

            string result = manufacturer.insertBlanketStock(manufacturerId, blanketId, qty);

            if (result == "1"){
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Stock Successfully Added!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{result}');", true);
            }
        }

        protected void btnCancelModel_Click(object sender, EventArgs e)
        {
            txtStockQuantity.Text = "";
            ddlStockModel.SelectedIndex = 0;
            dlManufacturer.SelectedIndex = 0;
        }
    }
}