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

            Console.Write ("Your Hashed Password ");
            Console.Write (Hash (password));
            Console.WriteLine ();

            Console.Write ("Has been cracked ");
            var times = makeRequest (Hash (password)).GetAwaiter ().GetResult ().Sum (e => e.Value);
            Console.Write (times);
            Console.Write (" times.");
            Console.WriteLine ();

            if(times > 0) Console.WriteLine("DO NOT USE THIS PASSWORD.");
        }

        public static async Task<Dictionary<string, int>> makeRequest (string hash)
        {
            var base_url = "https://api.pwnedpasswords.com/range";

            Dictionary<string, int> rtn = new Dictionary<string, int> ();

            var result = await base_url
                .AppendPathSegment (hash.Substring (0, 5))
                .GetAsync ()
                .ReceiveString ();

            result.Split (System.Environment.NewLine).ToList ().Where (e => hash.Substring (5).ToUpper() == e.Split(":")[0]).ToList ().ForEach (e => rtn.Add (e.Split (":") [0], Int32.Parse (e.Split (":") [1])));

            if (rtn.Count == 0)
            {
                rtn.Add ("NEVERCRACKED", 0);
            }

            return rtn;
        }

        static string Hash (string input)
        {
            var hash = new SHA1Managed ().ComputeHash (Encoding.UTF8.GetBytes (input));
            return string.Concat (hash.Select (b => b.ToString ("x2")));
        }
    }
}