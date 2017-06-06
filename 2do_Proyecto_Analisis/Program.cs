using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2do_Proyecto_Analisis
{
    class Program
    {
        static Datos datos;
        static BackTracking back;
        static void Main(string[] args)
        {
            datos = new Datos();
            imprimirPoblacion_Todo(Datos.listaHorariosHijos);
            back = new BackTracking();
            back.backTracking();
            ReadKey();
        }
        static void imprimirPoblacion_Aulas( List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i+1)+"\n");
                for (int j = 0; j < poblacion[i].aulas.GetLength(1); j++)
                {
                    WriteLine("     Aula " + (j)+ "\n");
                    for (int k = 0; k < poblacion[i].aulas.GetLength(0); k++)
                    {
                        if (poblacion[i].aulas[k, j] != null)
                        {
                            WriteLine("            " + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                                Datos.listaProfesores[poblacion[i].getEncargado(poblacion[i].aulas[k, j].getBloque(), poblacion[i].aulas[k, j].getCurso())].getNombre() + "\t" +
                                "\t Bloque " + poblacion[i].aulas[k, j].getBloque() + ": " +
                                Datos.listaCursos[poblacion[i].aulas[k, j].getBloque()][poblacion[i].aulas[k, j].getCurso()].getNombre());
                        }
                    }
                }
            }
            ReadKey();
        }
        static void imprimirPoblacion_Bloques(List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i + 1) + "\n");
                for (int j = 0; j < poblacion[i].bloques.GetLength(1); j++)
                {
                    WriteLine("     Bloque " + (j) + "\n");
                    for (int k = 0; k < poblacion[i].bloques.GetLength(0); k++)
                    {
                        if (poblacion[i].bloques[k, j] != null)
                        {
                            WriteLine("            " + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                                Datos.listaProfesores[poblacion[i].getEncargado(j, poblacion[i].bloques[k, j].getCurso())].getNombre() + "\t" +
                                "\t Aula " + poblacion[i].bloques[k, j].getAula() + ": " +
                                Datos.listaCursos[j][poblacion[i].bloques[k, j].getCurso()].getNombre());
                        }
                    }
                }
            }
            ReadKey();
        }
        static void imprimirPoblacion_Profesor(List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i + 1) + "\n");
                for (int j = 0; j < poblacion[i].profesores.GetLength(1); j++)
                {
                    WriteLine(Datos.listaProfesores[j].getNombre() + "\n");
                    for (int k = 0; k < poblacion[i].profesores.GetLength(0); k++)
                    {
                        if (poblacion[i].profesores[k, j] != null)
                        {
                            WriteLine("            " + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                                "\t Bloque " + poblacion[i].profesores[k, j].getBloque() +
                                "\t Aula " + poblacion[i].profesores[k, j].getAula() + ": " +
                                Datos.listaCursos[poblacion[i].profesores[k, j].getBloque()][poblacion[i].profesores[k, j].getCurso()].getNombre());
                        }
                    }
                }
            }
            ReadKey();
        }
        static void imprimirPoblacion_Todo(List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + (i + 1) + "\n");

                for (int j = 0; j < poblacion[i].aulas.GetLength(1); j++)
                {
                    WriteLine("\n--Aula " + (j));
                    for (int k = 0; k < poblacion[i].aulas.GetLength(0); k++)
                    {
                        if (poblacion[i].aulas[k, j] != null)
                        {
                            WriteLine("----" + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                                Datos.listaProfesores[poblacion[i].getEncargado(poblacion[i].aulas[k, j].getBloque(), poblacion[i].aulas[k, j].getCurso())].getNombre() + "\t" +
                                "\t Bloque " + poblacion[i].aulas[k, j].getBloque() + ": " +
                                Datos.listaCursos[poblacion[i].aulas[k, j].getBloque()][poblacion[i].aulas[k, j].getCurso()].getNombre());
                        }
                    }
                }
                for (int j = 0; j < poblacion[i].bloques.GetLength(1); j++)
                {
                    WriteLine("\n--Bloque " + (j));
                    for (int k = 0; k < poblacion[i].bloques.GetLength(0); k++)
                    {
                        if (poblacion[i].bloques[k, j] != null)
                        {
                            WriteLine("----" + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                                Datos.listaProfesores[poblacion[i].getEncargado(j, poblacion[i].bloques[k, j].getCurso())].getNombre() + "\t" +
                                "\t Aula " + poblacion[i].bloques[k, j].getAula() + ": " +
                                Datos.listaCursos[j][poblacion[i].bloques[k, j].getCurso()].getNombre());
                        }
                    }
                }
                for (int j = 0; j < poblacion[i].profesores.GetLength(1); j++)
                {
                    WriteLine("\n--" + Datos.listaProfesores[j].getNombre());
                    for (int k = 0; k < poblacion[i].profesores.GetLength(0); k++)
                    {
                        if (poblacion[i].profesores[k, j] != null)
                        {
                            WriteLine("----" + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                                "\t Bloque " + poblacion[i].profesores[k, j].getBloque() +
                                "\t Aula " + poblacion[i].profesores[k, j].getAula() + ": " +
                                Datos.listaCursos[poblacion[i].profesores[k, j].getBloque()][poblacion[i].profesores[k, j].getCurso()].getNombre());
                        }
                    }
                }
                ReadKey();
            }
            ReadKey();
        }
        static string AMPM(int hora)
        {
            if (hora <= 12)
            {
                if (hora == 12)
                    return (hora + ":00 PM ");
                else
                {
                    if (hora < 10)
                        return (" " + hora + ":00 AM ");
                    else
                        return (hora + ":00 AM ");
                }
            }
            else
                return (" " + hora % 12 + ":00 PM ");
        }
    }
}
