using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace App_Web_Catalogo
{
    public partial class Default : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        public bool FormatoLista { get; set; }
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Session.Add("ListaArticulosHome", negocio.Listar());

            if (!IsPostBack)
            {
                repRepetidor.DataSource = Session["ListaArticulosHome"];
                repRepetidor.DataBind();

                repRepetidor2.DataSource = Session["ListaArticulosHome"];
                repRepetidor2.DataBind();
            }
        }     

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["ListaArticulosHome"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            repRepetidor.DataSource = listaFiltrada;
            repRepetidor.DataBind();

            repRepetidor2.DataSource = listaFiltrada;
            repRepetidor2.DataBind();
        }

        protected void chkLista_CheckedChanged(object sender, EventArgs e)
        {
            FormatoLista = chkLista.Checked;
        }

        protected void chkAvanzadoHome_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzadoHome.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();

            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                repRepetidor.DataSource = negocio.filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text);

                repRepetidor2.DataSource = negocio.filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text);

                repRepetidor.DataBind();
                repRepetidor2.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Response.Redirect("Detalle.aspx?Id=" + id);
        }

        protected void btnFavoritos_Click(object sender, EventArgs e)
        {
            int idArticulo = int.Parse(((Button)sender).CommandArgument);
            Session.Add("ArticuloId", idArticulo);

            Favorito favorito = new Favorito();
            FavoritoNegocio nuevo = new FavoritoNegocio();

            favorito.IdUser = (int)Session["UserId"];
            favorito.IdArticulo = (int)Session["ArticuloId"];
            favorito.Id = nuevo.insertarNuevoFav(favorito);

            // Mostrar el toast (USE COPILOT! que desgracia)
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "toastFavorito", "var toastEl = document.getElementById('toastFavorito'); var toast = new bootstrap.Toast(toastEl); toast.show();", true);

        }
    }
}