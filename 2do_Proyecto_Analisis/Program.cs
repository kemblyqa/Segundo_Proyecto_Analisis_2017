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
        static Datos datos = new Datos();
        static BackTracking back;
        static int generaciones;
        static void Main(string[] args)
        {
            PMX.solucionPMX();
            CycleX.solucionCycleX();
            //WriteLine(Datos.listaAulas[1].horavalida(6));
            //imprimirPoblacion_Bloques(Datos.listaHorariosHijos);
            //int fitness = -1;
            //generaciones = 0;
            //Console.Clear();
            //while (fitness !=0)
            //{
            //    PMX.nuevaGeneracion();
            //    generaciones++;
            //    for (int i = 0; i < Datos.listaHorariosHijos.Count; i++)
            //    {
            //        System.Console.SetCursorPosition(0, 0);
            //        WriteLine("Promedio Fitness:" + Datos.listaHorariosHijos[i].fitness().ToString() + "\t\nMejor Fitness\t:" + fitness.ToString() + "\t\nGeneraciones\t:" + generaciones.ToString());
            //        fitness = Datos.listaHorariosHijos[i].fitness() < fitness || fitness==-1 ? Datos.listaHorariosHijos[i].fitness() : fitness;
            //        if (fitness == 0)
            //        {
            //            Datos.listaHorariosHijos[i].imprimir_Bloques();
            //            break;
            //        }
            //    }
            ////}
            //back = new BackTracking();
            //back.backTracking();
            //ReadKey();
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
