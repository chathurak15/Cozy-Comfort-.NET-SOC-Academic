<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellerInventory.aspx.cs" Inherits="ClientWebApplication.Seller.SellerInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Seller Inventory</title>
    <link href="Seller.css" rel="stylesheet" type="text/css" />
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
                    <a href="PlaceOrder.aspx" class="menu-item ">
                        <i>📋</i> Place Order
                    </a>
                    <a href="SellerInventory.aspx" class="menu-item active ">
                        <i>📋</i> View Inventory
                    </a>
                    <a href="CustomerOrders.aspx" class="menu-item ">
                        <i>📦</i> Customer Orders
                    </a>
                    <a href="RequestFromDistributor.aspx" class="menu-item ">
                        <i>🔄</i> Request New Stock
                    </a>
                    <a href="SellerRequests.aspx" class="menu-item">
                        <i>📋</i>My Stock Requests
                    </a>
                </div>
            </nav>

            <!-- Main Content -->
            <main class="main-content">
                <div class="content-header">
                    <h1>Distributor Inventory Overview</h1>
                    <p>Complete view of your inventory</p>
                </div>

                <div class="content-body">
                    <div class="form-row">
                        <div style="margin-top: 20px;" class="form-group">
                            <asp:Button ID="btnRefreshInventory" runat="server" Text="Refresh Data" CssClass="btn btn-secondary" OnClick="btnRefreshInventory_Click" />
                        </div>
                        <div class="form-row">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlStockModel" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="">
                                <asp:Button ID="btnSearch" runat="server" Text="Search Blanket" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="table-container">
                        <asp:GridView ID="gvSInventory" runat="server" CssClass="table" AutoGenerateColumns="False"
                            GridLines="None" CellPadding="0">
                            <HeaderStyle CssClass="table-header" />
                            <RowStyle CssClass="table-row" />
                            <AlternatingRowStyle CssClass="table-row-alt" />
                            <Columns>
                                <asp:BoundField DataField="ModelId" HeaderText="Model ID" />
                                <asp:BoundField DataField="Name" HeaderText="Model Name" />
                                <asp:BoundField DataField="Material" HeaderText="Material" />
                                <asp:BoundField DataField="Size" HeaderText="Size" />
                                <asp:BoundField DataField="Price" HeaderText="Price" />
                                <asp:BoundField DataField="Color" HeaderText="Color" />
                                <asp:BoundField DataField="Quantity" HeaderText="Current Stock" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Eval("Quantity")) > 0 ? "In Stock" : "Out of Stock" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" DataFormatString="{0:yyyy-MM-dd}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
