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
    public partial class Movimiento : System.Web.UI.Page
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

                    this.ddlDestinoEmpresa.Items.Clear();
                    this.ddlDestinoEmpresa.DataSource = Empresa.GetListForLineMovimientos(ddlDestinoLineaEmpresa.SelectedValue);
                    this.ddlDestinoEmpresa.DataBind();
                    this.ddlDestinoCuenta.Items.Clear();
                    if (ddlDestinoEmpresa.Items.Count > 0)
                    {
                        
                        this.ddlDestinoCuenta.DataSource = Empresa.GetListCuentasMovimientos(Convert.ToInt64(ddlDestinoEmpresa.SelectedValue));
                        ddlDestinoCuenta.DataBind();
                    }
                }
            
            }
        }

        protected void btnGuardarMovimiento_Click(object sender, EventArgs e)
        {
             try
                {
                  

            var M = new Movimientos();
            using (M as IDisposable)
            {
                 var sesion = Session["sessionRGV"] as Sesion;
                MovimientosBancos mo = new MovimientosBancos();
                Empresa_CuentasBancos x= M.GetIdMovimientos(Convert.ToInt16( ddlOrigenEmpresa.SelectedValue), ddlOrigenCuenta.SelectedValue);
                mo.idNumeroCuentaOrigen = x.idEmpresaCuenta;
                mo.EsCargaManual = true;
                mo.EsExceptuado = false;
                mo.EsFacturado = false;
                mo.EsProcesado = false;
                mo.EsVinculada = false;
                mo.fecha =Convert.ToDateTime( txtFecha.Text);
                mo.fechaRegistro = DateTime.Now;
                Empresa_CuentasBancos x2= M.GetIdMovimientos(Convert.ToInt16(ddlDestinoEmpresa.SelectedValue), ddlDestinoCuenta.SelectedValue);
                mo.idNumeroCuentaDestino = x2.idEmpresaCuenta;
                mo.idOrdinal = 0;
                mo.idTransaccionVinculada = 0;
                mo.idUsuarioRegistro = sesion.Id;
                mo.monto =Convert.ToDecimal( txtMonto.Text);
                mo.referencia = txtReferencia.Text;
                mo.tipoDocumento = ddlTipoMovimiento.SelectedValue;
                mo.saldo = 0;
                Int64 v=M.SaveMovimiento(mo);
                if (v != 0)
                {
                    M.SetActualizaSaldoMovimiento(x, mo.monto, "-");
                    M.SetActualizaSaldoMovimiento(x2, mo.monto, "+");
                }
                lblError.Text = "Movimiento guardado correctamente.";
                txtReferencia.Text = "";
                txtMonto.Text = "";
                txtFecha.Text = "";
               // M.SaveMovimiento();
            }
             }

               catch (Exception ex)
             {
                  lblError.Text = "Error al guardar el movimiento.";
              
                    //Logger.Error(ex.Message);
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

        protected void ddlDestinoLineaEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                this.ddlDestinoEmpresa.Items.Clear();
                this.ddlDestinoEmpresa.DataSource = Empresa.GetListForLineMovimientos(ddlDestinoLineaEmpresa.SelectedValue);
                this.ddlDestinoEmpresa.DataBind();
                this.ddlDestinoCuenta.Items.Clear();
                if (ddlDestinoEmpresa.Items.Count > 0)
                {
                
                    this.ddlDestinoCuenta.DataSource = Empresa.GetListCuentasMovimientos(Convert.ToInt64(ddlDestinoEmpresa.SelectedValue));
                    ddlDestinoCuenta.DataBind();
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

        protected void ddlDestinoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Empresa = new GAFEmpresa();
            using (Empresa as IDisposable)
            {
                if (ddlDestinoEmpresa.Items.Count > 0)
                {
                    this.ddlDestinoCuenta.Items.Clear();
                    this.ddlDestinoCuenta.DataSource = Empresa.GetListCuentasMovimientos(Convert.ToInt64(ddlDestinoEmpresa.SelectedValue));
                    ddlDestinoCuenta.DataBind();
                }
            }
        }

    }
}