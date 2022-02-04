using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAFWEB
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        void Page_Init(object sender, EventArgs e)
        {
            var sesion = Session["sessionRGV"] as Business.Sesion;
             if (sesion != null)
                 if (sesion.Rol == "Tesoreria")
                {
                    NavigationMenu.Items.Clear();
                    MenuItem Item = new MenuItem();
                    Item.Text = "Movimiento";
                    Item.NavigateUrl = "~/Movimiento.aspx";
                    NavigationMenu.Items.Add(Item);
                    MenuItem Item2 = new MenuItem();
                    Item2.Text = "Revisión Pago";
                    Item2.NavigateUrl = "~/wfrPagoConsulta.aspx";
                    NavigationMenu.Items.Add(Item2);
                    MenuItem Item3 = new MenuItem();
                    Item3.Text = "Consulta Pago";
                    Item3.NavigateUrl = "~/wfrPagoConsultaRechazadosAceptados.aspx";
                    NavigationMenu.Items.Add(Item3);
                }

            if (Session.IsNewSession)
                System.Web.Security.FormsAuthentication.SignOut();

            if (Session["sessionRGV"] == null)
            {
                this.Response.Redirect("Login.aspx");
            }
        }
    }
}
