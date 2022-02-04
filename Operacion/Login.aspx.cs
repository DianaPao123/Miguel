using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Business;


namespace GAFWEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
          //  FormsAuthentication.SignOut();//se borra la cookie de autenticacion
            //var u = Autentificacion.Autenticar(Login1.UserName, Login1.Password);
            //if (u != null)
            //{
            //    Sesion s = new Sesion();
            //    s.Id = (System.Guid)u.idUsuario;
            //    s.Nombre = u.Nombre;
            //    s.Rol = u.Rol;
            //    s.Usuario = u.Usuario1;
            //    Session["sessionRGV"] = s;
            //    e.Authenticated = true;
            //    //FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);

            //}
            //else
            //    e.Authenticated = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";

            var u = Autentificacion.AutenticarOperador(txtEmail.Text, txtPassword.Text);
            if (u != null)
            {
                Sesion s = new Sesion();
                s.Id = (System.Guid)u.idUsuario;
                s.Nombre = u.Nombre;
                s.ApellidoM = u.ApellidoM;
                s.ApellidoP = u.ApellidoP;
                s.Rol = u.Rol;
                s.Usuario = u.Usuario1;
                Session["sessionRGV"] = s;

                FormsAuthentication.SetAuthCookie(txtEmail.Text, true);
                Response.Redirect("Default.aspx");
              //  FormsAuthentication.RedirectFromLoginPage(txtmobile.Text, true);
                //      Response.Redirect("Default.aspx");

            }
            else
            {
                lblmsg.Text = "Error al iniciar sesión";
                FormsAuthentication.SignOut();
            }
        }
       
    }
}