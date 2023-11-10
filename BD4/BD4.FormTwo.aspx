<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BD4.FormTwo.aspx.cs" Inherits="BD4_FormTwo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

    <title>Задание 2</title>

    <link rel="stylesheet" href="Content/styles.css" />
    <script src="Scripts/scripts.js"></script>

</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <asp:Label ID="ConnectLabel" runat="server" Text=""></asp:Label>
        </div>

        <div class="ml-2">
            <asp:Label ID="Label1" runat="server" Text="Задание:  Вставить заказ с указанными параметрами. "></asp:Label>
        </div>

        <asp:Label class="ml-2" ID="Label2" runat="server" Text="Пожалуйста, заполните параметры заказа."></asp:Label>

        <div class="ml-4">
            <div class="mb-2">
                <asp:TextBox runat="server" ID="n_izd" MaxLength="6" placeholder="n_izd" />
            </div>

            <div class="mb-2">
                <asp:TextBox runat="server" ID="n_cl" MaxLength="6" placeholder="n_cl" />
            </div>

            <div class="mb-2">
                <asp:TextBox runat="server" ID="date_order" placeholder="date_order" />
            </div>

            <div class="mb-2">
                <asp:TextBox runat="server" ID="date_pay" placeholder="date_pay" data-toggle="tooltip" title="Can be NULL" />
            </div>

            <div class="mb-2">
                <asp:TextBox runat="server" ID="date_ship" placeholder="date_ship" data-toggle="tooltip" title="Can be NULL" />
            </div>

            <div class="mb-2">
                <asp:TextBox runat="server" ID="kol" placeholder="kol" min="0" />
            </div>

            <div class="mb-2">
                <asp:TextBox runat="server" ID="cost" placeholder="cost" min="0" />
            </div>
        </div>

        <div class="mb-2 ml-2">
            <asp:Button ID="ExecuteRequestButton" runat="server" Text="Выполнить запрос" OnClick="ExecuteRequestButton_Click" class="button-margin" />
            <asp:Button ID="Button1" runat="server" Text="Перейти к заданию 1" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Вернуться на начальную страницу" OnClick="Button2_Click" />
        </div>

        <div class="button-margin ml-2">
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        </div>

    </form>
</body>
</html>
