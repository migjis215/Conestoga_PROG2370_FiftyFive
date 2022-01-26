using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoString;

namespace FiftyFive
{
    public class Clear : Popup
    {
        private Game1 game1;
        private SpriteBatch spriteBatch;
        private EndingScene endingScene;
        private SimpleString simpleString;
        private string message = "CONGRATULATION\n\nPress enter";

        public Clear(Game game, GameScene scene) : base(game, scene)
        {
            game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            endingScene = (EndingScene)scene;
            Vector2 messagePosition = new Vector2(
                (Shared.screen.X - Font.MeasureString(message).X) / 2,
                (Shared.screen.Y - Font.MeasureString(message).Y) / 2);

            simpleString = new SimpleString(game1, spriteBatch, Font, message, messagePosition, Color.Black);

            this.Components.Add(simpleString);
            hide();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                this.hide();
                game1.hideAllScenes();
                endingScene.show();
                MediaPlayer.Play(game1.Content.Load<Song>("sounds/countdown"));
                MediaPlayer.IsRepeating = false;
            }

            base.Update(gameTime);
        }
    }
}
