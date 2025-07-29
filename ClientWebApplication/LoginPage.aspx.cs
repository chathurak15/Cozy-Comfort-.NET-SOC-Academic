using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication
{
    public partial class LoginPage : System.Web.UI.Page
    {

        AuthServiceReference.AuthWebServiceSoapClient auth = new AuthServiceReference.AuthWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (auth != null)
            {
                string result = auth.Login(txtUsername.Text, txtPassword.Text);
                
                if (result == "Login Sucees!") {
                    checkUserRole(auth.getRole(txtUsername.Text));
                }
                else
                {
                    lblError.Text = result;
                }
            }
            else
            {
                lblError.Text = "Service error";
            }
        }

        //check user role and redirect dashboards
        protected void checkUserRole(string role)
        {
            if(role == "Manufacturer")
            {
                Response.Redirect("Manufacturer/ViewInventory.aspx");
            }
            else if(role == "Distributor")
            {
                Response.Redirect("Distributor/DistributorInventory.aspx");
            }
            else if (role == "Seller")
            {
                Response.Redirect("Seller/PlaceOrder.aspx");
            }
        }

    }
}