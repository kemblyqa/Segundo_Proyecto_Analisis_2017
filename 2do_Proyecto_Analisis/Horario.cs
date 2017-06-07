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
        private List<List<int[]>> datocurso;

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
            int aula = Datos.randy.Next(0, Datos.listaAulas.Count);
            int hora = Datos.randy.Next(0, 50);
            while (this.aulas[hora, aula] != null || this.bloques[hora, bloque] != null || this.profesores[hora, datocurso[bloque][curso][0]] != null)
            {
                aula = Datos.randy.Next(0, Datos.listaAulas.Count);
                hora = Datos.randy.Next(0, 50);
            }
            this.aulas[hora, aula] = new Leccion(aula, bloque, curso);
            this.bloques[hora, bloque] = new Leccion(aula, bloque, curso);
            this.profesores[hora, datocurso[bloque][curso][0]] = new Leccion(aula, bloque, curso);
        }
        public void insertarCurso(Leccion x)
        {
            for (int y = 0; y < 150; y++)
            {
                int i = Datos.randy.Next(0, 50);
                if (this.aulas[i, x.getAula()] == null && this.bloques[i, x.getBloque()] == null && this.profesores[i, this.getEncargado(x)] == null)
                {
                    this.aulas[i, x.getAula()] = x;
                    this.bloques[i, x.getBloque()] = x;
                    this.profesores[i, this.getEncargado(x)] = x;
                    this.datocurso[x.getBloque()][x.getCurso()][1]++;
                    return;
                }
            }
            for (int j = 0; j < Datos.listaAulas.Count; j++)
            {
                for (int y = 0; y < 150; y++)
                {
                    int i = Datos.randy.Next(0, 50);
                    if (this.aulas[i, j] == null && this.bloques[i, x.getBloque()] == null && this.profesores[i, this.getEncargado(x)] == null)
                    {
                        x.setAutla(j);
                        this.aulas[i, x.getAula()] = x;
                        this.bloques[i, x.getBloque()] = x;
                        this.profesores[i, this.getEncargado(x)] = x;
                        this.datocurso[x.getBloque()][x.getCurso()][1]++;
                        return;
                    }
                }
            }
            Console.WriteLine("No se ha podido hallar un espacio disponible para la insercion segura");
            Console.ReadKey();
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
        public void insertarPMX(int hora, int bloque, Leccion x, int inicio, int fin)
        {
            Leccion aux;
            if (x == null)
            {
                if (this.bloques[hora, bloque] != null)
                {
                    aux = popLeccion(bloque,hora);
                    insertarCurso(aux);
                }
            }
            else
            {
                while (!validar_Campo(hora, bloque, x))
                {
                    aux = this.bloques[hora, bloque];
                    if (aux != null)
                    {
                        aux = popLeccion(aux.getBloque(), hora);
                        insertarCurso(aux);
                    }
                    aux = this.aulas[hora, x.getAula()];
                    if (aux != null)
                    {
                        aux = popLeccion(aux.getBloque(), hora);
                        insertarCurso(aux);
                    }
                    aux = this.profesores[hora, this.getEncargado(x)];
                    if (aux != null)
                    {
                        aux = this.popLeccion(aux.getBloque(), hora);
                        insertarCurso(aux);
                    }
                }
                this.setLeccion(hora, x.getBloque(), x, false);
            }
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
        public bool validar_Campo(int hora, int bloque, Leccion nueva)
        {
            if (nueva == null)
            {
                if (this.bloques[hora, bloque] == null)
                    return true;
                else
                    return false;
            }
            if (this.aulas[hora, nueva.getAula()] != null || this.bloques[hora, nueva.getBloque()] != null || this.profesores[hora, this.datocurso[nueva.getBloque()][nueva.getCurso()][0]] != null)
            {
                return false;
            }
            return true;
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
                    if (diferencia!=0)
                    {
                        fallos += 1;
                    }
                }
            }
            return fallos;
        }
        public int[] apto()
        {
            for (int i = 0; i < this.datocurso.Count; i++)
            {
                for (int j = 0; j < this.datocurso[i].Count; j++)
                {
                    if (this.datocurso[i][j][1] != 0)
                        return new int[2] { i,j};
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
