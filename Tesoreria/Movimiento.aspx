<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Movimiento.aspx.cs" Inherits="GAFWEB.Movimiento" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />

<asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>
                        <div class="tab-content">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <asp:Label ID="Label3" runat="server" Text="Fecha"></asp:Label>
                                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" />
                                    <asp:CompareValidator ID="cvFecha" runat="server" ControlToValidate="txtFecha" Display="Dynamic"
                                        ErrorMessage="* Fecha Invalida" ForeColor="Red" Operator="DataTypeCheck" Type="Date" />
                                    <asp:CalendarExtender ID="ceFecha" runat="server" Animated="False" Format="dd/MM/yyyy"
                                        PopupButtonID="txtFecha" TargetControlID="txtFecha" />
  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFecha" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="frmTab1" />
                                     </div>
                                <div class="form-group col-md-3">
                                    <asp:Label ID="Label4" runat="server" Text="Tipo de Movimiento"></asp:Label>
                                    <asp:DropDownList ID="ddlTipoMovimiento" runat="server" CssClass="form-control">
                                              <asp:ListItem Value="EFECTIVO" Text="EFECTIVO" ></asp:ListItem> 
                                             <asp:ListItem Value="CHEQUE" Text="CHEQUE" ></asp:ListItem> 
                                               <asp:ListItem Value="TRANSFERENCIA" Text="TRANSFERENCIA" ></asp:ListItem> 
                                             
                                    </asp:DropDownList>
                                 </div>
                                <div class="form-group col-md-3">
                                    <asp:Label ID="Label5" runat="server" Text="Monto"></asp:Label>
                                    <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control text-right"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtMonto" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="frmTab1" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtMonto" ForeColor="Red" Display="Dynamic" ErrorMessage="Dato invalido" 
                        ValidationExpression="\d+\.?\d?\d?\d?\d?\d?\d?" ValidationGroup="frmTab1" />
                                  </div>
                                <div class="form-group col-md-3">
                                    <asp:Label ID="Label14" runat="server" Text="Referencia"></asp:Label>
                                    <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" MaxLength="1000"></asp:TextBox>
                                </div>
                            </div>
                             <div class="card mt-2">
                                <div class="panel panel-default">
                                     <div class="panel-heading">
                                        <strong>Origen</strong></div>
                                    <div class="panel-body">
                                        <div class="form-group col-md-2">
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
                                        <div class="form-group col-md-5">
                                            <asp:Label ID="Label7" runat="server" Text="Empresa"></asp:Label>
                                            <asp:DropDownList ID="ddlOrigenEmpresa" runat="server" 
                                            CssClass="form-control" DataTextField="nombreCuenta" DataValueField="idEmpresa"
                                                CausesValidation="false" ValidationGroup="frmTab1" AutoPostBack="True" 
                                                onselectedindexchanged="ddlOrigenEmpresa_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-5">
                                            <asp:Label ID="Label8" runat="server" Text="Cuenta"></asp:Label>
                                            <asp:DropDownList ID="ddlOrigenCuenta" runat="server" CssClass="form-control"
                                            DataTextField="nombreBanco" DataValueField="numeroCuenta"
                                                CausesValidation="false" ValidationGroup="frmTab1">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card mt-2">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <strong>Destino</strong></div>
                                    <div class="panel-body">
                                        <div class="form-group col-md-2">
                                            <asp:Label ID="Label11" runat="server" Text="Línea"></asp:Label>
                                            <asp:DropDownList ID="ddlDestinoLineaEmpresa" runat="server" CssClass="form-control"
                                                CausesValidation="false" ValidationGroup="frmTab1" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDestinoLineaEmpresa_SelectedIndexChanged">
                                                <asp:ListItem Value="A" Text="A" ></asp:ListItem> 
                                               <asp:ListItem Value="B" Text="B" ></asp:ListItem> 
                                               <asp:ListItem Value="C" Text="C" ></asp:ListItem> 
                                               <asp:ListItem Value="D" Text="D" ></asp:ListItem> 
                                            </asp:DropDownList>
                                           </div>
                                        <div class="form-group col-md-5">
                                            <asp:Label ID="Label12" runat="server" Text="Empresa"></asp:Label>
                                            <asp:DropDownList ID="ddlDestinoEmpresa" runat="server" CssClass="form-control"
                                            DataTextField="nombreCuenta" DataValueField="idEmpresa"
                                                CausesValidation="false" ValidationGroup="frmTab1" AutoPostBack="True" 
                                                onselectedindexchanged="ddlDestinoEmpresa_SelectedIndexChanged">
                                            </asp:DropDownList>
                                           </div>
                                        <div class="form-group col-md-5">
                                            <asp:Label ID="Label13" runat="server" Text="Cuenta"></asp:Label>
                                            <asp:DropDownList ID="ddlDestinoCuenta" runat="server" CssClass="form-control"
                                                  DataTextField="nombreBanco" DataValueField="numeroCuenta"
                                                  CausesValidation="false" ValidationGroup="frmTab1">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                      <p> <asp:Label runat="server" ID="lblError" ForeColor="Red" />  </p>
                
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                                 <asp:Button ID="btnGuardarMovimiento" runat="server" class="btn btn-outline-primary" OnClick="btnGuardarMovimiento_Click" Text="Guardar" 
                        ValidationGroup="frmTab1"/>
                
                                </div>
                            </div>
                        </div>
                    
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
