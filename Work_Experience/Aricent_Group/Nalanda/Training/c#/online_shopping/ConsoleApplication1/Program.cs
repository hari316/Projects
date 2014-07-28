using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class groceries
    {
        static void Main(string[] args)
        {
            string[] list=new string[4];
            int[] price=new int[4];
            int[] qty = new int[4];
            int chosenp = 0;
            string choseni = null;
            int choice1 = 0;
            int choice2 = 0;
            int items = 0,index = 0;
            int f = 0;

            for (; ; )
            {
                Console.Clear();

                if (choice1 == 0)
                {
                    Console.WriteLine("1.Technology Items\n2.Food Items\n3.Display Current List\n4.Exit\n");
                    choice1 = Convert.ToInt32(Console.ReadLine());
                }
                if (choice1 == 4)
                    Environment.Exit(0);

                if (choice1 == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Item         Price\n1.Phone      30000\n2.Laptop     50000\n");
                    choice2 = Convert.ToInt32(Console.ReadLine());

                    if (choice2 == 1)
                    {
                        choseni = string.Copy("Phone");
                        chosenp = 30000;
                    }

                    else if (choice2 == 2)
                    {
                        choseni = string.Copy("Laptop");
                        chosenp = 50000;
                    }

                    else
                    {
                        Console.WriteLine("\nWrong Option\n");
                        choice1 = 1;
                        continue;
                    }

                }

                else if (choice1 == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Item        Price\n1.Rice      1000\n2.Chicken   4000\n");
                    choice2 = Convert.ToInt32(Console.ReadLine());

                    if (choice2 == 1)
                    {
                        choseni = string.Copy("Rice");
                        chosenp = 1000;
                    }

                    else if (choice2 == 2)
                    {
                        choseni = string.Copy("Chicken");
                        chosenp = 4000;
                    }

                    else
                    {
                        Console.WriteLine("\nWrong Option\n");
                        choice1 = 2;
                        continue;
                    }

                }

                else if (choice1 == 3)
                {
                    Console.Clear();
                    if (items == 0)
                    {
                        Console.WriteLine("List Empty");
                        Console.ReadLine();
                    }

                    else
                    {
                        Console.WriteLine("Item     Qunatity         Price\n");
                        for (index = 0; index < items; index++)
                        {
                            Console.WriteLine("{0}.{1}      {2}      {3}",(index+1),list[index],qty[index],price[index]);
                            
                        }
                        Console.ReadLine();
                    }

                    choice1 = 0;
                    continue;
                }

                else
                {
                    Console.WriteLine("\nWrong Option\n");
                    Console.ReadLine();
                }

                f=0;
                for (index = 0; index < items; index++)
                {
                    if (string.Compare(list[index], choseni) == 0)
                    {
                        f = 1;
                        break;
                    }
                }

                if (f == 1)
                {
                    price[index] += chosenp;
                    qty[index]++;
                    
                }

                else
                {
                    list[items] = string.Copy(choseni);
                    price[items] = chosenp;
                    qty[items]++;
                    items++;
                }



                choice1 = 0;    
            }

            //Console.ReadLine();

        }
    }
}
