public class Arbol
{
    public Nodo Raiz { get; private set; } // Nodo raíz del árbol

    public Arbol(string historiaInicial)
    {
        Raiz = new Nodo(historiaInicial); // Crear el nodo raíz con la historia inicial
    }
}