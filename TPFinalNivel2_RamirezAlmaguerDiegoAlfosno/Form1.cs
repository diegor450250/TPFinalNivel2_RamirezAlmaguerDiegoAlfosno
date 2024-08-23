using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBase;
using Dominio;

namespace TPFinalNivel2_RamirezAlmaguerDiegoAlfosno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cargar();
            cbCampo.Items.Add("Nombre");
            cbCampo.Items.Add("Codigo");
            cbCampo.Items.Add("Categoria");
            cbCampo.Items.Add("Marca");
            cbCampo.Items.Add("Precio");
        }

        public void Cargar()
        {
            ArticuloDB articuloDB = new ArticuloDB();
            try
            {
                List<Articulo> lista = articuloDB.List();
                dgw.DataSource = lista;
                dgw.Columns["Id"].Visible = false;
                dgw.Columns["Codigo"].Visible = false;
                dgw.Columns["Descripcion"].Visible = false;
                dgw.Columns["UrlImagen"].Visible = false;
                dgw.Columns["Precio"].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        public void LoadImagen(string imagen)
        {
            try
            {
                pb.Load(imagen);
            }
            catch (Exception ex)
            {

                pb.Load("https://tresubresdobles.com/wp-content/uploads/2019/07/skft-5ed6f087f52ef79a86da813cca5d3f00.jpg");
            }
        }

        private void dgw_SelectionChanged(object sender, EventArgs e)
        {
            if (dgw.CurrentRow != null)
            {
                Articulo articulo = (Articulo)dgw.CurrentRow.DataBoundItem;
                LoadImagen(articulo.UrlImagen);
                lblDescripcion.Text = articulo.Descripcion;
                lblCodigo.Text = articulo.Codigo;   
                lblPrecio.Text = articulo.Precio.ToString("F2");
                lblId.Text = articulo.Id.ToString();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            Cargar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo articulo = (Articulo)dgw.CurrentRow.DataBoundItem;
                Form2 form2 = new Form2(articulo);
                form2.ShowDialog();
                Cargar();
            }
            catch (Exception ex)
            {

                throw ex;
            }    
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo selected = (Articulo)dgw.CurrentRow.DataBoundItem;
                DialogResult validate = MessageBox.Show("Seguro que quieres eliminar el articulo " + selected.Nombre + "?", "Eliminar", MessageBoxButtons.YesNo);
                if (validate == DialogResult.Yes)
                {
                    ArticuloDB db = new ArticuloDB();
                    db.Delete(selected);
                    MessageBox.Show("Se ha eliminado correctamente");
                    Cargar();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string option = cbCampo.SelectedItem.ToString();
            switch (option)
            {
                case "Nombre":
                    cbCriterio.DataSource = null;
                    cbCriterio.Items.Clear();
                    cbCriterio.Items.Add("Inicia con");
                    cbCriterio.Items.Add("Termina con");
                    cbCriterio.Items.Add("Contiene");
                    tbFiltro.Enabled = true;
                    tbFiltro.Text = "";
                    break;

                case "Codigo":
                    cbCriterio.DataSource = null;
                    cbCriterio.Items.Clear();
                    cbCriterio.Items.Add("Inicia con");
                    cbCriterio.Items.Add("Termina con");
                    cbCriterio.Items.Add("Contiene");
                    tbFiltro.Enabled = true;
                    tbFiltro.Text = "";
                    break;

                case "Precio":
                    cbCriterio.DataSource = null;
                    cbCriterio.Items.Clear();
                    cbCriterio.Items.Add("Menor a");
                    cbCriterio.Items.Add("Mayor a");
                    cbCriterio.Items.Add("Igual a");
                    tbFiltro.Enabled = true;
                    tbFiltro.Text = "";
                    break;

                case "Categoria":
                    cbCriterio.DataSource = null;
                    cbCriterio.Items.Clear();
                    CategoriaDB categoriaDB = new CategoriaDB();
                    cbCriterio.DataSource = categoriaDB.listaCategoria();
                    tbFiltro.Enabled = false;
                    tbFiltro.Text = "";
                    break;
                
                default:
                    cbCriterio.DataSource = null;
                    cbCriterio.Items.Clear();
                    MarcaDB marcaDB = new MarcaDB();
                    cbCriterio.DataSource = marcaDB.listaMarca();
                    tbFiltro.Enabled = false;
                    tbFiltro.Text = "";
                    break;
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloDB articuloDB = new ArticuloDB();
            string consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdCategoria, C.Descripcion Categoria, A.IdMarca, M.Descripcion Marca From ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdCategoria = C.Id and A.IdMarca = M.Id and A.";
            string campo = cbCampo.SelectedItem.ToString();
            try
            {
                if (tbFiltro.Enabled == true)
                {
                    consulta += campo;
                    string criterio = cbCriterio.SelectedItem.ToString();
                    string filtro = tbFiltro.Text;
                    switch (criterio)
                    {
                        case "Inicia con":
                            consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdCategoria, C.Descripcion Categoria, A.IdMarca, M.Descripcion Marca From ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdCategoria = C.Id and A.IdMarca = M.Id and A." + campo + " like '" + filtro + "%'";
                            break;

                        case "Termina con":
                            consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdCategoria, C.Descripcion Categoria, A.IdMarca, M.Descripcion Marca From ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdCategoria = C.Id and A.IdMarca = M.Id and A." + campo + " like '%" + filtro + "'";
                            break;

                        case "Contiene":
                            consulta = "Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdCategoria, C.Descripcion Categoria, A.IdMarca, M.Descripcion Marca From ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdCategoria = C.Id and A.IdMarca = M.Id and A." + campo + " like '%" + filtro + "%'";
                            break;

                        case "Menor a":                           
                            consulta += "< " + filtro;
                            break;

                        case "Mayor a":
                                consulta += "> " +  filtro;      
                            break;
                        
                        default:
                                consulta += "= " + filtro;               
                            break;
                    }
                    if (campo == "Precio")
                    {
                        if (ValidarSoloNumeros(filtro))
                        {
                            dgw.DataSource = articuloDB.BusquedaFiltro(consulta);
                        }
                        else
                        {
                            MessageBox.Show("Solo ingrese numeros");
                        }
                    }else
                    {
                        dgw.DataSource = articuloDB.BusquedaFiltro(consulta);
                    }
                }else
                {
                    if (campo == "Categoria")
                    {
                        Categoria aux = (Categoria)cbCriterio.SelectedItem;
                        consulta += "IdCategoria = " + aux.Id;
                    }
                    else
                    {
                        Marca aux = (Marca)cbCriterio.SelectedItem;
                        consulta += "IdMarca = " + aux.Id;
                    }
                    dgw.DataSource = articuloDB.BusquedaFiltro(consulta);
                }
            }
            catch (Exception ex) 
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            tbFiltro.Text = "";
            Cargar();
        }

        private bool ValidarSoloNumeros(string filtro)
        {
            bool isValid = Regex.IsMatch(filtro, @"^[\d\p{P}]+$");
            return isValid;
        }
    }
}
