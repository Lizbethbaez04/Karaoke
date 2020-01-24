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
        public MainWindow()
        {
            InitializeComponent();
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
