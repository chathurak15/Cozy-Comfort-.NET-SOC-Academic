using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientWebApplication
{
    public partial class AddBlanket : System.Web.UI.Page
    {
        // Create a instance to access Manufacturer web service methods
        ManufacturerServiceReference.ManufacturerWebServiceSoapClient manufacturer = new ManufacturerServiceReference.ManufacturerWebServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddModel_Click(object sender, EventArgs e)
        {
            string name = txtModelName.Text;
            string material = ddlMaterial.SelectedValue;
            string size = ddlSize.SelectedValue;
            string color = txtColor.Text;
            int price = Int32.Parse(txtPrice.Text);

            string result = manufacturer.insertBlanketInfo(name,material,price,size,color);
            
            if (result == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Successfully Added!');", true);

                //Clear form fields after successful submission
                txtModelName.Text = "";
                ddlMaterial.SelectedIndex = 0;
                ddlSize.SelectedIndex = 0;
                txtColor.Text = "";
                txtPrice.Text = "";
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{result}');", true);
            }

        }

        //cancel btn 
        protected void btnCancelModel_Click(object sender, EventArgs e)
        {
            txtModelName.Text = "";
            ddlMaterial.SelectedIndex = 0;
            ddlSize.SelectedIndex = 0;
            txtColor.Text = "";
            txtPrice.Text = "";
        }
    }
}