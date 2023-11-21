<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BD4.FormOne.aspx.cs" Inherits="BD4_FormOne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Задание 1</title>

    <link rel="stylesheet" href="Content/styles.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <asp:Label ID="ConnectLabel" runat="server" Text=""></asp:Label>
        </div>

        <div>
            <asp:Label ID="Label1" runat="server" Text="Задание: Получить информацию о последней цене деталей, которые были поставлены для указанного изделия."></asp:Label>
        </div>

        <asp:Label ID="Label2" runat="server" Text="Пожалуйста, выберите изделие."></asp:Label>

        <asp:Table ID="Table1" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView DataKeyNames="n_izd" ID="ProductGridView" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="ProductGroup" AutoPostBack="true" OnCheckedChanged="RadioButton_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                            <asp:BoundField DataField="town" HeaderText="Town" SortExpression="town" />
                        </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <div>
            <asp:Button ID="ExecuteRequestButton" runat="server" Text="Выполнить запрос" OnClick="ExecuteRequestButton_Click" class="button-margin" />
            <asp:Button ID="Button1" runat="server" Text="Перейти к заданию 2" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Вернуться на начальную страницу" OnClick="Button2_Click" />
        </div>



        <div>
            <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
        </div>

        <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>

        <asp:GridView ID="ResponseGrid" runat="server" AutoGenerateColumns="False" Visible="false">
            <Columns>
                <asp:BoundField DataField="DetailNumber" HeaderText="n_det" />
                <asp:BoundField DataField="Cost" HeaderText="cost" />
            </Columns>
        </asp:GridView>

    </form>
</body>
</html>
