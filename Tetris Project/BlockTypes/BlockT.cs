using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockT : TetrisBlock
{
    public BlockT(GameWorld gameWorld, Color color) : base(gameWorld)
    {
        ShapeSize = 3;
        Shape = new bool[3, 3]
        {
            { false, true, false},
            { true, true, false },
            { false, true, false }
        };

        //
        BlockColor = color;
    }
}