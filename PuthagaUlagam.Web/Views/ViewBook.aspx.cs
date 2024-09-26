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

        private void LoadBooks()
        {
            TableBooks.DataSource = operationBL.GetBooks();
            TableBooks.DataBind();
        }

        protected void EditBtn(object sender, EventArgs e)
        {
            HandleBookButton(sender, OperationType.Update);
        }

        protected void DeleteBtn(object sender, EventArgs e)
        {
            HandleBookButton(sender, OperationType.Delete);
        }

        protected void HandleBookButton(object sender, OperationType operationType)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;

            int isbn = Convert.ToInt32(TableBooks.DataKeys[rowIndex].Value);

            if (operationType == OperationType.Update)
            {
                Session["ISBN"] = isbn;
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