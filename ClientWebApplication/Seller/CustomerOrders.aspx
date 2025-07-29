<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerOrders.aspx.cs" Inherits="ClientWebApplication.Seller.CustomerOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Customer Orders</title>
    <link href="Seller.css" rel="stylesheet" type="text/css" />
    <style>
        .table {
            width: 100%;
            border-collapse: collapse;
            border-radius: 10px;
            overflow: hidden;
        }

            .table th {
                background-color: #0c4401;
                color: white;
                padding: 12px 8px;
                font-weight: 700;
                text-align: center;
            }

            .table td {
                padding: 12px 10px;
                text-align: center;
                border-bottom: 1px solid #e0e0e0;
                font-size: 15px;
            }

            .table tr:nth-child(even) {
                background-color: #f9f9f9;
            }

        .action-buttons {
            display: flex;
            justify-content: center;
            gap: 5px;
        }

            .action-buttons .btn {
                padding: 6px 12px;
                border: none;
                border-radius: 6px;
                cursor: pointer;
                font-size: 15px;
                font-weight: 600;
            }

        .btn-approve {
            background-color: #0c4401;
            color: white;
        }

        .btn-reject {
            background-color: #f44336;
            color: white;
        }
    </style>
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
                    <a href="PlaceOrder.aspx" class="menu-item ">
                        <i>📋</i> Place Order
                    </a>
                    <a href="SellerInventory.aspx" class="menu-item ">
                        <i>📋</i> View Inventory
                    </a>
                    <a href="CustomerOrders.aspx" class="menu-item active">
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
                    <h1>Customer Orders</h1>
                    <p>Complete view of customer orders</p>
                </div>

                <div class="content-body">
                    <div style="margin-top: 20px;">
                        <asp:Button ID="btnRefreshInventory" runat="server" Text="Refresh Data" CssClass="btn btn-primary" OnClick="btnRefreshInventory_Click"  />
                    </div>
                    <div class="table-container">
                        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvOrders_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="OrderId" HeaderText="ID" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                 <asp:BoundField DataField="Phone" HeaderText="Customer Contact" />
                                <asp:BoundField DataField="ModelId" HeaderText="Blanket ID" />
                                <asp:BoundField DataField="ModelName" HeaderText="Blanket Name" />
                                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="Seller" HeaderText="Seller Name" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />

                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnApprove" runat="server" Text="Complete"
                                                CssClass="btn btn-approve"
                                                CommandName="Complete"
                                                CommandArgument='<%# Eval("OrderId") %>'
                                                OnClientClick="return confirm('Are you sure you want to Complete this order?');" />

                                            <asp:Button ID="btnReject" runat="server" Text="Delete"
                                                CssClass="btn btn-reject"
                                                CommandName="Delete"
                                                CommandArgument='<%# Eval("OrderId") %>'
                                                OnClientClick="return confirm('Are you sure you want to Delete this order?');" />
                                        </div>
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
