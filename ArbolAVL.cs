using System;

public class ArbolAVL<T> : ArbolBST<T> where T : IComparable<T>
{
    // Constructor
    public ArbolAVL() : base() { }

    // Sobrescribir el método de inserción para mantener el balance
    public override void Insertar(T valor)
    {
        Raiz = InsertarAVL(Raiz, valor);
    }

    private NodoBST<T> InsertarAVL(NodoBST<T> nodo, T valor)
    {
        // Realizar la inserción normal de BST
        nodo = InsertarRecursivo(nodo, valor);

        // Actualizar altura y equilibrar el nodo
        return Equilibrar(nodo);
    }

    // Sobrescribir el método de eliminación para mantener el balance
    public override void Eliminar(T valor)
    {
        Raiz = EliminarAVL(Raiz, valor);
    }

    private NodoBST<T> EliminarAVL(NodoBST<T> nodo, T valor)
    {
        // Realizar la eliminación normal de BST
        nodo = EliminarRecursivo(nodo, valor);

        // Si el nodo se eliminó, no necesitamos equilibrar
        if (nodo == null)
            return null;

        // Actualizar altura y equilibrar el nodo
        return Equilibrar(nodo);
    }

    // Método para equilibrar un nodo
    private NodoBST<T> Equilibrar(NodoBST<T> nodo)
    {
        // Obtener el factor de equilibrio
        int factorEquilibrio = ObtenerFactorEquilibrio(nodo);

        // Desequilibrio izquierdo-izquierdo (rotación simple derecha)
        if (factorEquilibrio > 1 && ObtenerFactorEquilibrio(nodo.Izquierdo) >= 0)
        {
            return RotacionDerecha(nodo);
        }

        // Desequilibrio izquierdo-derecho (rotación doble)
        if (factorEquilibrio > 1 && ObtenerFactorEquilibrio(nodo.Izquierdo) < 0)
        {
            nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo);
            return RotacionDerecha(nodo);
        }

        // Desequilibrio derecho-derecho (rotación simple izquierda)
        if (factorEquilibrio < -1 && ObtenerFactorEquilibrio(nodo.Derecho) <= 0)
        {
            return RotacionIzquierda(nodo);
        }

        // Desequilibrio derecho-izquierdo (rotación doble)
        if (factorEquilibrio < -1 && ObtenerFactorEquilibrio(nodo.Derecho) > 0)
        {
            nodo.Derecho = RotacionDerecha(nodo.Derecho);
            return RotacionIzquierda(nodo);
        }

        // Si no hay desequilibrio, devolver el nodo sin cambios
        return nodo;
    }

    // Cálculo del factor de equilibrio (altura del subárbol izquierdo - altura del subárbol derecho)
    public int ObtenerFactorEquilibrio(NodoBST<T> nodo)
    {
        if (nodo == null)
            return 0;
        return CalcularAltura(nodo.Izquierdo) - CalcularAltura(nodo.Derecho);
    }

    // Rotación simple a la derecha
    private NodoBST<T> RotacionDerecha(NodoBST<T> y)
    {
        NodoBST<T> x = y.Izquierdo;
        NodoBST<T> T2 = x.Derecho;

        // Realizar rotación
        x.Derecho = y;
        y.Izquierdo = T2;

        // Devolver la nueva raíz
        return x;
    }

    // Rotación simple a la izquierda
    private NodoBST<T> RotacionIzquierda(NodoBST<T> x)
    {
        NodoBST<T> y = x.Derecho;
        NodoBST<T> T2 = y.Izquierdo;

        // Realizar rotación
        y.Izquierdo = x;
        x.Derecho = T2;

        // Devolver la nueva raíz
        return y;
    }

    // Sobrescribir método de inserción recursiva para adaptarlo al AVL
    protected override NodoBST<T> InsertarRecursivo(NodoBST<T> nodo, T valor)
    {
        // Inserción normal de BST
        if (nodo == null)
        {
            return new NodoBST<T>(valor);
        }

        int comparacion = valor.CompareTo(nodo.Valor);

        if (comparacion < 0)
        {
            nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
        }
        else if (comparacion > 0)
        {
            nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
        }
        else // Duplicado, no hacemos nada (o podríamos manejarlo de otra manera)
        {
            return nodo;
        }

        return nodo;
    }
}