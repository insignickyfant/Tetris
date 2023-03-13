using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class EmptyCell : TetrisBlock
{
    public EmptyCell(GameWorld gameWorld) : base(gameWorld)
    {
        BlockColor = new Color(102, 102, 102);
        ShapeSize = 1;
    }
}