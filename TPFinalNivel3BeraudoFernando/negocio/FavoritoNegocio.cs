using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using dominio;

namespace negocio
{
    public class FavoritoNegocio
    {

        public int insertarNuevoFav(Favorito nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into FAVORITOS (IdUser, IdArticulo) output inserted.id values (@IdUser, @IdArticulo)");
                datos.setearParametros("@Id", nuevo.Id);
                datos.setearParametros("@IdUser", nuevo.IdUser);
                datos.setearParametros("@IdArticulo", nuevo.IdArticulo);
                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarFavorito(int id, int IdUser)
        {
            try
            {    
                AccesoDatos datos = new AccesoDatos();

                datos.setearConsulta("delete from FAVORITOS where idArticulo = @idArt and idUser = @idUser");
                datos.setearParametros("@idArt", id);
                datos.setearParametros("@idUser", IdUser);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
