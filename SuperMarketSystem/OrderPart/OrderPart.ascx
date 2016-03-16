<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderPart.ascx.cs" Inherits="SuperMarketSystem.OrderPart.OrderPart" %>
<style>
    #Sp-OrderPart .well{
        background:#b6ff00;
        padding: 5px 5px;
        margin:0 auto;
    }

    #Sp-OrderPart h1{
        color: #ff6a00;
        text-align: center;
    }

    #Sp-OrderPart .btn{
        background: #2863c1;
    }
</style>
<div id="Sp-OrderPart">
    <div class="well">
        <h1>Place an Order</h1>
        <asp:GridView ID="orderView" runat="server"></asp:GridView>

        <asp:Button CssClass="btn" ID="SubmitButton" runat="server" OnClick="SubmitButtonClick" Text="Submit" />
        <asp:Button CssClass="btn" ID="AddButton" runat="server" OnClick="AddButtonClick" Text="Add" />
    </div>
</div>
