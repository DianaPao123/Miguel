<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="wfrDevoluciones.aspx.cs" Inherits="GAFWEB.wfrDevoluciones" %>
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
                Devoluciones
            </div>
          </div>  <%--Termina Primera Sección - Generar CFDI--%>

     <%-------------------- Cuarta Sección - Pagos --------------------%>

        <div class = "card mt-2" id="PnlPagos" runat="server">
           
            <div class = "card-body" id="DivDPagos" runat="server" visible="true">
            
                <div id = "Div1" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvPagos" runat="server" AutoGenerateColumns="False" CssClass="style124" 
                     ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="idPagos">
                        <Columns>
                           <asp:BoundField DataField="fecha" HeaderText="FechaRegistro" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderText="Monto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-HorizontalAlign="Center" />
                         </Columns>
                    </asp:GridView>
                </div>
                <br />
           
                <div id = "TablaPagos" runat="server" class = "row justify-content-center">
                    <asp:GridView ID="gvDPagos" runat="server" AutoGenerateColumns="False" CssClass="style124" 
                    onrowdatabound="gvDPagos_RowDataBound"
                    onrowcommand="gvDPagos_RowCommand" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="idPagos">
                        <Columns>
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="Monto" DataFormatString="{0:C}" HeaderText="Monto" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MetodoPago" HeaderText="Metodo de Pago" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Banco" HeaderText="Banco" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ClabeInterbancaria" HeaderText="Clabe" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Para" HeaderText="De" ItemStyle-HorizontalAlign="Center" />
                           <asp:BoundField DataField="Cliente" HeaderText="Cliente" ItemStyle-HorizontalAlign="Center" />
                  
              <asp:TemplateField  HeaderText="Pago" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate> 
                         <asp:Button class="btn btn-outline-primary"   runat="server" Text="Pago"  CommandName="Pago" ID="btnPagarP" CommandArgument='<%#Eval("idPagos") %>' Visible='<%# (bool)Eval("EsRegistrado") != true  %>'  />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnPagarP" ConfirmText="¿Desea Pagar ?" />
            
                </ItemTemplate>
            </asp:TemplateField>            </Columns>
                    </asp:GridView>
                </div>

           
            </div>

            <div class = "form-group col-md-4 col-sm-6">
                        <asp:Label ID="Label3" runat="server" Text="Saldo Pendiente"></asp:Label>
                        <asp:TextBox ID="txtSaldoPendiente" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
              
                </div>

               <div class = "col-sm-12 mt-3">
                          <asp:Button ID="btnGenerarFactura" runat="server" class="btn btn-outline-primary" OnClick="btnGenerarFactura_Click" Text="Regresar" 
                        ValidationGroup="CrearFactura"/>
                      
                    </div>
                    <br />
        </div>  <%-------------------- Termina Cuarta Sección - Pagos --------------------%>
     
       

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

   
        


            <%--Impuestos--%>
            

            <%-------------------- Pagos --------------------%>
                     
     </ContentTemplate>



        
    </asp:UpdatePanel>    
</asp:Content>
