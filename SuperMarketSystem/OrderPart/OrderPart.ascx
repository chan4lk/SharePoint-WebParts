<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderPart.ascx.cs" Inherits="SuperMarketSystem.OrderPart.OrderPart" %>
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

    #Sp-OrderPart table td{
        text-align: center;
    }

    #Sp-OrderPart .btn {
        background: #D1DBEA;
        margin: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        border-radius: 5px;
    }

    #Sp-OrderPart input[type=text] {
        width: 96%;
        margin: 2px auto;
        padding-right: 5px;
    }

    #Sp-OrderPart #SubmitButton{
        float:right;
    }
</style>
<div id="Sp-OrderPart">
    <div class="well">
        <h1>Place an Order</h1>
        <asp:Button CssClass="btn" ID="ClearButton" runat="server" OnClick="ClearButtonClick" Text="New" />
        <asp:GridView ID="orderView" runat="server" ShowFooter="true" AutoGenerateColumns="false" OnRowCreated="OnRowCreated">
            <Columns>
                <asp:TemplateField HeaderText="Product ID">
                    <ItemTemplate>
                        <%# Eval("ProductId") %>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList
                            DataTextField="ProductId"
                            DataSourceID="ProductIDs"
                            runat="server" />
                    </InsertItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="ProductIdText" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <%# Eval("Quantity") %>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="QuantityText" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total"
                    ItemStyle-HorizontalAlign="Right"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Eval("Total", "{0:c}") %>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="AddButton"
                            runat="server"
                            OnClick="AddButton_Click" Text="Add"/>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Button CssClass="btn" ID="SubmitButton" runat="server" OnClick="SubmitButtonClick" Text="Submit" />

    </div>
</div>
