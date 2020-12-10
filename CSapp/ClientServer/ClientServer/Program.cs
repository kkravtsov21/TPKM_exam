using System;
using System.IO;
using System.Net.Sockets;

namespace ClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix1 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        var matrix2 = new int[3, 3] { { 11, 12, 13 }, { 14, 15, 16 }, { 17, 18, 19 } };
            var res = new int[3, 3];

            var client = new TcpClient();
            client.Connect("localhost", 32333);

            using (var stream = client.GetStream())
            using (var bw  = new BinaryWriter(stream))
            using (var br = new  BinaryReader(stream))
            {
                bw.Write(matrix1.GetLength(0));
                SendMatrix(bw, matrix1);
                SendMatrix(bw, matrix2);
                ReceiveMatrix(br, res);
            }

            PrintMatrix(res);
            Console.ReadLine();



}
        private static void  SendMatrix(BinaryWriter bw , int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                bw.Write(matrix[i, j]);
        }
        private static void ReceiveMatrix(BinaryReader br, int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = br.ReadInt32();
        }
        private static void PrintMatrix(int[,] matrix)
        {
            for(int i = 0; i< matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write("{0}\t", matrix[i, j]);
                Console.WriteLine();
            }
        }
    }
}
