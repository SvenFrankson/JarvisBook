using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvenFrankson
{
    class FBMessage
    {
        private FBUser author;
        public FBUser Author
        {
            get
            {
                return this.author;
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

        private string content;
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        private int likeCount;
        public int LikeCount
        {
            get
            {
                return this.likeCount;
            }
            set
            {
                this.likeCount = value;
            }
        }

        public FBMessage()
        {
            this.author = new FBUser();
        }
    }
}
