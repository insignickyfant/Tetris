using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using System.Reflection.Emit;
using System;
using Microsoft.Xna.Framework.Content;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
public class Grid
{
    /// The sprite of a single empty cell in the grid.
    public static Texture2D cellSprite;

    /// <summary>
    /// The position at which this TetrisGrid should be drawn.
    /// </summary>
    Vector2 position;

    /// <summary>
    /// The number of grid elements in the x-direction.
    /// </summary>
    public const int Width = 10;

    /// <summary>
    /// The number of grid elements in the y-direction.
    /// </summary>
    public const int Height = 22;

    private ContentManager contentManager;
    private GameWorld gameWorld;

    /// <summary>
    /// Stores for each cell of the grid what BlockType currently occupies it
    /// </summary>
    public TetrisBlockFactory.BlockTypeEnum[,] Occupation { get; private set; }
    public Color[,] BlocksInGrid { get; private set; }
    private Color emptyCellColor = new Color(102, 102, 102);

    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public Grid(ContentManager contentManager, GameWorld gameWorld)
    {
        this.contentManager = contentManager;
        this.gameWorld = gameWorld;
        cellSprite = contentManager.Load<Texture2D>("spr_block");
        position = new Vector2(0, -2 * cellSprite.Height);
        BlocksInGrid = new Color[Width, Height];
        Occupation = new TetrisBlockFactory.BlockTypeEnum[Width, Height];
        ClearGrid();
    }

    /// <summary>
    /// Draws the current state of the grid on the screen.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        // for each cell, draw the color of the block that is placed in the grid or an empty cell
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                // calculate the position on the screen by using the grid position and cell width
                position = new Vector2(x, y) * cellSprite.Width;
                spriteBatch.Draw(cellSprite, position, BlocksInGrid[x, y]);
            }
        }
    }

    /// <summary>
    /// Checks if a block's cell overlaps a cell of a block already placed in the grid.
    /// </summary>
    /// <param name="_cellPosition">Position of a block cell</param>
    /// <returns></returns>
    public bool OverlapCheck(Vector2 _cellPosition)
    {
        Vector2 gridCell = _cellPosition / cellSprite.Width;
        if (BlocksInGrid[(int)gridCell.X, (int)gridCell.Y] != emptyCellColor)
            return true;
        else return false;
    }

    /// <summary>
    /// Definitively places each cell of the block in the Grid.
    /// </summary>
    public void PlaceBlockInGrid(TetrisBlock block)
    {
        for (int x = 0; x < block.ShapeSize; x++)
        {
            for (int y = block.ShapeSize - 1; y >= 0; y--) // start at bottom-left cell
            {
                if (block.Shape[x, y] == true)
                {
                    Vector2 blockOffset = new Vector2(x, y) * cellSprite.Width;

                    Vector2 cellPosition = block.Position + blockOffset;
                    // Set the current block cell in the Grid.
                    Point gridCell = (cellPosition / cellSprite.Width).ToPoint();
                    //if (BlocksInGrid[gridCell.X, gridCell.Y] == emptyCellColor)
                    BlocksInGrid[gridCell.X, gridCell.Y] = block.BlockColor;
                }
            }
        }
        gameWorld.PlaySoundEffect("sound_place");
        CheckRowFull();
    }

    /// <summary>
    /// If a full row is found, the rows on top move down and the score is updated.
    /// </summary>
    void CheckRowFull()
    {
        int _rowsCleared = 0; // keep track of full rows
        for (int row = 0; row < Height; row++)
        {
            bool occupied = true;
            for (int column = 0; column < Width; column++)
            {
                // if any cell in this row is empty, this cell is not occupied and the 
                // next row should be checked
                if (BlocksInGrid[column, row] == emptyCellColor)
                {
                    occupied = false;
                    break;
                }
            }
            // continue to check the next row
            if (!occupied) continue;

            // if no unoccupied slots, row is full
            MoveRowsDown(row); // drop all rows above the full row
            gameWorld.PlaySoundEffect("sound_fullrow");

            _rowsCleared++;
        }
        // reward player and add bonuspoints if clearing multiple rows
        if (_rowsCleared > 0) 
            gameWorld.AddScore(_rowsCleared);
    }


    /// <summary>
    /// All the rows above the removed row move down.
    /// </summary>
    void MoveRowsDown(int _fullRow)
    {
        for (int y = _fullRow; y > 0; y--) // from the row that was full
        {
            for (int x = 0; x < Width; x++)
            {
                // move rows down
                BlocksInGrid[x, y] = BlocksInGrid[x, y - 1];
            }
        }
        CheckRowFull(); // check again
    }

    /// <summary>
    /// Clears the grid by filling it with empty cells.
    /// </summary>
    public void ClearGrid()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                BlocksInGrid[x, y] = emptyCellColor;
            }
        }
    }
}

