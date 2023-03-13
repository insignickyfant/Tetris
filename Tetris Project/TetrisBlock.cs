using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

/// <summary>
/// A class for general Tetris Blocks.
/// </summary>
public class TetrisBlock
{
    /// <summary>
    /// The block Texture.
    /// </summary>
    private Texture2D cellSprite;

    /// <summary>
    /// Positional vector for the tetris block.
    /// </summary>
    private Vector2 newPosition;

    private GameWorld gameWorld;

    public Vector2 Position { get; private set; }

    /// <summary>
    /// Velocity vector for the tetris block.
    /// </summary>
    protected Vector2 velocity;

    /// <summary>
    /// An array of booleans that stores whether a position in a block grid is filled or not. 
    /// Used to define the shape of a tetris block.
    /// </summary>
    public bool[,] Shape { get; protected set; }

    public int ShapeSize;

    /// <summary>
    /// The Color of this TetrisBlock, set by each BlockType
    /// </summary>
    public Color BlockColor { get; protected set; }

    /// <summary>
    /// Constructor for general tetris blocks.
    /// </summary>
    public TetrisBlock(GameWorld gameWorld)
    {
        this.gameWorld = gameWorld;

        cellSprite = Tetris.ContentManager.Load<Texture2D>("spr_block");

        // falls faster if level is higher
        velocity = new Vector2(0, cellSprite.Height + 15 * (gameWorld.Level - 1));

        Position = new Vector2(cellSprite.Width * (Grid.Width / 2 - 2), 0);
        newPosition = Vector2.Zero;
    }

    /// <summary>
    /// Updates the falling block as part of the game loop.
    /// </summary>
    /// <param name="gameTime">Keeps track of time passed.</param>
    /// <param name="gameWorld">Instance of the GameWorld.</param>
    public void Update(GameTime gameTime, GameWorld gameWorld)
    {
        this.gameWorld = gameWorld;
        // block continues falling down as long as it doesn't collide with another block
        newPosition = Position + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (!IsCollision(newPosition, Shape))
        {
            Position = newPosition;
        }
        // When the falling block collides, stop the block from falling down further
        // and set it in the Grid.
        else
        {
            SetBlock();
        }
    }

    /// <summary>
    /// Places this block in the Grid and, if not game over, gets next blocks.
    /// </summary>
    void SetBlock()
    {
        gameWorld.Grid.PlaceBlockInGrid(this);
        velocity = Vector2.Zero;

        // if the block had to be placed at the top of the screen, the game's over
        if (Position.Y < 2 * cellSprite.Height)
        {
            MediaPlayer.Stop();
            gameWorld.PlaySoundEffect("sound_gameover2");
            gameWorld.CurrentGameState = GameWorld.GameState.GameOver;
        }
        else
        {
            // Update the current and next block and type
            gameWorld.GetNextBlocks();
        }
    }

    /// <summary>
    /// Draws the block on the screen.
    /// </summary>
    /// <param name="_placement">Extra parameter given to adjust positioning of blocks.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 _placement)
    {
        for (int x = 0; x < ShapeSize; x++)
        {
            for (int y = 0; y < ShapeSize; y++)
            {
                if (Shape[x, y] == true)
                {
                    Vector2 blockOffset = new Vector2(x, y) * cellSprite.Width;
                    Vector2 drawPosition = Position + blockOffset + _placement;
                    spriteBatch.Draw(cellSprite, drawPosition, BlockColor);
                }
            }
        }
    }

    /// <summary>
    /// Rotates the block in the stated direction.
    /// </summary>
    /// <param name="direction">Input string indicating direction of rotation.</param>
    void Rotate(string direction)
    {
        // make a copy of the current block array to use in calculating the new array
        bool[,] copy = new bool[ShapeSize, ShapeSize];
        for (int row = 0; row < ShapeSize; row++)
        {
            for (int column = 0; column < ShapeSize; column++)
            {
                // Clockwise Rotation
                if (direction == "Right")
                {
                    copy[column, row] = Shape[row, ShapeSize - 1 - column];
                }
                // Counterclockwise Rotation
                else if (direction == "Left")
                {
                    copy[column, row] = Shape[ShapeSize - 1 - row, column];
                }
            }
        }
        if (!IsCollision(Position, copy))
        {
            Shape = copy;
        }
        else return;
    }

    /// <summary>
    /// Places the falling block down at the bottom-most position currently possible.
    /// </summary>
    void PutDown()
    {
        velocity = Vector2.Zero;
        // find the lowest non-colliding position using binary search
        int top = 0;
        int bottom = Grid.Height;
        while (top < bottom)
        {
            int mid = (top + bottom + 1) / 2;
            newPosition = new Vector2(Position.X, Position.Y + mid * cellSprite.Height);
            if (!IsCollision(newPosition, Shape))
            {
                top = mid;
            }
            else
            {
                bottom = mid - 1;
            }
        }

        // set the position to the lowest non-colliding position
        newPosition = new Vector2(Position.X, Position.Y + top * cellSprite.Height);
        Position = newPosition;

        SetBlock();
    }

    /// <summary>
    /// Checks if the current block overlaps any blocks already placed on the grid.
    /// </summary>
    /// <returns>True when outside of bounds or when there's overlap, otherwise False.</returns>
    bool IsCollision(Vector2 _newPosition, bool[,] _blockShape)
    {
        int gridWidth = Grid.Width * cellSprite.Width;

        Vector2 cellPosition;
        for (int x = 0; x < ShapeSize; x++)
        {
            // check from the bottom of the block, as this is the most likely point of collision
            for (int y = ShapeSize - 1; y >= 0; y--)
            {
                if (_blockShape[x, y] == true) // if this cell is part of the block / "filled in"
                {
                    cellPosition = _newPosition + new Vector2(x, y) * cellSprite.Width;
                    // Out of Bounds check
                    if (cellPosition.X < 0 || cellPosition.X >= gridWidth || cellPosition.Y >= Tetris.ScreenSize.Y) //gridHeight
                    {
                        gameWorld.PlaySoundEffect("sound_bump");
                        return true;
                    }
                    // if the new cell position is within bounds, check if the move overlaps a block
                    // that was already placed in the grid
                    // Check if the block overlaps with any existing blocks in the grid
                    else
                    {
                        bool overlap = gameWorld.Grid.OverlapCheck(cellPosition);
                        if (overlap)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Moves blocks according to the user input after checking if it is allowed.
    /// </summary>
    /// <param name="inputHelper">InputHelper object</param>
    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Down))
        {
            newPosition = new Vector2(Position.X, Position.Y + cellSprite.Height);
            if (!IsCollision(newPosition, Shape))
            {
                gameWorld.PlaySoundEffect("sound_move");
                Position = newPosition;
            }
        }
        if (inputHelper.KeyPressed(Keys.Left))
        {
            newPosition = new Vector2(Position.X - cellSprite.Width, Position.Y);
            if (!IsCollision(newPosition, Shape))
            {
                gameWorld.PlaySoundEffect("sound_move");
                Position = newPosition;
            }
        }
        if (inputHelper.KeyPressed(Keys.Right))
        {
            newPosition = new Vector2(Position.X + cellSprite.Width, Position.Y);
            if (!IsCollision(newPosition, Shape))
            {
                gameWorld.PlaySoundEffect("sound_move");
                Position = newPosition;
            }
        }
        if (inputHelper.KeyPressed(Keys.A))
        {
            Rotate("Left");
            gameWorld.PlaySoundEffect("sound_move");
        }
        if (inputHelper.KeyPressed(Keys.D))
        {
            Rotate("Right");
            gameWorld.PlaySoundEffect("sound_move");
        }
        // Position > 10 in order to avoid placing block immediately after
        // spacebar press used for starting the game
        if (inputHelper.KeyPressed(Keys.Space) && Position.Y > 10)
        {
            PutDown();
        }
    }
}
