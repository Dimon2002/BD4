using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainForm : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e) => Page.Response.Redirect("BD4.FormOne.aspx");

    protected void Button2_Click(object sender, EventArgs e) => Page.Response.Redirect("BD4.FormTwo.aspx");
} 