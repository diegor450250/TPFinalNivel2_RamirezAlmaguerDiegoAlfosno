using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Security.Cryptography.X509Certificates;

namespace DataBase
{
    public class ArticuloDB
    {
        public List<Articulo> List()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDB accesoDB = new AccesoDB();
            try
            {
                accesoDB.SetConsulta("Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdCategoria, C.Descripcion Categoria, A.IdMarca, M.Descripcion Marca From ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdCategoria = C.Id and A.IdMarca = M.Id");
                accesoDB.Lectura();
                while (accesoDB.Reader.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)accesoDB.Reader["Id"];
                    aux.Codigo = (string)accesoDB.Reader["Codigo"];
                    aux.Nombre = (string)accesoDB.Reader["Nombre"];
                    aux.Descripcion = (string)accesoDB.Reader["Descripcion"];
                    aux.Precio = (decimal)accesoDB.Reader["Precio"];
                    aux.UrlImagen = (string)accesoDB.Reader["ImagenUrl"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)accesoDB.Reader["IdCategoria"];
                    aux.Categoria.Descripcion = (string)accesoDB.Reader["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)accesoDB.Reader["IdMarca"];
                    aux.Marca.Descripcion = (string)accesoDB.Reader["Marca"];
                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }

        public void Add(Articulo articulo)
        {
            AccesoDB accesoDB = new AccesoDB();
            try
            {
                accesoDB.SetConsulta("Insert into ARTICULOS VALUEs ('" + articulo.Codigo + "', '" + articulo.Nombre + "', '" + articulo.Descripcion + "', " + articulo.Marca.Id + ", " + articulo.Categoria.Id + ", '" + articulo.UrlImagen + "', " + articulo.Precio + ") ");
                accesoDB.ExecuteAction();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally
            {
                accesoDB.CloseConexion();
            }
        }

        public void Edit(Articulo articulo)
        {
            AccesoDB accesoDB = new AccesoDB();
            try
            {
                accesoDB.SetConsulta("Update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @marca, IdCategoria = @categoria, ImagenUrl = @imagen, Precio = @precio where id = @id");
                accesoDB.SetParametro("@codigo", articulo.Codigo);
                accesoDB.SetParametro("@nombre", articulo.Nombre);
                accesoDB.SetParametro("@descripcion", articulo.Descripcion);
                accesoDB.SetParametro("@marca", articulo.Marca.Id.ToString());
                accesoDB.SetParametro("@categoria", articulo.Categoria.Id.ToString());
                accesoDB.SetParametro("@imagen", articulo.UrlImagen);
                accesoDB.SetParametro("@precio", articulo.Precio.ToString());
                accesoDB.SetParametro("@id", articulo.Id.ToString());
                accesoDB.ExecuteAction(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }finally
            {
                accesoDB.CloseConexion();
            }
        }

        public void Delete(Articulo articulo)
        {
            AccesoDB accesoDB= new AccesoDB();
            try
            {
                accesoDB.SetConsulta("Delete ARTICULOS where Id = " + articulo.Id);
                accesoDB.ExecuteAction();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally
            {
                accesoDB.CloseConexion();
            }
        }

        public List<Articulo> BusquedaFiltro(string consulta)
        {
            AccesoDB accesoDB = new AccesoDB();
            List<Articulo> lista = new List<Articulo>();
            try
            {
                accesoDB.SetConsulta(consulta);
                accesoDB.Lectura();
                while (accesoDB.Reader.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)accesoDB.Reader["Id"];
                    aux.Codigo = (string)accesoDB.Reader["Codigo"];
                    aux.Nombre = (string)accesoDB.Reader["Nombre"];
                    aux.Descripcion = (string)accesoDB.Reader["Descripcion"];
                    aux.Precio = (decimal)accesoDB.Reader["Precio"];
                    aux.UrlImagen = (string)accesoDB.Reader["ImagenUrl"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)accesoDB.Reader["IdCategoria"];
                    aux.Categoria.Descripcion = (string)accesoDB.Reader["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)accesoDB.Reader["IdMarca"];
                    aux.Marca.Descripcion = (string)accesoDB.Reader["Marca"];
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
