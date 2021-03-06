<!--
  SessionObjectTest.aspx illustrates the use of the
  Session object to store information on the server.
  This information is specific for each user.
-->

<html>

<head>
<script language="C#" runat="server">

  void Page_Load(Object sender, EventArgs e) 
  {
    int myInt;

    // check if count is null
    if (Session["count"] == null)
    {
      // count is null, so initialize myInt to 1
      myInt = 1;
    }
    else
    {
      // retrieve count and increment myInt by 1
      myInt = (int) Session["count"] + 1;
    }

    // set count value to myInt
    Session["count"] = myInt;

    // display myInt in myLabel
    myLabel.Text = "This page has been viewed " + myInt.ToString() +
      " times.";
  }

</script>
</head>

<body>
<asp:Label id="myLabel" runat="server"/>
<form runat="server">
<asp:Button text="Press the Button!" runat="server"/>
</form>
</body>

</html>