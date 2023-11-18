using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web.UI;
using System.Web.UI.WebControls;

public class DetailInfo
{
    public string DetailNumber { get; set; }
    public string Cost { get; set; }
}

public partial class BD4_FormOne : Page
{
    private readonly OdbcConnection _connection = new OdbcConnection();
    private OdbcCommand _command;
    private DataTable _dt = new DataTable();
    private List<DetailInfo> _res = new List<DetailInfo>();

    private string _connectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }
    }

    private string _selectedProduct
    {
        get
        {
            return ViewState["SelectedProduct"] as string ?? string.Empty;
        }
        set
        {
            ViewState["SelectedProduct"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ResponseGrid.DataSource = _res;

        Label3.Text = string.Empty;
        Label3.Visible = true;

        Connect();

        if (!IsPostBack && _connection.State == ConnectionState.Open)
        {
            ProductGridView.DataSource = _dt;
            ReceivingListProducts();
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Disconnect();
    }

    private void Connect()
    {
        _connection.ConnectionString = _connectionString;

        try
        {
            _connection.Open();
            ConnectLabel.Text = $"Connection status: {_connection.State}";
        }
        catch (Exception ex)
        {
            ConnectLabel.Text = ex.Message;
        }
    }

    private void Disconnect()
    {
        _connection.Dispose();
    }

    protected void ExecuteRequestButton_Click(object sender, EventArgs e)
    {
        if (_connection.State == ConnectionState.Open)
            ExecuteProcedure();
    }

    private void ExecuteProcedure()
    {
        const string SQL =
            "SELECT pmib0409.spj1.n_det, pmib0409.spj1.cost\r\n" +
            "FROM pmib0409.spj1\r\n" +
            "WHERE pmib0409.spj1.date_post = (SELECT MAX(t.date_post)\r\n" +
                                    "FROM pmib0409.spj1 as t\r\n" +
                                    "WHERE (t.n_izd = ?) AND t.n_det = pmib0409.spj1.n_det\r\n" +
           ");";

        using (_command = new OdbcCommand(SQL, _connection))
        {
            var parameter = new OdbcParameter();
            parameter.OdbcType = OdbcType.Text;
            parameter.Value = _selectedProduct;

            _command.Parameters.Add(parameter);

            OdbcTransaction transaction = null;
            OdbcDataReader reader = null;

            try
            {
                transaction = _connection.BeginTransaction();
                _command.Transaction = transaction;

                reader = _command.ExecuteReader();

                ResponseGrid.Visible = true;

                while (reader.Read())
                {
                    var s1 = reader["n_det"].ToString();
                    var s2 = reader["cost"].ToString();

                    _res.Add(new DetailInfo() { DetailNumber = s1, Cost = s2 });
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = ex.Message;
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
                reader.Dispose();
            }
        }

        ResponseGrid.DataBind();

        if (_res.Count == 0)
        {
            Label3.Visible = true;
            Label3.Text = "Данных не найдено!";
        }
    }

    private void ReceivingListProducts()
    {
        const string SQL = "SELECT * FROM pmib0409.j";

        using (_command = new OdbcCommand(SQL, _connection))
        {
            OdbcTransaction transaction = null;
            OdbcDataReader reader = null;

            try
            {
                transaction = _connection.BeginTransaction();
                _command.Transaction = transaction;

                reader = _command.ExecuteReader();

                _dt.Load(reader);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = ex.Message;
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
                reader.Dispose();
            }
        }

        ProductGridView.DataBind();

        if (ProductGridView.Rows.Count > 0)
        {
            _selectedProduct = ProductGridView.Rows[0].Cells[1].Text;

            var radio = ProductGridView.Rows[0].Cells[0].Controls[1] as RadioButton;
            radio.Checked = true;
        }
    }

    protected void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton radioButton = (RadioButton)sender;
        GridViewRow selectedRow = (GridViewRow)radioButton.Parent.Parent;

        int rowIndex = selectedRow.RowIndex;

        foreach (var x in ProductGridView.Rows)
        {
            var item = (GridViewRow)x;
            var radio = (RadioButton)item.Cells[0].Controls[1];

            if (radio == sender)
            {
                continue;
            }

            radio.Checked = false;
        }

        _selectedProduct = ProductGridView.Rows[rowIndex].Cells[1].Text.Trim();
    }

    protected void Button1_Click(object sender, EventArgs e) => Page.Response.Redirect("BD4.FormTwo.aspx");

    protected void Button2_Click(object sender, EventArgs e) => Page.Response.Redirect("MainForm.aspx");
}