using System;
using System.ServiceModel;
using GAFContract;
using GAFBusiness;
using System.Text.RegularExpressions;

namespace GAFWEB
{
    public partial class wfrUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {


                string idUsuarioString = Request.QueryString["idUsuario"];

                int idCliente;
                if (!string.IsNullOrEmpty(idUsuarioString) && int.TryParse(idUsuarioString, out idCliente))
                {

                    if (idCliente != 0)
                    {
                        btnSave.Visible = false;
                        btnActualizar.Visible = true;
                        var Cliente = new GAFClientes();
                        Usuario cliente;
                        using (Cliente as IDisposable)
                        {
                            cliente = Cliente.GetUsuario(idCliente);
                            ViewState["idUsuarioGui"] = cliente.idUsuario;
                        }

                        FillView(cliente);
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnActualizar.Visible = false;
                    }
                }


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
        private void FillView(Usuario u)
        {
             txtNombre.Text=u.Nombre ;
             txtApellidoP.Text=u.ApellidoP ;
             txtApellidoM.Text=u.ApellidoM ;
             txtEmail.Text=u.Email ;
             txtUsuario.Text=u.Usuario1;
             txtContraseña.Text=u.Contraseña;
             ddlRol.SelectedValue= u.Rol ;
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            var usu = new Usuarios();

            using (usu as IDisposable)
            {
                Usuario u = new Usuario();
                u.Nombre = txtNombre.Text;
                u.ApellidoP = txtApellidoP.Text;
                u.ApellidoM = txtApellidoM.Text;
                u.idUsuario =Guid.Parse( ViewState["idUsuarioGui"].ToString());
                u.Activo = true;
                u.Email = txtEmail.Text;
                u.Usuario1 = txtUsuario.Text;
                u.Contraseña = txtContraseña.Text;
                u.Rol = ddlRol.SelectedValue;
                bool x = usu.Actualizar(u);
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




    }
}