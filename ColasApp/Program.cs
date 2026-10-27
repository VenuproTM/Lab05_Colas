// Tema 5: Colas (Queue)
using System;

class Cola<T>
{
    private T[] elementos;
    private int frente;
    private int final;
    private int cantidad;
    private int capacidad;

    public Cola(int capacidad = 100)
    {
        this.capacidad = capacidad;
        elementos = new T[capacidad];
        frente = 0;
        final = -1;
        cantidad = 0;
    }

    public void Enqueue(T valor)
    {
        if (cantidad == capacidad)
            throw new InvalidOperationException("La cola esta llena.");

        final = (final + 1) % capacidad;
        elementos[final] = valor;
        cantidad++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("La cola esta vacia.");

        T valor = elementos[frente];
        frente = (frente + 1) % capacidad;
        cantidad--;
        return valor;
    }

    public T Front()
    {
        if (IsEmpty())
            throw new InvalidOperationException("La cola esta vacia.");

        return elementos[frente];
    }

    public bool IsEmpty() => cantidad == 0;

    public void Mostrar()
    {
        if (IsEmpty())
        {
            Console.WriteLine("(vacía)");
            return;
        }

        for (int i = 0; i < cantidad; i++)
        {
            int indice = (frente + i) % capacidad;
            Console.Write(elementos[indice] + " -> ");
        }

        Console.WriteLine();
    }
}

// Clase principal: punto de entrada del programa
class Program
{
    static void Main(string[] args)
    {
        Cola<string> prueba = new Cola<string>(3);

        Console.WriteLine(prueba.IsEmpty()); // True

        prueba.Enqueue("A");
        prueba.Enqueue("B");
        prueba.Enqueue("C");

        Console.WriteLine(prueba.Front());   // A
        Console.WriteLine(prueba.Dequeue()); // A

        prueba.Enqueue("D");

        prueba.Mostrar(); // B -> C -> D ->
    }
}
