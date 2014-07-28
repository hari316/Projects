using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            num1.Text = num1.Text + bt.Text; 
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            Button bt = (Button)sender;
            num2.Text = num2.Text + bt.Text; 
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

            Button bt = (Button)sender;
            //num2.Text = num2.Text + bt.Text;
            if (bt.CommandName == "c1")
                num1.Text = null;
            else
                num2.Text = null;
        }

        protected void calc_Click(object sender, EventArgs e)
        {
           
            if (num1.Text == ""  || num2.Text == "" )
            {
                res.Text = "Please enter a value";
                num1.Focus();
            }
            else
            {
                decimal a = Convert.ToDecimal(num1.Text);
                decimal b = Convert.ToDecimal(num2.Text);
                //Response.Write(a + b);
                char ch = Convert.ToChar(op.SelectedItem.Value);
                switch (ch)
                {
                    case '+':
                        res.Text = Convert.ToString(a + b);
                        break;

                    case '-':
                        res.Text = Convert.ToString(a - b);
                        break;

                    case '*':
                        res.Text = Convert.ToString(a * b);
                        break;

                    case '/':
                        if (b == 0)
                        {
                            res.Text = "Enter a non zero value for Number 2";
                            num2.Focus();
                        }
                        else
                            res.Text = Convert.ToString(a / b);
                        break;

                    default: res.Text = "Invalid Operation Performed";
                        break;
                }

            }
        }

        
    }
}
