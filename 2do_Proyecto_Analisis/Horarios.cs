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
        public Leccion[,] aulas;
        public Leccion[,] bloques;
        public Leccion[,] profesores;
        public List<List<int>> encargados;

        public Horario()
        {
            this.aulas = new Leccion[50,Datos.listaAulas.Count];
            this.bloques = new Leccion[50, Datos.listaCursos.Count];
            this.profesores = new Leccion[50, Datos.listaProfesores.Count];
            this.encargados = new List<List<int>>();
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                this.encargados.Add(new List<int>());
                for (int j = 0; j < Datos.listaCursos[i].Count; j++)
                {
                    encargados[i].Add(Datos.profesorDeClase(i,j));
                }
            }
        }
        public void insertarLeccion(int bloque, int curso)
        {
            int aula = Datos.randy.Next(0, Datos.listaAulas.Count);
            int hora = Datos.randy.Next(0, 50);
            while (this.aulas[hora, aula] != null || this.bloques[hora,bloque] !=null || this.profesores[hora,encargados[bloque][curso]] !=null)
            {
                aula = Datos.randy.Next(0, 5);
                hora = Datos.randy.Next(0, 50);
            }
            this.aulas[hora, aula] = new Leccion(aula, bloque, curso);
            this.bloques[hora, bloque] = new Leccion(aula, bloque, curso);
            this.profesores[hora, encargados[bloque][curso]] = new Leccion(aula, bloque, curso);
        }
        public int getEncargado(int bloque,int curso)
        {
            return encargados[bloque][curso];
        }
    }
}
