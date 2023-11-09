using System;
using System.Data.Odbc;
using System.Text;
using System.Web.UI;

public partial class BD4_FormTwo : Page
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

    private string _orderId;
    private string _productId;
    private string _clientId;
    private string _dateOrder;
    private string _datePay;
    private string _dateShip;
    private int _scopeDelivery;
    private int _costProduct;

    private bool _isNullDatePay;
    private bool _isNullDateShip;

    private StringBuilder _validationError = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        Connect();
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
        if (!ValidationData())
        {
            Label3.Text = _validationError.ToString();
            return;
        }

        ExecuteProcedure();
    }
   
    private void ExecuteProcedure()
    {
        // TODO: TRIM(?) for string
        // TODO: (concat('R',cast(nextval('pmib0409.first') as varchar)) for n_real ????

        const string SQL = "INSERT INTO pmib0409.r VALUES\r\n" +
            "?, " +
            "?, " +
            "?, " +
            "to_date(?, 'yyyy-mm-dd'), " +
            "to_date(?, 'yyyy-mm-dd'), " +
            "to_date(?, 'yyyy-mm-dd'), " +
            "?, " +
            "?);";

        using (_command = new OdbcCommand(SQL,_connection))
        {
            ParametersSetting();

            OdbcTransaction transaction = null;

            try
            {
                transaction = _connection.BeginTransaction();
                _command.Transaction = transaction;

                var numberProcessedRecords = _command.ExecuteNonQuery();

                transaction.Commit();

                Label3.Text = $"{numberProcessedRecords} запис(ь/и/ей) обработано.";
            }
            catch (Exception ex)
            {
                Label3.Text = ex.Message;
                transaction.Rollback();
            }
            finally 
            {
                transaction.Dispose();
            }
        }

    }
    
    // Боже что это..
    private void ParametersSetting()
    {
        var parametr0 = new OdbcParameter();
        parametr0.OdbcType = OdbcType.Text;
        parametr0.Value = _orderId;
        _command.Parameters.Add(parametr0);

        var parametr1 = new OdbcParameter();
        parametr1.OdbcType = OdbcType.Text;
        parametr1.Value = _productId;
        _command.Parameters.Add(parametr1);

        var parametr2 = new OdbcParameter();
        parametr2.OdbcType = OdbcType.Text;
        parametr2.Value = _clientId;
        _command.Parameters.Add(parametr2);

        var parametr3 = new OdbcParameter();
        parametr3.OdbcType = OdbcType.Text;
        parametr3.Value = _dateOrder;
        _command.Parameters.Add(parametr3);

        var parametr4 = new OdbcParameter();
        parametr4.OdbcType = OdbcType.Text;
        parametr4.Value = /*_isNullDatePay ? null : */_datePay;
        _command.Parameters.Add(parametr4);

        var parametr5 = new OdbcParameter();
        parametr5.OdbcType = OdbcType.Text;
        parametr5.Value = /*_isNullDateShip ? null :*/ _dateShip;
        _command.Parameters.Add(parametr5);

        var parametr6 = new OdbcParameter();
        parametr6.OdbcType = OdbcType.Int;
        parametr6.Value = _scopeDelivery;
        _command.Parameters.Add(parametr6);

        var parametr7 = new OdbcParameter();
        parametr7.OdbcType = OdbcType.Int;
        parametr7.Value = _costProduct;
        _command.Parameters.Add(parametr7);
    }

    private bool ValidationData()
    {
        /*if (!DateTime.TryParse(date_order.Text, out _dateOrder))
        {
            _validationError.AppendLine("Не коректная дата заказа!");
        }

        if (date_pay.Text.Length == 0)
        {
            _isNullDatePay = true;
        }
        else
        {
            if (!DateTime.TryParse(date_pay.Text, out _datePay))
            {
                _validationError.AppendLine("Не коректная дата оплаты!");
            }
        }

        if (date_ship.Text.Length == 0)
        {
            _isNullDateShip = true;
        }
        else
        {
            if (!DateTime.TryParse(date_ship.Text, out _dateShip))
            {
                _validationError.AppendLine("Не коректная дата отправки заказа!");
            }
        }*/

        _orderId = n_real.Text;
        _productId = n_izd.Text;
        _clientId = n_cl.Text;
        _dateOrder = date_order.Text;

        _datePay = date_pay.Text;
        _dateShip = date_ship.Text;

        if (!Int32.TryParse(kol.Text, out _scopeDelivery) || _scopeDelivery < 1)
        {
            _validationError.AppendLine("Некоректно укзан обьем поставки изделий!");
        }

        if (!Int32.TryParse(cost.Text, out _costProduct) || _costProduct < 1)
        {
            _validationError.AppendLine("Некоректно укзана отпускная цена изделия\r\n!");
        }    

        return _validationError.Length == 0;
    }
}