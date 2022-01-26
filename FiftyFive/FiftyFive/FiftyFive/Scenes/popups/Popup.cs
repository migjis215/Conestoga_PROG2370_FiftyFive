using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class Popup : GameScene
    {
        private Game1 game1;
        private SpriteBatch spriteBatch;
        private Texture2D popUpImage;
        private SpriteFont font;
        private SoundEffect selectSound;
        private GameScene scene;

        public Texture2D PopUpImage { get => popUpImage; set => popUpImage = value; }
        public SpriteFont Font { get => font; set => font = value; }
        public SoundEffect SelectSound { get => selectSound; set => selectSound = value; }

        public Popup(Game game,
                     GameScene scene) : base(game)
        {
            game1 = (Game1)game;
            spriteBatch = game1._spriteBatch;
            popUpImage = game1.Content.Load<Texture2D>("images/background/popup");
            font = game1.Content.Load<SpriteFont>("fonts/font");
            selectSound = game1.Content.Load<SoundEffect>("sounds/select");
            this.scene = scene;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(popUpImage, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
