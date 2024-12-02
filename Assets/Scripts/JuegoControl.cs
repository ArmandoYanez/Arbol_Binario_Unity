    using System;
using UnityEngine;
using TMPro; // Para usar TextMesh Pro

public class JuegoControl : MonoBehaviour
{
    public TextMeshProUGUI textoHistoria;  // Referencia al TextMesh Pro para mostrar la historia
    public GameObject panelEdicion, panelJuego; // Paneles de edición y juego

    public GameObject buttons1;
    public GameObject buttons2;
    public GameObject buttons3;
    public GameObject exitButton;
    
    public Nodo nodoActual;
    public ControladorUI _controladorUI;

    
    // Llamado cuando se presiona "Terminar" (pasar al modo juego)
    public void TerminarEdicion()
    {
        // Cambiar a modo juego
        panelEdicion.SetActive(false);
        panelJuego.SetActive(true);

        nodoActual =  _controladorUI.arbolDeHistorias.Raiz;
        
        // Mostrar la historia inicial (root)
        textoHistoria.text = nodoActual.Historia;
    }

    // Meetodo que maneja la elección "buena"
    public void ElegirBuena()
    {
        if (nodoActual.OpcionBuena != null)
        {
            nodoActual = nodoActual.OpcionBuena;
            textoHistoria.text = nodoActual.Historia;
        }
        else
        {
            EstadoFinal();
        }
    }

    // Metodo que maneja la elección "neutra"
    public void ElegirNeutra()
    {
        if (nodoActual.OpcionNeutra != null)
        {
            nodoActual = nodoActual.OpcionNeutra;
            textoHistoria.text = nodoActual.Historia;
        }
        else
        {
            EstadoFinal();
        }
    }

    // Metodo que maneja la elección "mala"
    public void ElegirMala()
    {
        if (nodoActual.OpcionMala != null)
        {
            nodoActual = nodoActual.OpcionMala;
            textoHistoria.text = nodoActual.Historia;
        }
        else 
        {
            EstadoFinal();
        }
    }

    public void EstadoFinal()
    {
        textoHistoria.text = "HISTORIA TERMINADA";
        buttons1.SetActive(false);
        buttons2.SetActive(false);
        buttons3.SetActive(false);
    }
}