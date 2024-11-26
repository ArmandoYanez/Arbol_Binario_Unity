using UnityEngine;
using TMPro;

public class ControladorUI : MonoBehaviour
{
    public TMP_Text nivelTexto; // Nivel actual en el arbol
    public TMP_InputField historiaInput; // Campo de texto para mostrar o escribir la historia
    public UnityEngine.UI.Button botonBuena, botonNeutra, botonMala;
    public UnityEngine.UI.Button botonRellenar, botonEntrar, botonTerminar, botonVolverRoot;

    public Arbol arbolDeHistorias;
    public Nodo nodoActual;
    private int nivelActual;
    
    private Nodo opcionSeleccionada; // Nodo seleccionado para entrar (Buena, Neutra o Mala)


    void Start()
    {
        // Inicializar el árbol y establecer nodo raíz
        arbolDeHistorias = new Arbol("Nodo raíz vacío.");
        nodoActual = arbolDeHistorias.Raiz;
        nivelActual = 0;
        opcionSeleccionada = null;

        ActualizarUI();

        // Asignar listeners a los botones
        botonBuena.onClick.AddListener(() => SeleccionarOpcion(1));
        botonNeutra.onClick.AddListener(() => SeleccionarOpcion(2));
        botonMala.onClick.AddListener(() => SeleccionarOpcion(3));
        botonRellenar.onClick.AddListener(RellenarNodo);
        botonEntrar.onClick.AddListener(EntrarNodo);
        botonVolverRoot.onClick.AddListener(VolverARaiz);
    }

    void ActualizarUI()
    {
        nivelTexto.text = $"Nivel: {nivelActual}";
        historiaInput.text = nodoActual?.Historia ?? "Nodo vacío.";
    }

    void SeleccionarOpcion(int opcion)
    {
        // Seleccionar el nodo hijo correspondiente, el ?? significa que si es nulo pasara algo, en este caso crear el nuevo nodo.
        switch (opcion)
        {
            case 1:
                opcionSeleccionada = nodoActual.OpcionBuena ?? new Nodo("Nodo bueno vacío.");
                break;
            case 2:
                opcionSeleccionada = nodoActual.OpcionNeutra ?? new Nodo("Nodo neutro vacío.");
                break;
            case 3:
                opcionSeleccionada = nodoActual.OpcionMala ?? new Nodo("Nodo malo vacío.");
                break;
        }

        Debug.Log($"Opción seleccionada: {opcionSeleccionada.Historia}");
    }

    void RellenarNodo()
    {
        if (nodoActual != null)
        {
            nodoActual.Historia = historiaInput.text;
            Debug.Log($"Nodo rellenado con historia: {nodoActual.Historia}");
        }
    }

    void EntrarNodo()
    {
        if (opcionSeleccionada == null)
        {
            Debug.LogWarning("Por favor selecciona una opción antes de entrar.");
            return;
        }

        // Crear el nodo hijo si no existe y avanzar hacia el
        if (opcionSeleccionada.Historia == "Nodo bueno vacío.")
            nodoActual.OpcionBuena = opcionSeleccionada;
        else if (opcionSeleccionada.Historia == "Nodo neutro vacío.")
            nodoActual.OpcionNeutra = opcionSeleccionada;
        else if (opcionSeleccionada.Historia == "Nodo malo vacío.")
            nodoActual.OpcionMala = opcionSeleccionada;

        nodoActual = opcionSeleccionada;
        nivelActual++;
        opcionSeleccionada = null; // Reiniciar la selección
        ActualizarUI();
    }

    void VolverARaiz()
    {
        nodoActual = arbolDeHistorias.Raiz;
        nivelActual = 0;
        opcionSeleccionada = null;
        ActualizarUI();
    }
}