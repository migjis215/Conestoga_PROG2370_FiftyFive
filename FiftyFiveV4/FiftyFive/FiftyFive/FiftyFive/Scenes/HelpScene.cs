using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D helpTexture;

        public HelpScene(Game game) : base(game)
        {
            Game1 game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            helpTexture = game1.Content.Load<Texture2D>("images/background/help");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(helpTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
