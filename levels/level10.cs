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
    internal class level10 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Song level;
        public level10(Game1 Game) : base(Game)
        {
        }
        public override void LoadContent()
        {
            Game.generateTerrain = true;
            this.level = Content.Load<Song>("finalboss");
            Game.playerPosition = new Vector2(160f, 160f);
            Game.backGround = Game.Content.Load<Texture2D>("level10");
            Game.conditionsMet = false;
            MediaPlayer.Play(level);
            Game.keyFlag = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game.Credits();
            }

        }
        public override void Draw(GameTime gameTime)
        {

        }

    }
}
