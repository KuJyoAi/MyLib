using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace XKC_P102_Below
{
    //九年级上册人教版新课程新练习102页下面的爱因斯坦测试题
    class Program
    {
        
        static void Main(string[] args)
        {
        }
    }
    class Person
    {
        const int Unkonwn = 0;
        //国籍
        const int NW = 1;
        const int DM = 2;
        const int DG = 3;
        const int RD = 4;
        const int YG = 5;
        //宠物
        const int dog = 6;
        const int bird = 7;
        const int horse = 8;
        const int cat = 9;
        const int fish = 10;
        //饮料
        const int beer = 11;
        const int milk = 12;
        const int tea = 13;
        const int water = 14;
        const int coffee = 15;
        //烟
        const int dun = 16;
        const int prin = 17;
        const int pau = 18;
        const int mixed = 19;
        const int WZY = 20;
        //房子
        const int red = 21;
        const int blue = 22;
        const int yellow = 23;
        const int green = 24;
        const int WZS = 25;

        //序列
        public int Order = Unkonwn;
        public int house = Unkonwn;
        public int pet = Unkonwn;
        public int smoke = Unkonwn;
        public int drink = Unkonwn;
        public int GJ = Unkonwn;
        public Person(int GJ)
        {
            this.GJ = GJ;
        }
    }
    class Board
    {
        /*
        顺序
        宠物
        饮料
        烟
        房子
        */
        const int Unkonwn = 0;
        //国籍
        const int NW = 1;
        const int DM = 2;
        const int DG = 3;
        const int RD = 4;
        const int YG = 5;
        //宠物
        const int dog = 6;
        const int bird = 7;
        const int horse = 8;
        const int cat = 9;
        const int fish = 10;
        //饮料
        const int beer = 11;
        const int milk = 12;
        const int tea = 13;
        const int water = 14;
        const int coffee = 15;
        //烟
        const int dun = 16;
        const int prin = 17;
        const int pau = 18;
        const int mixed = 19;
        const int WZY = 20;
        //房子
        const int red = 21;
        const int blue = 22;
        const int yellow = 23;
        const int green = 24;
        const int WZS = 25;

        Person[] son = new Person[5];
        public Board()
        { 
            son[0] = new Person(YG);
            son[1] = new Person(DM);
            son[2] = new Person(NW);
            son[3] = new Person(DG);
            son[4] = new Person(RD);
            son[0].house = red;
            son[1].drink = tea;
            son[2].Order = 1;
            son[3].smoke = prin;
            son[4].pet = dog;
        }
        public void Exchange(int pos1, int pos2)
        {

        }
        public bool check(Point P)
        {
            int x = P.X;
            int y = P.Y;
            if (x == 1)
            {

            }
        }
        public Point next()
        {
            for (int i = 0; i < 5; i++)
            {
                if (son[i].Order == 0)
                    return new Point(1, i);
                if (son[i].pet == 0)
                    return new Point(2, i);
                if (son[i].drink == 0)
                    return new Point(3, i);
                if (son[i].smoke == 0)
                    return new Point(4, i);
                if (son[i].house == 0)
                    return new Point(5, i);
            }
            return new Point(-1, -1);
        }
    }
}
