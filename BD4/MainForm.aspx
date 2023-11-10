<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainForm.aspx.cs" Inherits="MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="Content/styles.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div>
            
            <div class="button-margin">
                <asp:Label ID="Label1" runat="server" Text="Бригада 9. Вариант 5"></asp:Label>
            </div>
            
            <div class="button-margin">
                <asp:Label ID="Label2" runat="server" Text="Задание 1: Получить информацию о последней цене деталей, которые были поставлены для указанного изделия. "></asp:Label>
                <br/>
                <asp:Button ID="Button1" runat="server" Text="Перейти" OnClick="Button1_Click" />
            </div>

            <div class="button-margin">
                <asp:Label ID="Label3" runat="server" Text="Задание 2: Вставить заказ с указанными параметрами."></asp:Label>
                <br/>
                <asp:Button ID="Button2" runat="server" Text="Перейти" OnClick="Button2_Click" />
            </div>
        </div>

    </form>
</body>
</html>
