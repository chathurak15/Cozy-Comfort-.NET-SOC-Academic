using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ClientWebApplication.Distributor
{
    public partial class RequestStock : System.Web.UI.Page
    {
        DistributorServiceReference.DistributorWebServiceSoapClient distributor = new DistributorServiceReference.DistributorWebServiceSoapClient();
        // Create a instance to access Manufacturer web service methods
        ManufacturerServiceReference.ManufacturerWebServiceSoapClient manufacturer = new ManufacturerServiceReference.ManufacturerWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dataSet = manufacturer.getBlanketName();
                ddlStockModel.DataSource = dataSet;
                ddlStockModel.DataBind();
                ddlStockModel.DataValueField = "Name";
                ddlStockModel.DataBind();

            }
        }

        protected void btnupdateModel_Click(object sender, EventArgs e)
        {
            if (ddlStockModel.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All fields are required.');", true);
                return;
            }

            int modelId = Int32.Parse(manufacturer.getBlanketId(ddlStockModel.SelectedValue));
            int qty = Int32.Parse(txtQuantity.Text);
            int distributorId = 1; // Hardcoded

            // Call distributor web service to request stock
            string result = distributor.addDistributorStock(modelId, qty, distributorId);

            if (result == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Stock request submitted successfully!');", true);
                ddlStockModel.SelectedIndex = -1;
                txtQuantity.Text = "";
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Request failed: {result}');", true);
            }
        }
    }
}