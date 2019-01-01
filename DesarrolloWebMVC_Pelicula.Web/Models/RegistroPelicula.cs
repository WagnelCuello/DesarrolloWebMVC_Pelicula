using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DesarrolloWebMVC_Pelicula.Web.Models
{
    public class RegistroPelicula
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
            con = new SqlConnection(constr);
        }

        public int GrabarPelicula(Pelicula peli)
        {
            try
            {
                Conectar();
                SqlCommand comando = new SqlCommand("INSERT INTO TBL_PELICULA (Titulo,Director,AutorPrincipal,No_Actores,Duracion,Estreno)" +
                                                    "VALUES(@Titulo,@Director,@AutorPrincipal,@No_Actores,@Duracion,@Estreno)", con);
                comando.Parameters.Add("@Titulo", SqlDbType.VarChar);
                comando.Parameters.Add("@Director", SqlDbType.VarChar);
                comando.Parameters.Add("@AutorPrincipal", SqlDbType.VarChar);
                comando.Parameters.Add("@No_Actores", SqlDbType.Int);
                comando.Parameters.Add("@Duracion", SqlDbType.Float);
                comando.Parameters.Add("@Estreno", SqlDbType.Int);

                comando.Parameters["@Titulo"].Value = peli.Titulo;
                comando.Parameters["@Director"].Value = peli.Director;
                comando.Parameters["@AutorPrincipal"].Value = peli.AutorPrincipal;
                comando.Parameters["@No_Actores"].Value = peli.No_Actores;
                comando.Parameters["@Duracion"].Value = peli.Duracion;
                comando.Parameters["@Estreno"].Value = peli.Estreno;

                con.Open();
                int i = comando.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Este metodo recupera cada una de las peliculas contenidas en la base de datos
        /// </summary>
        /// <returns>Listado de todas las peliculas</returns>
        public List<Pelicula> RecuperarTodos()
        {
            Conectar();
            List<Pelicula> peliculas = new List<Pelicula>();

            try
            {
                SqlCommand com = new SqlCommand("SELECT Codigo,Titulo,Director,AutorPrincipal,No_Actores,Duracion,Estreno FROM TBL_PELICULAS", con);
                con.Open();
                SqlDataReader registros = com.ExecuteReader();

                while (registros.Read())
                {
                    Pelicula peli = new Pelicula()
                    {
                        Codigo = int.Parse(registros["Codigo"].ToString()),
                        Titulo = registros["Titulo"].ToString(),
                        Director = registros["Director"].ToString(),
                        AutorPrincipal = registros["AutorPrincipal"].ToString(),
                        No_Actores = int.Parse(registros["No_Actores"].ToString()),
                        Duracion = Convert.ToDouble(registros["Duracion"].ToString()),
                        Estreno = int.Parse(registros["Estreno"].ToString())
                    };
                    peliculas.Add(peli);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            con.Close();
            return peliculas;
        }

        /// <summary>
        /// Muestra un registro especifico de la base de datos
        /// </summary>
        /// <param name="codigo">codigo del registro especifico a retornar</param>
        /// <returns>registro encontrado en la base de datos</returns>
        public Pelicula Recuperar(int codigo)
        {
            Conectar();
            SqlCommand com = new SqlCommand("SELECT Codigo,Titulo,Director,AutorPrincipal,No_Actores,Duracion,Estreno FROM TBL_PELICULAS WHERE codigo = @Codigo", con);
            com.Parameters.Add("@Codigo", SqlDbType.Int);
            com.Parameters["@Codigo"].Value = codigo;
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            Pelicula pelicula = new Pelicula();
            if (registros.Read())
            {
                pelicula.Codigo = int.Parse(registros["Codigo"].ToString());
                pelicula.Titulo = registros["Titulo"].ToString();
                pelicula.Director = registros["Director"].ToString();
                pelicula.AutorPrincipal = registros["AutorPrincipal"].ToString();
                pelicula.No_Actores = int.Parse(registros["No_Actores"].ToString());
                pelicula.Duracion = double.Parse(registros["Duracion"].ToString());
                pelicula.Estreno = int.Parse(registros["Estreno"].ToString());
            }
            con.Close();
            return pelicula;
        }

        /// <summary>
        /// Modifica un registro especifico de la DB
        /// </summary>
        /// <param name="peli">modelo de objeto que se pasara como argumento</param>
        /// <returns>numero de registros afectados en DB</returns>
        public int Modificar(Pelicula peli)
        {
            Conectar();
            SqlCommand com = new SqlCommand("UPDATE TBL_PELICULAS " +
                                               "SET Titulo = @Titulo," +
                                                   "Director = @Director," +
                                                   "AutorPrincipal = @AutorPrincipal," +
                                                   "No_Actores = @No_Actores," +
                                                   "Duracion = @Duracion," +
                                                   "Estreno = @Estreno" +
                                             "WHERE Codigo = @Codigo", con);

            com.Parameters.Add("@Codigo", SqlDbType.Int);
            com.Parameters["@Codigo"].Value = peli.Codigo;
            com.Parameters.Add("@Titulo", SqlDbType.VarChar);
            com.Parameters["@Titulo"].Value = peli.Titulo;
            com.Parameters.Add("@Director", SqlDbType.VarChar);
            com.Parameters["@Director"].Value = peli.Director;
            com.Parameters.Add("@AutorPrincipal", SqlDbType.VarChar);
            com.Parameters["@AutorPrincipal"].Value = peli.AutorPrincipal;
            com.Parameters.Add("@No_Actores", SqlDbType.Int);
            com.Parameters["@No_Actores"].Value = peli.No_Actores;
            com.Parameters.Add("@Duracion", SqlDbType.Float);
            com.Parameters["@Duracion"].Value = peli.Duracion;
            com.Parameters.Add("@Estreno", SqlDbType.Int);
            com.Parameters["@Estreno"].Value = peli.Estreno;

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand com = new SqlCommand("DELETE FROM TBL_PELICULAS WHERE Codigo = @Codigo", con);
            com.Parameters.Add("@Codigo", SqlDbType.Int);
            com.Parameters["@Codigo"].Value = codigo;
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}