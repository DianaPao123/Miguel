<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrPreFacturaConsulta.aspx.cs" Inherits="GAFWEB.wfrPreFacturasConsulta" %>
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
              Reporte de&nbsp; Pre-CFDI
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
				<asp:RadioButtonList RepeatDirection="Horizontal" ID="rbStatus" runat="server" Visible="false">
					<asp:ListItem Text="Todas" Value="9" Selected="True" />
					
					<asp:ListItem Text="Pendientes" Value="0"/>
				    <asp:ListItem Text="Pagadas" Value="1" />
                    <asp:ListItem Text="Rechazadas" Value="3"/>
				
				</asp:RadioButtonList>

			 </div>
                  <div class = "col-9 ">
                      <asp:Button runat="server" ID="btnBuscar" Text="Buscar" 
			 onclick="btnBuscar_Click" class="btn btn-outline-primary"/>
			<asp:Button runat="server" ID="btnExportar" Text="Exportar Excel" 
                    onclick="btnExportar_Click" class="btn btn-outline-primary" Width="133px"/>
          </div>
                  </div>

                </div>
             </div>

                      <br />
        </ContentTemplate>
        <Triggers>
          <%--   <asp:AsyncPostBackTrigger ControlID="btnDescargarTodo" EventName="Click" /> --%>
         
              <asp:PostBackTrigger ControlID="btnExportar" />
     </Triggers>
        </asp:UpdatePanel>

      <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>
    <div style="height:100%; overflow-y: scroll;>
        <asp:HiddenField runat="server" ID="hidSel"  Value="Sel"/>
        <asp:GridView ShowFooter="True" runat="server"  CssClass="style124" 
            ID="gvFacturas" AutoGenerateColumns="False" DataKeyNames="idEmpresa,idCliente,idPreFactura,CFDI,TipoDeComprobante"
		onrowcommand="gvFacturas_RowCommand" AllowPaging="True" PageSize="30" Width="100%" Height="90%"
		onpageindexchanging="gvFacturas_PageIndexChanging" 
		onrowdatabound="gvFacturas_RowDataBound">
		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="#A8CF38" CssClass="page2"  Font-Bold="True" />
		<Columns>
			<asp:BoundField HeaderText="PreFolio" DataField="PreFolio" />
		    <asp:BoundField HeaderText="Fecha Emisión" DataField="Fecha" DataFormatString="{0:G}" />
				<asp:BoundField HeaderText="Cliente" DataField="RazonSocial" />
			<asp:BoundField HeaderText="Empresa" DataField="nombreEmpresa"  />
             <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
    <%--        <asp:BoundField HeaderText="Usuario" DataField="IDUsuario"/>
	--%>		<asp:BoundField HeaderText="Status" DataField="Estatus"/>
         <%--  <asp:ButtonField ButtonType="Link" Text="Generar"  CommandName="GenerarCFDI"   />
           <asp:ButtonField ButtonType="Link" Text="PDF" CommandName="DescargarPdf" />
	        
              <asp:ButtonField ButtonType="Link" Text="SubirPago" CommandName="Pagar" />--%>
              <asp:ButtonField ButtonType="Link" Text="Editar" CommandName="Editar" />
       		
		</Columns>
	</asp:GridView> 
    </div>
	
   
	<asp:GridView ID="gvFacturaCustumer"  CssClass="page2" Visible="False" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Serie" DataField="Serie" />
			<asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:d}" />
		  <asp:BoundField HeaderText="Empresa" DataField="nombreEmpresa"  />
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
         <%--   <asp:BoundField HeaderText="Usuario" DataField="IDUsuario"/>
		--%>	<asp:BoundField HeaderText="Status" DataField="Estatus"/>
        </Columns>
    </asp:GridView>
    <br />
   

  <asp:ModalPopupExtender runat="server" ID="mpePagar" TargetControlID="btnpagarDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarPagar" PopupControlID="pnlPagar"/>
	<asp:Panel runat="server" ID="pnlPagar" CssClass="page2" BackColor="White" Width="600px" style="text-align: center;">
		<h1>Comprobante de Pago Factura</h1>
	    <br />
	    No. de Folio: <asp:Label runat="server" ID="lblFolioPago" Text="?" />
		<p>
        <div>
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
                    

       </div>
       <br />
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
            onclick="btnCerrarPagar_Click"  ValidationGroup="CrearPagos" />
		<br /><br />

        	<asp:Label runat="server" ID="lblIdventa" Visible="False" />
		   <asp:Label runat="server" ID="lblErrorPago" ForeColor="Red" />
           <br />
    
	</asp:Panel>
    	<asp:Button runat="server" ID="btnPagarDummy" style="display: none;" class="btn btn-outline-primary"/>
       
       </ContentTemplate>
       <Triggers>
          <asp:PostBackTrigger ControlID="gvFacturas" />
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