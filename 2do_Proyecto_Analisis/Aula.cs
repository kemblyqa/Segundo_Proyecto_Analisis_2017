using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class Aula
    {
        string nombre;
        List<int[]> horasRestringidas;

        public Aula(string nombre)
        {
            this.nombre = nombre;
            this.horasRestringidas = new List<int[]>();
        }

        public void añadirIntervaloHoraRestringida(int inicio, int fin)
        {
            int[] horaRestringida = new int[2];

            for (int i = inicio; i <= fin; i++)
            {
                horaRestringida[0] = i / 10;
                horaRestringida[1] = i % 10;
                horasRestringidas.Add(horaRestringida);
            }
        }
    }
}
