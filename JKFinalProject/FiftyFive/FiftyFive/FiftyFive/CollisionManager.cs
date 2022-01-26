using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class CollisionManager : DrawableGameComponent
    {
        private HeadBlock headBlock;
        private One one;
        private NaughtyTwo naughtyTwo;
        private OctoNaughtyCube octoNaughtyCube;
        private Game1 game1;
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        private Animation wave;

        public Animation Wave { get => wave; set => wave = value; }

        public CollisionManager(Game game,
                                HeadBlock headBlock,
                                One one,
                                NaughtyTwo naughtyTwo,
                                OctoNaughtyCube octoNaughtyCube) : base(game)
        {
            this.game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            this.headBlock = headBlock;
            this.one = one;
            this.naughtyTwo = naughtyTwo;
            this.octoNaughtyCube = octoNaughtyCube;

            texture = game1.Content.Load<Texture2D>("images/shockwave");
            wave = new Animation(game1, spriteBatch, texture, Vector2.Zero, 1);
            game1.Components.Add(wave);
        }

        public override void Update(GameTime gameTime)
        {
            List<PlayerBlock> enabledBlocks = PlayerBlock.getEnabledBlocks();
            Rectangle headBlockRectangle = headBlock.getBounds(Shared.GAP, Shared.GAP);
            Rectangle naughtyTwoRectangle = naughtyTwo.getBounds(naughtyTwo.Rectangle.Width, naughtyTwo.Rectangle.Height);
            Rectangle octoNaughtyCubeRectangle = octoNaughtyCube.getBounds(octoNaughtyCube.Rectangle.Width, octoNaughtyCube.Rectangle.Height);
            Rectangle oneRectangle = one.getBounds(Shared.GAP, Shared.GAP);
            if (naughtyTwoRectangle.Intersects(headBlockRectangle) ||
                octoNaughtyCubeRectangle.Contains(oneRectangle))
            {
                headBlock.IsOver = true;
            }

            if (headBlock.Enabled && headBlock.Position == one.Position)
            {
                if (one.Position.X < Shared.stage.X / 2 &&
                    one.Position.Y < Shared.stage.Y / 2)
                {
                    octoNaughtyCube.Position = new Vector2(-Shared.GAP, -Shared.GAP);
                }
                else if (one.Position.X < Shared.stage.X / 2 &&
                    one.Position.Y >= Shared.stage.Y / 2)
                {
                    octoNaughtyCube.Position = new Vector2(-Shared.GAP, Shared.stage.Y - Shared.GAP);
                }
                else if (one.Position.X >= Shared.stage.X / 2 &&
                    one.Position.Y < Shared.stage.Y / 2)
                {
                    octoNaughtyCube.Position = new Vector2(Shared.stage.X - Shared.GAP, -Shared.GAP);
                }
                else 
                {
                    octoNaughtyCube.Position = new Vector2(Shared.stage.X - Shared.GAP, Shared.stage.Y - Shared.GAP);
                }

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
