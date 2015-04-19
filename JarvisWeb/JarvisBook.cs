//The MIT License (MIT)

//Copyright (c) 2015 SvenFrankson

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace SvenFrankson
{
    public partial class JarvisBook : Form
    {
        private SpeechSynthesizer vocalSynth;
        public List<string> menu;
        public List<string> action;

        public HtmlElement currentMessage;

        public JarvisBook()
        {
            this.vocalSynth = new SpeechSynthesizer();
            InitializeComponent();
            this.listBox1.KeyPress -= MenuKeyPressHandler;
            this.listBox1.KeyPress += MenuKeyPressHandler;
            this.LoadMenuMain();
        }

        private void ClearMenuDelegates()
        {
            this.listBox1.KeyPress -= MenuKeyPressHandler;
            this.listBox1.PreviewKeyDown -= ReadPostsKeyPressHandler;
            this.listBox1.SelectedIndexChanged -= ReadMenuOnChange;
            this.listBox1.SelectedIndexChanged -= InfoMenuOnChange;
        }

        public void MenuKeyPressHandler(Object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string command = "";
                string[] splitCommand = this.action[listBox1.SelectedIndex].Split(' ');
                if (splitCommand.Length > 0)
                {
                    command = splitCommand[0];
                }
                string commandArg = "";
                if (splitCommand.Length > 1)
                {
                    for (int i = 1; i < splitCommand.Length; i++)
                    {
                        commandArg += splitCommand[i] + ' ';
                    }
                }
                if (command == "GOTOACTU")
                {
                    webBrowser.Navigate("https://www.facebook.com");
                    this.LoadMenuWall();
                }
                else if (command == "GOTOGROUPS")
                {
                    webBrowser.Navigate("https://www.facebook.com/groups/?category=membership");
                    webBrowser.DocumentCompleted += PopulateMenuWithGroups;
                }
                else if (command == "GOTOFRIENDS")
                {
                    webBrowser.Navigate("https://www.facebook.com/sven.taton/friends_all");
                    this.LoadMenuWall();
                }
                else if (command == "GOTOMYPAGE")
                {
                    webBrowser.Navigate("https://www.facebook.com");
                    this.LoadMenuWall();
                }
                else if (command == "DEBUGUNETASSEDE")
                {
                    webBrowser.Navigate("https://www.facebook.com/groups/174190415929208/");
                    this.LoadMenuWall();
                }
                else if (command == "BACKTOMAINMENU")
                {
                    this.LoadMenuMain();
                }
                else if (command == "READPOSTS")
                {
                    this.FocusNextMessage();
                    this.LoadMenuInfoReadPosts();
                }
                else if (command == "WRITENEWPOST")
                {
                    
                }
                else if (command == "GOTOURL")
                {
                    webBrowser.Navigate(commandArg);
                    this.LoadMenuWall();
                }
            }
        }

        public void ReadPostsKeyPressHandler(Object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.FocusPrevMessage();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.FocusNextMessage();
            }
            else if (e.KeyCode == Keys.Left)
            {
                this.currentMessage = null;
                this.LoadMenuWall();
            }
        }

        private void LoadMenuMain()
        {
            this.menu = new List<string>();
            this.action = new List<string>();
            this.menu.Add("Actualités");
            this.action.Add("GOTOACTU");
            this.menu.Add("Groupes");
            this.action.Add("GOTOGROUPS");
            this.menu.Add("Amis");
            this.action.Add("GOTOFRIENDS");
            this.menu.Add("Ma page");
            this.action.Add("GOTOMYPAGE");
            this.menu.Add("UneTasseDe");
            this.action.Add("DEBUGUNETASSEDE");
            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.SelectedIndex = 0;
            this.listBox1.Select();
            this.listBox1.KeyPress += MenuKeyPressHandler;
            this.listBox1.SelectedIndexChanged += ReadMenuOnChange;
        }

        private void LoadMenuWall()
        {
            this.menu = new List<string>();
            this.action = new List<string>();
            this.menu.Add("Lire les messages");
            this.action.Add("READPOSTS");
            this.menu.Add("Ecrire un nouveau message");
            this.action.Add("WRITENEWPOST");
            this.menu.Add("Retour");
            this.action.Add("BACKTOMAINMENU");
            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.SelectedIndex = 0;
            this.listBox1.Select();
            this.listBox1.KeyPress += MenuKeyPressHandler;
            this.listBox1.SelectedIndexChanged += ReadMenuOnChange;
        }

        private void LoadMenuInfoReadPosts()
        {
            this.menu = new List<string>();
            this.action = new List<string>();
            this.menu.Add("Lecture du mur");
            this.action.Add("VOID");
            this.menu.Add("Haut : Message précedent");
            this.action.Add("VOID");
            this.menu.Add("Bas : Message suivant");
            this.action.Add("VOID");
            this.menu.Add("Gauche : Retour");
            this.action.Add("VOID");
            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.ClearSelected();
            this.listBox1.Select();
            this.listBox1.PreviewKeyDown += ReadPostsKeyPressHandler;
            this.listBox1.SelectedIndexChanged += InfoMenuOnChange;
        }

        private void FocusPrevMessage()
        {
            HtmlDocument doc = webBrowser.Document;
            HtmlElement lastFound = null;

            if (this.currentMessage == null)
            {
                return;
            }

            output.Text = "Not Found";
            int index = 0;
            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.GetAttribute("className").Contains("userContentWrapper"))
                {
                    index++;
                    if (el0 == this.currentMessage)
                    {
                        output.Text = "Found";
                        break;
                    }
                    lastFound = el0;
                }
            }

            if (lastFound != null)
            {
                this.currentMessage = lastFound;
            }

            output.Text += " " + index;
            string author = "Anonyme";
            string date = "inconnu";
            string content = "";
            int likeCount = 0;

            foreach (HtmlElement el1 in currentMessage.All)
            {
                if (el1.GetAttribute("className") == "fwb fcg")
                {
                    HtmlElement aProfile = el1.FirstChild;
                    author = DataFromHtmlElement(aProfile);

                    foreach (HtmlElement el2 in el1.Parent.Parent.Parent.All)
                    {
                        if (el2.TagName == "ABBR")
                        {
                            date = UnixTimeStampToDateTime(el2.GetAttribute("data-utime")).ToShortDateString();
                        }
                    }
                }
                else if (el1.GetAttribute("className").Contains("userContent"))
                {
                    foreach (HtmlElement el2 in el1.All)
                    {
                        content += DataFromHtmlElement(el2) + " ";
                    }
                }
            }

            string voiceOutput = "";
            voiceOutput += author + " le " + date + " : " + content + ". " + likeCount + " personnes aiment.";
            output.Text = voiceOutput;
            this.vocalSynth.SpeakAsyncCancelAll();
            this.vocalSynth.SpeakAsync(voiceOutput);

            this.currentMessage.Focus();
            this.currentMessage.ScrollIntoView(true);
        }

        private void FocusNextMessage()
        {
            HtmlDocument doc = webBrowser.Document;

            output.Text = "Not Found";
            int index = 0;
            bool found = false;
            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.GetAttribute("className").Contains("userContentWrapper"))
                {
                    index++;
                    if (found == true)
                    {
                        this.currentMessage = el0;
                        break;
                    }
                    else if (this.currentMessage == null)
                    {
                        this.currentMessage = el0;
                        break;
                    }
                    else if (el0 == this.currentMessage)
                    {
                        output.Text = "Found";
                        found = true;
                    }
                }
            }

            output.Text += " " + index;
            string author = "Anonyme";
            string date = "inconnu";
            string content = "";
            int likeCount = 0;

            if (currentMessage == null)
            {
                return;
            }

            foreach (HtmlElement el1 in currentMessage.All)
            {
                if (el1.GetAttribute("className") == "fwb fcg")
                {
                    HtmlElement aProfile = el1.FirstChild;
                    author = DataFromHtmlElement(aProfile);

                    foreach (HtmlElement el2 in el1.Parent.Parent.Parent.All)
                    {
                        if (el2.TagName == "ABBR")
                        {
                            date = UnixTimeStampToDateTime(el2.GetAttribute("data-utime")).ToString();
                        }
                    }
                }
                else if (el1.GetAttribute("className").Contains("userContent"))
                {
                    foreach (HtmlElement el2 in el1.All)
                    {
                        content += DataFromHtmlElement(el2) + " ";
                    }
                }
            }

            string voiceOutput = "";
            voiceOutput += author + " le " + date + " : " + content + ". " + likeCount + "aiment.";
            output.Text = voiceOutput;
            this.vocalSynth.SpeakAsyncCancelAll();
            this.vocalSynth.SpeakAsync(voiceOutput);


            this.currentMessage.Focus();
            this.currentMessage.ScrollIntoView(true);
        }

        private void PopulateMenuWithGroups(object sender, EventArgs e)
        {
            this.webBrowser.DocumentCompleted -= PopulateMenuWithGroups;

            this.menu = new List<string>();
            this.action = new List<string>();

            HtmlDocument doc = webBrowser.Document;

            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.TagName == "A")
                {
                    if (el0.GetAttribute("className") == "groupsRecommendedTitle")
                    {
                        this.menu.Add(DataFromHtmlElement(el0));
                        this.action.Add("GOTOURL " + el0.GetAttribute("href"));
                    }
                }
            }

            this.menu.Add("Retour");
            this.action.Add("BACKTOMAINMENU");

            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.SelectedIndex = 0;
            this.listBox1.Select();
            this.listBox1.KeyPress += MenuKeyPressHandler;
            this.listBox1.SelectedIndexChanged += ReadMenuOnChange;
        }

        private void FocusNewMessageBox()
        {
            output.Text = "Focus new message Box";

            HtmlDocument doc = webBrowser.Document;

            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.TagName == "DIV")
                {
                    if (el0.GetAttribute("className") == "_55d0")
                    {
                        foreach (HtmlElement el1 in el0.All)
                        {
                            if (el1.TagName == "TEXTAREA")
                            {
                                el1.Focus();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            //webBrowser.Navigate("https://www.facebook.com/sven.taton"); // Sven Taton
            //webBrowser.Navigate("https://www.facebook.com/groups/372937862903076/"); // Groupe test
            //webBrowser.Navigate("https://www.facebook.com/groups/174190415929208/"); // Unetassede
            //webBrowser.Navigate("https://www.facebook.com/"); // Accueil
            //webBrowser.DocumentCompleted += OnFacebookCompleted;
            this.DebugOutputBody();
        }

        public bool IsTextNode(HtmlElement element) {
              var result = false;
              var nativeNode = element.DomElement as mshtml.IHTMLDOMNode;
              if (nativeNode != null) {
                  var nodeType = nativeNode.nodeType;
                  result = nodeType == 3; // -- TextNode: http://msdn.microsoft.com/en-us/library/aa704085(v=vs.85).aspx
              }
              return result;
        }

        public string DataFromHtmlElement(HtmlElement e)
        {mshtml.IHTMLDOMNode node = e.DomElement as mshtml.IHTMLDOMNode;

            node = node.firstChild;

            string s = "";

            while (node != null)
            {
                if (node.nodeType == 3)
                {
                    s += node.nodeValue as string;
                    node = node.nextSibling;
                }
                else
                {
                    node = node.nextSibling;
                }
            }

            return s;
        }

        public static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            double timeStamp = Double.Parse(unixTimeStamp);
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void WriteYourself(StreamWriter stream, int indent, HtmlElement e)
        {
            string s = "" + indent;
            while (s.Length < indent + 3)
            {
                s += " ";
            }
            string sEclassName = e.GetAttribute("className");
            if (sEclassName != "") { sEclassName = "class=\"" + sEclassName + "\" "; }
            string sEid = e.GetAttribute("id");
            if (sEid != "") { sEid = "id=\"" + sEid + "\" "; }
            string sEhref = e.GetAttribute("href");
            if (sEhref != "") { sEhref = "href=\"" + sEhref + "\" "; }
            string sEtitle = e.GetAttribute("title");
            if (sEtitle != "") { sEtitle = "title=\"" + sEtitle + "\" "; }
            string sEsrc = e.GetAttribute("src");
            if (sEsrc != "") { sEsrc = "src=\"" + sEsrc + "\" "; }
            string sEdataUtime = e.GetAttribute("data-utime");
            if (sEdataUtime != "") { sEdataUtime = "data-utime=\"" + sEdataUtime + "\" "; }

            s += "<" + e.TagName + " " + sEclassName + sEid + sEhref + sEtitle + sEsrc + sEdataUtime + "> " + DataFromHtmlElement(e);
            
            stream.WriteLine(s);
            foreach (HtmlElement c in e.Children)
            {
                WriteYourself(stream, indent + 1, c);
            }
        }

        private void PostThisCrap(string crap)
        {
            string content = "JWAutopost : ";
            content += crap;
            HtmlDocument doc = webBrowser.Document;
            HtmlElement pushButton = null;

            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.TagName == "DIV")
                {
                    if (el0.GetAttribute("className") == "_55d0")
                    {
                        foreach (HtmlElement el1 in el0.All)
                        {
                            if (el1.TagName == "TEXTAREA")
                            {
                                el1.Focus();
                                el1.InnerText = content;
                            }
                            else if (el1.TagName == "BUTTON")
                            {
                                if (DataFromHtmlElement(el1) == "Publier")
                                {
                                    pushButton = el1;
                                }
                            }
                        }
                    }
                }
            }

            if (pushButton != null)
            {
                pushButton.InvokeMember("Click");
            }
        }

        // Putain si ça marche ça va être un joyeux bordel, mais c'est marrant.
        private void CommentEverythingOnScreen()
        {
            output.Text = "";
            HtmlDocument doc = webBrowser.Document;

            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.TagName == "DIV")
                {
                    if (el0.GetAttribute("className").Contains("UFIAddCommentInput"))
                    {
                        el0.Focus();
                        
                        break;
                    }
                }
            }
        }

        // Putain si ça marche ça va être un joyeux bordel, mais c'est marrant.
        private void LikeEverythingOnScreen()
        {
            output.Text = "";
            HtmlDocument doc = webBrowser.Document;

            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.GetAttribute("className") == "UFILikeLink")
                {
                    if (el0.GetAttribute("title") != "Ne plus aimer")
                    {
                        if (el0.GetAttribute("title") != "Je n’aime plus ce commentaire")
                        {
                            el0.InvokeMember("Click");
                        }
                    }
                    else
                    {
                        output.Text += "Nop. ";
                    }
                }
            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            DateTime a = DateTime.Now;

            StreamWriter structStream = new StreamWriter("FBWalloutput.txt");
            StreamWriter splitStream = new StreamWriter("splitStream.txt");
            StreamWriter outputStream = new StreamWriter("output.txt");
            StreamWriter outputCleanStream = new StreamWriter("outputClean.txt");
            HtmlDocument doc = webBrowser.Document;

            WriteYourself(outputStream, 0, doc.Body);

            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.GetAttribute("className") == "UFIPagerLink")
                {
                    el0.InvokeMember("Click");
                }
                else if (el0.GetAttribute("className").Contains(" fss"))
                {
                    if (el0.Parent.GetAttribute("className") == "UFICommentBody")
                    {
                        el0.InvokeMember("Click");
                    }
                }
                else if (el0.GetAttribute("className").Contains("userContentWrapper"))
                {
                    FBPost newPost = new FBPost();
                    WriteYourself(outputCleanStream, 0, el0.Parent);

                    foreach (HtmlElement el1 in el0.All)
                    {
                        if (el1.GetAttribute("className") == "fwb fcg")
                        {
                            HtmlElement aProfile = el1.FirstChild;
                            newPost.Author.Name = DataFromHtmlElement(aProfile);
                            newPost.Author.Url = aProfile.GetAttribute("href");

                            foreach (HtmlElement el2 in el1.Parent.Parent.Parent.All)
                            {
                                if (el2.TagName == "ABBR")
                                {
                                    newPost.Date = UnixTimeStampToDateTime(el2.GetAttribute("data-utime"));
                                }
                            }
                        }

                        if (el1.GetAttribute("className").Contains("userContent"))
                        {
                            string content = "";
                            foreach (HtmlElement el2 in el1.All)
                            {
                                content += DataFromHtmlElement(el2) + " ";
                            }
                            newPost.Content = content;
                        }

                        if (el1.GetAttribute("className") == "UFILikeSentenceText")
                        {
                            int likesCount = 0;
                            string sLikes = "";
                            foreach (HtmlElement el2 in el1.All)
                            {
                                if (el2.TagName == "A")
                                {
                                    string s = DataFromHtmlElement(el2);
                                    string first = s.Split(' ')[0];
                                    int count;
                                    if (int.TryParse(first, out count))
                                    {
                                        likesCount += count;
                                    }
                                    else
                                    {
                                        likesCount++;
                                    }
                                }
                                sLikes += DataFromHtmlElement(el2);
                            }
                            newPost.LikeCount = likesCount;
                        }

                        if (el1.GetAttribute("className") == "UFICommentContentBlock")
                        {
                            FBComment newComment = new FBComment();
                            foreach (HtmlElement el2 in el1.All)
                            {
                                if (el2.GetAttribute("className").Contains("UFICommentActorName"))
                                {
                                    newComment.Author.Name = DataFromHtmlElement(el2.FirstChild);
                                    newComment.Author.Url = el2.GetAttribute("href");
                                }
                                else if (el2.TagName == "ABBR")
                                {
                                    newComment.Date = UnixTimeStampToDateTime(el2.GetAttribute("data-utime"));
                                }
                                else if (el2.GetAttribute("className") == "UFICommentBody")
                                {
                                    string content = "";
                                    foreach (HtmlElement el3 in el2.All)
                                    {
                                        content += DataFromHtmlElement(el3) + " ";
                                    }
                                    newComment.Content = content;
                                }
                                else if (el2.GetAttribute("className") == "UFICommentLikeButton")
                                {
                                    foreach (HtmlElement el3 in el2.All)
                                    {
                                        if (el3.TagName == "SPAN")
                                        {
                                            int likeCount = 0;
                                            int.TryParse(DataFromHtmlElement(el3), out likeCount);
                                            newComment.LikeCount = likeCount;
                                        }
                                    }
                                }
                            }
                            newPost.Add(newComment);
                        }
                    }
                }
            }

            outputStream.Close();
            outputCleanStream.Close();
            splitStream.Close();
            structStream.Close();

            DateTime b = DateTime.Now;

            output.Text = (b - a).ToString();
        }

        private void ReadMenuOnChange(object sender, EventArgs e)
        {
            this.vocalSynth.SpeakAsyncCancelAll();
            if (this.listBox1.SelectedItem != null)
            {
                this.vocalSynth.SpeakAsync(this.listBox1.SelectedItem.ToString());
            }
        }

        private void InfoMenuOnChange(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count > 0)
            {
                this.listBox1.SelectedIndex = 0;
            }
        }

        private void DebugOutputBody()
        {
            string name = input.Text;
            StreamWriter sw = new StreamWriter(name + ".txt");

            HtmlElement e = webBrowser.Document.Body;
            WriteYourself(sw, 0, e);
        }
    }
}
