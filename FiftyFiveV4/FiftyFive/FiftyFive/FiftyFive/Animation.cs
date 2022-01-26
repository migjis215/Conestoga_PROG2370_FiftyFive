using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class Animation : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;

        private int delay;
        private int delayCounter;

        private const int ROW = 5;
        private const int COLUMN = 5;

        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Texture { get => texture; set => texture = value; }
        public Vector2 Dimension { get => dimension; set => dimension = value; }

        public Animation(Game game, 
                         SpriteBatch spriteBatch,
                         Texture2D texture,
                         Vector2 position,
                         int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.delay = delay;

            this.dimension = new Vector2(texture.Width / COLUMN,
                                         texture.Height / ROW);
            hide();
            createFrames();
        }

        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle ractangle = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(ractangle);
                }
            }
        }

        public void restart()
        {
            frameIndex = -1;
            delayCounter = 0;
            this.Enabled = true;
            this.Visible = true;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (frameIndex >= 0)
            {
                spriteBatch.Draw(texture, position, frames[frameIndex], Color.White); 
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROW * COLUMN - 1)
                {
                    frameIndex = -1;
                    hide();
                }

                delayCounter = 0;
            }

            base.Update(gameTime);
        }
    }
}
