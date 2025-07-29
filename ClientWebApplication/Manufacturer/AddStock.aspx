<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStock.aspx.cs" Inherits="ClientWebApplication.Manufacturer.AddStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Add Stock</title>
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
                    <a href="AddStock.aspx" class="menu-item active">
                        <i>➕</i> Add Stock 
                    </a>
                    <a href="ViewInventory.aspx" class="menu-item">
                        <i>📋</i> View Inventory 
                    </a>
                    <a href="UpdateStock.aspx" class="menu-item">
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
                    <h1>Add Stock to Inventory</h1>
                    <p>Add new stock quantities to your manufacturing inventory</p>
                </div>

                <div class="content-body">

                    <div class="form-group">
                        <label for="ddlStockModel">Blanket Model</label>
                        <asp:DropDownList ID="ddlStockModel" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtStockQuantity">Quantity to Add</label>
                        <asp:TextBox ID="txtStockQuantity" runat="server" CssClass="form-control" placeholder="Enter quantity"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="ddlStockModel">Manufacturer </label>
                        <asp:DropDownList ID="dlManufacturer" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnAddModel" runat="server" Text="Add Blanket Stock" CssClass="btn btn-primary" OnClick="btnAddModel_Click" />
                        <asp:Button ID="btnCancelModel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelModel_Click" />
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
