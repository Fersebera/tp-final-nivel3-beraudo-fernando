using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class TraineeNegocio
    {
        public int insertarNuevo(Trainee nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("insertarNuevo");
                datos.setearParametros("@email", nuevo.Email);
                datos.setearParametros("@pass", nuevo.Pass);
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

        public bool Login(Trainee trainee)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select id, email, pass, nombre, apellido, fechaNacimiento, admin, imagenPerfil from USERS where email = @email and pass = @pass");
                datos.setearParametros("@email", trainee.Email);
                datos.setearParametros("@pass", trainee.Pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    trainee.Id = (int)datos.Lector["id"];
                    trainee.Admin = (bool)datos.Lector["admin"];
                    if (!(datos.Lector["nombre"] is DBNull))
                        trainee.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["Apellido"] is DBNull))
                        trainee.Apellido = (string)datos.Lector["apellido"];
                    if (!(datos.Lector["fechaNacimiento"] is DBNull))
                        trainee.FechaNacimiento = DateTime.Parse(datos.Lector["fechaNacimiento"].ToString());
                    if (!(datos.Lector["imagenPerfil"] is DBNull))
                        trainee.ImagenPerfil = (string)datos.Lector["imagenPerfil"];

                    return true;
                }
                return false;
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
        public void Actualizar(Trainee user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update USERS set imagenPerfil=@imagen, nombre=@nombre, apellido=@apellido, fechaNacimiento=@fecha Where Id=@id");
                //datos.setearParametros("@imagen", user.ImagenPerfil != null ? user.ImagenPerfil : "");
                datos.setearParametros("@imagen", (object)user.ImagenPerfil ?? DBNull.Value);
                datos.setearParametros("@id", user.Id);
                //datos.setearParametros("@nombre", user.Nombre);
                datos.setearParametros("@nombre", (object)user.Nombre ?? DBNull.Value);
                //datos.setearParametros("@apellido", user.Apellido);
                datos.setearParametros("@apellido", (object)user.Apellido ?? DBNull.Value);
                //datos.setearParametros("@fecha", user.FechaNacimiento);
                datos.setearParametros("@fecha", (object)user.FechaNacimiento ?? DBNull.Value);
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
    }
}
