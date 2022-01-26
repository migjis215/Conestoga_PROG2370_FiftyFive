using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiftyFive
{
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Vector2 position;
        private string[] menuItems;
        private Color[] fontColors;
        private SoundEffect selectSound;

        private KeyboardState oldState;

        public int SelectedIndex { get; set; }

        public MenuComponent(Game game,
                             SpriteBatch spriteBatch,
                             SpriteFont font,
                             Vector2 position,
                             string[] menuItems,
                             Color[] fontColors,
                             SoundEffect selectSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.menuItems = menuItems;
            this.fontColors = fontColors;
            this.selectSound = selectSound;
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPosition = position;
            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Length; i++)
            {
                spriteBatch.DrawString(font, menuItems[i], tempPosition, Color.LightGray);
                if (SelectedIndex == i)
                {
                    Vector2 hilightPosition = new Vector2(tempPosition.X - 2, tempPosition.Y - 2);

                    spriteBatch.DrawString(font, menuItems[i], hilightPosition, fontColors[i]);
                }
                tempPosition.Y += font.LineSpacing;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex == menuItems.Length)
                {
                    SelectedIndex = 0;
                }
                selectSound.Play();
            }
            if (currentState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Length - 1;
                }
                selectSound.Play();
            }

            oldState = currentState;

            base.Update(gameTime);
        }
    }
}
