using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Content;
using System;
using System.Threading;
using MonoGame.Extended;
using monoGame_Project.levels;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static System.Formats.Asn1.AsnWriter;
using System.Xml;

namespace monoGame_Project
{
    public class Game1 : Game
    {
        public Character player;
        public Final_Boss finalboss;
        public int level = 0;
        public Texture2D obstacleTexture;
        public Texture2D heroTexture;
        public Texture2D goblin;
        public Texture2D bossTexture;
        public Texture2D backGround;
        public Texture2D door;
        public Texture2D doorLocked;
        public Texture2D keyFalse;
        public Texture2D keyTrue;
        public Texture2D pause_menu;
        public Texture2D dead_screen;
        public Texture2D stat_screen;
        public Texture2D potion;
        public Texture2D FinalBoss_Texture;
        public Boss boss;
        public Random rnd;
        public List<Enemy> enemyList;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool pauseFlag = false;
        bool Paused = false;
        bool Dead = false;
        bool IsAlive = false;
        bool surroundingBossFlage = false;
        public bool bossFlag = false;
        public bool bossFlag2 = false; //boss draw
        public bool level10flag = false;
        bool level10 = false;
        bool potionFlag;
        public bool keyFlag = false;
        public bool surroundFlag = false;
        public bool generateTerrain = false;
        public bool conditionsMet = false;
        public bool Start = false;
        public int[,] levelGrid = new int[13, 13];
        public int[] playerXY = new int[2];
        public int[] doorXY = new int[2] { 1, 1 };
        public int[] potionXY = new int[2];
        int temprnd;
        public int stepCount;
        public int finalBossSteps;
        bool step;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public ScreenManager scrnManager;
        Song main;
        List<SoundEffect> soundEffects;
        List<Enemy> surroundingEnemies;
        int goblinFightId;
        public float dieTimer;
        SpriteFont statFont;
        SpriteFont Lv;
        SpriteFont GoblinStat;
        int kills;
        Boss surroundingBoss;

        public Game1()
        {
            soundEffects = new List<SoundEffect>();
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 960;
            _graphics.PreferredBackBufferWidth = 960;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            scrnManager = new ScreenManager();
            Components.Add(scrnManager);


        }

        protected override void Initialize()
        {
            base.Initialize();
            MainMenu();
        }

        #region LevelLoads
        public void Credits()
        {
            scrnManager.LoadScreen(new credits(this), new FadeTransition(GraphicsDevice, Color.White, 5f));
        }
        public void MainMenu()
        {
            scrnManager.LoadScreen(new menu(this), new FadeTransition(GraphicsDevice, Color.Black, 0f));
            generateTerrain = false;
        }
        public void LoadLevel1()
        {
            scrnManager.LoadScreen(new level1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel2()
        {
            scrnManager.LoadScreen(new level2(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel3()
        {
            scrnManager.LoadScreen(new level3(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel4()
        {
            scrnManager.LoadScreen(new level4(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel5()
        {
            scrnManager.LoadScreen(new level5(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel6()
        {
            scrnManager.LoadScreen(new level6(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel7()
        {
            scrnManager.LoadScreen(new level7(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel8()
        {
            scrnManager.LoadScreen(new level8(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel9()
        {
            scrnManager.LoadScreen(new level9(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadLevel10()
        {
            scrnManager.LoadScreen(new level10(this), new FadeTransition(GraphicsDevice, Color.Red, 1f));
            level10flag = true;
        }
        #endregion

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //this.main = Content.Load<Song>("main");
            //MediaPlayer.Play(main);
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            //event hangok
            soundEffects.Add(Content.Load<SoundEffect>("pstep"));
            obstacleTexture = Content.Load<Texture2D>("obstacle");
            pause_menu = Content.Load<Texture2D>("pause_menu");
            dead_screen = Content.Load<Texture2D>("dead");
            stat_screen = Content.Load<Texture2D>("stats");
            player = new Character(Content.Load<Texture2D>("hero"));
            FinalBoss_Texture = Content.Load<Texture2D>("final_boss");
            Lv = Content.Load<SpriteFont>("Lv");
            statFont = Content.Load<SpriteFont>("statfont");
            GoblinStat = Content.Load<SpriteFont>("GoblinStat");
            backGround = Content.Load<Texture2D>("backGround");
            door = Content.Load<Texture2D>("door");
            doorLocked = Content.Load<Texture2D>("doorLocked");
            keyFalse = Content.Load<Texture2D>("KeyFalse");
            keyTrue = Content.Load<Texture2D>("KeyTrue");
            goblin = Content.Load<Texture2D>("goblin");
            potion = Content.Load<Texture2D>("potion");
            bossTexture = Content.Load<Texture2D>("enemy");
            rnd = new Random();
            level10flag = false;
            level10 = false;
            bossFlag = false;
            enemyList = new List<Enemy>();
            surroundingBoss = boss;
            Paused = false;
            pauseFlag = false;
            Dead = false;
            IsAlive = false;
            bossFlag = false;
            bossFlag2 = false;
            dieTimer = 0f;
            keyFlag = false;
            player.Key = false;
            kills = 0;
            stepCount = 0;
            level = 0;
            step = false;
            playerXY[0] = 2;
            playerXY[1] = 2;

        }
        //főcím dal
        //void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        //{
        //    // 0.0f is silent, 1.0f is full volume
        //    MediaPlayer.Volume -= 0.5f;
        //    MediaPlayer.Play(main);
        //}

        public float time1 = 0f;
        public float time2 = 0f;
        public float time3 = 0f;
        public float time4 = 0f;
        protected override void Update(GameTime gameTime)
        {
            switch (player.level)
            {
                case 10:
                    player.CharacterTexture = Content.Load<Texture2D>("hero2");
                    break;
                case 20:
                    player.CharacterTexture = Content.Load<Texture2D>("hero3");
                    break;
                case 30:
                    player.CharacterTexture = Content.Load<Texture2D>("hero4");
                    break;
                case 40:
                    player.CharacterTexture = Content.Load<Texture2D>("hero5");
                    break;
            }

            // TODO: MAIN MENU
            if (player.HP <= 0)
            {
                dieTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                Dead = true;
                if (dieTimer > 3000)
                {
                    if (IsAlive == false)
                    {
                        this.Initialize();
                        Dead = false;
                        IsAlive = false;
                        Start = false;
                        dieTimer = 0;
                    }
                }

            }

            var kstate = Keyboard.GetState();
            if (Dead == false)
            {

                if (Start)
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        if (!pauseFlag)
                        {
                            if (!Paused)
                            {
                                Paused = true;
                                pauseFlag = true;
                            }
                            else
                            {
                                Paused = false;
                                pauseFlag = true;
                            }
                        }
                    }
                    else
                    {
                        pauseFlag = false;
                    }
                }
                    if (Paused)
                    {
                        if (kstate.IsKeyDown(Keys.R))
                        {
                            this.Initialize();
                            Dead = false;
                            IsAlive = false;
                            Start = false;
                            Paused = false;
                            dieTimer = 0;
                        }
                        if (kstate.IsKeyDown(Keys.Space))
                        {
                            Exit();
                        }
                    }

                    if (!Paused)
                    {

                        if (kstate.IsKeyDown(Keys.F))
                        {
                            if (level10)
                            {
                                level10 = false;
                                LoadLevel10();
                            level10flag = true;
                            }
                        }


                        surroundingEnemies = Surrounding(enemyList, playerXY);

                    if (Start && !(playerXY[0] == doorXY[0] && playerXY[1] == doorXY[1] && conditionsMet))
                    {
                        #region MOVEMENT
                        #region W KEY
                        if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
                        {
                            time1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (!flag1)
                            {
                                //FALAK
                                if (levelGrid[playerXY[0], playerXY[1] - 1] != 1) // COLLISION
                                {
                                    //GOBLIN
                                    if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] - 1)
                                        && !(boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] - 1))
                                        {
                                        if (finalboss == null)
                                        {
                                            stepCount++;
                                            if (stepCount == 5 && enemyList.Count > 2)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            playerXY[1] -= 1;
                                            flag1 = true; // set when changing value
                                            if (step)
                                            {
                                                foreach (var item in enemyList)
                                                {
                                                    item.Advance(playerXY, levelGrid, enemyList, item, boss);
                                                }
                                                step = false;
                                            }
                                            else
                                            {
                                                step = true;
                                            }
                                        }
                                        else if (finalboss != null)
                                        {
                                            stepCount++;
                                            if (!((playerXY[0] == finalboss.position[0] || playerXY[0] == finalboss.position[1] || playerXY[0] == finalboss.position[2])
                                            && playerXY[1] - 1 == finalboss.position[5]))
                                            {
                                                playerXY[1] -= 1;
                                            }
                                            else
                                            {
                                                Fight();
                                            }
                                            if (stepCount == 5)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            flag1 = true; // set when changing value
                                            finalBossSteps++;
                                            finalboss.Advance(playerXY, ref finalBossSteps);
                                        }
                                    }
                                    else
                                    {
                                        if (enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] - 1))
                                        {
                                            goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] - 1);
                                            Fight(enemyList.ElementAt(goblinFightId));
                                            flag1 = true; // set when changing value
                                        }
                                        if (boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] - 1)
                                        {
                                            Fight(boss);
                                            flag1 = true; // set when changing value

                                        }
                                    }
                                }
                            }
                            if (time1 > 200f)
                            {
                                flag1 = false;
                                time1 = 0f;
                            }
                            surroundFlag = true;
                        }
                        else
                        {
                            flag1 = false; // reset when button is not down
                            time1 = 0f;
                        }
                        #endregion

                        #region A KEY
                        if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
                        {
                            time2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (!flag2)
                            {
                                //FALAK
                                if (levelGrid[playerXY[0] - 1, playerXY[1]] != 1) // COLLISION
                                {
                                    //GOBLIN    
                                    if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] - 1 && xy.position[1] == playerXY[1])
                                        && !(boss.position[0] == playerXY[0] - 1 && boss.position[1] == playerXY[1]))
                                    {
                                        if (finalboss == null)
                                        {
                                            stepCount++;
                                            if (stepCount == 5 && enemyList.Count > 2)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            playerXY[0] -= 1;
                                            flag2 = true; // set when changing value
                                            if (step)
                                            {
                                                foreach (var item in enemyList)
                                                {
                                                    item.Advance(playerXY, levelGrid, enemyList, item, boss);
                                                }
                                                step = false;
                                            }
                                            else
                                            {
                                                step = true;
                                            }
                                        }
                                        else
                                        {
                                            if (!((playerXY[1] == finalboss.position[3] || playerXY[1] == finalboss.position[4] || playerXY[1] == finalboss.position[5])
                                                && playerXY[0] - 1 == finalboss.position[2]))
                                            {
                                                playerXY[0] -= 1;
                                            }
                                            else
                                            {
                                                Fight();
                                            }
                                            if (stepCount == 5)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            flag2 = true;
                                            finalBossSteps++;
                                            finalboss.Advance(playerXY, ref finalBossSteps);
                                        }
                                    }
                                    else
                                    {
                                        if (enemyList.Exists(xy => xy.position[0] == playerXY[0] - 1 && xy.position[1] == playerXY[1]))
                                        {
                                            goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] - 1 && xy.position[1] == playerXY[1]);
                                            Fight(enemyList.ElementAt(goblinFightId));
                                            flag2 = true; // set when changing value
                                        }
                                        if (boss.position[0] == playerXY[0] - 1 && boss.position[1] == playerXY[1])
                                        {
                                            Fight(boss);
                                            flag2 = true; // set when changing value
                                        }
                                    }
                                }
                            }
                            if (time2 > 200f)
                            {
                                flag2 = false;
                                time2 = 0f;
                            }
                            surroundFlag = true;
                        }
                        else
                        {
                            flag2 = false; // reset when button is not down
                            time2 = 0f;
                        }
                        #endregion

                        #region S KEY
                        if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
                        {
                            time3 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (!flag3)
                            {
                                if (levelGrid[playerXY[0], playerXY[1] + 1] != 1)
                                {
                                    //GOBLIN
                                    if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] + 1)
                                        && !(boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] + 1))
                                    {
                                        if (finalboss == null)
                                        {
                                            stepCount++;
                                            if (stepCount == 5 && enemyList.Count > 2)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            playerXY[1] += 1;
                                            flag3 = true; // set when changing value
                                            if (step)
                                            {
                                                foreach (var item in enemyList)
                                                {
                                                    item.Advance(playerXY, levelGrid, enemyList, item, boss);
                                                }
                                                step = false;
                                            }
                                            else
                                            {
                                                step = true;
                                            }
                                        }
                                        else
                                        {
                                            if ((playerXY[0] == finalboss.position[0] || playerXY[0] == finalboss.position[1] || playerXY[0] == finalboss.position[2])
                                            && playerXY[1] + 1 == finalboss.position[3])
                                            {
                                                Fight();
                                            }
                                            else
                                            {
                                                playerXY[1] += 1;
                                            }
                                            stepCount++;
                                            if (stepCount == 5)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            finalBossSteps++;
                                            finalboss.Advance(playerXY, ref finalBossSteps);
                                            flag3 = true;
                                        }                                 
                                    }
                                    else
                                    {
                                        if (enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] + 1))
                                        {
                                            goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] + 1);
                                            Fight(enemyList.ElementAt(goblinFightId));
                                            flag3 = true; // set when changing value
                                        }
                                        if (boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] + 1)
                                        {
                                            Fight(boss);
                                            flag3 = true; // set when changing value

                                        }

                                    }
                                }
                            }
                            if (time3 > 200f)
                            {
                                flag3 = false;
                                time3 = 0f;
                            }
                            surroundFlag = true;
                        }
                        else
                        {
                            flag3 = false; // reset when button is not down
                            time3 = 0f;
                        }
                        #endregion

                        #region D KEY

                        if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
                        {
                            time4 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            if (!flag4)
                            {
                                if (levelGrid[playerXY[0] + 1, playerXY[1]] != 1)
                                {
                                    //GOBLIN
                                    if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] + 1 && xy.position[1] == playerXY[1])
                                        && !(boss.position[0] == playerXY[0] + 1 && boss.position[1] == playerXY[1]))
                                    {
                                        if (finalboss == null)
                                        {
                                            stepCount++;
                                            if (stepCount == 5 && enemyList.Count > 2)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            playerXY[0] += 1;
                                            flag4 = true;
                                            if (step)
                                            {
                                                foreach (var item in enemyList)
                                                {
                                                    item.Advance(playerXY, levelGrid, enemyList, item, boss);
                                                }
                                                step = false;
                                            }
                                            else
                                            {
                                                step = true;
                                            }
                                        }
                                        else
                                        {
                                            if(!((playerXY[1] == finalboss.position[3] || playerXY[1] == finalboss.position[4] || playerXY[1] == finalboss.position[5])
                                            && playerXY[0] + 1 == finalboss.position[0]))
                                            {
                                                playerXY[0] += 1;
                                            }
                                            else
                                            {
                                                Fight();
                                            }
                                            stepCount++;
                                            if (stepCount == 5)
                                            {
                                                if (player.HP + Convert.ToInt32(player.maxHP * 0.05f) > player.maxHP)
                                                {
                                                    player.HP = player.maxHP;
                                                    stepCount = 0;
                                                }
                                                else
                                                {
                                                    player.HP += Convert.ToInt32(player.maxHP * 0.05f);
                                                    stepCount = 0;
                                                }
                                            }
                                            finalBossSteps++;
                                            finalboss.Advance(playerXY, ref finalBossSteps);
                                            flag4 = true; // set when changing value
                                        }
                                    }
                                    else
                                    {
                                        if (enemyList.Exists(xy => xy.position[0] == playerXY[0] + 1 && xy.position[1] == playerXY[1]))
                                        {
                                            goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] + 1 && xy.position[1] == playerXY[1]);
                                            Fight(enemyList.ElementAt(goblinFightId));
                                            flag4 = true; // set when changing value
                                        }
                                        if (boss.position[0] == playerXY[0] + 1 && boss.position[1] == playerXY[1])
                                        {
                                            Fight(boss);
                                            flag4 = true; // set when changing value

                                        }
                                    }
                                }
                            }
                            if (time4 > 200f)
                            {
                                flag4 = false;
                                time4 = 0f;
                            }
                            surroundFlag = true;
                        }
                        else
                        {
                            flag4 = false; // reset when button is not down
                            time4 = 0f;
                        }
                        #endregion
                        #endregion
                        surroundingBoss = Surrounding(boss, playerXY);
                        surroundingEnemies = Surrounding(enemyList, playerXY);
                    }




                    if (playerXY[0] == potionXY[0] && playerXY[1] == potionXY[1])
                        {
                            potionFlag = false;
                            potionXY[0] = 0;
                            potionXY[1] = 0;
                            Heal(player);
                        }

                        //if (Keyboard.GetState().IsKeyDown(Keys.S))
                        //    soundEffects[0].CreateInstance().Play();
                        //if (Keyboard.GetState().IsKeyDown(Keys.A))
                        //    soundEffects[0].CreateInstance().Play();
                        //if (Keyboard.GetState().IsKeyDown(Keys.D))
                        //    soundEffects[0].CreateInstance().Play();

                    //if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    //{
                    //    if (SoundEffect.MasterVolume == 0.0f)
                    //        SoundEffect.MasterVolume = 1.0f;
                    //    else
                    //        SoundEffect.MasterVolume = 0.0f;
                    //}
                    base.Update(gameTime);
                }
            }
        }

        public float time5 = 0f;
        public float rotaation = 0f;

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);
            if (Start)
            {
                #region BACKGROUND
                // BACKGROUND
                _spriteBatch.Begin();
                _spriteBatch.Draw(
                backGround,
                new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2),
                null,
                Color.White,
                0f,
                new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
                _spriteBatch.End();
                #endregion

                #region AJTO
                // AJTO
                if (!level10flag)
                {
                    if (!conditionsMet)
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(
                        doorLocked,
                        new Vector2(_graphics.PreferredBackBufferWidth / 12f * doorXY[0] - doorLocked.Width / 2, _graphics.PreferredBackBufferHeight / 12f * doorXY[1] - doorLocked.Height / 2),
                        null,
                        Color.White,
                        0f,
                        new Vector2(obstacleTexture.Width / 2, obstacleTexture.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0.2f
                    );
                        _spriteBatch.End();
                    }
                    else
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(
                        door,
                        new Vector2(_graphics.PreferredBackBufferWidth / 12f * doorXY[0] - door.Width / 2, _graphics.PreferredBackBufferHeight / 12f * doorXY[1] - door.Height / 2),
                        null,
                        Color.White,
                        0f,
                        new Vector2(obstacleTexture.Width / 2, obstacleTexture.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0.2f
                    );
                        _spriteBatch.End();
                    }
                }
                #endregion

                #region PLAYER

                if ((playerXY[0] == doorXY[0] && playerXY[1] == doorXY[1] && conditionsMet))
                {
                    if (time5 < 1000)
                    {
                        time5 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        rotaation += 0.3f;
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(
                        player.CharacterTexture,
                        new Vector2(_graphics.PreferredBackBufferWidth / 12 * playerXY[0] - player.CharacterTexture.Width / 2, _graphics.PreferredBackBufferWidth / 12 * playerXY[1] - player.CharacterTexture.Height / 2),
                        null,
                        Color.White,
                        rotaation,
                        new Vector2(player.CharacterTexture.Width / 2, player.CharacterTexture.Height / 2),
                        new Vector2(1 - rotaation / 10, 1 - rotaation / 10),
                        SpriteEffects.None,
                        0.1f
                        );
                        _spriteBatch.End();
                    }
                }
                else
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                    player.CharacterTexture,
                    new Vector2(_graphics.PreferredBackBufferWidth / 12 * playerXY[0] - player.CharacterTexture.Width / 2, _graphics.PreferredBackBufferWidth / 12 * playerXY[1] - player.CharacterTexture.Height / 2),
                    null,
                    Color.White,
                    0f,
                    new Vector2(player.CharacterTexture.Width / 2, player.CharacterTexture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0.1f
                    );
                    _spriteBatch.End();
                    time5 = 0;
                    rotaation = 0;
                }

                #endregion

                #region GRID GENERÁLÁS PER PÁLYA
                if (generateTerrain)
                {
                    level++;
                    enemyList.Clear();
                    // HA GENERÁL AKKOR TÖRLI AZ EGÉSZET
                    for (int i = 1; i < levelGrid.GetLength(0); i++)
                    {
                        for (int j = 1; j < levelGrid.GetLength(1); j++)
                        {
                            levelGrid[i, j] = 0;
                        }

                    }
                    // ÉS CSINÁL EGY ÚJ GRIDET
                    for (int i = 1; i < levelGrid.GetLength(0); i++)
                    {
                        for (int j = 1; j < levelGrid.GetLength(1); j++)
                        {
                            if (level10flag)
                            {
                                if (i == 1 || i == 12 || ((i < 12 && i > 1) && (j == 1 || j == 12)))
                                {
                                    levelGrid[i, j] = 1;
                                }
                            }
                            else
                            {
                                if (i == 1 || i == 12 || ((i < 12 && i > 1) && (j == 1 || j == 12)))
                                {
                                    levelGrid[i, j] = 1;

                                }
                                if (levelGrid.GetLength(0) > i && levelGrid.GetLength(1) > j && i > 1 && j > 1)
                                {
                                    if (i != 12 && j != 12)
                                    {
                                        if (rnd.Next(1, 101) > 30 // RANDOM SZÁZALÉK ESÉLY / BLOKK
                                           && (levelGrid[i - 1, j - 1] != 1 && levelGrid[i - 1, j + 1] != 1 &&
                                           levelGrid[i + 1, j - 1] != 1 && levelGrid[i + 1, j + 1] != 1) &&        //CSÜCSÖKCSEKK
                                            !(i == 2 || i == 11 || ((i < 11 && i > 2) && (j == 2 || j == 11))))
                                        {
                                            levelGrid[i, j] = 1;
                                        }
                                    }

                                }
                            }

                        }

                    }
                    if (!level10flag)
                    {
                        while (generateTerrain)
                        {
                            for (int i = 1; i < levelGrid.GetLength(0); i++)
                            {
                                for (int j = 1; j < levelGrid.GetLength(1); j++)
                                {
                                    if (levelGrid[i, j] != 1 && rnd.Next(1, 101) > 95 && (i != 1 && j != 1))
                                    {
                                        doorXY[0] = i;
                                        doorXY[1] = j;
                                        generateTerrain = false;
                                        temprnd = rnd.Next(3, 7);
                                        break;
                                    }
                                }
                                if (!generateTerrain)
                                {
                                    break;
                                }

                            }
                            //GOBLINOK HELYE
                            for (int k = 0; k < temprnd; k++)
                            {
                                bool enemyspawn = false;
                                while (!enemyspawn)
                                {

                                    for (int i = 1; i < levelGrid.GetLength(0) - 1; i++)
                                    {
                                        for (int j = 1; j < levelGrid.GetLength(1) - 1; j++)
                                        {
                                            if (rnd.Next(1, 101) > 95 && (i != 2 && j != 2) && levelGrid[i, j] != 1 && (doorXY[0] != i && doorXY[1] != j) && !enemyList.Exists(xy => xy.position[0] == i && xy.position[1] == j))
                                            {
                                                enemyList.Add(new Enemy(goblin, level, i, j));
                                                enemyspawn = true;
                                                break;

                                            }
                                        }
                                        if (enemyspawn)
                                        {
                                            break;
                                        }

                                    }
                                }

                            }

                            // POTION

                            bool potionspawn = false;
                            while (!potionspawn)
                            {

                                for (int i = 1; i < levelGrid.GetLength(0); i++)
                                {
                                    for (int j = 1; j < levelGrid.GetLength(1); j++)
                                    {
                                        if (rnd.Next(1, 101) > 95 && (i != 1 && j != 1) && levelGrid[i, j] != 1 && (doorXY[0] != i && doorXY[1] != j) && !enemyList.Exists(xy => xy.position[0] == i && xy.position[1] == j) && !(i == 2 && j == 2))
                                        {
                                            potionXY[0] = i;
                                            potionXY[1] = j;
                                            potionspawn = true;
                                            potionFlag = true;
                                            break;

                                        }
                                    }
                                    if (potionspawn)
                                        break;
                                }

                            }
                            bool bossSpawn = false;

                            while (!bossSpawn)
                            {

                                for (int i = 1; i < levelGrid.GetLength(0); i++)
                                {
                                    for (int j = 1; j < levelGrid.GetLength(1); j++)
                                    {
                                        if (rnd.Next(1, 101) > 95 && (i != 1 && j != 1) && levelGrid[i, j] != 1 && (doorXY[0] != i && doorXY[1] != j) && !enemyList.Exists(xy => xy.position[0] == i && xy.position[1] == j) && (potionXY[0] != i && potionXY[1] != j))
                                        {
                                            boss = new Boss(bossTexture, level, i, j);
                                            boss.position[0] = i;
                                            boss.position[1] = j;
                                            bossSpawn = true;
                                            bossFlag2 = true;
                                            break;
                                        }
                                    }
                                    if (bossSpawn)
                                        break;
                                }

                            }
                        }
                        if (!keyFlag)
                            keyAdd(enemyList);
                    }
                }

                #endregion

                #region GOBLIN GENERÁLÁS
                foreach (var item in enemyList)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                    goblin,
                    new Vector2(_graphics.PreferredBackBufferWidth / 12f * item.position[0] - item.CharacterTexture.Width / 2, _graphics.PreferredBackBufferHeight / 12f * item.position[1] - item.CharacterTexture.Height / 2),
                    null,
                    Color.White,
                    0f,
                    new Vector2(obstacleTexture.Width / 2, obstacleTexture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
                    _spriteBatch.End();
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString(
                    Lv,
                    "lv " + item.level.ToString(),
                    new Vector2(_graphics.PreferredBackBufferWidth / 12f * item.position[0] - 30 - item.CharacterTexture.Width / 2, _graphics.PreferredBackBufferHeight / 12f * item.position[1] - 40 - item.CharacterTexture.Height / 2),
                    Color.White);
                    _spriteBatch.End();
                }
                if (bossFlag2)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                    bossTexture,
                    new Vector2(_graphics.PreferredBackBufferWidth / 12f * boss.position[0] - bossTexture.Width / 2, _graphics.PreferredBackBufferHeight / 12f * boss.position[1] - bossTexture.Height / 2),
                    null,
                    Color.White,
                    0f,
                    new Vector2(bossTexture.Width / 2, bossTexture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
                    _spriteBatch.End();
                }

                #endregion

                #region PÁLYA VETITÉSE
                // A GRIDET KIVETITI FOLYAMATOSAN
                for (int i = 1; i < levelGrid.GetLength(0); i++)
                {
                    for (int j = 1; j < levelGrid.GetLength(1); j++)
                    {
                        if (levelGrid[i, j] == 1)
                        {
                            _spriteBatch.Begin();
                            _spriteBatch.Draw(
                            obstacleTexture,
                            new Vector2(_graphics.PreferredBackBufferWidth / 12f * i - obstacleTexture.Width / 2, _graphics.PreferredBackBufferHeight / 12f * j - obstacleTexture.Height / 2),
                            null,
                            Color.White,
                            0f,
                            new Vector2(obstacleTexture.Width / 2, obstacleTexture.Height / 2),
                            Vector2.One,
                            SpriteEffects.None,
                            0.1f
                        );
                            _spriteBatch.End();
                        }
                    }
                }
                #endregion

                if (potionFlag)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                        potion,
                        new Vector2(_graphics.PreferredBackBufferWidth / 12 * potionXY[0] - potion.Width / 2, _graphics.PreferredBackBufferHeight / 12 * potionXY[1] - potion.Height / 2),
                        null,
                        Color.White,
                        0f,
                        new Vector2(potion.Width / 2, potion.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0.7f
                        );
                    _spriteBatch.End();
                }

                if (Paused)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                        pause_menu,
                        new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2),
                        null,
                        Color.White,
                        0f,
                        new Vector2(pause_menu.Width / 2, pause_menu.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0.7f
                        );
                    _spriteBatch.End();
                }

                #region MINECRAFT SIGN
                _spriteBatch.Begin();
                _spriteBatch.Draw(
                    stat_screen,
                    new Vector2(480, 40),
                    null,
                    Color.White,
                    0f,
                    new Vector2(stat_screen.Width / 2, stat_screen.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                0.7f
                    );
                _spriteBatch.End();
                _spriteBatch.Begin();
                _spriteBatch.Draw(
                    stat_screen,
                    new Vector2(480, 920),
                    null,
                    Color.White,
                    0f,
                    new Vector2(stat_screen.Width / 2, stat_screen.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                0.7f
                    );
                _spriteBatch.End();
                #endregion

                #region STATS
                //PLAYER STATS
                _spriteBatch.Begin();
                _spriteBatch.DrawString(statFont, $"HP:{player.HP} / {player.maxHP}", new Vector2(60, 20), Color.Black);
                _spriteBatch.DrawString(statFont, $"Armor:{player.shield}", new Vector2(240, 20), Color.Black);
                _spriteBatch.DrawString(statFont, $"Level:{player.level}", new Vector2(450, 20), Color.Black);
                _spriteBatch.DrawString(statFont, $"Damage:{player.damage}", new Vector2(630, 20), Color.Black);
                _spriteBatch.End();


                //GOBLIN STATS
                string surroundingA = "";
                string surroundingJ = "";
                string surroundingB = "";
                string surroundingF = "";


                if (surroundingBoss != null)
                {
                    if (surroundingBoss.around == "Alattad:")
                    {
                        surroundingA = $"HP:{surroundingBoss.HP}  SH:{surroundingBoss.shield}  PW:{surroundingBoss.damage}";
                    }
                    if (surroundingBoss.around == "Jobbra:")
                    {
                        surroundingJ = $"HP:{surroundingBoss.HP}  SH:{surroundingBoss.shield}  PW:{surroundingBoss.damage}";
                    }
                    if (surroundingBoss.around == "Balra:")
                    {
                        surroundingB = $"HP:{surroundingBoss.HP}  SH:{surroundingBoss.shield}  PW:{surroundingBoss.damage}";
                    }
                    if (surroundingBoss.around == "Feletted:")
                    {
                        surroundingF = $"HP:{surroundingBoss.HP}  SH:{surroundingBoss.shield}  PW:{surroundingBoss.damage}";
                    }
                    _spriteBatch.Begin();
                    if (surroundingA != "")
                        _spriteBatch.DrawString(GoblinStat, "(Boss)", new Vector2(75, 905), Color.Black);
                    if (surroundingJ != "")
                        _spriteBatch.DrawString(GoblinStat, "(Boss)", new Vector2(318, 905), Color.Black);
                    if (surroundingB != "")
                        _spriteBatch.DrawString(GoblinStat, "(Boss)", new Vector2(560, 905), Color.Black);
                    if (surroundingF != "")
                        _spriteBatch.DrawString(GoblinStat, "(Boss)", new Vector2(795, 905), Color.Black);
                    _spriteBatch.End();
                }



                if (surroundingEnemies != null)
                {
                    foreach (var item in surroundingEnemies)
                    {
                        if (item.around == "Alattad:")
                        {
                            surroundingA = $"HP:{item.HP}  SH:{item.shield}  PW:{item.damage}";
                        }
                        if (item.around == "Jobbra:")
                        {
                            surroundingJ = $"HP:{item.HP}  SH:{item.shield}  PW:{item.damage}";
                        }
                        if (item.around == "Balra:")
                        {
                            surroundingB = $"HP:{item.HP}  SH:{item.shield}  PW:{item.damage}";
                        }
                        if (item.around == "Feletted:")
                        {
                            surroundingF = $"HP:{item.HP}  SH:{item.shield}  PW:{item.damage}";
                        }
                    }
                }





                _spriteBatch.Begin();



                _spriteBatch.DrawString(GoblinStat, surroundingA, new Vector2(10, 928), Color.Black);
                _spriteBatch.DrawString(GoblinStat, surroundingJ, new Vector2(255, 928), Color.Black);
                _spriteBatch.DrawString(GoblinStat, surroundingB, new Vector2(505, 928), Color.Black);
                _spriteBatch.DrawString(GoblinStat, surroundingF, new Vector2(730, 928), Color.Black);
                _spriteBatch.DrawString(statFont, "Alattad:", new Vector2(60, 879), Color.Black);
                _spriteBatch.DrawString(statFont, "Jobbra:", new Vector2(305, 879), Color.Black);
                _spriteBatch.DrawString(statFont, "Balra:", new Vector2(555, 879), Color.Black);
                _spriteBatch.DrawString(statFont, "Feletted:", new Vector2(780, 879), Color.Black);
                _spriteBatch.End();
                surroundingBossFlage = false;
                #endregion

                #region DEAD SCREEN
                if (Dead == true && IsAlive == false)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                        dead_screen,
                        new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2),
                        null,
                        Color.White,
                        0f,
                        new Vector2(dead_screen.Width / 2, dead_screen.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0.7f
                        );
                    _spriteBatch.End();
                }
                #endregion

                #region KEY
                if (player.Key)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                        keyTrue,
                        new Vector2(900, 30),
                        null,
                        Color.White,
                        0f,
                        new Vector2(keyTrue.Width / 2, keyTrue.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0.7f
                        );
                    _spriteBatch.End();
                }
                else
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(
                        keyFalse,
                        new Vector2(900, 30),
                        null,
                        Color.White,
                        0f,
                        new Vector2(keyFalse.Width / 2, keyFalse.Height / 2),
                        Vector2.One,
                        SpriteEffects.None,
                        0.7f
                        );
                    _spriteBatch.End();
                }
                #endregion


            }
            base.Draw(gameTime);
        }

        public void Heal(Character player)
        {
            if (player.HP + Convert.ToInt32(player.maxHP * 0.25) > player.maxHP)
            {
                player.HP = player.maxHP;
                return;
            }
            player.HP += Convert.ToInt32(player.maxHP * 0.25);

        }

        public void Fight(Enemy currentEnemy)
        {
            //do
            // {
            player.Attack(player.Damage(player), currentEnemy);
            currentEnemy.Attack(currentEnemy.Damage(currentEnemy), player);
            if (currentEnemy.HP <= 0)
            {
                if (currentEnemy.key == true)
                    player.Key = true;
                kills++;
                enemyList.Remove(currentEnemy);
                player.level++;
                if (player.level % 2 == 0)
                    player.levelUp();
                // break;
            }
            // } while (currentEnemy.HP > 0 || player.HP > 0);
            //}
        }

        public void Fight(Boss boss)
        {
            //do
            // {
            player.Attack(player.Damage(player), boss);
            boss.Attack(boss.Damage(boss), player);
            if (boss.HP <= 0)
            {
                boss.position[1] = 0; boss.position[0] = 0;
                bossFlag = true;
                player.level++;
                if (player.level % 2 == 0)
                    player.levelUp();
                // break;
            }
            // } while (currentEnemy.HP > 0 || player.HP > 0);
            //}
        }
        public void Fight()
        {
            //do
            // {
            player.Attack(player.Damage(player), finalboss);
            finalboss.Attack(player);
            if (finalboss.HP <= 0)
            {
                Credits();
            }
            // } while (currentEnemy.HP > 0 || player.HP > 0);


            //}
        }

        public void keyAdd(List<Enemy> enemies)
        {
            int keyGoblinId;
            keyGoblinId = rnd.Next(1, enemyList.Count);
            enemyList[keyGoblinId].key = true;
            keyFlag = true;
        }

        List<Enemy> Surrounding(List<Enemy> enemies, int[] playerXY)
        {
            List<Enemy> result = new List<Enemy>();

            foreach (var enemy in enemies)
            {
                if (enemy.position[0] == playerXY[0] && enemy.position[1] == playerXY[1] - 1)
                {
                    enemy.around = "Feletted:";
                    result.Add(enemy);
                }
                if (enemy.position[0] == playerXY[0] - 1 && enemy.position[1] == playerXY[1])
                {
                    enemy.around = "Balra:";
                    result.Add(enemy);
                }
                if (enemy.position[0] == playerXY[0] && enemy.position[1] == playerXY[1] + 1)
                {
                    enemy.around = "Alattad:";
                    result.Add(enemy);
                }
                if (enemy.position[0] == playerXY[0] + 1 && enemy.position[1] == playerXY[1])
                {
                    enemy.around = "Jobbra:";
                    result.Add(enemy);
                }
            }
            return result;
        }
        Boss Surrounding(Boss boss, int[] playerXY)
        {
            Boss result = boss;
            if (boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] - 1)
            {
                boss.around = "Feletted:";
                return result;
            }
            else
            {
                boss.around = "";
            }
            if (boss.position[0] == playerXY[0] - 1 && boss.position[1] == playerXY[1])
            {
                boss.around = "Balra:";
                return result;
            }
            else
            {
                boss.around = "";

            }
            if (boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] + 1)
            {
                boss.around = "Alattad:";
                return result;
            }
            else
            {
                boss.around = "";
            }
            if (boss.position[0] == playerXY[0] + 1 && boss.position[1] == playerXY[1])
            {
                boss.around = "Jobbra:";
                return result;
            }
            else
            {
                boss.around = "";
            }
            return result;
        }

    }
}