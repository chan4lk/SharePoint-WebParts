﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderWebPart.ascx.cs" Inherits="SuperMarketSystem.OrderPart.OrderWebPart" %>
<style>
    #Sp-OrderPart .well {
        background: #F0F1E1;
        padding: 5px 5px;
        margin: 0 auto;
    }

    #Sp-OrderPart h1 {
        color: #ff6a00;
        text-align: center;
    }

    #Sp-OrderPart table {
        width: 100%;
    }

    #Sp-OrderPart table td {
        text-align: center;
    }

    #Sp-OrderPart table th {
            width: 33%
    }

    #Sp-OrderPart .btn {
        background: #D1DBEA;
        margin-top: 5px;
        margin-bottom: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        border-radius: 5px;
    }

    #Sp-OrderPart input[type=text] {
        width: 96%;
        margin: 5px auto;
        padding-right: 5px;
        max-width: 90%;
    }

    #Sp-OrderPart .submit {
        margin: 0 auto !Important;
        display: block;
        position: relative;
    }

    #Sp-OrderPart .col {
        display: block;
        float: left;
    }

    #Sp-OrderPart .msg {
        height: 100%;
    }

    #Sp-OrderPart .info {
        color: #000;
    }

    #Sp-OrderPart .err {
        color: #f00;
    }
</style>
<div id="Sp-OrderPart" data-onkeydown="return (event.keyCode!=13)">
    <div class="well">
        <h1>Place an Order</h1>
        <div>
            <asp:Button CssClass="btn" ID="ClearButton" runat="server" OnClick="OnClearButtonClick" Text="New" />
        </div>
        <div>
            <asp:GridView
                ID="orderView"
                runat="server"
                ShowFooter="true"
                ShowHeaderWhenEmpty="true"
                AutoGenerateColumns="false"
                OnRowDataBound="OnRowDataBound"
                OnRowCreated="OnRowCreated">
                <Columns>
                    <asp:TemplateField HeaderText="Product ID">
                        <ItemTemplate>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Bind("ProductId") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="ProductIdText" runat="server" Text="0"></asp:TextBox>
                            <br />
                            <label>Total</label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="QuantityLabel" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="QuantityText" runat="server" Text="0" AutoPostBack="true"></asp:TextBox>
                            <br />
                            <br />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <%# Eval("Total", "{0:c}") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <br />
                            <br />
                            <asp:Label runat="server" ID="TotalLabel"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="display: flex; margin-top: 10px; min-height:50px">
            <div class="col" style="width: 67%">
                <asp:Label ID="MessageLabel" CssClass="msg" runat="server"></asp:Label>
            </div>
            <div class="col" style="width: 33%;">
                <asp:Button CssClass="btn submit" ID="SubmitButton" runat="server" OnClick="OnSubmitButtonClick" Text="Submit" />
            </div>
        </div>
        <hr>
    </div>
</div>
