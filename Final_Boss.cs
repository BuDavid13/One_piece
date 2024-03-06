using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using monoGame_Project.levels;

namespace monoGame_Project
{
    public class Final_Boss: Character
    {
        public int[] position = new int[6];
        public Character player;
        public Final_Boss(Texture2D CharacterTexture, Character player):base(CharacterTexture)
        {
            position[0] = 9;
            position[1] = 10;
            position[2] = 11;
            position[3] = 9;
            position[4] = 10;
            position[5] = 11;
            this.level = 100;
            this.maxHP = 1000;
            this.HP = maxHP;
            this.maxShield = 1000;
            this.shield = maxShield;
            this.damage = 5;
            this.CharacterTexture = CharacterTexture;
            this.player = player;
        }
        public void Attack(Character character)
        {
            int tempShield = character.shield;
            int tempDamage = this.damage;
            if (character.shield > 0)
            {
                character.shield -= this.damage;
                if (character.shield > 0)
                    tempDamage = 0;
                else
                {
                    tempDamage = this.damage - tempShield;
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
        public void Advance(int[] playerXY, ref int step)
        {
            int[] bossXY = this.position;
            bool x;
            bool y;
            switch (playerXY[0])
            {
                case int n when n > bossXY[1]:
                    x = true;
                    break;
                default:
                    x = false;
                    break;
            }
            switch (playerXY[1])
            {
                case int n when n > bossXY[4]:
                    y = true;
                    break;
                default:
                    y = false;
                    break;
            }
            Random rnd = new Random();
            if (step <= 20)
            {
                if (rnd.Next(1, 3) == 1)
                {
                    switch (x)
                    {
                        case true:
                            if (bossXY[2] + 1 != 12)
                            {
                                if (!(playerXY[0] == bossXY[2] + 1 && (playerXY[1] == bossXY[3] || playerXY[1] == bossXY[4] || playerXY[1] == bossXY[5])))
                                {
                                    bossXY[0] += 1;
                                    bossXY[1] += 1;
                                    bossXY[2] += 1;
                                }
                                else
                                {
                                    this.Attack(this.player);
                                }

                            }
                            break;
                        case false:
                            if (bossXY[0] - 1 != 1)
                            {
                                if (!(playerXY[0] == bossXY[0] - 1 && (playerXY[1] == bossXY[3] || playerXY[1] == bossXY[4] || playerXY[1] == bossXY[5])))
                                {
                                    bossXY[0] -= 1;
                                    bossXY[1] -= 1;
                                    bossXY[2] -= 1;
                                }
                                else
                                {
                                    this.Attack(this.player);
                                }
                            }
                            break;
                    }
                }
                else
                {
                    switch (y)
                    {
                        case true:
                            if (bossXY[5] + 1 != 12)
                            {
                                if (!(playerXY[1] == bossXY[5] + 1 && (playerXY[0] == bossXY[0] || playerXY[0] == bossXY[1] || playerXY[0] == bossXY[2])))
                                {
                                    bossXY[3] += 1;
                                    bossXY[4] += 1;
                                    bossXY[5] += 1;
                                }
                                else
                                {
                                    this.Attack(this.player);
                                }
                            }
                            break;
                        case false:
                            if (bossXY[3] - 1 != 1)
                            {
                                if (!(playerXY[1] == bossXY[3] - 1 && (playerXY[0] == bossXY[0] || playerXY[0] == bossXY[1] || playerXY[0] == bossXY[2])))
                                {
                                    bossXY[3] -= 1;
                                    bossXY[4] -= 1;
                                    bossXY[5] -= 1;
                                }
                                else
                                {
                                    this.Attack(this.player);
                                }
                            }
                            break;
                    }
                }
            }
            else if(step <= 40)
            {
                if (rnd.Next(1, 3) == 1)
                {
                    switch (x)
                    {
                        case true:
                            if (bossXY[0] - 1 != 1)
                            {
                                if (!(playerXY[0] == bossXY[0] - 1 && (playerXY[1] == bossXY[3] || playerXY[1] == bossXY[4] || playerXY[1] == bossXY[5])))
                                {
                                    bossXY[0] -= 1;
                                    bossXY[1] -= 1;
                                    bossXY[2] -= 1;
                                }
                            }
                            break;
                        case false:
                            if (bossXY[2] + 1 != 12)
                            {
                                if (!(playerXY[0] == bossXY[0] + 1 && (playerXY[1] == bossXY[3] || playerXY[1] == bossXY[4] || playerXY[1] == bossXY[5])))
                                {
                                    bossXY[0] += 1;
                                    bossXY[1] += 1;
                                    bossXY[2] += 1;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    switch (y)
                    {
                        case true:
                            if (bossXY[3] - 1 != 1)
                            {
                                if (!(playerXY[1] == bossXY[1] - 1 && (playerXY[0] == bossXY[0] || playerXY[0] == bossXY[1] || playerXY[0] == bossXY[2])))
                                {
                                    bossXY[3] -= 1;
                                    bossXY[4] -= 1;
                                    bossXY[5] -= 1;
                                }
                            }
                            break;
                        case false:
                            if (bossXY[5] + 1 != 12)
                            {
                                if (!(playerXY[1] == bossXY[1] + 1 && (playerXY[0] == bossXY[0] || playerXY[0] == bossXY[1] || playerXY[0] == bossXY[2])))
                                {
                                    bossXY[3] += 1;
                                    bossXY[4] += 1;
                                    bossXY[5] += 1;
                                }
                            }
                            break;
                    }
                }
            }
            else
            {
                step = 0;
            }

        }
    }
}
