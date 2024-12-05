using System.Collections;
using TMPro;
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
    
    // Start is called before the first frame update
    public void startGame()
    {
        LoadTree();
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
            SaveTree();
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
            SaveTree();
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
        }
    }
    
    public void SaveTree()
    {
        string json = JsonUtility.ToJson(arbol, true); 
        string path = Application.persistentDataPath + "/tree.json"; 
        System.IO.File.WriteAllText(path, json); 
        Debug.Log("Árbol guardado en: " + path);
    }
    
    public void LoadTree()
    {
        string path = Application.persistentDataPath + "/tree.json";

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json)) // Verifica si el archivo está vacío
            {
                Debug.LogWarning("El archivo JSON está vacío. El árbol será establecido a null.");
                arbol = null;
                nodoActual = null;
            }
            else
            {
                arbol = JsonUtility.FromJson<Tree>(json); // Deserializa el JSON al objeto Tree
                nodoActual = arbol.nodoRaiz;
                Debug.Log("Árbol cargado correctamente.");
                gameState(); // Actualiza el estado del juego
            }
        }
        else
        {
            Debug.LogWarning("Archivo de árbol no encontrado. El árbol será establecido a null.");
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
}
