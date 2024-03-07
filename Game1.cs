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

namespace monoGame_Project
{
    public class Game1 : Game
    {
        public Vector2 playerPosition;
        public Character player;
        public int level = 0;
        public Texture2D obstacleTexture;
        public Texture2D heroTexture;
        public Texture2D goblin;
        public Texture2D bossTexture;
        public Texture2D backGround;
        public Texture2D door;
        public Texture2D doorLocked;
        public Texture2D pause_menu;
        public Texture2D dead_screen;
        public Texture2D potion;
        public Texture2D stat_screen;
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
        bool level10flag = false;
        bool potionFlag;
        bool bossFlag;
        public bool keyFlag = false;
        public bool generateTerrain = false;
        public bool conditionsMet = false;
        public bool Start = false;
        public int[,] levelGrid = new int[12, 12];
        public int[] playerXY = new int[2];
        public int[] doorXY = new int[2] { 1, 1 };
        public int[] potionXY = new int[2];
        int temprnd;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public ScreenManager scrnManager;
        List<SoundEffect> soundEffects;
        int goblinFightId;
        public float dieTimer;
        SpriteFont Lv;
        SpriteFont statfont;
        public Song main;


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

        // TODO: CREDITS
        #region LevelLoads
        public void Credits()
        {
            scrnManager.LoadScreen(new credits(this), new FadeTransition(GraphicsDevice, Color.White, 5f));
        }
        public void MainMenu()
        {
            scrnManager.LoadScreen(new menu(this), new FadeTransition(GraphicsDevice, Color.Black, 5f));
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
            //event hangok
            soundEffects.Add(Content.Load<SoundEffect>("Moving"));
            soundEffects.Add(Content.Load<SoundEffect>("LowHP"));
            soundEffects.Add(Content.Load<SoundEffect>("enemyDamaged"));
            soundEffects.Add(Content.Load<SoundEffect>("levelUp"));
            soundEffects.Add(Content.Load<SoundEffect>("heal"));
            soundEffects.Add(Content.Load<SoundEffect>("armourBreak"));
            obstacleTexture = Content.Load<Texture2D>("obstacle");
            pause_menu = Content.Load<Texture2D>("pause_menu");
            dead_screen = Content.Load<Texture2D>("dead");
            stat_screen = Content.Load<Texture2D>("stats");
            player = new Character(Content.Load<Texture2D>("hero"));
            Lv = Content.Load<SpriteFont>("Lv");
            statfont = Content.Load<SpriteFont>("statfont");
            backGround = Content.Load<Texture2D>("backGround");
            door = Content.Load<Texture2D>("door");
            playerPosition = new Vector2(160f, 160f);
            doorLocked = Content.Load<Texture2D>("doorLocked");
            goblin = Content.Load<Texture2D>("goblin");
            potion = Content.Load<Texture2D>("potion");
            bossTexture = Content.Load<Texture2D>("enemy");
            rnd = new Random();
            enemyList = new List<Enemy>();
            Dead = false;
            IsAlive = false;
            dieTimer = 0f;
            keyFlag = false;
            player.Key = false;
            level = 1;
            this.main = Content.Load<Song>("main");
            MediaPlayer.Play(main);
            MediaPlayer.Volume -= 0.8f;
        }

        public float time1 = 0f;
        public float time2 = 0f;
        public float time3 = 0f;
        public float time4 = 0f;
        public int alma = 0;
        public int Körte = 0;
        public int szőlő = 0;
        public int szilva = 0;
        protected override void Update(GameTime gameTime)
        {
            switch (player.level)
            {
                case 10:
                    player.CharacterTexture = Content.Load<Texture2D>("hero2");
                    if (alma==0)
                    {
                    soundEffects[3].Play();
                        alma++;
                    }
                    break;
                case 20:
                    player.CharacterTexture = Content.Load<Texture2D>("hero3");
                    if (Körte == 0)
                    {
                        soundEffects[3].Play();
                        Körte++;
                    }

                    break;
                case 30:
                    player.CharacterTexture = Content.Load<Texture2D>("hero4");
                    if (szilva == 0)
                    {
                        soundEffects[3].Play();
                        szilva++;
                    }

                    break;
                case 40:
                    player.CharacterTexture = Content.Load<Texture2D>("hero5");
                    if (szőlő == 0)
                    {
                        soundEffects[3].Play();
                        szőlő++;
                    }

                    break;
            }

            playerXY[0] = Convert.ToInt32(playerPosition.X / 80);
            playerXY[1] = Convert.ToInt32(playerPosition.Y / 80);
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
                


                if (!Paused)
                {
                    var kstate = Keyboard.GetState();

                    if (kstate.IsKeyDown(Keys.F))
                    {
                        if (level10flag)
                        {
                            LoadLevel10();
                            level10flag = false;
                        }
                    }



                    #region MOVEMENT
                    #region W KEY
                    if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
                    {
                        time1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (!flag1)
                        {
                            //FALAK
                            if (levelGrid[Convert.ToInt32(playerPosition.X / 80) - 1,
                                Convert.ToInt32((playerPosition.Y - 80) / 80) - 1] != 1) // COLLISION
                            {
                                //GOBLIN
                                if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] - 1)
                                    && !(boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] - 1))
                                {
                                    playerPosition.Y -= 80;
                                    flag1 = true; // set when changing value
                                    int barack = 0;
                                    if (barack == 0)
                                    {
                                        soundEffects[0].Play(0.1f, 0, 0);
                                        barack++;
                                    }
                                }
                                else
                                {
                                    if (enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] - 1))
                                    {
                                        goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] - 1);
                                        Fight(enemyList, enemyList.ElementAt(goblinFightId));
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
                            if (levelGrid[Convert.ToInt32((playerPosition.X - 80) / 80) - 1,
                            Convert.ToInt32(playerPosition.Y / 80) - 1] != 1) // COLLISION
                            {
                                //GOBLIN    
                                if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] - 1 && xy.position[1] == playerXY[1]) 
                                    && !(boss.position[0] == playerXY[0] - 1 && boss.position[1] == playerXY[1]))
                                {
                                    playerPosition.X -= 80;
                                    flag2 = true; // set when changing value
                                    int barack = 0;
                                    if (barack == 0)
                                    {
                                        soundEffects[0].Play(0.1f, 0, 0);
                                        barack++;
                                    }
                                }
                                else
                                {
                                    if (enemyList.Exists(xy => xy.position[0] == playerXY[0] - 1 && xy.position[1] == playerXY[1]))
                                    {
                                        goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] - 1 && xy.position[1] == playerXY[1]);
                                        Fight(enemyList, enemyList.ElementAt(goblinFightId));
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
                            if (levelGrid[Convert.ToInt32(playerPosition.X / 80) - 1,
                            Convert.ToInt32((playerPosition.Y + 80) / 80) - 1] != 1)
                            {
                                //GOBLIN
                                if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] + 1)
                                    && !(boss.position[0] == playerXY[0] && boss.position[1] == playerXY[1] + 1))
                                {
                                    playerPosition.Y += 80;
                                    flag3 = true; // set when changing value
                                    int barack = 0;
                                    if (barack == 0)
                                    {
                                        soundEffects[0].Play(0.1f, 0, 0);
                                        barack++;
                                    }
                                }
                                else
                                {
                                    if (enemyList.Exists(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] + 1))
                                    {
                                        goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] && xy.position[1] == playerXY[1] + 1);
                                        Fight(enemyList, enemyList.ElementAt(goblinFightId));
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
                            if (levelGrid[Convert.ToInt32((playerPosition.X + 80) / 80) - 1,
                            Convert.ToInt32(playerPosition.Y / 80) - 1] != 1)
                            {
                                //GOBLIN
                                if (!enemyList.Exists(xy => xy.position[0] == playerXY[0] + 1 && xy.position[1] == playerXY[1])
                                    && !(boss.position[0] == playerXY[0] + 1 && boss.position[1] == playerXY[1]))
                                {
                                    playerPosition.X += 80;
                                    flag4 = true; // set when changing value
                                    int barack = 0;
                                    if (barack == 0)
                                    {
                                        soundEffects[0].Play(0.1f, 0, 0);
                                        barack++;
                                    }
                                }
                                else
                                {
                                    if (enemyList.Exists(xy => xy.position[0] == playerXY[0] + 1 && xy.position[1] == playerXY[1]))
                                    {
                                        goblinFightId = enemyList.FindIndex(xy => xy.position[0] == playerXY[0] + 1 && xy.position[1] == playerXY[1]);
                                        Fight(enemyList, enemyList.ElementAt(goblinFightId));
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
                    }
                    else
                    {
                        flag4 = false; // reset when button is not down
                        time4 = 0f;
                    }
                    #endregion
                    #endregion

                    if (playerXY[0] == potionXY[0] && playerXY[1] == potionXY[1])
                    {
                        potionFlag = false;
                        potionXY[0] = 0;
                        potionXY[1] = 0;
                        Heal(player);
                    }
                    base.Update(gameTime);
                }
            }
        }
        public int narancs = 0;
        public int mandarin = 0;
        public int citrom = 0;
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
                1f
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

                // PLAYER
                _spriteBatch.Begin();
                _spriteBatch.Draw(
                player.CharacterTexture,
                playerPosition,
                null,
                Color.White,
                0f,
                new Vector2(_graphics.PreferredBackBufferWidth / 12, _graphics.PreferredBackBufferHeight / 12),
                Vector2.One,
                SpriteEffects.None,
                0.1f
                );
                _spriteBatch.End();



                #region GRID GENERÁLÁS PER PÁLYA
                if (generateTerrain)
                {
                    level++;
                    enemyList.Clear();
                    // HA GENERÁL AKKOR TÖRLI AZ EGÉSZET
                    for (int i = 0; i < levelGrid.GetLength(0) - 1; i++)
                    {
                        for (int j = 0; j < levelGrid.GetLength(1) - 1; j++)
                        {
                            levelGrid[i, j] = 0;
                        }

                    }
                    // ÉS CSINÁL EGY ÚJ GRIDET
                    for (int i = 0; i < levelGrid.GetLength(0); i++)
                    {
                        for (int j = 0; j < levelGrid.GetLength(1); j++)
                        {
                            if (level10flag)
                            {
                                if (i + 1 == 1 || i + 1 == 12 || ((i + 1 < 12 && i + 1 > 1) && (j + 1 == 1 || j + 1 == 12)))
                                {
                                    levelGrid[i, j] = 1;
                                }
                            }
                            else
                            {
                                if (i + 1 == 1 || i + 1 == 12 || ((i + 1 < 12 && i + 1 > 1) && (j + 1 == 1 || j + 1 == 12)))
                                {
                                    levelGrid[i, j] = 1;

                                }
                                if (levelGrid.GetLength(0) - 2 > i && levelGrid.GetLength(1) - 2 > j && i > 1 && j > 1)
                                {
                                    if (rnd.Next(1, 101) > 30 // RANDOM SZÁZALÉK ESÉLY / BLOKK
                                        && levelGrid[i - 1, j - 1] != 1 && levelGrid[i - 1, j + 1] != 1 &&
                                        levelGrid[i + 1, j - 1] != 1 && levelGrid[i + 1, j + 1] != 1)       //CSÜCSÖKCSEKK
                                    {
                                        levelGrid[i, j] = 1;
                                    }
                                }
                            }

                        }

                    }
                    if (!level10flag)
                    {
                        while (generateTerrain)
                        {
                            for (int i = 0; i < levelGrid.GetLength(0); i++)
                            {
                                for (int j = 0; j < levelGrid.GetLength(1); j++)
                                {
                                    if (levelGrid[i, j] != 1 && rnd.Next(1, 101) > 95 && (i != 1 && j != 1))
                                    {
                                        doorXY[0] = i + 1;
                                        doorXY[1] = j + 1;
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

                                    for (int i = 0; i < levelGrid.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < levelGrid.GetLength(1); j++)
                                        {
                                            if (rnd.Next(1, 101) > 95 && (i != 1 && j != 1) && levelGrid[i, j] != 1 && (doorXY[0] != i + 1 && doorXY[1] != j + 1) && !enemyList.Exists(xy => xy.position[0] == i + 1 && xy.position[1] == j + 1))
                                            {
                                                enemyList.Add(new Enemy(goblin, new Vector2(0, 0), level, i + 1, j + 1));
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

                                for (int i = 0; i < levelGrid.GetLength(0); i++)
                                {
                                    for (int j = 0; j < levelGrid.GetLength(1); j++)
                                    {
                                        if (rnd.Next(1, 101) > 95 && (i != 1 && j != 1) && levelGrid[i, j] != 1 && (doorXY[0] != i + 1 && doorXY[1] != j + 1) && !enemyList.Exists(xy => xy.position[0] == i + 1 && xy.position[1] == j + 1))
                                        {
                                            potionXY[0] = i + 1;
                                            potionXY[1] = j + 1;
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

                                for (int i = 0; i < levelGrid.GetLength(0); i++)
                                {
                                    for (int j = 0; j < levelGrid.GetLength(1); j++)
                                    {
                                        if (rnd.Next(1, 101) > 95 && (i != 1 && j != 1) && levelGrid[i, j] != 1 && (doorXY[0] != i + 1 && doorXY[1] != j + 1) && !enemyList.Exists(xy => xy.position[0] == i + 1 && xy.position[1] == j + 1) && (potionXY[0] != i + i && potionXY[1] != j + 1))
                                        {
                                            boss = new Boss(bossTexture, level, i + 1, j + 1);
                                            boss.position[0] = i + 1;
                                            boss.position[1] = j + 1;
                                            bossSpawn = true;
                                            bossFlag = true;
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
                if (bossFlag)
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
                for (int i = 0; i < levelGrid.GetLength(0); i++)
            {
                for (int j = 0; j < levelGrid.GetLength(1); j++)
                {
                    if (levelGrid[i, j] == 1)
                    {
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(
                        obstacleTexture,
                        new Vector2(_graphics.PreferredBackBufferWidth / 12f * (i + 1) - obstacleTexture.Width / 2, _graphics.PreferredBackBufferHeight / 12f * (j + 1) - obstacleTexture.Height / 2),
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
                string stattext = "             HP: " + player.HP.ToString() + "                    Armor: " + player.shield.ToString() + "                      Level: " + player.level.ToString();

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
                _spriteBatch.DrawString(
                    statfont,
                    stattext,
                    new Vector2(0, 20),
                    Color.Black);
                _spriteBatch.End();


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
            }
            base.Draw(gameTime);
        }
        public void Heal(Character player)
        {
            if (player.HP + player.maxHP / 10 > player.maxHP)
            {
                return;
            }
            player.HP += Convert.ToInt32(player.maxHP * 0.25);
                player.HP = player.maxHP;
                if (narancs==0)
                {
                    soundEffects[4].Play(1f, 1, 1);
                    narancs = 1;
                }
        }

        float fightTimer = 0;
        public void Fight(List<Enemy> enemies, Enemy currentEnemy)
        {
            //do
            // {
            player.Attack(player.Damage(player), enemyList[goblinFightId]);
            enemyList[goblinFightId].Attack(enemyList[goblinFightId].Damage(enemyList[goblinFightId]), player);
            if (player.shield<=0)
            {
                if (citrom==0)
                {
                    soundEffects[5].Play();
                    citrom++;
                }
            }
            if (player.maxHP*0.1>player.HP)
            {
                if (mandarin == 0)
                {
                    soundEffects[1].Play();
                    mandarin++;
                }
            }
            if (currentEnemy.HP <= 0)
            {
                soundEffects[2].Play();
                if (currentEnemy.key == true)
                    player.Key = true;

                enemies.Remove(currentEnemy);
                player.level++;
                if(player.level % 2 == 0)
                    player.levelUp();
                // break;
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

    }
}
