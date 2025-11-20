using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (id != "")
                {
                    datos.setearConsulta("select A.Id, A.Codigo as Código, A.Nombre, A.Descripcion as Descripción, A.IdMarca, M.Descripcion as Marca, A.IdCategoria, C.Descripcion as Categoría, A.ImagenUrl, A.Precio, A.Activo from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id and A.Id = " + id);
                }
                else
                {
                    datos.setearConsulta("select A.Id, A.Codigo as Código, A.Nombre, A.Descripcion as Descripción, A.IdMarca, M.Descripcion as Marca, A.IdCategoria, C.Descripcion as Categoría, A.ImagenUrl, A.Precio, A.Activo from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id");

                }
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Código"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripción"];
                    aux.Brand = new Marca();
                    aux.Brand.Id = (int)datos.Lector["IdMarca"];
                    aux.Brand.Descripcion = (string)datos.Lector["Marca"];
                    aux.Category = new Categoria();
                    aux.Category.Id = (int)datos.Lector["IdCategoria"];
                    aux.Category.Descripcion = (string)datos.Lector["Categoría"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }
                return lista;
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

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@IdMarca", nuevo.Brand.Id);
                datos.setearParametros("@IdCategoria", nuevo.Category.Id);
                datos.setearParametros("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametros("@Precio", nuevo.Precio);

                datos.ejecutarAccion();
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

        public void editar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @img, Precio = @precio where Id = @id");
                datos.setearParametros("@codigo", art.Codigo);
                datos.setearParametros("@nombre", art.Nombre);
                datos.setearParametros("@descripcion", art.Descripcion);
                datos.setearParametros("@idMarca", art.Brand.Id);
                datos.setearParametros("@idCategoria", art.Category.Id);
                datos.setearParametros("@img", art.ImagenUrl);
                datos.setearParametros("@precio", art.Precio);
                datos.setearParametros("@id", art.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from Articulos where id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarLogico(int id, bool activo = false)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update ARTICULOS set Activo = @activo where id= @id");
                datos.setearParametros("@id", id);
                datos.setearParametros("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public List<Articulo> filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select A.Id, A.Codigo as Código, A.Nombre, A.Descripcion as Descripción, A.IdMarca, M.Descripcion as Marca, A.IdCategoria, C.Descripcion as Categoría, A.ImagenUrl, A.Precio, A.Activo from ARTICULOS A, MARCAS M, CATEGORIAS C where A.IdMarca = M.Id and A.IdCategoria = C.Id and ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "A.Precio < " + filtro;
                            break;
                        default:
                            consulta += "A.Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                if (estado == "Activo")
                    consulta += " and A.Activo = 1";
                else if (estado == "Inactivo")
                    consulta += " and A.Activo = 0";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Código"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripción"];
                    aux.Brand = new Marca();
                    //aux.Brand.Id = (int)datos.Lector["Id"];
                    aux.Brand.Descripcion = (string)datos.Lector["Marca"];
                    aux.Category = new Categoria();
                    //aux.Category.Id = (int)datos.Lector["Id"];                    
                    aux.Category.Descripcion = (string)datos.Lector["Categoría"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
