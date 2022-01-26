using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiftyFive
{
    public class HeadBlock : PlayerBlock
    {
        private SoundEffect turnSound;
        private SoundEffect gameoverSound;
        private float rotation;
        private float scale = 1;
        private Vector2 origin;

        private KeyboardState oldState;
        private string key = "";
        private bool isOver = false;

        private List<string> keyOrder;

        public const int SPEED = 4;
        private const float ROTATION_FACTOR = 0.2f;
        private const float SCALE_FACTOR = 0.05f;
        private const float MIN_SCALE = 0;

        public float Rotation { get => rotation; set => rotation = value; }
        public float Scale { get => scale; set => scale = value; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public string Key { get => key; set => key = value; }
        public bool IsOver { get => isOver; set => isOver = value; }
        public List<string> KeyOrder { get => keyOrder; set => keyOrder = value; }

        public HeadBlock(Game game,
                     Vector2 position,
                     SoundEffect turnSound,
                     SoundEffect gameoverSound,
                     string key) : base(game, position)
        {
            this.turnSound = turnSound;
            this.gameoverSound = gameoverSound;
            origin = Vector2.Zero;

            keyOrder = new List<string>();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, Rectangle, Color.White, rotation, origin, scale, SpriteEffects.None, 0.1f);
            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            string previousKey = Key;
            KeyboardState currentState = Keyboard.GetState();
            Vector2 tempPosition;
            LastPosition = Position;

            if (Position.X == 0 ||
                Position.X == Shared.stage.X - Shared.GAP ||
                Position.Y == 0 ||
                Position.Y == Shared.stage.Y - Shared.GAP ||
                isCollided() || isOver)
            {
                Key = "";

                List<PlayerBlock> enabledBlocks = getEnabledBlocks();
                foreach (PlayerBlock block in enabledBlocks)
                {
                    if (block is TailBlock tail)
                    {
                        tail.Visible = false;
                    }
                }

                if ((int)Position.X % Shared.GAP == 0 && (int)Position.Y % Shared.GAP == 0)
                {
                    tempPosition = new Vector2(Position.X + Shared.GAP / 2,
                                               Position.Y + Shared.GAP / 2);
                    Position = tempPosition;
                    origin = new Vector2(Shared.GAP / 2, Shared.GAP / 2);
                    isOver = true;
                }

                if (rotation == 0)
                {
                    gameoverSound.Play();
                }
                rotation += ROTATION_FACTOR;
                scale -= SCALE_FACTOR;
                if (scale < MIN_SCALE)
                {
                    scale = MIN_SCALE;
                    this.Visible = false;
                }
            }
            else
            {
                if (currentState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up) && previousKey != "down")
                {
                    keyOrder.Insert(keyOrder.Count, "up");
                }
                if (currentState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right) && previousKey != "left")
                {
                    keyOrder.Insert(keyOrder.Count, "right");
                }
                if (currentState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down) && previousKey != "up")
                {
                    keyOrder.Insert(keyOrder.Count, "down");
                }
                if (currentState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left) && previousKey != "right")
                {
                    keyOrder.Insert(keyOrder.Count, "left");
                }

                oldState = currentState;
            }

            if ((int)Position.X % Shared.GAP == 0 && (int)Position.Y % Shared.GAP == 0)
            {
                if (keyOrder.Count != 0)
                {
                    Key = keyOrder[0];
                    keyOrder.RemoveAt(0);
                }

                if (previousKey != Key)
                {
                    turnSound.Play();
                }
            }

            tempPosition = Position;
            switch (Key)
            {
                case "up":
                    tempPosition.Y -= SPEED;
                    break;
                case "right":
                    tempPosition.X += SPEED;
                    break;
                case "down":
                    tempPosition.Y += SPEED;
                    break;
                case "left":
                    tempPosition.X -= SPEED;
                    break;
                default:
                    break;
            }
            Position = tempPosition;

            base.Update(gameTime);
        }

        private bool isCollided()
        {
            List<PlayerBlock> enabledBlocks = getEnabledBlocks();
            
            if (enabledBlocks.Count > 1 && enabledBlocks[0].Position != enabledBlocks[1].Position)
            {
                for (int i = 4; i < enabledBlocks.Count; i++)
                {
                    if (this.Position == enabledBlocks[i].Position)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
