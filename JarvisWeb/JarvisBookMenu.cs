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
using System.Collections.Generic;
using System.Windows.Forms;

namespace SvenFrankson
{
    public partial class JarvisBook : Form
    {
        private void LoadMenuMain()
        {
            this.menu = new List<string>();
            this.action = new List<string>();
            this.menu.Add("Menu Principal. Début du menu.");
            this.action.Add("VOID");
            this.menu.Add("Actualités");
            this.action.Add("GOTOACTU");
            this.menu.Add("Groupes");
            this.action.Add("GOTOGROUPS");
            this.menu.Add("Amis");
            this.action.Add("GOTOFRIENDS");
            this.menu.Add("Ma page");
            this.action.Add("GOTOMYPAGE");
            this.menu.Add("Menu Principal. Fin du menu.");
            this.action.Add("VOID");
            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.KeyPress += MenuKeyPressHandler;
            this.listBox1.SelectedIndexChanged += ReadMenuOnChange;
            this.listBox1.SelectedIndex = 0;
            this.listBox1.Select();
        }

        private void LoadMenuWall()
        {
            this.menu = new List<string>();
            this.action = new List<string>();
            this.menu.Add(this.pageName + ". Début du menu.");
            this.action.Add("VOID");
            this.menu.Add("Lire les messages");
            this.action.Add("READPOSTS");
            this.menu.Add("Ecrire un nouveau message");
            this.action.Add("WRITENEWPOST");
            this.menu.Add("Retour");
            this.action.Add("BACKTOMAINMENU");
            this.menu.Add(this.pageName + ". Fin du menu.");
            this.action.Add("VOID");
            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.KeyPress += MenuKeyPressHandler;
            this.listBox1.SelectedIndexChanged += ReadMenuOnChange;
            this.listBox1.SelectedIndex = 0;
            this.listBox1.Select();
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
            this.listBox1.PreviewKeyDown += ReadPostsKeyPressHandler;
            this.listBox1.SelectedIndexChanged += InfoMenuOnChange;
            this.listBox1.ClearSelected();
            this.listBox1.Select();
        }

        private void LoadMenuInfoReadComments()
        {
            this.menu = new List<string>();
            this.action = new List<string>();
            this.menu.Add("Lecture des commentaires");
            this.action.Add("VOID");
            this.menu.Add("Haut : Commentaire précedent");
            this.action.Add("VOID");
            this.menu.Add("Bas : Commentaire suivant");
            this.action.Add("VOID");
            this.menu.Add("Gauche : Retour");
            this.action.Add("VOID");
            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.PreviewKeyDown += ReadCommentsKeyPressHandler;
            this.listBox1.SelectedIndexChanged += InfoMenuOnChange;
            this.listBox1.ClearSelected();
            this.listBox1.Select();
        }

        private void PopulateMenuWithGroups(object sender, EventArgs e)
        {
            this.webBrowser.DocumentCompleted -= PopulateMenuWithGroups;

            this.menu = new List<string>();
            this.action = new List<string>();

            this.menu.Add("Liste des groupes. Début de la liste.");
            this.action.Add("VOID");

            HtmlDocument doc = webBrowser.Document;

            foreach (HtmlElement el0 in doc.All)
            {
                if (el0.TagName == "A")
                {
                    if (el0.GetAttribute("className") == "groupsRecommendedTitle")
                    {
                        this.menu.Add(DataFromHtmlElement(el0));
                        this.action.Add("GOTOURL " + el0.GetAttribute("href") + " " + DataFromHtmlElement(el0) + " (Groupe)");
                    }
                }
            }

            this.menu.Add("Retour");
            this.action.Add("BACKTOMAINMENU");
            this.menu.Add("Liste des groupes. Fin de la liste.");
            this.action.Add("VOID");

            this.ClearMenuDelegates();
            this.listBox1.Items.Clear();
            foreach (string s in this.menu)
            {
                this.listBox1.Items.Add(s);
            }
            this.listBox1.KeyPress += MenuKeyPressHandler;
            this.listBox1.SelectedIndexChanged += ReadMenuOnChange;
            this.listBox1.SelectedIndex = 0;
            this.listBox1.Select();
        }
    }
}
