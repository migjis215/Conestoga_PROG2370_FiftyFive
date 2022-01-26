using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class OctoNaughtyCube : Block
    {
        private Rectangle rectangle;
        private Vector2 speed;
        private One one;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }

        public OctoNaughtyCube(Game game, 
                               Vector2 position,
                               One one) : base(game, position)
        {
            rectangle = new Rectangle(200, 0, Shared.GAP * 2, Shared.GAP * 2);
            speed = new Vector2(1, 1);
            this.one = one;
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
                Vector2 target = one.Position;
                float xDiff = (target.X - Shared.GAP / 2) - Position.X;
                float yDiff = (target.Y - Shared.GAP / 2) - Position.Y;

                Vector2 tempPosition = Position;

                tempPosition.X += xDiff * speed.X * 0.01f;
                tempPosition.Y += yDiff * speed.Y * 0.01f;

                Position = tempPosition;
            }
            else
            {
                Position = new Vector2(0 - Shared.GAP / 2, 0 - Shared.GAP);
            }

            base.Update(gameTime);
        }
    }
}
