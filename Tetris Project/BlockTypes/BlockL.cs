using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockL : TetrisBlock
{
    public BlockL(GameWorld gameWorld, Color color) : base(gameWorld)
    {
        BlockColor = color;
        ShapeSize = 3;
        Shape = new bool[3, 3]
        {
            { false, true, false},
            { false, true, false },
            { true, true, false }
        };
        
    }
}