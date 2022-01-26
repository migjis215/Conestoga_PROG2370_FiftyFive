using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D aboutTexture;

        public AboutScene(Game game) : base(game)
        {
            Game1 game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            aboutTexture = game1.Content.Load<Texture2D>("images/background/about");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(aboutTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
