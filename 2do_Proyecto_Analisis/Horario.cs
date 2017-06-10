using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

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

        private Leccion[,] aulas;
        private Leccion[,] bloques;
        private Leccion[,] profesores;
        public List<List<int[]>> datocurso;
        public bool padre = false;
        public Horario()
        {
            this.aulas = new Leccion[50, Datos.listaAulas.Count];
            this.bloques = new Leccion[50, Datos.listaCursos.Count];
            this.profesores = new Leccion[50, Datos.listaProfesores.Count];
            this.datocurso = new List<List<int[]>>();
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                this.datocurso.Add(new List<int[]>());
                for (int j = 0; j < Datos.listaCursos[i].Count; j++)
                {
                    this.datocurso[i].Add(new int[2] { Datos.profesorDeClase(i, j), 0 });
                }
            }
        }
        public void insertarCurso(int bloque, int curso)
        {
            int hora = Datos.randy.Next(0, 50);
            Leccion nueva = new Leccion(Datos.randy.Next(0, Datos.listaAulas.Count), bloque,curso);
            while (!validar_Campo(hora,bloque,nueva))
            {
                nueva.setAula(Datos.randy.Next(0, Datos.listaAulas.Count));
                hora = Datos.randy.Next(0, 50);
            }
            this.aulas[hora, nueva.getAula()] = nueva;
            this.bloques[hora, bloque] = nueva;
            this.profesores[hora, datocurso[bloque][curso][0]] = nueva;
        }
        public void insertarCurso(Leccion x)
        {
            for (int y = 0; y < 150; y++)
            {
                int i = Datos.randy.Next(0, 50);
                if (validar_Campo(i,x.getBloque(),x))
                {
                    this.setLeccion(i,x.getBloque(),x,false);
                    return;
                }
            }
            for (int j = 0; j < Datos.listaAulas.Count; j++)
            {
                for (int y = 0; y < 150; y++)
                {
                    int i = Datos.randy.Next(0, 50);
                    x.setAula(j);
                    if (validar_Campo(i,x.getBloque(),x))
                    {
                        this.setLeccion(i, x.getBloque(), x, false);
                        return;
                    }
                }
            }
            Console.WriteLine("No se ha podido hallar un espacio disponible para la insercion segura");
            Console.ReadKey();
        }
        public Leccion getLeccion(int bloque,int hora)
        {
            return this.bloques[hora, bloque];
        }
        
        public bool setLeccion(int hora,int bloque,Leccion valor,bool clonado)
        {
            if (validar_Campo(hora, bloque, valor))
            {
                if (valor != null)
                {
                    this.bloques[hora, valor.getBloque()] = valor;
                    this.aulas[hora, valor.getAula()] = valor;
                    this.profesores[hora, this.datocurso[bloque][valor.getCurso()][0]] = valor;
                    if (!clonado)
                        this.datocurso[bloque][valor.getCurso()][1]++;
                }
                return true;
            }
            return false;
        }
        public bool insertarFuerte(int hora, int bloque, Leccion x, int inicio, int fin)
        {
            Leccion aux;
            int auxh;
            if (x == null)
            {
                if (!validar_bloque(hora, bloque))
                {
                    aux = getLeccion_bloque(hora, bloque);
                    auxh = getHoraDisponible(bloque, aux, inicio, fin);
                    popLeccion(bloque, hora);
                    if (auxh == -1)
                        return false;
                    if (!setLeccion(auxh, bloque, aux, false))
                    {

                    }
                }
            }
            else
            {
                if (!validar_Campo(hora, bloque, x))
                {
                    if (!Datos.listaProfesores[getEncargado(x)].horavalida(hora) || !Datos.listaAulas[x.getAula()].horavalida(hora))
                        return false;
                    else
                    {
                        if (!validar_bloque(hora, bloque))
                        {
                            aux = getLeccion_bloque(hora, bloque);
                            auxh = getHoraDisponible(bloque, aux, inicio, fin);
                            if (auxh == -1)
                                return false;
                            popLeccion(bloque, hora);
                            if (!setLeccion(auxh, bloque, aux, false))
                            {

                            }
                        }

                        if (!validar_profesor(hora, x.getBloque(), x.getCurso()))
                        {
                            aux = getLeccion_bloque(hora, getLeccion_profesor(hora, getEncargado(x)).getBloque());
                            auxh = getHoraDisponible(aux.getBloque(), aux, inicio, fin);
                            if (auxh == -1)
                                return false;
                            popLeccion(aux.getBloque(), hora);
                            if (!setLeccion(auxh, aux.getBloque(), aux, false))
                            {

                            }
                        }

                        if (!validar_aula(hora, x.getAula()))
                        {
                            aux = getLeccion_bloque(hora, getLeccion_aula(hora, x.getAula()).getBloque());
                            auxh = getHoraDisponible(aux.getBloque(), aux, inicio, fin);
                            if (auxh == -1)
                                return false;
                            popLeccion(aux.getBloque(), hora);
                            if (!setLeccion(auxh, aux.getBloque(), aux, false))
                            {

                            }
                        }
                        if (!setLeccion(hora, bloque, x, false))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    setLeccion(hora, bloque, x, false);
                }
            }
            return true;
        }
        public Leccion popLeccion(int bloque, int hora)
        {
            Leccion valor = this.bloques[hora, bloque];
            if (valor != null)
            {
                this.bloques[hora, bloque] = null;
                this.aulas[hora, valor.getAula()] = null;
                this.profesores[hora, this.getEncargado(valor)] = null;
                this.datocurso[bloque][valor.getCurso()][1]--;
            }
            return valor;
        }
        public int getCurso_Bloque(int hora, int bloque)
        {
            if (this.bloques[hora, bloque] == null)
                return -1;
            else
                return this.bloques[hora, bloque].getCurso();
        }
        public Leccion getLeccion_bloque(int hora, int bloque)
        {
            return this.bloques[hora, bloque];
        }
        public Leccion getLeccion_aula(int hora, int aula)
        {
            return this.aulas[hora, aula];
        }
        public Leccion getLeccion_profesor(int hora, int encargado)
        {
            return this.profesores[hora, encargado];
        }

        public int getAula(int hora,int bloque)
        {
            return this.bloques[hora, bloque].getAula();
        }
        
        public int getEncargado(int bloque,int curso)
        {
            return this.datocurso[bloque][curso][0];
        }
        public int getEncargado(Leccion x)
        {
            if (x == null)
                return -1;
            return this.datocurso[x.getBloque()][x.getCurso()][0];
        }
        public void setEncargado(int bloque, int curso, int encargado)
        {
            this.datocurso[bloque][curso][0] = encargado;
        }
        public int leccionesDentro()
        {
            int contador=0;
            for (int i = 0; i < Datos.listaCursos.Count; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (this.getLeccion_bloque(j,i)!=null)
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }
        public bool validar_Campo(int hora, int bloque, Leccion nueva)
        {

            if (nueva == null)
            {
                return validar_bloque(hora, bloque);
            }
            if(nueva.getAula()==1 && 9>=hora && hora >= 5)
            {

            }
                
            if (validar_bloque(hora, bloque) && validar_aula(hora, nueva.getAula()) && validar_profesor(hora, bloque, nueva.getCurso()))
            {
                return true;
            }
            return false;
        }
        public bool validar_bloque(int hora, int bloque)
        {
            return (this.bloques[hora, bloque] == null);
        }
        public bool validar_aula(int hora, int aula)
        {
            return (this.aulas[hora, aula] == null && Datos.listaAulas[aula].horavalida(hora));
        }
        public bool validar_profesor(int hora,int bloque, int curso)
        {
            return (this.profesores[hora, this.getEncargado(bloque, curso)] == null && Datos.listaProfesores[this.getEncargado(bloque, curso)].horavalida(hora));
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
                        if (this.bloques[k, i] != null && (this.bloques[k, i].getBloque() == i && this.bloques[k, i].getCurso() == j))
                        {
                            clases++;
                            while (k != 50 && this.bloques[k, i] != null && (this.bloques[k, i].getBloque() == i && this.bloques[k, i].getCurso() == j))
                            {
                                if ((k+1) % 10 == 0)
                                    break;
                                k++;
                            }
                        }
                    }
                    int diferencia = clases - Datos.listaCursos[i][j].getClases();
                    if (diferencia != 0)
                    {
                        fallos +=Math.Abs(diferencia);
                    }
                }
            }
            return fallos;
        }
        public int getHoraDisponible(int bloque, Leccion x,int inicio,int fin)
        {
            if (x==null)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (i<=fin && i >= inicio)
                    {
                        i = fin + 1;
                        continue;
                    }
                    if (this.getLeccion_bloque(i, bloque) == null)
                        return i;
                }
                return -1;
            }
            else
            {
                for (int i = 0; i < 50; i++)
                {
                    if (i <= fin && i >= inicio)
                    {
                        i = fin + 1;
                        continue;
                    }
                    if (validar_Campo(i, bloque, x))
                        return i;
                }
                return -1;
            }
        }
        public int[] apto()
        {
            for (int i = 0; i < this.datocurso.Count; i++)
            {
                for (int j = 0; j < this.datocurso[i].Count; j++)
                {
                    if (this.datocurso[i][j][1] != 0)
                        return new int[2] {i,j};
                }
            }
            return null;
        }
        public void imprimir_Aulas()
        {
                for (int j = 0; j < this.aulas.GetLength(1); j++)
                {
                    WriteLine("     Aula " + (j) + "\n");
                    for (int k = 0; k < this.aulas.GetLength(0); k++)
                    {
                        if (this.aulas[k, j] != null)
                        {
                            WriteLine("            " + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                                Datos.listaProfesores[this.getEncargado(this.aulas[k, j].getBloque(), this.aulas[k, j].getCurso())].getNombre() + "\t" +
                                "\t Bloque " + this.aulas[k, j].getBloque() + ": " +
                                Datos.listaCursos[this.aulas[k, j].getBloque()][this.aulas[k, j].getCurso()].getNombre());
                        }
                    }
            }
            ReadKey();
        }
        public void imprimir_Bloques()
        {
            for (int j = 0; j < this.bloques.GetLength(1); j++)
            {
                WriteLine("     Bloque " + (j) + "\n");
                for (int k = 0; k < this.bloques.GetLength(0); k++)
                {
                    if (this.bloques[k, j] != null)
                    {
                        WriteLine("            " + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                            Datos.listaProfesores[this.getEncargado(j, this.bloques[k, j].getCurso())].getNombre() + "\t" +
                            "\t Aula " + this.bloques[k, j].getAula() + ": " +
                            Datos.listaCursos[j][this.bloques[k, j].getCurso()].getNombre());
                    }
                }
            }
            WriteLine(this.fitness());
            ReadKey();
        }
        public void imprimir_Profesor()
        {
            for (int j = 0; j < this.profesores.GetLength(1); j++)
            {
                WriteLine(Datos.listaProfesores[j].getNombre() + "\n");
                for (int k = 0; k < this.profesores.GetLength(0); k++)
                {
                    if (this.profesores[k, j] != null)
                    {
                        WriteLine("            " + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                            "\t Bloque " + this.profesores[k, j].getBloque() +
                            "\t Aula " + this.profesores[k, j].getAula() + ": " +
                            Datos.listaCursos[this.profesores[k, j].getBloque()][this.profesores[k, j].getCurso()].getNombre());
                    }
                }
            }
            ReadKey();
        }
        public void imprimir_Todo()
        {
            for (int j = 0; j < this.aulas.GetLength(1); j++)
            {
                WriteLine("\n--Aula " + (j));
                for (int k = 0; k < this.aulas.GetLength(0); k++)
                {
                    if (this.aulas[k, j] != null)
                    {
                        WriteLine("----" + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                            Datos.listaProfesores[this.getEncargado(this.aulas[k, j].getBloque(), this.aulas[k, j].getCurso())].getNombre() + "\t" +
                            "\t Bloque " + this.aulas[k, j].getBloque() + ": " +
                            Datos.listaCursos[this.aulas[k, j].getBloque()][this.aulas[k, j].getCurso()].getNombre());
                    }
                }
            }
            for (int j = 0; j < this.bloques.GetLength(1); j++)
            {
                WriteLine("\n--Bloque " + (j));
                for (int k = 0; k < this.bloques.GetLength(0); k++)
                {
                    if (this.bloques[k, j] != null)
                    {
                        WriteLine("----" + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                            Datos.listaProfesores[this.getEncargado(j, this.bloques[k, j].getCurso())].getNombre() + "\t" +
                            "\t Aula " + this.bloques[k, j].getAula() + ": " +
                            Datos.listaCursos[j][this.bloques[k, j].getCurso()].getNombre());
                    }
                }
            }
            for (int j = 0; j < this.profesores.GetLength(1); j++)
            {
                WriteLine("\n--" + Datos.listaProfesores[j].getNombre());
                for (int k = 0; k < this.profesores.GetLength(0); k++)
                {
                    if (this.profesores[k, j] != null)
                    {
                        WriteLine("----" + Datos.dias[k / 10] + "\t" + AMPM((k % 10) + 7) + "\t" +
                            "\t Bloque " + this.profesores[k, j].getBloque() +
                            "\t Aula " + this.profesores[k, j].getAula() + ": " +
                            Datos.listaCursos[this.profesores[k, j].getBloque()][this.profesores[k, j].getCurso()].getNombre());
                    }
                }
            }
            ReadKey();
        }
        public void imprimirBloquesShort()
        {
            for (int i = 0; i < bloques.GetLength(1); i++)
            {
                Console.WriteLine("Bloque " + i + "\tL\tK\tM\tJ\tV");
                for (int j = 0; j < 10; j++)
                {
                    WriteLine(AMPM(j + 7)
                        + "\t" + (bloques[j, i] != null ? bloques[j, i].getCurso().ToString() + "," + Datos.listaAulas[bloques[j, i].getAula()].getNombre() : "-")
                        + "\t" + (bloques[j + 10, i] != null ? bloques[j + 10, i].getCurso().ToString() + "," + Datos.listaAulas[bloques[j + 10, i].getAula()].getNombre() : "-")
                        + "\t" + (bloques[j + 20, i] != null ? bloques[j + 20, i].getCurso().ToString() + "," + Datos.listaAulas[bloques[j + 20, i].getAula()].getNombre() : "-")
                        + "\t" + (bloques[j + 30, i] != null ? bloques[j + 30, i].getCurso().ToString() + "," + Datos.listaAulas[bloques[j + 30, i].getAula()].getNombre() : "-")
                        + "\t" + (bloques[j + 40, i] != null ? bloques[j + 40, i].getCurso().ToString() + "," + Datos.listaAulas[bloques[j + 40, i].getAula()].getNombre() : "-"));
                }
                Console.WriteLine();
            }
        }
        public string AMPM(int hora)
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
