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

namespace monoGame_Project
{
    internal class level9 : GameScreen
    {
        List<SoundEffect> SoundEffects = new List<SoundEffect>();
        public Song level;
        private new Game1 Game => (Game1)base.Game;
        public level9(Game1 Game) : base(Game)
        {
        }
        public override void LoadContent()
        {
            Game.player.Key = false;
            Game.generateTerrain = true;
            Game.playerPosition = new Vector2(160f, 160f);
            Game.conditionsMet = false;
            Game.keyFlag = false;
            this.level = Content.Load<Song>("level9");
            SoundEffects.Add(Content.Load<SoundEffect>("door01"));
            SoundEffects.Add(Content.Load<SoundEffect>("door02"));
            MediaPlayer.Play(level);
            Game.Start = true;
        }

        public int alma = 0;
        public int barack = 0;
        public override void Update(GameTime gameTime)
        {
            if (Game.player.Key)
            {
                Game.conditionsMet = true;
                if (alma == 0)
                {
                    SoundEffects[0].Play(0.1f, 0, 0);
                    alma++;
                }
            }
            if (Game.playerXY[0] == Game.doorXY[0] && Game.playerXY[1] == Game.doorXY[1] && Game.conditionsMet)
            {
                Game.LoadLevel10();
                if (barack == 0)
                {
                    SoundEffects[1].Play();
                    barack++;
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {

        }
    }
}