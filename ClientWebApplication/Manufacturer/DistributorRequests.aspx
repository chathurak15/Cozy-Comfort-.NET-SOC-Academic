<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributorRequests.aspx.cs" Inherits="ClientWebApplication.Manufacturer.DistributorRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Distributor Orders</title>
    <link href="Manufacturer.css" rel="stylesheet" type="text/css" />
    <style>
        .table {
            width: 100%;
            border-collapse: collapse;
            border-radius: 10px;
            overflow: hidden;
        }

            .table th {
                background-color: #444;
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
            background-color: #4CAF50;
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
                    <a href="UpdateStock.aspx" class="menu-item">
                        <i>📦</i> Update Stock 
                    </a>
                    <a href="DistributorRequests.aspx" class="menu-item active">
                        <i>📨</i> Distributor Request
                    </a>
                </div>
            </nav>

            <!-- Main Content -->
            <main class="main-content">
                <div class="content-header">
                    <h1>Distributor Order Requests</h1>
                    <p>Complete view of Distributor order request</p>
                </div>

                <div class="content-body">
                    <div style="margin-top: 20px;">
                        <asp:Button ID="btnRefreshInventory" runat="server" Text="Refresh Data" CssClass="btn btn-primary" OnClick="btnRefreshInventory_Click" />
                    </div>
                    <div class="table-container">
                        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvOrders_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="RequestId" HeaderText="ID" />
                                <asp:BoundField DataField="Distributor" HeaderText="Distributor" />
                                <asp:BoundField DataField="ModelId" HeaderText="Blanket ID" />
                                <asp:BoundField DataField="ModelName" HeaderText="Blanket Name" />
                                <asp:BoundField DataField="RequestedQty" HeaderText="Qty" />
                                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="Phone" HeaderText="Distributor Contact" />
                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />

                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve"
                                                CssClass="btn btn-approve"
                                                CommandName="Approve"
                                                CommandArgument='<%# Eval("RequestId") %>'
                                                OnClientClick="return confirm('Are you sure you want to approve this order?');" />

                                            <asp:Button ID="btnReject" runat="server" Text="Reject"
                                                CssClass="btn btn-reject"
                                                CommandName="Reject"
                                                CommandArgument='<%# Eval("RequestId") %>'
                                                OnClientClick="return confirm('Are you sure you want to reject this order?');" />
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
