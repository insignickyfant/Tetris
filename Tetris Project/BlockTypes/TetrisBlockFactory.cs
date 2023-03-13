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
        BlockTypeEnum blockType = (BlockTypeEnum)Tetris.Random.Next(7);
        switch (blockType)
        {
            case BlockTypeEnum.I:
                return new BlockI(gameWorld);
            case BlockTypeEnum.J:
                return new BlockJ(gameWorld);
            case BlockTypeEnum.L:
                return new BlockL(gameWorld);
            case BlockTypeEnum.O:
                return new BlockO(gameWorld);
            case BlockTypeEnum.S:
                return new BlockS(gameWorld);
            case BlockTypeEnum.T:
                return new BlockT(gameWorld);
            case BlockTypeEnum.Z:
                return new BlockZ(gameWorld);
            default:
                return new EmptyCell(gameWorld);
        }
    }

    /// <summary>
    /// Indicates the type of block
    /// </summary>
    public enum BlockTypeEnum { I, O, T, L, J, S, Z, empty }
}
