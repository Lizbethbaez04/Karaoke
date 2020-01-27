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
            {0,3,5,15,

            };
            var cambioLetra = new List<String>()
            {
                "Hice todo lo que dijeron que un chico no podía hacer","Porque llegué a Ciudad Almeja",
                "vencí a los cícloples", "viajé con Hassel Hove",
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
                    
            reader = new AudioFileReader(@"C:\Users\alumno\Downloads\bob-esponja.mp3");
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
