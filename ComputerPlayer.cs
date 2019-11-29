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
            this.minimax = new ArbolGeneral(0);
		}
		
		public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			// Cargar el árbol
            // Se va a llamar recursivamente hasta que no haya más cartas para agregar o que supere el límite
            if (cartasPropias.Count != 0 & cartasOponente.Count != 0 | limite > 0)
            {
                // Recorro cada carta del oponente, para agregarla en el nodo padre
                foreach (int carta in cartasOponente)
                {
                    // Creo un nodo de tipo ArbolGeneral para agregar al árbol minimax
                    ArbolGeneral nodoCarta = new ArbolGeneral(carta);

                    minimax.agregarHijo(nodoCarta);

                    // Una vez agregado debo sacarlo de la lista cartasOponente
                    List<int> aux = new List<int>();
                    aux = cartasOponente;
                    aux.Remove(carta);

                    // Reduzco el límite
                    limite -= carta;

                    // LLamo nuevamente al metodo, 
                    // pero invirtiendo el orden de las cartasPropias por las cartasOponente(aux)
                    this.incializar(aux, cartasPropias, limite);
                }
            }

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
		
	}
}
