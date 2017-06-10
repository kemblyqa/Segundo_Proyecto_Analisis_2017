using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2do_Proyecto_Analisis
{
    class BackTracking
    {
        List<List<Leccion>> listaPilasBloque;
        Stopwatch temporizador = new Stopwatch();
        int podas = 0, asig = 0, comp = 0, line = 0, mem = 0;
        int pesoHorario = (4 * 50 * 12 * (Datos.listaAulas.Count + Datos.listaCursos.Count + Datos.listaProfesores.Count)) + (Datos.listaCursos.Count * 8);

        float time;

        public BackTracking()
        {
            listaPilasBloque = new List<List<Leccion>>();
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                listaPilasBloque.Add(new List<Leccion>());
                for (int j = 0; j < Datos.listaCursos[i].Count; j++)
                {
                    for (int k = 0; k < Datos.listaCursos[i][j].getLecciones(); k++)
                    {
                        listaPilasBloque[i].Add(new Leccion(-1, i, j));
                    }
                }
            }           
        }

        public void backTracking()
        {
            temporizador.Start();
            backTrackingRec(Datos.horarioBackTracking, 0, listaPilasBloque); asig++; mem += 68 * 12;
            temporizador.Stop();

            Datos.horarioFinal.imprimir_Bloques();
            WriteLine();

            time = temporizador.ElapsedMilliseconds;
            WriteLine("\nCantidad de podas: " + podas +
                "\nComparaciones: " + comp +
                "\nAsignaciones: " + asig +
                "\nLineas ejecutadas: " + line +
                "\nTiempo en segundos: " + (time /= 1000) +
                "\nMemoria en bytes: " + mem);
            temporizador.Reset();
            ReadKey();
        }

        public bool backTrackingRec(Horario horario, int bloque, List<List<Leccion>> pilaPorBloque)
        {
            comp++; if (bloque < Datos.listaCursos.Count)
            {
                mem += 4; for (int i = 0; i < pilaPorBloque[bloque].Count; i++)
                {
                    comp++; line++; asig++;
                    mem += 4; for (int j = 0; j < Datos.listaAulas.Count; j++)
                    {
                        comp++; line++; asig++;
                        mem += 4; for (int k = 0; k < 50; k++)
                        {
                            comp++; line++; asig++; 
                            pilaPorBloque[bloque][i].setAula(j); asig++; line++;

                            comp++;  if (horario.setLeccion(k, bloque, pilaPorBloque[bloque][i], true))
                            {
                                comp += 8; asig += 3; 
                                Leccion temp = pilaPorBloque[bloque][i]; asig++; line++; mem += 12;
                                pilaPorBloque[bloque].RemoveAt(i); line++; 
                                comp+=2; asig++; if (backTrackingRec(horario, pilaPorBloque[bloque].Count == 0 ? bloque + 1 : bloque, pilaPorBloque))
                                {
                                    pilaPorBloque[bloque].Insert(i, temp); asig++; line++;
                                    return true;
                                }
                                pilaPorBloque[bloque].Insert(i, temp); asig++; line++;
                                horario.popLeccion(bloque, k); line++; asig++;
                            }
                            else
                            {
                                podas++; 
                            }
                        }
                    }
                }
                return false;
            }
            else
            {
                Datos.horarioFinal = Datos.clonar(horario); asig++;line++; mem += pesoHorario;
                return true;
            }           
        }
    }
}

//Console.SetCursorPosition(0, 0);
//Console.WriteLine("Bloque: " + bloque + " Lecciones restantes: " + pilaPorBloque[bloque].Count + "\t");