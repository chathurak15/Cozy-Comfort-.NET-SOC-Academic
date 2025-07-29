<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SellerRequests.aspx.cs" Inherits="ClientWebApplication.Seller.SellerRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - My Orders</title>
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
            <!-- Sidebar -->
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
                    <a href="CustomerOrders.aspx" class="menu-item ">
                        <i>📦</i> Customer Orders
                    </a>
                    <a href="RequestFromDistributor.aspx" class="menu-item ">
                        <i>🔄</i> Request New Stock
                    </a>
                    <a href="SellerRequests.aspx" class="menu-item active">
                        <i>📋</i>My Stock Requests
                    </a>
                </div>
            </nav>

            <!-- Main Content -->
            <main class="main-content">
                <div class="content-header">
                    <h1>My stock Request (Orders)</h1>
                    <p>Complete view of My order requests</p>
                </div>

                <div class="content-body">
                    <div style="margin-top: 20px;">
                        <asp:Button ID="btnRefreshInventory" runat="server" Text="Refresh Data" CssClass="btn btn-primary" OnClick="btnRefreshInventory_Click" />
                    </div>
                    <div class="table-container">
                        <asp:GridView ID="gvMyOrders" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvMyOrders_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="RequestId" HeaderText="ID" />
                                <asp:BoundField DataField="ModelId" HeaderText="Blanket ID" />
                                <asp:BoundField DataField="ModelName" HeaderText="Blanket Name" />
                                <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                                <asp:BoundField DataField="Distributor" HeaderText="Distributor" />
                                <asp:BoundField DataField="Phone" HeaderText="Distributor Contact" />
                                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />

                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnApprove" runat="server" Text="Complete"
                                                CssClass="btn btn-approve"
                                                CommandName="complete"
                                                CommandArgument='<%# Eval("RequestId") %>'
                                                OnClientClick="return confirm('Are you sure you want to complete this order?');" />

                                            <asp:Button ID="btnDelete" runat="server" Text="Delete"
                                                CssClass="btn btn-reject"
                                                CommandName="deleteOrder"
                                                CommandArgument='<%# Eval("RequestId") %>'
                                                OnClientClick="return confirm('Are you sure you want to delete this order?');" />
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

