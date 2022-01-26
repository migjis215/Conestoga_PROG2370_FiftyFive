using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class One : Block
    {
        private Rectangle rectangle;
        SoundEffect addSound;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public SoundEffect AddSound { get => addSound; set => addSound = value; }

        public One(Game game,
                   SpriteBatch spriteBatch,
                   Vector2 position,
                   SoundEffect addSound) : base(game, spriteBatch, position)
        {
            rectangle = new Rectangle(140, 20, Shared.GAP, Shared.GAP);
            this.addSound = addSound;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, rectangle, Color.White);
            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
