using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockS : TetrisBlock
{
    public BlockS(GameWorld gameWorld, Color color) : base(gameWorld)
    {
        BlockColor = color;
        ShapeSize = 3;
        Shape = new bool[3, 3]
        {
            { false, true, false},
            { true, true, false},
            { true, false, false}
        };
    }
}