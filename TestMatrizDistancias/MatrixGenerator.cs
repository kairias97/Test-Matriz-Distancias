using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace TestMatrizDistancias
{
    public class MatrixGenerator
    {
        public static List<Nodo> GenerarMatrizDistancias(int cantidadAtracciones)
        {
            List<Nodo> Nodos = new List<Nodo>();
            //Simulando una base preexistente de atracciones que se generan primero antes de ser agregadas a la matriz
            for (int i = 0; i < cantidadAtracciones; i++)
            {
                Nodos.Add(new Nodo
                {
                    Id = i,
                    GeoLocation = new GeoPoint
                    {
                        Latitude = RandomUtil.RandomNumberBetween(-10, 10),
                        Longitude = RandomUtil.RandomNumberBetween(-170, 70)
                    },
                    Aristas = new List<ConexionNodo>()
                });
            }
            //Calculo inicial de las distancias de cada arista
            foreach (var nodo in Nodos)
            {
                nodo.Aristas = (from n in Nodos
                                where n.Id != nodo.Id
                                select new ConexionNodo
                                {
                                    IdNodoDestino = n.Id,
                                    Distancia = DistanceCalculator.GetKmDistance(nodo.GeoLocation, n.GeoLocation)
                                }).ToList();
            }
            return Nodos;

        }

        public static List<Nodo> ProcesarNuevaAtraccion(List<Nodo> nodos, Nodo nuevoNodo)
        {
            nuevoNodo.Aristas = (from n in nodos
                                 select new ConexionNodo
                                 {
                                     Distancia = DistanceCalculator.GetKmDistance(nuevoNodo.GeoLocation, n.GeoLocation),
                                     IdNodoDestino = n.Id
                                 }).ToList();
            foreach (var nodo in nodos)
            {
                nodo.Aristas.Add(new ConexionNodo
                {
                    Distancia = DistanceCalculator.GetKmDistance(nodo.GeoLocation, nuevoNodo.GeoLocation),
                    IdNodoDestino = nuevoNodo.Id
                });
            }
            nodos.Add(nuevoNodo);
            return nodos;
        }

        public static List<Nodo> ProcesarAtraccionMenos(List<Nodo> nodos, int IdNodoAQuitar)
        {
            nodos.RemoveAll(n => n.Id == IdNodoAQuitar);

            foreach (var nodo in nodos)
            {
                nodo.Aristas.RemoveAll(n => n.IdNodoDestino == IdNodoAQuitar);
            }
            return nodos;
        }
    }
}
