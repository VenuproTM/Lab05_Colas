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

class Clinica
{
    private Cola<string> colaMujeres = new Cola<string>(); // Ginecología
    private Cola<string> colaHombres = new Cola<string>(); // Andrología

    public void RegistrarPaciente(string nombre, char genero)
    {
        if (genero == 'M' || genero == 'm')
        {
            colaMujeres.Enqueue(nombre);
            Console.WriteLine($"{nombre} se unió a la cola de Ginecología.");
        }
        else
        {
            colaHombres.Enqueue(nombre);
            Console.WriteLine($"{nombre} se unió a la cola de Andrología.");
        }
    }

    public void AtenderGinecologia()
    {
        if (colaMujeres.IsEmpty())
            Console.WriteLine("No hay pacientes en espera en Ginecología.");
        else
            Console.WriteLine($"Ginecología atiende a: {colaMujeres.Dequeue()}");
    }

    public void AtenderAndrologia()
    {
        if (colaHombres.IsEmpty())
            Console.WriteLine("No hay pacientes en espera en Andrología.");
        else
            Console.WriteLine($"Andrología atiende a: {colaHombres.Dequeue()}");
    }

    public void MostrarColas()
    {
        Console.Write("Cola Ginecología: "); colaMujeres.Mostrar();
        Console.Write("Cola Andrología:  "); colaHombres.Mostrar();
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

        // 2. Probando la clase Clinica
        Console.WriteLine("--- Pruebas de la Clase Clinica ---");



        Clinica miClinica = new Clinica();

        // 1. Registramos a cuatro pacientes: "Ana" (M), "Rosa" (M), "Luis" (H) y "Carlos" (H)
        Console.WriteLine("--- REGISTRO DE PACIENTES ---");
        miClinica.RegistrarPaciente("Ana", 'M');
        miClinica.RegistrarPaciente("Rosa", 'M');
        miClinica.RegistrarPaciente("Luis", 'H');
        miClinica.RegistrarPaciente("Carlos", 'H');

        // 2. Verificando con MostrarColas() que cada paciente quedó en la cola correspondiente
        Console.WriteLine("\n--- ESTADO INICIAL DE LAS COLAS ---");
        miClinica.MostrarColas();

        // 3. Invocamos dos veces AtenderGinecologia() y una vez AtenderAndrologia()
        Console.WriteLine("\n--- ATENDIENDO PACIENTES ---");
        miClinica.AtenderGinecologia(); // Debe atender a Ana primero (FIFO)
        miClinica.AtenderGinecologia(); // Debe atender a Rosa después
        miClinica.AtenderAndrologia(); // Debe atender a Luis primero

        // 4. Comprobando el estado final (la cola de Andrología debe conservar a "Carlos" en espera)
        Console.WriteLine("\n--- ESTADO FINAL DE LAS COLAS ---");
        miClinica.MostrarColas();
    }
}
