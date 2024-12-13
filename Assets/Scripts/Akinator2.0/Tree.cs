using UnityEngine;
public class Tree
{
    public Node nodoRaiz;

    public Tree(Node raiz)
    {
        nodoRaiz = raiz;
    }

    // Metodo para balancear el arbol
    public void Balancear()
    {
        nodoRaiz = BalancearNodo(nodoRaiz);
    }

    private Node BalancearNodo(Node nodo)
    {
        // Si el nodo es nulo o es un nodo hoja, no se hace nada
        if (nodo == null || (nodo.nodoSi == null && nodo.nodoNo == null))
        {
            return nodo;
        }

        // Balancear los subárboles
        nodo.nodoSi = BalancearNodo(nodo.nodoSi);
        nodo.nodoNo = BalancearNodo(nodo.nodoNo);

        // Verificar si la altura del abol necesita ser ajustada
        int alturaIzquierda = ObtenerAltura(nodo.nodoSi);
        int alturaDerecha = ObtenerAltura(nodo.nodoNo);

        // Si el árbol está desbalanceado y realizar rotacion
        if (Mathf.Abs(alturaIzquierda - alturaDerecha) > 1)
        {
            if (alturaIzquierda > alturaDerecha)
            {
                return RotarDerecha(nodo);
            }
            else
            {
                return RotarIzquierda(nodo);
            }
        }

        return nodo;
    }

    private int ObtenerAltura(Node nodo)
    {
        if (nodo == null)
        {
            return 0;
        }

        int alturaIzquierda = ObtenerAltura(nodo.nodoSi);
        int alturaDerecha = ObtenerAltura(nodo.nodoNo);

        return Mathf.Max(alturaIzquierda, alturaDerecha) + 1;
    }

    private Node RotarDerecha(Node nodo)
    {
        Node nuevoRaiz = nodo.nodoSi;
        nodo.nodoSi = nuevoRaiz.nodoNo;
        nuevoRaiz.nodoNo = nodo;

        return nuevoRaiz;
    }

    private Node RotarIzquierda(Node nodo)
    {
        Node nuevoRaiz = nodo.nodoNo;
        nodo.nodoNo = nuevoRaiz.nodoSi;
        nuevoRaiz.nodoSi = nodo;

        return nuevoRaiz;
    }
}
