<!--
  ApplicationObjectTest.aspx illustrates the use of the
  Application object to store information on the server.
  This information is shared for all users.
-->

<html>

<head>
<script language="C#" runat="server">

  void Page_Load(Object sender, EventArgs e) 
  {
    int myInt;

    // check if count is null
    if (Application["count"] == null)
    {
      // count is null, so initialize myInt to 1
      myInt = 1;
    }
    else
    {
      // retrieve count and increment myInt by 1
      myInt = (int) Application["count"] + 1;
    }

    // set count value to myInt
    Application["count"] = myInt;

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