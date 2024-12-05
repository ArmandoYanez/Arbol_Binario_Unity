using UnityEngine;

[System.Serializable]
public class Node
{
    public string descripcion; // Descripción del nodo
    public Node nodoSi;        // Nodo si la respuesta es "Sí"
    public Node nodoNo;        // Nodo si la respuesta es "No"

    // Constructor
    public Node(string descripcion)
    {
        this.descripcion = descripcion;
        nodoSi = null;
        nodoNo = null;
    }

    // Método para asignar nodos hijos
    public void AsignarHijos(Node si, Node no)
    {
        nodoSi = si;
        nodoNo = no;
    }
}