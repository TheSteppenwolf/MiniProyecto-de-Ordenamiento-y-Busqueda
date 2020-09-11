using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_Ejercicio01
{
    class Program
    {      
        static void Main(string[] args)
        {
            int[] arr = null, resArray = null;
            string[] text = null;
            string op1 = null, tempS = null;
            int temp = 0, res = 0;

            while(true)
            {
                #region Ingreso de datos               

                // Ingreso de los elementos del arreglo.
                while (true)
                {                    
                    try
                    {
                        Console.Write("Ingresa los elementos del arreglo: ");
                        text = Console.ReadLine().Trim().Split();
                        arr = new int[text.Length];
                        for (int i = 0; i < text.Length; i++) arr[i] = Int32.Parse(text[i]);
                    }
                    // Valida que el ingreso de los datos del arreglos sean enteros.
                    catch (System.FormatException)
                    {
                        Console.WriteLine("\nError: Los valores ingresados no son númericos!\n");
                        continue;
                    }
                    break;
                }

                #endregion

                #region Procesamiento y salida de la información

                // Muestra del arreglo ingresado.
                Console.Write("\nSu arreglo: ");
                foreach (var iter in arr) Console.Write($"{iter} "); Console.WriteLine();

                // Ordenamiento del arreglo.
                Shell(ref arr);
                Console.Write("Su arreglo ordenado: ");
                foreach (var iter in arr) Console.Write($"{iter} "); Console.WriteLine();

                // Area de búsqueda en el arreglo.                
                while(op1 != "6")
                {
                    op1 = "-";
                    Console.WriteLine();
                    Console.Write("Que desea buscar?\n\t" +
                        "1.Existencia de un elemento.\n\t" +
                        "2.Primera aparicion encontrada de un elemento.\n\t" +
                        "3.Indices de todas las apariciones de un elemento.\n\t" +
                        "4.Apariciones de un elemento.\n\t" +
                        "5.Las iteraciones de un elemento.\n\t" +
                        "6.Salir.\n" +
                        "Opcion: ");
                    op1 = Console.ReadLine().Trim();
                    switch(op1)
                    {
                        // Realiza la búsqueda de elemento dado con la finalidad de descubrir si este existe o no.
                        case ("1"):
                            temp = GetBusqueda();
                            tempS = GetExist(arr, temp) ? "existe" : "no existe";
                            Console.WriteLine($"El elemento {temp} {tempS}.");
                            break;

                        // Realiza la búsqueda de un elemento dado con la finalidad de devolver su primera aparición encontrada en el arreglo.
                        case ("2"):
                            temp = GetBusqueda();
                            res = GetIndex(arr, temp);
                            tempS = res != -1 ? $"se puede encontrar en el indice: {res.ToString()}" : "no existe";
                            Console.WriteLine($"El elemento {temp} {tempS}.");
                            break;

                        // Realiza la búsqueda de un elemento con la finalidad de devolver todas sus apariciones en el arreglo.
                        case ("3"):
                            temp = GetBusqueda();
                            resArray = GetIndeces(arr, temp);
                            if (resArray != null)
                            {
                                Console.Write($"El elemento {temp} se puede encontrar en los siguientes indices: ");
                                foreach (var iter in resArray) Console.Write(iter + " "); Console.WriteLine();
                            }
                            else Console.WriteLine($"El elemento {temp} no existe.");
                            resArray = null;
                            break;

                        case ("4"):
                            temp = GetBusqueda();
                            res = GetApariciones(arr, temp);
                            tempS = res != 0 ? $"aparece {res} veces" : "no existe";
                            Console.WriteLine($"El elemento {temp} {tempS}.");
                            break;

                        // Realiza la búsqueda de todas las iteraciones de un elemento.
                        case ("5"):
                            temp = GetBusqueda();
                            res = GetIteration(arr, temp);
                            tempS = res != -1 ? $"se repite {res} veces" : "no existe";
                            Console.WriteLine($"El elemento {temp} {tempS}.");
                            break;

                        // Salida del menu de búsqueda.
                        case ("6"):                            
                            break;

                        // Validación de ingreso erroneo de datos.
                        default:
                            Console.WriteLine("Error: Opcion seleccionada no disponible!");
                            break;
                    }
                }

                #endregion           

                #region Salida del programa

                // Pregunta si se desea ingresar un nuevo arreglo.
                Console.WriteLine();
                do
                {
                    Console.Write("Desea ingresar un nuevo arreglo? (S/N): "); op1 = Console.ReadLine().ToUpper();
                    if (op1 != "N" & op1 != "S") Console.WriteLine("Error: Opcion no valida!");
                } while (op1 != "N" & op1 != "S");
                // Se cierra el programa en caso de no requerir el ingreso de nuevos arreglos.
                if (op1 == "N") break;

                // Pregunta si se desea limpiar la pantalla de la consola.
                Console.WriteLine();
                do
                {
                    Console.Write("Desea limpiar la pantalla de la consola? (S/N): "); op1 = Console.ReadLine().ToUpper();
                    if (op1 != "N" & op1 != "S") Console.WriteLine("Error: Opcion no valida!");
                } while (op1 != "N" & op1 != "S");
                if (op1 == "S") Console.Clear();
                else { Console.WriteLine(); PrintLine(); Console.WriteLine(); }

                #endregion
            }
            Console.WriteLine("\n\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        #region Funciones de búsqueda y ordenamiento

        // Realiza el ordenamiento de un arreglo de enteros por el método de Shell de manera ascendente.
        static void Shell(ref int[] arr)
        {
            int gap = arr.Length / 2, temp;
            bool validator = true; 
            while(true)
            {
                validator = true;
                for (int i = 0; i < arr.Length; i++)
                {                    
                    if (i + gap < arr.Length && arr[i] > arr[gap + i])
                    {
                        temp = arr[i];
                        arr[i] = arr[gap + i];
                        arr[gap + i] = temp;
                        validator = false;
                    }                    
                }
                if (validator == true) gap/=2;
                if (validator == true & gap == 0) break;
            }
        }

        // Devuelve un valor booleano verdadero si existe un elemento en un arreglo y un valor falso si no existe.
        // El algoritmo usado es: Búsqueda dicotómica.
        static bool GetExist(int[] arr, int sValue)
        {
            int upper, lower, mid;
            upper = arr.Length-1;
            lower = 0;           
            while(lower <= upper)
            {
                mid = (upper + lower) / 2;
                if (sValue == arr[mid]) return true;
                else
                    if (sValue < arr[mid]) upper = mid - 1;
                    else lower = mid + 1;
            } return false;
        }

        // Devuelve el primer indice que se encentre un elemento en un arreglo y -1 si no lo encuentra.
        // El algoritmo usado es: Búsqueda dicotómica.
        static int GetIndex(int[] arr, int sValue)
        {
            int upper, lower, mid;
            upper = arr.Length - 1;
            lower = 0;
            while (lower <= upper)
            {
                mid = (upper + lower) / 2;
                if (sValue == arr[mid]) return mid;
                else
                    if (sValue < arr[mid]) upper = mid - 1;
                else lower = mid + 1;
            }
            return -1;
        }

        // Devuelve un arreglo con todos los indices en los que aparece un elemento en un arreglo y un arreglo con 0 elementos si no lo encuentra.
        // El algoritmo usado es: Búsqueda dicotómica.
        static int[] GetIndeces(int[] arr, int sValue)
        {
            int[] res = null;
            int index, counter = 0;
            if (!GetExist(arr, sValue)) return res;
            res = new int[0];
            while (true)
            {                                
                index = GetIndex(arr, sValue);
                if (index == -1) break;
                Array.Resize(ref res, res.Length + 1);
                if (!GetExist(res, index)) res[res.Length - 1] = index;
                else res[res.Length - 1] = index + counter;
                arr = arr.Where((val, idx) => idx != index).ToArray(); counter++;                
            }
            Shell(ref res);
            return res;
        }

        // Devuelve el número de apariciones de un elemento en un arreglo.
        static int GetApariciones(int[] arr, int sValue)
        {
            int[] temp = GetIndeces(arr, sValue);
            if(temp == null) return 0;
            return temp.Length;
        }

        // Devuelve el número de repeticiones de un elemento en un arreglo.
        static int GetIteration(int[] arr, int sValue)
        {
            int[] temp = GetIndeces(arr, sValue);
            if (temp == null) return -1;
            return temp.Length - 1;
        }

        #endregion

        #region Funciones para un ambiente amigable con el usuario

        // Pide al usuario un valor para buscar.
        static int GetBusqueda()
        {
            int temp;
            Console.Write("Ingresa el elemento que deseas buscar: ");
            while (true)
            {
                try { temp = Convert.ToInt32(Console.ReadLine()); }
                catch (System.FormatException)
                {
                    Console.WriteLine("\nError: Los valores ingresados no son númericos!\n");
                    continue;
                }
                break;
            }
            return temp;
        }

        // Imprime una línea de asteriscos.
        static void PrintLine()
        {
            for (int i = 0; i < 50; i++) Console.Write("*"); Console.WriteLine();
        }

        #endregion

    }
}
