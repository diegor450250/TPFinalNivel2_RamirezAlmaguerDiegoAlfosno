using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class MarcaDB
    {
        public List<Marca> listaMarca()
        {
            List<Marca> marcaList = new List<Marca>();
            AccesoDB accesoDB = new AccesoDB();
            try
            {
                accesoDB.SetConsulta("Select Id, Descripcion From MARCAS");
                accesoDB.Lectura();
                while (accesoDB.Reader.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)accesoDB.Reader["Id"];
                    aux.Descripcion = (string)accesoDB.Reader["Descripcion"];
                    marcaList.Add(aux); 
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return marcaList;
        }
    }
}
