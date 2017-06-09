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
            horaRestringida[0] = inicio;
            horaRestringida[1] = fin;
            horasRestringidas.Add(horaRestringida);
        }
        public bool horavalida(int hora)
        {
            for (int i = 0; i < horasRestringidas.Count; i++)
            {
                if (hora>=horasRestringidas[i][0] && hora<=horasRestringidas[i][1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
