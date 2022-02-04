<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrUsuarios.aspx.cs" Inherits="GAFWEB.wfrUsuarios" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />

    
    <div class = "card">
        <div class = "card-header">
            Editar Usuarios
        </div>
        <div class = "card-body">
            <p>
                <asp:Label runat="server" ID="lblError" ForeColor="Red" />
            </p>
           
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control"/>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNombre" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                </div>     
                                  
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblApellidoP" runat="server" Text="Apellido Paterno"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtApellidoP" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtApellidoP" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
       
                </div>                                
            </div>
               <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Apellido Materno"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtApellidoM" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApellidoM" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
       
                </div>                                
            </div>

            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsuario" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
              
                </div>                                
            </div>
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblContraeña" runat="server" Text="Contraseña"></asp:Label>
                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtContraseña" CssClass="form-control" 
                        TextMode="Password"/>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContraseña" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
                </div>                                
            </div>
                       
            <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>

                </div>
                <div class = "col-md-6">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"/>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarCliente" />
    
                </div>                                
            </div>
           <%-- <div class = "form-group row justify-content-center align-items-center">
                <div class = "col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Tipo Usuario"></asp:Label>

                </div>
                <div class = "col-md-6">
                   <asp:DropDownList runat="server" ID="ddlRol"  CssClass="form-control">
                            <asp:ListItem Value="Operador" Text="Operador" ></asp:ListItem> 
                            <asp:ListItem Value="Promotor" Text="Promotor" ></asp:ListItem> 
                           
                        </asp:DropDownList>
                </div>                                
            </div>--%>
            
                    
            <div class = "form-group row justify-content-center">
                <div class = "col-md-8">
                    <asp:Button runat="server" ID="btnSave" Text="Guardar" onclick="btnSave_Click"  
                    class="btn btn-outline-primary" ValidationGroup="AgregarCliente" />
                    &nbsp;&nbsp
                    <asp:Button runat="server" ID="btnCancel" Text="Cancelar" class="btn btn-outline-primary" 
                    onclick="btnCancel_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
