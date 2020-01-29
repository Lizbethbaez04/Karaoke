using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Forms;

namespace Asignacion1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        WaveOut output;
        AudioFileReader reader;        
        bool dragging = false;
        int actual;
        public MainWindow()
        {
            InitializeComponent();
            actual = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!dragging)
            {
                progressBar.Value = reader.CurrentTime.TotalSeconds;
            }
            var segundosActuales = reader.CurrentTime.TotalSeconds;
            var segundosTotales = reader.TotalTime.TotalSeconds;
            var cambioTiempo = new List<int>()
            {23, 28, 33, 37, 41, 45, 50, 54, 59, 63, 67, 71, 76, 79, 81, 85, 87, 90, 111, 114, 119,
             123, 128, 132, 136, 141, 146, 148, 150, 155, 157, 159, 163, 165, 167, 171, 174, 176, 177,
             198, 201               
            };
            var cambioLetra = new List<String>()
            {//29
                "Tengo que confesar que a veces","No me gusta tu forma de ser",
                "Luego te me desapareces", "Y no entiendo muy bien por qué", "No dices nada romatico",
                "Cuando llega el atardecer", "Te pones de un humor extraño", "Con cada luna llena al mes",
                "Pero todo lo demás", "Le gana lo bueno que me das", "Solo tenerte cerca",
                "Siento que vuelvo a empezar", "Yo te quiero \nCon limón y sal",
                "Yo te quiero \nTal y como estás", "No hace falta \nCambiarte nada",
                "Yo te quiero si vienes o si vas", "Si subes, si bajas, si no estás",
                "Seguro \nDe lo que sientes", "Tengo que confesarte ahora", "Nunca creí en la felicidad", 
                "A veces \nAlgo se le parece", "Pero es pura casualidad", "Luego me vengo a encontrar",
                "Con tus ojos\nMe dan algo más", "Solo tenerte cerca", "Siento que vuelvo a empezar",
                "Yo te quiero \nCon limón y sal", "Yo te quiero tal y como estás",
                "No hace falta\nCambiarte nada", "Yo te quiero si vienes o si vas",
                "Si subes, si bajas, si no estás", "Seguro\nDe lo que sientes",
                "Yo te quiero con limón y sal", "Yo te quiero tal y como estás",
                "No hace falta\nCambiarte nada", "Yo te quiero si vienes o si vas",
                "si subes, si bajas, si no estás", "Seguro\nDe lo que sientes", "Solo tenerte cerca",
                "Siento que vuelvo a empezar"
            };

            var cambioTiempo2 = cambioTiempo[actual];

            if (segundosActuales >= cambioTiempo2)
            {
                var textoCambioSiguiente = cambioLetra[actual];
                txtKaraoke.Text = textoCambioSiguiente;

                if (cambioTiempo.Count > actual + 1)
                {
                    actual++;
                }
            }
        }

        private void BtnReproducir_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;            
            txtKaraoke.Visibility = Visibility.Visible;
            btnReproducir.Visibility = Visibility.Collapsed;        
                    
            reader = new AudioFileReader(@"C:\Users\lizba\Downloads\Julieta Venegas - Limon Y Sal (Video Oficial).mp3");
            output = new WaveOut();
            output.Init(reader);
            output.Play();

            btnReproducir.IsEnabled = false;

            progressBar.Maximum = reader.TotalTime.TotalSeconds;
            progressBar.Value = reader.CurrentTime.TotalSeconds;
            timer.Start();
           
        }

        private void ProgressBar_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            dragging = true;
        }

        private void ProgressBar_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            dragging = false;
            if (reader != null && output != null && output.PlaybackState != PlaybackState.Stopped)
            {
                reader.CurrentTime = TimeSpan.FromSeconds(progressBar.Value);
            }
        }
    }
}
