<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBook.aspx.cs" Inherits="PuthagaUlagam.ViewBook" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Book List</h1>
    <asp:GridView ID="TableBooks" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="ISBN" HeaderText="Book ISBN" />
            <asp:BoundField DataField="Price" HeaderText="Price" />
            <asp:BoundField DataField="Date" HeaderText="Date" />
            <asp:BoundField DataField="Count" HeaderText="Book count" />
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:Button ID="btn_Edit" runat="server" Text="Edit" OnClick="EditBtn"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="DeleteBtn"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>