<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestStock.aspx.cs" Inherits="ClientWebApplication.Distributor.RequestStock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Update Stock</title>
    <link href="Distributor.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <!-- Sidebar -->
            <nav class="sidebar">
                <div class="sidebar-header">
                    <h2>Cozy Comfort</h2>
                    <p>Distributor Dashboard</p>
                </div>
                <div class="sidebar-menu">
                    <a href="DistributorInventory.aspx" class="menu-item ">
                        <i>📋</i> View Inventory
                    </a>
                    <a href="SellerOrders.aspx" class="menu-item ">
                        <i>📦</i> Seller Orders Request
                    </a>
                    <a href="RequestStock.aspx" class="menu-item active">
                        <i>🔄</i> Request New Stock
                    </a>
                    <a href="DistributorOrders.aspx" class="menu-item">
                        <i>📋</i>My Orders
                    </a>
                </div>
            </nav>

            <!-- Main Content -->
            <main class="main-content">
                <div class="content-header">
                    <h1>Request Stock to Inventory</h1>
                    <p>Request new stock quantities to your inventory</p>
                </div>

                <div class="content-body">

                    <div class="form-group">
                        <label for="ddlStockModel">Blanket Model</label>
                        <asp:DropDownList ID="ddlStockModel" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label for="txtStockQuantity">Quantity You Needed </label>
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Enter Quantity Needed "></asp:TextBox>
                    </div>


                    <div class="form-group">
                        <asp:Button ID="btnupdateModel" runat="server" Text="Send Request" CssClass="btn btn-primary" OnClick="btnupdateModel_Click" />
                        <asp:Button ID="btnCancelModel" runat="server" Text="Cancel" CssClass="btn btn-secondary" />
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
