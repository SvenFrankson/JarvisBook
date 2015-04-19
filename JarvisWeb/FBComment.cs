using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisWeb
{
    class FBComment : FBMessage
    {
        public void Print(StreamWriter output)
        {
            output.WriteLine("      # Comment by " + this.Author.Name + "(" + this.Author.Url + ") #");
            output.WriteLine("        " + this.Date + ".");
            output.WriteLine("        " + this.Content + ".");
            output.WriteLine("        " + this.LikeCount + " people like this.");
        }
    }
}
