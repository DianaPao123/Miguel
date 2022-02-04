using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using System.IO;

using System.Data;
using Business;
using System.Configuration;
using Contract;
namespace GAFWEB
{
    public partial class EstadoCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var Empresa = new GAFEmpresa();
                using (Empresa as IDisposable)
                {
                    this.ddlOrigenEmpresa.Items.Clear();
                    this.ddlOrigenEmpresa.DataSource = Empresa.GetListForLineMovimientos(ddlOrigenLineaEmpresa.SelectedValue);
                    this.ddlOrigenEmpresa.DataBind();
                    this.ddlOrigenCuenta.Items.Clear();
                    if (ddlOrigenEmpresa.Items.Count > 0)
                    {
                      
                        this.ddlOrigenCuenta.DataSource = Empresa.GetListCuentasMovimientos(Convert.ToInt64(ddlOrigenEmpresa.SelectedValue));
                        ddlOrigenCuenta.DataBind();
                    }

                    
                }
                this.FillView();

            }
        }


        private void FillView()
        {


             var factu = new Movimientos();
             var M = new Movimientos();
       
             using (M as IDisposable)
             {
                 Empresa_CuentasBancos x = M.GetIdMovimientos(Convert.ToInt16(ddlOrigenEmpresa.SelectedValue), ddlOrigenCuenta.SelectedValue);
             
               var ventas=  factu.GetMovimientos(x.idEmpresaCuenta);
             
            /*
            var filtro = Convert.ToInt16(rbStatus.SelectedValue);
            if (!string.IsNullOrEmpty(this.ddlClientes.SelectedValue))
                using (factu as IDisposable)
                {
                    DateTime fechaInicial = DateTime.Parse(this.txtFechaInicial.Text);
                    DateTime fechaFinal = DateTime.Parse(this.txtFechaFinal.Text).AddDays(1).AddSeconds(-1);
                    if ((fechaFinal - fechaInicial).TotalDays > 93)
                    {
                        lblError.Text = "El rango de fechas no puede ser mayor a 93 dias";
                        return;
                    }
            */
            /*      
            var ventas = factu.GetListPrefactura(fechaInicial, fechaFinal, int.Parse(this.ddlEmpresas.SelectedValue), filtro,
                        int.Parse(this.ddlClientes.SelectedValue));


                    List<vprefactura> lista;
                    {
                        lista = ventas.ToList();
                    }
             
             /* 
                    var gridFatura = new GridView();
                    foreach (DataControlField colum in gvFacturas.Columns)
                    {
                        gridFatura.Columns.Add(colum);
                    }

            */

                    ViewState["facturas"] = ventas;

                    this.gvFacturas.DataSource = ventas;
                    this.gvFacturas.DataBind();

                }
            
        }

        


        protected void ddlOrigenLineaEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                this.ddlOrigenEmpresa.Items.Clear();
                this.ddlOrigenEmpresa.DataSource = Empresa.GetListForLineMovimientos(ddlOrigenLineaEmpresa.SelectedValue);
                this.ddlOrigenEmpresa.DataBind();
                this.ddlOrigenCuenta.Items.Clear();
                if (ddlOrigenEmpresa.Items.Count > 0)
                {
                   
                    this.ddlOrigenCuenta.DataSource = Empresa.GetListCuentasMovimientos(Convert.ToInt64(ddlOrigenEmpresa.SelectedValue));
                    ddlOrigenCuenta.DataBind();
                }
            }
            
        }

        

        protected void ddlOrigenEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                if (ddlOrigenEmpresa.Items.Count > 0)
                {
                    this.ddlOrigenCuenta.Items.Clear();
                    this.ddlOrigenCuenta.DataSource = Empresa.GetListCuentasMovimientos(Convert.ToInt64(ddlOrigenEmpresa.SelectedValue));
                    ddlOrigenCuenta.DataBind();
                }
            }
        }

        protected void btnConsultarMovimiento_Click(object sender, EventArgs e)
        {
            this.FillView();
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFacturas.DataSource = ViewState["facturas"];
            this.gvFacturas.PageIndex = e.NewPageIndex;
            this.gvFacturas.DataBind();
         
        }

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        

    }
}