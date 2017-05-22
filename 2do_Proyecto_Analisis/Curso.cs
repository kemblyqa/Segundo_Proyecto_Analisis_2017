using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class Curso
    {
        string nombre;
        int numeroClases, bloque, numeroLecciones;

        public Curso(string nombre, int numeroClases, int bloque, int numeroLecciones)
        {
            this.nombre = nombre;
            this.numeroClases = numeroClases;
            this.bloque = bloque;
            this.numeroLecciones = numeroLecciones;
        }
    }
}
