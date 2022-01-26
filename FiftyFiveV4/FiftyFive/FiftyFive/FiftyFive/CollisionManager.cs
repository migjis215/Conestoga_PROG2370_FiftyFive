using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class CollisionManager : DrawableGameComponent
    {
        private HeadBlock headBlock;
        private One one;
        private Game1 game1;
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        private Animation wave;

        public Animation Wave { get => wave; set => wave = value; }

        public CollisionManager(Game game,
                                HeadBlock headBlock,
                                One one) : base(game)
        {
            this.game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            this.headBlock = headBlock;
            this.one = one;

            texture = game1.Content.Load<Texture2D>("images/shockwave");
            wave = new Animation(game1, spriteBatch, texture, Vector2.Zero, 1);
            game1.Components.Add(wave);
        }

        public override void Update(GameTime gameTime)
        {
            List<PlayerBlock> enabledBlocks = PlayerBlock.getEnabledBlocks();

            if (headBlock.Enabled && headBlock.Position == one.Position)
            {
                Vector2 tempPosition = new Vector2();
                tempPosition.X = one.Position.X - (Wave.Dimension.X - Shared.GAP) / 2;
                tempPosition.Y = one.Position.Y - (Wave.Dimension.Y - Shared.GAP) / 2;

                Wave = new Animation(game1, spriteBatch, texture, tempPosition, 1);
                game1.Components.Add(Wave);

                Wave.Position = tempPosition;
                Wave.DrawOrder = 1;
                Wave.restart();

                PlayerBlock lastBlock = enabledBlocks[enabledBlocks.Count - 1];
                TailBlock newBlock = (TailBlock)Block.blocks[enabledBlocks.Count];

                newBlock.Position = lastBlock.Position;
                newBlock.Route = new Vector2[]
                {
                    new Vector2(newBlock.Position.X, newBlock.Position.Y),
                    new Vector2(newBlock.Position.X, newBlock.Position.Y),
                    new Vector2(newBlock.Position.X, newBlock.Position.Y),
                    new Vector2(newBlock.Position.X, newBlock.Position.Y),
                    new Vector2(newBlock.Position.X, newBlock.Position.Y)
                };
                newBlock.Enabled = true;
                newBlock.Visible = true;
                PlayerBlock.updateBlockTexture();

                one.AddSound.Play();
                one.Position = Block.getRandomPosition();
            }

            base.Update(gameTime);
        }
    }
}
