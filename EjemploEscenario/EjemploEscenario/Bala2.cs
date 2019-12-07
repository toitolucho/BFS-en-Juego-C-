using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using EjemploEscenario;

namespace JuegoNavesV1
{
    class Bala2 : PictureBox
    {
        delegate void disparador(Form escenario, PictureBox bala, int distancia);


        private Image defaulSkin;
        public Image DefaulSkin
        {
            get { return defaulSkin; }
            set { defaulSkin = value; }
        }


        private Image impactSkin;
        public Image ImpactSkin
        {
            get { return impactSkin; }
            set { impactSkin = value; }
        }
        
        private int desplazamiento;
        public int Desplazamiento
        {
            get { return desplazamiento; }
            set { desplazamiento = value; }
        }

        private int direccion;
        public int Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private Form formulario;
        public Form Formulario
        {
            get { return formulario; }
            set { formulario = value; }
        }
        //es el formulario donde tiene
        //que aparecer la bala

        public Bala2()
        {
            defaulSkin = EjemploEscenario.Properties.Resources.bala;
            //impactSkin = JuegoNavesV1.Properties.Resources.balaImpacto;
            Image = DefaulSkin;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            desplazamiento = 10;
            direccion = 1;
            this.Size = new Size(20,20);
        }
        /// <summary>
        /// Constructor para crear una bala independiente
        /// </summary>
        /// <param name="alto">Alto en pixeles</param>
        /// <param name="ancho">ancho en pixeles</param>
        /// <param name="dir">direccion  1 Vertical, 2 Horizontal</param>
        /// <param name="dx"> la variacion del desplazamiento</param>
        /// <param name="form">en que formulario tiene que aparecer</param>
        /// <param name="skin1">Skin por defecto</param>
        /// <param name="skin2">skin de impacto</param>
        public Bala2(int alto, int ancho, int dir, int dx, Form form,
            Image skin1, Image skin2)
        {
            this.Size = new Size(alto, ancho);
            direccion = dir;
            desplazamiento = dx;
            defaulSkin = skin1;
            impactSkin = skin2;
            this.Image = defaulSkin;
            this.formulario = form;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //bala1.animar(10);
        public void animar(int nroVeces)
        {
            for (int i = 0; i < nroVeces; i++)
            {
                //llamar la funcion que se encarga des desplazar la bala
                //mover();
                if(formulario.InvokeRequired)
                    formulario.Invoke(new disparador(mover), formulario, this, desplazamiento);
                //pausar independientemente el movimiento
                Thread.Sleep(100);
            }
        }

        public void posicionarBala(Personaje per)
        {
            if (direccion == 1)
            {
                this.Top = per.Top - 5;
                this.Left = per.Left + 5;
            }
            if (direccion == 2)
            {
                this.Top = per.Top - 5;
                this.Left = per.Left + per.Width + 5;
            }
            if (direccion == 3)
            {
                this.Top = per.Top + per.Height + 5;
                this.Left = per.Left + 5;
            }
            if (direccion == 4)
            {
                this.Top = per.Top + 5;
                this.Left = per.Left - 5;
            }
        }

        /// <summary>
        /// Funcion generica para desplazar la bala
        /// </summary>
        /// <param name="escenario">En que formulario?</param>
        /// <param name="bala">que Bala?</param>
        /// <param name="movimiento">Cuanto se  mueve</param>
        public void mover(Form escenario, PictureBox bala, int distancia)
        {
            
            //if(direccion == 1)
            //    bala.Top = bala.Top - distancia;            


            if (direccion == 1)//arriba
            {
                bala.Top = bala.Top - distancia;
            }

            if (direccion == 2)//derecha
            {
                bala.Left = bala.Left + distancia;
            }

            if (direccion == 3)//abajo
            {
                bala.Top = bala.Top + distancia;
            }

            if (direccion == 4)//izquierda
            {
                bala.Left = bala.Left - distancia;
            }

            


            
            foreach (Control nave in escenario.Controls)
            {
                if (nave.GetType() == typeof(Personaje))
                {
                    if (nave.Bounds.IntersectsWith(bala.Bounds))
                    {
                        nave.Hide();
                        escenario.Controls.Remove(nave);
                        escenario.Controls.Remove(bala);
                    }

                }
            }
            if (bala.Visible ==true  && bala.Top < 15)
            {
                escenario.Controls.Remove(bala);
            }
        }
    }
}
