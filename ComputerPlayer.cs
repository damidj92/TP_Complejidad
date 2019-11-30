using System;
using System.Collections;
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
            int suma = 0;
            this.ponderarArbol(this.minimax, ref suma);
        }
				
		public override int descartarUnaCarta()
		{
            ArrayList disponibles = new ArrayList();

            float mayor = 0;
            ArbolGeneral auxArbol = null;

            // Como el turno anterior fue del usuario, la raiz ahora es una carta de Human
            // Por lo tanto, los hijos son las cartas que puede jugar Computer
            foreach(ArbolGeneral arbol in this.minimax.getHijos())
            {
                disponibles.Add(arbol.getNumCartaRaiz());
                if(arbol.getPonderacionRaiz() > mayor)
                {
                    mayor = arbol.getPonderacionRaiz();
                    auxArbol = arbol;
                }
            }
            Console.WriteLine("Naipes disponibles (Computer):");
            for (int i = 0; i < disponibles.Count; i++)
            {
                Console.Write(disponibles[i].ToString());
                if (i < disponibles.Count - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Computer elige: {0}", auxArbol.getNumCartaRaiz());
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
                    ArbolGeneral auxArbol = null;
                    auxArbol = hijos;
                    this.setMinimax(auxArbol);
                }
            }

        }

        public void cargarArbol(ArbolGeneral arbol, List<int> cartasPropias, List<int> cartasOponente, int limite, bool turno)
        {
            // Se va a llamar recursivamente hasta que no haya más cartas para agregar
            if ((cartasPropias.Count != 0 & cartasOponente.Count != 0) | limite >= 0)
            {
                // Recorro cada carta del oponente, para agregarla en el nodo padre
                // Console.WriteLine("Cartas por procesar: " + cartasOponente.Count);
                foreach (int carta in cartasOponente)
                {
                    // Reduzco el límite
                    limite -= carta;

                    // Creo un nodo de tipo ArbolGeneral para agregar al árbol minimax
                    ArbolGeneral nodoCarta = new ArbolGeneral(carta, limite, turno);
                    arbol.agregarHijo(nodoCarta);

                    // Una vez agregado debo sacarlo de la lista cartasOponente
                    List<int> aux = new List<int>(cartasOponente);
                    aux.Remove(carta);

                    // LLamo nuevamente al metodo 
                    // pero invirtiendo el orden de las cartasPropias por las cartasOponente(aux)
                    this.cargarArbol(nodoCarta, aux, cartasPropias, limite, !turno);
                }
            }
        }

        public void ponderarArbol(ArbolGeneral arbol, ref int suma)
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
                if (arbol.getLimiteRaiz() >= 0 & arbol.getTurnoRaiz() == false)
                {
                    arbol.setPonderacionRaiz(1);
                    suma += 1;
                }
                // Llego a la útima carta y supero el límite
                else
                {
                    arbol.setPonderacionRaiz(0);
                }
            }
            // Como no es hoja, voy recorriendo cada nodo hasta llegar a una hoja
            // Luego de eso, las hojas van a ir teniendo valor de ponderación
            // entonces voy a poder calcular la ponderación de cada nodo padre
            else
            {
                List<ArbolGeneral> auxHijos = arbol.getHijos();
                int sumaRec = 0;
                foreach (ArbolGeneral hijos in auxHijos)
                {
                    ponderarArbol(hijos, ref sumaRec);

                    /*foreach (ArbolGeneral misHijos in hijos.getHijos())
                    {
                        suma += misHijos.getPonderacionRaiz();
                    }*/
                    sumaRec += hijos.getPonderacionRaiz();
                    hijos.setPonderacionRaiz(hijos.getPonderacionRaiz()+1);
                }
                arbol.setPonderacionRaiz(suma);
            }
        }

        public void setMinimax(ArbolGeneral unArbol)
        {
            this.minimax = unArbol;
        }
    }
}
