using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication.Seller
{
    public partial class PlaceOrder : System.Web.UI.Page
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
            }
        }

        protected void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (ddlStockModel.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtCustomer.Text) || string.IsNullOrWhiteSpace(txtQty.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('All fields are required.');", true);
                return;
            }

            int modelId = Int32.Parse(manufacturer.getBlanketId(ddlStockModel.SelectedValue));
            int qty = Int32.Parse(txtQty.Text);
            string customerName = txtCustomer.Text;
            string phone = txtPhone.Text;
            int sellerId = 1; // Hardcoded

            string result = seller.addCustomerOrder(modelId,customerName,phone??"",qty,sellerId) ;

            if (result == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Customer Order Placed successfully!');", true);
                ddlStockModel.SelectedIndex = -1;
                txtQty.Text = "";
                txtCustomer.Text = "";
                txtPhone.Text = "";
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Request failed: {result}');", true);
            }

        }

        protected void btnCancelModel_Click(object sender, EventArgs e)
        {
            ddlStockModel.SelectedIndex = -1;
            txtQty.Text = "";
            txtCustomer.Text = "";
            txtPhone.Text = "";
        }
    }
}