using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc6Fabio.Components
{
    public class Enemy
    {
        string NameEnemy, Lifes, Damage;
        int PosX, PosY;

        Enemy(string nameEnemy,string lifes, string damage, int posX,int posY)
        {
            NameEnemy = nameEnemy;
            Lifes = lifes;
            Damage = damage;
            PosY1 = posY;
            PosX = posX;
        }

        public string NameEnemy1 { get => NameEnemy; set => NameEnemy = value; }
        public string Lifes1 { get => Lifes; set => Lifes = value; }
        public string Damage1 { get => Damage; set => Damage = value; }
        public int PosX1 { get => PosX; set => PosX = value; }
        public int PosY1 { get => PosY; set => PosY = value; }
    }
}
