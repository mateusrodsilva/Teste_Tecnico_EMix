using System.Text.Json;

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
            int total = triangle[0][0];
            int index = 0;

            for (int i = 1; i < triangle.Length; i++)
            {
                int[] currentLine = triangle[i];
                int left = currentLine[index];
                int right = currentLine.Length > index + 1 ? currentLine[index + 1] : 0;

                if (left > right)
                {
                    total += left;
                }
                else
                {
                    total += right;
                    index++;
                }
            }

            return total;
        }
    }
}
