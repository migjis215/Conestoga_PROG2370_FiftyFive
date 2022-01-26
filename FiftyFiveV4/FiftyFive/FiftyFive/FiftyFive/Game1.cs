using FileManager;
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
        private ScoreScene scoreScene;
        private HelpScene helpScene;
        private AboutScene aboutScene;

        private NewAndLoad newAndLoad;

        private KeyboardState oldState;

        public const int FIRST = 0;
        public const int SECOND = 1;
        public const int THIRD = 2;
        public const int FOURTH = 3;
        public const int FIFTH = 4;

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

            //Score.scores = new List<Score>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            scoreScene = new ScoreScene(this);
            this.Components.Add(scoreScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            startScene.show();

            newAndLoad = new NewAndLoad(this, startScene);
            this.Components.Add(newAndLoad);
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
            if (newAndLoad.Enabled)
            {
                int selectedPopUpIndex = newAndLoad.PopUpMenu.SelectedIndex;
                if (selectedPopUpIndex == FIRST && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    MediaPlayer.Stop();
                    playScene = new PlayScene(this, startScene);
                    this.Components.Add(playScene);
                    playScene.show();
                    startScene.hide();
                    newAndLoad.hide();
                }
                else if (selectedPopUpIndex == SECOND && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    // Loading
                    startScene.hide();
                }
            }

            if (startScene.Enabled)
            {
                int selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == FIRST && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startScene.Menu.Enabled = false;
                    startScene.Menu.SelectedIndex = -1;
                    newAndLoad.show();
                }
                else if (selectedIndex == SECOND && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    scoreScene.show();
                }
                else if (selectedIndex == THIRD && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if (selectedIndex == FOURTH && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startScene.hide();
                    aboutScene.show();
                }
                else if (selectedIndex == FIFTH && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    Exit();
                }
            }

            oldState = currentState;

            if (scoreScene.Enabled || helpScene.Enabled || aboutScene.Enabled)
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
