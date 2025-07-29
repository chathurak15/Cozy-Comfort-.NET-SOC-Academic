<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateStock.aspx.cs" Inherits="ClientWebApplication.Manufacturer.UpdateStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Update Stock</title>
    <link href="Manufacturer.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <!-- Sidebar -->
            <nav class="sidebar">
                <div class="sidebar-header">
                    <h2>Cozy Comfort</h2>
                    <p>Manufacturer Dashboard</p>
                </div>
                <div class="sidebar-menu">
                    <a href="AddBlanket.aspx" class="menu-item">
                        <i>🧵</i> Add Blanket Model 
                    </a>
                    <a href="AddStock.aspx" class="menu-item">
                        <i>➕</i> Add Stock 
                    </a>
                    <a href="ViewInventory.aspx" class="menu-item">
                        <i>📋</i> View Inventory 
                    </a>
                    <a href="UpdateStock.aspx" class="menu-item active">
                        <i>📦</i> Update Stock 
                    </a>
                    <a href="DistributorRequests.aspx" class="menu-item ">
                        <i>📨</i> Distributor Request
                    </a>
                </div>
            </nav>

            <!-- Main Content -->
            <main class="main-content">
                <div class="content-header">
                    <h1>Update Stock</h1>
                    <p>Add new stock quantities to your manufacturing inventory</p>
                </div>

                <div class="content-body">

                    <div class="form-group">
                        <label for="ddlStockModel">Blanket Model</label>
                        <asp:DropDownList ID="ddlStockModel" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="ddlStockModel">Manufacturer </label>
                            <asp:DropDownList ID="dlManufacturer" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtStockQuantity">Current Stock </label>
                            <asp:TextBox ID="lblQuantity" runat="server" CssClass="form-control" placeholder="0 " ReadOnly></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btncurrentStock" runat="server" Text="Check Current Stock" CssClass="btn btn-secondary" OnClick="btncurrentStock_Click" />
                    </div>
                    <hr />
                    <br />
                    <div class="form-group">
                        <label for="txtStockQuantity">Update Stock (Total Stock count) </label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Enter Total quantity "></asp:TextBox>
                    </div>


                    <div class="form-group">
                        <asp:Button ID="btnupdateModel" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnupdateModel_Click" />
                        <asp:Button ID="btnCancelModel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelModel_Click" />
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
