<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrPrePagos.aspx.cs" Inherits="GAFWEB.wfrPrePagos" %>
<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />

<asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" >
    <ContentTemplate>
                        <div class="tab-content">
                           
                             <div class="card mt-2">
                                <div class="panel panel-default">
                                     <div class="panel-heading">
                                         CFDI Pagos</div>
                                    <div class="panel-body">

                                          <div class = "row"> 
                                        <div class="form-group col-md-9 col-sm-6">
                                         
                                    <asp:Label ID="Label2" runat="server" Text="Giro"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlGiro" DataTextField="Descripcion"
                        DataValueField="Id_Giro" class = "form-control" AutoPostBack="True" 
                            onselectedindexchanged="ddlGiro_SelectedIndexChanged">                  
                    </asp:DropDownList>
                    </div>
                    </div>
                                            <div class = "row"> 
                                        <div class="form-group col-md-9 col-sm-6">
                                            <asp:Label ID="Label7" runat="server" Text="Empresa"></asp:Label>
                                            <asp:DropDownList ID="ddlEmpresa" runat="server" 
                                            CssClass="form-control" DataTextField="RazonSocial" DataValueField="idEmpresa"
                                                CausesValidation="false" ValidationGroup="frmTab1" AutoPostBack="True" 
                                               >
                                            </asp:DropDownList>
                                        </div>

                                        </div>
                                         <div class = "row"> <%--Cliente--%>
                    <div class = "form-group col-md-6 col-sm-12">
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlClientes" AutoPostBack="True" 
                            DataTextField="RazonSocial" DataValueField="idCliente" 
                        CssClass="form-control" 
                            onselectedindexchanged="ddlClientes_SelectedIndexChanged" />                        
                    </div>
                    </div>
                                          <div class = "row">
              <div class = "form-group col-md-4 col-sm-6">
                   
    <asp:Label ID="lblFechaPagoComp" runat="server" Text="FechaPago"></asp:Label>
    <asp:TextBox ID="txtFechaPagoP" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender33" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txtFechaPagoP" 
                            TargetControlID="txtFechaPagoP" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFechaPagoP" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="CrearFactura" />
                   </div>
       <div class = "form-group col-md-4 col-sm-6">
             
    <asp:Label ID="lblFormaDePagoPComp" runat="server"  Text="FormaDePago"></asp:Label>
   <asp:DropDownList runat="server" ID="ddlFormaPagoP" AutoPostBack="True" CssClass="form-control"
                         OnSelectedIndexChanged="ddlFormaPagoP_SelectedIndexChanged" 
                         style="margin-left: 0px" >
   <asp:ListItem runat="server" Value="01" Text="Efectivo"></asp:ListItem>
<asp:ListItem runat="server" Value="02" Text="Cheque nominativo"></asp:ListItem>
<asp:ListItem runat="server" Value="03" Text="Transferencia electrónica de fondos"></asp:ListItem>
<%--<asp:ListItem runat="server" Value="04" Text="Tarjeta de crédito"></asp:ListItem>
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
<asp:ListItem runat="server" Value="99" Text="Por definir"></asp:ListItem>--%>
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
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="CrearFactura" />
             
    </div>
   
  </div>
  

  
        
        
        <br />
                                        <div class = "row"> 
                    <div class = "form-group col-md-6">
               
		    <asp:FileUpload runat="server" ID="archivoPagos" Height="50px" CssClass="form-control" />
             <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server"
              ErrorMessage="Solo Imagenes" ControlToValidate="archivoPagos" 
              ValidationExpression= "(.*).(.jpg|.JPG|.gif|.GIF|.jpeg|.JPEG|.bmp|.BMP|.png|.PNG|.pdf|.PDF)$" />
             </div>
                    <div class = "form-group col-md-3">
           
             <asp:Button runat="server" ID="btnSubir" Text="Subir" class="btn btn-outline-primary" 
                onclick="btnSubir_Click"/>
            </div>
            </div>

            <div class = "row"> 
                                       <div class="form-group col-md-5">
                                        <asp:Button ID="btnBuscar" runat="server" class="btn btn-outline-primary" OnClick="btnBuscar_Click" Text="Buscar" 
                        />
                </div>
                
                                       </div>
                                               <div class = "row"> 
                                        <div class="form-group col-md-9 col-sm-6">
                                     <asp:Label ID="Label1" runat="server" Text="Facturas Pendientes por Liquidar" 
                                                style="font-weight: 700"></asp:Label>
                 </div>
                 </div>

             <div  class = "row justify-content-center">
               
    <asp:GridView runat="server" ID="gvPagos" AutoGenerateColumns="False" CssClass="style124"
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvPagos_RowCommand" 
            DataKeyNames="idPreFactura" onrowdatabound="gvPagos_RowDataBound"
          onpageindexchanging="gvPagos_PageIndexChanging">
			<Columns>
              	<asp:BoundField ReadOnly="True"  HeaderText="PreFolio" DataField="PreFolio"  ItemStyle-HorizontalAlign="Center" />
         		<asp:BoundField ReadOnly="True" HeaderText="Monto Factura" DataFormatString="{0:C}" DataField="Total"  ItemStyle-HorizontalAlign="Center" />
              	<asp:BoundField ReadOnly="True" HeaderText="SaldoAnterior" DataFormatString="{0:C}" DataField="SaldoAnteriorPago"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ReadOnly="True" HeaderText="Monto a Pagar" DataFormatString="{0:C}" DataField="SaldoAnteriorPago"  ItemStyle-HorizontalAlign="Center" />
                
                <%--<asp:TemplateField HeaderText="Monto a Pagar" ItemStyle-HorizontalAlign="Center">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Monto" runat="server" Text='<%#Eval("SaldoAnteriorPago") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Monto" runat="server" Text='<%#Eval("SaldoAnteriorPago") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                </asp:TemplateField> --%>
                <asp:BoundField ReadOnly="True" HeaderText="Parcialidad" DataFormatString="{0:D}" DataField="Parcialidad"  ItemStyle-HorizontalAlign="Center" />
              	
<%--		<asp:CommandField ShowEditButton="true" />--%>


        	</Columns>
		</asp:GridView></td>
        </div>
        
  <br />
                                         <div class = "row"> 
                                       <div class="form-group col-md-5">
                                        <asp:Button ID="btnGenerar" runat="server" class="btn btn-outline-primary" OnClick="btnGenerar_Click" Text="Generar" 
                        ValidationGroup="CrearFactura"/>
                                               <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Confirma que deseas generar el complemento de pago" 
                        TargetControlID="btnGenerar" />
                </div>
                
                                       </div>

                                       <div style=" width:100% ">
           <div style="float:right; text-align:right;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" />
            </div>
          </div>

                                       
    </div>
                                    </div>
                                </div>
                            </div>
                         
                            
                        </div>
                    
</ContentTemplate>
<Triggers>
       <asp:PostBackTrigger  ControlID="btnSubir"/>
         </Triggers>
</asp:UpdatePanel>
</asp:Content>
