using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    static class PMX
    {
        static string texto;
        public static bool distintos(Horario a,Horario b)
        {
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if ((a.getLeccion(i, j) != null ? a.getLeccion(i, j).getCurso() : -1) != 
                        (b.getLeccion(i, j) != null ? b.getLeccion(i, j).getCurso() : -1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static void nuevaGeneracion()
        {
            texto = "";
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
            Console.WriteLine(fitness[0][1]);
            int padre, madre=0;
            while (Datos.listaHorariosHijos.Count < 10)
            {
                madre = Datos.randy.Next(0,4);
                padre = Datos.randy.Next(0, Datos.listaHorariosHijos.Count);
                List<Horario> camada = cruce_Bloques(padre,madre);
                if (camada[0] != null)
                {
                    if (Datos.listaHorariosHijos.Count == 0)
                    {
                        Datos.listaHorariosHijos.Add(camada[0]);
                        Console.WriteLine("Hijo agregado");
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
                            Console.WriteLine("Hijo agregado");
                        }
                    }
                }
                if (camada[1] != null && Datos.listaHorariosHijos.Count < 10)
                {
                    if (Datos.listaHorariosHijos.Count == 0)
                    {
                        Datos.listaHorariosHijos.Add(camada[1]);
                        Console.WriteLine("Hijo agregado");
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
                            Console.WriteLine("Hijo agregado");
                        }
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Datos.listaHorariosPadres[i].fitness() < Datos.listaHorariosHijos[j].fitness())
                    {
                        Datos.listaHorariosHijos[j] = Datos.listaHorariosPadres[i];
                        i++;
                        break;
                    }
                }
            }
            Console.WriteLine(texto);
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
                camada[0].insertarPMX(i, bloque, camada[1].popLeccion(bloque, i), intervalo[0], intervalo[1]);
                camada[1].insertarPMX(i, bloque, aux, intervalo[0], intervalo[1]);
                int[] tupla = new int[2];

                aux = camada[0].getLeccion(bloque, i);
                tupla[0] = aux != null ? aux.getCurso() : -1;

                aux = camada[1].getLeccion(bloque, i);
                tupla[1] = aux != null ? aux.getCurso() : -1;
                mapeo = insertarMapeo(mapeo, tupla);
            }
            if (mapeo.Count == 0)
            {
                if (Datos.randy.Next(0, 10) == 0)
                {
                    intervalo[0] = Datos.randy.Next(intervalo[0], intervalo[1]);
                    intervalo[1] = Datos.randy.Next(intervalo[0], intervalo[1]);

                    int[] tupla = new int[2];
                    aux = camada[0].popLeccion(bloque, intervalo[0]);
                    tupla[0] = camada[1].getLeccion(bloque, intervalo[0])!=null? camada[1].getLeccion(bloque, intervalo[0]).getCurso():-1;
                    camada[0].insertarPMX(intervalo[0], bloque, camada[1].popLeccion(bloque, intervalo[1]), intervalo[0], intervalo[1]);
                    tupla[1] = aux != null ? aux.getCurso() : -1;
                    camada[1].insertarPMX(intervalo[1], bloque, aux, intervalo[0], intervalo[1]);

                    mapeo = insertarMapeo(mapeo, tupla);
                    if (mapeo.Count == 0)
                    {
                        return new List<Horario>() { null, null };
                    }
                }
                else
                    return new List<Horario>() { null, null };
            }
            for (int i = 0; i < mapeo.Count; i++)
            {
                text += mapeo[i][0] + "\t - \t" + mapeo[i][1] + "\n";
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
                    int x = Datos.randy.Next(0, 2);
                    for (int k = 0; k < 50; k++)
                    {
                        if (k == intervalo[0])
                        {
                            k = intervalo[1] + 1;
                            continue;
                        }
                        for (int m = 0; m < 2; m++)
                        {
                            if (mapeo[h][m] == -1)
                            {
                                if (camada[m].getLeccion(bloque, k) == null && horas[h,m]==-1)
                                {
                                    if (x == 0)
                                    {
                                        x = Datos.randy.Next(0, 2);
                                        pila[(m + 1) % 2, h] = null;
                                        horas[h, m] = k;
                                    }
                                    else
                                    {
                                        x = Datos.randy.Next(0, 2);
                                    }
                                }
                            }
                            else
                            {
                                if (horas[h,m]==-1&&camada[m].getLeccion(bloque, k) != null && camada[m].getLeccion(bloque, k).getCurso() == mapeo[h][m])
                                {
                                    if (x == 0)
                                    {
                                        x = Datos.randy.Next(0, 2);
                                        aux = camada[m].popLeccion(bloque, k); ;
                                        pila[(m + 1) % 2, h] = aux;
                                        horas[h, m] = k;
                                    }
                                    else
                                    {
                                        x = Datos.randy.Next(0, 2);
                                    }
                                }
                            }
                        }
                    }
                    rep--;
                }
                if (rep == 0)
                    return new List<Horario>() { null, null };
            }
            //mutacion
            for (int i = 0; i < 2; i++)
            {
                int x = Datos.randy.Next(0, pila.GetLength(1)/3==0?2: (pila.GetLength(1) / 3)+1);
                text += "al menos " + x + " mutaciones";
                for (int j = 0; j < x; j++)
                {
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
                    camada[j].insertarPMX(horas[i, j], bloque, pila[j, i],intervalo[0],intervalo[1]);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (camada[i].apto() != null)
                {
                    Console.WriteLine("Perdida de consistencia");
                    Console.WriteLine(camada[i].apto()[0] + ", " + camada[i].apto()[1]);
                    camada[i] = null;
                }
            }
            if (camada[0] != null || camada[1] != null)
                texto += text;
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
