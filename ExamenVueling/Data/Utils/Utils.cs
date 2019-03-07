using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Utils
{
    public class Utils
    {
        public class CurrencyCalc
        {
            

            public string[] CoinNodes;
            public double[,] MainGraph;

            public double Dijkstra(string source, int verticesCount, string destination)
            {
                double[] distance = new double[verticesCount];
                bool[] shortestPath = new bool[verticesCount];

                for (int i = 0; i < verticesCount; ++i)
                {
                    distance[i] = int.MaxValue;
                    shortestPath[i] = false;
                }
                int ps = LocationNode(source);
                distance[ps] = 1;

                for (int count = 0; count < verticesCount - 1; ++count)
                {
                    int u = SearchMinimumDistance(distance, shortestPath, verticesCount);
                    shortestPath[u] = true;

                    for (int v = 0; v < verticesCount; ++v)
                        if (!shortestPath[v] && Convert.ToBoolean(MainGraph[u, v]) && distance[u] != int.MaxValue && distance[u] * MainGraph[u, v] < distance[v])
                            distance[v] = distance[u] * MainGraph[u, v];
                }
                double dist = 0;
                int p = LocationNode(destination);
                for (int i = 0; i < verticesCount; ++i)
                    if (i == p)
                    {
                        dist = distance[i];
                    }
                return dist;
            }

            private static int SearchMinimumDistance(double[] distance, bool[] shortestPathTreeSet, int verticesCount)
            {
                double min = int.MaxValue;
                int minIndex = 0;

                for (int v = 0; v < verticesCount; ++v)
                {
                    if (shortestPathTreeSet[v] == false && distance[v] <= min)
                    {
                        min = distance[v];
                        minIndex = v;
                    }
                }
                return minIndex;
            }
            
            public void AddWay(string origen, string destino, float distancia)
            {
                int n1 = LocationNode(origen);
                int n2 = LocationNode(destino);
                MainGraph[n1, n2] = distancia;
                MainGraph[n2, n1] = distancia;
            }
            
            private int LocationNode(string nodo)
            {
                for (int i = 0; i < CoinNodes.Length; i++)
                {
                    if (CoinNodes[i] == nodo) return i;
                }
                return -1;
            }
            
            public void AddCoin(string coins)
            {
                coins = coins.TrimEnd('*');
                CoinNodes = coins.Split("*");
                MainGraph = new double[CoinNodes.Length, CoinNodes.Length];
            }
        }



    }
}
