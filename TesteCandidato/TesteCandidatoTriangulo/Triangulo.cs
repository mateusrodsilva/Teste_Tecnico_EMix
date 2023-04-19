using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string[] linhas = dadosTriangulo.Trim('[', ']').Split("],[");
            int[][] matriz = new int[linhas.Length][];

            for (int i = 0; i < linhas.Length; i++)
            {
                string[] colunas = linhas[i].Split(",");
                matriz[i] = new int[colunas.Length];

                for (int j = 0; j < colunas.Length; j++)
                {
                    matriz[i][j] = int.Parse(colunas[j]);
                }
            }

            for (int i = matriz.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < matriz[i].Length; j++)
                {
                    int max = Math.Max(matriz[i + 1][j], matriz[i + 1][j + 1]);
                    matriz[i][j] += max;
                }
            }   

            return matriz[0][0];
            // return 0;
        }
    }
}
