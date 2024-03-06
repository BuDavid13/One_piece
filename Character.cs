using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using monoGame_Project.levels;

namespace monoGame_Project
{
    public class Character
    {
        public int level;
        public int maxHP;
        public int HP;
        public int shield;
        public int maxShield;
        public int damage;
        public Texture2D CharacterTexture;
        public bool Key;

        public Character(int maxHP, int HP, int damage, int shield, int maxShield, Texture2D CharacterTexture)
        {
            this.level = 1;
            this.maxHP = 20 + D6(3);
            this.HP = maxHP;
            this.damage = D6(2);
            this.maxShield = 5 + D6(1);
            this.shield = maxShield;
            this.CharacterTexture = CharacterTexture;
        }
        public Character(Texture2D CharacterTexture)
        {
            this.maxHP = 20 + D6(3);
            this.HP = maxHP;
            this.damage = D6(2);
            this.maxShield = 5 + D6(1);
            this.shield = maxShield;
            this.CharacterTexture = CharacterTexture;
        }
        public Character()
        {
        }
        //random (1-6) * a bekért számmal
        public int D6(int num)
        {
            Random rnd = new Random();
            int d6 = (rnd.Next(1, 6)) * num;

            return d6;
        }

        public void levelUp()
        {
            this.maxHP += D6(1);
            this.damage += D6(1);
            this.maxShield += D6(1);
        }

        public int Damage(Character character)
        {
            int AttackDamage = character.damage + D6(1);
            return AttackDamage;
        }

        public void Attack(int attackDamage, Enemy enemy)
        {
            int tempShield = enemy.shield;
            int tempDamage = attackDamage;
            if (enemy.shield > 0)
            {
                enemy.shield -= attackDamage;
                if (enemy.shield > 0)
                    tempDamage = 0;
                else
                {
                    tempDamage = attackDamage - tempShield;
                    enemy.shield = 0;
                }
            }

            if (tempDamage > 0)
            {
                enemy.HP = enemy.HP - (tempDamage);
                if (enemy.HP < 0)
                    enemy.HP = 0;
            }
        }
        
        public void Attack(int attackDamage, Final_Boss enemy)
        {
            int tempShield = enemy.shield;
            int tempDamage = attackDamage;
            if (enemy.shield > 0)
            {
                enemy.shield -= attackDamage;
                if (enemy.shield > 0)
                    tempDamage = 0;
                else
                {
                    tempDamage = attackDamage - tempShield;
                    enemy.shield = 0;
                }
            }

            if (tempDamage > 0)
            {
                enemy.HP = enemy.HP - (tempDamage);
                if (enemy.HP < 0)
                    enemy.HP = 0;
            }
        }

        public void Attack(int attackDamage, Boss boss)
        {
            int tempShield = boss.shield;
            int tempDamage = attackDamage;
            if (boss.shield > 0)
            {
                boss.shield -= attackDamage;
                if (boss.shield > 0)
                    tempDamage = 0;
                else
                {
                    tempDamage = attackDamage - tempShield;
                    boss.shield = 0;
                }
            }

            if (tempDamage > 0)
            {
                boss.HP = boss.HP - (tempDamage);
                if (boss.HP < 0)
                    boss.HP = 0;
            }
        }
  
        public void Regeneration()
        {
            Random rnd = new Random();
            int chance = (rnd.Next(0, 10));
            if (chance <= 4)
            {
                this.HP += Convert.ToInt32(maxHP * 0.25);
                if (maxHP < HP)
                {
                    HP = maxHP;
                }
            }
            else if (chance > 4 || chance <= 8)
            {
                this.HP += Convert.ToInt32(maxHP * 0.45);
                if (maxHP < HP)
                {
                    HP = maxHP;
                }
            }
            else
            {
                this.HP += Convert.ToInt32(maxHP * 0.65);
                if (maxHP < HP)
                {
                    HP = maxHP;
                }

            }
        }
    }
}
