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
    
    private bool isfirst = true;
    
   private void Start()
     { 
         
        if (nodoActual == null)
        {
            textoPregunta.text = "¿En que se diferencia el personaje?";
            nodoActual = arbol.raiz;
            nextButtonObj.SetActive(true);
            input1.SetActive(true);
        }
        else
        {
            textoPregunta.text = arbol.raiz.RespuestaORespuesta;
        }
    }

    public void nextButton()
    {
        if (nodoActual == null)
        {
            string respuesta = inputRespuesta.text;
            NodoA newNodo = new NodoA("¿" + respuesta + "?");
            nodoActual = newNodo;
            
            // Activamos y desactivamos los inputs correspondintes
            nextButtonObj.SetActive(false);
            input1.SetActive(false);
            
            textoPregunta.text = "¿Como se llama el personaje?";
            nextButtonObj2.SetActive(true);
            input2.SetActive(true);
        }
    }

    public void nextButton2()
    {
        string respuesta = inputRespuesta2.text;
        NodoA newNodo = new NodoA("¿Tu personaje es " + respuesta + "?");   
        nodoActual.setNodoSi(newNodo);
        
        estadoDeJuego();
    }

    public void estadoDeJuego()
    {
        
        nextButtonObj.SetActive(false);
        input1.SetActive(false);
        nextButtonObj2.SetActive(false);
        input2.SetActive(false);
        
        yesButton.SetActive(true);
        noButton.SetActive(true);
        
        textoPregunta.text =  nodoActual.RespuestaORespuesta;
    }

    public void precionarSi()
    {
        nodoActual = nodoActual.GetNodoSi();
        estadoDeJuego();
    }

    public void precionarNo()
    {
        nodoActual = nodoActual.GetNodoNo();

        if (nodoActual == null)
        {
            textoPregunta.text = "¿En que se diferencia el personaje?";
            nextButtonObj.SetActive(true);
            input1.SetActive(true);
        }
        else
        {
            estadoDeJuego();
        }
    }
}
