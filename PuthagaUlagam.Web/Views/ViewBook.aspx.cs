using PuthagaUlagam.Logic;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PuthagaUlagam
{
    public partial class ViewBook : Page
    {
        BookOperationBL operationBL = new BookOperationBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBooks();
            }
        }

        private void LoadBooks()
        {
            TableBooks.DataSource = operationBL.GetBooks();
            TableBooks.DataBind();
        }

        protected void EditBtn(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;
            Session["RowIndex"] = rowIndex;
            Response.Redirect("AddUpdateBook.aspx");
        }

        protected void DeleteBtn(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;

            operationBL.DeleteBook(rowIndex);
            LoadBooks();
        }
    }
}