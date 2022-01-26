using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoString
{
    public class SimpleString : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        private SpriteFont font;
        protected String message;
        protected Vector2 position;
        protected Color color;

        public SpriteFont Font { get => font; set => font = value; }
        public string Message { get => message; set => message = value; }
        public Vector2 Position { get => position; set => position = value; }

        public SimpleString(Game game,
            SpriteBatch spriteBatch, 
            SpriteFont font,
            String message,
            Vector2 position,
            Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.message = message;
            this.position = position;
            this.color = color;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, position, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}
