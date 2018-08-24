using System;
using System.Collections.Generic;
using System.Linq;
namespace TestMatrizDistancias
{
    class Program
    {
        static void Main(string[] args)
        {
            //Para simular una matriz ya existente de distancias para x ciudad, esto simula solo la parte de las matrices
            //En este ejemplo simula solo una ciudad 
            int cantidadAtracciones = 200;
            //Este proceso como tal es rapido pero esta parte igual es solo para fines de prueba
            List<Nodo> MatrizNodos = MatrixGenerator.GenerarMatrizDistancias(cantidadAtracciones);
            //Esto puede tardar dependiendo de la cantidad de items, para 10 items se tardo en mi pc unos 333ms
            //para 200 se tardo 244ms 
            string matrizNodosTextoJson = Newtonsoft.Json.JsonConvert.SerializeObject(MatrizNodos);
            //De la bd lo que se obtendrá es el string de arriba y tocará deserializarlo primero en una lista algo compleja antes de generar la matriz de distancias 
            //ya en el formato de solo double
            //Para deserializar el string se tardó 132ms
            List<Nodo> matrizObtenidaBD = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Nodo>>(matrizNodosTextoJson);
            
            //Ahora procedemos a generar la matriz de doubles, que para 200 elementos se tarda en generar la matriz de distancias en 9ms
            double[][] matrizDistancias = (from nodoBD in matrizObtenidaBD
                                           select nodoBD.Aristas.Select(a => a.Distancia).ToArray()
                                          ).ToArray();

            //Ahora el siguiente ejemplo es trabajando con la matrizObtenidaBD para simular añadir una nueva atraccion para asi generar una nueva matriz
            var nuevoNodo = new Nodo
            {
                Id = 500,
                GeoLocation = new GeoPoint { Latitude = RandomUtil.RandomNumberBetween(-10, 10), Longitude = RandomUtil.RandomNumberBetween(-100, 100) },
                Aristas = new List<ConexionNodo>()
            };
            Console.WriteLine($"El tamaño de la matriz antes de agregar un nuevo nodo es de {matrizObtenidaBD.Count}");
            matrizObtenidaBD = MatrixGenerator.ProcesarNuevaAtraccion(matrizObtenidaBD, nuevoNodo);
            Console.WriteLine($"El tamaño de la matriz luego de agregar un nuevo nodo es de {matrizObtenidaBD.Count}");

            //Ahora simulamos el quitar un nodo de la bd actual, usamos el id del nodo que agregamos antes
            matrizObtenidaBD = MatrixGenerator.ProcesarAtraccionMenos(matrizObtenidaBD, 500);
            Console.WriteLine($"El tamaño de la matriz luego de quitar el nodo agregado recientemente es de {matrizObtenidaBD.Count}");


            //Console.WriteLine(matrizNodosTextoJson);

            Console.ReadLine();
        }
    }
}
