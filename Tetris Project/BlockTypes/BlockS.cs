using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockS : TetrisBlock
{
    public BlockS()
    {
        ShapeSize = 3;
        Shape = new bool[3, 3]
        {
            { false, true, false},
            { true, true, false},
            { true, false, false}
        };
        BlockColor = new Color(223, 29, 102);
        TypeEnum = TetrisBlockFactory.BlockTypeEnum.S;
    }
}