<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDFUploader.aspx.cs" Inherits="NewMagzineApp.PDFUploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="pdfUploaderForm" runat="server">
        <div class="container-fluid">
            <h2>File Upload</h2>
            <asp:FileUpload ID="fileUploader" runat="server" />
            <br />
            <br />
            <asp:Button ID="btnSaveFile" CssClass="btn btn-primary" OnClick="btnSaveFile_Click" runat="server" Text="Save File" />
            <br />
            <br />
            <asp:Label ID="lblmessage" runat="server" />

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
    </form>
</body>
</html>
