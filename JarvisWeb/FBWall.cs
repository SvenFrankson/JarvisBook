using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisWeb
{
    class FBWall
    {
        private string name;
        private string url;
        private List<FBPost> posts;

        public FBWall(string name, string url)
        {
            this.name = name;
            this.url = url;
            this.posts = new List<FBPost>();
        }

        public void Add(FBPost post)
        {
            FBPost found = null;
            int newIndex = 0;

            // Search if given post already exists in the posts list.
            foreach (FBPost p in this.posts)
            {
                if (p.Date == post.Date)
                {
                    if (p.Author.Name == post.Author.Name)
                    {
                        found = p;
                        break;
                    }
                }
                else if (p.Date < post.Date) {
                    break;
                }
                newIndex++;
            }

            // If given post already exists, update its informations.
            if (found != null)
            {
                found.Content = post.Content;
                found.LikeCount = post.LikeCount;
                found.Comments = post.Comments;
            }

            // Otherwise, it's added where it's supposed to be, in chronological order
            else
            {
                this.posts.Insert(newIndex, post);
            }
        }

        public void Print(StreamWriter output)
        {
            output.WriteLine("  # Wall " + this.name + "(" + this.url + ") #");
            foreach (FBPost post in this.posts)
            {
                post.Print(output);
            }
        }
    }
}
