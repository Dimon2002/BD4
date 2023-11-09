<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BD4.FormOne.aspx.cs" Inherits="BD4_FormOne" %>

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
            margin-bottom: 20px;
        }
    </style>
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
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>n_izd</asp:TableHeaderCell>
            </asp:TableHeaderRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:RadioButtonList ID="ProductRadioButtonList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProductRadioButtonList_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <div>
            <asp:Button ID="ExecuteRequestButton" runat="server" Text="Выполнить запрос" OnClick="ExecuteRequestButton_Click" class="button-margin" />
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
