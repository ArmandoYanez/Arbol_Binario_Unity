public class Nodo
{
    public string Historia { get; set; } // Historia asociada al nodo
    public Nodo OpcionBuena { get; set; } // Referencia al nodo de la opción "Buena"
    public Nodo OpcionNeutra { get; set; } // Referencia al nodo de la opción "Neutra"
    public Nodo OpcionMala { get; set; } // Referencia al nodo de la opción "Mala"

    public Nodo(string historia)
    {
        Historia = historia;
    }
}