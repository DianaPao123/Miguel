<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carga.aspx.cs" Inherits="GAFWEB.Carga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
  <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
    <script src="Content/js/jquery.min.js"></script>
 <link href="Content/css/font-awesome.css" rel="stylesheet" />

<br />
<asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>
    <div class="card mt-2">   
            <div class="card-header">
                Cargar Archivo
            </div>
            <div class ="card-body">
                <div class = "row"> <%--Línea/Empresa--%>
                    <div class = "form-group col-md-4 col-sm-6">
                          <asp:Label ID="lblCarga" runat="server" Text="Cargar Archivo:"></asp:Label>
                     
  <asp:FileUpload runat="server" ID="archivoPagos"  />
             <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server"
              ErrorMessage="Solo Imagenes" ControlToValidate="archivoPagos" 
              ValidationExpression= "(.*).(.XML|.xml)$" />
              </div>
           <div class = "form-group col-md-3 col-sm-6">
                      <asp:Button runat="server" ID="btnSubir" Text="Subir" class="btn btn-outline-primary" 
                onclick="btnSubir_Click"/>
                </div>
                </div>

                  <p>
                <asp:Label runat="server" ID="lblError" ForeColor="Red" />
                </p>
           
                </div>
                </div>

</ContentTemplate>
 <Triggers>
       <asp:PostBackTrigger  ControlID="btnSubir"/>
     </Triggers>
</asp:UpdatePanel>

</asp:Content>
