using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisWeb
{
    static class AnoMaker
    {
        static private int anoIndex = 0;
        static private Dictionary<string, string> anoPairs;
        static private Dictionary<string, string> AnoPairs
        {
            get
            {
                if (anoPairs == null)
                {
                    anoPairs = new Dictionary<string, string>();
                }
                return anoPairs;
            }
        }

        static public string GetAnonymized(string name)
        {
            string anoName = "Anonymous";

            if (AnoPairs.Keys.Contains(name))
            {
                anoName = AnoPairs[name];
            }
            else
            {
                AnoPairs.Add(name, "Anonymous" + anoIndex);
                anoName = AnoPairs[name];
                anoIndex++;
            }

            return anoName;
        }
    }
}
