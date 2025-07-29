<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManufacturerDashboard.aspx.cs" Inherits="ClientWebApplication.ManufacturerDashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cozy Comfort - Manufacturer Dashboard</title>
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
                    <a href="ViewInventory.aspx" class="menu-item">
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
                    <h1>Dashboard Overview</h1>
                    <p>Welcome to your manufacturer control center</p>
                </div>

                <div class="stats-grid">
                    <div class="stat-card">
                        <h3>247</h3>
                        <p>Total Blanket Models</p>
                    </div>
                    <div class="stat-card">
                        <h3>12,450</h3>
                        <p>Total Items in Stock</p>
                    </div>
                    <div class="stat-card">
                        <h3>89</h3>
                        <p>Active Distributors</p>
                    </div>
                    <div class="stat-card">
                        <h3>1,234</h3>
                        <p>Orders This Month</p>
                    </div>
                </div>

                <div class="content-body">
                    <h2>Recent Activity</h2>
                    <div class="table-container">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Activity</th>
                                    <th>Blanket Model</th>
                                    <th>Quantity</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>2024-06-23</td>
                                    <td>Stock Added</td>
                                    <td>Premium Wool Blanket</td>
                                    <td>150</td>
                                    <td>Completed</td>
                                </tr>
                                <tr>
                                    <td>2024-06-22</td>
                                    <td>New Model Added</td>
                                    <td>Cashmere Deluxe</td>
                                    <td>-</td>
                                    <td>Completed</td>
                                </tr>
                                <tr>
                                    <td>2024-06-22</td>
                                    <td>Stock Updated</td>
                                    <td>Cotton Comfort</td>
                                    <td>200</td>
                                    <td>Completed</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
