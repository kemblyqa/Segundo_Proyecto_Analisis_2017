using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class BackTracking
    {
        List<Leccion> listaLecciones;
        public BackTracking()
        {
            listaLecciones = new List<Leccion>();
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                for (int j = 0; j < Datos.listaCursos[i].Count; j++)
                {

                }
            }
        }

        public void backTracking()
        {
            backTrackingRec(Datos.horarioBackTracking, Datos.listaCursos, 0, 0);
        }

        public bool backTrackingRec(Horario horario, List<List<Curso>> cursos, int hora, int aula)
        {
            bool successful = false;
            for (int i = 0; i < cursos.Count; i++)//total de bloques
            {
                for (int j = 0; j < cursos[i].Count; j++)//total de cursos por bloque
                {
                    Leccion nueva = new Leccion(aula, i, j);
                    if (horario.validar_Campo(hora, nueva))
                    {
                        successful = true;
                    }

                    else if ()//validaciones, restricciones antes de llamada recursiva
                    {

                    }

                    if (successful)
                    {
                        horario.bloques[hora, i] = nueva;
                        horario.aulas[hora, aula] = nueva;
                        horario.profesores[hora, horario.encargados[i][j]] = nueva;
         
                        
                                      
                    }
                }
                
            }
            return false;
        }
    }
}
