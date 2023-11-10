﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Globalization;
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

    private string _productId;
    private string _clientId;

    private DateTime _dateOrder;
    private DateTime _datePay;
    private DateTime _dateShip;

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
        const string SQL = "INSERT INTO pmib0409.r VALUES\r\n" +
            "(concat('R',cast(nextval('pmib0409.seq_n_real') as varchar)), " +
            "?, " +
            "?, " +
            "?, " +
            "?, " +
            "?, " +
            "?, " +
            "?);";

        using (_command = new OdbcCommand(SQL, _connection))
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
                transaction?.Dispose();
            }
        }

    }

    private void ParametersSetting()
    {
        var parameters = new List<(OdbcType Type, object Value)>
        {
            (OdbcType.Text, _productId),
            (OdbcType.Text, _clientId),
            (OdbcType.DateTime, _dateOrder),
            (OdbcType.Date, _isNullDatePay ? DBNull.Value : (object)_datePay),
            (OdbcType.DateTime, _isNullDateShip ? DBNull.Value : (object)_dateShip),
            (OdbcType.Int, _scopeDelivery),
            (OdbcType.Int, _costProduct)
        };

        parameters.ForEach(p => _command.Parameters.Add(new OdbcParameter { OdbcType = p.Type, Value = p.Value }));
    }

    private bool ValidationData()
    {
        if (string.IsNullOrEmpty(n_izd.Text))
        {
            _validationError.Append("Некорректно указан номер изделия!<br />");
        }

        if (string.IsNullOrEmpty(n_cl.Text))
        {
            _validationError.Append("Некорректно указан номер покупателя!<br />");
        }

        _productId = n_izd.Text;
        _clientId = n_cl.Text;
        
        _isNullDatePay = string.IsNullOrEmpty(date_pay.Text);
        _isNullDateShip = string.IsNullOrEmpty(date_ship.Text);

        if (!DateTime.TryParseExact(date_order.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dateOrder))
        {
            _validationError.AppendLine("Некорректная дата заказа!<br />");
        }

        if (!_isNullDatePay && !DateTime.TryParseExact(date_pay.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _datePay))
        {
            _validationError.AppendLine("Некорректная дата оплаты!<br />");
        }
        
        if (!_isNullDateShip && !DateTime.TryParseExact(date_ship.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dateShip))
        {
            _validationError.AppendLine("Некорректная дата отправки заказа!<br />");
        }

        if (!Int32.TryParse(kol.Text, out _scopeDelivery) || _scopeDelivery < 1)
        {
            _validationError.AppendLine("Некоректно укзан обьем поставки изделий!<br />");
        }

        if (!Int32.TryParse(cost.Text, out _costProduct) || _costProduct < 1)
        {
            _validationError.AppendLine("Некоректно укзана отпускная цена изделия!<br />");
        }

        return _validationError.Length == 0;
    }

    protected void Button1_Click(object sender, EventArgs e) => Page.Response.Redirect("BD4.FormOne.aspx");

    protected void Button2_Click(object sender, EventArgs e) => Page.Response.Redirect("MainForm.aspx");
}