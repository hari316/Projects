using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator        
{
    class Program
    {
        static void Main(string[] args)
        {
            int flag = 1;
            int a =0;
            int b = 0;
            int res = 0;
            int i = 0;
       
            int op;
            char cont;
            string[] operations = new string[] { "Addition", "Subtraction", "Multipliaction", "Division", "Exit" };
            while(flag == 1)
            {
                Console.Clear();
                Console.Write("Please enter the operation to be performed :\n");
                //Console.Write("\n1) Addition\n2) Subtraction\n3) Multiplication\n4) Division\n5) exit\n\n");
                i = 1;
                foreach (string option in operations)
                {
                    Console.Write("\n" + i + ") " + option + "\n");
                    i++;
                }
                op = Convert.ToInt32(Console.ReadLine());
                                         
                if(op < 5)
                {
                    Console.Write("\n************************************************************************\n");
                    
                    Console.Write("Please enter 2 integers \n");
                    a = Convert.ToInt32(Console.ReadLine());                                       
                    b = Convert.ToInt32(Console.ReadLine());                     
                   
                }

                switch(op)
                {
                   
                    case 1:
                        Console.Write("\n************************************************************************\n");
                        Console.Write("Operation to be performed is {0}\n", operations[0]);
                        Add op1 = new Add();
                        res = op1.addition(a, b);
                        break;
                    case 2:
                        Console.Write("\n************************************************************************\n");
                        Console.Write("Operation to be performed is {0}\n", operations[1]);
                        Sub op2 = new Sub();
                        res = op2.substraction(a, b);
                        break;
                    case 3:
                        Console.Write("\n************************************************************************\n");
                        Console.Write("Operation to be performed is {0}\n", operations[2]);
                        multiply op3 = new multiply();
                        res = op3.multiplication(a, b);
                        break;
                    case 4:
                        Console.Write("\n************************************************************************\n");
                        Console.Write("Operation to be performed is {0}\n", operations[3]);
                        if (b == 0)
                        {
                            Console.Write("Division by zero error !!! \n\nPlease enter a non zero value for b \n");
                            
                            b = Convert.ToInt32(Console.ReadLine());
                        }
                        divide op4 = new divide();
                        res = op4.division(a, b);
                        break;
                    case 5:
                        Console.Write("\n************************************************************************\n");
                        Console.Write("Operation to be performed is {0}\n", operations[4]);
                        Environment.Exit(0);
                        break;
                    default :
                        Console.Write("Invalid operation\n");
                        break;
                }
                Console.Write("\n************************************************************************\n");
                Console.Write("\nResult of the operation is : {0}", res);
                Console.Write("\n************************************************************************\n");
                Console.Write("\n\nDo you want to continue : Y or N\n");
                cont = Convert.ToChar(Console.ReadLine());
                    
                if (cont == 'N' || cont == 'n')
                    flag = 0;

                
            }
                

        }
    }
}
