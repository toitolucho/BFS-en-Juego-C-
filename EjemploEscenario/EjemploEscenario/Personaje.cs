using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using JuegoNavesV1;
using System.Threading;

namespace EjemploEscenario
{
     public class Personaje : PictureBox
    {

        private int nivelVida;
        private int nrobalas;
        private Image[] skins;
        private int nroSkinActual;
        private Bala2 balita;
        private int sentido;
            
        public Personaje()
        {
            nivelVida = 10;
            nrobalas = 1000000;
            skins = new Image[5];
            nroSkinActual = 1;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            
        }

          public Personaje(int escala)
        {
            nivelVida = 10;
            nrobalas = 1000000;
            skins = new Image[5];
            nroSkinActual = 1;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Size = new Size(escala - 2, escala - 2);
        }

        public Personaje(int nv, int nb)
        {
            nivelVida = nv;
            nrobalas = nb;
            skins = new Image[5];
            nroSkinActual = 1;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }

        public void setSkins(ImageList lista)
        {
            for(int i = 1; i<lista.Images.Count; i++)
            {
                skins[i] = lista.Images[i];
            }
        }

        public void mandarPosicion(int x, int y, int sentido)
        {
            this.Left = x;
            this.Top = y;
            this.Image = skins[sentido];
        }

        /// <summary>
        /// permitira mover la nave
        /// </summary>
        /// <param name="nroPasos">la cantidad de pasos que se mueve</param>
        /// <param name="sentido"> 1.-Arriba  , 2 -> Abajo, 3->Derecha , 4->izquierda</param>
        public void mover(int nroPasos, int sentido)
        {
            //movimiento hacia arriba
            if (sentido == 1)
            {
                this.Top = this.Top - nroPasos;
            }
            if (sentido == 3)
            {
                this.Top = this.Top + nroPasos;
            }
            if (sentido == 2)
            {
                this.Left = this.Left + nroPasos;
            }
            if (sentido == 4)
            {
                this.Left = this.Left - nroPasos;
            }
            this.Image = skins[sentido];
            this.sentido = sentido;
        }
        
        public void prepararDisparo(Form form)
        {
            balita = new Bala2(20,20,1, 20, form, 
                EjemploEscenario.Properties.Resources.bala, null);

            balita.Direccion = sentido;

            //balita.Left = this.Left + 5;
            //balita.Top = this.Top - 20;
            balita.posicionarBala(this);
            
            form.Controls.Add(balita);
            Thread hilo = new Thread(new ThreadStart(disparar2));
            hilo.Start();

        }

        public void disparar2()
        {
            nrobalas = nrobalas - 1;
            balita.animar(20);

            //balita.mover_n_Veces(15);
        }
        public bool estaMuerto()
        {
            return (nivelVida == 0);
        }

        public void setSkin(Image skin, int pos)
        {
            skins[pos] = skin;
        }

        public void seleccionarSkin(int pos)
        {
            this.Image = skins[pos];
            Invalidate();
        }

        public void seleccionarSkinRotado(int pos)
        {
            Bitmap bitmap1 = (Bitmap) skins[pos];
            bitmap1.RotateFlip(RotateFlipType.Rotate180FlipY);
            this.Image = bitmap1;
            Invalidate();
        }
        

        //public void disparar(Form formulario, int direccion)
        //{
        //    if (direccion == 1)
        //    {
        //        balita = new Bala(formulario, Left + 10, Top - 15, 10);
        //    }
        //    else
        //    {
        //        balita = new Bala(formulario, Left + 10, Top + 15, -10);
        //    }
        //    Thread Proceso1 = new Thread(new ThreadStart(disparar2));
        //    Proceso1.Start();
        //}
    }
}
