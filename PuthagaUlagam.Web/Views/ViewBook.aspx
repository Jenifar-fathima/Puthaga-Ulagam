<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBook.aspx.cs" Inherits="PuthagaUlagam.ViewBook" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="view-table">
        <div class="page-title">
            <h1>Book List</h1>
        </div>
        <br />
        <div class="table-button">
            <asp:GridView ID="TableBooks" runat="server" AutoGenerateColumns="False" DataKeyNames="BookISBN">
                <Columns>
                    <asp:BoundField DataField="BookID" HeaderText="ID" />
                    <asp:BoundField DataField="BookName" HeaderText="Title" />
                    <asp:BoundField DataField="BookAuthor" HeaderText="Author" />
                    <asp:BoundField DataField="BookISBN" HeaderText="ISBN" />
                    <asp:BoundField DataField="BookPrice" HeaderText="Price" />
                    <asp:BoundField DataField="DateOfPublication" HeaderText="Date" />
                    <asp:BoundField DataField="BookCount" HeaderText="Count" />
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:Button ID="btn_Edit" runat="server" Text="Edit" OnClick="EditBtn" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this book?');" OnClick="DeleteBtn" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>