using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace monoGame_Project
{

    internal class menu : GameScreen
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private new Game1 Game => (Game1)base.Game;
        Texture2D MainTitle;
        public menu(Game1 Game) : base(Game)
        {
            
        }
        public override void LoadContent()
        {
            _graphics = Game._graphics;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MainTitle = Content.Load<Texture2D>("main_title");
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game.LoadLevel1();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
            MainTitle,
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
        }

    }
}