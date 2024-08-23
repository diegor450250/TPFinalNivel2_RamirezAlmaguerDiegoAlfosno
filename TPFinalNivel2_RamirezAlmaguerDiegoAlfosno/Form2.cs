using DataBase;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPFinalNivel2_RamirezAlmaguerDiegoAlfosno
{
    public partial class Form2 : Form
    {
        Articulo Articulo = null;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Articulo articulo)
        {
            InitializeComponent();
            Articulo = articulo;
            Text = "Editar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbImagen_Leave(object sender, EventArgs e)
        {
            try
            {
                pbAgregar.Load(tbImagen.Text);
            }
            catch (Exception)
            {

                pbAgregar.Load("https://th.bing.com/th/id/R.1e958c3a912bbf00ee3ef3834cb23bfc?rik=650X2T0pwzXJvA&pid=ImgRaw&r=0");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            AccesoDB accesoDB = new AccesoDB();
            bool valido = true;
            try
            {
                if (Articulo == null)
                {
                    Articulo = new Articulo();  
                }
                if (tbCodigo.TextLength == 0)
                {
                    lblCodigo.ForeColor = Color.Red;
                    valido = false;
                }else
                {
                   Articulo.Codigo = tbCodigo.Text.ToUpper(); 
                   lblCodigo.ForeColor= Color.Black;
                }
                if (tbNombre.TextLength == 0)
                {
                    lblNombre.ForeColor = Color.Red;
                    valido = false;
                }else
                {
                    Articulo.Nombre = tbNombre.Text;
                    lblNombre .ForeColor= Color.Black;
                }
                if (tbImagen.TextLength == 0)
                {
                    lblImagen.ForeColor = Color.Red;
                    valido = false;
                }else
                {
                    Articulo.UrlImagen = tbImagen.Text;
                    lblImagen .ForeColor= Color.Black;
                }
                if (tbPrecio.TextLength == 0)
                {
                    lblPrecio.ForeColor = Color.Red;
                    valido = false;
                }else
                {
                    Articulo.Precio = decimal.Parse(tbPrecio.Text);
                    lblPrecio.ForeColor= Color.Black;
                }
                if (cbMarca.SelectedIndex == -1)
                {
                    lblMarca.ForeColor = Color.Red;
                    valido = false;
                }else
                {
                    Articulo.Marca = (Marca)cbMarca.SelectedItem;
                    lblMarca.ForeColor= Color.Black;
                }
                if (cbCategoria.SelectedIndex == -1)
                {
                    lblCategoria.ForeColor = Color.Red;
                    valido = false;
                }else
                {
                    Articulo.Categoria = (Categoria)cbCategoria.SelectedItem;
                    lblCategoria.ForeColor= Color.Black;
                }
                if (tbDescripcion.TextLength == 0)
                {
                    lblDescripcion .ForeColor = Color.Red;
                    valido = false;
                }else
                {
                    Articulo.Descripcion = tbDescripcion.Text;
                    lblDescripcion.ForeColor= Color.Black;
                }
                if (valido)
                {
                    ArticuloDB articuloDB = new ArticuloDB();
                    if (Articulo.Id == 0)
                    {
                        articuloDB.Add(Articulo);
                        MessageBox.Show("Se ha agregado correctamente");
                        Limpiar();
                    }else
                    {
                        articuloDB.Edit(Articulo);
                        MessageBox.Show("Se ha editado correctamente");
                        Close();
                    }
                }else
                {
                    MessageBox.Show("Agregue los datos completos y vuelva a intenar");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void Limpiar()
        {
            tbCodigo.Text = "";
            tbNombre.Text = "";
            tbImagen.Text = "";
            tbPrecio.Text = "";
            tbDescripcion.Text = "";
            cbCategoria.SelectedIndex = -1;
            cbMarca.SelectedIndex = -1;
        }

        private void tbPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, signos de puntuación y teclas de control (como retroceso)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MarcaDB marcaDB = new MarcaDB();
            cbMarca.DataSource = marcaDB.listaMarca();
            cbMarca.ValueMember = "Id";
            cbMarca.DisplayMember = "Descripcion";
            cbMarca.SelectedIndex = -1;
            CategoriaDB categoriaDB = new CategoriaDB();
            cbCategoria.DataSource = categoriaDB.listaCategoria();
            cbCategoria.ValueMember = "Id";
            cbCategoria.DisplayMember = "Descripcion";
            cbCategoria.SelectedIndex = -1;
            if (Articulo != null)
            {
                tbCodigo.Text = Articulo.Codigo;
                tbNombre.Text = Articulo.Nombre;
                tbImagen.Text = Articulo.UrlImagen;
                tbPrecio.Text = Articulo.Precio.ToString();
                tbDescripcion.Text = Articulo.Descripcion;
                cbCategoria.SelectedValue = Articulo.Categoria.Id;
                cbMarca.SelectedValue = Articulo.Marca.Id;
                try
                {
                    pbAgregar.Load(Articulo.UrlImagen);
                }
                catch (Exception)
                {

                    pbAgregar.Load("https://th.bing.com/th/id/R.1e958c3a912bbf00ee3ef3834cb23bfc?rik=650X2T0pwzXJvA&pid=ImgRaw&r=0");
                }    
            }
        }
    }
}
