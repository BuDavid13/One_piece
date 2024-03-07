using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace monoGame_Project.levels
{

    public class Enemy : Character
    {
        public int level;
        public bool key = false;
        public int[] position = new int[2];


        public Enemy(Texture2D CarachterTexture, Vector2 StartPosition, int level, int x, int y) : base(CarachterTexture)
        {
            this.level = spawnLevel(level);
            this.maxHP = 2 * level * D6(1);
            this.HP = maxHP;
            this.maxShield = level * D6(1);
            this.shield = maxShield;
            this.damage = level * D6(1);
            position[0] = x;
            position[1] = y;

        }



        public Enemy(int maxHP, int HP, int damage, int shield, int maxShield, Vector2 StartPosition, Vector2 CarachterPosition, Texture2D CarachterTexture) : base(maxHP, HP, damage, shield, maxShield, StartPosition, CarachterPosition, CarachterTexture)
        {
        }

        public int Damage(Enemy enemy)
        {
            int attackDamage = enemy.damage + D6(1) * 2;
            return attackDamage;
        }

        public void Attack(int attackDamage, Character character)
        {
            int tempShield = character.shield;
            int tempDamage = attackDamage;
            if (character.shield > 0)
            {
                character.shield -= attackDamage;
                if (character.shield > 0)
                    tempDamage = 0;
                else
                    tempDamage = attackDamage - tempShield;
            }


            if (tempDamage > 0)
            {
                character.HP = character.HP - (tempDamage);
            }
        }

        protected int spawnLevel(int level)
        {
            Random rnd = new Random();
            int chance = (rnd.Next(0, 9));
            if (chance <= 4)
            {
            }
            else if (chance > 4 || chance <= 8)
            {
                level++;
            }
            else
            {
                level += 2;
            }

            return level;
        }
    }
}
