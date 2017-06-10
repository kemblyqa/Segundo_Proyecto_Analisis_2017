using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class BackTracking
    {
        List<List<Leccion>> listaPilasBloque;
        Random rnd;
        public BackTracking()
        {
            rnd = new Random();
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
            backTrackingRec(Datos.horarioBackTracking, 0, listaPilasBloque);
            Datos.horarioFinal.imprimir_Bloques();
        }

        public bool backTrackingRec(Horario horario, int bloque, List<List<Leccion>> pilaPorBloque)
        {
            if (bloque < Datos.listaCursos.Count)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Bloque: " + bloque + " Lecciones restantes: " + pilaPorBloque[bloque].Count + "\t");
                for (int i = 0; i < pilaPorBloque[bloque].Count; i++)
                {
                    for (int j = 0; j < Datos.listaAulas.Count; j++)
                    {
                        for (int k = 0; k < 50; k++)
                        {
                            pilaPorBloque[bloque][i].setAula(j);

                            if (horario.setLeccion(k, bloque, pilaPorBloque[bloque][i], true))
                            {
                                Leccion temp = pilaPorBloque[bloque][i];
                                pilaPorBloque[bloque].RemoveAt(i);
                                if (backTrackingRec(horario, pilaPorBloque[bloque].Count == 0 ? bloque + 1 : bloque, pilaPorBloque))
                                {
                                    pilaPorBloque[bloque].Insert(i, temp);
                                    return true;
                                }
                                pilaPorBloque[bloque].Insert(i, temp);
                                horario.popLeccion(bloque, k);
                            }
                        }
                    }
                }
                return false;
            }
            else
            {
                Datos.horarioFinal = Datos.clonar(horario);
                return true;
            }           
        }
    }
}
