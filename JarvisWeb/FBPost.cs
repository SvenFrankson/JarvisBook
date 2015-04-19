using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisWeb
{
    class FBPost : FBMessage
    {
        private List<FBComment> comments;
        public List<FBComment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public FBPost() : base ()
        {
            this.comments = new List<FBComment>();
        }

        public void Add(FBComment comment)
        {
            FBComment found = null;
            int newIndex = 0;

            // Search if given post already exists in the posts list.
            foreach (FBComment c in this.comments)
            {
                if (c.Date == comment.Date)
                {
                    if (c.Author.Name == comment.Author.Name)
                    {
                        found = c;
                        break;
                    }
                }
                else if (c.Date < comment.Date)
                {
                    break;
                }
                newIndex++;
            }

            // If given post already exists, update its informations.
            if (found != null)
            {
                found.Content = comment.Content;
                found.LikeCount = comment.LikeCount;
            }

            // Otherwise, it's added where it's supposed to be, in chronological order
            else
            {
                this.comments.Insert(newIndex, comment);
            }
        }

        public void Print(StreamWriter output)
        {
            output.WriteLine("    # Post by " + this.Author.Name + "(" + this.Author.Url + ") #");
            output.WriteLine("      " + this.Date + ".");
            output.WriteLine("      " + this.Content + ".");
            output.WriteLine("      " + this.LikeCount + " people like this.");

            foreach (FBComment comment in this.comments)
            {
                comment.Print(output);
            }

            output.WriteLine("");
        }
    }
}
