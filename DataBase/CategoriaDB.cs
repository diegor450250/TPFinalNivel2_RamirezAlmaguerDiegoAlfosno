using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class CategoriaDB
    {
        public List<Categoria> listaCategoria()
        {
            AccesoDB accesoDB = new AccesoDB();
            List<Categoria> lista = new List<Categoria>();
            try
            {
                accesoDB.SetConsulta("Select Id, Descripcion From CATEGORIAS");
                accesoDB.Lectura();
                while (accesoDB.Reader.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)accesoDB.Reader["Id"];
                    categoria.Descripcion = (string)accesoDB.Reader["Descripcion"];
                    lista.Add(categoria);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }

    }
}
