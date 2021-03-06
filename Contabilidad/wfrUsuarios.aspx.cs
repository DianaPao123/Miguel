using System;
using System.ServiceModel;
using Contract;
using Business;
using System.Text.RegularExpressions;

namespace GAFWEB
{
    public partial class wfrUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {




            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
              var usu = new Usuarios();

              using (usu as IDisposable)
              {
                  Usuario u = new Usuario();
                  u.Nombre = txtNombre.Text;
                  u.ApellidoP = txtApellidoP.Text;
                  u.ApellidoM = txtApellidoM.Text;
                  u.idUsuario = Guid.NewGuid();
                  u.Activo = true;
                  u.Email = txtEmail.Text;
                  u.Usuario1 = txtUsuario.Text;
                  u.Contraseña = txtContraseña.Text;
                  u.Rol = ddlRol.SelectedValue;
                  bool x= usu.Save(u);
                  if (x == true)
                  {
                      txtApellidoM.Text = "";
                      txtApellidoP.Text = "";
                      txtNombre.Text = "";
                      txtUsuario.Text = "";
                      txtEmail.Text = "";
                      txtContraseña.Text = "";
                      lblError.Text = "Usuario guardado correctamente";
                  }
                  else
                      lblError.Text = "Error al guardar usuario";
             
              }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //  Response.Redirect("wfrClientesConsulta.aspx");
            Response.Redirect("Default.aspx");
        }

        //----------------------------------------------------





    }
}