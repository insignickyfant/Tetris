using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockO : TetrisBlock
{
    public BlockO(GameWorld gameWorld, Color color) : base(gameWorld)
    {
        ShapeSize = 2;
        Shape = new bool[2, 2]
        {
            { true, true },
            { true, true }
        };
        
        //
        BlockColor = color;
    }
}