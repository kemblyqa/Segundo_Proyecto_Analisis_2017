using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class BackTracking
    {
        // List<Leccion> listaLecciones;
        //listaLecciones.Add(new Leccion(-1, i, j));
        //listaLecciones = new List<Leccion>();

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
            backTrackingRec(Datos.horarioBackTracking, 0, 0, 0, listaPilasBloque);
        }

        public bool backTrackingRec(Horario horario, int bloque, int hora, int aula, List<List<Leccion>> pilaPorBloque)
        {
            if (pilaPorBloque[bloque].Count == 0)
            {
                if (bloque != Datos.listaCursos.Count - 1)
                {
                    if (backTrackingRec(horario, bloque + 1, 0, 0, pilaPorBloque))
                    {
                        return true;
                    }
                }
                else
                {
                    Datos.horarioFinal = Datos.clonar(horario);
                    return true;
                }
            }

            for (int i = 0; i < pilaPorBloque[bloque].Count; i++)
            {
                for (int j = 0; j < Datos.listaAulas.Count; j++)
                {
                    pilaPorBloque[bloque][i].setAula(j);

                    if (horario.setLeccion(hora, bloque, pilaPorBloque[bloque][i]))
                    {
                        pilaPorBloque[bloque].RemoveAt(i);
                        if (backTrackingRec(horario, bloque, hora + 1, aula, pilaPorBloque))
                        {
                            pilaPorBloque[bloque].Insert(i, pilaPorBloque[bloque][i]);
                            return true;
                        }
                        pilaPorBloque[bloque].Insert(i, pilaPorBloque[bloque][i]);
                        horario.popLeccion(bloque, hora);
                    }
                }        
            }
            return false;
        }
    }
}
