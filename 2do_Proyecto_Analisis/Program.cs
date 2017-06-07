using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2do_Proyecto_Analisis
{
    class Program
    {
        static Datos datos;
        static void Main(string[] args)
        {
            datos = new Datos();
            imprimirPoblacion_Bloques(Datos.listaHorariosHijos);
            int fitness = 18;
            while (fitness !=0)
            {
                PMX.nuevaGeneracion();
                for (int i = 0; i < Datos.listaHorariosHijos.Count; i++)
                {
                    fitness = Datos.listaHorariosHijos[i].fitness() < fitness ? Datos.listaHorariosHijos[i].fitness() : fitness;
                }
            }
            imprimirPoblacion_Bloques(Datos.listaHorariosHijos);
            ReadKey();
        }
        static void imprimirPoblacion_Aulas( List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i+1)+"\n");
                poblacion[i].imprimir_Aulas();
            }
            ReadKey();
        }
        static void imprimirPoblacion_Bloques(List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i + 1) + "\n");
                poblacion[i].imprimir_Bloques();
            }
            ReadKey();
        }
        static void imprimirPoblacion_Profesor(List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i + 1) + "\n");
                poblacion[i].imprimir_Profesor();
            }
            ReadKey();
        }
        static void imprimirPoblacion_Todo(List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i + 1) + "\n");

                poblacion[i].imprimir_Aulas();
                poblacion[i].imprimir_Bloques();
                poblacion[i].imprimir_Profesor();
                ReadKey();
            }
            ReadKey();
        }
    }
}
