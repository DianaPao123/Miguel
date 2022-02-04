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
                if (sesion.Rol == "ConsultaContabilidad")
                {
                    NavigationMenu.Items.Clear();
                    MenuItem Item = new MenuItem();
                    Item.Text = "Reportes CFDI";
                    Item.NavigateUrl = "~/wfrConsultaEmitidos.aspx";
                    NavigationMenu.Items.Add(Item);
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
