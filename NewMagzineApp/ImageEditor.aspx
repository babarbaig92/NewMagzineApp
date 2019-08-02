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


<%--    <script src="scripts/cropperjs/src/js/cropper.js"></script>
    <link href="scripts/cropperjs/src/css/cropper.css" rel="stylesheet" />
    <script src="scripts/jquery-cropper/dist/jquery-cropper.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            
            var imgId = $("#hdnOriginalImageId").val();
            var originalHeight = imgId.naturalHeight;
            var originalWidth = imgId.naturalWidth;


            // Show original size in console to make sure is correct (optional):
            console.log('IMG width: ' + originalWidth + ', heigth: ' + originalHeight)

            $('img#' + imgId).imgAreaSelect({
                //aspectRatio: '1:1',
                handles: true,
                fadeSpeed: 200,
                imageHeight: originalHeight,
                imageWidth: originalWidth,
                onSelectEnd: ExtractImageSectionDetail
            });

            //$image.cropper({
            //    aspectRatio: 16 / 9,
            //    crop: function (event) {
            //        console.log(event.detail.x);
            //        console.log(event.detail.y);
            //        console.log(event.detail.width);
            //        console.log(event.detail.height);
            //        console.log(event.detail.rotate);
            //        console.log(event.detail.scaleX);
            //        console.log(event.detail.scaleY);
            //    }
            //});

            //// Get the Cropper.js instance after initialized
            //var cropper = $image.data('cropper');




            $("#btnSavePart").on("click", function () {
                //                GetCroppedData();
            });

        });

        //function GetCroppedData(image) {
        //    const cropper = new Cropper(image,
        //        cropper.getCroppedCanvas().toBlob((blob) => {
        //            const formData = new FormData();
        //            formData.append('croppedImage', blob);

        //            // Use `jQuery.ajax` method
        //            $.ajax('ImageEdiotr.aspx/SaveCroppedData', {
        //                method: "POST",
        //                data: formData,
        //                processData: false,
        //                contentType: false,
        //                success() {
        //                    console.log('Upload success');
        //                },
        //                error() {
        //                    console.log('Upload error');
        //                },
        //            });
        //        })
        //    );
        //}
        function ExtractImageSectionDetail(img, selection) {

            if (!selection.width || !selection.height)
                return;

            // With this two lines i take the proportion between the original size and
            // the resized img
            var porcX = img.naturalWidth / img.width;
            var porcY = img.naturalHeight / img.height

            var imagePart = {
                X1: Math.round(selection.x1 * porcX),
                Y1: Math.round(selection.y1 * porcY),
                X2: Math.round(selection.x2 * porcX),
                Y2: Math.round(selection.y2 * porcY),
                Width: Math.round(selection.width * porcX),
                Height: Math.round(selection.height * porcY),
                OriginalImageId: $("#hdnOriginalImageId").val(),
                OriginalImageName: $("#hdnOriginalImageName").val()
            };
            var imgPartJSON = JSON.stringify(imagePart);
            $("#hdnImagePart").val(imgPartJSON);
        }
    </script>

    <style>
        img {
            max-width: 100%; /* This rule is very important, please do not ignore this! */
        }
    </style>
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
        <div class="container" id="imageContainer" runat="server">
        </div>
        <asp:HiddenField ID="hdnImagePart" runat="server" Value="" />
        <asp:HiddenField ID="hdnOriginalImageId" runat="server" Value="" />
        <asp:HiddenField ID="hdnOriginalImageName" runat="server" Value="" />
    </form>
</body>
</html>
