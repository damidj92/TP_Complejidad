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
            this.cargarArbol(this.minimax, cartasPropias, cartasOponente, limite, turnoOponente);

            // Ponderar el arbol
            this.ponderarArbol(this.minimax);
        }
				
		public override int descartarUnaCarta()
		{
            float mayor = 0;
            ArbolGeneral auxArbol = new ArbolGeneral(0,0,false);

            // Como el turno anterior fue del usuario, la raiz ahora es una carta de Human
            // Por lo tanto, los hijos son las cartas que puede jugar Computer
            foreach(ArbolGeneral arbol in this.minimax.getHijos())
            {
                if(arbol.getPonderacionRaiz() > mayor)
                {
                    mayor = arbol.getPonderacionRaiz();
                    auxArbol = arbol;
                }
            }

            this.setMinimax(auxArbol);

            return auxArbol.getNumCartaRaiz();
		}
		
		public override void cartaDelOponente(int carta)
		{
            // Como el primer turno es el de Human, este va a descartar una carta, que será recibida por el metodo
            // Entonces busco en los hijos de la raíz(Computer) la carta de Human
            foreach(ArbolGeneral hijos in this.minimax.getHijos())
            {
                // Si el numero de carta de uno de los hijos coincide con la carta recibida
                // entonces mi arbol minimax va a empezar por el nodo que corresponde a la carta de Human
                if(hijos.getNumCartaRaiz() == carta)
                {
                    this.setMinimax(hijos);
                }
            }

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
                    this.minimax.agregarHijo(nodoCarta);

                    // Una vez agregado debo sacarlo de la lista cartasOponente
                    List<int> aux = new List<int>(cartasOponente);
                    aux.Remove(carta);

                    // LLamo nuevamente al metodo 
                    // pero invirtiendo el orden de las cartasPropias por las cartasOponente(aux)
                    this.cargarArbol(this.minimax, aux, cartasPropias, limite, !turno);
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
                foreach (ArbolGeneral hijos in arbol.getHijos())
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

        public void setMinimax(ArbolGeneral unArbol)
        {
            this.minimax = unArbol;
        }
    }
}
