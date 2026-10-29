
using System;

// ========================================
// CLASE COLA GENERICA
// ========================================
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
        {
            throw new InvalidOperationException("La cola esta llena.");
        }

        final = (final + 1) % capacidad;
        elementos[final] = valor;
        cantidad++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("La cola esta vacia.");
        }

        T valor = elementos[frente];

        frente = (frente + 1) % capacidad;
        cantidad--;

        return valor;
    }

    public bool IsEmpty()
    {
        return cantidad == 0;
    }
}

// ========================================
// CLASE CONTROL DE TRAFICO
// ========================================
class ControlTrafico
{
    private Cola<string>[] carriles = new Cola<string>[4];

    private string[] nombres = { "Norte", "Sur", "Este", "Oeste" };

    public ControlTrafico()
    {
        for (int i = 0; i < 4; i++)
        {
            carriles[i] = new Cola<string>();
        }
    }

    public void AgregarVehiculo(int carril, string placa)
    {
        if (carril < 0 || carril >= 4)
        {
            Console.WriteLine("Carril invalido.");
            return;
        }

        carriles[carril].Enqueue(placa);

        Console.WriteLine($"Vehiculo {placa} llega al carril {nombres[carril]}.");
    }

    public void SimularSemaforo()
    {
        bool hayVehiculos = true;
        int ronda = 1;

        while (hayVehiculos)
        {
            Console.WriteLine($"\n--- Ronda {ronda} ---");

            hayVehiculos = false;

            for (int i = 0; i < 4; i++)
            {
                if (!carriles[i].IsEmpty())
                {
                    string placa = carriles[i].Dequeue();

                    Console.WriteLine(
                        $"Semaforo {nombres[i]}: pasa el vehiculo {placa}"
                    );

                    hayVehiculos = true;
                }
                else
                {
                    Console.WriteLine(
                        $"Semaforo {nombres[i]}: sin vehiculos, se omite el turno"
                    );
                }
            }

            ronda++;
        }

        Console.WriteLine(
            "\nTodos los vehiculos de la interseccion han sido atendidos."
        );
    }
}

// ========================================
// PROGRAMA PRINCIPAL
// ========================================
class Program
{
    static void Main()
    {
        ControlTrafico trafico = new ControlTrafico();

        Console.WriteLine("===== CONTROL DE TRAFICO =====\n");

        // Vehiculos del carril Norte
        trafico.AgregarVehiculo(0, "N1");
        trafico.AgregarVehiculo(0, "N2");
        trafico.AgregarVehiculo(0, "N3");

        // Vehiculos del carril Sur
        trafico.AgregarVehiculo(1, "S1");

        // Vehiculos del carril Este
        trafico.AgregarVehiculo(2, "E1");
        trafico.AgregarVehiculo(2, "E2");

        // Simulacion del semaforo
        trafico.SimularSemaforo();
    }
}
