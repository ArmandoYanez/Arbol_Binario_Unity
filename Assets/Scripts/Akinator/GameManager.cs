using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
       public TMP_Text textoPregunta;
    public TMP_InputField inputRespuesta;
    public TMP_InputField inputRespuesta2;

    public GameObject nextButtonObj;
    public GameObject input1;

    public GameObject nextButtonObj2;
    public GameObject input2;

    public GameObject yesButton;
    public GameObject noButton;

    private ArbolA arbol = new ArbolA();
    private NodoA nodoActual;
    private NodoA nodoPendiente;

    private bool isFirst = true;
    
    private void Start()
    {
        // Comprobamos si el árbol tiene nodos al inicio
        if (arbol.raiz == null)
        {
            textoPregunta.text = "¿En qué se diferencia el personaje?";
            nextButtonObj.SetActive(true);
            input1.SetActive(true);
        }
        else
        {
            textoPregunta.text = arbol.raiz.RespuestaORespuesta;
            nodoActual = arbol.raiz;  // Aseguramos que nodoActual apunta al nodo raíz
            estadoDeJuego();
        }
    }

    public void nextButton()
    {
        if (nodoActual == null)
        {
            // Primer input: descripción del personaje
            string respuesta = inputRespuesta.text;
            NodoA newNodo = new NodoA("¿" + respuesta + "?");
            nodoActual = newNodo;

            // Cambiar pregunta para el nombre del personaje
            nextButtonObj.SetActive(false);
            input1.SetActive(false);
            textoPregunta.text = "¿Cómo se llama el personaje?";
            nextButtonObj2.SetActive(true);
            input2.SetActive(true);
            Debug.Log("entro");
        }
        else
        {
            // Si ya existe un nodo, creamos un nuevo nodo "No"
            string respuesta = inputRespuesta.text;
            NodoA newNodoNo = new NodoA("¿" + respuesta + "?");
            nodoActual.setNodoN0(newNodoNo);
            nodoActual = newNodoNo;  // Ahora nodoActual apunta al nodo recién creado

            // Cambiar pregunta para el nombre del personaje
            nextButtonObj.SetActive(false);
            input1.SetActive(false);
            textoPregunta.text = "¿Cómo se llama el personaje?";
            nextButtonObj2.SetActive(true);
            input2.SetActive(true);
            Debug.Log("entro");
        }
    }

    public void nextButton2()
    {
        // Asegúrate de que nodoActual no sea null antes de continuar
        if (nodoActual == null)
        {
            Debug.LogError("nodoActual es null en nextButton2");
            return;
        }

        // Segundo input: nombre del personaje
        string respuesta = inputRespuesta2.text;
        NodoA newNodo = new NodoA("¿Tu personaje se llama " + respuesta + "?");

        // Asignamos el nodo "Sí" para la respuesta correcta
        nodoActual.setNodoSi(newNodo);

        // Guardamos el nodo raíz de vuelta en el árbol
        arbol.raiz = nodoActual;  // Actualizamos la raíz del árbol

        // Regresamos al estado de juego y mostramos la primera pregunta
        estadoDeJuego();
    }

    public void estadoDeJuego()
    {
        // Comprobamos si nodoActual es null antes de proceder
        if (nodoActual == null)
        {
            Debug.LogError("nodoActual es null en estadoDeJuego");
            return;
        }

        nextButtonObj.SetActive(false);
        input1.SetActive(false);
        nextButtonObj2.SetActive(false);
        input2.SetActive(false);

        yesButton.SetActive(true);
        noButton.SetActive(true);

        textoPregunta.text = nodoActual.RespuestaORespuesta;
    }

    public void precionarSi()
    {
        if (nodoActual == null)
        {
            Debug.LogError("nodoActual es null en precionarSi");
            return;
        }

        // Ir al nodo "Sí" y continuar con el juego
        if (nodoActual.GetNodoSi() != null)
        {
            nodoActual = nodoActual.GetNodoSi();
            estadoDeJuego();
        }
        else
        {
            // Si el nodo "Sí" es vacío, agregar una nueva pregunta
            textoPregunta.text = "¿En qué se diferencia el personaje?";
            nextButtonObj.SetActive(true);
            input1.SetActive(true);
        }
    }

    public void precionarNo()
    {
        if (nodoActual == null)
        {
            Debug.LogError("nodoActual es null en precionarNo");
            return;
        }

        // Ir al nodo "No" y continuar con el juego
        if (nodoActual.GetNodoNo() != null)
        {
            nodoActual = nodoActual.GetNodoNo();
            estadoDeJuego();
        }
        else
        {
            // Si el nodo "No" es vacío, agregar una nueva pregunta
            textoPregunta.text = "¿En qué se diferencia el personaje?";
            nextButtonObj.SetActive(true);
            input1.SetActive(true);

            // Crear el nuevo nodo "No" con la pregunta
            NodoA newNodoNo = new NodoA("¿En qué se diferencia el personaje?");
            nodoActual.setNodoN0(newNodoNo);
            nodoActual = newNodoNo;  // Ahora nodoActual apunta al nodo recién creado

            // Esperar la respuesta
            nextButtonObj.SetActive(true);
            input1.SetActive(true);
        }
    }
}
