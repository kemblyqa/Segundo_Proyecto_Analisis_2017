using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class BackTracking
    {
        public void backTracking()
        {
            backTrackingRec(Datos.horarioBackTracking, Datos.listaCursos, 0);
        }

        public bool backTrackingRec(Horario horario, List<List<Curso>> cursos, int hora)
        {
            for (int i = 0; i < cursos.Count; i++)//total de bloques
            {
                for (int j = 0; j < ; j++)
                {

                }
            }
            return false;
        }
    }
}
