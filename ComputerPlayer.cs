using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
        // Declaro una variable minimax de tipo ArbolGeneral
        private ArbolGeneral minimax;

		public ComputerPlayer()
		{
            // Instancio la variable minimax con un nodo raíz en 0
            this.minimax = new ArbolGeneral(0,0, false);
		}
		
		public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
            // Cargar el arbol
            bool turnoOponente = true;
            this.cargarArbol(minimax, cartasPropias, cartasOponente, limite, turnoOponente);

            // Ponderar el arbol
            this.ponderarArbol(minimax);
        }
				
		public override int descartarUnaCarta()
		{
			//Implementar. La carta que va a jugar la máquina.
			return 0;
		}
		
		public override void cartaDelOponente(int carta)
		{
			//Implementar. Obtengo la carta que juega el oponente para ir moviendo la raíz.
		}

        public void cargarArbol(ArbolGeneral arbol, List<int> cartasPropias, List<int> cartasOponente, int limite, bool turno)
        {
            // Se va a llamar recursivamente hasta que no haya más cartas para agregar
            if (cartasPropias.Count != 0 & cartasOponente.Count != 0)
            {
                // Recorro cada carta del oponente, para agregarla en el nodo padre
                foreach (int carta in cartasOponente)
                {
                    // Reduzco el límite
                    limite -= carta;

                    // Creo un nodo de tipo ArbolGeneral para agregar al árbol minimax
                    ArbolGeneral nodoCarta = new ArbolGeneral(carta, limite, turno);
                    minimax.agregarHijo(nodoCarta);

                    // Una vez agregado debo sacarlo de la lista cartasOponente
                    List<int> aux = new List<int>(cartasOponente);
                    aux.Remove(carta);

                    // LLamo nuevamente al metodo 
                    // pero invirtiendo el orden de las cartasPropias por las cartasOponente(aux)
                    this.cargarArbol(minimax, aux, cartasPropias, limite, !turno);
                }
            }
        }

        public void ponderarArbol(ArbolGeneral arbol)
        {
            /*  
                Recorro el arbol hasta llegar a las hojas. El nivel de las hojas es de Computer.
                Si la hoja es ganadora (limite >= 0) la pondero con 1. Sino la pondero con 0.
                Si es un nodo padre, sumo las ponderaciones de los hijos y la divido por la cantidad de hijos  
            */

            // Caso base
            if (arbol.esHoja())
            {
                // Llego a la última carta y no supero el límite
                if (arbol.getLimiteRaiz() >= 0)
                    arbol.setPonderacionRaiz(1);
                // Llego a la útima carta y supero el límite
                else 
                    arbol.setPonderacionRaiz(0);
            }
            // Como no es hoja, voy recorriendo cada nodo hasta llegar a una hoja
            // Luego de eso, las hojas van a ir teniendo valor de ponderación
            // entonces voy a poder calcular la ponderación de cada nodo padre
            else
            {
                foreach(ArbolGeneral hijos in arbol.getHijos())
                {
                    ponderarArbol(hijos);
                    float suma = 0;
                    foreach (ArbolGeneral misHijos in hijos.getHijos())
                    {
                        suma += misHijos.getPonderacionRaiz();
                    }
                    hijos.setPonderacionRaiz(suma / hijos.getHijos().Count);
                }
            }
        }
    }
}
