using UnityEngine;

[System.Serializable]
public class Node
{
    public string descripcion; // Descripción del nodo
    public Node nodoSi;        // Nodo si la respuesta es "Sí"
    public Node nodoNo;        // Nodo si la respuesta es "No"
    public int altura;         // Altura del nodo para cálculo de balance

    // Constructor
    public Node(string descripcion)
    {
        this.descripcion = descripcion;
        nodoSi = null;
        nodoNo = null;
        altura = 1; // Altura inicial de un nodo recién creado
    }

    // Método para asignar nodos hijos
    public void AsignarHijos(Node si, Node no)
    {
        nodoSi = si;
        nodoNo = no;
    }
}