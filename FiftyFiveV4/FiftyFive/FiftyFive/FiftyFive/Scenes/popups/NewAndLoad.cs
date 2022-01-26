using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiftyFive
{

    public class NewAndLoad : Popup
    {
        private MenuComponent popUpMenu;
        private SpriteBatch spriteBatch;
        private StartScene startScene;
        private Game1 game1;

        string[] menuItems = {
            "NEW GAME",
            "LOAD GAME"
        };
        Color[] fontColors = {
            Color.Black,
            Color.Black
        };

        public MenuComponent PopUpMenu { get => popUpMenu; set => popUpMenu = value; }

        public NewAndLoad(Game game,
                          GameScene scene) : base(game, scene)
        {
            game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            startScene = (StartScene)scene;

            Vector2 menuPosition = new Vector2(
                (Shared.screen.X - Font.MeasureString(menuItems[0] + "\n" + menuItems[1]).X) / 2,
                (Shared.screen.Y - Font.MeasureString(menuItems[0] + "\n" + menuItems[1]).Y) / 2);
            popUpMenu = new MenuComponent(game1, spriteBatch, Font, menuPosition, menuItems, fontColors, SelectSound);

            this.Components.Add(popUpMenu);
            hide();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                startScene.Menu.Enabled = true;
                startScene.Menu.SelectedIndex = 0;
                this.hide();
            }

            base.Update(gameTime);
        }
    }
}
