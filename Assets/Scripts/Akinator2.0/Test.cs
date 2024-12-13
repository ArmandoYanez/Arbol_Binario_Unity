using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Animator animacionEntrada;
    
    public TextMeshProUGUI descripcionTexto;  
    public TMP_InputField  inputField;             
    public Button botonSi;                  
    public Button botonNo;                 
    public GameObject txt;
    public GameObject title;
    
    public GameObject botonSiObj;
    public GameObject botonNoObj;

    public GameObject inputFieldObj;
    public GameObject botonNextOne;
    public GameObject botonNextTwo;

    public GameObject lampara;
    public GameObject monito;
    
    private Node nodoActual; 
    private Tree arbol;

    public bool isChange = false;
    public bool finishGame = false;
    
    public List<string> treeData = new List<string>(); // Lista para almacenar el árbol
    private int index = 0; // Índice para llevar el control durante la deserialización
    
    public void startGame()
    {
        LoadTreeManual();
        txt.SetActive(true);
        
        if (arbol == null)
        {
            Debug.Log("lol");
            descripcionTexto.text = "NOMBRE DE LO QUE ESTAS PENSANDO?";
            
            Node nodoRaiz = new Node("");
            arbol = new Tree(nodoRaiz);
      
            nodoActual = nodoRaiz;
            
            botonNextOne.SetActive(true);
            inputFieldObj.SetActive(true);
        }
        else
        {
            nodoActual = arbol.nodoRaiz;
            gameState();
            botonNoObj.SetActive(true);
            botonSiObj.SetActive(true);
        }
    }

    public void pressNext()
    {
        if (!isChange)
        {
            nodoActual.descripcion = $"Es {inputField.text}?"; // Guardamos en nodo root la priemra pregunta.
           
            
            botonNextTwo.SetActive(false);
            inputField.text = "";
            inputFieldObj.SetActive(false);
        
            botonSiObj.SetActive(true);
            botonNoObj.SetActive(true);
            nodoActual = arbol.nodoRaiz;
            
            gameState();
        }
        else
        {
            string temporal = nodoActual.descripcion;
            nodoActual.nodoNo = new Node(temporal);
            
            nodoActual.descripcion = $"{inputField.text}?"; // Guardamos en nodo root la priemra pregunta.
            descripcionTexto.text = "NOMBRE DE LO QUE ESTAS PENSANDO?";
        
            botonNextOne.SetActive(false);
            botonNextTwo.SetActive(true);
            inputField.text = "";
        }
    }
    
    public void pressNextwo()
    {
        if (!isChange)
        {
            nodoActual.nodoSi = new Node($"Piensas en {inputField.text}?");
            Debug.Log(arbol.nodoRaiz.nodoSi.descripcion);
       
            botonNextTwo.SetActive(false);
            inputField.text = "";
            inputFieldObj.SetActive(false);
        
            botonSiObj.SetActive(true);
            botonNoObj.SetActive(true);
            nodoActual = arbol.nodoRaiz;
            
            arbol.Balancear();
            gameState();
            SaveTreeManual();
        }
        else
        {
            nodoActual.nodoSi = new Node($"Piensas en {inputField.text}?");
            
            botonNextTwo.SetActive(false);
            inputField.text = "";
            inputFieldObj.SetActive(false);
        
            botonSiObj.SetActive(true);
            botonNoObj.SetActive(true);
            nodoActual = arbol.nodoRaiz;
            isChange = false;
            
            arbol.Balancear();
            gameState();
            SaveTreeManual();
        }
    }

    public void gameState()
    {
        descripcionTexto.text = nodoActual.descripcion;
    }

    // Método para mover al siguiente nodo en el árbol, cuando se selecciona 'Sí'
    public void PressYes()
    {
        if (!finishGame)
        {
            if (nodoActual.nodoSi != null)
            {
                nodoActual = nodoActual.nodoSi;
                gameState();
            }
            else
            {
                descripcionTexto.text = "QUIERES VOLVER A JUGAR?";
                finishGame = true;
            }
        }
        else
        {
            nodoActual = arbol.nodoRaiz;
            finishGame = false;
            gameState();
        }
        arbol.Balancear();
    }

    // Método para mover al siguiente nodo en el árbol, cuando se selecciona 'No'
    public void PressNo()
    {
        if (!finishGame)
        {
            Debug.Log("entro");
            if (nodoActual.nodoNo == null)
            {
                isChange = true;
                botonNoObj.SetActive(false);
                botonSiObj.SetActive(false);
            
                descripcionTexto.text = "DIFERENCIA DE LO QUE ESTAS PENSANDO?";
            
                botonNextOne.SetActive(true);
                inputFieldObj.SetActive(true);
            }
            else
            {
                nodoActual = nodoActual.nodoNo;
                gameState();
            }
        }
        else
        {
            Application.Quit();
            SaveTreeManual();
        }
    }

    public void buttonSalir()
    {
        Application.Quit(); 
    }

    public void buttonStart()
    {
        StartCoroutine(animacion());
    }
    
    IEnumerator animacion()
    {
        animacionEntrada.Play("opening");
        yield return new WaitForSeconds(7.5f);
        
        monito.SetActive(true);
        lampara.SetActive(false);
        
        title.SetActive(false);
        startGame();
    }
    
    // Método para reconstruir el árbol en preorden
    public Node DeserializeTree()
    {
        if (index >= treeData.Count || treeData[index] == "null")
        {
            index++; // Avanza el índice si es un nodo nulo
            return null;
        }

        // Crea un nuevo nodo con la descripción actual
        Node nodo = new Node(treeData[index]);
        index++;

        nodo.nodoSi = DeserializeTree(); // Reconstruye el subárbol izquierdo
        nodo.nodoNo = DeserializeTree(); // Reconstruye el subárbol derecho

        return nodo;
    }

    // Guardar el árbol en un archivo
    public void SaveTreeManual()
    {
        treeData.Clear(); // Limpia la lista antes de serializar
        SerializeTree(arbol.nodoRaiz); // Inicia la serialización desde la raíz

        string json = string.Join("\n", treeData); 
        Debug.Log("Árbol en formato JSON-like:\n" + json); // Imprime el árbol en formato JSON

        string path = Application.persistentDataPath + "/tree_manual.txt"; 
        System.IO.File.WriteAllLines(path, treeData); // Guarda cada línea como un nodo
        Debug.Log("Árbol guardado manualmente en: " + path);
    }
    
    public void SerializeTree(Node nodo)
    {
        if (nodo == null)
        {
            treeData.Add("null"); // Marca un nodo nulo para reconstruir el árbol correctamente
            return;
        }

        treeData.Add(nodo.descripcion); // Agrega la descripción del nodo actual
        SerializeTree(nodo.nodoSi); // Recorre el subárbol izquierdo (nodoSí)
        SerializeTree(nodo.nodoNo); // Recorre el subárbol derecho (nodoNo)
    }
    
    // Cargar el árbol desde un archivo
    public void LoadTreeManual()
    {
        string path = Application.persistentDataPath + "/tree_manual.txt"; 

        if (System.IO.File.Exists(path))
        {
            treeData = new List<string>(System.IO.File.ReadAllLines(path)); // Carga los nodos del archivo

            string json = string.Join("\n", treeData); 
            Debug.Log("Contenido del archivo JSON-like cargado:\n" + json); // Imprime el contenido del archivo

            index = 0; // Reinicia el índice
            arbol = new Tree(DeserializeTree()); // Reconstruye el árbol desde la raíz
            Debug.Log("Árbol cargado manualmente.");
        }
        else
        {
            Debug.LogWarning("Archivo del árbol no encontrado.");
        }
    }
    
    // Método para borrar el archivo que guarda el árbol
    public void DeleteTreeManual()
    {
        string path = Application.persistentDataPath + "/tree_manual.txt"; 

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path); // Elimina el archivo
            Debug.Log("Archivo del árbol eliminado exitosamente: " + path);
        }
        else
        {
            Debug.LogWarning("No se encontró ningún archivo para eliminar en: " + path);
        }
    }
}
