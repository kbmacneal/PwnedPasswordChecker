using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace pwnedpasswordchecker
{
    class Program
    {
        static void Main (string[] args)
        {
            string password = args[0];

            Console.WriteLine("Your Hashed Password:");

            Console.WriteLine(Hash(password));

            Console.Write("Has been cracked ");
            Console.Write(makeRequest(Hash(password)).GetAwaiter().GetResult().Sum(e=>e.Value));
            Console.Write(" times.");
            Console.WriteLine();
        }

        public static async Task<Dictionary<string,int>> makeRequest(string hash)
        {
            var base_url = "https://api.pwnedpasswords.com/range";

            Dictionary<string,int> rtn = new Dictionary<string, int>();

            var result = await base_url
                .AppendPathSegment (hash.Substring(0,5))
                .GetAsync()
                .ReceiveString ();

            result.Split(System.Environment.NewLine).ToList().ForEach(e=>rtn.Add(e.Split(":")[0],Int32.Parse(e.Split(":")[1])));

            return rtn;
        }

        static string Hash (string input)
        {
            var hash = new SHA1Managed ().ComputeHash (Encoding.UTF8.GetBytes (input));
            return string.Concat (hash.Select (b => b.ToString ("x2")));
        }
    }
}