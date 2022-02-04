<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrConsultaEmitidos.aspx.cs" Inherits="GAFWEB.wfrConsultaEmitidos" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
	    tr:nth-child(even){background-color: #EBEBEB}
	.mpeBack
	{
		background-color: Gray;
		filter: alpha(opacity=70);
		opacity: 0.7;
	}
	.style124
    {
        border-left:  1px none #A8CF38;
    border-right:  1px none #A8CF38;
    border-top:  1px none #A8CF38;
    border-bottom:  1px none #b3b3b3;
        border-radius: 15px;
        padding: 2px 4px;
        color: #000101;
        background-color:transparent;
        
     background-color: #DCD9D9;
        text-align: center;
        }
	</style>
  <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />

    
 
    <style type="text/css">
     
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js">
    
</script>
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "images/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "images/plus.png");
        $(this).closest("tr").next().remove();
    });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>ReporteS EMITIDOS</h1>
	<p>
		<asp:Label runat="server" ID="lblError" ForeColor="Red" />
	</p>
   
    <table>
    <tr>
     <td align="right"> <asp:Label ID="lblLinea" runat="server" Text="Linea:"></asp:Label></td>
          <td><asp:DropDownList runat="server" ID="ddlLinea" AutoPostBack="True" 
           onselectedindexchanged="ddlLinea_SelectedIndexChanged" >
                  
                    <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                    <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                    <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                    <asp:ListItem Value="D" Text="D" ></asp:ListItem> 
            </asp:DropDownList>
            </td>    
       </tr>
       <tr>
        <td>
	Empresa:</td><td colspan="3"><asp:DropDownList runat="server" ID="ddlEmpresas" AutoPostBack="true" DataTextField="RazonSocial"
		AppendDataBoundItems="True" DataValueField="idEmpresa" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged" />
        </td>
        </tr>
       
		<tr>
			<td>Fecha Inicial:</td>
			<td>
				<asp:TextBox runat="server" ID="txtFechaInicial" Width="75px" />
				<asp:CompareValidator runat="server" ID="cvFechaInicial" ControlToValidate="txtFechaInicial" Display="Dynamic" 
				 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" />
				<asp:CalendarExtender runat="server" ID="ceFechaInicial" Animated="False" PopupButtonID="txtFechaInicial" TargetControlID="txtFechaInicial" Format="dd/MM/yyyy" />
			</td>
			<td style="text-align: right">Fecha Final:</td>
			<td>
				<asp:TextBox runat="server" ID="txtFechaFinal" />
				<asp:CompareValidator runat="server" ID="cvFechaFinal" ControlToValidate="txtFechaFinal" Display="Dynamic" 
				 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" />
				<asp:CalendarExtender runat="server" ID="ceFechaFinal" Animated="False" PopupButtonID="txtFechaFinal" TargetControlID="txtFechaFinal" Format="dd/MM/yyyy" />
			</td>
			
		</tr>
		<tr>
			<td>Clientes:</td>
			<td colspan="2"><asp:DropDownList runat="server" ID="ddlClientes" AppendDataBoundItems="True" DataTextField="RazonSocial"
			 DataValueField="idCliente" Width="400px" /></td>
			<td></td>
			<td><asp:TextBox runat="server" ID="txtTexto" Visible="False" /></td>
		
		</tr>
        </table>
        <table>
		<tr>
			
			<td>
				<asp:RadioButtonList RepeatDirection="Horizontal" ID="rbStatus" runat="server" >
					<asp:ListItem Text="Todas" Value="Todos" Selected="True"/>
					
					<asp:ListItem Text="Ingreso" Value="Ingreso"/>
				    <asp:ListItem Text="Egreso" Value="Egreso" />
                    <asp:ListItem Text="Pago" Value="Pago"/>
				
				</asp:RadioButtonList>

			</td>
			
			<td style="text-align: right;"><asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
			 onclick="btnBuscar_Click" class="btn btn-outline-primary"/></td>
			<td><asp:Button runat="server" ID="btnExportar" Text="Exportar Excel" 
                    onclick="btnExportar_Click" class="btn btn-outline-primary" Width="133px"/></td>
            <td>&nbsp;</td>
		</tr>
	</table><br />
    <div style="height:100%; overflow-y: scroll;>
        <asp:HiddenField runat="server" ID="hidSel"  Value="Sel"/>
        <asp:GridView ShowFooter="True" runat="server"  CssClass="style124" 
            ID="gvFacturas" AutoGenerateColumns="False" DataKeyNames="idventa,Uid,IdCliente"
		onrowcommand="gvFacturas_RowCommand" AllowPaging="True" PageSize="10" Width="100%" Height="90%"
		onpageindexchanging="gvFacturas_PageIndexChanging" OnRowDataBound="OnRowDataBound" 	>
		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="#A8CF38" CssClass="page2"  Font-Bold="True" />
		<Columns>
        <asp:TemplateField ItemStyle-Width ="50px">
                <ItemTemplate  >
                    <img alt = "" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none; background-color:White" >
                        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                             <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                             <asp:BoundField DataField="ClaveUnidad" HeaderText="ClaveUnidad" />
                             <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                             <asp:BoundField DataField="ClaveProdServ" HeaderText="ClaveProdServ" />
                             <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                              <asp:BoundField DataField="ValorUnitario" HeaderText="ValorUnitario" />
                              <asp:BoundField DataField="Importe" HeaderText="Importe" />
                              <asp:BoundField DataField="IVA" HeaderText="IVAConcepto" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
			<asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Folio Fiscal" DataField="Uid" />
           	<asp:BoundField HeaderText="Fecha Emisión" DataField="Fecha" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="Empresa" DataField="nombreempresa"/>
            <asp:BoundField HeaderText="EmpresaRFC" DataField="RFCEmpresa"/>
            <asp:BoundField HeaderText="Cliente" DataField="nombrecliente"/>
            <asp:BoundField HeaderText="ClienteRFC" DataField="RFCCliente"/>
            <asp:BoundField HeaderText="Tipo Comprobante" DataField="TipoDocumentoStr"/>
            <asp:BoundField HeaderText="SubTotal" DataField="Subtotal" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
			 <asp:BoundField HeaderText="IVA" DataField="IVA" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
	        <asp:BoundField HeaderText="FormaPago" DataField="FormaPago"/>
            <asp:BoundField HeaderText="Moneda" DataField="Moneda"/>
         	<asp:ButtonField ButtonType="Link" Text="XML" CommandName="DescargarXml" />
			<asp:ButtonField ButtonType="Link" Text="PDF" CommandName="DescargarPdf" />
			
           	</Columns>
	</asp:GridView> 
    </div>
	
   
	<asp:GridView ID="gvFacturaCustumer"  CssClass="page2" Visible="False" OnRowDataBound="OnRowDataBound2"
     runat="server" AutoGenerateColumns="False" DataKeyNames="idventa" >
        <Columns>
         <asp:TemplateField ItemStyle-Width ="50px">
                <ItemTemplate  >
                    <img alt = "" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none; background-color:White" >
                        <asp:GridView ID="gvOrders2" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                             <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                             <asp:BoundField DataField="ClaveUnidad" HeaderText="ClaveUnidad" />
                             <asp:BoundField DataField="Unidad" HeaderText="Unidad" />
                             <asp:BoundField DataField="ClaveProdServ" HeaderText="ClaveProdServ" />
                             <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                              <asp:BoundField DataField="ValorUnitario" HeaderText="ValorUnitario" />
                              <asp:BoundField DataField="Importe" HeaderText="Importe" />
                              <asp:BoundField DataField="IVA" HeaderText="IVAConcepto" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Folio Fiscal" DataField="Uid" />
           	<asp:BoundField HeaderText="Fecha Emisión" DataField="Fecha" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="Empresa" DataField="nombreempresa"/>
            <asp:BoundField HeaderText="EmpresaRFC" DataField="RFCEmpresa"/>
            <asp:BoundField HeaderText="Cliente" DataField="nombrecliente"/>
            <asp:BoundField HeaderText="ClienteRFC" DataField="RFCCliente"/>
            <asp:BoundField HeaderText="Tipo Comprobante" DataField="TipoDocumentoStr"/>
            <asp:BoundField HeaderText="SubTotal" DataField="Subtotal" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
			 <asp:BoundField HeaderText="IVA" DataField="IVA" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
	        <asp:BoundField HeaderText="FormaPago" DataField="FormaPago"/>
            <asp:BoundField HeaderText="Moneda" DataField="Moneda"/>
			
        </Columns>
    </asp:GridView>
    <br />
   
	<asp:ModalPopupExtender runat="server" ID="mpePagar" TargetControlID="btnpagarDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarPagar" PopupControlID="pnlPagar"/>
	<asp:Panel runat="server" ID="pnlPagar" CssClass="page2" BackColor="White" Width="600px" style="text-align: center;">
		<h1>Pagar Factura</h1>
		<asp:Label runat="server" ID="lblIdventa" Visible="False" />
		<asp:Label runat="server" ID="lblErrorPago" ForeColor="Red" />
	    No. de Folio: <asp:Label runat="server" ID="lblFolioPago" />
		<p>
			Fecha Pago: <asp:TextBox runat="server" ID="txtFechaPago" Text='<%# DateTime.Now %>' />
			<asp:CompareValidator runat="server" ID="cvFechaPago" ControlToValidate="txtFechaPago" Display="Dynamic" 
			 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" ValidationGroup="Pago" />
			<asp:CalendarExtender runat="server" ID="ceFechaPago" TargetControlID="txtFechaPago" PopupButtonID="txtFechaPago" Format="dd/MM/yyyy" />
			<asp:RequiredFieldValidator runat="server" ID="rfvFechaPago" ErrorMessage="* Requerido" ControlToValidate="txtFechaPago"
			 ValidationGroup="Pago" Display="Dynamic"/>
		</p>
		<p>
			Referencia: <asp:TextBox runat="server" ID="txtReferenciaPago" Width="300px" />
			<asp:RequiredFieldValidator runat="server" ID="rfvReferenciaPago" ErrorMessage="* Requerido"
			 ControlToValidate="txtReferenciaPago" ValidationGroup="Pago" Display="Dynamic"/>
		</p>
		<asp:Button runat="server" ID="btnPagar" Text="Pagar" onclick="btnPagar_Click" ValidationGroup="Pago" class="btn btn-outline-primary"/>&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnCerrarPagar" Text="Cancelar" class="btn btn-outline-primary"
            onclick="btnCerrarPagar_Click" />
		<br /><br />
	</asp:Panel>

	<asp:ModalPopupExtender runat="server" ID="mpeEmail" TargetControlID="btnEmailDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarEmail" PopupControlID="pnlEmail" />
	<asp:Panel runat="server" ID="pnlEmail" style="text-align: center;" CssClass="page2" Width="800px" BackColor="White">
		<h1>Direcciones de envio</h1>
		<asp:Label runat="server" ID="lblGuid" Visible="False" />
		<p>
			Se enviara a: <asp:Label runat="server" ID="lblEmailCliente" />
		</p>
		<p>
			Correos adicionales:
			<asp:TextBox runat="server" ID="txtEmails" Width="250px" />&nbsp;&nbsp;&nbsp;
			<span style="font-size: 8pt;">Separados por comas</span>
		</p>
		<br />
		<asp:Button runat="server" ID="btnEnviarEmail" Text="Enviar" onclick="btnEnviarMail_Click" class="btn btn-outline-primary"/>&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnCerrarEmail" Text="Cancelar" class="btn btn-outline-primary"/>
	</asp:Panel>
	<asp:Button runat="server" ID="btnEmailDummy" style="display: none;" class="btn btn-outline-primary"/>
	<asp:Button runat="server" ID="btnPagarDummy" style="display: none;" class="btn btn-outline-primary"/>
  

</asp:Content>