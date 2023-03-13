using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

public class TetrisBlockFactory
{
    GameWorld gameWorld;
    public TetrisBlockFactory(GameWorld gameWorld)
    {
        this.gameWorld = gameWorld;
    }

    public TetrisBlock GenerateBlock()
    {
        BlockColorEnum blockColor = (BlockColorEnum)Tetris.Random.Next(3);
        BlockTypeEnum blockType = (BlockTypeEnum)Tetris.Random.Next(7);
        Color color;

        switch (blockColor)
        {
            case BlockColorEnum.White:
                color = Color.White;
                break;
            case BlockColorEnum.Red:
                color = new Color(223, 29, 102);
                break;
            case BlockColorEnum.Green:
                color = new Color(53, 148, 118);
                break;
            default:
                color = Color.White;
                break;
        }
        switch (blockType)
        {
            case BlockTypeEnum.I:
                return new BlockI(gameWorld, color);
            case BlockTypeEnum.J:
                return new BlockJ(gameWorld, color);
            case BlockTypeEnum.L:
                return new BlockL(gameWorld, color);
            case BlockTypeEnum.O:
                return new BlockO(gameWorld, color);
            case BlockTypeEnum.S:
                return new BlockS(gameWorld, color);
            case BlockTypeEnum.T:
                return new BlockT(gameWorld, color);
            case BlockTypeEnum.Z:
                return new BlockZ(gameWorld, color);
            default:
                return new EmptyCell(gameWorld);
        }
    }

    /// <summary>
    /// Indicates the type of block that needs to be created
    /// </summary>
    private enum BlockTypeEnum { I, O, T, L, J, S, Z }
    
    private enum BlockColorEnum { White, Red, Green }
}
