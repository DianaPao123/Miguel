<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrPagos.aspx.cs" Inherits="GAFWEB.wfrPagos" %>
<%@ Register Assembly="DropDownChosen" Namespace="CustomDropDown" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />




<asp:UpdatePanel ID="up1" runat="server"  UpdateMode="Conditional" defaultbutton="btnGenerar" >
    <ContentTemplate>
                        <div class="tab-content">
                           
                             <div class="card mt-2">
                                <div class="panel panel-default">
                                     <div class="panel-heading">
                                         CFDI Pagos</div>
                                    <div class="panel-body">
                               

                                        
                                            <div class = "row"> 
                                        <div class="form-group col-md-9 col-sm-6">
                                            <asp:Label ID="Label7" runat="server" Text="Empresa"></asp:Label>
                                          
                                               <asp:TextBox runat="server" ID="txtEmpresa" Enabled="false" CssClass="form-control" />
                                        </div>

                                        </div>
                                         <div class = "row"> <%--Cliente--%>
                    <div class = "form-group col-md-6 col-sm-12">
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                           <asp:TextBox runat="server" ID="txtClientes" CssClass="form-control" Enabled="false" />
                                               
                    </div>
                    </div>
                                          <div class = "row">
              <div class = "form-group col-md-4 col-sm-6">
                   
    <asp:Label ID="lblFechaPagoComp" runat="server" Text="PreFolio"></asp:Label>
    <asp:TextBox ID="txtPreFolio" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
               
                   </div>
       
  
      <div class = "form-group col-md-4 col-sm-6">
     
    <asp:Label ID="lblMontoComp" runat="server"  Text="Monto" ></asp:Label>
    <asp:TextBox ID="txtMonto" runat="server"   CssClass="form-control" Enabled="false"></asp:TextBox>
             
    </div>
   
  </div>
      
         <div class = "row">
         <div class = "form-group col-md-12 col-sm-6">
     
            <asp:Label ID="lblError" runat="server" ForeColor="Red" />
            </div>
          </div>
  <div class = "row">
<div class = "form-group col-md-4 col-sm-6">
</div>
</div>


  <div  class = "row justify-content-center">
              
    <asp:GridView runat="server" ID="gvPagos" AutoGenerateColumns="False" CssClass="style124"
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvPagos_RowCommand" 
            DataKeyNames="idPreFactura"
        onselectedindexchanged="gvPagos_SelectedIndexChanged" 
          onrowediting="gvPagos_RowEditing" 
          onrowcancelingedit="gvPagos_RowCancelingEdit" 
          onrowupdating="gvPagos_RowUpdating" 
          onpageindexchanging="gvPagos_PageIndexChanging">
			<Columns>
              	<asp:BoundField ReadOnly="True"  HeaderText="PreFolio" DataField="PreFolio"  ItemStyle-HorizontalAlign="Center" />
         		<asp:BoundField ReadOnly="True" HeaderText="Monto Factura" DataFormatString="{0:C}" DataField="Total"  ItemStyle-HorizontalAlign="Center" />
              	<asp:BoundField ReadOnly="True" HeaderText="SaldoAnterior" DataFormatString="{0:C}" DataField="SaldoAnteriorPago"  ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Monto a Pagar" ItemStyle-HorizontalAlign="Center"  >  
                    <ItemTemplate >  
                        <asp:Label ID="lbl_Monto" runat="server"  Text='<%#Eval("SaldoAnteriorPago")   %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Monto" runat="server" Text='<%#Eval("SaldoAnteriorPago") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                </asp:TemplateField> 
                <asp:BoundField ReadOnly="True" HeaderText="Parcialidad" DataFormatString="{0:D}" DataField="Parcialidad"  ItemStyle-HorizontalAlign="Center" />
              	
<%--		<asp:CommandField ShowEditButton="true" />--%>
<asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Aplicar" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Actualizar" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancelar" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField> 

        	</Columns>
		</asp:GridView>
        </div>
        
        <br />
        <div>  <strong>Documentos Relacionados</strong></div>
        <br />
  <div  class = "row justify-content-center">
              
    <asp:GridView runat="server" ID="gvPagosParcialidad" AutoGenerateColumns="False" CssClass="style124"
			Width="100%" ShowHeaderWhenEmpty="True"   DataKeyNames="idPreFactura" 
          OnRowCommand="gvPagosParcialidad_RowCommand" onpageindexchanging="gvPagosParcialidad_PageIndexChanging"
       >
			<Columns>
              	<asp:BoundField ReadOnly="True"  HeaderText="PreFolio" DataField="PreFolio"  ItemStyle-HorizontalAlign="Center" />
         		<asp:BoundField ReadOnly="True" HeaderText="SaldoAnterior" DataFormatString="{0:C}" DataField="SaldoAnteriorPago"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ReadOnly="True" HeaderText="Monto Pagado" DataFormatString="{0:C}" DataField="MontoPagado"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ReadOnly="True" HeaderText="Parcialidad" DataFormatString="{0:D}" DataField="Parcialidad"  ItemStyle-HorizontalAlign="Center" />
              	
<%--		<asp:CommandField ShowEditButton="true" />--%>
               <asp:ButtonField CommandName="EliminarPracialidad" ItemStyle-HorizontalAlign="Center" Text="Eliminar"  />

        	</Columns>
		</asp:GridView>
        </div>       

        <br />
        <asp:CheckBox runat="server" ID="cbDoctoRelacionado" Text="Active la casilla si su factura es 3.2" 
                    AutoPostBack="True" oncheckedchanged="cbDoctoRelacionado_CheckedChanged" 
                    style="font-weight: 700" Visible="false"/>
 <div id="RelacionadosDIV" runat="server">
            <asp:Panel ID="Panel5" runat="server"  CssClass="page1" BorderStyle="Double" HorizontalAlign="Left" Width="100%" > 
		
          <div class = "row"> 
                    <div class = "form-group col-md-2">
             <asp:Label ID="Label5" runat="server" Text="  Subir factura 3.2"></asp:Label>  
             </div>
             <div class = "form-group col-md-6">
		    <asp:FileUpload runat="server" ID="archivoPagos" Height="50px" CssClass="form-control" />
             <asp:RegularExpressionValidator ID="REGEXFileUploadLogo" runat="server"
              ErrorMessage="Solo xml" ControlToValidate="archivoPagos" ForeColor="Red"
              ValidationExpression= "(.*).(.xml|.XML)$" />
             </div>
                    <div class = "form-group col-md-3">
           
             <asp:Button runat="server" ID="btnSubir" Text="Subir" class="btn btn-outline-primary" 
                onclick="btnSubir_Click"/>
            </div>
            </div>

       
          <div id="DivDoctoRelacionado"  style="width:100%" runat="server" >
         <table width="100%">

<tr>
<td class="text-right">
    <asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"></asp:Label>
    IdDocumento:</td>
<td>
    <asp:TextBox ID="txtIdDocumento" runat="server"  Enabled="false" Width="258px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIdDocumento" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarDocumento" />
    </td>
<td class="text-right">&nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td class="text-right">
     Folio:</td>
<td>
    <asp:TextBox ID="txtFolioD" runat="server" Width="100px" Enabled="false"></asp:TextBox>
    </td>
<td class="text-right">MetodoDePagoDR:</td>
<td><asp:DropDownList runat="server" ID="ddlMetodoDePagoDR" Enabled="false">
                            <asp:ListItem runat="server" Text="Pago en parcialidades o diferido" Value="PPD"></asp:ListItem>
                              <asp:ListItem runat="server" Text="Pago en una sola exhibición" Value="PUE"></asp:ListItem>
                          </asp:DropDownList>
    </td>
</tr>
<tr>
<td class="text-right">MonedaDR:</td>
<td>
       <cc1:DropDownListChosen ID="ddlMonedaDR" runat="server" AllowSingleDeselect="true" AutoPostBack="True" CausesValidation="false" 
                        DataPlaceHolder="Escriba aquí..." NoResultsText="No hay resultados coincidentes." 
                        OnSelectedIndexChanged="ddlMonedaDR_SelectedIndexChanged" width="206px" SelectMethod="" CssClass="form-control">
                        </cc1:DropDownListChosen>
    </td>
<td class="text-right">

  </td>
<td class="text-right">
    <asp:Label ID="lblTipoCambioDR" runat="server" Text="Tipo Cambio:" 
        Visible="False" />
    </td>
<td>
    <asp:TextBox ID="txtTipoCambioDR" runat="server" Visible="False" Width="86px" />
    </td>
</tr>
<tr>
<td class="text-right"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>NumParcialidad:</td>
<td>
    <asp:TextBox ID="txtNumParcialidad" runat="server" Width="100px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumParcialidad" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarDocumento" />
                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                       TargetControlID="txtNumParcialidad" />
    </td>
<td class="text-right"><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>ImpSaldoAnt:</td>
<td>
    <asp:TextBox ID="txtImpSaldoAnt" runat="server" Width="100px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtImpSaldoAnt" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarDocumento" />
                          <asp:CompareValidator ID="cvPrecio" runat="server" ControlToValidate="txtImpSaldoAnt" Display="Dynamic" 
                        ErrorMessage="* Precio invalido" ForeColor="Red" Operator="DataTypeCheck" Type="Double" ValidationGroup="AgregarDocumento" />

    </td>
</tr>
<tr>
<td class="text-right"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>ImpPagado:</td>
<td>
    <asp:TextBox ID="txtImpPagado" runat="server" Width="100px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImpPagado" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarDocumento" />
                                              <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtImpPagado" Display="Dynamic" 
                        ErrorMessage="* Precio invalido" ForeColor="Red" Operator="DataTypeCheck" Type="Double" ValidationGroup="AgregarDocumento" />

    </td>
<td class="text-right"><asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>ImpSaldoInsoluto:</td>
<td>
    <asp:TextBox ID="txtImpSaldoInsoluto" runat="server" Width="100px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtImpSaldoInsoluto" Display="Dynamic" 
                        ErrorMessage="* Requerido" ForeColor="Red" ValidationGroup="AgregarDocumento" />
                                                                      <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtImpSaldoInsoluto" Display="Dynamic" 
                        ErrorMessage="* Precio invalido" ForeColor="Red" Operator="DataTypeCheck" Type="Double" ValidationGroup="AgregarDocumento" />
    </td>
</tr>

<tr>
<td colspan="4" style="text-align: right">
<asp:Button runat="server" ID="AgregarDocumento" Text="Agregar Documento" 
        ValidationGroup="AgregarDocumento"  class="btn btn-outline-primary" 
        onclick="btnAgregarDocumento_Click"/>
</td>
</tr>
<tr>
<td colspan="5">
    <asp:GridView runat="server" ID="gvDocumento" AutoGenerateColumns="False" CssClass="style124"
			Width="100%" ShowHeaderWhenEmpty="True" onrowcommand="gvDocumento_RowCommand">
			<Columns>
                <asp:BoundField HeaderText="IdDocumento" DataField="IdDocumento" ItemStyle-HorizontalAlign="Center"  />
				<asp:BoundField HeaderText="Folio" DataField="Folio"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="MonedaDR" DataField="MonedaDR" ItemStyle-HorizontalAlign="Center" />
               	<asp:BoundField HeaderText="NumParcialidad" DataField="NumParcialidad"  ItemStyle-HorizontalAlign="Center" />
                	<asp:BoundField HeaderText="ImpPagado" DataField="ImpPagado" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="ImpSaldoAnt" DataField="ImpSaldoAnt" ItemStyle-HorizontalAlign="Center" />
				<asp:BoundField HeaderText="ImpSaldoInsoluto" DataField="ImpSaldoInsoluto" ItemStyle-HorizontalAlign="Center" />
					
				<asp:ButtonField Text="Eliminar" CommandName="EliminarDocumento" Visible="False" ItemStyle-HorizontalAlign="Center" />
			</Columns>
		</asp:GridView></td>
</tr>
</table>
    
        
      </div>
      </asp:Panel>
      </div>
        <br />
                                   

                                         <div class = "row"> 
                                       <div class="form-group col-md-5">
                                        <asp:Button ID="btnGenerar" runat="server"  class="btn btn-outline-primary" OnClick="btnGenerar_Click" Text="Generar" 
                       ValidationGroup="CrearFactura"   />
                     <%--   <asp:Button ID="Button1" runat="server" class="btn btn-outline-primary" OnClick="Button1_Click" Text="Generar" 
                        ValidationGroup="CrearFactura" />
                       <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Confirma que deseas generar el pago" 
                        TargetControlID="Button1" />
                     --%>

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
