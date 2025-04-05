using System;
using System.Collections.Generic;

public class ArbolBST<T> where T : IComparable<T>
{
    public NodoBST<T> Raiz { get; set; }

    public ArbolBST()
    {
        Raiz = null;
    }

    // Insertar un valor en el árbol
    public virtual void Insertar(T valor)
    {
        Raiz = InsertarRecursivo(Raiz, valor);
    }

    protected virtual NodoBST<T> InsertarRecursivo(NodoBST<T> nodo, T valor)
    {
        // Si el nodo es nulo, crear un nuevo nodo
        if (nodo == null)
        {
            return new NodoBST<T>(valor);
        }

        // Comparar el valor a insertar con el valor del nodo actual
        int comparacion = valor.CompareTo(nodo.Valor);

        // Si el valor es menor, insertar en el subárbol izquierdo
        if (comparacion < 0)
        {
            nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
        }
        // Si el valor es mayor, insertar en el subárbol derecho
        else if (comparacion > 0)
        {
            nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
        }
        // Si el valor ya existe, no hacer nada (o puedes manejar duplicados según lo necesites)

        return nodo;
    }

    // Buscar un valor en el árbol
    public bool Buscar(T valor)
    {
        return BuscarRecursivo(Raiz, valor);
    }

    private bool BuscarRecursivo(NodoBST<T> nodo, T valor)
    {
        // Si el nodo es nulo, el valor no se encontró
        if (nodo == null)
        {
            return false;
        }

        // Comparar el valor buscado con el valor del nodo actual
        int comparacion = valor.CompareTo(nodo.Valor);

        // Si el valor es igual al valor del nodo, lo encontramos
        if (comparacion == 0)
        {
            return true;
        }
        // Si el valor es menor, buscar en el subárbol izquierdo
        else if (comparacion < 0)
        {
            return BuscarRecursivo(nodo.Izquierdo, valor);
        }
        // Si el valor es mayor, buscar en el subárbol derecho
        else
        {
            return BuscarRecursivo(nodo.Derecho, valor);
        }
    }

    // Eliminar un valor del árbol
    public virtual void Eliminar(T valor)
    {
        Raiz = EliminarRecursivo(Raiz, valor);
    }

    protected virtual NodoBST<T> EliminarRecursivo(NodoBST<T> nodo, T valor)
    {
        // Si el nodo es nulo, el valor no está en el árbol
        if (nodo == null)
        {
            return null;
        }

        // Comparar el valor a eliminar con el valor del nodo actual
        int comparacion = valor.CompareTo(nodo.Valor);

        // Si el valor es menor, buscar en el subárbol izquierdo
        if (comparacion < 0)
        {
            nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
        }
        // Si el valor es mayor, buscar en el subárbol derecho
        else if (comparacion > 0)
        {
            nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
        }
        // Si el valor es igual, este es el nodo a eliminar
        else
        {
            // Caso 1: Nodo hoja (sin hijos)
            if (nodo.Izquierdo == null && nodo.Derecho == null)
            {
                return null;
            }
            // Caso 2: Nodo con un solo hijo
            else if (nodo.Izquierdo == null)
            {
                return nodo.Derecho;
            }
            else if (nodo.Derecho == null)
            {
                return nodo.Izquierdo;
            }
            // Caso 3: Nodo con dos hijos
            else
            {
                // Encontrar el sucesor (el valor más pequeño en el subárbol derecho)
                T sucesorValor = EncontrarValorMinimo(nodo.Derecho);
                nodo.Valor = sucesorValor;
                // Eliminar el sucesor del subárbol derecho
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesorValor);
            }
        }

        return nodo;
    }

    // Encontrar el valor mínimo en un subárbol
    private T EncontrarValorMinimo(NodoBST<T> nodo)
    {
        T valorMinimo = nodo.Valor;
        while (nodo.Izquierdo != null)
        {
            valorMinimo = nodo.Izquierdo.Valor;
            nodo = nodo.Izquierdo;
        }
        return valorMinimo;
    }

    // Recorrido InOrden (ordenado para BST)
    public List<T> RecorridoInOrden()
    {
        List<T> resultados = new List<T>();
        RecorridoInOrdenRecursivo(Raiz, resultados);
        return resultados;
    }

    private void RecorridoInOrdenRecursivo(NodoBST<T> nodo, List<T> resultados)
    {
        if (nodo != null)
        {
            RecorridoInOrdenRecursivo(nodo.Izquierdo, resultados);
            resultados.Add(nodo.Valor);
            RecorridoInOrdenRecursivo(nodo.Derecho, resultados);
        }
    }

    // Obtener la altura del árbol
    public int ObtenerAltura()
    {
        return CalcularAltura(Raiz);
    }

    protected int CalcularAltura(NodoBST<T> nodo)
    {
        if (nodo == null)
        {
            return 0;
        }
        return 1 + Math.Max(CalcularAltura(nodo.Izquierdo), CalcularAltura(nodo.Derecho));
    }
}

// Clase para nodos del BST
public class NodoBST<T> where T : IComparable<T>
{
    public T Valor { get; set; }
    public NodoBST<T> Izquierdo { get; set; }
    public NodoBST<T> Derecho { get; set; }

    public NodoBST(T valor)
    {
        Valor = valor;
        Izquierdo = null;
        Derecho = null;
    }
}