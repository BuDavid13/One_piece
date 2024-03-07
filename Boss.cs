using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monoGame_Project
{
    public class Boss : Character
    {
        public int level;
        public int[] position = new int[2];

        public Boss(Texture2D CharacterTexture, int level, int x, int y) : base(CharacterTexture)
        {
            this.level = level;
            this.maxHP = 2 * this.level * D6(1) + D6(1);
            this.HP = maxHP;
            this.damage = this.level * D6(1) + (D6(1) / 2);
            this.shield = this.level * D6(1) + this.level;
            this.position[0] = x;
            this.position[1] = y;
        }

        public Boss(int maxHP, int HP, int damage, int shield, int maxShield, Vector2 StartPosition, Vector2 CharacterPosition, Texture2D CharacterTexture) : base(maxHP, HP, damage, shield, maxShield, StartPosition, CharacterPosition, CharacterTexture)
        {
        }

        public int Attack(Boss boss)
        {
            int AttackDamage = boss.damage * 2 + D6(1) * 2;
            return AttackDamage;
        }
    }
}
