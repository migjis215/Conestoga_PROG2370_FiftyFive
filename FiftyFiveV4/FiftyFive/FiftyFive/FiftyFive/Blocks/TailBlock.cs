using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FiftyFive
{
    public class TailBlock : PlayerBlock
    {
        private PlayerBlock previousBlock;
        private Vector2[] route;

        public PlayerBlock PreviousBlock { get => previousBlock; set => previousBlock = value; }
        public Vector2[] Route { get => route; set => route = value; }

        public TailBlock(Game game,
                     SpriteBatch spriteBatch,
                     Vector2 position) : base(game, spriteBatch, position)
        { }

        public override void Update(GameTime gameTime)
        {
            LastPosition = Position;
            int lastIndex = Route.Length - 1;

            if (Route[lastIndex] != PreviousBlock.LastPosition)
            {
                for (int i = 0; i < lastIndex; i++)
                {
                    Route[i] = Route[i + 1];
                }
                Route[lastIndex] = PreviousBlock.LastPosition;
                Position = Route[0];
            }

            base.Update(gameTime);
        }

    }
}
