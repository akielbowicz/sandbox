using System;
using System.Linq;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// dotnet run --project src/BenchmarkOutput/BenchmarkOutput.csproj --configuration=Release
namespace BenchmarkOutput
{
    public class Md5VsSha256
    {
        private SHA256 sha256 = SHA256.Create();
        private MD5 md5 = MD5.Create();
        private byte[] data;

        [Params(1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            data = new byte[N];
            new Random().NextBytes(data);
        }

        [Benchmark]
        public double Sha256() => sha256.ComputeHash(data).Sum(e => (double)e);

        [Benchmark]
        public double Md5() => md5.ComputeHash(data).Sum(e => (double)e);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Md5VsSha256>();
        }
    }
}