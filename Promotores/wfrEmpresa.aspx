<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEmpresa.aspx.cs" Inherits="GAFWEB.wfrEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" ID="lblError" ForeColor="Red" />

    <%-------------------- Panel Superior --------------------%>

    <div class = "panel panel-default">
        <div class = " display-4 panel-heading">
            Empresa
        </div>
        <div class = "panel-body">
            <%-------------------- RFC - Línea --------------------%>
            <div class = "form-group row justify-content-center">
                <div class = "col-md-2 col-sm-6">
                    <asp:Label ID="lblRFC" runat="server" Text="RFC"></asp:Label>
                    <asp:TextBox runat="server" ID="txtRFC" CssClass="form-control"/>                  
                </div>
              <%--  <div class = "col-md-2 col-sm-6">
                    <asp:Label ID="lblLinea" runat="server" Text="Linea"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlLinea" class = "form-control">                  
                        <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                        <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                        <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                        <asp:ListItem Value="D" Text="D" ></asp:ListItem> 
                    </asp:DropDownList>--%>
                </div>
            </div>
            <%-------------------- Termina RFC - Línea --------------------%>

            <%-------------------- Razón Social/Régiman Fiscal --------------------%>
            <div class = "row justify-content-center">
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social"></asp:Label>
                    <asp:TextBox runat="server" ID="txtRazonSocial" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblRegimenFiscal" runat="server" Text="Régimen Fiscal"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlRegimen" AutoPostBack="True" 
                    onselectedindexchanged="ddlRegimen_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Value="601" Text="General de Ley Personas Morales" runat="server" />
                        <asp:ListItem Value="603" Text="Personas Morales con Fines no Lucrativos" runat="server" />
                        <asp:ListItem Value="605" Text="Sueldos y Salarios e Ingresos Asimilados a Salarios" runat="server" />
                        <asp:ListItem Value="606" Text="Arrendamiento" runat="server" />
                        <asp:ListItem Value="608" Text="Demás ingresos" runat="server" />
                        <asp:ListItem Value="609" Text="Consolidación" runat="server" />
                        <asp:ListItem Value="610" Text="Residentes en el Extranjero sin Establecimiento Permanente en México" runat="server"/>
                        <asp:ListItem Value="611" Text="Ingresos por Dividendos (socios y accionistas)" runat="server" />
                        <asp:ListItem Value="612" Text="Personas Físicas con Actividades Empresariales y Profesionales" runat="server" />
                        <asp:ListItem Value="614" Text="Ingresos por intereses" runat="server" />
                        <asp:ListItem Value="616" Text="Sin obligaciones fiscales" runat="server" />
                        <asp:ListItem Value="620" Text="Sociedades Cooperativas de Producción que optan por diferir sus ingresos" runat="server" />
                        <asp:ListItem Value="621" Text="Incorporación Fiscal" runat="server" />
                        <asp:ListItem Value="622" Text="Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras" runat="server" />
                        <asp:ListItem Value="623" Text="Opcional para Grupos de Sociedades" runat="server" />
                        <asp:ListItem Value="624" Text="Coordinados" runat="server" />
                        <asp:ListItem Value="628" Text="Hidrocarburos" runat="server" />
                        <asp:ListItem Value="607" Text="Régimen de Enajenación o Adquisición de Bienes" runat="server" />
                        <asp:ListItem Value="629" Text="De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales" runat="server" />
                        <asp:ListItem Value="630" Text="Enajenación de acciones en bolsa de valores" runat="server" />
                        <asp:ListItem Value="615" Text="Régimen de los ingresos por obtención de premios" runat="server" />
                    </asp:DropDownList>
                  
                </div>
            </div>
            <%-------------------- Termina Razón Social/Régiman Fiscal --------------------%>
        </div>
   </div>

   <%-------------------- Termina Panel Superior --------------------%>

   <%-------------------- Panel Datos Generales --------------------%>

   <div class = "panel panel-default">
        <div class = "panel-body">
            <div class = "row justify-content-center">
                <div class = "form-group col-md-4 col-sm-12">
                    <asp:Label ID="lblCURP" runat="server" Text="CURP (Si aplica)"></asp:Label>
                    <asp:TextBox runat="server" ID="txtCURP" MaxLength="24" CssClass="form-control"/>
                </div>
            </div>
            <div class = "row">
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblCalle" runat="server" Text="Calle"></asp:Label>
                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-1 col-sm-6">
                    <asp:Label ID="lblNoExterior" runat="server" Text="No. Ext."></asp:Label>
                    <asp:TextBox runat="server" ID="txtExt" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-1 col-sm-6">
                    <asp:Label ID="lblNoInterior" runat="server" Text="No. Int."></asp:Label>
                    <asp:TextBox runat="server" ID="txtInt" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblColonia" runat="server" Text="Colonia"></asp:Label>
                    <asp:TextBox runat="server" ID="txtColonia" CssClass = "form-control"/>
                </div>
            </div>
            <div class = "row justify-content-center">
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblMunicipio" runat="server" Text="Municipio"></asp:Label>
                    <asp:TextBox runat="server" ID="txtMunicipio" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblLocalidad" runat="server" Text="Localidad"></asp:Label>
                    <asp:TextBox runat="server" ID="txtLocalidad" CssClass="form-control"/>
                </div>
            </div>
            <div class = "row">
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="Estado" runat="server" Text="Estado"></asp:Label>
                    <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-2 col-sm-6">
                    <asp:Label ID="lblCP" runat="server" Text="Código Postal"></asp:Label>
                    <asp:TextBox runat="server" ID="txtCP" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblReferencia" runat="server" Text="Referencia"></asp:Label>
                    <asp:TextBox runat="server" ID="txtReferencia" CssClass="form-control"/>
                </div>
            </div>
            <div class = "row">
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalido email."
                     ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </div>
                <div class = "form-group col-md-2 col-sm-6">
                    <asp:Label ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label>
                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control"/>
                </div>
                <div class = "form-group col-md-5 col-sm-12">
                    <asp:Label ID="lblWeb" runat="server" Text="Web"></asp:Label>
                    <asp:TextBox runat="server" ID="txtWeb" CssClass="form-control"/>
                </div>
            </div>
            <div class = "row justify-content-center">
                <div class = "form-group col-md-6 col-sm-12">
                    <asp:Label ID="lblContactoEmpresa" runat="server" Text="Contacto Administrativo"></asp:Label>
                    <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control"/>
                </div>
            </div>
        </div>
    </div>

    <%-------------------- Termina Panel Datos Generales --------------------%>

    <%-------------------- Panel Sucursal --------------------%>

    <div id = "panelSucursal" class = "panel panel-default">
        <div class = "panel-body" id="pnlSucursal" runat="server" visible="false">
            <div class = "row">
                <div class = "form-group col-md-6 col-sm-12">
                    <asp:Label ID="lblSucursal" runat="server" Text="Sucursal"></asp:Label>
                    <asp:TextBox runat="server" ID="txtSucursal" CssClass="form-control"/>
                    <asp:RequiredFieldValidator runat="server" ID="rfvSucursal" ControlToValidate="txtSucursal"
                      Display="Dynamic" ErrorMessage="* Requerido" />
                </div>
                <div class = "form-group col-md-6 col-sm-12">
                    <asp:Label ID="lblLugarExpedicion" runat="server" Text="Lugar de Expedición"></asp:Label>
                    <asp:TextBox runat="server" ID="txtLugarExpedicion" CssClass="form-control"/>
                    <asp:RequiredFieldValidator runat="server" ID="rfvLugarExpedicion" ControlToValidate="txtLugarExpedicion"
                      Display="Dynamic" ErrorMessage="* Requerido" />
                </div>
            </div>
            <div class = "row justify-content-center">
                <div class = "form-group col-md-6 col-sm-12">
                    <asp:Label ID="lblContactoSucursal" runat="server" Text="Contacto Administrativo"></asp:Label>
                    <asp:TextBox runat="server" ID="txtContacto" CssClass="form-control"/>
                </div>
            </div>
        </div>
    </div>        
   

    <%-------------------- Termina Panel Sucursal --------------------%>

    <%-------------------- Panel Archivos --------------------%>

    <div class = "panel panel-default">
        <div class = "panel-body">
        <div class = "row align-items-center">
           
                <div class = "form-group col-md-4 col-sm-6">
                    <asp:Label ID="lblLogo" runat="server" Text="Logo de la Empresa"></asp:Label>
                    <asp:FileUpload runat="server" ID="fuLogoEmpresa"/>
                    <asp:Label ID="lblPesoLogo" runat="server" Text="(Peso máximo 50kb)"></asp:Label>
                </div>
                <div class = "form-group col-md-3">
                    <asp:Label ID="lblOrientacionArchivo" runat="server" Text="Orientación del Archivo PDF"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlOrientacion" CssClass="form-control">
                        <asp:ListItem Value="0" Text="Vertical" ></asp:ListItem> 
                        <asp:ListItem Value="1" Text="Horizontal" ></asp:ListItem> 
                    </asp:DropDownList>
                </div>
            </div>
            <div class = "row align-items-center">
                <div class = "form-group col-md-4 col-sm-6">
                    <asp:Label ID="lblCertificado" runat="server" Text="Certificado Digital"></asp:Label>
                    <asp:FileUpload runat="server" ID="fuCertificado"/>
                </div>
                <div class = "form-group col-md-4 col-sm-6">
                    <asp:Label ID="lblLlave" runat="server" Text="Llave Privada"></asp:Label>
                    <asp:FileUpload runat="server" ID="fuLlave"/>
                </div>
                </div>
                 
            <div class = "row align-items-center">
                <div class = "form-group col-md-3 col-sm-6">
                    <asp:Label ID="lblPasswordLlave" runat="server" Text="Contraseña Llave"></asp:Label>
                    <asp:TextBox runat="server" ID="txtPassWordLlave" TextMode="Password" CssClass="form-control"/>
                </div>
                <div class = "form-group col-sm-1">
                    <asp:Button ID="btnValidar" runat="server" Text="Validar" onclick="btnValidar_Click"  class="btn btn-outline-primary" />                    
                </div>
                <div class = "form-group col-md-4 col-sm-12">
                    <asp:Label ID="lblEncabezado" runat="server" Text="Leyenda Encabezado"></asp:Label>
                    <asp:TextBox runat="server" ID="txtLeyendaSuperior" CssClass="form-control"></asp:TextBox>
                </div>
                <div class = "form-group col-md-4 col-sm-12">
                    <asp:Label ID="lblPiePagina" runat="server" Text="Leyenda Pie de Página"></asp:Label>
                    <asp:TextBox runat="server" ID="txtLeyendaPie" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <%-------------------- Termina Panel Archivos --------------------%>

    <%-------------------- Panel Botones --------------------%>

    <div class = "panel panel-default">
        <div class = "panel-body">
            <div class = "row justify-content-center">
                <div class = "form-group col-sm-1">
                    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" onclick="btnGuardar_Click"  class="btn btn-outline-primary" />
                </div>
                <div class = "form-group col-sm-1">
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" onclick="btnCancelar_Click" CausesValidation="False"  
             class="btn btn-outline-primary" />
                </div>
            </div>
        </div>
    </div>

    <%-------------------- Termina Panel Botones --------------------%>


    
    <table>
       
      
            
          <tr>
            <td></td>
            <td></td>
            <td> 
                <asp:Label runat="server" ID="lblVencimiento" 
                    style="color: #000066"></asp:Label></td>
       </tr>
        <tr>
            <td></td>
            <td></td>
            <td> <asp:Label runat="server" ID="lblAdvertencia" ForeColor="Red" ></asp:Label></td>
        </tr>
        
        <tr>
            <td></td>
            <td></td> 
        </tr>
    </table>
  
   
</asp:Content>