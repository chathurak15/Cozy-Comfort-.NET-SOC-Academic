<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlaceOrder.aspx.cs" Inherits="ClientWebApplication.Seller.PlaceOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Place Customer Order</title>
    <link href="seller.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <nav class="sidebar">
                <div class="sidebar-header">
                    <h2>Cozy Comfort</h2>
                    <p>Seller Dashboard</p>
                </div>
                <div class="sidebar-menu">
                    <a href="PlaceOrder.aspx" class="menu-item active">
                        <i>📋</i> Place Order
                    </a>
                    <a href="SellerInventory.aspx" class="menu-item ">
                        <i>📋</i> View Inventory
                    </a>
                    <a href="CustomerOrders.aspx" class="menu-item ">
                        <i>📦</i> Customer Orders
                    </a>
                    <a href="RequestFromDistributor.aspx" class="menu-item ">
                        <i>🔄</i> Request New Stock
                    </a>
                    <a href="SellerRequests.aspx" class="menu-item ">
                        <i>📋</i>My Stock Requests
                    </a>
                </div>
            </nav>

            <!-- Main Content -->
            <main class="main-content">
                <div class="content-header">
                    <h1>Place Custome Order</h1>
                    <p>Create a new blanket model in your product catalog</p>
                </div>

                <div class="content-body">
                    <div class="form-group">
                        <label for="ddlStockModel">Blanket Model</label>
                        <asp:DropDownList ID="ddlStockModel" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-row">
                        <div class="form-group">
                            <label for="txtname">Customer Name</label>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" placeholder="Customer Name"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtPrice">Customer Contact</label>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone Number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtQty">Qty</label>
                        <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" placeholder="Enter Quantity "></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnAddOrder" runat="server" Text="Place Order" CssClass="btn btn-primary" OnClick="btnAddOrder_Click" />
                        <asp:Button ID="btnCancelModel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelModel_Click" />
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
