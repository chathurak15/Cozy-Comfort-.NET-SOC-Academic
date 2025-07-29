<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBlanket.aspx.cs" Inherits="ClientWebApplication.AddBlanket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Add Blanket Model</title>
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
                    <a href="AddBlanket.aspx" class="menu-item active">
                        <i>🧵</i> Add Blanket Model 
                    </a>
                    <a href="AddStock.aspx" class="menu-item">
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
                    <h1>Add New Blanket Model</h1>
                    <p>Create a new blanket model in your product catalog</p>
                </div>

                <div class="content-body">
                    <div class="form-row">
                        <div class="form-group">
                            <label for="txtModelName">Model Name</label>
                            <asp:TextBox ID="txtModelName" runat="server" CssClass="form-control" placeholder="Enter blanket model name"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="ddlMaterial">Material</label>
                            <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Material" Value=""></asp:ListItem>
                                <asp:ListItem Text="Cotton" Value="Cotton"></asp:ListItem>
                                <asp:ListItem Text="Wool" Value="Wool"></asp:ListItem>
                                <asp:ListItem Text="Cashmere" Value="Cashmere"></asp:ListItem>
                                <asp:ListItem Text="Fleece" Value="Fleece"></asp:ListItem>
                                <asp:ListItem Text="Synthetic Blend" Value="Synthetic"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group">
                            <label for="ddlSize">Size</label>
                            <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Size" Value=""></asp:ListItem>
                                <asp:ListItem Text="Twin" Value="Twin"></asp:ListItem>
                                <asp:ListItem Text="Full" Value="Full"></asp:ListItem>
                                <asp:ListItem Text="Queen" Value="Queen"></asp:ListItem>
                                <asp:ListItem Text="King" Value="King"></asp:ListItem>
                                <asp:ListItem Text="California King" Value="CalKing"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtColor">Color</label>
                            <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" placeholder="Enter color"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="txtPrice">Price (LKR)</label>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter price"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnAddModel" runat="server" Text="Add Blanket Model" CssClass="btn btn-primary" OnClick="btnAddModel_Click" />
                        <asp:Button ID="btnCancelModel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelModel_Click" />
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
