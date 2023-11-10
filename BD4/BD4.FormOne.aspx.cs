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

    private List<DetailInfo> _res = new List<DetailInfo>();

    protected void Page_Load(object sender, EventArgs e)
    {
        ResponseGrid.DataSource = _res;

        Connect();

        if (!IsPostBack && _connection.State == ConnectionState.Open)
        {
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

    protected void ProductRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ProductRadioButtonList.SelectedValue))
        {
            _selectedProduct = ProductRadioButtonList.SelectedValue;
        }
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
        //ProductRadioButtonList.Items.Clear();

        const string SQL = "SELECT n_izd AS n_izd FROM pmib0409.j";

        using (_command = new OdbcCommand(SQL, _connection))
        {
            OdbcTransaction transaction = null;
            OdbcDataReader reader = null;

            try
            {
                transaction = _connection.BeginTransaction();
                _command.Transaction = transaction;

                reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    string value = reader["n_izd"].ToString();
                    ProductRadioButtonList.Items.Add(new ListItem(value));
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

        if (ProductRadioButtonList.Items.Count > 0)
        {
            _selectedProduct = ProductRadioButtonList.Items[0].Value;
            ProductRadioButtonList.SelectedIndex = 0;
        }
    }

    protected void Button1_Click(object sender, EventArgs e) => Page.Response.Redirect("BD4.FormTwo.aspx");

    protected void Button2_Click(object sender, EventArgs e) => Page.Response.Redirect("MainForm.aspx");
}