<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BD4.FormTwo.aspx.cs" Inherits="BD4_FormTwo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        .container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 10vh;
        }

        .button-margin {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <asp:Label ID="ConnectLabel" runat="server" Text=""></asp:Label>
        </div>

        <div>
            <asp:Label ID="Label1" runat="server" Text="Задание:  Вставить заказ с указанными параметрами. "></asp:Label>
        </div>

        <asp:Label ID="Label2" runat="server" Text="Пожалуйста, заполните параметры заказа."></asp:Label>

        <div class="button-margin">
            <asp:TextBox runat="server" ID="n_real" MaxLength="10" Required="true" placeholder="n_real" />
            <br />

            <asp:TextBox runat="server" ID="n_izd" MaxLength="6" Required="true" placeholder="n_izd" />
            <br />

            <asp:TextBox runat="server" ID="n_cl" MaxLength="6" Required="true" placeholder="n_cl" />
            <br />

            <asp:TextBox runat="server" ID="date_order" Required="true" placeholder="date_order" />
            <br />

            <asp:TextBox runat="server" ID="date_pay" placeholder="date_pay" />
            <br />

            <asp:TextBox runat="server" ID="date_ship" placeholder="date_ship" />
            <br />

            <asp:TextBox runat="server" ID="kol" Required="true" placeholder="kol" min="0" />
            <br />

            <asp:TextBox runat="server" ID="cost" Required="true" placeholder="cost" min="0" />
            <br />
        </div>

        <div>
            <asp:Button ID="ExecuteRequestButton" runat="server" Text="Выполнить запрос" OnClick="ExecuteRequestButton_Click" class="button-margin" />
        </div>

        <div>
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        </div>

    </form>
</body>
</html>
