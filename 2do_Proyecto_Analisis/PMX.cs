using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2do_Proyecto_Analisis
{
    static class PMX
    {
        static int mutaciones,fallos = 0;
        static int generaciones;
        static List<Horario> camada;
        public static void crucePMX()
        {
            mutaciones = 0;
            fallos = 0;
            generaciones = 0;
            int fitness = -1;
            Datos.listaHorariosHijos = new List<Horario>();
            Datos.listaHorariosPadres = new List<Horario>();
            Datos.poblacionInicial((Datos.cantidadlecciones / 3) * 4);
            generaciones = Datos.cantidadlecciones * 3;
            Console.Clear();
            for (int j = 0; j < Datos.cantidadlecciones * 3 && fitness != 0; j++)
            {
                PMX.nuevaGeneracion();
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
        public static void insertarCria(Horario nueva)
        {
            if (nueva == null || Datos.listaHorariosHijos.Count == Datos.individuosporgeneracion)
                return;
            int newfit=nueva.fitness();
            for (int i = 0; i < Datos.listaHorariosHijos.Count; i++)
            {
                if (!Datos.distintos(Datos.listaHorariosHijos[i], nueva,Datos.cantidadlecciones/4))
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
        public static void nuevaGeneracion()
        {
            Console.WriteLine(mutaciones + " mutaciones \n" + fallos + " camadas fallidas");
            Datos.listaHorariosPadres = Datos.listaHorariosHijos;
            Datos.listaHorariosHijos = new List<Horario>();
            int padre, madre=0;
            while (Datos.listaHorariosHijos.Count < Datos.listaHorariosPadres.Count)
            {
                madre = Datos.randy.Next(0,(Datos.individuosporgeneracion/3));
                padre = Datos.randy.Next(0, Datos.listaHorariosHijos.Count);

                List<Horario> camada = cruce_Bloques(padre,madre,true);
                insertarCria(camada[0]);
                insertarCria(camada[1]);
            }
            for (int i = 0; i < Datos.individuosporgeneracion/3; i++)
            {
                if (Datos.listaHorariosPadres[i].fitness() < Datos.listaHorariosHijos[i].fitness())
                {
                    Datos.listaHorariosHijos[i] = Datos.listaHorariosPadres[i];
                }
            }
        }
        public static List<Horario> cruce(int padre, int madre)
        {

            return null;
        }
        public static List<Horario> cruce_Bloques(int padre, int madre, bool todo)
        {
            PMX.camada = new List<Horario>();
            camada.Add(Datos.clonar(Datos.listaHorariosPadres[padre]));
            camada.Add(Datos.clonar(Datos.listaHorariosPadres[madre]));
            int[] intervalo = new int[2];
            int repeticiones;
            if (todo)
            {
                repeticiones=Datos.randy.Next(0,Datos.listaCursos.Count);
            }
            else
            {
                repeticiones = 1;
            }
            for (int r = 0; r < repeticiones; r++)
            {
                int bloque = Datos.randy.Next(0, Datos.listaCursos.Count);
                List<int[]> mapeo = new List<int[]>();
                Leccion aux;
                intervalo[0] = Datos.randy.Next(0, 30);
                intervalo[1] = Datos.randy.Next(intervalo[0], intervalo[0] + 20);
                for (int i = intervalo[0]; i <= intervalo[1]; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        int x = camada[j].getEncargado(camada[(j + 1) % 2].getLeccion_bloque(i, bloque));
                        if (x != -1 && !Datos.listaProfesores[x].horavalida(i))
                        {
                            fallos++;
                            return new List<Horario>() { null, null };
                        }
                        else
                        {

                        }
                    }
                }
                for (int i = intervalo[0]; i <= intervalo[1]; i++)
                {
                    aux = camada[0].popLeccion(bloque, i);
                    if (
                    !camada[0].insertarFuerte(i, bloque, camada[1].popLeccion(bloque, i), intervalo[0], intervalo[1]) ||
                    !camada[1].insertarFuerte(i, bloque, aux, intervalo[0], intervalo[1]))
                    {

                    }

                    int[] tupla = new int[2];

                    aux = camada[0].getLeccion_bloque(i, bloque);
                    tupla[0] = aux != null ? aux.getCurso() : -1;

                    aux = camada[1].getLeccion_bloque(i, bloque);
                    tupla[1] = aux != null ? aux.getCurso() : -1;
                    mapeo = insertarMapeo(mapeo, tupla);
                }
                Leccion[,] pila = new Leccion[2, mapeo.Count];
                int[,] horas = new int[mapeo.Count, 2];
                for (int h = 0; h < mapeo.Count; h++)
                {
                    for (int k = 0; k < 50; k++)
                    {
                        if (k == intervalo[0])
                        {
                            k = intervalo[1];
                        }
                        for (int m = 0; m < 2; m++)
                        {
                            if (mapeo[h][m] == -1)
                            {
                                if (camada[m].getLeccion_bloque(k, bloque) == null)
                                {
                                    pila[(m + 1) % 2, h] = null;
                                    horas[h, m] = k;
                                }
                            }
                            else
                            {
                                if (pila[(m + 1) % 2, h] == null && camada[m].getLeccion_bloque(k, bloque) != null &&
                                    camada[m].getLeccion_bloque(k, bloque).getCurso() == mapeo[h][m])
                                {
                                    aux = camada[m].popLeccion(bloque, k); ;
                                    pila[(m + 1) % 2, h] = aux;
                                    horas[h, m] = k;
                                }
                            }
                        }
                    }
                }
                //mutacion
                for (int i = 0; i < 2; i++)
                {
                    int x = Datos.randy.Next(0, pila.GetLength(1) / 3 == 0 ? Datos.randy.Next(0, 2) : (pila.GetLength(1) / 3) + 1);
                    for (int j = 0; j < x; j++)
                    {
                        mutaciones++;
                        int a, b;
                        a = Datos.randy.Next(0, pila.GetLength(1));
                        b = Datos.randy.Next(0, pila.GetLength(1));
                        aux = pila[i, a];
                        pila[i, a] = pila[i, b];
                        pila[i, b] = aux;
                    }
                }
                //mutacion
                for (int i = 0; i < pila.GetLength(1); i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        camada[j].insertarFuerte(horas[i, j], bloque, pila[j, i], intervalo[0], intervalo[1]);
                    }
                }
            }
            
            for (int i = 0; i < 2; i++)
            {
                if (camada[i].apto() != null)
                {
                    camada[i] = null;
                }
            }
            if (camada[0] == null && camada[1] == null)
                fallos++;
            return camada;
        }
        public static List<int[]> insertarMapeo(List<int[]> mapeo,int[] nuevo)
        {
            if (nuevo[0]==nuevo[1])
                return mapeo;
            for (int i = 0; i < mapeo.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (nuevo[(j + 1) % 2] == mapeo[i][j])
                    {
                        nuevo[(j + 1) % 2] = mapeo[i][(j + 1) % 2];
                        mapeo.RemoveAt(i);
                        return insertarMapeo(mapeo, nuevo);
                    }
                }
            }
            mapeo.Add(nuevo);
            return mapeo;
        }
        
    }
}
