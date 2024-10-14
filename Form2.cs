using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace PlayerUI
{
    public partial class Form2 : Form
    {
        public string archivoLista = "lista_archivos.txt";
        private string[] ArchivosMusica;
        private string[] LocalizacionArchivos;
        public List<string> datoslistbox
        {
            get
            {
                List<string> datos = new List<string>();
                foreach (var item in listBox1.Items)
                {
                    string dato = item.ToString();
                    datos.Add(dato);
                }
                return datos;
            }
        }

        public Form2()
        {
            InitializeComponent();
            RecuperarListaArchivos();
        }

        //metodo para recuperar la lista de canciones guardados en el .txt
        private void RecuperarListaArchivos()
        {
            if (File.Exists(archivoLista))
            {
                using (StreamReader fichero = new StreamReader(archivoLista))
                {
                    while (fichero.Peek() > -1)
                    {
                        string linea = fichero.ReadLine();
                        listBox1.Items.Add(Path.GetFileName(linea));
                        Array.Resize(ref ArchivosMusica, listBox1.Items.Count);
                        Array.Resize(ref LocalizacionArchivos, listBox1.Items.Count);
                        ArchivosMusica[listBox1.Items.Count - 1] = Path.GetFileName(linea);
                        LocalizacionArchivos[listBox1.Items.Count - 1] = linea;
                    }
                }
            }
        }
        //fin

        //Funcion para guardae las canciones en el .txt
        private void GuardarListaArchivos()
        {
            using (StreamWriter fichero = new StreamWriter(archivoLista))
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    fichero.WriteLine(LocalizacionArchivos[i]);
                }
            }
        }
        //Fin

        //boton para cerrar el formulario 2 (a su vez si se le da a este tambien se guarda la lista de canciones en el archivo .txt)
        private void button5_Click(object sender, EventArgs e)
        {
            //GuardarListaArchivos();
            this.Close();
        }
        //Fin

        //Boton para abrir una ventana de dialogo y seleccionar la cantidad de archivos de musica que desee el usuario
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog EncontrarArchivo = new OpenFileDialog();
            EncontrarArchivo.Multiselect = true;
            if (EncontrarArchivo.ShowDialog() == DialogResult.OK)
            {
                foreach (var archivo in EncontrarArchivo.FileNames)
                {
                    listBox1.Items.Add(Path.GetFileName(archivo));
                    Array.Resize(ref ArchivosMusica, listBox1.Items.Count);
                    Array.Resize(ref LocalizacionArchivos, listBox1.Items.Count);
                    ArchivosMusica[listBox1.Items.Count - 1] = Path.GetFileName(archivo);
                    LocalizacionArchivos[listBox1.Items.Count - 1] = archivo;

                }
            }
        }
        //fin

        //Boton para que se guarde el contenido del listbox en el .txt
        private void button1_Click(object sender, EventArgs e)
        {
            GuardarListaArchivos();
        }
        //fin

        //evento del listbox1 utilizado como prueba para reproduccion de los archivos
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                string archivoSeleccionado = LocalizacionArchivos[listBox1.SelectedIndex];
            }
        }

       
    }
     
}


