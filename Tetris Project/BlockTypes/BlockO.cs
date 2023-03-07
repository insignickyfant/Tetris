using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockO : TetrisBlock
{
    public BlockO()
    {
        ShapeSize = 2;
        Shape = new bool[2, 2]
        {
            { true, true },
            { true, true }
        };
        BlockColor = Color.White;
        TypeEnum = TetrisBlockFactory.BlockTypeEnum.O;
    }
}