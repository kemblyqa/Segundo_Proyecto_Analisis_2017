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
        static void Main(string[] args)
        {
            datos = new Datos();
            imprimirPoblacion(Datos.listaHorariosHijos);
            ReadKey();
        }
        static void imprimirPoblacion( List<Horario> poblacion)
        {
            for (int i = 0; i < poblacion.Count; i++)
            {
                WriteLine("Horario " + i+"\n");
                for (int j = 0; j < poblacion[i].horario.GetLength(0); j++)
                {
                    WriteLine("     Aula " + j+ "\n");
                    for (int k = 0; k < poblacion[i].horario.GetLength(1); k++)
                    {
                        if (poblacion[i].horario[j, k, 0] != -1)
                        {
                            WriteLine("            " + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" + Datos.listaProfesores[poblacion[i].horario[j, k, 1]].getNombre() + "\t" + Datos.listaCursos[poblacion[i].horario[j, k, 0]].getNombre());
                        }
                    }
                }
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
