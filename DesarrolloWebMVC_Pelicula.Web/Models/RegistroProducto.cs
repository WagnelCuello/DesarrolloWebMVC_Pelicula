using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DesarrolloWebMVC_Pelicula.Web.Models
{
    public class RegistroProducto
    {
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
            con = new SqlConnection(constr);
        }

        public int GrabarProducto(Producto pro)
        {
            try
            {
                Conectar();
                SqlCommand comando = new SqlCommand("INSERT INTO TBL_PRODUCTOS (Descripcion,Precio) VALUES(@Descripcion,@Precio)", con);

                comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
                comando.Parameters.Add("@Precio", SqlDbType.VarChar);

                comando.Parameters["@Descripcion"].Value = pro.Descripcion;
                comando.Parameters["@Precio"].Value = pro.Precio;
                
                con.Open();
                int i = comando.ExecuteNonQuery();
                con.Close();
                return i;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Producto> RecuperarTodos()
        {
            Conectar();
            List<Producto> productos = new List<Producto>();

            try
            {
                SqlCommand com = new SqlCommand("SELECT Id,Descripcion,Precio FROM TBL_PRODUCTOS", con);
                con.Open();
                SqlDataReader registros = com.ExecuteReader();

                while (registros.Read())
                {
                    Producto pro = new Producto()
                    {
                        Codigo = int.Parse(registros["Id"].ToString()),
                        Descripcion = registros["Descripcion"].ToString(),
                        Precio = float.Parse(registros["Precio"].ToString())
                    };
                    productos.Add(pro);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            con.Close();
            return productos;
        }

        public Producto Recuperar(int codigo)
        {
            Conectar();
            SqlCommand com = new SqlCommand("SELECT Id,Descripcion,Precio FROM TBL_PRODUCTOS WHERE Id = @Codigo", con);
            com.Parameters.Add("@Codigo", SqlDbType.Int);
            com.Parameters["@Codigo"].Value = codigo;
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            Producto producto = new Producto();
            if (registros.Read())
            {
                producto.Codigo = int.Parse(registros["Id"].ToString());
                producto.Descripcion = registros["Descripcion"].ToString();
                producto.Precio = float.Parse(registros["Precio"].ToString());
            }
            con.Close();
            return producto;
        }

        public int Modificar(Producto pro)
        {
            Conectar();
            SqlCommand com = new SqlCommand("UPDATE TBL_PRODUCTOS " +
                                               "SET Descripcion = @Descripcion, " +
                                                   "Precio = @Precio " +
                                             "WHERE Id = @Codigo", con);

            com.Parameters.Add("@Codigo", SqlDbType.Int);
            com.Parameters["@Codigo"].Value = pro.Codigo;

            com.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            com.Parameters["@Descripcion"].Value = pro.Descripcion;

            com.Parameters.Add("@Precio", SqlDbType.VarChar);
            com.Parameters["@Precio"].Value = pro.Precio;
            
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand com = new SqlCommand("DELETE FROM TBL_PRODUCTOS WHERE Id = @Codigo", con);
            com.Parameters.Add("@Codigo", SqlDbType.Int);
            com.Parameters["@Codigo"].Value = codigo;
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}