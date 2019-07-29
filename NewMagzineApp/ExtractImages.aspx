<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExtractImages.aspx.cs" Inherits="NewMagzineApp.ExtractImages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image Extractor</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link href="Content/bootstrap.css" rel="stylesheet" />

    <script type="text/javascript">
        function TransferImageForEditing(imageId, imageName) {
            document.getElementById("hdnImageId").value = imageId;
            document.getElementById("hdnImageName").value = imageName;
            document.getElementById("hdnTransferImageForEditing").click();
        }
    </script>
</head>
<body>
    <form id="imgExtractorForm" runat="server">
        <div id="imageContainer" class="container" runat="server">
        </div>

        <asp:HiddenField ID="hdnImageId" runat="server" Value=""/>
        <asp:HiddenField ID="hdnImageName" runat="server" Value="" />

        <asp:ImageButton ID="hdnTransferImageForEditing" OnClick="hdnTransferImageForEditing_Click" runat="server" style="display:none;"/>

    </form>
</body>
</html>
