using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class Profesor
    {
        string nombre;
        List<int[]> horasRestringidas;
        List<int> indicesCursos;

        public Profesor(string nombre)
        {
            this.nombre = nombre;
            this.horasRestringidas = new List<int[]>();
            this.indicesCursos = new List<int>();
        }

        public void añadirCurso(int indice)
        {
            indicesCursos.Add(indice);
        }

        public void añadirIntervaloHoraRestringida(int inicio, int fin)
        {
            int[] horaRestringida = new int[2];

            for (int i = inicio; i <= fin; i++)
            {
                horaRestringida[0] = i/10;
                horaRestringida[1] = i%10;
                horasRestringidas.Add(horaRestringida);
            }            
        }
        public bool imparteClase(int clase)
        {
            if (this.indicesCursos.Contains(clase))
                return true;
            return false;
        }
        public String getNombre()
        {
            return nombre;
        }
    }
}
