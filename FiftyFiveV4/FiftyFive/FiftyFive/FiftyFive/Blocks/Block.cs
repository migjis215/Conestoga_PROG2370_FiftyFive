using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class Block : DrawableGameComponent
    {
        public static List<Block> blocks;

        private Game1 game1;
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;

        public Block(Game game,
                     SpriteBatch spriteBatch,
                     Vector2 position) : base(game)
        {
            this.game1 = (Game1)game;
            this.spriteBatch = spriteBatch;
            texture = game1.Content.Load<Texture2D>("images/numberblocks");
            this.position = position;
        }

        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
        public Texture2D Texture { get => texture; set => texture = value; }
        public Vector2 Position { get => position; set => position = value; }

        public static Vector2 getRandomPosition()
        {
            List<Vector2> emptyPositions = new List<Vector2>();
            List<PlayerBlock> enabledBlocks = PlayerBlock.getEnabledBlocks();
            for (int i = 0; i < Shared.positions.Count; i++)
            {
                for (int j = 0; j < enabledBlocks.Count; j++)
                {
                    if (Shared.positions[i].X < enabledBlocks[j].Position.X - Shared.GAP ||
                        Shared.positions[i].X > enabledBlocks[j].Position.X + Shared.GAP &&
                        Shared.positions[i].Y < enabledBlocks[j].Position.Y - Shared.GAP ||
                        Shared.positions[i].Y > enabledBlocks[j].Position.Y + Shared.GAP)
                    {
                        emptyPositions.Add(Shared.positions[i]);
                    }
                }
            }

            Random random = new Random();
            int index = random.Next(0, emptyPositions.Count - 1);

            return emptyPositions[index];
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Shared.GAP, Shared.GAP);
        }
    }
}
