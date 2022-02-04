<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrEmpresasConsulta.aspx.cs" Inherits="GAFWEB.wfrEmpresasConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            color: #000000;
            font-weight: normal;
            border:1px;
        }
        .style124
    {
        border-style:solid;
        border-left:  #A8CF38;
    border-right:  #A8CF38;
    border-top:  #A8CF38;
    border-bottom:  #b3b3b3;
        border-radius: 15px ;
        border-width: 1px;
         padding: 2px 4px;
        height:40%rem;
        width:60%;
        color: #000101;
        background-color:transparent;
        
        background-color: #f2f2f2;
        text-align: center;
    }
    </style>
    
<link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h1 class="style1">Empresas</h1>
    <div >
   <table width="50%" style=" text-align:left"  >
   <tr>
   <td align="right">Linea:</td>
   <td><asp:DropDownList runat="server" ID="ddlLinea" AutoPostBack="True" 
           onselectedindexchanged="ddlLinea_SelectedIndexChanged" >
                  
                    <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                    <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                    <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                    <asp:ListItem Value="D" Text="D" ></asp:ListItem> 
            </asp:DropDownList>
                
    
            
            </td>
   </tr>
   </table>
    <asp:GridView runat="server" ID="gvEmpresas"  CssClass="style124" 
        AutoGenerateColumns="False" onrowcommand="gvEmpresas_RowCommand"
     DataKeyNames="IdEmpresa" AllowPaging="True" 
        onpageindexchanging="gvEmpresas_PageIndexChanging" >
        <EmptyDataTemplate>
            No se encontraron registros.
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField   HeaderText="RFC"   DataField="RFC" />
            <asp:BoundField HeaderText="Razón Social" DataField="RazonSocial" />
            <asp:BoundField   HeaderText="Linea"   DataField="Linea" />
            <asp:ButtonField Text="Editar" CommandName="EditarEmpresa" />
        <%--    <asp:ButtonField Text="Sucursales" CommandName="EditarSucursal" />
       --%>     <asp:ButtonField Text="Conceptos" CommandName="EditarConceptos" />
        </Columns>
    </asp:GridView>
    <br />
    <div align="right" style="width:70%">
        <asp:Button runat="server" ID="btnNuevaEmpresa" Text="Nueva Empresa" onclick="btnNuevaEmpresa_Click"  class="btn btn-outline-primary" />
    </div>
    </div>
</asp:Content>
