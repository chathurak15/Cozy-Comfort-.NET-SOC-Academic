<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewInventory.aspx.cs" Inherits="ClientWebApplication.Manufacturer.ViewInventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Manufacturer Inventory</title>
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
                    <a href="ViewInventory.aspx" class="menu-item active">
                        <i>📋</i> View Inventory 
                    </a>
                    <a href="UpdateStock.aspx" class="menu-item">
                        <i>📦</i> Update Stock 
                    </a>
                    <a href="DistributorRequests.aspx" class="menu-item">
                        <i>📨</i> Distributor Request
                    </a>
                </div>
            </nav>

            <!-- Main Content -->
            <main class="main-content">
                <div class="content-header">
                    <h1>Inventory Overview</h1>
                    <p>Complete view of your manufacturing inventory</p>
                </div>

                <div class="content-body">
                    <div style="margin-top: 20px;">
                        <asp:Button ID="btnRefreshInventory" runat="server" Text="Refresh Data" CssClass="btn btn-primary" OnClick="btnRefreshInventory_Click" />
                    </div>
                    <div class="table-container">
                        <asp:GridView ID="gvInventory" runat="server" CssClass="table" AutoGenerateColumns="False"
                            GridLines="None" CellPadding="0" OnRowCommand="gvInventory_RowCommand">
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
                                <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete"
                                            CommandName="DeleteRow"
                                            CommandArgument='<%# Eval("ModelId") %>'
                                            CssClass="btn btn-danger"
                                            OnClientClick="return confirm('Are you sure you want to delete this blanket?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
