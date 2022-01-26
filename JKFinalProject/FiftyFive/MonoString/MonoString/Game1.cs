using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoString
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SimpleString s1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteFont font = this.Content.Load<SpriteFont>("fonts/SimpleString");
            Vector2 pos1 = new Vector2(100, 200);
            s1 = new SimpleString(this, _spriteBatch, font, "Hello everybody", pos1, Color.Blue);
            this.Components.Add(s1);
            SimpleString s2 = new SimpleString(this, _spriteBatch, font, "Hello everybody", new Vector2(150, 250), Color.Green);
            this.Components.Add(s2);

            BlinkingString b1 = new BlinkingString(this, _spriteBatch, font, "I am blinking", new Vector2(200, 300), Color.Red, 1);
            this.Components.Add(b1);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //s1.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
