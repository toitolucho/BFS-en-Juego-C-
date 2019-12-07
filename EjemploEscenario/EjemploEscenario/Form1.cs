using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EjemploEscenario
{
    public partial class Form1 : Form
    {
        //public char [,] esceneario ={
        //                            {'P','P','P','P','P','P','X','P','P','P','P'},
        //                            {'P','P','P','P','P','P','X','X','P','P','P'},
        //                            {'P','P','P','P','P','P','X','X','P','P','P'},
        //                            {'P','P','P','P','P','P','X','X','P','P','P'},
        //                            {'P','P','P','P','P','P','P','X','P','P','P'},
        //                            {'P','X','X','X','X','X','X','X','P','P','P'},
        //                            {'P','X','P','P','P','P','X','X','P','P','P'},
        //                            {'P','X','P','P','P','P','P','P','P','P','P'},
        //                            {'P','X','P','P','P','P','P','P','P','P','P'},
        //                            {'P','S','P','P','P','P','P','P','P','P','P'},
        //                            {'P','X','X','P','P','P','P','P','P','P','P'},
        //                            {'P','X','X','X','X','X','X','X','P','P','P'},
        //                            {'P','X','X','X','P','X','X','X','X','P','P'},
        //                            {'P','P','P','P','P','P','P','P','I','P','P'}
        //                     };

       // public Nodo[] vector =  new Nodo [5];// {new Nodo('X',4,5), new Nodo ('X',4,6)}
        public char[,] esceneario = Reader.LeerMapa("mapa2.txt");
        
        int x_personaje = 2;  //representa a la columna
        int y_personaje = 10; //  representa la fila
        
        int escala = 30;  // representa el tamaño de un assets (de un cuadro) en pixeles
        int direccion = 0;//representa el sentido de movimiento del personaje
        int largo = Reader.LeerLargo("mapa2.txt");
        int ancho = Reader.LeerAncho("mapa2.txt");
        BFS explorador, explorador2;
        Personaje jugador;
        Personaje enemigo1;
        Personaje enemigo2;
        public bool movimiento = false;
        public Form1()
        {
            
            InitializeComponent();
            BFS.esceneario = esceneario;
            BFS.iniciar();

            //vector[0] = new Nodo('X', 5, 6);
            //vector[1] = new Nodo('X', 5, 7);
            //vector[2] = new Nodo('X', 5, 8);
            //vector[3] = new Nodo('X', 6, 8);
            //vector[4] = new Nodo('X', 6, 9);

            jugador = new Personaje(escala);
            jugador.setSkins(imageListPersonaje);
            jugador.mandarPosicion((x_personaje-1) * escala, (y_personaje-1) * escala, 3);
            this.Controls.Add(jugador);

            enemigo1 = new Personaje(escala);
            enemigo1.setSkins(imageListEnemigo);
            enemigo1.mandarPosicion((7 - 1) * escala, (6 - 1) * escala, 1);
            this.Controls.Add(enemigo1);


            enemigo2 = new Personaje(escala);
            enemigo2.setSkins(imageListEnemigo);
            enemigo2.mandarPosicion((9 - 1) * escala, (14 - 1) * escala, 1);
            this.Controls.Add(enemigo2);


            //pictureBox1.Left = (x-1) * escala, (y-1);
            //pictureBox1.Top = (y-1) * escala;
            //pictureBox1.Image = imageList1.Images[0];
            //explorador = new BFS(esceneario, new  Nodo('F',6,7),new Nodo('I', 1,7));
            explorador = new BFS(esceneario, new Nodo('I', enemigo1, escala), new Nodo('F', jugador, escala));
            explorador.explorar();
            explorador.trazarRuta();
            //label4.Text = explorador.obtenerMatrizDistancia();
            //label4.Text = explorador.obtenerMatrizDistancia();
            //label5.Text = explorador.obtenerListaRutaCadena();



            explorador2 = new BFS(esceneario, new Nodo('I', enemigo2, escala), new Nodo('F', jugador, escala));            
            explorador2.explorar();
            explorador2.trazarRuta();


            label4.Text = explorador2.obtenerMatrizDistancia();
            label5.Text = explorador2.obtenerListaRutaCadena();


            timer1.Start();
            timer2.Start();

            this.DoubleBuffered = true;

        }

        private void Form1_Paint(object sender, PaintEventArgs lienzo)
        {
            lienzo.Graphics.DrawImage(Image.FromFile("Escenario2.jpg"), 0, 0, ancho * escala, largo*escala);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            movimiento = false;
            
            if (e.KeyCode == Keys.Up)
            {
               // char c = esceneario[x - 1, y - 2] ;
                if (((y_personaje - 1) == 0) || esceneario[ y_personaje-2,x_personaje-1] == 'P')
                {
                    return;
                }

                direccion = 1;
                y_personaje--;
                jugador.mover(escala, direccion);
                movimiento = true;
               
                
            }
            if (e.KeyCode == Keys.Down)
            {
                if (((y_personaje + 1) == largo + 1) || esceneario[y_personaje , x_personaje - 1] == 'P')
                {
                    return;
                }

                direccion = 3;
                y_personaje++;
                jugador.mover(escala, direccion);
                movimiento = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                if (((x_personaje - 1) == 0) || esceneario[y_personaje-1, x_personaje - 2] == 'P')
                {
                    return;
                }

                direccion = 4;
                x_personaje--;
                jugador.mover(escala, direccion);
                movimiento = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                if (((x_personaje + 1) == ancho+1) || esceneario[y_personaje - 1, x_personaje ] == 'P')
                {
                    return;
                }

                direccion = 2;
                x_personaje++;
                jugador.mover(escala, direccion);
                movimiento = true;
            }


            if (e.KeyCode == Keys.Space)
            {
                jugador.prepararDisparo(this);
            }

           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }
    }
}
