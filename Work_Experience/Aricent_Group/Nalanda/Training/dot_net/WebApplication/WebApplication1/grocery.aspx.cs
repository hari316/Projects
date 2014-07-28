using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{

    public partial class lisy : System.Web.UI.Page
    {
        List<string> item1 = new List<string>(){"Phone $ 500", "Laptop $ 800"};
        List<string> item2 = new List<string>() { "Cheese $ 4", "Milk $ 3" };
        List<string> item3 = new List<string>() { "Shirt $ 250", "Socks $ 60" };
        public string[] selected = new string[10];
        public string[] split = new string[15];
        
        string emp = "                ";
        int[] qty = new int[10]{0,0,0,0,0,0,0,0,0,0};
        int[] pr = new int[10];
        int count;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void next_box(object sender, EventArgs e)
        {
            ListBox2.Items.Clear();
            string sel=ListBox1.SelectedItem.Value;

           if(string.Compare(sel,"1")==0)
           {
               foreach (string si in item1)
               {
                   ListBox2.Items.Add(si);
               }

        
           }
           else if (string.Compare(sel, "2") == 0)
           {
              foreach(string si in item2)
              {
                  ListBox2.Items.Add(si);
              }

           }
           else
           {
               foreach (string si in item3)
               {
                   ListBox2.Items.Add(si);
               }

           }
        }
        public void purchase_item(object sender, EventArgs e)
        {
            if (ListBox2.SelectedItem == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "Message PopUp", "<script language='JavaScript'> alert('You must select an item'); </script>");
            }
            else
            {
                count = 0;
                foreach (var su in ListBox3.Items)
                {



                    if (count != 0)
                    {
                        split = su.ToString().Split(' ');
                        selected[count] = split[0];

                        pr[count] = System.Convert.ToInt32(split[2]);
                        qty[count] = System.Convert.ToInt32(split[3]);

                    }

                    else
                    {
                        selected[count] = su.ToString();
                    }

                    
                    count++;

                }
                
                ListBox3.Items.Clear();
                int f=0;
                count=0;
                string sq=null;
                int eq = System.Convert.ToInt32(DropDownList1.SelectedItem.Value);
                foreach (string su in selected)
                {
                    if (su == null)
                        break;

                    split = ListBox2.SelectedItem.Text.Split(' ');
                    if (string.Compare(split[0], su) == 0)
                    {
                        f=1;
                        pr[count] = System.Convert.ToInt32(split[2]);
                        qty[count] += eq;
                    }
                    count++;
                }

                if (f == 0)
                {
                    split = ListBox2.SelectedItem.Text.Split(' ');
                    selected[count]= string.Copy(split[0]);
                    
                    pr[count] = System.Convert.ToInt32(split[2]);
                   
                    qty[count] = eq;
                }

                count = 0;
              
                foreach (string su in selected)
                {
                    if (su == null)
                        break;

                    if (count != 0)
                    {
                        sq = su + " $ " + pr[count] + " " + qty[count] + " $ " + (pr[count] * qty[count]);
                        ListBox3.Items.Add(sq);
                    }

                    else
                    {
                        ListBox3.Items.Add(su);
                    }
                    count++;
                }

            }
        }
    }
}