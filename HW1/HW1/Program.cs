using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Этап_1
            Console.WriteLine("Введите кол-во вершин");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите кол-во дуг");
            int m = int.Parse(Console.ReadLine());
            int[] I = new int[m];
            int[] S = new int[n+1];
            for (int l = 0; l < m; l++)
            {
                Console.WriteLine($"Введите номер вершины, из которой выходит {l} дуга");
                I[l] = int.Parse(Console.ReadLine());
            }
            for (int k = 0; k < m; k++)//просмотр дуг сети
            {
                int i = I[k];//номер вершины, из которой выходит дуга k
                S[i + 1]++;//количество дуг, выходящих из вершмны i
            }

            //Этап_2
            //Заполнение массива входов
            for (int i = 1; i <= n; i++)
            {
                S[i] += S[i - 1];
            }

            //Этап_3
            //Расстановка дуг по местам
            int[] P = new int[S.Length-1];
            for(int i =1; i < P.Length; i++)
            {
                P[i] = S[i];
            }
            for(int i = 0;i < n-1; i++)
            { 
                for(int k = P[i]; k < S[i+1];)
                {
                    if (I[k] == i) { k++; }
                    else
                    {
                        int j = I[k];
                        int l = P[j];
                        int h = I[k];
                        I[k] = I[l];
                        I[l] = h;
                        P[j]++;
                    }
                }
            }
            //Вывод конечного результата
            for(int i = 0;i < m ; i++)
            {
                Console.WriteLine($"{i} дуга выходит из {I[i]} вершины");
            }
        }
    }
}
