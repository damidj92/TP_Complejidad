using System;
using System.Collections.Generic;

namespace juegoIA
{
    public class NodoGeneral
    {
        private int numCarta; // Numero de carta
        private int limite; // Guarda el límite llegado a ese nodo
        private bool turno; // True para Human, False para Computer
        private float ponderacion;  // Si es hoja, es igual a 1 si gana Computer y 0 si gana Human
                                    // Si tiene hijos, sumo las ponderaciones de los hijos y divido por la cantidad de hijos
        private List<NodoGeneral> hijos;

        public NodoGeneral(int carta, int lim, bool unTurno)
        {
            this.numCarta = carta;
            this.limite = lim;
            this.turno = unTurno;
            this.ponderacion = 0;
            this.hijos = new List<NodoGeneral>();
        }

        public int getNumCarta()
        {
            return this.numCarta;
        }

        public int getLimite()
        {
            return this.limite;
        }

        public bool getTurno()
        {
            return this.turno;
        }

        public float getPonderacion()
        {
            return this.ponderacion;
        }

        public List<NodoGeneral> getHijos()
        {
            return this.hijos;
        }

        public void setNumCarta(int dato)
        {
            this.numCarta = dato;
        }

        public void setLimite (int dato)
        {
            this.limite = dato;
        }

        public void setTurno(bool unTurno)
        {
            this.turno = unTurno;
        }

        public void setPonderacion(float dato)
        {
            this.ponderacion = dato;
        }
        public void setHijos(List<NodoGeneral> hijos)
        {
            this.hijos = hijos;
        }

    }
}
