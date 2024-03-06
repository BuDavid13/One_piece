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
        public level10(Game1 Game) : base(Game)
        {
        }
        public override void LoadContent()
        {
            Game.generateTerrain = true;
            Game.playerXY[0] = 2;
            Game.playerXY[1] = 2;
            Game.backGround = Game.Content.Load<Texture2D>("level10");
            Game.conditionsMet = false;
            Game.bossFlag = false;
            Game.keyFlag = false;
            Game.player.shield = Game.player.maxShield;
            Game.Start = true;
            Game.stepCount = 0;
            Game.finalboss = new Final_Boss(Game.FinalBoss_Texture, Game.player);
            Game.bossFlag = false;
            Game.boss.position[0] = 0;
            Game.boss.position[1] = 0;
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
            Game._spriteBatch.Begin();
            Game._spriteBatch.Draw(
                Game.finalboss.CharacterTexture,
                new Vector2(Game._graphics.PreferredBackBufferWidth / 12 * Game.finalboss.position[0],
                Game._graphics.PreferredBackBufferHeight / 12 * Game.finalboss.position[3]),
                null,
                Color.White,
                0f,
                new Vector2(Game._graphics.PreferredBackBufferWidth / 12, Game._graphics.PreferredBackBufferHeight / 12),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            Game._spriteBatch.End();
        }

    }
}
