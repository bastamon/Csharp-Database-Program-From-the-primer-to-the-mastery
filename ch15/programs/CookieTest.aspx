<!--
  CookieTest.aspx illustrates the use of a cookie to
  store information on the client
-->

<html>

<head>
<script language="C#" runat="server">

  void Page_Load(Object sender, EventArgs e) 
  {
    int myInt;

    // check if count is null
    if (Request.Cookies["count"] == null)
    {
      // count is null, so initialize myInt to 1
      myInt = 1;

      // create an HttpCookie object
      HttpCookie myHttpCookie = new HttpCookie("count", myInt.ToString());

      // add HttpCookie object to Response
      Response.AppendCookie(myHttpCookie);
    }
    else
    {
      // retrieve count and increment myInt by 1
      myInt = Int32.Parse(Request.Cookies["count"].Value) + 1;
    }

    // set count value to myInt
    Response.Cookies["count"].Value = myInt.ToString();

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