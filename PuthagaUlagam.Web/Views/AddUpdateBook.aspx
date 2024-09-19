<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUpdateBook.aspx.cs" Inherits="PuthagaUlagam.AddUpdateBook" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Add or Update Book</h1>
    <div>
        <div>
            <div>
                <asp:TextBox ID="txtTitle" runat="server" Placeholder="Enter Title" AutoComplete="off" /><br />
            </div>
            <div>
                <asp:TextBox ID="txtAuthor" runat="server" Placeholder="Enter Author" AutoComplete="off" /><br />
            </div>
            <div>
                <asp:TextBox ID="txtISBN" runat="server" Placeholder="Enter ISBN" AutoComplete="off" /><br />
            </div>
            <div>
                <asp:TextBox ID="txtPrice" runat="server" Placeholder="Enter Price" AutoComplete="off" /><br />
            </div>
            <div>
                <asp:Button ID="PickTheDateOfPublication" runat="server" Text="Pick Date" OnClick="PickTheDateOfPublication_Click" />
                <asp:Calendar ID="DateOfPublication" runat="server" Visible="false" OnSelectionChanged="Calender_date_changed" />
                <asp:Label ID="DisplayDate" runat="server"></asp:Label>
            </div>
            <div>
                <asp:TextBox ID="txtBookCount" runat="server" Placeholder="Enter Book Count" AutoComplete="off" /><br />
            </div>
        </div>

        <div>
            <asp:Button ID="btnAdd" runat="server" Text="Add Book" OnClick="btnAdd_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update Book" OnClick="btnUpdate_Click" />
        </div>
        <div>
            <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>