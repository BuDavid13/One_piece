using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monoGame_Project.levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monoGame_Project
{
    public class Boss : Character
    {
        public int[] position = new int[2];
        public string around;
        public Boss(Texture2D CharacterTexture, int level, int x, int y) : base(CharacterTexture)
        {
            this.level = level;
            this.maxHP = 2 * this.level * D6(1) + D6(1);
            this.HP = maxHP;
            this.damage = (this.level/2) * D6(1) + (D6(1) / 2);
            this.shield = this.level * D6(1) + this.level;
            this.position[0] = x;
            this.position[1] = y;
        }

        public Boss(int maxHP, int HP, int damage, int shield, int maxShield, Texture2D CharacterTexture) : base(maxHP, HP, damage, shield, maxShield, CharacterTexture)
        {
        }
        

        public int Damage(Boss boss)
        {
            int attackDamage = boss.damage + D6(1);
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
                {
                    tempDamage = attackDamage - tempShield;
                    character.shield = 0;
                }
            }


            if (tempDamage > 0)
            {
                character.HP = character.HP - (tempDamage);
                if (character.HP < 0)
                    character.HP = 0;
            }
        }
    }
}
