using System;
using System.IO;
using System.Net.Sockets;
namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            var listener = new TcpListener(32333);
            listener.Start();

            var client = listener.AcceptTcpClient();

            using (var stream = client.GetStream())
            using(var br = new BinaryReader(stream))
            using(var bw = new BinaryWriter(stream))
            {
                var size = br.ReadInt32();
                var matrix1 = new int[size, size];
                var matrix2 = new int[size, size];
                var result =new int[size, size];

                ReceiveMatrix(br, matrix1);
                ReceiveMatrix(br, matrix2);
                
                MultMatrix(matrix1, matrix2, result);
                SendMatrix(bw, result);
            }

        }
        private static void MultMatrix (int[,] matrix1, int[,] matrix2, int[,] result)
        {
            result[1, 1] = 352;

        }
        private static void SendMatrix(BinaryWriter bw , int[,] matrix)
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
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write("{0}\t", matrix[i, j]);
                Console.WriteLine();
            }
        }
    }
}
