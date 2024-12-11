using System.Collections;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Animator animacionEntrada;
    
    public TextMeshProUGUI descripcionTexto;  // Referencia al TextMeshPro para mostrar la descripción del nodo
    public TMP_InputField  inputField;             // Referencia al InputField para capturar el texto
    public Button botonSi;                   // Botón para ir al nodoSi
    public Button botonNo;                   // Botón para ir al nodoNo

    public GameObject txt;
    public GameObject title;
    
    public GameObject botonSiObj;
    public GameObject botonNoObj;

    public GameObject inputFieldObj;
    public GameObject botonNextOne;
    public GameObject botonNextTwo;

    public GameObject lampara;
    public GameObject monito;
    
    private Node nodoActual;  // Nodo actual en el árbol
    private Tree arbol;

    public bool isChange = false;
    public bool finishGame = false;
    
    public List<string> treeData = new List<string>(); // Lista para almacenar el árbol
    private int index = 0; // Índice para llevar el control durante la deserialización
    
    // Start is called before the first frame update
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
            SaveTree();
            
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
    
    public void SaveTree()
    {
        if (arbol == null)
        {
            Debug.LogWarning("El árbol está vacío. Nada que guardar.");
            return;
        }

        string json = JsonUtility.ToJson(arbol, true); // Serializa el árbol
        string path = Application.persistentDataPath + "/tree.json";
        System.IO.File.WriteAllText(path, json); // Guarda el JSON en un archivo
        Debug.Log("Árbol guardado correctamente en: " + path);
    }
    
    public void LoadTree()
    {
        string path = Application.persistentDataPath + "/tree.json";

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path); // Lee el archivo JSON
            if (!string.IsNullOrWhiteSpace(json)) // Asegúrate de que el archivo no esté vacío
            {
                arbol = JsonUtility.FromJson<Tree>(json); // Deserializa el JSON en el objeto Tree
                nodoActual = arbol.nodoRaiz; // Establece el nodo actual como la raíz
                Debug.Log("Árbol cargado correctamente.");
            }
            else
            {
                Debug.LogWarning("El archivo JSON está vacío. No se puede cargar el árbol.");
                arbol = null;
                nodoActual = null;
            }
        }
        else
        {
            Debug.LogWarning("Archivo de árbol no encontrado. Se inicializará uno nuevo.");
            arbol = null;
            nodoActual = null;
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
    
    public void ClearTree()
    {   
        string path = Application.persistentDataPath + "/tree.json"; 
        if (System.IO.File.Exists(path))
        {
            System.IO.File.WriteAllText(path, ""); 
            Debug.Log("Archivo JSON limpiado.");
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo JSON para limpiar.");
        }
        
        arbol = null;
        nodoActual = null;
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

        string json = string.Join("\n", treeData); // Convierte la lista a un JSON-like string
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

            string json = string.Join("\n", treeData); // Convierte la lista a un JSON-like string
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
}
