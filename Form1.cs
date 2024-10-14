using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace PlayerUI
{

    public partial class Form1 : Form
    {
        private Dictionary<string, string> archivomusica = new Dictionary<string, string>();
        bool play;        
        //Metodo para cargar el contenido del archivo guardado en la otra pestaña
        private void añadircontenido()
        {
            string rutaArchivo = @"lista_archivos.txt";
            try
            {
                StreamReader leer = new StreamReader(rutaArchivo);
                string line;
                while ((line = leer.ReadLine()) != null)
                {
                    string musiquita = Path.GetFileName(line);
                    listBox2.Items.Add(musiquita);
                    archivomusica[musiquita] = line;
                }

                leer.Close();
            }
            catch (FileNotFoundException)
            {

            }
        }
        //Fin

        //Metodo para reproducir musica
        private void ReproducirArchivo(string rutaArchivo)
        {
            reproductor.URL = rutaArchivo;
        }
        //fin

        //Evento de la listbox2
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                string nombreArchivo = listBox2.SelectedItem.ToString();
                string rutaarchivo = archivomusica[nombreArchivo];
                ReproducirArchivo(rutaarchivo);
            }
        }
        //fin

        //Boton para reproducir y pausar las cancion actual en reproduccion
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            switch (play)
            {
                case true:
                    reproductor.Ctlcontrols.pause(); 
                    play = false;
                    break;
                case false:
                    reproductor.Ctlcontrols.play();
                    play = true;
                    break;
            }
        }
        //fin

        

        public Form1()
        {
            InitializeComponent();
            añadircontenido();
            hideSubMenu();
        }

        private void hideSubMenu()
        {
            panelMediaSubMenu.Visible = false;
            panelPlaylistSubMenu.Visible = false;
            panelToolsSubMenu.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }

        #region MediaSubMenu
        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
            hideSubMenu();
        }
        #endregion

        private void btnPlaylist_Click(object sender, EventArgs e)
        {
            showSubMenu(panelPlaylistSubMenu);
        }

        #region PlayListManagemetSubMenu
        private void button8_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
       
            hideSubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }
        #endregion

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //..
            //funcion para llamar al instructivo
            //..
            System.Diagnostics.Process.Start("instructivo.docx");
           
            hideSubMenu();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panelPlayer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            añadircontenido();
            espectrodesonido();
            if (listBox2.Visible)
            {
                listBox2.Visible = false;
            }
            else
            {
                listBox2.Visible = true;
            }
           
        }
        private void espectrodesonido()
        {
            if (reproductor.Visible)
            {
                reproductor.Visible = false;
            }
            else
            {
                reproductor.Visible = true;
            }
        }
        private void panelMediaSubMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelToolsSubMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < listBox2.Items.Count - 1)
            {
                listBox2.SelectedIndex += 1;
            }
        }
        //

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > 0)
            {
                listBox2.SelectedIndex -= 1;
            }
        }
        //

        private void timer1_Tick(object sender, EventArgs e)
        {
            ActualizarEstatus();
            StatusCancion.Value = (int)reproductor.Ctlcontrols.currentPosition;
            Barravolumen.Value = reproductor.settings.volume;
            
        }
        
        
        //
        public void ActualizarEstatus()
        {
            if (reproductor.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                StatusCancion.Maximum = (int)reproductor.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }
            else if (reproductor.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }
            if (reproductor.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                double labeltiempoTotal = reproductor.currentMedia.duration;
                double labeltiempoTranscurrido = reproductor.Ctlcontrols.currentPosition;
                string tiempoTotalString = TimeSpan.FromSeconds(labeltiempoTotal).ToString(@"mm\:ss");
                string tiempoTranscurridoString = TimeSpan.FromSeconds(labeltiempoTranscurrido).ToString(@"mm\:ss");

                StatusCancion.Maximum = (int)labeltiempoTotal;
                timer1.Start();

                tiempoTotal.Text = tiempoTotalString;
                tiempoTranscurrido.Text = tiempoTranscurridoString;
            }
            else if (reproductor.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }
        }
        //
        private void Barravolumen_ValueChanged(object sender, decimal value)
        {
            reproductor.settings.volume = Barravolumen.Value;
        }
        //
        private void reproductor_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            ActualizarEstatus();
        }
    
    }
}
