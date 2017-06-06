using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    static class PMX
    {
        public static void nuevaGeneracion()
        {
            Datos.listaHorariosPadres = Datos.listaHorariosHijos;
            Datos.listaHorariosHijos = new List<Horario>();
            List<int[]> fitness = new List<int[]>();
            for (int i = 0; i < Datos.listaHorariosPadres.Count; i++)
            {
                int[] aux = new int[2];
                aux[0] = i;
                aux[1] = Datos.listaHorariosPadres[i].fitness();
                for (int j = 0; j < fitness.Count+1; j++)
                {
                    if (j == fitness.Count)
                        fitness.Add(aux);
                    if (fitness[j][1] > aux[1])
                        fitness.Insert(j, aux);
                }
            }

            while (Datos.listaHorariosHijos.Count < 4)
            {
                int padre, madre;
                madre = 0;
                padre = 1 + Datos.randy.Next(0, 1);
                List<Horario> camada = cruce(padre,madre);
                if (camada == null)
                    continue;
                if (camada[0] != null)
                    Datos.listaHorariosHijos.Add(camada[0]);
                if (camada[1] != null && Datos.listaHorariosHijos.Count < 4)
                    Datos.listaHorariosHijos.Add(camada[1]);
            }
        }
        public static List<Horario> cruce(int padre, int madre)
        {

            return null;
        }
        public static List<Horario> crucePorAulas(int padre,int madre)
        {
            List<Horario> camada = new List<Horario>();
            camada.Add(Datos.clonar(Datos.listaHorariosPadres[padre]));
            camada.Add(Datos.clonar(Datos.listaHorariosPadres[madre]));
            int[] intervalo = new int[2];
            intervalo[0] = Datos.randy.Next(0, 50);
            intervalo[1] = Datos.randy.Next(intervalo[0], 50);
            int aula = Datos.randy.Next(0, Datos.listaAulas.Count);
            Leccion[,] pila = new Leccion[2, (intervalo[1] - intervalo[0] + 1)];
            List<int[]> mapeo = new List<int[]>();
            Leccion aux;
            for (int i = intervalo[0]; i <= intervalo[1]; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (camada[j].aulas[i, aula] != null)
                        pila[j, i - intervalo[0]] = camada[j].aulas[i, aula];
                    else
                    {
                        int bloque = 0;
                        for (int k = 0; k < camada[j].bloques.GetLength(1); k++)
                        {
                            if (camada[k].bloques[i, k] == null)
                            {
                                bloque = k;
                                break;
                            }
                        }
                        pila[j, i - intervalo[0]] = new Leccion(aula, bloque, -1);
                    }
                    aux = pila[j, i - intervalo[0]];
                    camada[j].bloques[i, aux.getBloque()] = null;
                    camada[j].profesores[i, camada[j].getEncargado(aux.getBloque(), aux.getCurso())] = null;
                    camada[j].aulas[i, aula] = null;
                }
            }
            for (int i = intervalo[0]; i <= intervalo[1]; i++)
            {
                int[] tupla = new int[2];
                for (int j = 0; j < 2; j++)
                {
                    aux = pila[(j + 1) % 2, i - intervalo[0]];
                    camada[j].aulas[i,aula] = aux;
                    camada[j].bloques[i, aux.getBloque()] = aux;
                    camada[j].profesores[i, camada[j].getEncargado(aux.getBloque(), aux.getCurso())]=aux;
                    tupla[j] = aux.getCurso();
                }
                mapeo = insertarMapeo(mapeo, tupla);
            }
            pila = new Leccion[2, mapeo.Count];
            int[,] horas = new int[mapeo.Count,2];
            for (int h = 0; h < mapeo.Count; h++)
            {
                for (int j = 0; j < Datos.listaAulas.Count; j++)
                {
                    for (int k = 0; k < 50; k++)
                    {
                        if (j==aula && k==intervalo[0])
                        {
                            j = intervalo[1] + 1;
                            continue;
                        }
                        for (int m = 0; m < 2; m++)
                        {
                            if (mapeo[h][m] == -1)
                            {
                                if (camada[m].aulas[k, j] == null)
                                {
                                    int bloque = -1;
                                    for (int l = 0; l < camada[m].bloques.GetLength(1); l++)
                                    {
                                        if (camada[m].bloques[k, l] == null)
                                        {
                                            bloque = k;
                                            break;
                                        }
                                    }
                                    pila[(m + 1) % 2, h] = new Leccion(j, bloque, -1);
                                    horas[h, m] = k;
                                }
                            }
                            else
                            {
                                if (camada[m].aulas[k, j].getCurso() == mapeo[h][m])
                                {
                                    aux = camada[m].aulas[k, j];
                                    pila[(m+1)%2, h] = aux;
                                    horas[h, m] = k;
                                    camada[m].aulas[k, j] = null;
                                    camada[m].bloques[k, aux.getBloque()] = null;
                                    camada[m].profesores[k, camada[m].getEncargado(aux.getBloque(), aux.getCurso())] = null;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < pila.GetLength(1); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (pila[j, i].getCurso() != -1)
                    {
                        camada[j].aulas[horas[i, j], pila[j, i].getAula()] = pila[j, i];
                        camada[j].bloques[horas[i, j], pila[j, i].getBloque()] = pila[j, i];
                        camada[j].profesores[horas[i, j], camada[j].getEncargado(pila[j, i].getBloque(), pila[j, i].getCurso())]=pila[j,i];
                    }
                }
            }
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
