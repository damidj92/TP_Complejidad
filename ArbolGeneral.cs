using System;
using System.Collections.Generic;

namespace juegoIA
{
    public class ArbolGeneral
    {

        private NodoGeneral raiz;

        public ArbolGeneral(int carta, int lim, bool unTurno)
        {
            this.raiz = new NodoGeneral(carta, lim, unTurno);
        }

        private ArbolGeneral(NodoGeneral nodo)
        {
            this.raiz = nodo;
        }

        private NodoGeneral getRaiz()
        {
            return raiz;
        }

        public int getNumCartaRaiz()
        {
            return this.getRaiz().getNumCarta();
        }

        public int getLimiteRaiz()
        {
            return this.getRaiz().getLimite();
        }

        public bool getTurnoRaiz()
        {
            return this.getRaiz().getTurno();
        }

        public float getPonderacionRaiz()
        {
            return this.getRaiz().getPonderacion();
        }

        public void setPonderacionRaiz(float dato)
        {
            this.raiz.setPonderacion(dato);
        }

        public List<ArbolGeneral> getHijos()
        {
            List<ArbolGeneral> temp = new List<ArbolGeneral>();
            foreach (var element in this.raiz.getHijos())
            {
                temp.Add(new ArbolGeneral(element));
            }
            return temp;
        }

        public void agregarHijo(ArbolGeneral hijo)
        {
            this.raiz.getHijos().Add(hijo.getRaiz());
        }

        public void eliminarHijo(ArbolGeneral hijo)
        {
            this.raiz.getHijos().Remove(hijo.getRaiz());
        }

        public bool esVacio()
        {
            return this.raiz == null;
        }

        public bool esHoja()
        {
            return this.raiz != null && this.getHijos().Count == 0;
        }

        /* public int altura() {
            return 0;
        }
    
        
        public int nivel(T dato) {
            return 0;
        } */

    }
}
