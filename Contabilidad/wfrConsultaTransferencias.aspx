<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrConsultaTransferencias.aspx.cs" Inherits="GAFWEB.wfrConsultaTransferencias" %>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
        <ContentTemplate>

    <h1>CFDI</h1>
	<p>
		<asp:Label runat="server" ID="lblError" ForeColor="Red" />
	</p>
<%--   
    <table>
    
       
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
		
        </table>
        <table>
		<tr>
			
			<td>
				&nbsp;</td>
			
			<td style="text-align: right;"><asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
			 onclick="btnBuscar_Click" class="btn btn-outline-primary"/></td>
			<td>&nbsp;</td>
            <td>&nbsp;</td>
		</tr>
	</table>--%>
    
    <br />
    <div style="text-align: right">
    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar Todo" onclick="btnEliminar_Click"  
                    class="btn btn-outline-primary"  />
    </div>
    
    <div style="height:100%; overflow-y: scroll;>
        <asp:HiddenField runat="server" ID="hidSel"  Value="Sel"/>
        <asp:GridView ShowFooter="True" runat="server"  CssClass="style124" 
            ID="gvFacturas" AutoGenerateColumns="False" DataKeyNames="ID"
		onrowcommand="gvFacturas_RowCommand" AllowPaging="True" PageSize="10" Width="100%" Height="90%"
		onpageindexchanging="gvFacturas_PageIndexChanging" 
		onrowdatabound="gvFacturas_RowDataBound">
		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="#A8CF38" CssClass="page2"  Font-Bold="True" />
		<Columns>
		    <asp:BoundField HeaderText="IDEmpresa" DataField="IdEmpres" />
		    <asp:BoundField HeaderText="Empresa" DataField="RazonSocialEmpresa" />
			 <asp:BoundField HeaderText="IDCliente" DataField="IdCliente" />
		    <asp:BoundField HeaderText="Cliente" DataField="RazonSocialCliente" />
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
             <asp:ButtonField ButtonType="Link" Text="Facturar" CommandName="Editar" />
      	</Columns>
	</asp:GridView> 
    </div>
	
    <br />
   

  <asp:ModalPopupExtender runat="server" ID="mpePagar" TargetControlID="btnpagarDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarPagar" PopupControlID="pnlPagar"/>
	<asp:Panel runat="server" ID="pnlPagar" CssClass="page2" BackColor="White" Width="600px" style="text-align: center;">
		<h1>Comprobante de Pago Factura</h1>
	    <br />
	    No. de Folio: <asp:Label runat="server" ID="lblFolioPago" Text="?" />
		<p>
			<%-- <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" MaxFiles="10" />--%>
             <asp:FileUpload runat="server" ID="archivoPagos" Width="300px" />
             <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server"
              ErrorMessage="Solo Imagenes" ControlToValidate="archivoPagos" 
              ValidationExpression= "(.*).(.jpg|.JPG|.gif|.GIF|.jpeg|.JPEG|.bmp|.BMP|.png|.PNG)$" />
             <asp:Button runat="server" ID="btnSubir" Text="Subir" class="btn btn-outline-primary" 
                onclick="btnSubir_Click"/>
             </p>
		<br />
		<asp:Button runat="server" ID="btnCerrarPagar" Text="Cerrar" class="btn btn-outline-primary"
            onclick="btnCerrarPagar_Click" />
		<br /><br />

        	<asp:Label runat="server" ID="lblIdventa" Visible="False" />
		   <asp:Label runat="server" ID="lblErrorPago" ForeColor="Red" />
           <br />
    
	</asp:Panel>
    	<asp:Button runat="server" ID="btnPagarDummy" style="display: none;" class="btn btn-outline-primary"/>
       
       </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger  ControlID="btnSubir"/>
           <asp:PostBackTrigger ControlID="gvFacturas" />
       </Triggers>
       </asp:UpdatePanel>

</asp:Content>