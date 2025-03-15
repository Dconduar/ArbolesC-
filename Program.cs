using System;
using System.Collections.Generic;

class Nodo
{
    public int Valor;
    public List<Nodo> Hijos;

    public Nodo(int valor)
    {
        Valor = valor;
        Hijos = new List<Nodo>();
    }
}

class Arbol
{
    public Nodo Raiz;

    public Arbol(int valorRaiz)
    {
        Raiz = new Nodo(valorRaiz);
    }

    public Nodo BuscarNodo(Nodo nodo, int valor)
    {
        if (nodo == null) return null;
        if (nodo.Valor == valor) return nodo;
        
        foreach (var hijo in nodo.Hijos)
        {
            var resultado = BuscarNodo(hijo, valor);
            if (resultado != null) return resultado;
        }
        return null;
    }

    public void AgregarNodo(int valorPadre, int valorHijo)
    {
        Nodo padre = BuscarNodo(Raiz, valorPadre);
        if (padre != null)
            padre.Hijos.Add(new Nodo(valorHijo));
        else
            Console.WriteLine("Nodo padre no encontrado.");
    }

    // Función para mostrar el árbol visualmente en consola
    public void MostrarArbol(Nodo nodo, string prefijo = "", bool esUltimo = true)
    {
        if (nodo == null) return;

        // Imprime el nodo actual con su prefijo
        Console.WriteLine(prefijo + (esUltimo ? "└── " : "├── ") + nodo.Valor);

        // Llamada recursiva para los hijos
        for (int i = 0; i < nodo.Hijos.Count; i++)
        {
            MostrarArbol(nodo.Hijos[i], prefijo + (esUltimo ? "    " : "│   "), i == nodo.Hijos.Count - 1);
        }
    }

    public int CalcularAltura(Nodo nodo)
    {
        if (nodo == null) return 0;
        if (nodo.Hijos.Count == 0) return 1;
        
        int alturaMax = 0;
        foreach (var hijo in nodo.Hijos)
        {
            alturaMax = Math.Max(alturaMax, CalcularAltura(hijo));
        }
        return alturaMax + 1;
    }

    public int CalcularGrado(Nodo nodo)
    {
        if (nodo == null) return 0;
        int maxGrado = nodo.Hijos.Count;
        foreach (var hijo in nodo.Hijos)
        {
            maxGrado = Math.Max(maxGrado, CalcularGrado(hijo));
        }
        return maxGrado;
    }

    public double CalcularOrden(Nodo nodo)
    {
        if (nodo == null) return 0;
        int totalNodos = 0;
        int totalNiveles = 0;

        // Función recursiva para contar nodos por nivel
        void ContarNodosPorNivel(Nodo n, int nivel)
        {
            if (n == null) return;
            totalNodos++;
            totalNiveles = Math.Max(totalNiveles, nivel);

            foreach (var hijo in n.Hijos)
            {
                ContarNodosPorNivel(hijo, nivel + 1);
            }
        }

        ContarNodosPorNivel(nodo, 0);
        return (double)totalNodos / (totalNiveles + 1);
    }

    public void RecorrerPreorden(Nodo nodo)
    {
        if (nodo == null) return;
        Console.Write(nodo.Valor + " ");
        foreach (var hijo in nodo.Hijos)
        {
            RecorrerPreorden(hijo);
        }
    }

    public void RecorrerInorden(Nodo nodo)
    {
        if (nodo == null) return;
        if (nodo.Hijos.Count > 0)
            RecorrerInorden(nodo.Hijos[0]);
        Console.Write(nodo.Valor + " ");
        for (int i = 1; i < nodo.Hijos.Count; i++)
            RecorrerInorden(nodo.Hijos[i]);
    }

    public void RecorrerPostorden(Nodo nodo)
    {
        if (nodo == null) return;
        foreach (var hijo in nodo.Hijos)
        {
            RecorrerPostorden(hijo);
        }
        Console.Write(nodo.Valor + " ");
    }

    // Función para buscar un valor en el árbol
    public void BuscarValor(int valor)
    {
        Console.WriteLine($"Buscando el valor: {valor}");
        Console.Write("Preorden: ");
        RecorrerPreorden(Raiz);
        Console.WriteLine();

        Console.Write("Inorden: ");
        RecorrerInorden(Raiz);
        Console.WriteLine();

        Console.Write("Postorden: ");
        RecorrerPostorden(Raiz);
        Console.WriteLine();
    }
}

class Programa
{
    static void Main()
    {
        Console.Write("Ingrese el valor de la raíz: ");
        int valorRaiz = int.Parse(Console.ReadLine());
        Arbol arbol = new Arbol(valorRaiz);

        while (true)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Agregar Nodo");
            Console.WriteLine("2. Mostrar Árbol");
            Console.WriteLine("3. Calcular Altura");
            Console.WriteLine("4. Calcular Grado");
            Console.WriteLine("5. Calcular Orden");
            Console.WriteLine("6. Recorridos (Preorden, Inorden, Postorden)");
            Console.WriteLine("7. Buscar un Valor en el Árbol");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");
            
            int opcion = int.Parse(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el valor del nodo padre: ");
                    int padre = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el valor del nuevo nodo: ");
                    int hijo = int.Parse(Console.ReadLine());
                    arbol.AgregarNodo(padre, hijo);
                    break;
                case 2:
                    arbol.MostrarArbol(arbol.Raiz);
                    break;
                case 3:
                    Console.WriteLine("Altura del árbol: " + arbol.CalcularAltura(arbol.Raiz));
                    break;
                case 4:
                    Console.WriteLine("Grado del árbol: " + arbol.CalcularGrado(arbol.Raiz));
                    break;
                case 5:
                    Console.WriteLine("Orden del árbol: " + arbol.CalcularOrden(arbol.Raiz));
                    break;
                case 6:
                    Console.Write("Preorden: ");
                    arbol.RecorrerPreorden(arbol.Raiz);
                    Console.WriteLine();
                    Console.Write("Inorden: ");
                    arbol.RecorrerInorden(arbol.Raiz);
                    Console.WriteLine();
                    Console.Write("Postorden: ");
                    arbol.RecorrerPostorden(arbol.Raiz);
                    Console.WriteLine();
                    break;
                case 7:
                    Console.Write("Ingrese el valor a buscar: ");
                    int valorBuscar = int.Parse(Console.ReadLine());
                    arbol.BuscarValor(valorBuscar);
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
