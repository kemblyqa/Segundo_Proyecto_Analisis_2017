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
        public static List<Profesor> listaProfesores;
        public static List<Aula> listaAulas;
        public static List<Horario> listaHorariosPadres;
        public static List<Horario> listaHorariosHijos;
        public static List<String> dias;
        public static Random randy;

        public Datos()
        {
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
            poblacionInicial();
        }
        public void poblacionInicial()
        {
            for (int i = 0; i < 10; i++)
            {
                Horario nuevo = new Horario();
                for (int j = 0; j < listaCursos.Count; j++)
                {
                    for (int k = 0; k < listaCursos[j].Count; k++)
                    {
                        for (int l = 0; l < listaCursos[j][k].getLecciones(); l++)
                        {
                            nuevo.insertarLeccion(j,k);
                        }

                    }
                }
                listaHorariosHijos.Add(nuevo);
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

        public void llenarCursos()
        {
            //0
            listaCursos.Add(new List<Curso>());
            listaCursos[0].Add(new Curso("Inglés Básico", 1, 0, 3)); //0
            listaCursos[0].Add(new Curso("Matemática General", 2, 0, 5));//1
            //1
            listaCursos.Add(new List<Curso>());
            listaCursos[1].Add(new Curso("Ingles I", 1, 1, 3));//0
            listaCursos[1].Add(new Curso("Comunicación Técnica", 1, 1, 4));//1
            listaCursos[1].Add(new Curso("Fundamentos de organización de computadoras", 1, 1, 4));//2
            listaCursos[1].Add(new Curso("Introducción a la programación", 2, 1, 4));//3
            listaCursos[1].Add(new Curso("Taller de programación", 2, 1, 4));//4
            listaCursos[1].Add(new Curso("Matemática discreta", 2, 1, 4));//5
            //2
            listaCursos.Add(new List<Curso>());
            listaCursos[2].Add(new Curso("Ingles II", 1, 2, 3));//0
            listaCursos[2].Add(new Curso("Estructuras de datos", 1, 2, 4));//1
            listaCursos[2].Add(new Curso("Programación orientada a objetos", 1, 2, 4));//2
            listaCursos[2].Add(new Curso("Arquitectura de computadores", 1, 2, 4));//3
            listaCursos[2].Add(new Curso("Cálculo", 2, 2, 4));//4
            //3
            listaCursos.Add(new List<Curso>());
            listaCursos[3].Add(new Curso("Ingles III", 1, 3, 3));//0
            listaCursos[3].Add(new Curso("Ambiente humano", 1, 3, 3));//1
            listaCursos[3].Add(new Curso("Análisis de algoritmos", 2, 3, 4));//2
            listaCursos[3].Add(new Curso("Bases de datos I", 2, 3, 4));//3
            listaCursos[3].Add(new Curso("Álgebra lineal", 2, 3, 4));//4
        }

        public void llenarProfesores()
        {
            // PROFESORES DE INGLES
            listaProfesores.Add(new Profesor("Danilo Alpizar"));
            listaProfesores[0].añadirCurso(0,0); listaProfesores[0].añadirIntervaloHoraRestringida(0, 19);
            listaProfesores[0].añadirCurso(0,0);
            listaProfesores[0].añadirCurso(2,0);
            listaProfesores[0].añadirCurso(3,0);

            listaProfesores.Add(new Profesor("Marlon Perez"));
            listaProfesores[1].añadirCurso(0,0); listaProfesores[1].añadirIntervaloHoraRestringida(45, 49);
            listaProfesores[1].añadirCurso(1,0); listaProfesores[1].añadirIntervaloHoraRestringida(0, 4);
            listaProfesores[1].añadirCurso(2,0);
            listaProfesores[1].añadirCurso(3,0);

            // PROFESORES DE MATES
            listaProfesores.Add(new Profesor("Karina Gonzales"));
            listaProfesores[2].añadirCurso(0,1); listaProfesores[2].añadirIntervaloHoraRestringida(20, 29);
            listaProfesores[2].añadirCurso(1,5);
            listaProfesores[2].añadirCurso(2,4);
            listaProfesores[2].añadirCurso(3,4);

            listaProfesores.Add(new Profesor("Dere Elizondo"));
            listaProfesores[3].añadirCurso(0,1); listaProfesores[3].añadirIntervaloHoraRestringida(30, 39);
            listaProfesores[3].añadirCurso(1,5);
            listaProfesores[3].añadirCurso(2,4);
            listaProfesores[3].añadirCurso(3,4);

            // PROFESORES DE CIENCIAS SOCIALES
            listaProfesores.Add(new Profesor("Erick Acuña"));
            listaProfesores[4].añadirCurso(1,1); listaProfesores[4].añadirIntervaloHoraRestringida(5, 9);

            listaProfesores.Add(new Profesor("Shirley Alarcón"));
            listaProfesores[5].añadirCurso(3,1); 

            listaProfesores.Add(new Profesor("Adrián Jaen"));
            listaProfesores[6].añadirCurso(3,1);

            // PROFESORES DE CARRERA
            listaProfesores.Add(new Profesor("Diego Rojas"));
            listaProfesores[7].añadirCurso(1,2); listaProfesores[7].añadirIntervaloHoraRestringida(15, 18);
            listaProfesores[7].añadirCurso(2,1); listaProfesores[7].añadirIntervaloHoraRestringida(25, 28);

            listaProfesores.Add(new Profesor("Vera Gamboa"));
            listaProfesores[8].añadirCurso(1,3); listaProfesores[8].añadirIntervaloHoraRestringida(0, 9);
            listaProfesores[8].añadirCurso(1,4);

            listaProfesores.Add(new Profesor("Dennis Valverde"));
            listaProfesores[9].añadirCurso(1,3);
            listaProfesores[9].añadirCurso(1,4);

            listaProfesores.Add(new Profesor("Rogelio Gonzales"));
            listaProfesores[10].añadirCurso(2,1);

            listaProfesores.Add(new Profesor("Oscar Víquez"));
            listaProfesores[11].añadirCurso(2,1); listaProfesores[11].añadirIntervaloHoraRestringida(10, 12);
            listaProfesores[11].añadirCurso(2,2); listaProfesores[11].añadirIntervaloHoraRestringida(30, 34);

            listaProfesores.Add(new Profesor("Lorena Valerio"));
            listaProfesores[12].añadirCurso(2,1);
            listaProfesores[12].añadirCurso(2,2);
            listaProfesores[12].añadirCurso(3,2);

            listaProfesores.Add(new Profesor("Rocío Quirós"));
            listaProfesores[13].añadirCurso(1,2); listaProfesores[13].añadirIntervaloHoraRestringida(0, 1);
            listaProfesores[13].añadirCurso(2,3); listaProfesores[13].añadirIntervaloHoraRestringida(10, 11);

            listaProfesores.Add(new Profesor("Jorge Alfaro"));
            listaProfesores[14].añadirCurso(2,3); listaProfesores[14].añadirIntervaloHoraRestringida(15, 19);

            listaProfesores.Add(new Profesor("Leonardo Víquez"));
            listaProfesores[15].añadirCurso(3,3);
        }

        public void llenarAulas()
        {
            listaAulas.Add(new Aula("I1")); listaAulas[0].añadirIntervaloHoraRestringida(16, 17);
            listaAulas.Add(new Aula("I2")); listaAulas[1].añadirIntervaloHoraRestringida(5, 9);
            listaAulas.Add(new Aula("I3")); listaAulas[2].añadirIntervaloHoraRestringida(20, 23);
            listaAulas.Add(new Aula("I4")); listaAulas[3].añadirIntervaloHoraRestringida(32, 35);
            listaAulas.Add(new Aula("I5")); listaAulas[4].añadirIntervaloHoraRestringida(45, 49);
        }
    }
}
