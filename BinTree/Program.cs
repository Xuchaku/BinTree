using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinTree
{
    
    class BinTree
    {
        public Petal root;
        public int[] arr;
        public int iCurrent;
        public BinTree(int N)
        {
            root = null;
            arr = new int[N];
            iCurrent = 0;
        }

        public void remove(Petal curPetal)
        {
            if (curPetal.left == null && curPetal.right == null)
            {
                Petal parentPetal = curPetal.parent;
                if (curPetal == parentPetal.left)
                {
                    parentPetal.left = null;
                }
                if (curPetal == parentPetal.right)
                {
                    parentPetal.right = null;
                }
            }

            if ((curPetal.left == null && curPetal.right != null) || (curPetal.left != null && curPetal.right == null))
            {
                if (curPetal.parent.left == curPetal)
                {
                    if (curPetal.left != null)
                        curPetal.parent.left = curPetal.left;
                    else
                        curPetal.parent.left = curPetal.right;
                }
                if (curPetal.parent.right == curPetal)
                {
                    if (curPetal.right != null)
                        curPetal.parent.right = curPetal.right;
                    else
                        curPetal.parent.right = curPetal.left;
                }
            }

            if (curPetal.left != null && curPetal.right != null)
            {
                if (curPetal.right != null && curPetal.right.left != null)
                {

                    Petal leftTree = curPetal.left;
                    Petal start = curPetal.right.left;
                    while (start.left != null)
                    {
                        start = start.left;
                    }

                    start.parent.left = null;
                    Petal rightTree = curPetal.right;
                    start.left = leftTree;
                    start.right = rightTree;
                    if (curPetal.parent.left == curPetal)
                    {
                        curPetal.parent.left = start;
                    }
                    else
                    {
                        curPetal.parent.right = start;
                    }
                    return;

                }
                if (curPetal.right != null && curPetal.right.left == null)
                {
                    Console.Write("HERE");
                    Petal leftChild = curPetal.left;
                    Petal rightChild = curPetal.right;
                    rightChild.left = leftChild;
                    if (curPetal.parent.left == curPetal)
                    {
                        curPetal.parent.left = rightChild;
                    }
                    else
                    {
                        curPetal.parent.right = rightChild;
                    }

                }


            }
        }
        public Petal search(int val)
        {

            Petal currentPetal = root;
            while (currentPetal != null)
            {
                if (currentPetal.data == val)
                    return currentPetal;
                else if (val > currentPetal.data)
                {
                    currentPetal = currentPetal.right;
                }
                else
                {
                    currentPetal = currentPetal.left;
                }
            }
            return null;

        }
        public void Show2(int level, Petal[] curPetals)
        {

            if (level == 0)
            {
                if (root != null)
                {
                    int cnt = 0;
                    Console.Write("    " + root.data);
                    Console.Write("\n");
                    Petal[] pts = new Petal[(level + 1) * 2];
                    pts[0] = root.left;
                    pts[1] = root.right;
                    Show2(level + 1, pts);
                }
            }
            else
            {
                for (int i = 0; i < curPetals.Length; i++)
                {
                    if (i == (curPetals.Length / 2))
                        Console.Write("    ");
                    if (curPetals[i] != null)
                    {
                        Console.Write(" " + curPetals[i].data);
                    }
                    else
                    {
                        Console.Write(" *");
                    }
                }

                Console.Write("\n");
                int j = 0;
                int cntNull = 0;
                Petal[] pts = new Petal[(int)(Math.Pow(2, (level + 1)))];
                for (int i = 0; i < curPetals.Length; i++)
                {
                    if (curPetals[i] == null)
                    {
                        cntNull++;
                        pts[j] = null;
                        pts[j + 1] = null;
                    }
                    else
                    {
                        pts[j] = curPetals[i].left;
                        pts[j + 1] = curPetals[i].right;

                    }

                    j += 2;
                }

                if (cntNull == curPetals.Length)
                    return;
                else
                {
                    Show2(level + 1, pts);
                }

            }
        }

        public void Add(int d)
        {
            bool flag = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (d == arr[i])
                    flag = true;
            }

            if (!flag)
            {
                arr[0] = d;
                Array.Sort(arr);
                /*for(int i = 0;i<arr.Length;i++)
                    Console.Write(arr[i] + " ");*/
            }
            
            if (root != null)
            {
                Petal newPetal = new Petal(d);
                Petal currentPetal = root;
                while (currentPetal != null)
                {
                    if (newPetal.data > currentPetal.data)
                    {
                        if (currentPetal.right != null)
                            currentPetal = currentPetal.right;
                        else
                        {
                            currentPetal.right = newPetal;
                            newPetal.parent = currentPetal;
                            break;
                        }
                    }
                    if (newPetal.data < currentPetal.data)
                    {
                        if (currentPetal.left != null)
                            currentPetal = currentPetal.left;
                        else
                        {
                            currentPetal.left = newPetal;
                            newPetal.parent = currentPetal;
                            break;
                        }
                    }
                }
            }
            else
            {
                root = new Petal(d);
            }
        }
    }

    class Petal
    {
        public Petal left;
        public Petal right;
        public Petal parent;
        public int data;
        public Petal(int d)
        {
            data = d;
            right = null;
            left = null;
            parent = null;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            FileStream infile = new FileStream("./"+args[0], FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(infile);
            FileStream outfile = new FileStream("./programm.out", FileMode.Open, FileAccess.Write);
            outfile.SetLength(0);
            StreamWriter writer = new StreamWriter(outfile);

            FileStream outfile2 = new FileStream("./programm2.out", FileMode.Open, FileAccess.Write);
            outfile2.SetLength(0);
            StreamWriter writer2 = new StreamWriter(outfile2);
            FileStream infile2 = new FileStream("./" + args[0], FileMode.Open, FileAccess.Read);
            StreamReader reader2 = new StreamReader(infile2);
            int N = int.Parse(reader.ReadLine());
            BinTree tree = new BinTree(N);
            for (int i = 0; i < N; i++)
            {
                int num = int.Parse(reader.ReadLine());
                if (tree.search(num) == null)
                {
                    writer.WriteLine("-");
                    Console.WriteLine("-");
                    tree.Add(num);
                }
                else
                {
                    writer.WriteLine("+");
                    Console.WriteLine("+");
                }
            }

            tree = new BinTree(N);
            Console.WriteLine();
            infile.Seek(0, SeekOrigin.Begin);
            reader.ReadLine();
            for (int i = 0; i < N; i++)
            {
                int num = int.Parse(reader.ReadLine());
                if (tree.search(num) == null)
                {
                    Console.Write("-");
                    writer2.Write("-");
                    tree.Add(num);
                    int index = Array.IndexOf(tree.arr, num);
                    if (index + 1 < N)
                    {
                        if (tree.arr[index + 1] == 0)
                        {
                            Console.Write(" -");
                            writer2.Write(" -");
                        }
                        else
                        {
                            Console.Write(" " + tree.arr[index + 1]);
                            writer2.Write(" " + tree.arr[index + 1]);
                        }
                    }
                    else
                    {
                        Console.Write(" -");
                        writer2.Write(" -");

                    }
                }
                else
                {
                    Console.Write("+");
                    writer2.Write("+");
                    int index = Array.IndexOf(tree.arr, num);
                    if (index + 1 < N)
                    {
                        if (tree.arr[index + 1] == 0)
                        {
                            Console.Write(" -");
                            writer2.Write(" -");
                        }
                        else
                        {
                            Console.Write(" " + tree.arr[index + 1]);
                            writer2.Write(" " + tree.arr[index + 1]);
                        }
                    }
                    else
                    {
                        Console.Write(" -");
                        writer2.Write(" -");
                    }
                }
                Console.WriteLine();
                writer2.WriteLine();
            }
            Console.ReadKey();
            writer.Close();
            writer2.Close();
            
        }
    }
}
