﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2do_Proyecto_Analisis
{
    class Leccion
    {
        int idAula;
        int idBloque;
        int idCurso;
        public Leccion(int aula,int bloque, int curso)
        {
            this.idAula = aula;
            this.idBloque = bloque;
            this.idCurso = curso;
        }
        public int getAula()
        {
            return this.idAula;
        }
        public int getBloque()
        {
            return this.idBloque;
        }
        public int getCurso()
        {
            return this.idCurso;
        }
        public void setAula(int idAula)
        {
            this.idAula = idAula;
        }
        public void setBloque(int idBloque)
        {
            this.idBloque = idBloque;
        }
        public void setCurso(int idCurso)
        {
            this.idCurso = idCurso;
        }
    }
}
