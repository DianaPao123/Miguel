<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrClientes.aspx.cs" Inherits="GAFWEB.wfrClientes" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />

    
    <div class = "card">
        <div class = "card-header">
            Editar Cliente
        </div>
        <div class = "card-body">
            <p>
                <asp:Label runat="server" ID="lblError" ForeColor="Red" />
            </p>
           
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblRfc" runat="server" Text="RFC"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtRFC" CssClass="form-control" 
                        Enabled="False"/>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRFC" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                </div>     
               <%-- <div class = "col-md-1">
                 <asp:Button runat="server" ID="btnBuscar" Text="Buscar" onclick="btnBuscar_Click"  class="btn btn-outline-primary"/>     
                 </div>--%>                     
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtRazonSocial" CssClass="form-control" 
                        Enabled="False"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRFC" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
       
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblCalle" runat="server" Text="Calle"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblNoExterior" runat="server" Text="Número Exterior"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtExt" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblNoInterior" runat="server" Text="Número Interior"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtInt" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblColonia" runat="server" Text="Colonia"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtColonia" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblMunicipio" runat="server" Text="Municipio"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtMunicipio" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
          
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblPais" runat="server" Text="País"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtPais" CssClass="form-control" 
                        Enabled="False">México</asp:TextBox>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblCP" runat="server" Text="Código Postal"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtCP" CssClass="form-control" MaxLength="5" 
                        Enabled="False" />
                </div>                                
            </div>
          
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblBcc" runat="server" Text="CCO"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtBcc" CssClass="form-control" 
                        Enabled="False"/>
                </div>                                
            </div>
           
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Porcentaje Promotor"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtPPromotor" CssClass="form-control" 
                        Enabled="False"/>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPPromotor" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txtPPromotor" ForeColor="Red" Display="Dynamic" ErrorMessage="Dato invalido" 
                        ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarCliente" />
            </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Porcentaje Cliente"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtPCliente" CssClass="form-control" 
                        Enabled="False" EnableTheming="True"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPCliente" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtPCliente" ForeColor="Red" Display="Dynamic" ErrorMessage="Dato invalido" 
                        ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarCliente" />
             </div>                                
            </div>
             <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Porcentaje Despacho"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtPEmpresa" CssClass="form-control" 
                        Enabled="False"/>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPEmpresa" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtPEmpresa" ForeColor="Red" Display="Dynamic" ErrorMessage="Dato invalido" 
                        ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarCliente" />
         </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label4" runat="server" Text="Porcentaje Contacto"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtPContacto" CssClass="form-control" 
                        Enabled="False"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPContacto" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtPContacto" ForeColor="Red" Display="Dynamic" ErrorMessage="Dato invalido" 
                        ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarCliente" />
              </div>                                
            </div>
             <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label5" runat="server" Text="Régimen Fiscal"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:DropDownList runat="server" ID="ddlRegimen" 
                    CssClass="form-control"  >
                 <asp:ListItem Value="601" Text="General de Ley Personas Morales"	 runat="server" />
 <asp:ListItem Value="603" Text="Personas Morales con Fines no Lucrativos"	 runat="server" />
 <asp:ListItem Value="605" Text="Sueldos y Salarios e Ingresos Asimilados a Salarios"	 runat="server" />
 <asp:ListItem Value="606" Text="Arrendamiento"	 runat="server" />
 <asp:ListItem Value="608" Text="Demás ingresos"	 runat="server" />
 <asp:ListItem Value="609" Text="Consolidación"	 runat="server" />
 <asp:ListItem Value="610" Text="Residentes en el Extranjero sin Establecimiento Permanente en México"	 runat="server"/>
 <asp:ListItem Value="611" Text="Ingresos por Dividendos (socios y accionistas)"	 runat="server" />
 <asp:ListItem Value="612" Text="Personas Físicas con Actividades Empresariales y Profesionales"	 runat="server" />
 <asp:ListItem Value="614" Text="Ingresos por intereses"	 runat="server" />
 <asp:ListItem Value="616" Text="Sin obligaciones fiscales"	 runat="server" />
 <asp:ListItem Value="620" Text="Sociedades Cooperativas de Producción que optan por diferir sus ingresos"	 runat="server" />
 <asp:ListItem Value="621" Text="Incorporación Fiscal"	 runat="server" />
 <asp:ListItem Value="622" Text="Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras"	 runat="server" />
 <asp:ListItem Value="623" Text="Opcional para Grupos de Sociedades"	 runat="server" />
 <asp:ListItem Value="624" Text="Coordinados"	 runat="server" />
 <asp:ListItem Value="625" Text="Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas"	 runat="server" />
 <asp:ListItem Value="628" Text="Hidrocarburos"	 runat="server" />
 <asp:ListItem Value="607" Text="Régimen de Enajenación o Adquisición de Bienes"	 runat="server" />
 <asp:ListItem Value="629" Text="De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales"	 runat="server" />
 <asp:ListItem Value="630" Text="Enajenación de acciones en bolsa de valores"	 runat="server" />
 <asp:ListItem Value="615" Text="Régimen de los ingresos por obtención de premios"	 runat="server" />
   <asp:ListItem Value="626" Text="Regimen Simplificado de Confianza"	 runat="server" />        
                </asp:DropDownList>
              
                </div>                                
            </div>

             <p>
                <asp:Label runat="server" ID="lblMensaje" ForeColor="Red" />
            </p>
            <div class = "form-group row justify-content-center">
                <div class = "col-md-8">
                    <asp:Button runat="server" ID="btnSave" Text="Validar" onclick="btnSave_Click"  
                    class="btn btn-outline-primary" ValidationGroup="AgregarCliente" Visible="False"/>
                    &nbsp;&nbsp
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" class="btn btn-outline-primary" 
                    onclick="btnCancel_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
