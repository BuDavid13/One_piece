using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monoGame_Project
{
    internal class credits : GameScreen
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        SpriteFont font;
        string creditsText = "                                    Fin.\n\n\n                                Made by:\n\n\nMozgas meg palyageneralas: David\n\nHang: Patrik\n\nGoblinok (Patrikok) es karakter es harc: Istvan";
        Vector2 textPos;
        float speed;
        private new Game1 Game => (Game1)base.Game;
        public credits(Game1 Game) : base(Game)
        {
        }
        public override void LoadContent()
        {
            font = Content.Load<SpriteFont>("File");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics = Game._graphics;
            Game.Start = false;
            textPos = new Vector2(_graphics.PreferredBackBufferWidth / 2 - Convert.ToInt32(font.MeasureString(creditsText).X) / 2, 1000f);
            speed = 100f;
        }
        public override void Update(GameTime gameTime)
        {
            textPos.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(textPos.Y < -500f)
            {
                Game.Exit();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            Game._graphics.GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(
            font,
            creditsText,
            textPos,
            Color.Black);
            _spriteBatch.End();
        }
    }
}
