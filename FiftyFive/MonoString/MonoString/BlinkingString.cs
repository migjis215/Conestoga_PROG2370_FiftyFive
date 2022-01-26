using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MonoString
{
    class BlinkingString : SimpleString
    {
        private double delay;
        private double delayCounter;
        private bool flag = true;

        public BlinkingString(
            Game game, 
            SpriteBatch spriteBatch, 
            SpriteFont font, 
            string message, 
            Vector2 position, 
            Color color,
            double delay) : base(game, spriteBatch, font, message, position, color)
        {
            this.delay = delay;
        }

        public override void Draw(GameTime gameTime)
        {
            //if (flag)
            //{
            //    spriteBatch.Begin();
            //    spriteBatch.DrawString(font, message, position, color);
            //    spriteBatch.End();
            //}

            if (flag)
            {
                base.Draw(gameTime); 
            }
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter += gameTime.ElapsedGameTime.TotalSeconds;
            if (delayCounter >= delay)
            {
                flag = !flag;
                delayCounter = 0;
            }

            base.Update(gameTime);
        }
    }
}
