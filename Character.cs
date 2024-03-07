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
        public Vector2 StartPosition;
        public Vector2 CharacterPosition;
        public Texture2D CharacterTexture;
        public bool Key;




        public Character(int maxHP, int HP, int damage, int shield, int maxShield, Vector2 StartPosition, Vector2 CharacterPosition, Texture2D CharacterTexture)
        {
            this.level = 1;
            this.maxHP = 20000 + D6(3);
            this.HP = maxHP;
            this.damage = D6(2);
            this.maxShield = 5 + D6(1);
            this.shield = maxShield;
            this.StartPosition = StartPosition;

            this.CharacterTexture = CharacterTexture;
        }
        public Character(Texture2D CharacterTexture)
        {
            this.maxHP = 20000 + D6(3);
            this.HP = maxHP;
            this.damage = D6(2);
            this.maxShield = 5 + D6(1);
            this.shield = maxShield;
            this.StartPosition = StartPosition;

            this.CharacterTexture = CharacterTexture;
        }
        public Character(Vector2 StartPosition)
        {
            this.StartPosition = StartPosition;
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
                    tempDamage = attackDamage - tempShield;
            }

            if (tempDamage > 0)
            {
                enemy.HP = enemy.HP - (tempDamage);
            }
        }
        public void Attack(int attackDamage, Boss enemy)
        {
            int tempShield = enemy.shield;
            int tempDamage = attackDamage;
            if (enemy.shield > 0)
            {
                enemy.shield -= attackDamage;
                if (enemy.shield > 0)
                    tempDamage = 0;
                else
                    tempDamage = attackDamage - tempShield;
            }

            if (tempDamage > 0)
            {
                enemy.HP = enemy.HP - (tempDamage);
            }
        }
        public void Regeneration()
        {
            Random rnd = new Random();
            int chance = (rnd.Next(0, 10));
            if (chance <= 4)
            {
                this.HP += maxHP / 10;
                this.shield += maxShield / 10;
                if (maxHP < HP)
                {
                    HP = maxHP;
                }
                if (maxShield < shield)
                {
                    shield = maxShield;
                }
            }
            else if (chance > 4 || chance <= 8)
            {
                this.HP += maxHP / 3;
                this.shield += maxShield / 3;
                if (maxHP < HP)
                {
                    HP = maxHP;
                }
                if (maxShield < shield)
                {
                    shield = maxShield;
                }
            }
            else
            {
                this.HP += maxHP / 2;
                this.maxHP += maxHP / 2;
                if (maxHP < HP)
                {
                    HP = maxHP;
                }
                if (maxShield < shield)
                {
                    shield = maxShield;
                }
            }
        }
    }
}

