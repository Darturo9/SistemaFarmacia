using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Security.Permissions;
using System.Reflection;
using System.Collections;
using System.Security.Claims;
using System.Xml.Linq;

namespace CapaDatos
{
    public class CD_Usuario
    {

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {

                try
                {

                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select u.IdUsuario, u.Documento, u.NombreCompleto, u.Correo, u.Clave, u.Estado, r.IdRol, r.Descripcion from usuario u");
                    query.AppendLine("inner join ROL r on r.IdRol = u.IdRol");



                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);

                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol()
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                }
                            });

                        }
                    }

                }
                catch (Exception ex)
                {

                    lista = new List<Usuario>();
                }
            }

            return lista;

        }

        public int Registrar(Usuario obj, out string Mensaje ) 
        {
            int idusuariogenerado = 0;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

//                    Create proc SP_REGISTRARUSUARIO(

//@Documento varchar(50),
//@NombreCompleto varchar(100),
//@Correo varchar(100),
//@Clave varchar(100),
//@IdRol int,
//@Estado bit,
//@IdUsuarioResultado int output,
//@Mensaje varchar(500) output

//)


                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO".ToString(), oConexion);

                    cmd.Parameters.AddWithValue("Documento",obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    

                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                }

            }
            catch(Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }

            return idusuariogenerado;
        }

    }
}
