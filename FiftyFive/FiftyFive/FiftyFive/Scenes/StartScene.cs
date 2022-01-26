using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace FiftyFive
{
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D fiftyFive;
        private Vector2 fiftyFivePosition;
        private Vector2 speed;

        string[] menuItems = { 
            "PLAY", 
            "HELP", 
            "ENEMIES",
            "ABOUT", 
            "QUIT"
        };

        Color[] fontColors = {
            Color.OrangeRed,
            Color.Orange,
            Color.Gold,
            Color.LimeGreen,
            Color.SkyBlue
        };

        public MenuComponent Menu { get => menu; set => menu = value; }

        public StartScene(Game game) : base(game)
        {
            Game1 game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;

            SoundEffect selectSound = game1.Content.Load<SoundEffect>("sounds/select");
            Song titleSong = game1.Content.Load<Song>("sounds/title");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(titleSong);

            fiftyFive = game1.Content.Load<Texture2D>("images/background/fiftyFive");
            fiftyFivePosition = new Vector2(-fiftyFive.Width, Shared.screen.Y - fiftyFive.Height);
            speed = new Vector2(2, 0);

            background = game1.Content.Load<Texture2D>("images/background/start");

            SpriteFont font = game1.Content.Load<SpriteFont>("fonts/font");
            Vector2 menuPosition = new Vector2(
                Shared.screen.X - font.MeasureString(getLongestItem(menuItems).PadRight(Shared.GAP, ' ')).X,
                font.LineSpacing / 2
            );
            menu = new MenuComponent(game1, spriteBatch, font, menuPosition, menuItems, fontColors, selectSound);
            this.Components.Add(menu);
        }

        private string getLongestItem(string[] menuItems)
        {
            string longestItem = "";
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (menuItems[i].Length > longestItem.Length)
                {
                    longestItem = menuItems[i];
                }
            }

            return longestItem;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(fiftyFive, fiftyFivePosition, Color.White);
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            fiftyFivePosition += speed;
            if (fiftyFivePosition.X > Shared.screen.X)
            {
                fiftyFivePosition.X = -fiftyFive.Width;
            }

            base.Update(gameTime);
        }
    }
}
