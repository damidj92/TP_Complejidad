using System;
using System.Collections.Generic;

namespace juegoIA
{
    /// <summary>
    /// Description of NodoGeneral.
    /// </summary>
    public class NodoGeneral
    {
        private int numCarta;
        private int limite;
        private float ponderacion;
        private List<NodoGeneral> hijos;

        public NodoGeneral(int carta, int lim)
        {
            this.numCarta = carta;
            this.limite = lim;
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
