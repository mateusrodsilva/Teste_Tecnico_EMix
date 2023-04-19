using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TesteCandidatoTriangulo
{
    public class Triangulo
    {
        /// <summary>
        ///    6
        ///   3 5
        ///  9 7 1
        /// 4 6 8 4
        /// Um elemento somente pode ser somado com um dos dois elementos da próxima linha. Como o elemento 5 na Linha 2 pode ser somado com 7 e 1, mas não com o 9.
        /// Neste triangulo o total máximo é 6 + 5 + 7 + 8 = 26
        /// 
        /// Seu código deverá receber uma matriz (multidimensional) como entrada. O triângulo acima seria: [[6],[3,5],[9,7,1],[4,6,8,4]]
        /// </summary>
        /// <param name="dadosTriangulo"></param>
        /// <returns>Retorna o resultado do calculo conforme regra acima</returns>
        public int ResultadoTriangulo(string dadosTriangulo)
        {
            int[][] triangle = JsonSerializer.Deserialize<int[][]>(dadosTriangulo);

            for (int i = triangle.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j <= i; j++)
                {
                    int[] nextLine = triangle[i + 1];
                    int maxSum = Math.Max(nextLine[j], nextLine[j + 1]);
                    triangle[i][j] += maxSum;
                }
            }

            return triangle[0][0];
            // return 0;
        }
    }
}
