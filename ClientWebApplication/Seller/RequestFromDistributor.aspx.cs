using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Seller
{
    public partial class RequestFromDistributor : System.Web.UI.Page
    {
        // Create a instance to access Manufacturer web service methods
        ManufacturerServiceReference.ManufacturerWebServiceSoapClient manufacturer = new ManufacturerServiceReference.ManufacturerWebServiceSoapClient();

        // Create a instance to access Auth web service methods
        AuthServiceReference.AuthWebServiceSoapClient auth = new AuthServiceReference.AuthWebServiceSoapClient();

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

                DataSet Distributorsds = auth.getUserName("Distributors");
                ddlDistributor.DataSource = Distributorsds;
                ddlDistributor.DataBind();
                ddlDistributor.DataValueField = "Name";
                ddlDistributor.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlStockModel.SelectedIndex == -1 || ddlDistributor.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All fields are required.');", true);
                return;
            }

            int qty = Int32.Parse(txtQuantity.Text);
            int modelId = Int32.Parse(manufacturer.getBlanketId(ddlStockModel.SelectedValue));
            int distributor = Int32.Parse(auth.getUserId(ddlDistributor.SelectedValue,"Distributors"));
            int sellerId = 1; //hardcode

            string result = seller.addSellerDistributorRequest(sellerId, distributor, modelId, qty);

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
        protected void btnCancelModel_Click(object sender, EventArgs e)
        {
            txtQuantity.Text = "";
            ddlStockModel.SelectedIndex = -1;
            ddlDistributor.SelectedIndex = -1;
        }
    }
}