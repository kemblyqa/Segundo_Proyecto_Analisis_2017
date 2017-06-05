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
        public Leccion cambiar(Leccion nueva, int hora)
        {
            Leccion retorno = this.aulas[hora, nueva.getAula()];
            List<Leccion> contenedor = new List<Leccion>();
            int t = Datos.listaCursos[nueva.getBloque()][nueva.getCurso()].getLecciones();
            for (int i = 0; i < 50 || contenedor.Count<t ; i++)
            {
                if(this.bloques[i, nueva.getBloque()].getCurso() == nueva.getCurso())
                {
                    contenedor.Add(this.bloques[i, nueva.getBloque()]);
                }
            }
            Leccion escogida = contenedor[Datos.randy.Next(0, contenedor.Count())];
            this.aulas[hora, escogida.getAula()] = null;
            this.bloques[hora, escogida.getBloque()] = null;
            this.profesores[hora, this.encargados[escogida.getBloque()][escogida.getCurso()]] = null;
            this.aulas[hora, nueva.getAula()] = nueva;
            this.bloques[hora, nueva.getBloque()]= nueva;
            this.profesores[hora, this.encargados[nueva.getBloque()][nueva.getCurso()]] = nueva;
            return retorno;
        }
        public void cambiar_Aula(int hora, int aula, int pareja)
        {
            Leccion objetivo = this.aulas[hora, aula];
            if (Datos.listaHorariosPadres[pareja].validar_Campo(hora, this.aulas[hora, this.aulas[hora, aula].getAula()]))
                {
                Leccion nuevo = Datos.listaHorariosPadres[pareja].cambiar(objetivo, hora);
                if (this.validar_Campo(hora, nuevo))
                {
                    this.aulas[hora, nuevo.getAula()] = nuevo;
                    this.bloques[hora, nuevo.getBloque()] = nuevo;
                    this.profesores[hora, this.encargados[nuevo.getBloque()][nuevo.getCurso()]] = nuevo;
                }
                else
                {
                    Datos.listaHorariosPadres[pareja].cambiar(nuevo, hora);
                }
            }
        }
        public int fitness()
        {
            int fallos = 0;
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                for (int j = 0; j < Datos.listaCursos[i].Count; j++)
                {
                    int clases = 0;
                    for (int k = 0; k < 50; k++)
                    {
                        if (this.bloques[i,k].getCurso()==j)
                        {
                            clases++;
                            while (k!=50 && this.bloques[i, k].getCurso() == j)
                            {
                                k++;
                            }
                        }
                    }
                    int diferencia = clases - Datos.listaCursos[i][j].getClases();
                    if (diferencia!=0)
                    {
                        fallos += Math.Abs(diferencia);
                    }
                }
            }
            return fallos;
        }
        public void cruceAulas_Seccion(int macho)
        {
            int inicio = Datos.randy.Next(0, 50);
            int limite = Datos.randy.Next(inicio,(inicio+20<50)? inicio+20:50);
            int aula = Datos.randy.Next(0, Datos.listaAulas.Count);
            int hembra = Datos.randy.Next(0, Datos.listaHorariosPadres.Count);
            if (hembra == macho)
                hembra = (hembra + 1) % Datos.listaHorariosPadres.Count;
            for (int i = inicio; i < limite; i++)
            {
                this.cambiar_Aula(i,aula, hembra);
            }
        }
    }
}
