using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriYapilariOdev_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private string PostfixDonusumu(string infix)
        {

            Stack<char> yıgın = new Stack<char>(); 
            string cıktı = ""; 
            foreach (char a in infix)
            {
                if (char.IsLetterOrDigit(a)) 
                
                {
                    cıktı += a; 
                }
                else if (a == '(') 
                {
                    yıgın.Push(a); 
                }
                else if (a == ')') 
                {
                    while (yıgın.Count > 0 && yıgın.Peek() != '(')
                    {
                        cıktı += yıgın.Pop(); // Yığından eleman al
                    }
                    if (yıgın.Count > 0) // Açık parantez varsa yapıdan çıkarır.
                    {
                        yıgın.Pop(); // Parantezi çıkart
                    }
                }
                else // Operatör
                {
                    while (yıgın.Count > 0 && OncelikSırası(a) <= OncelikSırası(yıgın.Peek()))
                    {
                        cıktı += yıgın.Pop(); // Yığından eleman al
                    }
                    yıgın.Push(a); // Yığına operatör ekleme işlemi için
                }
            }
            while (yıgın.Count > 0)
            {
                cıktı += yıgın.Pop(); 
            }
        
            return cıktı;
        }

        private int OncelikSırası(char o)
        {
            switch (o)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default:
                    return 0;
            }
        }

        private string PrefixDonustur(string infix) 
        {
           
            char[] karakterDizisi = infix.ToCharArray();
            Array.Reverse(karakterDizisi);
            string tersInfix = new string(karakterDizisi);

           
            tersInfix = tersInfix.Replace('(', 'X').Replace(')', '(').Replace('X', ')'); //Bu kısma dikkat et!!!

            string postfix = PostfixDonusumu(tersInfix); // Ters infix'i postfix'e çevir

            
            char[] postfixDizisi = postfix.ToCharArray();
            Array.Reverse(postfixDizisi);
            
            return new string(postfixDizisi); // Prefix ifadeyi döndür
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string infix = textBox1.Text;
            string prefix = PrefixDonustur(infix);
            label2.Text = "Prefix:  " + prefix;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string infix = textBox1.Text;
            string postfix = PostfixDonusumu(infix);
            label1.Text = "Postfix:  " + postfix;
        }
    }
}
