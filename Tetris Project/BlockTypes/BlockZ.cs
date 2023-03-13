using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

class BlockZ : TetrisBlock
{
    public BlockZ(GameWorld gameWorld) : base(gameWorld)
    {
        ShapeSize = 3;
        Shape = new bool[3, 3] 
        { 
            { true, false, false}, 
            { true, true, false}, 
            { false, true, false} 
        };
        BlockColor = Color.White;
        TypeEnum = TetrisBlockFactory.BlockTypeEnum.Z;
    }
}