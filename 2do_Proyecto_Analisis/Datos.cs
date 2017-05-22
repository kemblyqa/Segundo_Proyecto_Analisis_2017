using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class Datos
    {
        public static List<Curso> listaCursos;
        public static List<Profesor> listaProfesores;
        public static List<Aula> listaAulas;
        public static List<Horario> listaHorariosPadres;
        public static List<Horario> listaHorariosHijos;
        public static List<String> dias;
        public static Random inicial;

        public Datos()
        {
            Datos.listaCursos = new List<Curso>();
            Datos.listaProfesores = new List<Profesor>();
            Datos.listaAulas = new List<Aula>();
            Datos.listaHorariosPadres = new List<Horario>();
            Datos.listaHorariosHijos = new List<Horario>();
            Datos.inicial = new Random();
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
                    for (int k = 0; k < listaCursos[j].getLecciones(); k++)
                    {
                        nuevo.insertarClase(j, profesorDeClase(j));
                    }
                }
                listaHorariosHijos.Add(nuevo);
            }
        }

        public int profesorDeClase(int clase)
        {
            List<int> disponibles = new List<int>();
            for (int i = 0; i < Datos.listaProfesores.Count; i++)
            {
                if (Datos.listaProfesores[i].imparteClase(clase))
                    disponibles.Add(i);
            }
            if (disponibles.Count != 0)
                return disponibles[inicial.Next(0, disponibles.Count)];
            return -1;
        }

        public void llenarCursos()
        {
            listaCursos.Add(new Curso("Inglés Básico", 1, 0, 3)); //0
            listaCursos.Add(new Curso("Matemática General", 2, 0, 5));//1
            //8
            listaCursos.Add(new Curso("Ingles I", 1, 1, 3));//2
            listaCursos.Add(new Curso("Comunicación Técnica", 1, 1, 4));//3
            listaCursos.Add(new Curso("Fundamentos de organización de computadoras", 1, 1, 4));//4
            listaCursos.Add(new Curso("Introducción a la programación", 2, 1, 4));//5
            listaCursos.Add(new Curso("Taller de programación", 2, 1, 4));//6
            listaCursos.Add(new Curso("Matemática discreta", 2, 1, 4));//7
            //23
            listaCursos.Add(new Curso("Ingles II", 1, 2, 3));//8
            listaCursos.Add(new Curso("Estructuras de datos", 1, 2, 4));//9
            listaCursos.Add(new Curso("Programación orientada a objetos", 1, 2, 4));//10
            listaCursos.Add(new Curso("Arquitectura de computadores", 1, 2, 4));//11
            listaCursos.Add(new Curso("Cálculo", 2, 2, 4));//12
            //19
            listaCursos.Add(new Curso("Ingles III", 1, 3, 3));//13
            listaCursos.Add(new Curso("Ambiente humano", 1, 3, 3));//14
            listaCursos.Add(new Curso("Análisis de algoritmos", 2, 3, 4));//15
            listaCursos.Add(new Curso("Bases de datos I", 2, 3, 4));//16
            listaCursos.Add(new Curso("Álgebra lineal", 2, 3, 4));//17
            //18
        }

        public void llenarProfesores()
        {
            // PROFESORES DE INGLES
            listaProfesores.Add(new Profesor("Danilo Alpizar"));
            listaProfesores[0].añadirCurso(0); listaProfesores[0].añadirIntervaloHoraRestringida(0, 19);
            listaProfesores[0].añadirCurso(2);
            listaProfesores[0].añadirCurso(8);
            listaProfesores[0].añadirCurso(13);

            listaProfesores.Add(new Profesor("Marlon Perez"));
            listaProfesores[1].añadirCurso(0); listaProfesores[1].añadirIntervaloHoraRestringida(45, 49);
            listaProfesores[1].añadirCurso(2); listaProfesores[1].añadirIntervaloHoraRestringida(0, 4);
            listaProfesores[1].añadirCurso(8);
            listaProfesores[1].añadirCurso(13);

            // PROFESORES DE MATES
            listaProfesores.Add(new Profesor("Karina Gonzales"));
            listaProfesores[2].añadirCurso(1); listaProfesores[2].añadirIntervaloHoraRestringida(20, 29);
            listaProfesores[2].añadirCurso(7);
            listaProfesores[2].añadirCurso(12);
            listaProfesores[2].añadirCurso(17);

            listaProfesores.Add(new Profesor("Dere Elizondo"));
            listaProfesores[3].añadirCurso(1); listaProfesores[3].añadirIntervaloHoraRestringida(30, 39);
            listaProfesores[3].añadirCurso(7);
            listaProfesores[3].añadirCurso(12);
            listaProfesores[3].añadirCurso(17);

            // PROFESORES DE CIENCIAS SOCIALES
            listaProfesores.Add(new Profesor("Erick Acuña"));
            listaProfesores[4].añadirCurso(3); listaProfesores[4].añadirIntervaloHoraRestringida(5, 9);

            listaProfesores.Add(new Profesor("Shirley Alarcón"));
            listaProfesores[5].añadirCurso(14); 

            listaProfesores.Add(new Profesor("Adrián Jaen"));
            listaProfesores[6].añadirCurso(14);

            // PROFESORES DE CARRERA
            listaProfesores.Add(new Profesor("Diego Rojas"));
            listaProfesores[7].añadirCurso(4); listaProfesores[7].añadirIntervaloHoraRestringida(15, 18);
            listaProfesores[7].añadirCurso(9); listaProfesores[7].añadirIntervaloHoraRestringida(25, 28);

            listaProfesores.Add(new Profesor("Vera Gamboa"));
            listaProfesores[8].añadirCurso(5); listaProfesores[8].añadirIntervaloHoraRestringida(0, 9);
            listaProfesores[8].añadirCurso(6);

            listaProfesores.Add(new Profesor("Dennis Valverde"));
            listaProfesores[9].añadirCurso(5);
            listaProfesores[9].añadirCurso(6);

            listaProfesores.Add(new Profesor("Rogelio Gonzales"));
            listaProfesores[10].añadirCurso(9);

            listaProfesores.Add(new Profesor("Oscar Víquez"));
            listaProfesores[11].añadirCurso(9); listaProfesores[11].añadirIntervaloHoraRestringida(10, 12);
            listaProfesores[11].añadirCurso(10); listaProfesores[11].añadirIntervaloHoraRestringida(30, 34);

            listaProfesores.Add(new Profesor("Lorena Valerio"));
            listaProfesores[12].añadirCurso(9);
            listaProfesores[12].añadirCurso(10);
            listaProfesores[12].añadirCurso(15);

            listaProfesores.Add(new Profesor("Rocío Quirós"));
            listaProfesores[13].añadirCurso(4); listaProfesores[13].añadirIntervaloHoraRestringida(0, 1);
            listaProfesores[13].añadirCurso(11); listaProfesores[13].añadirIntervaloHoraRestringida(10, 11);

            listaProfesores.Add(new Profesor("Jorge Alfaro"));
            listaProfesores[14].añadirCurso(11); listaProfesores[14].añadirIntervaloHoraRestringida(15, 19);

            listaProfesores.Add(new Profesor("Leonardo Víquez"));
            listaProfesores[15].añadirCurso(16);
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
