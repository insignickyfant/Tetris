using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockJ : TetrisBlock
{
    public BlockJ(GameWorld gameWorld, Color color) : base(gameWorld)
    {
        BlockColor = color;
        ShapeSize = 3;
        Shape = new bool[3, 3]
        {
            { true, true, false},
            { false, true, false },
            { false, true, false }
        };   
    }
}