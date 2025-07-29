<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributorInventory.aspx.cs" Inherits="ClientWebApplication.Distributor.DistributorInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Distributor Inventory</title>
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
                    <a href="DistributorInventory.aspx" class="menu-item active">
                        <i>📋</i> View Inventory
                    </a>
                    <a href="SellerOrders.aspx" class="menu-item ">
                        <i>📦</i> Seller Orders Request
                    </a>
                    <a href="RequestStock.aspx" class="menu-item">
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
                    <h1>Distributor Inventory Overview</h1>
                    <p>Complete view of your inventory</p>
                </div>

                <div class="content-body">
                    <div style="margin-top: 20px;">
                        <asp:Button ID="btnRefreshInventory" runat="server" Text="Refresh Data" CssClass="btn btn-primary" OnClick="btnRefreshInventory_Click" />
                    </div>
                    <div class="table-container">
                        <asp:GridView ID="gvDInventory" runat="server" CssClass="table" AutoGenerateColumns="False"
                            GridLines="None" CellPadding="0">
                            <HeaderStyle CssClass="table-header" />
                            <RowStyle CssClass="table-row" />
                            <AlternatingRowStyle CssClass="table-row-alt" />
                            <Columns>
                                <asp:BoundField DataField="ModelId" HeaderText="Model ID" />
                                <asp:BoundField DataField="Name" HeaderText="Model Name" />
                                <asp:BoundField DataField="Material" HeaderText="Material" />
                                <asp:BoundField DataField="Size" HeaderText="Size" />
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
