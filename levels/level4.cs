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
    internal class level4 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public level4(Game1 Game) : base(Game)
        {
        }
        public override void LoadContent()
        {
            Game.generateTerrain = true;
            Game.playerXY[0] = 2;
            Game.playerXY[1] = 2;
            Game.conditionsMet = false;
            Game.player.Key = false;
            Game.keyFlag = false;
            Game.bossFlag = false;
            Game.player.Regeneration();
            Game.player.shield = Game.player.maxShield;
            Game.level++;
            Game.stepCount = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.player.Key && Game.bossFlag)
            {
                Game.conditionsMet = true;
            }
            if (Game.playerXY[0] == Game.doorXY[0] && Game.playerXY[1] == Game.doorXY[1] && Game.conditionsMet)
            {
                Game.LoadLevel5();
            }
        }
        public override void Draw(GameTime gameTime)
        {

        }
    }
}
