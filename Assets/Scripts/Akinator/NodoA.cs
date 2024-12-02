public class NodoA
{
    public string RespuestaORespuesta;
    private NodoA nodoSi;
    private NodoA nodoNo;

    public NodoA(string respuesta)
    {
        RespuestaORespuesta = respuesta;
        nodoSi = null;
        nodoNo = null;
    }

    public void setNodoSi(NodoA nuevoNodo)
    {
        nodoSi = nuevoNodo;
    }
    
    public void setNodoN0(NodoA nuevoNodo)
    {
        nodoNo = nuevoNodo;
    }

    public NodoA GetNodoSi()
    {
        return nodoSi;
    }

    public NodoA GetNodoNo()
    {
        return nodoNo;
    }

    public bool EsHoja()
    {
        return nodoSi == null && nodoNo == null;
    }
}
