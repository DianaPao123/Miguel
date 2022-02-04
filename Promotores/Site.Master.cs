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
            if (Session.IsNewSession)
                System.Web.Security.FormsAuthentication.SignOut();

            if (Session["sessionRGV"] == null)
            {
                this.Response.Redirect("Login.aspx");
            }
            var sesion = Session["sessionRGV"] as Business.Sesion;
            if (sesion.Rol == "PromotorEspecial")
                {
                    MenuItem Item = new MenuItem();
                    Item.Text = "CFDI LineaB";
                    Item.NavigateUrl = "~/wfrFacturaLineaB.aspx";
                    NavigationMenu.Items.Add(Item);
                    MenuItem Item2 = new MenuItem();
                    Item2.Text = "Reportes LineaB";
                    Item2.NavigateUrl = "~/wfrFacturasConsultaB.aspx";
                    NavigationMenu.Items.Add(Item2);
                }
        }
    }
}
