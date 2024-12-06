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

    // Método para obtener la altura de un nodo
    private int ObtenerAltura(Node nodo)
    {
        return nodo == null ? 0 : nodo.altura;
    }

    // Método para calcular el factor de balance
    private int CalcularBalance(Node nodo)
    {
        return nodo == null ? 0 : ObtenerAltura(nodo.nodoSi) - ObtenerAltura(nodo.nodoNo);
    }

// Rotación simple a la derecha
    private Node RotacionDerecha(Node y)
    {
        Node x = y.nodoSi;
        Node T2 = x.nodoNo;

        // Rotación
        x.nodoNo = y;
        y.nodoSi = T2;

        // Actualizar alturas
        y.altura = Mathf.Max(ObtenerAltura(y.nodoSi), ObtenerAltura(y.nodoNo)) + 1;
        x.altura = Mathf.Max(ObtenerAltura(x.nodoSi), ObtenerAltura(x.nodoNo)) + 1;

        return x; // Nueva raíz
    }

// Rotación simple a la izquierda
    private Node RotacionIzquierda(Node x)
    {
        Node y = x.nodoNo;
        Node T2 = y.nodoSi;

        // Rotación
        y.nodoSi = x;
        x.nodoNo = T2;

        // Actualizar alturas
        x.altura = Mathf.Max(ObtenerAltura(x.nodoSi), ObtenerAltura(x.nodoNo)) + 1;
        y.altura = Mathf.Max(ObtenerAltura(y.nodoSi), ObtenerAltura(y.nodoNo)) + 1;

        return y; // Nueva raíz
    }
    
    public Node Insertar(Node nodo, string descripcion)
    {
        // Inserciasd normal en un arbol binario
        if (nodo == null)   
            return new Node(descripcion);

        if (string.Compare(descripcion, nodo.descripcion) < 0)
            nodo.nodoSi = Insertar(nodo.nodoSi, descripcion);
        else if (string.Compare(descripcion, nodo.descripcion) > 0)
            nodo.nodoNo = Insertar(nodo.nodoNo, descripcion);
        else
            return nodo; // Descripciones iguales no se permiten

        // Actualizar altura
        nodo.altura = Mathf.Max(ObtenerAltura(nodo.nodoSi), ObtenerAltura(nodo.nodoNo)) + 1;

        // Obtener factor de balance
        int balance = CalcularBalance(nodo);

        // Rotaciones para balancear el asrbol
        // Caso izquierda-izquierda
        if (balance > 1 && string.Compare(descripcion, nodo.nodoSi.descripcion) < 0)
            return RotacionDerecha(nodo);

        // Caso derecha-derecha
        if (balance < -1 && string.Compare(descripcion, nodo.nodoNo.descripcion) > 0)
            return RotacionIzquierda(nodo);

        // Caso izquierda-derecha
        if (balance > 1 && string.Compare(descripcion, nodo.nodoSi.descripcion) > 0)
        {
            nodo.nodoSi = RotacionIzquierda(nodo.nodoSi);
            return RotacionDerecha(nodo);
        }

        // Caso derecha-izquierda
        if (balance < -1 && string.Compare(descripcion, nodo.nodoNo.descripcion) < 0)
        {
            nodo.nodoNo = RotacionDerecha(nodo.nodoNo);
            return RotacionIzquierda(nodo);
        }

        return nodo;
    }
}