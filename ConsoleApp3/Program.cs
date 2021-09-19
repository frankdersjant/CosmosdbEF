using System;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("CosmosDB and EF : a marriage made in Heaven?");

            await CosmosDBEfDemo.Run();
        }
    }
}
