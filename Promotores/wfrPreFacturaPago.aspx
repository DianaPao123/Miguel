<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="wfrPreFacturaPago.aspx.cs" Inherits="GAFWEB.wfrPreFacturaPago" %>
<%--<%@ Register TagPrefix="cc1" Namespace="WebControls.FilteredDropDownList" Assembly="WebControls.FilteredDropDownList" %>--%>
<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />


<h1></h1>
<asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>

        <%---------------Primera Sección - Generar CFDI---------------%>

        <div class="card mt-2">   
            <div class="card-header">
                Generar CFDI
            </div>
            <div class ="card-body">
                <div class = "row"> <%--Línea/Empresa--%>
                    <div class = "form-group col-md-3 col-sm-6">
                        <asp:Label ID="lblLinea" runat="server" Text="Linea:"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlLinea" AutoPostBack="True" CssClass="form-control"
                        onselectedindexchanged="ddlLinea_SelectedIndexChanged">
                            <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                            <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                            <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                            <asp:ListItem Value="D" Text="D" ></asp:ListItem> 
                        </asp:DropDownList>
                    </div>
                    </div>
                    <div class = "row">
                    <div class="form-group col-md-9 col-sm-6">
                        <asp:Label ID="Label13" runat="server" Text="Empresa:"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlEmpresa" AutoPostBack="True" DataTextField="RazonSocial"
                        DataValueField="idEmpresa" CssClass="form-control" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged"/>
                    </div>
                </div>  <%--Termina Línea/Empresa--%>

                <div class = "row"> <%--Cliente--%>
                    <div class = "form-group col-md-6 col-sm-12">
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlClientes" AutoPostBack="True" DataTextField="RazonSocial" DataValueField="idCliente" 
                        CssClass="form-control" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged"/>                        
                    </div>
                    <div class = "form-group col-md-6 col-sm-12">
                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" Enabled="true" TextMode="MultiLine"/>
                    </div>
                </div>  <%--Termina Cliente--%>

                <div class = "row"> <%--Serie/Folio/Sucursal--%>
                    <%--<div class = "form-group col-md-4 col-sm-12">
                        <asp:Label ID="lblSerie" runat="server" Text="Serie"></asp:Label>
                        <asp:TextBox ID="txtSerie" runat="server" CssClass="form-control" />
                    </div>--%>
                    <div class = "form-group col-md-4 col-sm-12">
                        <asp:Label ID="lblFolio" runat="server" Text="Folio"></asp:Label>
                        <asp:TextBox ID="txtFolio" runat="server" Enabled="False" CssClass="form-control" />
                    </div>
                   
                </div>  <%--Termina Serie/Folio/Sucursal--%>


            </div>  <%--Termina panel-body--%>
        </div>  <%--Termina Primera Sección - Generar CFDI--%>



        <%---------------Segunda Sección - CFDI Relacionados---------------%>
   <%---------------Tercera Sección - Conceptos---------------%>

     
        <%---------------Cuarta Sección - Impuestos---------------%>
<%--
        <div class = "card mt-2">
            <div class = "card-header">
                <asp:CheckBox ID="cbImpuestos" runat="server" AutoPostBack="True" oncheckedchanged="cbImpuestos_CheckedChanged" 
                Text=". Impuestos" />
            </div>
            <div class ="card-body" id="DivComplementos" runat="server" visible="false">
                <div class ="row">
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblAsteriscoTipoImpuesto" runat="server" ForeColor="Red" Text="* "></asp:Label>
                        <asp:Label ID="lblTipoImpuesto" runat="server" Text="Tipo de Impuesto"></asp:Label> 
                        <asp:DropDownList ID="ddlTipoImpuesto" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlTipoImpuesto_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Value="Traslados">Traslados</asp:ListItem>
                            <asp:ListItem Value="Retenciones">Retenciones</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblAsteriscoBase" runat="server" ForeColor="Red" Text="* "></asp:Label>
                        <asp:Label ID="lblBase" runat="server" Text="Base"></asp:Label>
                        <asp:TextBox ID="txtBase" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" 
                        TargetControlID="txtBase" ValidChars="." />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBase" Display="Dynamic" 
                        ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarImpuesto"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBase" 
                        Display="Dynamic" ForeColor="Red" ErrorMessage="Dato invalido" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarImpuesto" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblAsteriscoClaveConcepto" runat="server" ForeColor="Red" Text="* "></asp:Label>
                        <asp:Label ID="lblClaveConcepto" runat="server" Text="Clave Concepto"></asp:Label>
                        <asp:DropDownList ID="ddlConceptos" runat="server" AutoPostBack="True" CssClass="form-control" 
                        onselectedindexchanged="ddlConceptos_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>                                       
                </div>
                <div class = "row">
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblAsteriscoTipoFactor" runat="server" ForeColor="Red" Text="* "></asp:Label>
                        <asp:Label ID="lblTipoFactor" runat="server" Text="Tipo Factor"></asp:Label>
                        <asp:DropDownList ID="ddlTipoFactor" runat="server" AutoPostBack="True" onselectedindexchanged="ddlTipoFactor_SelectedIndexChanged"
                        CssClass="form-control">
                            <asp:ListItem Value="Tasa">Tasa</asp:ListItem>
                            <asp:ListItem Value="Cuota">Cuota</asp:ListItem>
                            <asp:ListItem Value="Exento">Exento</asp:ListItem>
                        </asp:DropDownList>                        
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblAsteriscoImpuesto" runat="server" ForeColor="Red" Text="* "></asp:Label>
                        <asp:Label ID="lblImpuesto" runat="server" Text="Impuesto"></asp:Label>
                        <asp:DropDownList ID="ddlImpuesto" runat="server" AutoPostBack="True" CssClass="form-control"
                        onselectedindexchanged="ddlImpuesto_SelectedIndexChanged">
                            <asp:ListItem Value="002">IVA</asp:ListItem>
                            <asp:ListItem Value="001">ISR</asp:ListItem>
                            <asp:ListItem Value="003">IEPS</asp:ListItem>
                        </asp:DropDownList>                      
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblAsteriscoTasa" runat="server" ForeColor="Red" Text="* "></asp:Label>
                        <asp:Label ID="lblTasa" runat="server" Text="Tasa o Cuota"></asp:Label>
                        <asp:TextBox ID="txtTasa" runat="server" class="form-control" ></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" 
                        FilterType="Numbers, Custom" TargetControlID="txtTasa" ValidChars="." />
                        <asp:DropDownList ID="ddlTasaOCuota" runat="server" DataTextField="Maximo" DataValueField="Maximo" CssClass="form-control">
                        </asp:DropDownList>                                                
                    </div>
                </div>
                <div class = "row">
                    <div class = "col-md-2">
                        <asp:Button ID="btnAgregarImpuesto" runat="server" class="btn btn-outline-primary" onclick="btnAgregarImpuesto_Click" 
                        Text="Agregar Impuesto" ValidationGroup="AgregarImpuesto"/>
                    </div>
                </div>
                <div id="TablaImpuestos" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvImpuestos" runat="server" AutoGenerateColumns="False" CssClass="style124" onrowcommand="gvImpuestos_RowCommand" 
                    ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="TipoImpuesto" HeaderText="TipoImpuesto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Impuesto" HeaderText="Impuesto" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="Base" HeaderText="Base" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="TasaOCuota" HeaderText="TasaOCuota" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Importe" DataFormatString="{0:C}" HeaderText="Importe" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="TipoFactor" HeaderText="TipoFactor" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField CommandName="EliminarImpuesto" ItemStyle-HorizontalAlign="Center" Text="Eliminar" Visible="False" />
                        </Columns>
                    </asp:GridView>                    
                </div>
            </div>
        </div>
--%>
        <%-------------------- Cuarta Sección - Pagos --------------------%>
     <%----------------------------------pagos--------------------------------------------------%>
     <div class = "card mt-2" id="PnlCompPagos" runat="server">
            <div class = "card-header">
        
<asp:Label ID="LabelP1" runat="server" Text="Pagos" Font-Bold="True"  style="font-size: medium"></asp:Label>
   </div>
     <div class = "card-body" id="DivComPagos" runat="server" visible="true">
            <div class = "row">
              <div class = "form-group col-md-4 col-sm-6">
                   
    <asp:Label ID="lblFechaPagoComp" runat="server" Text="FechaPago"></asp:Label>
    <asp:TextBox ID="txtFechaPagoP" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender33" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txtFechaPagoP" 
                            TargetControlID="txtFechaPagoP" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFechaPagoP" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="btnAgregarPagosComp" />
                   </div>
       <div class = "form-group col-md-4 col-sm-6">
             
    <asp:Label ID="lblFormaDePagoPComp" runat="server"  Text="FormaDePago"></asp:Label>
   <asp:DropDownList runat="server" ID="ddlFormaPagoP" AutoPostBack="True" CssClass="form-control"
                         OnSelectedIndexChanged="ddlFormaPagoP_SelectedIndexChanged" 
                         style="margin-left: 0px" >
   <asp:ListItem runat="server" Value="01" Text="Efectivo"></asp:ListItem>
<asp:ListItem runat="server" Value="02" Text="Cheque nominativo"></asp:ListItem>
<asp:ListItem runat="server" Value="03" Text="Transferencia electrónica de fondos"></asp:ListItem>
<asp:ListItem runat="server" Value="04" Text="Tarjeta de crédito"></asp:ListItem>
<asp:ListItem runat="server" Value="05" Text="Monedero electrónico"></asp:ListItem>
<asp:ListItem runat="server" Value="06" Text="Dinero electrónico"></asp:ListItem>
<asp:ListItem runat="server" Value="08" Text="Vales de despensa"></asp:ListItem>
<asp:ListItem runat="server" Value="12" Text="Dación en pago"></asp:ListItem>
<asp:ListItem runat="server" Value="13" Text="Pago por subrogación"></asp:ListItem>
<asp:ListItem runat="server" Value="14" Text="Pago por consignación"></asp:ListItem>
<asp:ListItem runat="server" Value="15" Text="Condonación"></asp:ListItem>
<asp:ListItem runat="server" Value="17" Text="Compensación"></asp:ListItem>
<asp:ListItem runat="server" Value="23" Text="Novación"></asp:ListItem>
<asp:ListItem runat="server" Value="24" Text="Confusión"	></asp:ListItem>
<asp:ListItem runat="server" Value="25" Text="Remisión de deuda"></asp:ListItem>
<asp:ListItem runat="server" Value="26" Text="Prescripción o caducidad"></asp:ListItem>
<asp:ListItem runat="server" Value="27" Text="A satisfacción del acreedor"></asp:ListItem>
<asp:ListItem runat="server" Value="28" Text="Tarjeta de débito"></asp:ListItem>
<asp:ListItem runat="server" Value="29" Text="Tarjeta de servicios"></asp:ListItem>
<asp:ListItem runat="server" Value="30" Text="Aplicación de anticipos"></asp:ListItem>
<asp:ListItem runat="server" Value="99" Text="Por definir"></asp:ListItem>
 </asp:DropDownList>
 </div>
 
         <div class = "form-group col-md-4 col-sm-6">
       
    <asp:Label ID="lblMonedaComp" runat="server"  Text="Moneda"></asp:Label>
   <cc1:DropDownListChosen ID="ddlMonedaP" runat="server"  CausesValidation="false" CssClass="form-control"
            NoResultsText="No hay resultados coincidentes." width="250px"   SelectMethod=""  OnSelectedIndexChanged="ddlMonedaP_SelectedIndexChanged"        
            DataPlaceHolder="Escriba aquí..." AllowSingleDeselect="true" AutoPostBack="True" 
                           >                
        </cc1:DropDownListChosen>
        </div>
   <div class = "form-group col-md-4 col-sm-6">
       
        <asp:Label runat="server" ID="lblTipoCambioP" Text="Tipo Cambio" 
                            Visible="False" />
     <asp:TextBox runat="server" ID="txtTipoCambioP" CssClass="form-control" Visible="False" />
       </div>
            </div>
   <div class = "row">
  
      <div class = "form-group col-md-4 col-sm-6">
     
    <asp:Label ID="lblMontoComp" runat="server"  Text="Monto" ></asp:Label>
    <asp:TextBox ID="txtMonto" runat="server"   CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMonto" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="btnAgregarPagosComp" />
             
    </div>
     <div class = "form-group col-md-4 col-sm-6">
     
    <asp:Label ID="lblNumOperacionComp" runat="server"  Text="NumOperacion"></asp:Label>
    <asp:TextBox ID="txtNumOperacion" runat="server"  CssClass="form-control"></asp:TextBox>
    </div>
    
      <div class = "form-group col-md-4 col-sm-6">
    
    <asp:Label ID="lblRfcEmisorCtaOrdComp" runat="server"  Text="RfcEmisorCtaOrd"></asp:Label>
    <asp:TextBox ID="txtRfcEmisorCtaOrd" runat="server"    CssClass="form-control"></asp:TextBox>
  </div>
  </div>
    <div class = "row">
      <div class = "form-group col-md-4 col-sm-6">
   <asp:Label ID="lblNomBancoOrdExtComp" runat="server"  Text="NomBancoOrdExt"></asp:Label>
   <asp:TextBox ID="txtNomBancoOrdExt" runat="server"    CssClass="form-control"></asp:TextBox>
    </div>

      <div class = "form-group col-md-4 col-sm-6">
       <asp:Label ID="lblCtaOrdenanteComp" runat="server"  Text="CtaOrdenante"></asp:Label>
     <asp:TextBox ID="txtCtaOrdenante" runat="server"   CssClass="form-control"></asp:TextBox>
    </div>
        <div class = "form-group col-md-4 col-sm-6">
      <asp:Label ID="lblRfcEmisorCtaBen" runat="server"  Text="RfcEmisorCtaBen"></asp:Label>
      <asp:TextBox ID="txtRfcEmisorCtaBen" runat="server"    CssClass="form-control"
        ></asp:TextBox>
   </div>
   </div>
   <div class = "row">
      <div class = "form-group col-md-4 col-sm-6">
     <asp:Label ID="lblCtaBeneficiarioComp" runat="server"  Text="CtaBeneficiario"></asp:Label>
     <asp:TextBox ID="txtCtaBeneficiario" runat="server"    CssClass="form-control"></asp:TextBox>
    </div>
        <div class = "form-group col-md-4 col-sm-6">
   <asp:Label ID="lblTipoCadPagoComp" runat="server"  Text="TipoCadPago"></asp:Label>
    <asp:TextBox ID="txtTipoCadPago" runat="server"    CssClass="form-control"
        ></asp:TextBox>
   </div>
</div>
<%-- <div class = "row">
      <div class = "form-group col-md-4 col-sm-6">
     <asp:Label ID="lblCertPagoComp" runat="server"  Text="CertPago"></asp:Label>
<asp:TextBox ID="txtCertPago" runat="server"  Width="100px"></asp:TextBox>
</div>
 <div class = "form-group col-md-4 col-sm-6">
  <asp:Label ID="lblCadPagoComp" runat="server"  Text="CadPago"></asp:Label>
<asp:TextBox ID="txtCadPago" runat="server"  Width="100px"></asp:TextBox></td>
</div>
</div>
<div class = "row">
<div class = "form-group col-md-4 col-sm-6">
   <asp:Label ID="lblSelloPagoComp" runat="server"  Text="SelloPago"></asp:Label>
<asp:TextBox ID="txtSelloPago" runat="server"  Width="100px"></asp:TextBox></td>
</div>
</div>--%>
<div class = "row">
<div class = "form-group col-md-4 col-sm-6">
<asp:Button runat="server" ID="btnAgregarPagos" Text="Agregar Pagos" 
        ValidationGroup="btnAgregarPagosComp"  class="btn btn-outline-primary" 
        onclick="btnAgregarPagos_Click"/>
</div>
</div>

  <div  class = "row justify-content-center">
              
    <asp:GridView runat="server" ID="gvPagos" AutoGenerateColumns="False" CssClass="style124"
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvPagos_RowCommand" 
        onselectedindexchanged="gvPagos_SelectedIndexChanged">
			<Columns>
              	<asp:BoundField HeaderText="FechaPago" DataField="FechaPago"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="FormaDePagoP" DataField="FormaDePagoP" ItemStyle-HorizontalAlign="Center"  />
				<asp:BoundField HeaderText="MonedaP" DataField="MonedaP" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="Monto" DataField="Monto"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="NumOperacion" DataField="NumOperacion"  ItemStyle-HorizontalAlign="Center" />
               			
				<asp:ButtonField Text="Eliminar" CommandName="EliminarPago" Visible="False" ItemStyle-HorizontalAlign="Center" />
			</Columns>
		</asp:GridView></td>
        </div>
        
       <div class = "card mt-2">
            <div class = "card-header">
             
     	<asp:CheckBox runat="server" ID="cbDoctoRelacionado" Text=". DoctoRelacionado" 
                    AutoPostBack="True" oncheckedchanged="cbDoctoRelacionado_CheckedChanged" 
                    style="font-weight: 700"/>
		</div>
       
          <div id="DivDoctoRelacionado"  style="width:100%" runat="server" visible="false">
          <div class ="row">
                    <div class = "form-group col-md-4 col-sm-6">
      <asp:Label ID="Label21" runat="server"  Text="ID"></asp:Label>
        <asp:DropDownList ID="ddlID" runat="server" AutoPostBack="True"   CssClass="form-control"
        onselectedindexchanged="ddlID_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlID" Display="Dynamic" 
                         ForeColor="Red"   InitialValue="" ErrorMessage="* Requerido" ValidationGroup="AgregarDocumento"></asp:RequiredFieldValidator>
    </div>
    </div>
      
  <div  class = "row justify-content-center">
    <asp:GridView ShowFooter="True" runat="server"  CssClass="style124" 
            ID="gvFacturas" AutoGenerateColumns="False" DataKeyNames="Uid,idventa"
		onrowcommand="gvFacturas_RowCommand" AllowPaging="True" PageSize="10" Width="100%" Height="90%"
		onpageindexchanging="gvFacturas_PageIndexChanging" 
		onrowdatabound="gvFacturas_RowDataBound">
		<PagerSettings Position="Bottom" Visible="true" />
	    <FooterStyle BackColor="#A8CF38" CssClass="page2"  Font-Bold="True" />
		<Columns>
			<asp:BoundField HeaderText="Folio" DataField="folio" />
			<asp:BoundField HeaderText="Folio Fiscal" DataField="Uid" />
          	<asp:BoundField HeaderText="Serie" DataField="Serie" />
           <asp:BoundField HeaderText="Fecha Emisión" DataField="fecha" DataFormatString="{0:d}" />
		   <asp:BoundField HeaderText="SubTotal" DataField="Subtotal" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
			<asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
         	<asp:BoundField HeaderText="Status" DataField="StatusFactura"/>
       		<asp:ButtonField ButtonType="Link" Text="Seleccionar" CommandName="seleccionar" />
			
		</Columns>
	</asp:GridView> 
    </div>
     <div class ="row">
                       <div class = "form-group col-md-4 col-sm-6">
<asp:Button runat="server" ID="BuscarFactura" Text="Buscar" 
          class="btn btn-outline-primary" 
        onclick="BuscarFactura_Click"/>
</div>
</div>

     <div class ="row">
                   <div class = "form-group col-md-4 col-sm-6">
    
    <asp:Label ID="lblIdDocumentoComp" runat="server" Text="IdDocumento"></asp:Label>
    <asp:TextBox ID="txtIdDocumento" runat="server"  CssClass="form-control"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtIdDocumento" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarDocumento" />
         
    </div>
             <div class = "form-group col-md-4 col-sm-6">
      <asp:Label ID="lblSerieComp" runat="server" Text="Serie"></asp:Label>
      <asp:TextBox ID="txtSerieD" runat="server"  CssClass="form-control"></asp:TextBox>
    </div>
  
                   <div class = "form-group col-md-4 col-sm-6">
     <asp:Label ID="lblFolioComp" runat="server" Text="Folio"></asp:Label>
     <asp:TextBox ID="txtFolioD" runat="server"  CssClass="form-control"></asp:TextBox>
   </div>
     </div>
  <div class ="row">
      <div class = "form-group col-md-4 col-sm-6">
      <asp:Label ID="lblMetodoDePagoDR" runat="server" Text="MetodoDePagoDR"></asp:Label>
<asp:DropDownList runat="server"  CssClass="form-control" ID="ddlMetodoDePagoDR" AutoPostBack="True" OnSelectedIndexChanged="ddlMetodoDePagoDR_SelectedIndexChanged">
                           <asp:ListItem runat="server" Text="Pago en una sola exhibición" Value="PUE"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Pago en parcialidades o diferido" Value="PPD"></asp:ListItem>
                        </asp:DropDownList>
 </div>
            <div class = "form-group col-md-4 col-sm-6">
  <asp:Label ID="lblMonedaDRComp" runat="server" Text="MonedaDR"></asp:Label>
    <cc1:DropDownListChosen ID="ddlMonedaDR" runat="server"  CssClass="form-control"
        AllowSingleDeselect="true" AutoPostBack="True" CausesValidation="false" 
        DataPlaceHolder="Escriba aquí..." Height="18px" 
        NoResultsText="No hay resultados coincidentes." 
        OnSelectedIndexChanged="ddlMonedaDR_SelectedIndexChanged" SelectMethod="" 
        width="210px">
    </cc1:DropDownListChosen>
    </div>
    
                   <div class = "form-group col-md-4 col-sm-6">
    <asp:Label ID="lblTipoCambioDR" runat="server" Text="Tipo Cambio" 
        Visible="False" />
    <asp:TextBox ID="txtTipoCambioDR" runat="server" Visible="False"  CssClass="form-control" />
   </div>
      </div>
 <div class ="row">
                   <div class = "form-group col-md-4 col-sm-6">
                    <asp:Label ID="lblNumParcialidadComp" runat="server" Text="NumParcialidad" ></asp:Label>
        <asp:TextBox ID="txtNumParcialidad" runat="server"  CssClass="form-control"></asp:TextBox>
    </div>

           <div class = "form-group col-md-4 col-sm-6">
                     <asp:Label ID="lblImpSaldoAntComp" runat="server" Text="ImpSaldoAnt" ></asp:Label>
    <asp:TextBox ID="txtImpSaldoAnt" runat="server"  CssClass="form-control"></asp:TextBox>
 </div>

                   <div class = "form-group col-md-4 col-sm-6">
                      <asp:Label ID="lblImpPagadoComp" runat="server" Text="ImpPagado" ></asp:Label>
    <asp:TextBox ID="txtImpPagado" runat="server"  CssClass="form-control"></asp:TextBox>
    </div>
     </div>

 <div class ="row">
                 <div class = "form-group col-md-4 col-sm-6">
                      <asp:Label ID="lblImpSaldoInsolutoComp" runat="server" Text="ImpSaldoInsoluto" ></asp:Label>
    <asp:TextBox ID="txtImpSaldoInsoluto" runat="server" CssClass="form-control"></asp:TextBox>
 </div>
 </div>

 <div class ="row">
                   <div class = "form-group col-md-4 col-sm-6">
<asp:Button runat="server" ID="AgregarDocumento" Text="Agregar Documento" 
        ValidationGroup="AgregarDocumento"  class="btn btn-outline-primary" 
        onclick="btnAgregarDocumento_Click"/>
</div>
</div>
     <div id="Div1" runat="server" class = "row justify-content-center">
          
    <asp:GridView runat="server" ID="gvDocumento" AutoGenerateColumns="False" CssClass="style124"
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvDocumento_RowCommand">
			<Columns>
              	<asp:BoundField HeaderText="ID" DataField="ID"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="IdDocumento" DataField="IdDocumento" ItemStyle-HorizontalAlign="Center"  />
				<asp:BoundField HeaderText="Serie" DataField="Serie" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="Folio" DataField="Folio"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="MonedaDR" DataField="MonedaDR" ItemStyle-HorizontalAlign="Center" />
               	<asp:BoundField HeaderText="NumParcialidad" DataField="NumParcialidad"  ItemStyle-HorizontalAlign="Center" />
                		
				<asp:ButtonField Text="Eliminar" CommandName="EliminarDocumento" Visible="False" ItemStyle-HorizontalAlign="Center" />
			</Columns>
		</asp:GridView>
        </div>
        
      </div>
      </div>
           
           
      </div>
      </div>
        <%-------------------- Quinta Sección - Totales --------------------%>

        <div class = "card mt-2">
            <div class = "card-header">
                Totales
            </div>
            <div class = "card-body">
                <div class = "row">
                    <div class = "col-sm-12">
                        Subtotal:
                        <asp:Label ID="lblSubtotal" runat="server" />
                    </div>
                    <div class = "col-sm-12">
                        Retenciones:
                        <asp:Label ID="lblRetenciones" runat="server" />
                    </div>
                    <div class = "col-sm-12">
                        Traslados:
                        <asp:Label ID="lblTraslados" runat="server" />
                    </div>
                    <div class = "col-sm-12">
                        Descuento:
                        <asp:Label ID="lblDescuento" runat="server" />
                    </div>
                    <div class = "col-sm-12">
                        Total:
                        <asp:Label ID="lblTotal" runat="server" />
                    </div>
                    <div class = "col-sm-12 mt-3">
                        <asp:Button ID="btnLimpiar" runat="server" class="btn btn-outline-primary" OnClick="btnLimpiar_Click" Text="Limpiar"/>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnVistaPrevia" runat="server" class="btn btn-outline-primary" OnClick="btnGenerarPreview_Click" Text="Vista Previa" 
                        ValidationGroup="CrearFactura"/>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnGenerarFactura" runat="server" class="btn btn-outline-primary" OnClick="btnGenerarFactura_Click" Text="Generar" 
                        ValidationGroup="CrearFactura"/>
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Confirma que deseas generar el comprobante" 
                        TargetControlID="btnGenerarFactura" />
                    </div>
                </div>
            </div>
        </div>
   

        <%--------------------Termina formato--------------------%>
        
           <div style=" width:100% ">
           <div style="float:right; text-align:right;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" />
            </div>
          </div>

         <div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                AssociatedUpdatePanelID="up1">
                <ProgressTemplate>
                    <div id="Background">
                    </div>
                    <div id="Progress">
                        <br/>
                        <br/>
                        <br/>
                        <br>
                        </br>
                        CFDI en proceso ..
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            </div>

   
            <br/>
            <table class="page2" width="100%">
                
                <tr ID="trDonativo" runat="server" visible="False">
                    <td class="style141" style="text-align: right">
                        N. de autorización del Donativo:
                    </td>
                    <td class="style146">
                        <asp:TextBox ID="txtDonatAutorizacion" runat="server"></asp:TextBox>
                    </td>
                    <td class="style9">
                        Fecha de autorización del donativo:
                    </td>
                    <td class="style147">
                        <asp:TextBox ID="txtDonatFechautorizacion" runat="server"></asp:TextBox>
                        <asp:CalendarExtender runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txtDonatFechautorizacion" 
                            TargetControlID="txtDonatFechautorizacion" />
                    </td>
                </tr>
            </table>
            <div ID="divParcialidades" runat="server" class="page3" visible="False">
                <h1 style="text-align: center">
                    Parcialidades</h1>
                <table width="95%">
                    <tr>
                        <td class="style130" style="text-align: right;">
                            Folio Fiscal Original
                        </td>
                        <td>
                            <asp:TextBox ID="txtFolioOriginal" runat="server" />
                        </td>
                        <td class="style3" style="text-align: right;">
                            Fecha del Folio Fiscal Original:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaOriginal" runat="server" Width="100%" />
                            <asp:CalendarExtender ID="ceFechaOriginal" runat="server" 
                                PopupButtonID="txtFechaOriginal" TargetControlID="txtFechaOriginal" />
                            <asp:CompareValidator ID="dvFechaOriginal" runat="server" 
                                ControlToValidate="txtFechaOriginal" Display="Dynamic" 
                                ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" 
                                ValidationGroup="CrearFactura" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style130" style="text-align: right;">
                            Monto del Folio Fiscal Original
                        </td>
                        <td>
                            <asp:TextBox ID="txtMontoOriginal" runat="server" Width="100%" />
                            <asp:CompareValidator ID="cvMontoOriginal" runat="server" 
                                ControlToValidate="txtMontoOriginal" Display="Dynamic" 
                                ErrorMessage="* Monto invalido" Operator="DataTypeCheck" Type="Currency" 
                                ValidationGroup="CrearFactura" />
                        </td>
                        <td style="text-align: right">
                            Serie del Folio Fiscal Original
                        </td>
                        <td>
                            <asp:TextBox ID="txtSerieOriginal" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div ID="divViasConcesionadas" runat="server" visible="False">
                <h1>
                    Firmas</h1>
                <table width="100%">
                    <tr>
                        <td>
                            Vo. Bo.
                        </td>
                        <td>
                            Nombre:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVoBoNombre" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Puesto:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVoBoPuesto" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Area:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVoBoArea" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Recibí
                        </td>
                        <td>
                            Nombre:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRecibiNombre" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Puesto:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRecibiPuesto" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Area:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRecibiArea" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Autorizó
                        </td>
                        <td>
                            Nombre:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAutorizoNombre" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Puesto:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAutorizoPuesto" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Area:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAutorizoArea" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <br />


                <table width="100%">
                   
                    <tr>
                        <td class="style153">
                            <asp:TextBox ID="txtDetalles" runat="server" Enabled="False" 
                                TextMode="MultiLine" Visible="False" Width="193%" />
                        </td>
                    </tr>
                   
                       <tr>
                        <td>
                            <asp:HiddenField ID="txtIdProducto" runat="server" />
                        </td>
                    </tr>
                </table>
                           


            <%--Impuestos--%>
            

            <%-------------------- Pagos --------------------%>
                     

            <asp:ModalPopupExtender ID="mpeBuscarConceptos" runat="server" 
                BackgroundCssClass="mpeBack" CancelControlID="btnCerrarConcepto" 
                PopupControlID="pnlBuscarConceptos" TargetControlID="btnConceptoDummy" />
            <asp:Panel ID="pnlBuscarConceptos" runat="server" BackColor="White" 
                CssClass="page3" Style="text-align: center;" Width="800px">
                <h1>
                    Buscar Conceptos</h1>
                <p>
                    <asp:TextBox ID="txtConceptoBusqueda" runat="server" Width="250px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBuscarConcepto" runat="server" class="btn btn-outline-primary" 
                        OnClick="btnBuscarConcepto_Click" Text="Buscar" />
                </p>
                <div style="height: 400px;, overflow-y: scroll">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />
                    <asp:GridView ID="gvBuscarConceptos" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="c_ClaveProdServ1" OnRowCommand="gvBuscarConceptos_RowCommand" 
                        ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="c_ClaveProdServ1" HeaderText="Código" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <%--<asp:Button runat="server" ID="btnSeleccionarConcepto" Text="Seleccionar" onclick="btnSeleccionarConcepto_Click" />&nbsp;&nbsp;--%>
                <asp:Button ID="btnCerrarConcepto" runat="server" class="btn btn-outline-primary" 
                    Text="Cancelar" />
            </asp:Panel>
            <asp:Button ID="btnConceptoDummy" runat="server" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpeEdita" runat="server" 
                BackgroundCssClass="mpeBack" CancelControlID="btnCancelar" 
                PopupControlID="panelEditaConcepto" TargetControlID="btnEditarDummy" />
            <asp:Panel ID="panelEditaConcepto" runat="server" BackColor="White" 
                CssClass="page3" Style="text-align: center;" Width="800px">
                <h1>
                    <asp:Label ID="lblConcepto" runat="server">Editar Concepto</asp:Label>
                </h1>
                <table class="linea" width="600px">
                    <tr>
                        <td>
                            ClaveUnidad:</td>
                        <td align="left">
                            <cc1:DropDownListChosen ID="ddlClaveUnidadE" runat="server" 
                                AllowSingleDeselect="true" CausesValidation="false" 
                                DataPlaceHolder="Escriba aquí..." Height="16px" 
                                NoResultsText="No hay resultados coincidentes." SelectMethod="" width="206px">
                            </cc1:DropDownListChosen>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Código:</td>
                        <td align="left">
                            <asp:TextBox ID="txtCodigoE" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                ControlToValidate="txtCodigoE" Display="Dynamic" ErrorMessage="* Requerido" 
                                ValidationGroup="Concepto" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cantidad:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCantidadEdita" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtCantidadEdita" Display="Dynamic" 
                                ErrorMessage="* Requerido" ValidationGroup="Concepto" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                ControlToValidate="txtCantidadEdita" Display="Dynamic" 
                                ErrorMessage="* Cantidad Invalida" Operator="DataTypeCheck" Type="Double" 
                                ValidationGroup="Concepto" />
                            <asp:HiddenField ID="hidDetalle" runat="server" />
                            <asp:HiddenField ID="hidNumero" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Unidad de Medida:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUnidadEdita" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtUnidadEdita" Display="Dynamic" ErrorMessage="* Requerido" 
                                ValidationGroup="Concepto" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            N° de identificación:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNoIdentificacionEdita" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Descripción:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDescripcionEdita" runat="server" Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtDescripcionEdita" Display="Dynamic" 
                                ErrorMessage="* Requerido" ValidationGroup="Concepto" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Observaciones:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtObservacionesEdita" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Precio Unitario:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPrecioUnitarioEdita" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="txtPrecioUnitarioEdita" Display="Dynamic" 
                                ErrorMessage="* Cantidad Invalida" Operator="DataTypeCheck" Type="Double" 
                                ValidationGroup="Concepto" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="txtPrecioUnitarioEdita" Display="Dynamic" 
                                ErrorMessage="* Requerido" ValidationGroup="Concepto" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Descuento:</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDescuentoE" runat="server" Width="106px" />
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
                <div align="right">
                    <asp:Button ID="btnGuardar" runat="server" class="btn btn-outline-primary" 
                        OnClick="btnGuardar_Click" Text="Guardar" ValidationGroup="Concepto" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                        class="btn btn-outline-primary" Text="Cancelar" />
                </div>
            </asp:Panel>
            <asp:Button ID="btnEditarDummy" runat="server" class="btn btn-outline-primary" 
                Style="display: none;" />
            <asp:ModalPopupExtender ID="mpeBuscarConceptoHistorico" runat="server" 
                BackgroundCssClass="mpeBack" CancelControlID="btnCerrarConceptoHistorico" 
                PopupControlID="pnlBuscarConceptoHistorico" 
                TargetControlID="btnConceptoHistoricoDummy" />
            <asp:Panel ID="pnlBuscarConceptoHistorico" runat="server" BackColor="White" 
                CssClass="page3" style="text-align: center;" Width="800px">
                <h1>
                    Buscar Conceptos Historico</h1>
                <p>
                    <asp:TextBox ID="txtConceptoHistoricoBusqueda" runat="server" Width="250px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBuscarConceptoHistorico" runat="server" 
                        class="btn btn-outline-primary" onclick="btnBuscarConceptoHistorico_Click" 
                        Text="Buscar" />
                </p>
                <div style="height: 400px">
                    <asp:Label ID="lblMensajeHistorico" runat="server" ForeColor="Red" />
                    <asp:GridView ID="gvBuscarConceptosHistorico" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="IdProducto" 
                        onrowcommand="gvBuscarConceptosHistorico_RowCommand" ShowHeaderWhenEmpty="True" 
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Código" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                            <asp:BoundField DataField="PrecioP" DataFormatString="{0:C}" 
                                HeaderText="Precio" />
                            <asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <%--<asp:Button runat="server" ID="btnSeleccionarConcepto" Text="Seleccionar" onclick="btnSeleccionarConcepto_Click" />&nbsp;&nbsp;--%>
                <asp:Button ID="btnCerrarConceptoHistorico" runat="server" 
                    class="btn btn-outline-primary" Text="Cancelar" />
            </asp:Panel>
            <asp:Button ID="btnConceptoHistoricoDummy" runat="server" style="display: none;" />
     </ContentTemplate>



        <Triggers>
            <asp:PostBackTrigger ControlID="btnLimpiar" />
       <%--     <asp:PostBackTrigger ControlID="gvDetalles" />
     --%>       <asp:PostBackTrigger ControlID="BtnVistaPrevia" />
        </Triggers>
    </asp:UpdatePanel>    
</asp:Content>
