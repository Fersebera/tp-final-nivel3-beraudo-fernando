using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Web_Catalogo
{
    public partial class Detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            ddlMarca.Enabled = false;
            ddlCategoria.Enabled = false;
            txtPrecio.Enabled = false;
            txtDescripcion.Enabled = false;            
            
            try
            {
                if (!IsPostBack)
                {
                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarca = negocioMarca.Listar();

                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                    List<Categoria> listaCategoria = negocioCategoria.Listar();

                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ddlCategoria.DataSource = listaCategoria;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();
                }

                string id = Request.QueryString["Id"] != null ? Request.QueryString["Id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo seleccionado = (negocio.Listar(id))[0];

                    //guardo Articulo seleccionado en sesión
                    Session.Add("ArtSeleccionado", seleccionado);

                    //pre cargar los datos
                    txtId.Text = id;
                    txtCodigo.Text = seleccionado.Codigo;
                    txtNombre.Text = seleccionado.Nombre;

                    ddlMarca.SelectedValue = seleccionado.Brand.Id.ToString();
                    ddlCategoria.SelectedValue = seleccionado.Category.Id.ToString();

                    txtPrecio.Text = (seleccionado.Precio).ToString();
                    txtDescripcion.Text = seleccionado.Descripcion;
                    imgArticulo.ImageUrl = seleccionado.ImagenUrl;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}