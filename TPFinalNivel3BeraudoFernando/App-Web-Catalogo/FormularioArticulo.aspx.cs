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
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public bool OkEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            OkEliminacion = false;
            try
            {
                //Configuración inicial de la pantalla
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

                //configuración si estamos modificando
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
                    txtUrlImg.Text = seleccionado.ImagenUrl;
                    txtUrlImg_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                if (Validacion.validaTextoVacio(txtCodigo) || Validacion.validaTextoVacio(txtNombre))
                {
                    Session.Add("error", "Debes completar ambos campos...");
                    Response.Redirect("Error.aspx", false);
                }
                else
                {
                    nuevo.Codigo = txtCodigo.Text;
                    nuevo.Nombre = txtNombre.Text;

                    nuevo.Brand = new Marca();
                    nuevo.Brand.Id = int.Parse(ddlMarca.SelectedValue);
                    nuevo.Category = new Categoria();
                    nuevo.Category.Id = int.Parse(ddlCategoria.SelectedValue);

                    nuevo.Precio = decimal.Parse(txtPrecio.Text);
                    nuevo.Descripcion = txtDescripcion.Text;
                    nuevo.ImagenUrl = txtUrlImg.Text;

                    if (Request.QueryString["Id"] != null)
                    {
                        nuevo.Id = int.Parse(txtId.Text);
                        negocio.editar(nuevo);
                    }
                    else
                        negocio.agregar(nuevo);

                    Response.Redirect("ArticulosLista.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }      

        protected void txtUrlImg_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtUrlImg.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            OkEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ArticulosLista.Aspx");
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