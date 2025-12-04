using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Web_Catalogo
{
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["trainee"]))
                    {
                        string id = Session["UserId"].ToString();

                        if (id != "" && !IsPostBack)
                        {                            
                            ArticuloNegocio negocio = new ArticuloNegocio();
                            Session.Add("ListaArticulosFav", negocio.ListarFav(id));
                            dgvFavoritos.DataSource = Session["ListaArticulosFav"];
                            dgvFavoritos.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void dgvFavoritos_SelectedIndexChanged(object sender, EventArgs e)
        {
            FavoritoNegocio negocio = new FavoritoNegocio();
            int id = int.Parse(dgvFavoritos.SelectedDataKey.Value.ToString());
            int IdUser = (int)Session["UserId"];

            negocio.eliminarFavorito(id, IdUser);
            Response.Redirect("Favoritos.aspx");
        }
    }
}