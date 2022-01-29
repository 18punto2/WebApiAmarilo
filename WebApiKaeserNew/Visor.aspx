<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visor.aspx.cs" Inherits="WebApiKaeser.Visor" %>

<%@ Register assembly="DevExpress.XtraReports.v14.2.Web, Version=14.2.17.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<script src="scripts/bowser.min.js"></script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <script type="text/javascript">
            function init(s) {
                var createFrameElement = s.viewer.printHelper.createFrameElement;
                s.viewer.printHelper.createFrameElement = function (name) {
                    var frameElement = createFrameElement.call(this, name);
                    if (bowser.name == 'Chrome') {
                        frameElement.addEventListener("load", function (e) {
                            if (frameElement.contentDocument && frameElement.contentDocument.contentType !== "text/html")
                                frameElement.contentWindow.print && frameElement.contentWindow.print();
                        });
                    }
                    return frameElement;
                }
            }
        </script>
    <div>
    <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server" ClientSideEvents-Init="init">
            <SettingsReportViewer ShouldDisposeReport="False" />
        </dx:ASPxDocumentViewer>
    </div>       
    </form>
</body>
</html>
