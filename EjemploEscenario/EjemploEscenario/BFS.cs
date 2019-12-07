using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace EjemploEscenario
{
    public class Nodo
    {
        public int x;
        public int y;
        public bool visitado;
        public int peso_distancia;
        public char tipo;
        public Nodo nodo_anterior;

        /// <summary>
        /// Se proporciona el tipo de nodo
        /// </summary>
        /// <param name="tipo"></param>        
        /// <param name="x"> representa columna</param>
        /// /// <param name="y"> representa la fila </param>
        public Nodo(char tipo, int x, int y)
        {
            visitado = false;
            peso_distancia = 0;
            this.tipo = tipo;
            this.x = x;
            this.y = y;
        }
        public Nodo(char tipo, Personaje personaje, int escala)
        {
            visitado = false;
            peso_distancia = 0;
            this.tipo = tipo;
            this.y = (personaje.Top / escala) + 1;
            this.x = (personaje.Left / escala) + 1;
        }
        public Nodo()
        {
            visitado = false;
            peso_distancia = 0;            
        }

        public override string ToString()
        {
            return tipo+ " | F=" + y+", C="+x;
        }

        public bool mismaPosicion(Nodo otro)
        {
            return x == otro.x && y == otro.y;
        }
    }


    public class BFS
    {
        public static char[,] esceneario;
        static Nodo[,] matriz;
        Queue<Nodo> cola;
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = {-1, 0, 1, 0 };
        Nodo inicio;
        Nodo objetivo;
        public Stack<Nodo> rutaNodos;

        public BFS(char[,] esc, Nodo ini, Nodo obj)
        {
            esceneario = esc;
            this.inicio = ini;
            this.objetivo = obj;

            cola = new Queue<Nodo>();
            rutaNodos = new Stack<Nodo>();
        }

        public static void iniciar()
        {
            if (esceneario != null && matriz == null)
            {
                matriz = new Nodo[esceneario.GetLength(0)+2, esceneario.GetLength(1)+2];
            }
            for (int i = 1; i <= esceneario.GetLength(0); i++)
            {
                for (int j = 1; j <= esceneario.GetLength(1); j++)
                {
                    matriz[i, j] = new Nodo();
                    matriz[i, j].tipo = esceneario[i-1, j-1];
                    matriz[i, j].peso_distancia = 0;
                    matriz[i, j].visitado = false;
                    matriz[i, j].nodo_anterior = null;
                    matriz[i, j].y = i;
                    matriz[i, j].x = j;


                    //if(esceneario[i-1,j-1] == inicio.tipo)
                    //    inicio = matriz[i,j];
                    //if(esceneario[i-1,j-1] == objetivo.tipo)
                    //    objetivo = matriz[i,j];

                }
            }
        }


        public void resetear()
        {
            for (int i = 1; i < matriz.GetLength(0)-1; i++)
            {
                for (int j = 1; j < matriz.GetLength(1)-1; j++)
                {                    
                    matriz[i, j].peso_distancia = 0;
                    matriz[i, j].visitado = false;
                    matriz[i, j].nodo_anterior = null;
                    rutaNodos.Clear();
                }
            }
        }

        public override string ToString()
        {
            StringBuilder dato = new StringBuilder();
            for (int i = 1; i < matriz.GetLength(0)-1; i++)
            {
                for (int j = 1; j < matriz.GetLength(1)-1; j++)
                {
                    dato.Append(matriz[i,j].tipo );
                }
                dato.Append("\n" );
            }
           return dato.ToString();
        }

        public string obtenerMatrizDistancia()
        {
            StringBuilder dato = new StringBuilder();
            for (int i = 1; i < matriz.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matriz.GetLength(1) - 1; j++)
                {
                    if (i == inicio.y && j == inicio.x)
                    {
                        dato.Append("XZ ");
                    }
                    else if (i == inicio.y && j == inicio.x)
                    {
                        dato.Append("S ");
                    }
                    else if (matriz[i, j].tipo == 'X')
                        dato.Append( matriz[i, j].peso_distancia.ToString().PadLeft(2, '0') + " ");
                    else
                        dato.Append("__ ");
                }
                dato.Append("\n");
            }
            return dato.ToString();
        }

        public string obtenerMatrizExplorada()
        {

            StringBuilder dato = new StringBuilder();
            for (int i = 1; i < matriz.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matriz.GetLength(1) - 1; j++)
                {
                    dato.Append(matriz[i, j].visitado? "1" : "0");
                }
                dato.Append("\n");
            }
            return dato.ToString();
        }

        public string obtenerListaRutaCadena()
        {
            StringBuilder dato = new StringBuilder();
            foreach (Nodo nodo in rutaNodos)
            {
                dato.Append("\n" + nodo);
            }
            return dato.ToString();
        }

        public void trazarRuta()
        {
            rutaNodos.Clear();
            Nodo nodo_explorador = objetivo;
            while (nodo_explorador != null)
            {
                rutaNodos.Push(nodo_explorador);
                nodo_explorador = nodo_explorador.nodo_anterior;
            }
            //rutaNodos.Pop();
        }

        

        public void actualizarObjetivo(int x, int y)
        {
            objetivo.x = x;
            objetivo.y = y;
        }

        public void actualizarPuntoPartida(int x, int y)
        {
            inicio.x = x;
            inicio.y = y;
        }

        public void actualizarObjetivo(Personaje personaje, int escala)
        {
            //objetivo.x = x;
            //objetivo.y = y;

            objetivo.y = (personaje.Top / escala) + 1;
            objetivo.x = (personaje.Left / escala) + 1;
        }

        public void actualizarPuntoPartida(Personaje personaje, int escala)
        {
            inicio.y = (personaje.Top / escala) + 1;
            inicio.x = (personaje.Left / escala) + 1;
        }


        public bool sePuedeRecorrer()
        {
            return rutaNodos.Count > 0;
        }
        public Nodo obtenerSiguienteNodo()
        {
            if (sePuedeRecorrer())
                return rutaNodos.Pop();
            else
                return null;
        }


        public void explorar()
        {
            resetear();
            inicio.peso_distancia = 0;
            inicio.visitado = true;
            cola.Enqueue(inicio);
           // matriz[inicio.y, inicio.x].peso_distancia = 0;
            matriz[inicio.y, inicio.x].peso_distancia = 0;
            while(cola.Count!=0)//mientras la cola tenga elementos
            {
                //Nave nv1;
                Nodo v;
                //sacar un elemento de la cola
                v = cola.Dequeue();
                for (int i = 0; i < 4; i++ )
                {
                    int x1, y1;
                    x1 = v.x + dx[i];
                    y1 = v.y + dy[i];
                    Nodo z;

                    if (x1 > 0 && y1 > 0 && x1 < matriz.GetLength(0) - 1 && x1 < matriz.GetLength(1))
                    {
                        z = matriz[y1, x1];
                        if (z != null)
                        {
                            if (z.tipo == 'X' && !z.visitado)
                            {
                                z.visitado = true;
                                z.peso_distancia = v.peso_distancia + 1;                                
                                z.nodo_anterior = v;
                                cola.Enqueue(z);
                            }
                            //if (i == inicio.x && j == inicio.y)
                            if (z.mismaPosicion(objetivo))
                            {
                                objetivo.nodo_anterior = v;
                                cola.Clear();
                                return;
                            }
                        }
                        
                    }
                    

                }
            }

        }

    }
}
