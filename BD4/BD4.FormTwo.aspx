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
    <script src="Scripts/forms.js" defer></script>


</head>
<body>
    <form id="form1" runat="server">

        <!-- Статус соединеня !-->
        <div class="container">
            <asp:Label ID="ConnectLabel" runat="server" Text=""></asp:Label>
        </div>

        <!-- Задание !-->
        <div class="ml-2">
            <asp:Label ID="Label1" runat="server" Text="Задание:  Вставить заказ с указанными параметрами. "></asp:Label>
        </div>

        <asp:Label class="ml-2" ID="Label2" runat="server" Text="Пожалуйста, заполните параметры заказа."></asp:Label>

        <div class="ml-4">


            <div class="mb-2">
                <asp:Label ID="Label7" runat="server" Text="Номер изделия"></asp:Label>
                <asp:DropDownList ID="ProductIdListBox" runat="server" AutoPostBack="False" OnSelectedIndexChanged="ProductIdListBox_SelestedIndexChanged">
                </asp:DropDownList>
                <%--<asp:TextBox runat="server" ID="n_izd" MaxLength="6" placeholder="n_izd" />--%>
            </div>

            <div class="mb-2">
                <asp:Label ID="Label8" runat="server" Text="Номер покупателя"></asp:Label>
                <asp:DropDownList ID="ClientListBox" runat="server" AutoPostBack="False" OnSelectedIndexChanged="ClientListBox_SelectedIndexChanged"></asp:DropDownList>
                <%--<asp:TextBox runat="server" ID="n_cl" MaxLength="6" placeholder="n_cl" />--%>
            </div>
            
            <!-- Дата заказа !-->
            <div class="mb-2">
                <asp:Label ID="Label4" runat="server" Text="Дата заказа"></asp:Label>
                <input type="date" id="order_date"/>
                <asp:TextBox runat="server" ID="date_order" hidden/>
            </div>

            <!-- Дата оплаты !-->
            <div class="mb-2">
                <asp:Label ID="Label5" runat="server" Text="Дата оплаты"></asp:Label>
                <input type="date" id="pay_date"/>
                <asp:TextBox runat="server" ID="date_pay" hidden/>
            </div>

            <!-- Дата отправки заказа !-->
            <div class="mb-2">
                <asp:Label ID="Label6" runat="server" Text="Дата отправки заказа"></asp:Label>
                <input type="date" id="ship_date"/>
                <asp:TextBox runat="server" ID="date_ship" hidden/>
            </div>

            <!-- Объем поставки !-->
            <div class="mb-2">
                <asp:TextBox runat="server" ID="kol" placeholder="kol" min="0" />
            </div>

            <!-- Цена !-->
            <div class="mb-2">
                <asp:TextBox runat="server" ID="cost" placeholder="cost" min="0" />
            </div>
        </div>

        <!-- Навигация !-->
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
