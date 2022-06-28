<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrFacturasConsulta.aspx.cs" Inherits="GAFWEB.wfrFacturasConsulta" %>
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
       <style type="text/css">
        .overlay  
        {
          position: fixed;
          z-index: 98;
          top: 0px;
          left: 0px;
          right: 0px;
          bottom: 0px;
           background-color:transparent; 
            /*filter: alpha(opacity=80); 
            opacity: 0.8;*/ 
        }
        .overlayContent
        {
          z-index: 99;
          margin: 250px auto;
          width: 80px;
          height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            /*color: #000;*/
        }
        .overlayContent img
        {
          width: 100px;
          height: 100px;
        }
    </style>
    <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>

         <div class="card mt-2">   
            <div class="card-header">
               Reporte de CFDI
            </div>
            <div class ="card-body">
                <div class = "row"> 
            	<p><asp:Label runat="server" ID="lblError" ForeColor="Red" />	</p>
                </div>
                <div class="row mt-2">
          <div class = "col-2 float-right">
                <asp:Label ID="lblLinea" runat="server" Text="Linea:" CssClass="form-text"></asp:Label>
          </div>
           <div class = "col-2">
                 <asp:DropDownList runat="server" ID="ddlLinea" AutoPostBack="True"   CssClass="form-control  mx-3"
           onselectedindexchanged="ddlLinea_SelectedIndexChanged" >
                  
                    <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                    <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                 
                                 </asp:DropDownList>
          </div>
                    </div>
                   <div class="row mt-2">
         <div class = "col-2 justify-content-end"><asp:Label ID="Label1" runat="server" Text="Empresa:" CssClass="form-text"></asp:Label></div>
         <div class = "col-3 ">
            
             <asp:DropDownList runat="server" ID="ddlEmpresas" AutoPostBack="true" DataTextField="RazonSocial" CssClass="form-control form-control-sm  mx-3"
		AppendDataBoundItems="True" DataValueField="idEmpresa" onselectedindexchanged="ddlEmpresas_SelectedIndexChanged" />
             </div>
                       </div>
              <div class="row mt-2">
         <div class = "col-2 justify-content-end"><asp:Label ID="Label2" runat="server" Text="Fecha Inicial:" CssClass="form-text"></asp:Label></div>
           <div class = "col-3 ">
            			 <asp:TextBox runat="server" ID="txtFechaInicial"   CssClass="form-control form-control-sm mx-3" />
				<asp:CompareValidator runat="server" ID="cvFechaInicial" ControlToValidate="txtFechaInicial" Display="Dynamic" 
				 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" />
				<asp:CalendarExtender runat="server" ID="ceFechaInicial" Animated="False" PopupButtonID="txtFechaInicial" TargetControlID="txtFechaInicial" Format="dd/MM/yyyy" />
     	</div>		
        <div class = "col-2 justify-content-end"><asp:Label ID="Label3" runat="server" Text="Fecha Final:" CssClass="form-text"></asp:Label></div>
            <div class = "col-3 ">
              	<asp:TextBox runat="server" ID="txtFechaFinal"   CssClass="form-control form-control-sm mx-3"/>
				<asp:CompareValidator runat="server" ID="cvFechaFinal" ControlToValidate="txtFechaFinal" Display="Dynamic" 
				 ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" />
				<asp:CalendarExtender runat="server" ID="ceFechaFinal" Animated="False" PopupButtonID="txtFechaFinal" TargetControlID="txtFechaFinal" Format="dd/MM/yyyy" />
		</div>
           </div>
               <div class="row mt-2">
                <div class = "col-2 float-right">
                       <asp:Label ID="Label4" runat="server" Text="Clientes:" CssClass="form-text"></asp:Label>
 			</div>
                       <div class = "col-2">
       
                    <asp:DropDownList runat="server" ID="ddlClientes"  CssClass="form-control form-control-sm mx-3" AppendDataBoundItems="True" DataTextField="RazonSocial"
			 DataValueField="idCliente" Width="400px" />
			<asp:TextBox runat="server" ID="txtTexto" Visible="False" />
		</div>
             </div>
		  

            <div class="row mt-2">
                <div class = "col-3 float-right">
       
				<asp:RadioButtonList RepeatDirection="Horizontal" ID="rbStatus" runat="server" Width="100%" >
					
				<asp:ListItem Text="Ingreso" Value="Ingreso"  Selected="True"/>
				    <asp:ListItem Text="Egreso" Value="Egreso" />
                    <asp:ListItem Text="Pago" Value="Pago"/>
				<%--	<asp:ListItem Text="Todas" Value="Todos"/>--%>
				
				
				</asp:RadioButtonList>
             </div>
                  <div class = "col-9 ">
       <asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
			 onclick="btnBuscar_Click" class="btn btn-outline-primary"/></td>
			<td><asp:Button runat="server" ID="btnExportar" Text="Exportar Excel" 
                    onclick="btnExportar_Click" class="btn btn-outline-primary" Width="133px"/></td>
            <td><asp:Button runat="server" Width="168px" ID="btnDescargarTodo" Visible="false"
                    Text="Descargar Seleccionados" OnClick="btnDescargarTodo_OnClick" 
                    class="btn btn-outline-primary"/></td>
		
                      </div>
                  </div>

                </div>
             </div>

                      <br />
        </ContentTemplate>
        <Triggers>
          <%--   <asp:AsyncPostBackTrigger ControlID="btnDescargarTodo" EventName="Click" /> --%>
          <asp:PostBackTrigger ControlID="btnDescargarTodo" />
              <asp:PostBackTrigger ControlID="btnExportar" />
     </Triggers>
        </asp:UpdatePanel>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>
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
    <asp:BoundField HeaderText="MontoPago" DataField="Monto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
        <asp:BoundField HeaderText="SubTotalPago" DataField="SubTotalPago" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
         <asp:BoundField HeaderText="IVAPago" DataField="IVAPago" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
         <asp:BoundField HeaderText="EstausCFDI" DataField="EstusCFDI"/>  
   
     <%--       <asp:BoundField HeaderText="Usuario" DataField="Usuario"/>
	--%>		<%--<asp:BoundField HeaderText="Status" DataField="StatusFactura"/>--%>

            <asp:BoundField HeaderText="Fecha Cancelación" DataField="FechaCancelacion" DataFormatString="{0:d}"  />
	     	<%--<asp:ButtonField ButtonType="Link" Text="Pagar" CommandName="Pagar" /--%>
			<asp:ButtonField ButtonType="Link" Text="XML" CommandName="DescargarXml" />
			<asp:ButtonField ButtonType="Link" Text="PDF" CommandName="DescargarPdf" />
			<asp:ButtonField ButtonType="Link" Text="Enviar Email" CommandName="EnviarEmail" />
          <%--  <asp:TemplateField  HeaderText="Cancelar">
                <ItemTemplate>
                    <asp:Button class="btn btn-outline-primary"   runat="server" Text='<%# (short)Eval("Cancelado") == 1 ? "Acuse Cancelacion" : "Cancelar"  %>'  CommandName='<%# (short)Eval("Cancelado") == 1 ? "Acuse" : "Cancelar"  %>' ID="btnCancelarf" CommandArgument='<%#Eval("idventa") %>'  />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnCancelarf" ConfirmText="¿Cancelar Documento?" Enabled='<%# (short)Eval("Cancelado") != 1  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <asp:Button class="btn btn-outline-primary"  runat="server" ID="btnSelectAll" Text='<%# this.SelText %>' CommandName="SelectAll" />
                </HeaderTemplate>
                <ItemTemplate> 
                    
                    <asp:CheckBox ID="cbChecked" runat="server" Checked='<%# (bool)Eval("Seleccionar") %>'/>
                </ItemTemplate>
            </asp:TemplateField>--%>
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
			--%>
            <asp:BoundField HeaderText="SubTotal" DataField="Subtotal" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
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
  
        
        </ContentTemplate>
                 <Triggers>
                      <asp:PostBackTrigger ControlID="gvFacturas" />
 <%--     <asp:PostBackTrigger ControlID="btnSelectAll" />--%>
    <%--   <asp:AsyncPostBackTrigger ControlID="gvFacturas"/>--%>
        
                     </Triggers>
        </asp:UpdatePanel>
 <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <h2>Cargando...</h2>
                <img src="Images/ajax-loader.gif" alt="Loading"  />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>