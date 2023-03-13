using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class EmptyCell : TetrisBlock
{
    public EmptyCell(GameWorld gameWorld) : base(gameWorld)
    {
        ShapeSize = 1;
        BlockColor = new Color(102, 102, 102);
        TypeEnum = TetrisBlockFactory.BlockTypeEnum.empty;
    }
}