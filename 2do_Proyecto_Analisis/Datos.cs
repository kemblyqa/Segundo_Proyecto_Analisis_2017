using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class Datos
    {
        public static List<List<Curso>> listaCursos;
        public static List<int> leccionesPorCurso;
        public static List<Profesor> listaProfesores;
        public static List<Aula> listaAulas;
        public static List<Horario> listaHorariosPadres;
        public static List<Horario> listaHorariosHijos;
        public static List<String> dias;
        public static Random randy;

        public static Horario horarioBackTracking;
        public static Horario horarioFinal;
        public static int individuosporgeneracion;
        public static int cantidadlecciones;
        public Datos()
        {
            leccionesPorCurso = new List<int>();
            Datos.listaCursos = new List<List<Curso>>();
            Datos.listaProfesores = new List<Profesor>();
            Datos.listaAulas = new List<Aula>();
            Datos.listaHorariosPadres = new List<Horario>();
            Datos.listaHorariosHijos = new List<Horario>();
            Datos.randy = new Random();
            Datos.dias = new List<String>();

            Datos.dias.Add("Lunes ");
            Datos.dias.Add("Martes ");
            Datos.dias.Add("Miercoles ");
            Datos.dias.Add("Jueves ");
            Datos.dias.Add("Viernes ");

            llenarCursos();
            llenarProfesores();
            llenarAulas();
            poblacionInicial(1);
            cantidadlecciones = listaHorariosHijos[0].leccionesDentro();
            poblacionInicial((cantidadlecciones/3)*4);

            Datos.horarioBackTracking = new Horario();
            Datos.horarioFinal = new Horario();
        }
        public static void poblacionInicial(int cantidad)
        {
            individuosporgeneracion = cantidad+1;
            while(listaHorariosHijos.Count<cantidad)
            {
                Horario nuevo = new Horario();
                for (int j = 0; j < listaCursos.Count; j++)
                {
                    for (int k = 0; k < listaCursos[j].Count; k++)
                    {
                        for (int l = 0; l < listaCursos[j][k].getLecciones(); l++)
                        {
                            nuevo.insertarCurso(j, k);
                        }
                    }
                }

                bool unico = true;
                for (int m = 0; m < listaHorariosHijos.Count; m++)
                {
                    if (!distintos(nuevo, listaHorariosHijos[m],1))
                    {
                        unico = false;
                    }
                }
                if (unico)
                {
                    listaHorariosHijos.Add(nuevo);
                }
            }
        }

        public static int profesorDeClase(int bloque, int curso)
        {
            List<int> disponibles = new List<int>();
            for (int i = 0; i < Datos.listaProfesores.Count; i++)
            {
                if (Datos.listaProfesores[i].imparteClase(bloque, curso))
                    disponibles.Add(i);
            }
            if (disponibles.Count != 0)
                return disponibles[randy.Next(0, disponibles.Count)];
            return -1;
        }
        public void insertar_curso(string nombre,int clases,int bloque, int lecciones)
        {
            for (int i = listaCursos.Count; i <= bloque; i++)
            {
                listaCursos.Add(new List<Curso>());
                leccionesPorCurso.Add(0);
            }
            listaCursos[bloque].Add(new Curso(nombre, clases, bloque, lecciones));
            leccionesPorCurso[bloque] += lecciones;
        }
        public void llenarCursos() //[0] bloque, [1] cursos del bloque
        {
            //0
            insertar_curso("Inglés Básico", 1, 0, 3); //0
            insertar_curso("Matemática General", 2, 0, 5);//1
            //1
            insertar_curso("Ingles I", 1, 1, 3);//0
            insertar_curso("Comunicación Técnica", 1, 1, 4);//1
            insertar_curso("Fundamentos de organización de computadoras", 1, 1, 4);//2
            insertar_curso("Introducción a la programación", 2, 1, 4);//3
            insertar_curso("Taller de programación", 2, 1, 4);//4
            insertar_curso("Matemática discreta", 2, 1, 4);//5
            //2
            insertar_curso("Ingles II", 1, 2, 3);//0
            insertar_curso("Estructuras de datos", 1, 2, 4);//1
            insertar_curso("Programación orientada a objetos", 1, 2, 4);//2
            insertar_curso("Arquitectura de computadores", 1, 2, 4);//3
            insertar_curso("Cálculo", 2, 2, 4);//4
            //3
            insertar_curso("Ingles III", 1, 3, 3);//0
            insertar_curso("Ambiente humano", 1, 3, 3);//1
            insertar_curso("Análisis de algoritmos", 2, 3, 4);//2
            insertar_curso("Bases de datos I", 2, 3, 4);//3
            insertar_curso("Álgebra lineal", 2, 3, 4);//4
        }

        public void llenarProfesores()
        {
            // PROFESORES DE INGLES
            listaProfesores.Add(new Profesor("Danilo Alpizar"));//0
            listaProfesores[0].añadirCurso(0, 0); listaProfesores[0].añadirIntervaloHoraRestringida(15, 19);
            listaProfesores[0].añadirCurso(0, 0);
            listaProfesores[0].añadirCurso(2, 0);
            listaProfesores[0].añadirCurso(3, 0);

            listaProfesores.Add(new Profesor("Marlon Perez"));//1
            listaProfesores[1].añadirCurso(0, 0); listaProfesores[1].añadirIntervaloHoraRestringida(45, 49);
            listaProfesores[1].añadirCurso(1, 0);
            listaProfesores[1].añadirCurso(2, 0);
            listaProfesores[1].añadirCurso(3, 0);

            // PROFESORES DE MATES
            listaProfesores.Add(new Profesor("Karina Gonzales"));//2
            listaProfesores[2].añadirCurso(0, 1); listaProfesores[2].añadirIntervaloHoraRestringida(25, 29);
            listaProfesores[2].añadirCurso(1, 5);
            listaProfesores[2].añadirCurso(2, 4);
            listaProfesores[2].añadirCurso(3, 4);

            listaProfesores.Add(new Profesor("Dere Elizondo"));//3
            listaProfesores[3].añadirCurso(0, 1); listaProfesores[3].añadirIntervaloHoraRestringida(35, 39);
            listaProfesores[3].añadirCurso(1, 5);
            listaProfesores[3].añadirCurso(2, 4);
            listaProfesores[3].añadirCurso(3, 4);

            // PROFESORES DE CIENCIAS SOCIALES
            listaProfesores.Add(new Profesor("Erick Acuña"));//4
            listaProfesores[4].añadirCurso(1, 1); listaProfesores[4].añadirIntervaloHoraRestringida(5, 9);

            listaProfesores.Add(new Profesor("Shirley Alarcón"));//5
            listaProfesores[5].añadirCurso(3, 1);

            listaProfesores.Add(new Profesor("Adrián Jaen"));//6
            listaProfesores[6].añadirCurso(3, 1);

            // PROFESORES DE CARRERA
            listaProfesores.Add(new Profesor("Diego Rojas"));////7
            listaProfesores[7].añadirCurso(1, 2); listaProfesores[7].añadirIntervaloHoraRestringida(15, 18);
            listaProfesores[7].añadirCurso(2, 1); 

            listaProfesores.Add(new Profesor("Vera Gamboa"));//8
            listaProfesores[8].añadirCurso(1, 3); listaProfesores[8].añadirIntervaloHoraRestringida(0, 5);
            listaProfesores[8].añadirCurso(1, 4);

            listaProfesores.Add(new Profesor("Dennis Valverde"));//9
            listaProfesores[9].añadirCurso(1, 3);
            listaProfesores[9].añadirCurso(1, 4);

            listaProfesores.Add(new Profesor("Rogelio Gonzales"));//10
            listaProfesores[10].añadirCurso(2, 1);

            listaProfesores.Add(new Profesor("Oscar Víquez"));//11
            listaProfesores[11].añadirCurso(2, 1); listaProfesores[11].añadirIntervaloHoraRestringida(10, 12);
            listaProfesores[11].añadirCurso(2, 2); 

            listaProfesores.Add(new Profesor("Lorena Valerio"));//12
            listaProfesores[12].añadirCurso(2, 1);
            listaProfesores[12].añadirCurso(2, 2);
            listaProfesores[12].añadirCurso(3, 2);

            listaProfesores.Add(new Profesor("Rocío Quirós"));//13
            listaProfesores[13].añadirCurso(1, 2); listaProfesores[13].añadirIntervaloHoraRestringida(0, 1);
            listaProfesores[13].añadirCurso(2, 3); 

            listaProfesores.Add(new Profesor("Jorge Alfaro"));//14
            listaProfesores[14].añadirCurso(2, 3); listaProfesores[14].añadirIntervaloHoraRestringida(15, 19);

            listaProfesores.Add(new Profesor("Leonardo Víquez"));//15
            listaProfesores[15].añadirCurso(3, 3);
        }

        public void llenarAulas()
        {
            listaAulas.Add(new Aula("I1")); listaAulas[0].añadirIntervaloHoraRestringida(16, 17);
            listaAulas.Add(new Aula("I2")); listaAulas[1].añadirIntervaloHoraRestringida(5, 9);
            /*
            listaAulas.Add(new Aula("I3")); listaAulas[2].añadirIntervaloHoraRestringida(20, 23);
            listaAulas.Add(new Aula("I4")); listaAulas[3].añadirIntervaloHoraRestringida(32, 35);
            listaAulas.Add(new Aula("I5")); listaAulas[4].añadirIntervaloHoraRestringida(45, 49);*/
        }
        public static Horario clonar(Horario origen)
        {
            Horario destino = new Horario();
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                for (int j = 0; j < Datos.listaCursos[i].Count; j++)
                {
                    destino.setEncargado(i, j, origen.getEncargado(i, j));
                }
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < Datos.listaCursos.Count; j++)
                {
                    destino.setLeccion(i, j, clonar(origen.getLeccion_bloque(i, j)), true);
                }
            }
            return destino;
        }
        public static Leccion clonar(Leccion origen)
        {
            if (origen == null)
                return null;
            return new Leccion(origen.getAula(),origen.getBloque(),origen.getCurso());
        }
        public static bool cursoEqual(Leccion x, Leccion y)
        {
            return (!(x == null && y != null) &&
                    !(x != null && y == null) &&
                    ((x == null && y == null) || (x.getCurso() == y.getCurso())));
        }

        public static bool distintos(Horario a, Horario b, int x)
        {
            int diferencias = 0;
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (!Datos.cursoEqual(a.getLeccion_bloque(j, i), b.getLeccion_bloque(j, i)))
                    {
                        diferencias++;
                        if (diferencias > cantidadlecciones / x)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
