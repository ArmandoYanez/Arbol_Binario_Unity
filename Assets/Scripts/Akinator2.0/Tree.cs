using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Tree
{
    public Node nodoRaiz;  // Nodo raíz del árbol

    // Constructor de la clase Tree
    public Tree(Node raiz)
    {
        nodoRaiz = raiz;
    }

    // Método para acceder al nodo raíz
    public Node ObtenerNodoRaiz()
    {
        return nodoRaiz;
    }

    // Puedes agregar otros métodos para navegar o interactuar con el árbol si es necesario
}