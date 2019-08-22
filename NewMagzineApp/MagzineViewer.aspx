<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MagzineViewer.aspx.cs" Inherits="NewMagzineApp.MagzineViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Magzine Viewer</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.0.0.js"></script>

    <link href="Content/datetimepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="Content/datetimepicker/js/bootstrap-datepicker.js"></script>


    <!-- JavaScript -->
    <script src="//cdn.jsdelivr.net/npm/alertifyjs@1.11.4/build/alertify.min.js"></script>

    <!-- CSS -->
    <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.11.4/build/css/alertify.min.css" />
    <!-- Default theme -->
    <link rel="stylesheet" href="//cdn.jsdelivr.net/npm/alertifyjs@1.11.4/build/css/themes/default.min.css" />


    <script type="text/javascript">

        alertify.defaults.transition = "zoom";
        alertify.defaults.theme.ok = "ui positive button";
        alertify.defaults.theme.cancel = "ui black button";

        $(document).ready(function () {
            $('.datepicker').datepicker();
            $("#btnSearchUploadDocs").on('click', function (e) {
                e.preventDefault();
                ValidateUploadDate();
            });
        });

        function ValidateUploadDate() {
            var fromDate = $("#dtFromDocDate").val();
            var toDate = $("#dtToDocDate").val();
            var formattedToDate = "";
            var formattedFromDate = "";
            if (fromDate != '' || toDate != '') {

                var dtFrom = new Date(fromDate);
                var dtTo = new Date(toDate);

                if (dtFrom > dtTo) {
                    alertify.alert("'From Date' should always be less than 'To Date'");
                    return;
                }
                // FromDate Format
                var FromYear = dtFrom.getFullYear();
                var FromMonth = dtFrom.getMonth() + 1;
                var FromDate = dtFrom.getDate();

                formattedFromDate = FromYear + "-" + FromMonth + "-" + FromDate;

                // ToDate Format
                var ToYear = dtTo.getFullYear();
                var ToMonth = dtTo.getMonth() + 1;
                var ToDate = dtTo.getDate();

                formattedToDate = ToYear + "-" + ToMonth + "-" + ToDate;
            }
            $("#hdnFromDate").val(formattedFromDate);
            $("#hdnToDate").val(formattedToDate);

            $("#btnLoadMagzineDocs").click();
        }

        function ProcessPDF(documentId, documentName, hasImages) {
            document.getElementById("hdnDocumentId").value = documentId;
            document.getElementById("hdnDocumentName").value = documentName;
            document.getElementById("hdnHasImages").value = hasImages;
            document.getElementById("btnRedirectToPageViewer").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="PDFUploader.aspx">Magzine App</a>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav mr-auto">
                </ul>
            </div>
        </nav>
        <br />
        <div class="container">
            <div class="row mb-5">
                <div class="col-md-4">
                    <input id="dtFromDocDate" class="datepicker form-control w-50" data-date-format="mm/dd/yyyy" placeholder="From Date" title="From Date" autocomplete="off"/>
                </div>
                <div class="col-md-4">
                    <input id="dtToDocDate" class="datepicker form-control w-50" data-date-format="mm/dd/yyyy" placeholder="To Date" title="To Date" autocomplete="off" />
                </div>
                <div class="col-md-4">
                    <button id="btnSearchUploadDocs" class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:DataList ID="savedFilesList" runat="server">
                        <ItemTemplate>
                            <table class="table">
                                <tr>
                                    <td>
                                        <b>Document Name: </b><a href="javascript:ProcessPDF('<%# Eval("DocumentId")%>','<%# Eval("DocumentName") %>','<%# Eval("HasGeneratedImages") %>');"><span><%# Eval("DocumentName") %></span></a><br />
                                        <b>Upload Date: </b><span><%# Eval("UploadDate")%></span><br />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hdnFromDate" runat="server" Value="" />
        <asp:HiddenField ID="hdnToDate" runat="server" Value="" />

        <asp:HiddenField ID="hdnDocumentId" Value="" runat="server" />
        <asp:HiddenField ID="hdnDocumentName" Value="" runat="server" />
        <asp:HiddenField ID="hdnHasImages" Value="" runat="server" />
        <asp:ImageButton ID="btnRedirectToPageViewer" Style="display: none;" OnClick="btnRedirectToPageViewer_Click" runat="server" />

        <asp:ImageButton ID="btnLoadMagzineDocs" Style="display: none;" OnClick="btnLoadMagzineDocs_Click" runat="server" />
    </form>
</body>
</html>
