using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockI : TetrisBlock
{
    public BlockI(GameWorld gameWorld, Color color) : base (gameWorld)
    {
        BlockColor = color;
        ShapeSize = 4;
        Shape = new bool[4, 4]
        {
            { false, true, false, false},
            { false, true, false, false},
            { false, true, false, false},
            { false, true, false, false}
        };
    }
}