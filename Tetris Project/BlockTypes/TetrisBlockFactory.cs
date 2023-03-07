using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class TetrisBlockFactory
{
    public static TetrisBlock GenerateBlock()
    {
        BlockTypeEnum blockType = (BlockTypeEnum)Tetris.Random.Next(7);
        switch (blockType)
        {
            case BlockTypeEnum.I:
                return new BlockI();
            case BlockTypeEnum.J:
                return new BlockJ();
            case BlockTypeEnum.L:
                return new BlockL();
            case BlockTypeEnum.O:
                return new BlockO();
            case BlockTypeEnum.S:
                return new BlockS();
            case BlockTypeEnum.T:
                return new BlockT();
            case BlockTypeEnum.Z:
                return new BlockZ();
            default:
                return new EmptyCell();
        }
    }

    /// <summary>
    /// Indicates the type of block
    /// </summary>
    public enum BlockTypeEnum { I, O, T, L, J, S, Z, empty }
}
