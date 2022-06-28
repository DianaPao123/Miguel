<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrClientesConsulta.aspx.cs" Inherits="GAFWEB.wfrClientesConsulta" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
<style type="text/css">
 
        .style124
    {
        border-left:  1px none #A8CF38;
    border-right:  1px none #A8CF38;
    border-top:  1px none #A8CF38;
    border-bottom:  3px none #b3b3b3;
        border-radius: 20px ;
        padding: 2px 4px;
        color: #000101;
        background-color:transparent;
        
        background-color: #f2f2f2;
        text-align: left;
    }
        tr:nth-child(even){background-color: #E6E6E6}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />

    <h1>Consulta de Clientes</h1>
    <table>
       <tr>
   <td align="right">Linea:</td>
   <td><asp:DropDownList runat="server" ID="ddlLinea" AutoPostBack="True"  onselectedindexchanged="ddlLinea_SelectedIndexChanged"
       CssClass="form-control form-control-sm" Width="100px" 
            >
                  
                    <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                    <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                    <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                  
            </asp:DropDownList>
                
    
            
            </td>
           <td> &nbsp;&nbsp;&nbsp;</td>
            <td>
        RFC :</td><td> <asp:TextBox runat="server" ID="txtBusqueda" Width="400px" CssClass="form-control form-control-sm" />
        </td>
           <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td align="right">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
            onclick="btnBuscar_Click" class="btn btn-outline-primary" Height="34px" 
            Width="62px" />
                </td>
            </tr>
        </table>
    <br />
    <asp:GridView runat="server" ID="gvClientes" AutoGenerateColumns="False" Width="100%" Height="90%"
        onrowcommand="gvClientes_RowCommand" CssClass="style124"
     DataKeyNames="idCliente" AllowPaging="True" 
        onpageindexchanging="gvClientes_PageIndexChanging"  >
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
     <%--       <asp:BoundField HeaderText="Promotor" DataField="NombreCompleto" />
    --%>   
            <asp:BoundField HeaderText="RFC" DataField="RFC" />
            <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial" />
        <%--      <asp:BoundField HeaderText="PorcentajeCliente" DataField="PorcentajeCliente" />
              <asp:BoundField HeaderText="PorcentajeContacto" DataField="PorcentajeContacto" />
            <asp:BoundField HeaderText="PorcentajeDespacho" DataField="PorcentajeEmpresa" />
            <asp:BoundField HeaderText="PorcentajePromotor" DataField="PorcentajePromotor" />
        --%>
             <asp:BoundField HeaderText="Linea" DataField="Linea" />
            <asp:ButtonField Text="Editar" ButtonType="Link" CommandName="EditarCliente"/>

            <%--<asp:TemplateField>
             <ItemTemplate>
                <asp:LinkButton Text="Eliminar" runat="server" ID="btnEliminarCliente" CommandArgument='<%# Eval("idCliente") %>' CommandName="Eliminar"/>
                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnEliminarCliente" ConfirmText="Confirma que deseas eliminar el registro"/>
             </ItemTemplate>
            </asp:TemplateField>--%>
            </Columns>
    </asp:GridView>
    <p style="width:100%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 
        <asp:Button runat="server" Text="Nuevo Cliente" OnClick="btnNuevoCliente_Click" 
            class="btn btn-outline-primary"  /> </p>
     <p>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    </p>
</asp:Content>
