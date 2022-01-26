using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoString;

namespace FiftyFive
{
    public class GameOver : Popup
    {
        private MenuComponent popUpMenu;
        private SpriteBatch spriteBatch;
        private StartScene startScene;
        private Game1 game1;
        private SimpleString simpleString;

        private string title = "GAME OVER";

        string[] menuItems = {
            "Try Again This Stage",
            "Return to Menu"
        };

        Color[] fontColors = {
            Color.Black,
            Color.Black
        };

        public MenuComponent PopUpMenu { get => popUpMenu; set => popUpMenu = value; }

        public GameOver(Game game, 
                        GameScene scene) : base(game, scene)
        {
            game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            startScene = (StartScene)scene;

            Vector2 menuPosition = new Vector2(
                (Shared.screen.X - Font.MeasureString(menuItems[0] + "\n" + menuItems[1]).X) / 2,
                (Shared.screen.Y - Font.MeasureString(menuItems[0] + "\n" + menuItems[1]).Y) / 2);
            popUpMenu = new MenuComponent(game1, spriteBatch, Font, menuPosition, menuItems, fontColors, SelectSound);

            simpleString = new SimpleString(game1, spriteBatch, Font, title,
                new Vector2(
                (Shared.screen.X - Font.MeasureString(title).X) / 2,
                Font.LineSpacing * HeadBlock.SPEED + Shared.GAP), Color.Black);

            this.Components.Add(popUpMenu);
            this.Components.Add(simpleString);
            hide();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.hide();
                game1.hideAllScenes();
                startScene.show();
                startScene.Menu.Enabled = true;
                startScene.Menu.SelectedIndex = 0;
                MediaPlayer.Play(game1.Content.Load<Song>("sounds/title"));
            }

            base.Update(gameTime);
        }
    }
}
