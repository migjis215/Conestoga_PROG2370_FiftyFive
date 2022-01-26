using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace FiftyFive
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private StartScene startScene;
        private PlayScene playScene;
        private HelpScene helpScene;
        private EnemiesScene enemiesScene;
        private AboutScene aboutScene;
        private EndingScene endingScene;

        private KeyboardState oldState;

        public const int PLAY = 0;
        public const int HELP = 1;
        public const int ENEMIES = 2;
        public const int ABOUT = 3;
        public const int QUIT = 4;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.screen = new Vector2(_graphics.PreferredBackBufferWidth,
                                       _graphics.PreferredBackBufferHeight);

            Shared.stage = new Vector2(Shared.GAP * 2 + Shared.STAGE_WITH,
                                       Shared.GAP * 2 + Shared.STAGE_HIEGHT);

            Shared.positions = new List<Vector2>();
            for (int i = Shared.GAP; i < Shared.STAGE_WITH; i += Shared.GAP)
            {
                for (int j = Shared.GAP; j < Shared.STAGE_HIEGHT; j += Shared.GAP)
                {
                    Vector2 position = new Vector2(i, j);
                    Shared.positions.Add(position);
                }
            }

            Shared.numbers = new List<Rectangle>();
            for (int i = 0; i < PlayerBlock.TEN; i++)
            {
                Rectangle rectangle = new Rectangle(i * Shared.GAP, 0, Shared.GAP, Shared.GAP);
                Shared.numbers.Insert(Shared.numbers.Count, rectangle);
            }

            Shared.nines = new List<Rectangle>();
            for (int i = 0; i <= 2; i++)
            {
                Rectangle rectangle = new Rectangle(i * Shared.GAP, Shared.GAP, Shared.GAP, Shared.GAP);
                Shared.nines.Insert(Shared.nines.Count, rectangle);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            endingScene = new EndingScene(this, startScene);
            this.Components.Add(endingScene);

            playScene = new PlayScene(this, startScene, endingScene);
            this.Components.Add(playScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            enemiesScene = new EnemiesScene(this);
            this.Components.Add(enemiesScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            startScene.show();
        }

        public void hideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene scene)
                {
                    scene.hide();
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            KeyboardState currentState = Keyboard.GetState();

            if (startScene.Enabled)
            {
                int selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == PLAY && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    MediaPlayer.Stop();
                    startScene.hide();
                    playScene.show();
                }
                else if (selectedIndex == HELP && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if (selectedIndex == ENEMIES && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    enemiesScene.show();
                }
                else if (selectedIndex == ABOUT && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    aboutScene.show();
                }
                else if (selectedIndex == QUIT && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    Exit();
                }
            }

            oldState = currentState;

            if (helpScene.Enabled || enemiesScene.Enabled ||aboutScene.Enabled)
            {
                if (currentState.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                    startScene.Menu.Enabled = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
