using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoString;

namespace FiftyFive
{
    public class PlayScene : GameScene
    {
        private Game1 game1;
        private GameOver gameOver;
        private Clear clear;
        private StartScene startScene;
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Vector2 backgroundPosition;
        private HeadBlock headBlock;
        private One one;
        private NaughtyTwo naughtyTwo;
        private OctoNaughtyCube octoNaughtyCube;
        private SimpleString score;
        private KeyboardState oldState;

        public const int OUTSIDE = -10000;
        public const int STAGE_ONE = 20;
        public const int STAGE_TWO = 40;
        public const int STAGE_THREE = 55;

        SimpleString[] stages = new SimpleString[3];
        int[] clearScore = { STAGE_ONE, STAGE_TWO, STAGE_THREE };

        public PlayScene(Game game,
                         StartScene startScene,
                         EndingScene endingScene) : base(game)
        {
            game1 = (Game1)game;
            this.spriteBatch = game1._spriteBatch;
            this.startScene = startScene;
            gameOver = new GameOver(game1, startScene);
            game1.Components.Add(gameOver);
            clear = new Clear(game1, endingScene);
            game1.Components.Add(clear);

            background = game1.Content.Load<Texture2D>("images/background/stage");
            backgroundPosition = Vector2.Zero;

            SpriteFont stageFont = game1.Content.Load<SpriteFont>("fonts/stageFont");
            Vector2 stagePosition = new Vector2(Shared.stage.X + Shared.GAP, Shared.screen.Y / 2);

            stages[0] = new SimpleString(game1, spriteBatch, stageFont, "STAGE 1", stagePosition, Color.White);
            stages[1] = new SimpleString(game1, spriteBatch, stageFont, "STAGE 2", stagePosition, Color.White);
            stages[2] = new SimpleString(game1, spriteBatch, stageFont, "STAGE 3", stagePosition, Color.White);

            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].Visible = false;
                this.Components.Add(stages[i]);
            }

            SpriteFont scoreFont = game1.Content.Load<SpriteFont>("fonts/scoreFont");
            string scoreMessage = "1";
            Vector2 scorePosition = new Vector2(getCenterOfPanel(scoreFont, scoreMessage),
                                                Shared.screen.Y / 2 + stageFont.LineSpacing * 2);
            score = new SimpleString(game1, spriteBatch, scoreFont, scoreMessage, scorePosition, Color.FloralWhite);

            SpriteFont blocksFont = game1.Content.Load<SpriteFont>("fonts/blocksFont");
            string blocksMessage = "blocks";
            Vector2 blocksPostition = new Vector2(getCenterOfPanel(blocksFont, blocksMessage),
                                                  scorePosition.Y + scoreFont.LineSpacing - Shared.GAP);
            SimpleString blocks = new SimpleString(game1, spriteBatch, blocksFont, blocksMessage, blocksPostition, Color.White);

            this.Components.Add(score);
            this.Components.Add(blocks);

            Vector2 headBlockPosition = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            SoundEffect turnSound = game1.Content.Load<SoundEffect>("sounds/turn");
            SoundEffect gameoverSound = game1.Content.Load<SoundEffect>("sounds/gameover");
            headBlock = new HeadBlock(game1, headBlockPosition, turnSound, gameoverSound, "");
            Block.blocks = new List<Block>();
            Block.blocks.Add(headBlock);

            SoundEffect addSound = game1.Content.Load<SoundEffect>("sounds/add");
            Vector2 onePosition = Block.getRandomPosition();
            one = new One(game1, onePosition, addSound);

            naughtyTwo = new NaughtyTwo(game1, new Vector2(0, 0));
            naughtyTwo.hide();
            octoNaughtyCube = new OctoNaughtyCube(game1, new Vector2(0, Shared.screen.Y - Shared.GAP), one);
            octoNaughtyCube.hide();

            CollisionManager collisionManager = new CollisionManager(game1, headBlock, one, naughtyTwo, octoNaughtyCube);

            this.Components.Add(headBlock);
            this.Components.Add(one);
            this.Components.Add(naughtyTwo);
            this.Components.Add(octoNaughtyCube);
            this.Components.Add(collisionManager);

            PlayerBlock lastBlock = headBlock;
            while (Block.blocks.Count < PlayerBlock.FIFTY_FIVE)
            {
                TailBlock block = new TailBlock(game1, new Vector2(OUTSIDE, OUTSIDE));
                block.PreviousBlock = lastBlock;
                Block.blocks.Insert(Block.blocks.Count, block);
                block.Enabled = false;
                block.Visible = false;
                this.Components.Add(block);

                lastBlock = block;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, backgroundPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            List<PlayerBlock> enabledBlocks = PlayerBlock.getEnabledBlocks();

            if (enabledBlocks.Count <= STAGE_ONE)
            {
                naughtyTwo.hide();
                octoNaughtyCube.hide();

            }
            else if (enabledBlocks.Count < STAGE_TWO)
            {
                naughtyTwo.show();
            }
            else if (enabledBlocks.Count == STAGE_TWO)
            {
                naughtyTwo.Position = Vector2.Zero;
                naughtyTwo.hide();
                octoNaughtyCube.hide();
            }
            else
            {
                naughtyTwo.hide();
                octoNaughtyCube.show();
            }

            if (!headBlock.Visible)
            {
                gameOver.show();
                gameOver.DrawOrder = 1;

                KeyboardState currentState = Keyboard.GetState();
                if (gameOver.Enabled)
                {
                    int selectedPopUpIndex = gameOver.PopUpMenu.SelectedIndex;
                    
                    if (selectedPopUpIndex == 0 && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {
                        int length = 0;
                        if (enabledBlocks.Count >= STAGE_TWO)
                        {
                            length = STAGE_TWO;
                        }
                        else if (enabledBlocks.Count >= STAGE_ONE)
                        {
                            length = STAGE_ONE;
                        }
                        else
                        {
                            length = 1;
                        }

                        initialize(length);

                        naughtyTwo.Position = new Vector2(0, 0);

                        gameOver.hide();
                    }
                    else if (selectedPopUpIndex == 1 && currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {
                        game1.hideAllScenes();
                        startScene.show();
                        startScene.Menu.Enabled = true;
                        startScene.Menu.SelectedIndex = 0;
                        MediaPlayer.Play(game1.Content.Load<Song>("sounds/title"));
                        gameOver.hide();
                        gameOver.PopUpMenu.SelectedIndex = 0;
                    }
                }
            }

            if (enabledBlocks.Count == 55)
            {
                clear.show();
                clear.DrawOrder = 1;
                this.Enabled = false;
                foreach (PlayerBlock block in enabledBlocks)
                {
                    if (block is TailBlock tail)
                    {
                        tail.Visible = false;
                    }
                }
                initialize(1);
            }

            for (int i = 0; i < stages.Length; i++)
            {
                if (enabledBlocks.Count < clearScore[i])
                {
                    foreach (SimpleString stage in stages)
                    {
                        stage.Visible = false;
                    }
                    stages[i].Visible = true;

                    break;
                }
            }

            score.Message = enabledBlocks.Count.ToString();
            score.Position = new Vector2(getCenterOfPanel(score.Font, score.Message),
                                         score.Position.Y);
            
            base.Update(gameTime);
        }

        private int getCenterOfPanel(SpriteFont font, string message)
        {
            return (int)((Shared.stage.X - Shared.GAP / 2) +
                (Shared.screen.X - Shared.stage.X - font.MeasureString(message).X) / 2);
        }

        private void initialize(int length)
        {
            List<PlayerBlock> enabledBlocks = PlayerBlock.getEnabledBlocks();

            int lastIndex = enabledBlocks.Count - 1;
            for (int i = lastIndex; i > length - 1; i--)
            {
                enabledBlocks[i].Enabled = false;
            }

            headBlock.Visible = true;
            headBlock.Position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            headBlock.Rotation = 0;
            headBlock.Scale = 1;
            headBlock.Origin = Vector2.Zero;
            headBlock.IsOver = false;

            headBlock.KeyOrder.Clear();

            enabledBlocks = PlayerBlock.getEnabledBlocks();
            PlayerBlock.updateBlockTexture();


            foreach (PlayerBlock block in enabledBlocks)
            {
                if (block is TailBlock tail)
                {
                    for (int i = 0; i < tail.Route.Length; i++)
                    {
                        tail.Route[i] = headBlock.Position;
                    }
                    tail.Position = headBlock.Position;
                    tail.Visible = true;
                }
            }

            one.Position = Block.getRandomPosition();
            headBlock.Key = "";
        }
    }
}
