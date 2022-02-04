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
                <div class = "col-md-5">
                    <asp:TextBox runat="server" ID="txtRFC" CssClass="form-control"/>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRFC" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                </div>     
                <div class = "col-md-1">
                 <asp:Button runat="server" ID="btnBuscar" Text="Buscar" onclick="btnBuscar_Click"  class="btn btn-outline-primary"/>     
                 </div>                     
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtRazonSocial" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRazonSocial" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
       
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblCalle" runat="server" Text="Calle"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblNoExterior" runat="server" Text="Número Exterior"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtExt" CssClass="form-control"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblNoInterior" runat="server" Text="Número Interior"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtInt" CssClass="form-control"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblColonia" runat="server" Text="Colonia"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtColonia" CssClass="form-control"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblMunicipio" runat="server" Text="Municipio"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtMunicipio" CssClass="form-control"/>
                </div>                                
            </div>
          
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control"/>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblPais" runat="server" Text="País"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtPais" CssClass="form-control">México</asp:TextBox>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblCP" runat="server" Text="Código Postal"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtCP" CssClass="form-control" MaxLength="5" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalido CP."
                     ControlToValidate="txtCP" 
                        ValidationExpression="^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$"  
                        Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                </div>                                
            </div>
          
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalido email."
                     ControlToValidate="txtEmail" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  
                        Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblBcc" runat="server" Text="CCO"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtBcc" CssClass="form-control"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalido email."
                     ControlToValidate="txtBcc" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  Display="Dynamic" ForeColor="Red">
                    </asp:RegularExpressionValidator>
                </div>                                
            </div>
           
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Porcentaje Promotor"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtPPromotor" CssClass="form-control"/>
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
                    <asp:TextBox runat="server" ID="txtPCliente" CssClass="form-control"/>
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
                    <asp:TextBox runat="server" ID="txtPEmpresa" CssClass="form-control"/>
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
                    <asp:TextBox runat="server" ID="txtPContacto" CssClass="form-control"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPContacto" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtPContacto" ForeColor="Red" Display="Dynamic" ErrorMessage="Dato invalido" 
                        ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarCliente" />
        
                </div>                                
            </div>

             <p>
                <asp:Label runat="server" ID="lblMensaje" ForeColor="Red" />
            </p>
            <div class = "form-group row justify-content-center">
                <div class = "col-md-8">
                    <asp:Button runat="server" ID="btnSave" Text="Guardar" onclick="btnSave_Click"  
                    class="btn btn-outline-primary" ValidationGroup="AgregarCliente" Visible="False"/>
                    &nbsp;&nbsp
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" class="btn btn-outline-primary" 
                    onclick="btnCancel_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
