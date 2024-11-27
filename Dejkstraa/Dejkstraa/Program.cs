using System;
using System.Collections.Generic;

public class Dijkstra
{
    public class Edge
    {
        public int Target { get; set; }
        public int Weight { get; set; }

        public Edge(int target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }

    public static int[] FindShortestPaths(int n, List<Edge>[] graph, int start)
    {
        // Массив расстояний от стартовой вершины
        int[] distances = new int[n];
        for (int i = 0; i < n; i++)
        {
            distances[i] = int.MaxValue;
        }
        distances[start] = 0;

        // Приоритетная очередь с минимальными расстояниями
        var pq = new SortedSet<(int distance, int vertex)>();
        pq.Add((0, start));

        while (pq.Count > 0)
        {
            // Извлекаем вершину с минимальным расстоянием
            var (currentDistance, currentVertex) = pq.Min;
            pq.Remove(pq.Min);

            // Если расстояние больше текущего, пропускаем
            if (currentDistance > distances[currentVertex]) continue;

            // Рассматриваем все рёбра для текущей вершины
            foreach (var edge in graph[currentVertex])
            {
                int neighbor = edge.Target;
                int weight = edge.Weight;

                int newDistance = currentDistance + weight;

                // Если найден более короткий путь, обновляем
                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    pq.Add((newDistance, neighbor)); // Добавляем в очередь с новым расстоянием
                }
            }
        }

        return distances;
    }
}

public class Program
{
    public static void Main()
    {
        // Ввод количества вершин и рёбер
        Console.WriteLine("Введите количество вершин в графе:");
        int n = int.Parse(Console.ReadLine());

        // Создание списка смежности для графа
        var graph = new List<Dijkstra.Edge>[n];
        for (int i = 0; i < n; i++)
            graph[i] = new List<Dijkstra.Edge>();

        Console.WriteLine("Введите количество рёбер:");
        int m = int.Parse(Console.ReadLine());

        // Ввод рёбер (начало, конец, вес)
        Console.WriteLine("Введите рёбра графа в формате 'начало конец вес':");
        for (int i = 0; i < m; i++)
        {
            var input = Console.ReadLine().Split();
            int u = int.Parse(input[0]);
            int v = int.Parse(input[1]);
            int weight = int.Parse(input[2]);

            // Добавляем рёбра в граф
            graph[u].Add(new Dijkstra.Edge(v, weight));
            // Если граф неориентированный, то добавляем обратное ребро
            // graph[v].Add(new Dijkstra.Edge(u, weight));
        }

        // Ввод стартовой вершины
        Console.WriteLine("Введите стартовую вершину:");
        int startVertex = int.Parse(Console.ReadLine());

        // Выполнение алгоритма Дейкстры
        var distances = Dijkstra.FindShortestPaths(n, graph, startVertex);

        // Вывод результатов
        Console.WriteLine("Кратчайшие расстояния от вершины " + startVertex + ":");
        for (int i = 0; i < n; i++)
        {
            if (distances[i] == int.MaxValue)
                Console.WriteLine($"До вершины {i}: нет пути");
            else
                Console.WriteLine($"До вершины {i}: {distances[i]}");
        }
    }
}
