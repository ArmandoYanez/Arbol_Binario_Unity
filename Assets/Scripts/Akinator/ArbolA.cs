using System.IO;
using UnityEngine;

public class ArbolA
{
    public NodoA raiz;

    // Cargar el árbol desde un archivo JSON
    public static ArbolA CargarDeJson(string archivo)
    {
        if (File.Exists(archivo))
        {
            string json = File.ReadAllText(archivo);
            return JsonUtility.FromJson<ArbolA>(json);
        }
        return null;
    }

    // Guardar el árbol a un archivo JSON
    public void GuardarEnJson(string archivo)
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(archivo, json);
    }
}