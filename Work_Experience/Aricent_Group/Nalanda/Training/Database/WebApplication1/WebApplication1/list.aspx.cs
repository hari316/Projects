using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApplication1
{
    public partial class list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListBox2.Items.Clear(); 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < ListBox1.Items.Count;i++)
            {
                if (ListBox1.Items[i].Selected == true)
                {
                    ListBox2.Items.Add(ListBox1.Items[i]);
                    ListBox1.Items.Remove(ListBox1.Items[i]);
                    i--;
                }
            }

            SortListBox(ListBox2);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                if (ListBox2.Items[i].Selected == true)
                {
                    ListBox1.Items.Add(ListBox2.Items[i]);
                    ListBox2.Items.Remove(ListBox2.Items[i]);
                    i--;
                }
            }

            SortListBox(ListBox1);
        } 
        private void SortListBox(ListBox list_box)
        {
           
            
            ArrayList ListBoxArray = new ArrayList();

            int i = 0;

            while (i < list_box.Items.Count)
            {

                ListBoxArray.Add(list_box.Items[i].Text);

                ++i;

            }

            list_box.Items.Clear();

            ListBoxArray.Sort();

            i = 0;

            while (ListBoxArray.Count > i)
            {

                list_box.Items.Add(ListBoxArray[i].ToString());

                ++i;

            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                ListBox1.Items.Remove(ListBox1.Items[i]);
                i--;
            }
               
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                    ListBox2.Items.Remove(ListBox2.Items[i]);
                    i--;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                if (ListBox1.Items[i].Selected == true)
                {
                    ListBox1.Items.Remove(ListBox1.Items[i]);
                    i--;
                }
            }
            for (int i = 0; i < ListBox2.Items.Count; i++)
            {
                if (ListBox2.Items[i].Selected == true)
                {
                    ListBox2.Items.Remove(ListBox2.Items[i]);
                    i--;
                }
            }
        }

       

    }
}
