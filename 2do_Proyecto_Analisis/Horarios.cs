using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class Horario
    {
        /**
         * cada dia son 10 horas de clases
         * desde las 7am hasta las 5pm
         * lista de 50 elementos
         * elementos al curso, profesor y aula
         * pos 0 curso, pos 1 profesor
         **/
        int[,,] horario;

        public Horario()
        {
            this.horario = new int[5, 50, 2];
        }
    }
}
