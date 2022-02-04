<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="wfrFactura.aspx.cs" Inherits="GAFWEB.wfrFactura" %>
<%@ Register TagPrefix="cc1" Namespace="WebControls.FilteredDropDownList" Assembly="WebControls.FilteredDropDownList" %>
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
                    <div class = "form-group col-md-4 col-sm-12">
                        <asp:Label ID="lblSerie" runat="server" Text="Serie"></asp:Label>
                        <asp:TextBox ID="txtSerie" runat="server" CssClass="form-control" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-12">
                        <asp:Label ID="lblFolio" runat="server" Text="Folio"></asp:Label>
                        <asp:TextBox ID="txtFolio" runat="server" Enabled="False" CssClass="form-control" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-12">
                        <asp:Label ID="lblSucursal" runat="server" Text="Sucursal"></asp:Label>
                        <asp:DropDownList ID="ddlSucursales" runat="server" AppendDataBoundItems="True" CssClass="form-control" 
                            DataTextField="Nombre" DataValueField="LugarExpedicion" />
                    </div>
                </div>  <%--Termina Serie/Folio/Sucursal--%>

                <div class = "row"> <%--Tipo/Estatus/Confirmación--%>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo de Documento"></asp:Label>
                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" AutoPostBack="True" CssClass="form-control"
                        OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                            <asp:ListItem runat="server" Selected="True" Text="Factura" Value="Ingreso" />
                            <asp:ListItem runat="server" Text="Nota de Crédito" Value="Egreso" />
                            <asp:ListItem runat="server" Text="Recibo de Donativo" Value="Donativo" />
                        </asp:DropDownList>
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblEstatusFactura" runat="server" Text="Estatus Factura"></asp:Label>
                        <asp:DropDownList ID="ddlStatusFactura" runat="server" AutoPostBack="True" CssClass="form-control" 
                            OnSelectedIndexChanged="ddlStatusFactura_SelectedIndexChanged">
                            <asp:ListItem Text="Pendiente" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Pagada" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblConfirmacion" runat="server" Text="Confirmación"></asp:Label>
                        <asp:TextBox ID="txtConfirmacion" runat="server" CssClass="form-control"/>
                    </div>
                    <div class = "form-group col-md-3 col-sm-6">    <%--No es visible--%>
                        <asp:Label ID="lblFechaDePago" runat="server" Text="Fecha de Pago" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtFechaPago" runat="server" ontextchanged="txtFechaPago_TextChanged" Visible="False" CssClass="form-control"/>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtFechaPago" Display="Dynamic" 
                        ErrorMessage="* Fecha Invalida" Operator="DataTypeCheck" Type="Date" ForeColor="Red" />
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Animated="False" Format="dd/MM/yyyy" PopupButtonID="txtFechaPago" 
                        TargetControlID="txtFechaPago"/>
                    </div>
                </div>  <%--Termina Tipo/Estatus/Confirmación--%>

                <div class = "row"> <%--Moneda/TipoCambio--%>
                    <div class = "form-group col-md-8 col-sm-6">
                        <asp:Label ID="lblMoneda" runat="server" Text="Moneda"></asp:Label>
                        <cc1:DropDownListChosen ID="ddlMoneda" runat="server" AllowSingleDeselect="true" AutoPostBack="True" CausesValidation="false" 
                        DataPlaceHolder="Escriba aquí..." NoResultsText="No hay resultados coincidentes." 
                        OnSelectedIndexChanged="ddlMoneda_SelectedIndexChanged" SelectMethod="" CssClass="form-control">
                        </cc1:DropDownListChosen>
                    </div>
                    <div class = "form-group col-md-2 col-sm-6">
                        <asp:Label ID="lblTipoCambio" runat="server" Text="Tipo de Cambio" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtTipoCambio" runat="server" Visible="false" CssClass="form-control"/>
                    </div>
                </div>  <%--Termina Moneda/TipoCambio--%>

                <div class = "row"> <%--Forma/Método/Condiciones--%>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblFormaPago" runat="server" Text="Forma de Pago"></asp:Label>
                        <asp:DropDownList ID="ddlFormaPago" runat="server" AutoPostBack="True" CssClass="form-control" 
                            OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged" 
                            style="margin-left: 0px" Width="184px">
                            <asp:ListItem runat="server" Text="Seleccionar" Value="00"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Efectivo" Value="01"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque nominativo" Value="02"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia electrónica de fondos" Value="03"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Tarjeta de crédito" Value="04"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Monedero electrónico" Value="05"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Dinero electrónico" Value="06"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Vales de despensa" Value="08"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Dación en pago" Value="12"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Pago por subrogación" Value="13"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Pago por consignación" Value="14"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Condonación" Value="15"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Compensación" Value="17"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Novación" Value="23"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Confusión" Value="24"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Remisión de deuda" Value="25"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Prescripción o caducidad" Value="26"></asp:ListItem>
                            <asp:ListItem runat="server" Text="A satisfacción del acreedor" Value="27"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Tarjeta de débito" Value="28"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Tarjeta de servicios" Value="29"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Aplicación de anticipos" Value="30"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Por definir" Value="99"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMetodoPago" runat="server" Text="Método de Pago"></asp:Label>
                        <asp:DropDownList ID="ddlMetodoPago" runat="server" AutoPostBack="True" CssClass="form-control" 
                        OnSelectedIndexChanged="ddlMetodoPago_SelectedIndexChanged">
                            <asp:ListItem runat="server" Value="00" Text="Seleccionar"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Pago en una sola exhibición" Value="PUE"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Pago en parcialidades o diferido" Value="PPD"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblCondicionesPago" runat="server" Text="Condiciones de Pago"></asp:Label>
                        <asp:TextBox ID="txtCondicionesPago" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>  <%--Termina Forma/Método/Condiciones--%>

                <div class = "row"> <%--Uso/Observaciones--%>
                    <div class = "form-group col-md-6 col-sm-12">
                        <asp:Label ID="lblUsoCFDI" runat="server" Text="Uso del CFDI"></asp:Label>
                        <asp:DropDownList ID="ddlUsoCFDI" runat="server" AutoPostBack="True" CssClass="form-control">
                            <asp:ListItem runat="server" Text="Adquisición de mercancias" Value="G01"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Devoluciones, descuentos o bonificaciones" Value="G02"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Gastos en general" Value="G03"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Construcciones" Value="I01"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Mobilario y equipo de oficina por inversiones" Value="I02"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Equipo de transporte" Value="I03"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Equipo de computo y accesorios" Value="I04"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Dados, troqueles, moldes, matrices y herramental" Value="I05"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Comunicaciones telefónicas" Value="I06"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Comunicaciones satelitales" Value="I07"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Otra maquinaria y equipo" Value="I08"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Honorarios médicos, dentales y gastos hospitalarios." Value="D01"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Gastos médicos por incapacidad o discapacidad" Value="D02"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Gastos funerales." Value="D03"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Donativos." Value="D04"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación)." 
                            Value="D05"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Aportaciones voluntarias al SAR." Value="D06"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Primas por seguros de gastos médicos." Value="D07"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Gastos de transportación escolar obligatoria." Value="D08"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones." 
                            Value="D09"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Pagos por servicios educativos (colegiaturas)" Value="D10"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Por definir" Value="P01"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class = "form-group col-md-6 col-sm-12">
                        <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                        <asp:TextBox ID="txtProyecto" runat="server" CssClass="form-control"/>
                    </div>
                </div>  <%--Termina Uso/Observaciones--%>
            </div>  <%--Termina panel-body--%>
        </div>  <%--Termina Primera Sección - Generar CFDI--%>



        <%---------------Segunda Sección - CFDI Relacionados---------------%>

        <div class = "card mt-2">   
            <div class = "card-header">
                <asp:CheckBox ID="cbCfdiRelacionados" runat="server" AutoPostBack="True" oncheckedchanged="cbCfdiRelacionados_CheckedChanged" 
                Text=". CFDIs Relacionados" />
            </div>
            <div class = "card-body" id="DivCfdiRelacionados" runat="server" visible="false">
                <div class = "row">
                    <div class = "form-group col-md-4">
                        <asp:Label ID="lblUUID" runat="server" Text="UUID"></asp:Label>
                        <asp:TextBox ID="txtUUDI" runat="server" CssClass="form-control"/>
                    </div>
                    <div class = "form-group col-md-8">
                        <asp:Label ID="lblTipoRelacion" runat="server" Text="Tipo de Relación"></asp:Label>
                        <asp:DropDownList ID="ddlTipoRelacion" runat="server" AutoPostBack="True" CssClass="form-control">
                            <asp:ListItem runat="server" Text="Nota de crédito de los documentos relacionados" Value="01" />
                            <asp:ListItem runat="server" Text="Nota de débito de los documentos relacionados" Value="02" />
                            <asp:ListItem runat="server" Text="Devolución de mercancía sobre facturas o traslados previos" Value="03" />
                            <asp:ListItem runat="server" Text="Sustitución de los CFDI previos" Value="04" />
                            <asp:ListItem runat="server" Text="Traslados de mercancias facturados previamente" Value="05" />
                            <asp:ListItem runat="server" Text="Factura generada por los traslados previos" Value="06" />
                            <asp:ListItem runat="server" Text="CFDI por aplicación de anticipo" Value="07" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class = "row">
                    <div class = "col-md-3">
                        <asp:Button ID="btnCfdiRelacionado" runat="server" Cssclass="btn btn-outline-primary" 
                        onclick="btnCfdiRelacionado_Click" Text="Agregar Cfdi Relacionado" ValidationGroup="AgregarCfdiRelacionado"/>                            
                    </div>
                </div>
                <div>
                    <div >
                        <asp:GridView ID="gvCfdiRelacionado" runat="server" AutoGenerateColumns="False" 
                          onrowcommand="gvCfdiRelacionado_RowCommand" ShowHeaderWhenEmpty="True" Width="80%">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="UUID" HeaderText="UUID" ItemStyle-HorizontalAlign="Center" />
                                <asp:ButtonField CommandName="EliminarCfdiRelacionado" ItemStyle-HorizontalAlign="Center" 
                                Text="Eliminar" Visible="True" />
                            </Columns>
                        </asp:GridView>                            
                    </div>
                </div>
            </div>  <%--Termina panel-body--%>
        </div>  <%--Termina Segunda Sección - CFDI Relacionados--%>



        <%---------------Tercera Sección - Conceptos---------------%>

        <div class = "card mt-2"> 
            <div class = "card-header">
                Conceptos
            </div>
            <div class = "card-body">
                <div class = "row"> <%--Clave Unidad/No. Identificación--%>
                    <div class = "form-group col-md-9">
                        <asp:Label ID="lblClaveUnidad" runat="server" Text="Unidad de medida"></asp:Label>
                        <cc1:DropDownListChosen ID="ddlClaveUnidad" runat="server" AllowSingleDeselect="true" CausesValidation="false" 
                        DataPlaceHolder="Escriba aquí..." NoResultsText="No hay resultados coincidentes." SelectMethod="" CssClass="form-control">
                        </cc1:DropDownListChosen>
                    </div>
                    <div class = "form-group col-md-3">
                    </div>
                </div>  <%--Termina Clave Unidad/No. Identificación--%>
                <div class = "row"> <%--Código/Botón Clave/Botón Buscador/Precio Unitario--%>
                    <div class = "form-group col-md-3 col-sm-6">
                        <asp:Label ID="lblCodigo" runat="server" Text="Clave de producto"></asp:Label>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCodigo" Display="Dynamic" 
                        ErrorMessage="* Requerido" ValidationGroup="AgregarConcepto" ForeColor="Red" />
                    </div>
                    <div class = "col-md-3 col-sm-6 align-self-center">
                    </div>
                    <div class = "col-md-3 col-sm-6 align-self-center">
                        <input type="button" value="BuscadorSAT" onclick="javascript:window.open('http://200.57.3.46:443/PyS/catPyS.aspx','','width=600,height=400,left=50,top=50,toolbar=yes');" class="btn btn-outline-primary" />
                    </div>
                    <div class = "form-group col-md-3 col-sm-6">
                        <asp:Label ID="lblPrecioUnitario" runat="server" Text="Precio Unitario"></asp:Label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" Display="Dynamic" 
                        ErrorMessage="* Requerido" ValidationGroup="AgregarConcepto" ForeColor="Red"/>
                        <asp:CompareValidator ID="cvPrecio" runat="server" 
                            ControlToValidate="txtPrecio" Display="Dynamic" 
                        ErrorMessage="* Precio invalido" Operator="DataTypeCheck" Type="Double" 
                            ValidationGroup="AgregarConcepto" ForeColor="Red" />
                    </div>
                </div>  <%--Termina Código/Botón Clave/Botón Buscador/Precio Unitario--%>
                <div class = "row"> <%--Cantidad/Unidad/Descuento--%>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtCantidad" Display="Dynamic" 
                        ErrorMessage="* Requerido" ValidationGroup="AgregarConcepto" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtCantidad" Display="Dynamic" ErrorMessage="Dato invalido" 
                        ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarConcepto" ForeColor="Red"/>
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblDescu" runat="server" Text="Descuento"></asp:Label>
                        <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control"/>
                    </div>
                </div>  <%--Termina Cantidad/Unidad/Descuento--%>
                <div class = "row"> <%--Descripción/Botón Agregar/ Botón Buscar--%>
                    <div class = "form-group col-md-6 col-sm-12">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion" Display="Dynamic" 
                        ErrorMessage="* Requerido" ValidationGroup="AgregarConcepto" ForeColor="Red"/>
                    </div>
                    <div class = "col-md-3 col-sm-6 align-self-center">
                        <asp:Button ID="btnAgregar" runat="server" class="btn btn-outline-primary" OnClick="btnAgregar_Click" Text="Agregar Concepto" 
                        ValidationGroup="AgregarConcepto"/>
                    </div>
                    <div class = "col-md-3 col-sm-6 align-self-center">
                        <asp:Button ID="btnBuscarHistorico" runat="server" class="btn btn-outline-primary" OnClick="btnBuscarHistorico_Click" Text="Buscar"/>
                    </div>
                </div>  <%--Termina Descripción/Botón Agregar/ Botón Buscar--%>
                
                <div >
                    <asp:GridView ID="gvDetalles" runat="server" AutoGenerateColumns="False" 
                        CssClass="style124" HorizontalAlign="Center" 
                        OnRowCommand="gvDetalles_RowCommand" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="ConceptoClaveProdServ" HeaderText="ClaveProdServ" 
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ConceptoNoIdentificacion" 
                                HeaderText="NoIdentificacion" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ConceptoCantidad" HeaderText="Cantidad" 
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ConceptoClaveUnidad" HeaderText="ClaveUnidad" 
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ConceptoUnidad" HeaderText="Unidad" 
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ConceptoValorUnitario" DataFormatString="{0:C}" 
                                HeaderText="ValorUnitario" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ConceptoDescripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="ConceptoImporte" DataFormatString="{0:C}" 
                                HeaderText="Importe" />
                            <asp:BoundField DataField="ConceptoDescuento" DataFormatString="{0:C}" 
                                HeaderText="Descuento" ItemStyle-HorizontalAlign="Right" />
                            <asp:ButtonField CommandName="Editar" ItemStyle-HorizontalAlign="Center" 
                                Text="Editar" Visible="False" />
                            <asp:ButtonField CommandName="EliminarConcepto" 
                                ItemStyle-HorizontalAlign="Center" Text="Eliminar" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>  <%--Termina panel-body--%>

        </div>  <%--Termina Tercera Sección - Conceptos--%>


        <%---------------Cuarta Sección - Impuestos---------------%>

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
                        ErrorMessage="Requerido" ValidationGroup="AgregarImpuesto" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBase" 
                        Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="Red" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="AgregarImpuesto" />
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

        <%-------------------- Cuarta Sección - Pagos --------------------%>
         <div class ="card mt-2"  runat="server" id="PnlPagos" >
            <div class = "card-header">
                <asp:CheckBox ID="cbDPagos" runat="server" AutoPostBack="True" oncheckedchanged="cbDPagos_CheckedChanged"  
                Text=". Descripción de pagos" />
            </div>
            <div class = "card-body">
                <div class = "row">
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblFechaPago" runat="server" Text="Fecha"></asp:Label>
                        <asp:TextBox ID="txtDPFecha" runat="server" CssClass="form-control"/>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        PopupButtonID="txtDPFecha" TargetControlID="txtDPFecha"  />
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtDPFecha" Display="Dynamic" 
                        ErrorMessage="* Fecha Invalida" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="AgregarDPago" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDPFecha" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPago"></asp:RequiredFieldValidator>
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMontoPago" runat="server" Text="Monto"></asp:Label>
                        <asp:TextBox ID="txtDPMonto" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" 
                        TargetControlID="txtDPMonto" ValidChars="." /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDPMonto" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPago">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDPMonto" 
                        Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="Red" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" 
                        ValidationGroup="AgregarDPago" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMetodoPagoPagos" runat="server" Text="Método de Pago"></asp:Label>
                        <asp:DropDownList ID="ddlDPMetodoPago" runat="server" CssClass="form-control">
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class = "row justify-content-center">
                    <div class = "form-goup col-md-6 col-sm-12">
                        <asp:Label ID="lblObservacionesPagos" runat="server" Text="Observaciones"></asp:Label>
                        <asp:TextBox ID="txtDPObservaciones" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                    </div>
                </div>
                <div class = "row mt-3">
                    <div class = "form-group col-md-3">
                        <asp:Button ID="btnAgregarDpago" runat="server" class="btn btn-outline-primary" onclick="btnAgregarDpago_Click" 
                        Text="Agregar Pago" ValidationGroup="AgregarDPago" />
                    </div>
                </div>
                <div id = "TablaPagos" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvDPagos" runat="server" AutoGenerateColumns="False" CssClass="style124" 
                    onrowcommand="gvDPagos_RowCommand" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderText="Monto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MetododePago" HeaderText="Metodo de Pago" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField CommandName="EliminarDPago" ItemStyle-HorizontalAlign="Center" Text="Eliminar" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>  <%-------------------- Termina Cuarta Sección - Pagos --------------------%>
       
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
                        <asp:Button ID="btnGenerarFactura" runat="server" class="btn btn-outline-primary" OnClick="btnGenerarFactura_Click" Text="Generar Factura" 
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

           <div id="DivDPagos" runat="server">
           
           
           </div> 

           

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
            <asp:PostBackTrigger ControlID="gvDetalles" />
            <asp:PostBackTrigger ControlID="BtnVistaPrevia" />
        </Triggers>
    </asp:UpdatePanel>    
</asp:Content>
