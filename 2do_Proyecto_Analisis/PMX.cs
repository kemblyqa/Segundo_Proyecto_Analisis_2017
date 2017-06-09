using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    static class PMX
    {
        static int mutaciones,fallos = 0;
        public static bool distintos(Horario a,Horario b)
        {
            int diferencias = 0;
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if ((a.getLeccion(i, j) != null ? a.getLeccion(i, j).getCurso() : -1) != 
                        (b.getLeccion(i, j) != null ? b.getLeccion(i, j).getCurso() : -1))
                    {
                        diferencias++;
                        if (diferencias > 5)
                            return true;
                    }
                }
            }
            return false;
        }
        public static void nuevaGeneracion()
        {
            Console.WriteLine(mutaciones+" mutaciones \n"+fallos+" camadas fallidas");
            Datos.listaHorariosPadres = Datos.listaHorariosHijos;
            Datos.listaHorariosHijos = new List<Horario>();
            List<int[]> fitness = new List<int[]>();
            for (int i = 0; i < Datos.listaHorariosPadres.Count; i++)
            {
                int[] aux = new int[2];
                aux[0] = i;
                aux[1] = Datos.listaHorariosPadres[i].fitness();
                if (i == 0)
                    fitness.Add(aux);
                else
                    for (int j = 0; j <= fitness.Count; j++)
                    {
                        if (j == fitness.Count)
                        {
                            fitness.Add(aux);
                            break;
                        }
                        if (fitness[j][1] > aux[1])
                        {
                            fitness.Insert(j, aux);
                            break;
                        }
                    }
            }
            int padre, madre=0;
            while (Datos.listaHorariosHijos.Count < Datos.listaHorariosPadres.Count)
            {
                madre = Datos.randy.Next(0,(Datos.individuosporgeneracion/5)*2);
                padre = Datos.randy.Next(0, Datos.listaHorariosHijos.Count);

                List<Horario> camada = cruce_Bloques(padre,madre);
                if (camada[0] != null)
                {
                    if (Datos.listaHorariosHijos.Count == 0)
                    {
                        Datos.listaHorariosHijos.Add(camada[0]);
                    }
                    else
                    {
                        bool diferente = true;
                        for (int i = 0; i < Datos.listaHorariosHijos.Count; i++)
                        {
                            if (!distintos(camada[0], Datos.listaHorariosHijos[i]))
                            {
                                diferente = false;
                            }
                        }
                        if (diferente)
                        {
                            Datos.listaHorariosHijos.Add(camada[0]);
                        }
                    }
                }
                if (camada[1] != null && Datos.listaHorariosHijos.Count < Datos.individuosporgeneracion)
                {
                    if (Datos.listaHorariosHijos.Count == 0)
                    {
                        Datos.listaHorariosHijos.Add(camada[1]);
                    }
                    else
                    {
                        bool diferente = true;
                        for (int i = 0; i < Datos.listaHorariosHijos.Count; i++)
                        {
                            if (!distintos(camada[1], Datos.listaHorariosHijos[i]))
                            {
                                diferente = false;
                            }
                        }
                        if (diferente)
                        {
                            Datos.listaHorariosHijos.Add(camada[1]);
                        }
                    }
                }
            }
            for (int i = 0; i < Datos.listaHorariosPadres.Count/3; i++)
            {
                for (int j = 0; j < Datos.listaHorariosPadres.Count; j++)
                {
                    if (Datos.listaHorariosPadres[i].fitness() < Datos.listaHorariosHijos[j].fitness())
                    {
                        Datos.listaHorariosHijos[j] = Datos.listaHorariosPadres[i];
                        i++;
                        break;
                    }
                }
            }
        }
        public static List<Horario> cruce(int padre, int madre)
        {

            return null;
        }

        public static List<Horario> cruce_Bloques(int padre, int madre)
        {
            List<Horario> camada = new List<Horario>();
            camada.Add(Datos.clonar(Datos.listaHorariosPadres[padre]));
            camada.Add(Datos.clonar(Datos.listaHorariosPadres[madre]));
            int[] intervalo = new int[2];
            intervalo[0] = Datos.randy.Next(0, 30);
            intervalo[1] = Datos.randy.Next(intervalo[0] +20, (intervalo[0] + 20 > 50 ? 50 : intervalo[0] + 20));
            int bloque = Datos.randy.Next(0, Datos.listaCursos.Count);
            string text = ("\nBloque " + bloque + ", " + intervalo[0].ToString() + "-" + intervalo[1] + "\n");
            Leccion[,] pila = new Leccion[2, (intervalo[1] - intervalo[0] + 1)];
            List<int[]> mapeo = new List<int[]>();
            Leccion aux;
            for (int i = intervalo[0]; i <= intervalo[1]; i++)
            {
                aux = camada[0].popLeccion(bloque, i);
                if (!camada[0].insertarPMX(i, bloque, camada[1].popLeccion(bloque, i), intervalo[0], intervalo[1]) ||
                    !camada[1].insertarPMX(i, bloque, aux, intervalo[0], intervalo[1]))
                {
                    fallos++;
                    return new List<Horario>() { null, null };
                }
                int[] tupla = new int[2];

                aux = camada[0].getLeccion(bloque, i);
                tupla[0] = aux != null ? aux.getCurso() : -1;

                aux = camada[1].getLeccion(bloque, i);
                tupla[1] = aux != null ? aux.getCurso() : -1;
                mapeo = insertarMapeo(mapeo, tupla);
            }

            pila = new Leccion[2, mapeo.Count];
            int[,] horas = new int[mapeo.Count, 2];
            for (int h = 0; h < mapeo.Count; h++)
            {
                horas[h, 0] = -1;
                horas[h, 1] = -1;
                int rep = 30;
                while ((horas[h, 0] == -1 || horas[h,1]==-1) && rep!=0)
                {
                    for (int k = 0; k < 50; k++)
                    {
                        if (k == intervalo[0])
                        {
                            k = intervalo[1] + 1;
                            continue;
                        }
                        
                    }
                    rep--;
                }
                if (rep == 0)
                {
                    fallos++;
                    return new List<Horario>() { null, null };

                }
            }
            //mutacion
            for (int i = 0; i < 2; i++)
            {
                int x = Datos.randy.Next(0, pila.GetLength(1)/3==0?Datos.randy.Next(0,2): (pila.GetLength(1) / 3)+1);
                for (int j = 0; j < x; j++)
                {
                    mutaciones++;
                    int a, b;
                    a = Datos.randy.Next(0, pila.GetLength(1));
                    b = Datos.randy.Next(0, pila.GetLength(1));
                    aux = pila[i, a];
                    pila[i, a] = pila[i, b];
                    pila[i, b] = aux;
                }
            }
            //mutacion
            for (int i = 0; i < pila.GetLength(1); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if(!camada[j].insertarPMX(horas[i, j], bloque, pila[j, i], intervalo[0], intervalo[1]))
                    {
                        return new List<Horario>() { null, null };
                    }
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (camada[i].apto() != null)
                {
                    camada[i] = null;
                }
            }
            if (camada[0] == null && camada[1] == null)
                fallos++;
            return camada;
        }
        public static List<int[]> insertarMapeo(List<int[]> mapeo,int[] nuevo)
        {
            if (nuevo[0]==nuevo[1])
                return mapeo;
            for (int i = 0; i < mapeo.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (nuevo[(j + 1) % 2] == mapeo[i][j])
                    {
                        nuevo[(j + 1) % 2] = mapeo[i][(j + 1) % 2];
                        mapeo.RemoveAt(i);
                        return insertarMapeo(mapeo, nuevo);
                    }
                }
            }
            mapeo.Add(nuevo);
            return mapeo;
        }
    }
}
