using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FiftyFive
{
    public abstract class GameScene : DrawableGameComponent
    {
        public List<GameComponent> Components { get; set; }

        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        public GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent component)
                {
                    if (component.Visible)
                    {
                        component.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
    }
}
