<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDFUploader.aspx.cs" Inherits="NewMagzineApp.PDFUploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PDF Uploader</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link href="Content/bootstrap.css" rel="stylesheet" />

    <script type="text/javascript">
        function ProcessPDF(documentId, documentName) {
            document.getElementById("hdnDocumentId").value = documentId;
            document.getElementById("hdnDocumentName").value = documentName;
            document.getElementById("btnRedirectToPDFProcessor").click();
        }
    </script>

</head>
<body>
    <form id="pdfUploaderForm" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="PDFUploader.aspx">Magzine App</a>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav mr-auto">
                </ul>
                <input class="form-control mr-sm-2" type="search" placeholder="Search by date" aria-label="Search" />
                <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
            </div>
        </nav>
        <br />
        <div class="container">
            <div class="flex-row">
                <h2>File Upload</h2>
            </div>
            <br />
            <div class="row">
                <div class="col-md-6">
                    <asp:FileUpload ID="fileUploader" runat="server" />
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnSaveFile" CssClass="btn btn-primary" OnClick="btnSaveFile_Click" runat="server" Text="Save File" />
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Label ID="lblmessage" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:DataList ID="savedFilesList" runat="server">
                        <ItemTemplate>
                            <table class="table">
                                <tr>
                                    <td>
                                        <b>ID: </b><a href="javascript:ProcessPDF('<%# Eval("DocumentId")%>','<%# Eval("DocumentName") %>');"><span><%# Eval("DocumentId") %></span></a><br />
                                        <b>File Name: </b><span><%# Eval("DocumentName") %></span><br />
                                        <b>Upload Date: </b><span><%# Eval("UploadDate")%></span><br />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnDocumentId" Value="" runat="server" />
        <asp:HiddenField ID="hdnDocumentName" Value="" runat="server" />
        <asp:ImageButton ID="btnRedirectToPDFProcessor" Style="display: none;" OnClick="btnRedirectToPDFProcessor_Click" runat="server" />
    </form>
</body>
</html>
