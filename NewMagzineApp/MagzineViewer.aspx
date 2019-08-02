<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagzineViewer.aspx.cs" Inherits="NewMagzineApp.MagzineViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" id="imageContainer" runat="server">
        </div>

        <asp:HiddenField ID="hdnFromDate" runat="server" Value="" />
        <asp:HiddenField ID="hdnToDate" runat="server" Value="" />
    </form>
</body>
</html>
