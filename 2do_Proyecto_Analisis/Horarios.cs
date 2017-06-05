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
            this.aulas = new Leccion[50, Datos.listaAulas.Count];
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
                aula = Datos.randy.Next(0, Datos.listaAulas.Count);
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
        public bool validar_Campo(int hora, Leccion nueva)
        {
            if (this.aulas[hora, nueva.getAula()] != null || this.bloques[hora, nueva.getBloque()] != null || this.profesores[hora, this.encargados[nueva.getBloque()][nueva.getCurso()]] != null)
            {
                return false;
            }
            return true;
        }
        //public Leccion cambiar(Leccion nueva, int hora)
        //{
        //    Leccion retorno = this.aulas[hora, nueva.getAula()];
        //    List<Leccion> contenedor = new List<Leccion>();
        //    int t = Datos.listaCursos[nueva.getBloque()][nueva.getCurso()].getLecciones();
        //    for (int i = 0; i < 50 || contenedor.Count<t ; i++)
        //    {
        //        if(this.bloques[i, nueva.getBloque()].getCurso() == nueva.getCurso())
        //        {
        //            contenedor.Add(this.bloques[i, nueva.getBloque()]);
        //        }
        //    }
        //    Leccion escogida = contenedor[Datos.randy.Next(0, contenedor.Count())];
        //    this.aulas[hora, escogida.getAula()] = null;
        //    this.bloques[hora, escogida.getBloque()] = null;
        //    this.profesores[hora, this.encargados[escogida.getBloque()][escogida.getCurso()]] = null;
        //    this.aulas[hora, nueva.getAula()] = nueva;
        //    this.bloques[hora, nueva.getBloque()]= nueva;
        //    this.profesores[hora, this.encargados[nueva.getBloque()][nueva.getCurso()]] = nueva;
        //    return retorno;
        //}
        //public void cambiar_Aula(int hora, int aula, ref Horario pareja)
        //{
        //    Leccion objetivo = this.aulas[hora, aula];
        //    if (pareja.validar_Campo(hora, this.aulas[hora, this.aulas[hora, aula].getAula()]))
        //        {
        //        Leccion nuevo = pareja.cambiar(objetivo, hora);
        //        if (this.validar_Campo(hora, nuevo))
        //        {
        //            this.aulas[hora, nuevo.getAula()] = nuevo;
        //            this.bloques[hora, nuevo.getBloque()] = nuevo;
        //            this.profesores[hora, this.encargados[nuevo.getBloque()][nuevo.getCurso()]] = nuevo;
        //        }
        //        else
        //        {
        //            pareja.cambiar(nuevo, hora);
        //        }
        //    }
        //}

        
    }
}
