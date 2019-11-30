using System;
using System.Collections.Generic;

namespace juegoIA
{
    public class Cola
	{		
		private List<ArbolGeneral> colaArbol = new List<ArbolGeneral>();
	
		public void encolar(ArbolGeneral elem) {
			this.colaArbol.Add(elem);
		}
	
		public ArbolGeneral desencolar() {
            ArbolGeneral temp = this.colaArbol[0];
			this.colaArbol.RemoveAt(0);
			return temp;
		}
		
		public ArbolGeneral tope() {
			return this.colaArbol[0]; 
		}
		
        public bool esVacia() {
			return this.colaArbol.Count == 0;
		}

    }
}
