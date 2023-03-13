using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
public class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    public enum GameState
    {
        StartScreen,
        Playing,
        GameOver
    }

    double timer;

    /// <summary>
    /// Enum value of the current GameState.
    /// </summary>
    public GameState CurrentGameState;

    /// <summary>
    /// Keeps track of the player score.
    /// </summary>
    private int score;

    /// <summary>
    /// Keeps track of the current level.
    /// </summary>
    private int level;

    private int previousLevel;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// Gamestate sprite.
    /// </summary>
    Texture2D background_start, spr_gameover, background_play;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    public Grid Grid { get; protected set; }

    /// <summary>
    /// TetrisBlock object used for drawing the correct block type.
    /// </summary>
    private TetrisBlock fallingBlock, upcomingBlock;

    private TetrisBlockFactory blockFactory;

    public GameWorld(ContentManager contentManager)
    {
        CurrentGameState = GameState.StartScreen;
        blockFactory = new(this);
        previousLevel = 0;

        font = contentManager.Load<SpriteFont>("font_SpelFont");
        background_start = contentManager.Load<Texture2D>("backgrounds/background_Start");
        background_play = contentManager.Load<Texture2D>("backgrounds/background_Play");
        spr_gameover = contentManager.Load<Texture2D>("spr_gameover");

        MediaPlayer.IsRepeating = true;

        // Create Grid and Block objects
        Grid = new Grid(contentManager, this);
    }

    public void Update(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // stop updating the game world if game is over
        if (CurrentGameState == GameState.GameOver)
        {
            return;
        }

        if (CurrentGameState == GameState.Playing)
        {
            fallingBlock.Update(gameTime, this);
            // update timer
            if (timer != 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (CurrentGameState == GameState.Playing)
        {
            fallingBlock.HandleInput(inputHelper);
        }
        if (CurrentGameState != GameState.Playing && inputHelper.KeyPressed(Keys.Enter))
        {
            Reset();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        // Start Screen
        if (CurrentGameState == GameState.StartScreen)
        {
            spriteBatch.Draw(background_start, new Vector2(0, 0), null, Color.White, 0f,
                             Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
        }
        // Playing Screen
        if (CurrentGameState != GameState.StartScreen)
        {
            DrawPlayingState(spriteBatch);
            // When player has leveled up, the timer is set and feedback should be drawn
            if (timer > 0)
            {
                spriteBatch.DrawString(font, "Level up!", new Vector2(Tetris.ScreenSize.X / 2.2f, 150), Color.Purple, 0f,
                    Vector2.Zero, 2f, SpriteEffects.None, 0f);

            }

            // Game Over Overlay
            if (CurrentGameState == GameState.GameOver)
            {
                spriteBatch.Draw(spr_gameover, new Vector2(120, 120),
                    null, Color.White, 0f, Vector2.Zero, 0.88f, SpriteEffects.None, 0f);
            }
        }
        spriteBatch.End();
    }

    /// <summary>
    /// If the player's score has exceeded the required score for leveling up,
    /// adds a level and sets a timer indicating the duration of the feedback text drawn.
    /// </summary>
    public void LevelUpCheck()
    {
        int requiredScore = level * 20;

        if (score >= requiredScore)
        {
            PlaySoundEffect("sound_yay");
            level++;
            timer = 3;
        }
    }

    /// <summary>
    /// Draws the playing background, the grid, falling block and GUI
    /// </summary>
    /// <param name="spriteBatch"></param>
    void DrawPlayingState(SpriteBatch spriteBatch)
    {
        // background
        spriteBatch.Draw(background_play, new Vector2(220, -60), null, Color.White, 0f,
                            Vector2.Zero, 0.69f, SpriteEffects.None, 0f);

        Grid.Draw(spriteBatch);

        // GUI
        spriteBatch.DrawString(font, "Level: ", new Vector2(Tetris.ScreenSize.X / 2 - 30, 50), Color.White);
        spriteBatch.DrawString(font, level.ToString(), new Vector2(Tetris.ScreenSize.X / 2 + 30, 50), Color.White);
        spriteBatch.DrawString(font, "Score: ", new Vector2(Tetris.ScreenSize.X / 2 - 30, 95), Color.White);
        spriteBatch.DrawString(font, score.ToString(), new Vector2(Tetris.ScreenSize.X / 2 + 30, 95), Color.White);
        spriteBatch.DrawString(font, "Upcoming Block: ", new Vector2(Tetris.ScreenSize.X / 2 - 30, 300), Color.White);
        upcomingBlock.Draw(spriteBatch, new Vector2(310, 340)); // draw at side of the screen

        // currently falling block
        fallingBlock.Draw(spriteBatch, new Vector2(0, -30));
    }


    /// <summary>
    /// Sets the falling block and upcoming block to be a random next block from the factory.
    /// If there's already an upcoming block defined: updates this to be the falling block and
    /// only gets a new upcoming block.
    /// </summary>
    public void GetNextBlocks()
    {
        if (upcomingBlock == null)
        {
            fallingBlock = blockFactory.GenerateBlock();
            upcomingBlock = blockFactory.GenerateBlock();
        }
        else
        {
            fallingBlock = upcomingBlock;
            upcomingBlock = blockFactory.GenerateBlock();
        }
    }

    public void AddScore(int _rows)
    {
        score += 5 * _rows;
        LevelUpCheck();
    }

    /// <summary>
    /// Resets the game and creates new blocks.
    /// </summary>
    public void Reset()
    {
        CurrentGameState = GameState.Playing;
        score = 0;
        level = 1;
        Grid.ClearGrid();

        GetNextBlocks();

        MediaPlayer.Play(Tetris.ContentManager.Load<Song>("sounds/sound_bop"));
    }

    /// <summary>
    /// Loads and plays the sound effect with the given asset name.
    /// </summary>
    /// <param name="soundfile">The name of the asset to load.</param>
    public void PlaySoundEffect(string soundfile)
    {
        SoundEffect sound = Tetris.ContentManager.Load<SoundEffect>("sounds/" + soundfile);
        sound.Play();
    }

}
