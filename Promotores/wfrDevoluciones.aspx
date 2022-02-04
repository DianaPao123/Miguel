<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="wfrDevoluciones.aspx.cs" Inherits="GAFWEB.wfrDevoluciones" %>
<%--<%@ Register TagPrefix="cc1" Namespace="WebControls.FilteredDropDownList" Assembly="WebControls.FilteredDropDownList" %>--%>
<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%--<%@ PreviousPageType VirtualPath="~/wfrPrefacturaConsultaDevolucion.aspx" %> --%>

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
                Devoluciones
            </div>
      

     <%-------------------- Cuarta Sección - Pagos --------------------%>

        <div class = "card mt-2" id="PnlPagos" runat="server">
            <div class = "card-header">
                   <asp:Label ID="Label1" runat="server" Text=". Instrucciones para devolución"></asp:Label>
                      
            </div>
            <div class = "card-body" id="DivDPagos" runat="server" visible="true">
            <div class = "card-header">
                  <asp:Label ID="Label2" runat="server" Text=". Devolución Empresa "></asp:Label>
                      
            </div>
             <div class = "row">
             <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label3" runat="server" Text="Total a Devolver Empresa"></asp:Label>
                        <asp:TextBox ID="txtDevolverEmpresa" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
              
                </div>
                </div>
                <div class = "row">
                    <%--<div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblFechaPago" runat="server" Text="Fecha"></asp:Label>
                        <asp:TextBox ID="txtDPFecha" runat="server" CssClass="form-control"/>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        PopupButtonID="txtDPFecha" TargetControlID="txtDPFecha"  />
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtDPFecha" Display="Dynamic" 
                        ErrorMessage="* Fecha Invalida" ForeColor="Red" Operator="DataTypeCheck" Type="Date" ValidationGroup="AgregarDPago" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDPFecha" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPago"></asp:RequiredFieldValidator>
                    </div>--%>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMontoPagoEmpresa" runat="server" Text="Monto"></asp:Label>
                        <asp:TextBox ID="txtDPMontoEmpresa" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" 
                        TargetControlID="txtDPMontoEmpresa" ValidChars="." /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDPMontoEmpresa" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoEmpresa">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDPMontoEmpresa" 
                        Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="Red" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" 
                        ValidationGroup="AgregarDPagoEmpresa" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMetodoPagoEmpresa" runat="server" Text="Método de Pago"></asp:Label>
                        <asp:DropDownList ID="ddlDPMetodoPagoEmpresa" runat="server" CssClass="form-control" 
                            onselectedindexchanged="ddlDPMetodoPagoEmpresa_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class = "row" id="DivBancoEmpresa" runat="server">
                
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblBancoEmpresa" runat="server" Text="Banco:"></asp:Label>
                        <asp:DropDownList ID="ddlBancoEmpresa" runat="server" CssClass="form-control" 
                            >
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                        <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblClaveBancariaEmpresa" runat="server" Text="CLABE interbancaria"></asp:Label>
                        <asp:TextBox ID="txtClaveBancariaEmpresa" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtClaveBancariaEmpresa" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoEmpresa">
                        </asp:RequiredFieldValidator>
                </div>
                <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label11" runat="server" Text="Nombre beneficiario"></asp:Label>
                        <asp:TextBox ID="txtbeneficiarioEmpresa" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtbeneficiarioEmpresa" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoEmpresa">
                        </asp:RequiredFieldValidator>
                </div>
                </div>
                <div class = "row justify-content-center">
                    <div class = "form-goup col-md-6 col-sm-12">
                        <asp:Label ID="lblObservacionesPagosEmpresa" runat="server" Text="Observaciones"></asp:Label>
                        <asp:TextBox ID="txtDPObservacionesEmpresa" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                    </div>
                </div>
                <div class = "row mt-3">
                    <div class = "form-group col-md-3">
                        <asp:Button ID="btnAgregarDpagoEmpresa" runat="server" class="btn btn-outline-primary" onclick="btnAgregarDpagoEmpresa_Click" 
                        Text="Agregar Pago" ValidationGroup="AgregarDPagoEmpresa" />
                    </div>
                </div>
                <div id = "TablaPagosEmpresa" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvDPagosEmpresa" runat="server" AutoGenerateColumns="False" CssClass="style124" 
                    onrowcommand="gvDPagosEmpresa_RowCommand" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                  <%--          <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                  --%>          <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderText="Monto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MetododePago" HeaderText="Metodo de Pago" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ClaveBancaria" HeaderText="Clabe" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField CommandName="EliminarDPago" ItemStyle-HorizontalAlign="Center" Text="Eliminar" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>

                  <div class = "card-header">
                  <asp:Label ID="Label4" runat="server" Text=". Devolución Promotor"></asp:Label>
                  </div>
             <div class = "row">
             <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label5" runat="server" Text="Total a Devolver Promotor"></asp:Label>
                        <asp:TextBox ID="txtDevolverPromotor" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
              </div>
              </div>
             <div class = "row">
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMontoPagoPromotor" runat="server" Text="Monto"></asp:Label>
                        <asp:TextBox ID="txtDPMontoPromotor" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" 
                        TargetControlID="txtDPMontoPromotor" ValidChars="." /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDPMontoPromotor" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoPromotor">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDPMontoPromotor" 
                        Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="Red" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" 
                        ValidationGroup="AgregarDPagoPromotor" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMetodoPagoPromotor" runat="server" Text="Método de Pago"></asp:Label>
                        <asp:DropDownList ID="ddlDPMetodoPagoPromotor" runat="server" CssClass="form-control" 
                            onselectedindexchanged="ddlDPMetodoPagoPromotor_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                  <div class = "row" id="DivBancoPromotor" runat="server">
                
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblBancoPromotor" runat="server" Text="Banco:"></asp:Label>
                        <asp:DropDownList ID="ddlBancoPromotor" runat="server" CssClass="form-control" 
                            >
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                        <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblClaveBancariaPromotor" runat="server" Text="CLABE interbancaria"></asp:Label>
                        <asp:TextBox ID="txtClaveBancariaPromotor" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClaveBancariaPromotor" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoPromotor">
                        </asp:RequiredFieldValidator>
                </div>
                <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label8" runat="server" Text="Nombre beneficiario"></asp:Label>
                        <asp:TextBox ID="txtbeneficiarioPromotor" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbeneficiarioPromotor" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoPromotor">
                        </asp:RequiredFieldValidator>
                </div>
                </div>
              <div class = "row justify-content-center">
                    <div class = "form-goup col-md-6 col-sm-12">
                        <asp:Label ID="lblObservacionesPagosPromotor" runat="server" Text="Observaciones"></asp:Label>
                        <asp:TextBox ID="txtDPObservacionesPromotor" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                    </div>
                </div>
                           <div class = "row mt-3">
                    <div class = "form-group col-md-3">
                        <asp:Button ID="btnAgregarDpagoPromotor" runat="server" class="btn btn-outline-primary" onclick="btnAgregarDpagoPromotor_Click" 
                        Text="Agregar Pago" ValidationGroup="AgregarDPagoPromotor" />
                    </div>
                </div>
               <div id = "TablaPagosPromotor" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvDPagosPromotor" runat="server" AutoGenerateColumns="False" CssClass="style124" 
                    onrowcommand="gvDPagosPromotor_RowCommand" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                         <%--   <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                         --%>   <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderText="Monto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MetododePago" HeaderText="Metodo de Pago" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ClaveBancaria" HeaderText="Clabe" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField CommandName="EliminarDPago" ItemStyle-HorizontalAlign="Center" Text="Eliminar" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>

       <div class = "card-header">
                  <asp:Label ID="Label6" runat="server" Text=". Devolución Cliente"></asp:Label>
                  </div>
             <div class = "row">
             <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label7" runat="server" Text="Total a Devolver Cliente"></asp:Label>
                        <asp:TextBox ID="txtDevolverCliente" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
              </div>
              </div>
             <div class = "row">
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMontoPagoCliente" runat="server" Text="Monto"></asp:Label>
                        <asp:TextBox ID="txtDPMontoCliente" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" 
                        TargetControlID="txtDPMontoCliente" ValidChars="." /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDPMontoCliente" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoCliente">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDPMontoCliente" 
                        Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="Red" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" 
                        ValidationGroup="AgregarDPagoCliente" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMetodoPagoCliente" runat="server" Text="Método de Pago"></asp:Label>
                        <asp:DropDownList ID="ddlDPMetodoPagoCliente" runat="server" CssClass="form-control" 
                            onselectedindexchanged="ddlDPMetodoPagoCliente_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                     <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label12" runat="server" Text="Cliente"></asp:Label>
                        <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control" 
                            onselectedindexchanged="ddlCliente_SelectedIndexChanged" 
                            AutoPostBack="True">
                         </asp:DropDownList>
                    </div>
                </div>
                  <div class = "row" id="DivBancoCliente" runat="server">
                
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblBancoCliente" runat="server" Text="Banco:"></asp:Label>
                        <asp:DropDownList ID="ddlBancoCliente" runat="server" CssClass="form-control" 
                            >
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                        <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblClaveBancariaCliente" runat="server" Text="CLABE interbancaria"></asp:Label>
                        <asp:TextBox ID="txtClaveBancariaCliente" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtClaveBancariaCliente" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoCliente">
                        </asp:RequiredFieldValidator>
                </div>
                <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblbeneficiarioCliente" runat="server" Text="Nombre beneficiario"></asp:Label>
                        <asp:TextBox ID="txtbeneficiarioCliente" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtbeneficiarioCliente" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoCliente">
                        </asp:RequiredFieldValidator>
                </div>
                </div>
              <div class = "row justify-content-center">
                    <div class = "form-goup col-md-6 col-sm-12">
                        <asp:Label ID="lblObservacionesPagosCliente" runat="server" Text="Observaciones"></asp:Label>
                        <asp:TextBox ID="txtDPObservacionesCliente" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                    </div>
                </div>
                           <div class = "row mt-3">
                    <div class = "form-group col-md-3">
                        <asp:Button ID="btnAgregarDpagoCliente" runat="server" class="btn btn-outline-primary" onclick="btnAgregarDpagoCliente_Click" 
                        Text="Agregar Pago" ValidationGroup="AgregarDPagoCliente" />
                    </div>
                </div>
               <div id = "TablaPagosCliente" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvDPagosCliente" runat="server" AutoGenerateColumns="False" CssClass="style124" 
                    onrowcommand="gvDPagosCliente_RowCommand" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                          <%--  <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                         --%>   <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderText="Monto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MetododePago" HeaderText="Metodo de Pago" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ClaveBancaria" HeaderText="Clabe" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Cliente" HeaderText="Cliente" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField CommandName="EliminarDPago" ItemStyle-HorizontalAlign="Center" Text="Eliminar" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>

        <div class = "card-header">
                  <asp:Label ID="Label9" runat="server" Text=". Devolución Contacto"></asp:Label>
                  </div>
             <div class = "row">
             <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label10" runat="server" Text="Total a Devolver Contacto"></asp:Label>
                        <asp:TextBox ID="txtDevolverContacto" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
              </div>
              </div>
             <div class = "row">
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMontoPagoContacto" runat="server" Text="Monto"></asp:Label>
                        <asp:TextBox ID="txtDPMontoContacto" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers, Custom" 
                        TargetControlID="txtDPMontoContacto" ValidChars="." /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDPMontoContacto" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoContacto">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDPMontoContacto" 
                        Display="Dynamic" ErrorMessage="Dato invalido" ForeColor="Red" ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" 
                        ValidationGroup="AgregarDPagoContacto" />
                    </div>
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblMetodoPagoContacto" runat="server" Text="Método de Pago"></asp:Label>
                        <asp:DropDownList ID="ddlDPMetodoPagoContacto" runat="server" CssClass="form-control" 
                            onselectedindexchanged="ddlDPMetodoPagoContacto_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                  <div class = "row" id="DivBancoContacto" runat="server">
                
                    <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblBancoContacto" runat="server" Text="Banco:"></asp:Label>
                        <asp:DropDownList ID="ddlBancoContacto" runat="server" CssClass="form-control" 
                            >
                            <asp:ListItem runat="server" Text="Efectivo" Value="Efectivo"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Cheque" Value="Cheque"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Transferencia" Value="Transferencia"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Depósito" Value="Deposito"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                        <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblClaveBancariaContacto" runat="server" Text="CLABE interbancaria"></asp:Label>
                        <asp:TextBox ID="txtClaveBancariaContacto" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtClaveBancariaContacto" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoContacto">
                        </asp:RequiredFieldValidator>
                </div>
                <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="lblbeneficiarioContacto" runat="server" Text="Nombre beneficiario"></asp:Label>
                        <asp:TextBox ID="txtbeneficiarioContacto" runat="server" CssClass="form-control"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtbeneficiarioContacto" 
                        Display="Dynamic" ErrorMessage="Requerido" ForeColor="Red" ValidationGroup="AgregarDPagoContacto">
                        </asp:RequiredFieldValidator>
                </div>
                </div>
              <div class = "row justify-content-center">
                    <div class = "form-goup col-md-6 col-sm-12">
                        <asp:Label ID="lblObservacionesPagosContacto" runat="server" Text="Observaciones"></asp:Label>
                        <asp:TextBox ID="txtDPObservacionesContacto" runat="server" TextMode="MultiLine" CssClass="form-control"/>
                    </div>
                </div>
                           <div class = "row mt-3">
                    <div class = "form-group col-md-3">
                        <asp:Button ID="btnAgregarDpagoContacto" runat="server" class="btn btn-outline-primary" onclick="btnAgregarDpagoContacto_Click" 
                        Text="Agregar Pago" ValidationGroup="AgregarDPagoContacto" />
                    </div>
                </div>
               <div id = "TablaPagosContacto" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvDPagosContacto" runat="server" AutoGenerateColumns="False" CssClass="style124" 
                    onrowcommand="gvDPagosContacto_RowCommand" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                        <%--    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                        --%>    <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderText="Monto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MetododePago" HeaderText="Metodo de Pago" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ClaveBancaria" HeaderText="Clabe" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField CommandName="EliminarDPago" ItemStyle-HorizontalAlign="Center" Text="Eliminar" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </div>



            </div>
        </div>  <%-------------------- Termina Cuarta Sección - Pagos --------------------%>
     
        <%-------------------- Quinta Sección - Totales --------------------%>

        <div class = "card mt-2">
            <div class = "card-header">
                
            </div>
            <div class = "card-body">
                <div class = "row">
                    
                    <div class = "col-sm-12 mt-3">
                          <asp:Button ID="btnGenerarFactura" runat="server" class="btn btn-outline-primary" OnClick="btnGenerarFactura_Click" Text="Guardar" 
                        ValidationGroup="CrearFactura"/>
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Confirma que deseas generar las devoluciones" 
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
           
                           


            <%--Impuestos--%>
            

            <%-------------------- Pagos --------------------%>
                     

     </ContentTemplate>



        
    </asp:UpdatePanel>    
</asp:Content>
