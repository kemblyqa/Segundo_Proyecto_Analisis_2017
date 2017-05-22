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
        public int[,,] horario;

        public Horario()
        {
            this.horario = new int[5, 50, 2];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        this.horario[i, j, k] = -1;
                    }
                }
            }
        }
        public void insertarClase(int clase, int profesor)
        {
            int aula = Datos.inicial.Next(0, 5);
            int hora = Datos.inicial.Next(0, 50);
            while (this.horario[aula, hora, 0] != -1)
            {
                aula = Datos.inicial.Next(0, 5);
                hora = Datos.inicial.Next(0, 50);
            }
            this.horario[aula, hora, 0] = clase;
            this.horario[aula, hora, 1] = profesor;
        }
    }
}
