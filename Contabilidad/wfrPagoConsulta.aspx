<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrPagoConsulta.aspx.cs" Inherits="GAFWEB.wfrPagoConsulta" %>
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
        }<%--
        
        img {  width:100%; height:100%; }--%>
        
	</style>
   
<link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
        <ContentTemplate>

    <h1>Reporte de&nbsp; PAGOS</h1>
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
        <td></td>
        </tr>
        <tr>
        <td>
	Empresa:</td><td colspan="3"><asp:DropDownList runat="server" ID="ddlEmpresas"  AppendDataBoundItems="True" DataTextField="RazonSocial"
		DataValueField="idEmpresa" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged" />
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
			
		</tr>
        </table>
        <table>
		<tr>
			
			<td>
				<asp:RadioButtonList RepeatDirection="Horizontal" ID="rbStatus" runat="server" Visible="false">
					<asp:ListItem Text="Todas" Value="9" Selected="True"/>
					
					<asp:ListItem Text="Pendientes" Value="0"/>
				    <asp:ListItem Text="Pagadas" Value="1" />
                    <%--<asp:ListItem Text="Canceladas" Value="3"/>--%>
				
				</asp:RadioButtonList>

			</td>
			
			<td style="text-align: right;"><asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
			 onclick="btnBuscar_Click" class="btn btn-outline-primary"/></td>
			<td>&nbsp;</td>
            <td>&nbsp;</td>
		</tr>
	</table><br />


    <div style="height:100%; overflow-y: scroll;">
        <asp:HiddenField runat="server" ID="hidSel"  Value="Sel"/>
        <asp:GridView ShowFooter="True" runat="server"  CssClass="style124" 
            ID="gvFacturas" AutoGenerateColumns="False" DataKeyNames="idEmpresa,idCliente,idPreFactura,CFDI"
		onrowcommand="gvFacturas_RowCommand" AllowPaging="True" PageSize="10" Width="100%" Height="90%"
		onpageindexchanging="gvFacturas_PageIndexChanging" 
		onrowdatabound="gvFacturas_RowDataBound">
		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="#A8CF38" CssClass="page2"  Font-Bold="True" />
		<Columns>
			<asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Cliente" DataField="RazonSocial" />
			<asp:BoundField HeaderText="Empresa" DataField="nombreEmpresa"  />
            <asp:BoundField HeaderText="Fecha Emisión" DataField="Fecha" DataFormatString="{0:d}" />
			
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
    <%--        <asp:BoundField HeaderText="Usuario" DataField="IDUsuario"/>
	--%>	             
      <%--     <asp:ButtonField ButtonType="Link" Text="Generar"  CommandName="GenerarCFDI"   />
          <asp:ButtonField ButtonType="Link" Text="PDF" CommandName="DescargarPdf" />
	        <asp:ButtonField ButtonType="Link" Text="Revisar" CommandName="Editar" />
              <%--  <asp:ButtonField ButtonType="Link" Text="SubirPago" CommandName="Pagar" />
       	--%>	
        <asp:ButtonField ButtonType="Link" Text="Ver Pago" CommandName="Pagar" />
		</Columns>
	</asp:GridView> 
    </div>
	
   
	<asp:GridView ID="gvFacturaCustumer"  CssClass="page2" Visible="False" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Serie" DataField="Serie" />
			<asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:d}" />
		   <asp:BoundField HeaderText="SubTotal" DataField="SubTotal" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
         <%--   <asp:BoundField HeaderText="Usuario" DataField="IDUsuario"/>
		--%>	<asp:BoundField HeaderText="Status" DataField="Estatus"/>
        </Columns>
    </asp:GridView>
    <br />
   

  <asp:ModalPopupExtender runat="server" ID="mpePagar" TargetControlID="btnpagarDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarPagar" PopupControlID="pnlPagar"/>
	<asp:Panel runat="server" ID="pnlPagar" CssClass="page2" BackColor="White" Width="600px" Height="600px" style="text-align: center;">
		<h1>Comprobante de Pago Factura</h1>
	    <br />
	    No. de Folio: <asp:Label runat="server" ID="lblFolioPago" Text="?" />
		<p>
     <%--   <div>
        <asp:Label ID="Label23" runat="server" Text="Banco:"></asp:Label>
                        <asp:DropDownList ID="ddlBanco" runat="server" CssClass="form-control" 
                            >
                        </asp:DropDownList>

                        <asp:Label ID="Label21" runat="server" Text="Monto"></asp:Label>
                        <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" 
                        TargetControlID="txtMonto" ValidChars="." /> 
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMonto" 
                        Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="Red" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" 
                        ValidationGroup="CrearPagos" />
                    

       </div>--%>
       <br />
			<%-- <cc1:FileUploaderAJAX ID="FileUploaderAJAX1" runat="server" MaxFiles="10" />--%>
             <%--<asp:FileUpload runat="server" ID="archivoPagos" Width="300px" />
             <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server"
              ErrorMessage="Solo Imagenes" ControlToValidate="archivoPagos" 
              ValidationExpression= "(.*).(.jpg|.JPG|.gif|.GIF|.jpeg|.JPEG|.bmp|.BMP|.png|.PNG)$" />
             <asp:Button runat="server" ID="btnSubir" Text="Subir" class="btn btn-outline-primary" 
                onclick="btnSubir_Click"/>
             </p>--%>
        <div style="width:600px;height:400px;">

      <%--  <div style="width:600px;height:400px;overflow:auto;">
     --%>      <img alt="" src="data:image/jpeg;" id="IMA" runat="server"  width="600" height="400"  />
       </div> 
		<asp:Button runat="server" ID="btnCerrarPagar" Text="Cerrar" class="btn btn-outline-primary"
            onclick="btnCerrarPagar_Click"  ValidationGroup="CrearPagos" />
		<br /><br />

        	<asp:Label runat="server" ID="lblIdventa" Visible="False" />
		   <asp:Label runat="server" ID="lblErrorPago" ForeColor="Red" />
           <br />
     <%--   <asp:Image ID="Image1" runat="server"  ImageUrl="~/images/ajax-loader1.gif" Height="100%" Width="100%" />
	 --%> </asp:Panel>
    	<asp:Button runat="server" ID="btnPagarDummy" style="display: none;" class="btn btn-outline-primary"/>
       


       <asp:ModalPopupExtender runat="server" ID="mpePagarPreFactura" TargetControlID="btnpagarDummyPrefactura" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarPagarPrefactura" PopupControlID="pnlPagarPrefactura"/>
     <asp:Panel runat="server" ID="pnlPagarPrefactura" CssClass="page2" BackColor="White" Width="600px" style="text-align: center;">
		<h1>Pago de PreFactura</h1>
	    <br />
	     ¿Desea pagar la prefactura con No. de Folio: <asp:Label runat="server" ID="lblFolioPagoPrefactura" Text="?" />  ?
		<p>
        <asp:Label runat="server" ID="lblIdPrefactura" Text="?" Visible="false" />
        <div>
      	<asp:Button runat="server" ID="btnPagar" Text="Pagar" onclick="btnPagar_Click" ValidationGroup="Pago" class="btn btn-outline-primary"/>&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnCerrarPagarPrefactura" Text="Cerrar" class="btn btn-outline-primary"
            onclick="btnCerrarPagarPrefactura_Click"  ValidationGroup="CrearPagos" />
		<br /><br />

    	</asp:Panel>
     	<asp:Button runat="server" ID="btnPagarDummyPrefactura" style="display: none;" class="btn btn-outline-primary"/>
    
       </ContentTemplate>
       <Triggers>
    <%--   <asp:PostBackTrigger  ControlID="btnSubir"/>
    --%>      <asp:PostBackTrigger ControlID="gvFacturas" />
       </Triggers>
       </asp:UpdatePanel>

</asp:Content>