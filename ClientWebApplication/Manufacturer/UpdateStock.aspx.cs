using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Manufacturer
{
    public partial class UpdateStock : System.Web.UI.Page
    {
        // Create a instance to access Manufacturer web service methods
        ManufacturerServiceReference.ManufacturerWebServiceSoapClient manufacturer = new ManufacturerServiceReference.ManufacturerWebServiceSoapClient();

        // Create a instance to access Auth web service methods
        AuthServiceReference.AuthWebServiceSoapClient auth = new AuthServiceReference.AuthWebServiceSoapClient();

        string blanketId;
        string manufacturerId;
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

        protected void btnupdateModel_Click(object sender, EventArgs e)
        {
            string blanketId = manufacturer.getBlanketId(ddlStockModel.SelectedValue);
            string manufacturerId = auth.getUserId(dlManufacturer.SelectedValue, "Manufacturers");
            int qty = Int32.Parse(txtQuantity.Text);

            string result = manufacturer.updateStock(blanketId,manufacturerId,qty);
            
            if(result == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Successfully updated!');", true);
                
                ddlStockModel.SelectedIndex = -1;
                dlManufacturer.SelectedIndex = -1;
                lblQuantity.Text = "";
                txtQuantity.Text = "";
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{result}');", true);
            }
        }

        protected void btnCancelModel_Click(object sender, EventArgs e)
        {
            ddlStockModel.SelectedIndex = -1;
            dlManufacturer.SelectedIndex = -1;
            lblQuantity.Text = "";
            txtQuantity.Text = "";
        }

        protected void btncurrentStock_Click(object sender, EventArgs e)
        {
           
            string blanketId = manufacturer.getBlanketId(ddlStockModel.SelectedValue);
            string manufacturerId = auth.getUserId(dlManufacturer.SelectedValue, "Manufacturers");

            string stockQty = manufacturer.getcurrentStock(blanketId, manufacturerId);

            lblQuantity.Text = "Current Stock: " + stockQty;

        }
    }
}