using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjemploEscenario
{
    public static class Reader
    {
        public static char[,] LeerMapa(string nombre)
        {
            char[,] matriz;
            string[] lineas;
            lineas = System.IO.File.ReadAllLines(nombre);            
            int filas, columnas;
            filas =int.Parse( lineas[0]);
            columnas = int.Parse(lineas[1]);
            matriz = new char[filas, columnas];
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matriz[i, j] = lineas[i + 2][j];
                }
            }
            return matriz;
        }


        public static int LeerLargo(string nombre)
        {
            
            string[] lineas;
            lineas = System.IO.File.ReadAllLines(nombre);
            int filas;
            filas = int.Parse(lineas[0]);
            
            return filas;
        }

        public static int LeerAncho(string nombre)
        {

            string[] lineas;
            lineas = System.IO.File.ReadAllLines(nombre);
            int columnas;
            columnas = int.Parse(lineas[1]);

            return columnas;
        }
    }
}
