<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstadoCuenta.aspx.cs" Inherits="GAFWEB.EstadoCuenta" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />

<asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>
                        <div class="tab-content">
                           
                             <div class="card mt-2">
                                <div class="panel panel-default">
                                     <div class="panel-heading">
                                        <strong>Estado de Cuenta</strong></div>
                                    <div class="panel-body">
                                     <div class = "row"> 
                                        <div class = "form-group col-md-3 col-sm-6">
                                            <asp:Label ID="Label6" runat="server" Text="Línea"></asp:Label>
                                            <asp:DropDownList ID="ddlOrigenLineaEmpresa" runat="server" CssClass="form-control"
                                                CausesValidation="false" AutoPostBack="True" 
                                                onselectedindexchanged="ddlOrigenLineaEmpresa_SelectedIndexChanged" >
                                               <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                                               <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                                               <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                                               <asp:ListItem Value="D" Text="D" ></asp:ListItem> 
                                            </asp:DropDownList>
                                           </div>
                                           </div>
                                            <div class = "row"> 
                                        <div class="form-group col-md-9 col-sm-6">
                                            <asp:Label ID="Label7" runat="server" Text="Empresa"></asp:Label>
                                            <asp:DropDownList ID="ddlOrigenEmpresa" runat="server" 
                                            CssClass="form-control" DataTextField="nombreCuenta" DataValueField="idEmpresa"
                                                CausesValidation="false" ValidationGroup="frmTab1" AutoPostBack="True" 
                                                onselectedindexchanged="ddlOrigenEmpresa_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        </div>
                                         <div class = "row"> 
                                        <div class="form-group col-md-9 col-sm-6">
                                            <asp:Label ID="Label8" runat="server" Text="Cuenta"></asp:Label>
                                            <asp:DropDownList ID="ddlOrigenCuenta" runat="server" CssClass="form-control"
                                            DataTextField="nombreBanco" DataValueField="numeroCuenta"
                                                CausesValidation="false" ValidationGroup="frmTab1">
                                            </asp:DropDownList>
                                        </div>
                                        </div>
                                         <div class = "row"> 
                                       <div class="form-group col-md-5">
                                        <asp:Button ID="btnConsultarMovimiento" runat="server" class="btn btn-outline-primary" OnClick="btnConsultarMovimiento_Click" Text="Consultar" 
                        ValidationGroup="frmTab1"/>
                </div>
                
                                       </div>


                                       <div style="height:100%; overflow-y: scroll;>
        <asp:HiddenField runat="server" ID="hidSel"  Value="Sel"/>
        <asp:GridView ShowFooter="True" runat="server"  CssClass="style124" 
            ID="gvFacturas" AutoGenerateColumns="False" DataKeyNames="idMovimiento"
		onrowcommand="gvFacturas_RowCommand" AllowPaging="True" PageSize="10" Width="100%" Height="90%"
		onpageindexchanging="gvFacturas_PageIndexChanging" 
		onrowdatabound="gvFacturas_RowDataBound">
		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="#A8CF38" CssClass="page2"  Font-Bold="True" />
		<Columns>
			<asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}" />
			<asp:BoundField HeaderText="TipoMovimiento" DataField="tipoDocumento" />
		    <asp:BoundField HeaderText="Operacion" DataField="operacion"  />
            <asp:BoundField HeaderText="Monto" DataField="monto"  />
            <asp:BoundField HeaderText="Fecha Emisión" DataField="fechaRegistro" DataFormatString="{0:d}" />
			<asp:BoundField HeaderText="Timbrado" DataField="EsFacturado" />
             <asp:ButtonField ButtonType="Link" Text="Timbrar" CommandName="Timbrar" />
         
		</Columns>
	</asp:GridView> 
    </div>
                                    </div>
                                </div>
                            </div>
                         
                            
                        </div>
                    
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
