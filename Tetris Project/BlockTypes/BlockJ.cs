using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockJ : TetrisBlock
{
    public BlockJ(GameWorld gameWorld) : base(gameWorld)
    {
        ShapeSize = 3;
        Shape = new bool[3, 3]
        {
            { true, true, false},
            { false, true, false },
            { false, true, false }
        };
        BlockColor = new Color(223, 29, 102);
        TypeEnum = TetrisBlockFactory.BlockTypeEnum.J;
    }
}