using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2do_Proyecto_Analisis
{
    static class CycleX
    {
        static int mutaciones,fallos,generaciones;
        public static void insertarCria(Horario nueva)
        {
            if (nueva == null || Datos.listaHorariosHijos.Count == Datos.individuosporgeneracion)
                return;
            int newfit = nueva.fitness();
            for (int i = 0; i < Datos.listaHorariosHijos.Count; i++)
            {
                if (!Datos.distintos(Datos.listaHorariosHijos[i], nueva, 2))
                {
                    return;
                }
                if (newfit < Datos.listaHorariosHijos[i].fitness())
                {
                    Datos.listaHorariosHijos.Insert(i, nueva);
                    return;
                }
            }
            Datos.listaHorariosHijos.Add(nueva);
        }
        public static void solucionCycleX()
        {
            mutaciones = 0;
            fallos = 0;
            generaciones = 0;
            int fitness = -1;
            Datos.listaHorariosHijos = new List<Horario>();
            Datos.listaHorariosPadres = new List<Horario>();
            Datos.poblacionInicial((Datos.cantidadlecciones / 3) * 5);
            generaciones = (Datos.cantidadlecciones/3) * 4;
            Console.Clear();
            for (int j = 0; j < (Datos.cantidadlecciones / 3) * 4 && fitness != 0; j++)
            {
                CycleX.nuevaGeneracion();
                generaciones--;
                int prom = 0;
                for (int i = 0; i < Datos.listaHorariosHijos.Count; i++)
                {
                    prom += Datos.listaHorariosHijos[i].fitness();
                    fitness = Datos.listaHorariosHijos[i].fitness() < fitness || fitness == -1 ? Datos.listaHorariosHijos[i].fitness() : fitness;
                }
                System.Console.SetCursorPosition(0, 0);
                WriteLine("Promedio Fitness:" + prom / Datos.individuosporgeneracion +
                    "\t\nMejor Fitness\t:" + fitness.ToString() + "\t\nGeneraciones restantes\t:" +
                    generaciones.ToString() + "\t");

            }
            WriteLine();
            WriteLine();
            Datos.listaHorariosHijos[0].imprimirBloquesShort();
            WriteLine("Fitness :" + fitness);
            ReadKey();
        }
        public static void nuevaGeneracion()
        {
            Console.WriteLine(mutaciones + " mutaciones \n" + fallos + " individuos fallidos");
            Datos.listaHorariosPadres = Datos.listaHorariosHijos;
            Datos.listaHorariosHijos = new List<Horario>();
            int padre, madre = 0;
            while (Datos.listaHorariosHijos.Count < Datos.listaHorariosPadres.Count)
            {
                madre = Datos.randy.Next(0, (Datos.individuosporgeneracion / 3));
                padre = Datos.randy.Next(0, Datos.listaHorariosHijos.Count);

                Horario[] camada = cruceCyclex(padre, madre);
                insertarCria(camada[0]);
                insertarCria(camada[1]);
            }
            for (int i = 0; i < Datos.individuosporgeneracion / 3; i++)
            {
                if (Datos.listaHorariosPadres[i].fitness() < Datos.listaHorariosHijos[i].fitness())
                {
                    Datos.listaHorariosHijos[i] = Datos.listaHorariosPadres[i];
                }
            }
        }
        public static Horario[] cruceCyclex(int padre, int madre)
        {
            Horario[] camada = new Horario[2] { new Horario(), new Horario() };
            Horario a = Datos.listaHorariosPadres[padre];
            Horario b = Datos.listaHorariosPadres[madre];
            camada[0].datocurso = a.datocurso;
            camada[1].datocurso = b.datocurso;

            int bloque = Datos.randy.Next(0, Datos.listaCursos.Count);
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                if (i != bloque)
                {
                    for (int j = 0; j < 50; j++)
                    {
                        camada[0].setLeccion(j, i, a.getLeccion_bloque(j, i), true);
                        camada[1].setLeccion(j, i, b.getLeccion_bloque(j, i), true);
                    }
                }
            }
            List<int> restantes = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                restantes.Add(i);
            }
            List<List<int>> ciclos = new List<List<int>>();
            while (restantes.Count != 0)
            {
                ciclos.Add(new List<int>());
                int inicio = Datos.randy.Next(0, restantes.Count);
                ciclos[ciclos.Count - 1].Add(restantes[inicio]);
                restantes.RemoveAt(inicio);
                while (!Datos.cursoEqual(   a.getLeccion_bloque(ciclos[ciclos.Count - 1][0], bloque),
                                            b.getLeccion_bloque(ciclos[ciclos.Count - 1][ciclos[ciclos.Count - 1].Count-1], bloque)))
                {
                    for (int i = 0; i < restantes.Count; i++)
                    {
                        if (Datos.cursoEqual(   a.getLeccion_bloque(restantes[i], bloque), 
                                                b.getLeccion_bloque(ciclos[ciclos.Count - 1][ciclos[ciclos.Count - 1].Count - 1], bloque)))
                        {
                            ciclos[ciclos.Count - 1].Add(restantes[i]);
                            restantes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            Horario[] padres = new Horario[2] { a, b };
            bool mutacion=false;
            for (int i = 0; i < ciclos.Count; i++)
            {

                if (Datos.randy.Next(0, 9) == 0)
                {
                    mutacion = true;
                    mutaciones++;
                }
                while (ciclos[i].Count!=0)
                {
                    if (camada[0] != null && !camada[0].insertarFuerte(ciclos[i][0], bloque, padres[i % 2].getLeccion_bloque(ciclos[i][0], bloque), 51, -2))
                    {
                        camada[0] = null;
                        fallos++;
                    }
                    if (camada[1] != null && !camada[1].insertarFuerte(ciclos[i][0], bloque, padres[(i + 1) % 2].getLeccion_bloque(ciclos[i][0], bloque), 51, -2))
                    {
                        camada[1] = null;
                        fallos++;
                    }
                    ciclos[i].RemoveAt(0);
                    if (camada[0] == null && camada[1] == null)
                        return camada;
                }

                mutacion =false;
            }
            return camada;
        }
    }
}
