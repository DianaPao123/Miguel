<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrConceptos.aspx.cs" Inherits="GAFWEB.wfrConceptos" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style>
tr:nth-child(even){background-color: #EBEBEB}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<link rel="Stylesheet" href="Styles/bootstrap4.css" type="text/css" />
<link rel="Stylesheet" href="Styles/bootstrap.min.css" type="text/css" />
<link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />

    
 
	<h1>Conceptos de Facturación de la empresa <asp:Label runat="server" ID="lblEmpresa"></asp:Label></h1>
	<div style="width: 100%; overflow: scroll;height: 10%;">
		<asp:HiddenField runat="server" ID="txtIdEmpresa"/>
		<asp:GridView runat="server" ID="gvDetalles" AutoGenerateColumns="False" DataKeyNames="IdProducto" 
			Width="100%" EmptyDataText="No se han dado de alta conceptos" 
			onrowcommand="gvDetalles_RowCommand" >
			<Columns>
				<asp:BoundField HeaderText="Unidad" DataField="Unidad" />
				<asp:BoundField HeaderText="N° de identificación" DataField="Codigo" />
				<asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
				<asp:BoundField HeaderText="Observaciones" DataField="Observaciones" />
				<asp:BoundField HeaderText="Precio Unitario" DataField="PrecioP" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right"  />
				<asp:ButtonField Text="Editar" CommandName="Editar" />
			</Columns>
		</asp:GridView>
	</div>
	<asp:ModalPopupExtender runat="server" ID="mpeBuscarConcepto" TargetControlID="btnConceptoDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCancelar" PopupControlID="pnlBuscarConcepto" />
	<asp:Panel runat="server" ID="pnlBuscarConcepto" style="text-align: center;" Width="800px" BackColor="White">
		<h1><asp:Label runat="server" ID="lblConcepto" ></asp:Label></h1>
		 <table width="600px">
		<tr>
			<td>Unidad de Medida:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtUnidad" />
				<asp:RequiredFieldValidator runat="server" ID="rfvNombre" ErrorMessage="* Requerido" Display="Dynamic"
				 ControlToValidate="txtUnidad" ValidationGroup="Concepto" />
				  <asp:HiddenField runat="server" ID="txtIdProducto"/>
			</td>
		</tr>
		<tr>
			<td>N° de identificación:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtCodigo"  Width="400px"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>Descripción:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtDescripcion"  Width="400px"></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ID="rfvLugarExpedicion" ErrorMessage="* Requerido" Display="Dynamic"
				 ControlToValidate="txtDescripcion" ValidationGroup="Concepto" />
			</td>
		</tr>
		<tr>
			<td>Observaciones:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtObservaciones"  Width="400px" ></asp:TextBox>
				
			</td>
		</tr>
		 <tr>
			<td>Precio Unitario:</td>
			<td align="left">
				<asp:TextBox runat="server" ID="txtPrecioUnitario"  ></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="* Requerido" Display="Dynamic"
				 ControlToValidate="txtPrecioUnitario" ValidationGroup="Concepto" />
			</td>
		</tr>

	</table>
	<div align="right">
		<asp:Button runat="server" ID="btnGuardar" Text="Guardar"  ValidationGroup="Concepto"
			onclick="btnGuardar_Click"  class="btn btn-outline-primary" />&nbsp;&nbsp;&nbsp;
		<asp:Button runat="server" ID="btnCancelar" Text="Cancelar"  class="btn btn-outline-primary" 
			CausesValidation="False" />
	</div>

		<br />
		
	</asp:Panel>
	<asp:Button runat="server" ID="btnConceptoDummy" style="display: none;"  class="btn btn-outline-primary" />
	<div align="right">
		<asp:Button runat="server" ID="btnNuevoConcepto" Text="Nuevo Concepto"  class="btn btn-outline-primary"  
			onclick="btnNuevoConcepto_Click" />
	</div>
	

</asp:Content>
