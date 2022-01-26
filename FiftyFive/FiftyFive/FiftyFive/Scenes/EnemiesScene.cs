using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class EnemiesScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D enemiesTexture;

        public EnemiesScene(Game game) : base(game)
        {
            Game1 game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            enemiesTexture = game1.Content.Load<Texture2D>("images/background/enemies");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(enemiesTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
