<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrSucursales.aspx.cs" Inherits="GAFWEB.wfrSucursales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <style>
     .style124
    {
       border-style: solid;
       border:1px;
        border-width: 1px;
        border-radius: 15px;
         padding: 2px 4px;
          height:40%rem;
        width:50%;
        color: #000101;
            background-color:transparent;
        background-color: #f2f2f2;
        text-align: center;
    }
     .style130
     {
         width: 164px;
         text-align: right;
     }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />
    <h1>Sucursal</h1>
    <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    <table  class="style124">
        <tr>
            <td class="style130">Nombre:</td>
            <td>
                <asp:HiddenField runat="server" ID="txtIdEmpresa"/>
                <asp:TextBox runat="server" ID="txtNombre" Width="400px" />
                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ErrorMessage="* Requerido" Display="Dynamic"
                 ControlToValidate="txtNombre" />
            </td>
        </tr>
        <tr>
            <td class="style130">Lugar de expedición:</td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlEstado_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMunicipio" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlMunicipio_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCP" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlCP_SelectedIndexChanged">
                </asp:DropDownList>

                <%--
                <asp:TextBox runat="server" ID="txtLugarExpedicion"  Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvLugarExpedicion" ErrorMessage="* Requerido" Display="Dynamic"
                 ControlToValidate="txtLugarExpedicion" />
                 --%>
            </td>
        </tr>
        <tr>
            <td class="style130">Domicilio:</td>
            <td>
                <asp:TextBox runat="server" ID="txtDomicilio"  Width="400px" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="* Requerido" Display="Dynamic"
                 ControlToValidate="txtDomicilio" />
            </td>
        </tr>
    </table>
    <div align="right" style="width:70%">
        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" onclick="btnGuardar_Click" class="btn btn-outline-primary"/>&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" class="btn btn-outline-primary"
            CausesValidation="False" onclick="btnCancelar_Click" />
    </div>
</asp:Content>
