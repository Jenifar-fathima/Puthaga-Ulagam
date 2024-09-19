using PuthagaUlagam.Common;
using PuthagaUlagam.Data;
using PuthagaUlagam.Logic;
using System;
using System.Linq;

namespace PuthagaUlagam
{
    public partial class AddUpdateBook : System.Web.UI.Page
    {
        BookOperationBL operationBL = new BookOperationBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["RowIndex"] != null)
                {
                    int rowIndex = (int)Session["RowIndex"];
                    Book book = operationBL.GetBookById(rowIndex);
                    if (book != null)
                    {
                        txtTitle.Text = book.Title;
                        txtAuthor.Text = book.Author;
                        txtISBN.Text = book.ISBN.ToString();
                        txtPrice.Text = book.Price.ToString();
                        DisplayDate.Text = book.Date.ToString("d");
                        txtBookCount.Text = book.Count.ToString();
                        btnAdd.Visible = false;
                        btnUpdate.Visible = true;
                        txtISBN.ReadOnly = true;
                    }

                    Session["RowIndex"] = null;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            HandleBookOperation(OperationType.Add);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            HandleBookOperation(OperationType.Update);
        }

        private void HandleBookOperation(OperationType operationType)
        {
            lblErrorMessage.Text = "";

            if (IsInputValid())
            {
                BookDTO bookdto = CreateBookDTO();

                if (operationType == OperationType.Add)
                {
                    var repeatedISBN = DataContext.Books
                        .FirstOrDefault(b => b.ISBN == bookdto.ISBN);

                    if (repeatedISBN == null)
                    {
                        bool IsAddedSuccess = operationBL.AddBook(bookdto).IsSuccess;
                        if (IsAddedSuccess)
                        {
                            lblErrorMessage.Text = Messages.BookAddSuccess;
                        }
                    }
                    else if (repeatedISBN != null)
                    {
                        lblErrorMessage.Text = Messages.ISBNAlreadyExist;
                    }
                    else
                    {
                        lblErrorMessage.Text = Messages.BookAddFail;
                    }
                }
                else if (operationType == OperationType.Update)
                {
                    bool IsUpdateSuccess = operationBL.UpdateBook(bookdto).IsSuccess;

                    if (!IsUpdateSuccess)
                    {
                        lblErrorMessage.Text = Messages.BookUpdateFail;
                    }
                    else
                    {
                        lblErrorMessage.Text = Messages.BookUpdateSuccess;
                    }

                    Response.Redirect("ViewBook.aspx");
                }

                ClearDataField();
            }
            else
            {
                InputValidationMessageMethod();
            }
        }

        private BookDTO CreateBookDTO()
        {
            return new BookDTO
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                ISBN = int.Parse(txtISBN.Text),
                Price = decimal.Parse(txtPrice.Text),
                Date = DateOfPublication.SelectedDate,
                Count = int.Parse(txtBookCount.Text)
            };
        }

        private bool IsInputValid()
        {
            return InputValidation.IsValidISBN(txtISBN.Text) &&
                   InputValidation.IsValidCount(txtBookCount.Text) &&
                   InputValidation.IsValidPrice(txtPrice.Text);
        }

        private void ClearDataField()
        {
            txtTitle.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtISBN.Text = string.Empty;
            txtPrice.Text = string.Empty;
            DateOfPublication.SelectedDate = DateTime.Now;
            DisplayDate.Text = string.Empty;
            txtBookCount.Text = string.Empty;
        }

        protected void PickTheDateOfPublication_Click(object sender, EventArgs e)
        {
            DateOfPublication.Visible = !DateOfPublication.Visible;
        }

        protected void Calender_date_changed(object sender, EventArgs e)
        {
            DateOfPublication.Visible = false;
            DisplayDate.Text = "Selected Date: " + DateOfPublication.SelectedDate.ToString("d");
        }

        private void InputValidationMessageMethod()
        {
            if (!InputValidation.IsValidISBN(txtISBN.Text))
            {
                lblErrorMessage.Text += Messages.InvalidISBN;
            }
            if (!InputValidation.IsValidCount(txtBookCount.Text))
            {
                lblErrorMessage.Text += Messages.InvalidCount;
            }
            if (!InputValidation.IsValidPrice(txtPrice.Text))
            {
                lblErrorMessage.Text += Messages.InvalidPrice;
            }
            if (!InputValidation.IsValidDateOfPublication(DateOfPublication.SelectedDate))
            {
                lblErrorMessage.Text += Messages.InvalidDate;
            }
        }
    }
}