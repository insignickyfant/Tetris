﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockT : TetrisBlock
{
    public BlockT()
    {
        ShapeSize = 3;
        Shape = new bool[3, 3]
        {
            { false, true, false},
            { true, true, false },
            { false, true, false }
        };
        BlockColor = new Color(53, 148, 118);
        TypeEnum = TetrisBlockFactory.BlockTypeEnum.T;
    }
}