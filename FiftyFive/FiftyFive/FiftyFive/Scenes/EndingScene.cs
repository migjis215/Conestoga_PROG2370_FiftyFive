using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FiftyFive
{
    public class EndingScene : GameScene
    {
        private Game1 game1;
        private StartScene startScene;
        private SpriteBatch spriteBatch;
        private Texture2D endingTexture;
        private Texture2D fiftyFiveTexture;
        private Vector2 fiftyFivePosition;
        private Vector2 dimension;
        private List<Rectangle> rectangles;
        private Rectangle rectangle;

        private double delay;
        private double delayCounter;
        private bool flag = true;
        private int index;

        private const int ROW = 4;
        private const int COLUMN = 3;

        public EndingScene(Game game,
                           StartScene startScene) : base(game)
        {
            game1 = (Game1)game;
            spriteBatch = game1._spriteBatch;
            this.startScene = startScene;
            endingTexture = game1.Content.Load<Texture2D>("images/ending");
            fiftyFiveTexture = game1.Content.Load<Texture2D>("images/endingFiftyFive");
            fiftyFivePosition = new Vector2(Shared.screen.X / 2 - fiftyFiveTexture.Width + Shared.GAP,
                                            Shared.screen.Y - fiftyFiveTexture.Height);
            delay = 0.7;
            dimension = new Vector2(endingTexture.Width / COLUMN, endingTexture.Height / ROW);

            rectangles = new List<Rectangle>();
            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    int x = i * (int)dimension.X;
                    int y = j * (int)dimension.Y;
                    Rectangle rectangle = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    rectangles.Add(rectangle);
                }
            }
            rectangle = rectangles[index];

            hide();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (flag)
            {
                spriteBatch.Draw(endingTexture, Vector2.Zero, rectangle, Color.White);
            }
            else if (!flag)
            {
                spriteBatch.Draw(endingTexture, Vector2.Zero, rectangles[rectangles.Count - 1], Color.White);
                spriteBatch.Draw(fiftyFiveTexture, fiftyFivePosition, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (flag)
            {
                delayCounter += gameTime.ElapsedGameTime.TotalSeconds;
                if (delayCounter >= delay)
                {
                    index++;
                    rectangle = rectangles[index];
                    delayCounter = 0;
                    if (index == rectangles.Count - 2)
                    {
                        delay = 3;
                    }
                    if (index == rectangles.Count - 1)
                    {
                        flag = !flag;
                        MediaPlayer.Play(game1.Content.Load<Song>("sounds/ending"));
                    }
                }
            }
            else if (!flag)
            {
                fiftyFivePosition.Y -= 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                flag = true;
                delay = 0.7;
                index = 0;
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
