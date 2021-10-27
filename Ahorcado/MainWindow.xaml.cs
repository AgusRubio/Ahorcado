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

namespace Ahorcado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Creamos tres variables globales para poder manejar el programa
        string palabra; //Para generar la palabra que vamos a usar y guardarla
        int j = 0, condicionVictoria = 0; //Para guardar la imagen en la que nos encontramos y una variable que suma 1 cada vez que acertamos una letra

        public MainWindow()
        {
            InitializeComponent();
            GenerarPalabra();
            GenerarLetras();
            PintarLineas();
        }

        public void GenerarPalabra()
        {
            Random aleatorio = new Random();

            string[] palabras = new string[] { "GATO", "PERRO", "MONO", "JIRAFA", "VACA", "TORO" };

            palabra = palabras[aleatorio.Next(palabras.Count())];
        }

        public void GenerarLetras()
        {
            //Gereramos el array de letras
            string abecedario = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            char[] letras = abecedario.ToCharArray();

            for (int i = 0; i < 27; i++)
            {

                //Creamos los botones
                Button boton = new Button();
                boton.Margin = new Thickness(3);
                LetrasUniformGrid.Children.Add(boton);
                boton.Tag = letras[i].ToString();

                //Añadimos el evento
                boton.Click += Button_Click;

                //Añadimos un ViewBox y ponemos la letra que es
                TextBlock texto = new TextBlock();
                texto.Text = letras[i].ToString();
                Viewbox box = new Viewbox();
                box.Child = texto;
                boton.Content = box;
            }
        }


        public void PintarLineas()
        {

            for (int i = 0; i < palabra.Length; i++)
            {
                Border borde = new Border();
                TextBlock lineas = new TextBlock();

                borde.BorderBrush = Brushes.Black;
                borde.BorderThickness = new Thickness(0, 0, 0, 2);
                borde.Margin = new Thickness(2);
                borde.Padding = new Thickness(40);

                lineas.FontSize = 50;

                borde.Child = lineas;
                LetrasPalabra.Children.Add(borde);
            }
        }

        public void Victoria()
        {
            MessageBox.Show("Enhorabuena!! Has ganado!!", "Victoria");
            LimpiarGrid();
            GenerarPalabra();
            GenerarLetras();
            PintarLineas();
            j = 0;
            BitmapImage src = new BitmapImage(new Uri("/assets/0.jpg", UriKind.Relative));
            Imagen.Source = src;
        }

        public void Derrota()
        {
            char[] palabraArray = palabra.ToCharArray();

            for (int i = 0; i < palabraArray.Length; i++)
            {
                ((TextBlock)((Border)LetrasPalabra.Children[i]).Child).Text = palabraArray[i].ToString();
            }

            MessageBox.Show("Has perdido!", "Derrota");
            LimpiarGrid();
            GenerarPalabra();
            GenerarLetras();
            PintarLineas();
            j = 0;
            BitmapImage src = new BitmapImage(new Uri("/assets/0.jpg", UriKind.Relative));
            Imagen.Source = src;
        }

        public void LimpiarGrid()
        {
            LetrasPalabra.Children.Clear();
            LetrasUniformGrid.Children.Clear();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            char[] palabraArray = palabra.ToCharArray();
            Button boton = (Button)sender;
            string letra = boton.Tag.ToString();
            boton.IsEnabled = false;
            bool acierto = false;

            for (int i = 0; i < palabraArray.Length; i++)
            {
                if (palabraArray[i] == Convert.ToChar(letra))
                {
                    ((TextBlock)((Border)LetrasPalabra.Children[i]).Child).Text = letra;
                    acierto = true;
                    condicionVictoria++;
                }
            }

            if(condicionVictoria == palabra.Length)
            {
                Victoria();
            }

            if (!acierto)
            {
                j++;
                BitmapImage src = new BitmapImage(new Uri("/assets/" + j + ".jpg", UriKind.Relative));
                Imagen.Source = src;
                if(j == 10)
                {
                    Derrota();
                }
            }
        }

        private void Button_ClickRendirse(object sender, RoutedEventArgs e)
        {
            char[] palabraArray = palabra.ToCharArray();

            for (int i = 0; i < palabraArray.Length; i++)
            {
                ((TextBlock)((Border)LetrasPalabra.Children[i]).Child).Text = palabraArray[i].ToString();
            }

            MessageBox.Show("Rendirse es de cobardes...", "Derrota");
            LimpiarGrid();
            GenerarPalabra();
            GenerarLetras();
            PintarLineas();
            j = 0;
            BitmapImage src = new BitmapImage(new Uri("/assets/0.jpg", UriKind.Relative));
            Imagen.Source = src;
        }

        private void Button_ClickNewGame(object sender, RoutedEventArgs e)
        {
            char[] palabraArray = palabra.ToCharArray();

            for (int i = 0; i < palabraArray.Length; i++)
            {
                ((TextBlock)((Border)LetrasPalabra.Children[i]).Child).Text = palabraArray[i].ToString();
            }

            LimpiarGrid();
            GenerarPalabra();
            GenerarLetras();
            PintarLineas();
            j = 0;
            BitmapImage src = new BitmapImage(new Uri("/assets/0.jpg", UriKind.Relative));
            Imagen.Source = src;
        }


        private void Key_Up(object sender, KeyEventArgs e)
        {
            char[] palabraArray = palabra.ToCharArray();
            string letra = e.Key.ToString(); //Convertimos lo que nos devuelve la tecla a string.
            bool acierto = false;

            for (int i = 0; i < palabraArray.Length; i++)
            {
                if (palabraArray[i] == Convert.ToChar(letra))
                {
                    ((TextBlock)((Border)LetrasPalabra.Children[i]).Child).Text = letra;
                    acierto = true;
                    condicionVictoria++;
                }
            }

            if (condicionVictoria == palabra.Length)
            {
                Victoria();
            }

            if (!acierto)
            {
                j++;
                BitmapImage src = new BitmapImage(new Uri("/assets/" + j + ".jpg", UriKind.Relative));
                Imagen.Source = src;
                if (j == 10)
                {
                    Derrota();
                }
            }
        }
    }
}
