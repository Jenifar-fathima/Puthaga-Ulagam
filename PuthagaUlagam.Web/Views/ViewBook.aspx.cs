using PuthagaUlagam.Common;
using PuthagaUlagam.Logic;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PuthagaUlagam
{
    public partial class ViewBook : Page
    {
        private readonly BookOperationBL operationBL = new BookOperationBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBooks();
            }
        }
        public void LoadBooks()
        {
            TableBooks.DataSource = operationBL.GetBooks();
            TableBooks.DataBind();
        }
        protected void EditBtn(object sender, EventArgs e)
        {
            HandleBookButton(sender, e, OperationType.Update);
        }

        protected void DeleteBtn(object sender, EventArgs e)
        {
            HandleBookButton(sender, e, OperationType.Delete);
        }

        protected void HandleBookButton(object sender, EventArgs e, OperationType operationType)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;

            var books = operationBL.GetBooks();
            int isbn = Convert.ToInt32(books.Rows[rowIndex]["BookISBN"]);

            if (operationType == OperationType.Update)
            {
                Session["bookISBN"] = isbn;
                Response.Redirect("AddUpdateBook.aspx");
            }
            else if (operationType == OperationType.Delete)
            {
                operationBL.DeleteBook(isbn);
                LoadBooks();
            }
        }
    }
}