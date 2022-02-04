<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrFacturasConsultas.aspx.cs" Inherits="GAFWEB.wfrFacturasConsultas" %>
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

    <h1>Reporte de CFDI</h1>
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
       <%--             <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                    <asp:ListItem Value="D" Text="D" ></asp:ListItem> 
      --%>      </asp:DropDownList>
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
        <tr>
        		<td>Promotor:</td>
			<td colspan="2"><asp:DropDownList runat="server" ID="ddlPromotor" AppendDataBoundItems="True" DataTextField="Nombre"
			 DataValueField="idUsuario" Width="400px" /></td>
			<td></td>
			<td><asp:TextBox runat="server" ID="TextBox1" Visible="False" /></td>
        </tr>
        </table>
        <table>
		<tr>
			
			<td>
				<asp:RadioButtonList RepeatDirection="Horizontal" ID="rbStatus" runat="server" Visible="false" >
					<asp:ListItem Text="Todas" Value="Todos" Selected="True"/>
					
					<asp:ListItem Text="Pendientes" Value="Pendiente"/>
				    <asp:ListItem Text="Pagadas" Value="Pagado" />
                    <asp:ListItem Text="Canceladas" Value="Cancelado"/>
				
				</asp:RadioButtonList>

			</td>
			
			<td style="text-align: right;"><asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
			 onclick="btnBuscar_Click" class="btn btn-outline-primary"/></td>
			<td><asp:Button runat="server" ID="btnExportar" Text="Exportar Excel" 
                    onclick="btnExportar_Click" class="btn btn-outline-primary" Width="133px"/></td>
            <td><asp:Button runat="server" Width="168px" ID="btnDescargarTodo" 
                    Text="Descargar Seleccionados" OnClick="btnDescargarTodo_OnClick" 
                    class="btn btn-outline-primary"/></td>
		</tr>
	</table><br />
    <div style="height:100%; overflow-y: scroll;>
        <asp:HiddenField runat="server" ID="hidSel"  Value="Sel"/>
        <asp:GridView ShowFooter="True" runat="server"  CssClass="style124" 
            ID="gvFacturas" AutoGenerateColumns="False" DataKeyNames="Uid,IdCliente,idventa,IdEmpresa"
		onrowcommand="gvFacturas_RowCommand" AllowPaging="True" PageSize="30" Width="100%" Height="90%"
		onpageindexchanging="gvFacturas_PageIndexChanging" 
		onrowdatabound="gvFacturas_RowDataBound">
		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="#A8CF38" CssClass="page2"  Font-Bold="True" />
		<Columns>
        	<asp:BoundField HeaderText="PreFolio" DataField="PreFolio" />
			<asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Folio Fiscal" DataField="Uid" />
            
               <asp:BoundField HeaderText="Cliente" DataField="nombrecliente"/>
                <asp:BoundField HeaderText="Empresa" DataField="nombreempresa"/>
			<asp:BoundField HeaderText="Fecha Emisión" DataField="fecha" DataFormatString="{0:d}" />
		<%--	<asp:BoundField HeaderText="Cliente" DataField="Cliente" />
            <asp:BoundField HeaderText="RFC" DataField="Rfc" />
	--%>		<asp:BoundField HeaderText="SubTotal" DataField="Subtotal" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
			<%--<asp:BoundField HeaderText="I.V.A." DataField="IVA" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Retención I.V.A." DataField="RetIva" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Retención I.S.R." DataField="RetIsr" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="I.E.P.S." DataField="Ieps" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
--%>
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
     <%--       <asp:BoundField HeaderText="Usuario" DataField="Usuario"/>
	--%>		<%--<asp:BoundField HeaderText="Status" DataField="StatusFactura"/>--%>
            <asp:BoundField HeaderText="Fecha Cancelación" DataField="FechaCancelacion" DataFormatString="{0:d}"  />
             <asp:BoundField HeaderText="Operador" DataField="nombreUsuario" />
             <asp:BoundField HeaderText="Promotor" DataField="promotor" />
		<%--	<asp:ButtonField ButtonType="Link" Text="Pagar" CommandName="Pagar" />--%>
			<asp:ButtonField ButtonType="Link" Text="XML" CommandName="DescargarXml" />
			<asp:ButtonField ButtonType="Link" Text="PDF" CommandName="DescargarPdf" />
<%--			<asp:ButtonField ButtonType="Link" Text="Enviar Email" CommandName="EnviarEmail" />--%>
         <%--   <asp:TemplateField  HeaderText="Cancelar">
                <ItemTemplate>
                    <asp:Button class="btn btn-outline-primary"   runat="server" Text='<%# (short)Eval("Cancelado") == 1 ? "Acuse Cancelacion" : "Cancelar"  %>'  CommandName='<%# (short)Eval("Cancelado") == 1 ? "Acuse" : "Cancelar"  %>' ID="btnCancelarf" CommandArgument='<%#Eval("idventa") %>'  />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancelarf" ConfirmText="¿Cancelar Documento?" Enabled='<%# (short)Eval("Cancelado") != 1  %>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField  HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <asp:Button class="btn btn-outline-primary"  runat="server" ID="btnSelectAll" Text='<%# this.SelText %>' CommandName="SelectAll" />
                </HeaderTemplate>
                <ItemTemplate> 
                    
                    <asp:CheckBox ID="cbChecked" runat="server" Checked='<%# (bool)Eval("Seleccionar") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
		</Columns>
	</asp:GridView> 
    </div>
	
   
	<asp:GridView ID="gvFacturaCustumer"  CssClass="page2" Visible="False" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Folio Fiscal" DataField="Uid" />
			<asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}" />
			<%--<asp:BoundField HeaderText="Cliente" DataField="Cliente" />
            <asp:BoundField HeaderText="RFC" DataField="Rfc" />
			<%--<asp:BoundField HeaderText="% I.V.A." DataField="PorcentajeIva" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
			--%><asp:BoundField HeaderText="SubTotal" DataField="Subtotal" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
		<%--	<asp:BoundField HeaderText="I.V.A." DataField="IVA" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Retención I.V.A." DataField="RetIva" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Retención I.S.R." DataField="RetIsr" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="I.E.P.S." DataField="Ieps" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
--%>
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
              <asp:BoundField HeaderText="nombreempresa" DataField="nombreempresa"/>
			<asp:BoundField HeaderText="Status" DataField="StatusFactura"/>
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

       <asp:ModalPopupExtender runat="server" ID="mpeCancelar" TargetControlID="btnCancelarDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarCancelar" PopupControlID="pnlCancelar" />
	<asp:Panel runat="server" ID="pnlCancelar" style="text-align: center;" CssClass="page2"  BackColor="White">
		<h1>Cancelar CFDI</h1>
	 <asp:UpdatePanel ID="up11" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>
    	
        <table class="table" >
           <tr>
		<td>
        	Motivo: <asp:Label runat="server" ID="txtMotivo" />
             <asp:DropDownList runat="server" ID="ddlMotivo" style="margin-left: 0px" 
              AutoPostBack="True"  onselectedindexchanged="ddlMotivo_SelectedIndexChanged" CssClass="form-control2"  >
                         <asp:ListItem runat="server" Value="01" Text="Comprobante emitido con errores con relación" ></asp:ListItem>
                         <asp:ListItem runat="server" Value="02" Text="Comprobante emitido con errores sin relación" ></asp:ListItem>
                         <asp:ListItem runat="server" Value="03" Text="No se llevó a cabo la operación" ></asp:ListItem>
                         <asp:ListItem runat="server" Value="04" Text="Operación nominativa relacionada en la factura global" ></asp:ListItem>
                    </asp:DropDownList>   
		</td>
        </tr>
		
			<tr>
        <td>
			FolioSustituto:
			<asp:TextBox runat="server" ID="txtFolioSustituto" CssClass="form-control0" 
                 />
		</td>
		</tr>
        
     </table>
       </ContentTemplate>
          <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ddlMotivo" EventName="SelectedIndexChanged" /> 
   
     </Triggers>             
        </asp:UpdatePanel>
   
     <table class="table">
		<tr>
        <td><asp:Button runat="server" ID="btnCancelarSAT" Text="Cancelar" onclick="btnCancelarSAT_Click"  class="btn btn-primary"/>&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnCerrarCancelar" Text="Salir" class="btn btn-primary"/>
        </td>
        </tr>
        </table>
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
  	<asp:Button runat="server" ID="btnCancelarDummy" style="display: none;" class="btn btn-primary"/>
 

</asp:Content>