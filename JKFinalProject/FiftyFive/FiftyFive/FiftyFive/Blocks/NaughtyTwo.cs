using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class NaughtyTwo : Block
    {
        private Rectangle rectangle;
        private Vector2 speed;

        //public Vector2 Speed { get => speed; set => speed = value; }
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }

        public NaughtyTwo(Game game, 
                          Vector2 position) : base(game, position)
        {
            rectangle = new Rectangle(160, 20, Shared.GAP * 2, Shared.GAP);
            speed = new Vector2(1, 1);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, rectangle, Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Visible)
            {
                Position += speed;
                if (Position.Y < 0)
                {
                    speed.Y = -speed.Y;
                }
                if (Position.X > Shared.stage.X - rectangle.Width)
                {
                    speed.X = -speed.X;
                }
                if (Position.X < 0)
                {
                    speed.X = -speed.X;
                }
                if (Position.Y > Shared.stage.Y - rectangle.Height)
                {
                    speed.Y = -speed.Y;
                }
            }
            else if(!this.Visible)
            {
                Position = new Vector2(0, 0);
            }
            
            base.Update(gameTime);
        }
    }
}
