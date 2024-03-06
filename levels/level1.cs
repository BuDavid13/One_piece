﻿using Microsoft.Xna.Framework;
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
    internal class level1 : GameScreen
    {

        private new Game1 Game => (Game1)base.Game;
        public level1(Game1 Game) : base(Game)
        {
        }
        public override void LoadContent()
        {
            Game.player.Key = false;
            Game.generateTerrain = true;
            Game.playerXY[0] = 2;
            Game.playerXY[1] = 2;
            Game.conditionsMet = false;
            Game.keyFlag = false;
            Game.Start = true;
            Game.bossFlag = false;
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
                Game.LoadLevel2();
            }
        }
        public override void Draw(GameTime gameTime)
        {

        }
    }
}
