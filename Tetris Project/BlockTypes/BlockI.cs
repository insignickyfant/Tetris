using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockI : TetrisBlock
{
    public BlockI(GameWorld gameWorld) : base (gameWorld)
    {
        ShapeSize = 4;
        Shape = new bool[4, 4]
        {
            { false, true, false, false},
            { false, true, false, false},
            { false, true, false, false},
            { false, true, false, false}
        };
        BlockColor = Color.White;
        TypeEnum = TetrisBlockFactory.BlockTypeEnum.I;
    }
}