using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== COMPARACIÓN DE RENDIMIENTO ENTRE ÁRBOLES BST Y AVL =====");
        
        const int CANTIDAD_ELEMENTOS = 10000;
        
        ArbolBST<int> arbolBST = new ArbolBST<int>();
        ArbolAVL<int> arbolAVL = new ArbolAVL<int>();
        
        List<int> numeros = GenerarNumerosAleatorios(CANTIDAD_ELEMENTOS);
        
        Stopwatch cronometro = new Stopwatch();
        
        Console.WriteLine("\n----- INSERCIÓN DE ELEMENTOS -----");
        
        cronometro.Restart();
        foreach (var num in numeros)
        {
            arbolBST.Insertar(num);
        }
        cronometro.Stop();
        Console.WriteLine($"Tiempo de inserción de {CANTIDAD_ELEMENTOS} elementos en BST: {cronometro.ElapsedMilliseconds} ms");
        Console.WriteLine($"Altura del árbol BST: {arbolBST.ObtenerAltura()}");
        
        cronometro.Restart();
        foreach (var num in numeros)
        {
            arbolAVL.Insertar(num);
        }
        cronometro.Stop();
        Console.WriteLine($"Tiempo de inserción de {CANTIDAD_ELEMENTOS} elementos en AVL: {cronometro.ElapsedMilliseconds} ms");
        Console.WriteLine($"Altura del árbol AVL: {arbolAVL.ObtenerAltura()}");
        
        Console.WriteLine("\n----- BÚSQUEDA DE ELEMENTOS -----");
        
        int elementoInicio = numeros[0];
        int elementoMedio = numeros[CANTIDAD_ELEMENTOS / 2];
        int elementoFinal = numeros[CANTIDAD_ELEMENTOS - 1];
        
        Console.WriteLine("Búsqueda en BST:");
        MedirTiempoBusqueda(arbolBST, elementoInicio, "inicio");
        MedirTiempoBusqueda(arbolBST, elementoMedio, "medio");
        MedirTiempoBusqueda(arbolBST, elementoFinal, "final");
        
        Console.WriteLine("\nBúsqueda en AVL:");
        MedirTiempoBusqueda(arbolAVL, elementoInicio, "inicio");
        MedirTiempoBusqueda(arbolAVL, elementoMedio, "medio");
        MedirTiempoBusqueda(arbolAVL, elementoFinal, "final");
        
        Console.WriteLine("\n----- ELIMINACIÓN DE ELEMENTOS -----");
        
        cronometro.Restart();
        arbolBST.Eliminar(elementoMedio);
        cronometro.Stop();
        Console.WriteLine($"Tiempo de eliminación en BST: {cronometro.ElapsedMilliseconds} ms");
        
        cronometro.Restart();
        arbolAVL.Eliminar(elementoMedio);
        cronometro.Stop();
        Console.WriteLine($"Tiempo de eliminación en AVL: {cronometro.ElapsedMilliseconds} ms");
        
        Console.ReadKey();
    }
    
    static List<int> GenerarNumerosAleatorios(int cantidad)
    {
        List<int> numeros = new List<int>(cantidad);
        Random random = new Random();
        
        for (int i = 0; i < cantidad; i++)
        {
            numeros.Add(random.Next(1, 1000000));
        }
        
        return numeros;
    }
    
    static void MedirTiempoBusqueda<T>(ArbolBST<T> arbol, T valor, string posicion) where T : IComparable<T>
    {
        Stopwatch cronometro = new Stopwatch();
        cronometro.Start();
        bool encontrado = arbol.Buscar(valor);
        cronometro.Stop();
        
        Console.WriteLine($"- Búsqueda de elemento al {posicion}: {cronometro.ElapsedTicks} ticks ({(encontrado ? "encontrado" : "no encontrado")})");
    }
}