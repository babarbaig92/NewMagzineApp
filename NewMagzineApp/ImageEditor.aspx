<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageEditor.aspx.cs" Inherits="NewMagzineApp.ImageEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/imgareaselect-default.css" />

    <script type="text/javascript" src="scripts/jquery.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.imgareaselect.pack.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('img#photo').imgAreaSelect({
                handles: true,
                onSelectEnd: ExtractImageSectionDetail
            });
        });
        function ExtractImageSectionDetail(img, selection) {
            var imagePart = {
                X1: selection.x1,
                Y1: selection.y1,
                X2: selection.x2,
                Y2: selection.y2,
                Width: selection.width,
                Height: selection.height,
                OriginalImageId: $("#hdnOriginalImageId").val(),
                OriginalImageName: $("#hdnOriginalImageName").val()
            };
            var imgPartJSON = JSON.stringify(imagePart);
            $("#hdnImagePart").val(imgPartJSON);
        }

    </script>
</head>
<body>
    <form id="imgEditorForm" runat="server">
        
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <h4>Create Section</h4>
            &nbsp &nbsp
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item active">
                        <asp:Button ID="btnSavePart" CssClass="btn btn-primary btn-sm" runat="server" Text="Save Part" OnClick="btnSavePart_Click" />
                    </li>
                </ul>
            </div>
        </nav>
        <div class="container-fluid">
            <img src="images/1.jpg" alt="test" runat="server" id="photo" />
        </div>
        <asp:HiddenField ID="hdnImagePart" runat="server" Value="" />
        <asp:HiddenField ID="hdnOriginalImageId" runat="server" Value="" />
        <asp:HiddenField ID="hdnOriginalImageName" runat="server" Value="" />
    </form>
</body>
</html>