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
        List<string> indicesCursos;

        public Profesor(string nombre)
        {
            this.nombre = nombre;
            this.horasRestringidas = new List<int[]>();
            this.indicesCursos = new List<string>();
        }

        public bool horavalida(int hora)
        {
            for (int i = 0; i < horasRestringidas.Count; i++)
            {
                if(hora>=horasRestringidas[i][0] && hora <= horasRestringidas[i][1])
                {
                    return false;
                }
            }
            return true;
        }
        public void añadirCurso(int bloque, int curso)
        {
            indicesCursos.Add(bloque+","+curso);
        }

        public void añadirIntervaloHoraRestringida(int inicio, int fin)
        {
            int[] horaRestringida = new int[2];
            horaRestringida[0] = inicio;
            horaRestringida[1] = fin;
            horasRestringidas.Add(horaRestringida);       
        }
        public bool imparteClase(int bloque, int curso)
        {
            if (this.indicesCursos.Contains(bloque+","+curso))
                return true;
            return false;
        }
        public String getNombre()
        {
            return nombre;
        }
    }
}
