[System.Serializable]
public class Node
{
    public string descripcion;
    public Node nodoSi;
    public Node nodoNo;

    public Node(string descripcion)
    {
        this.descripcion = descripcion;
        this.nodoSi = null;
        this.nodoNo = null;
    }
}