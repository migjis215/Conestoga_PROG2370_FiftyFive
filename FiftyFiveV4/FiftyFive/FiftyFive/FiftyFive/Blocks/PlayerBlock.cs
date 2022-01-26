using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class PlayerBlock : Block
    {
        public const int SEVEN = 7;
        public const int NINE = 9;
        public const int TEN = 10;
        public const int TWENTY = 20;
        public const int THIRTY = 30;
        public const int FOURTY = 40;
        public const int FIFTY = 50;
        public const int FIFTY_FIVE = 55;

        private Rectangle rectangle;
        private Vector2 lastPosition;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
        public Vector2 LastPosition { get => lastPosition; set => lastPosition = value; }

        public PlayerBlock(Game game,
                           SpriteBatch spriteBatch,
                           Vector2 position) : base(game, spriteBatch, position)
        {
            rectangle = new Rectangle(0, 0, Shared.GAP, Shared.GAP);
            lastPosition = this.Position;
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, Rectangle, Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public static List<PlayerBlock> getEnabledBlocks()
        {
            List<PlayerBlock> enabledBlocks = new List<PlayerBlock>();

            foreach (PlayerBlock block in blocks)
            {
                if (block.Enabled)
                {
                    enabledBlocks.Insert(enabledBlocks.Count, block);
                }
            }

            return enabledBlocks;
        }

        public static int getNextBlockIndex()
        {
            return getEnabledBlocks().Count;
        }

        public static void updateBlockTexture()
        {
            List<PlayerBlock> enabledBlocks = getEnabledBlocks();

            if (enabledBlocks.Count <= TEN)
            {
                updateBlockTextureUntilTen(enabledBlocks.Count, enabledBlocks);
            }
            else
            {
                updateBlockTextureUntilTen(enabledBlocks.Count % TEN, enabledBlocks);
                updateBlockTextureOverTen(enabledBlocks.Count % TEN, enabledBlocks);
            }
        }

        private static void updateBlockTextureUntilTen(int count, List<PlayerBlock> enabledBlocks)
        {
            if (count == SEVEN)
            {
                for (int i = 0; i < SEVEN; i++)
                {
                    setRectangleForSeven(enabledBlocks[i], SEVEN - 1 - i);
                }
            }
            else if (count == NINE)
            {
                for (int i = 0; i < NINE; i++)
                {
                    int j = i / 3;
                    setRectangleForNine(enabledBlocks[i], j);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    setRectangle(enabledBlocks[i], count);
                }
            }
        }

        private static void updateBlockTextureOverTen(int startIndex, List<PlayerBlock> enabledBlocks)
        {
            for (int i = startIndex; i < enabledBlocks.Count; i++)
            {
                if (enabledBlocks.Count < TWENTY)
                {
                    enabledBlocks[i].Rectangle = new Rectangle(180, 0, Shared.GAP, Shared.GAP);
                }
                else if (enabledBlocks.Count < THIRTY)
                {
                    enabledBlocks[i].Rectangle = new Rectangle(60, 20, Shared.GAP, Shared.GAP);
                }
                else if (enabledBlocks.Count < FOURTY)
                {
                    enabledBlocks[i].Rectangle = new Rectangle(80, 20, Shared.GAP, Shared.GAP);
                }
                else if (enabledBlocks.Count < FIFTY)
                {
                    enabledBlocks[i].Rectangle = new Rectangle(100, 20, Shared.GAP, Shared.GAP);
                }
                else
                {
                    enabledBlocks[i].Rectangle = new Rectangle(120, 20, Shared.GAP, Shared.GAP);
                }
            }
        }

        private static void setRectangle(PlayerBlock block, int index)
        {
            block.Rectangle = Shared.numbers[index - 1];
        }

        private static void setRectangleForSeven(Block block, int index)
        {
            if (block is TailBlock tailBlock)
            {
                tailBlock.Rectangle = Shared.numbers[index];
            }
            if (block is HeadBlock headBlock)
            {
                headBlock.Rectangle = Shared.numbers[index];
            }
        }

        private static void setRectangleForNine(Block block, int index)
        {
            if (block is TailBlock tailBlock)
            {
                tailBlock.Rectangle = Shared.nines[index];
            }
            if (block is HeadBlock headBlock)
            {
                headBlock.Rectangle = Shared.nines[index];
            }
        }
    }
}
