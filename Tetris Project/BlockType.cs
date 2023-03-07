//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;

//public class BlockType : TetrisBlock
//{
//    public BlockType(TetrisBlockFactory.BlockTypeEnum _type) : base(contentManager, level)
//    {
//        switch(_type)
//        {
//            case TetrisBlockFactory.BlockTypeEnum.I:
//                blockSize = 4;
//                blockShape = new bool[4, 4]
//                {
//                    { false, true, false, false},
//                    { false, true, false, false},
//                    { false, true, false, false},
//                    { false, true, false, false}
//                };
//                color = Color.White;
//                break;
//            case TetrisBlockFactory.BlockTypeEnum.S:
//                blockSize = 3;
//                blockShape = new bool[3, 3]
//                {
//                    { false, true, false},
//                    { true, true, false},
//                    { true, false, false}
//                };
//                color = new Color(223, 29, 102);
//                break;
//            case TetrisBlockFactory.BlockTypeEnum.J:
//                blockSize = 3;
//                blockShape = new bool[3, 3]
//                {
//                    { true, true, false},
//                    { false, true, false },
//                    { false, true, false }
//                };
//                color = new Color(223, 29, 102);
//                break;
//            case TetrisBlockFactory.BlockTypeEnum.L:
//                blockSize = 3;
//                blockShape = new bool[3, 3]
//                {
//                    { false, true, false},
//                    { false, true, false },
//                    { true, true, false }
//                };
//                color = new Color(53, 148, 118);
//                break;
//            case TetrisBlockFactory.BlockTypeEnum.O:
//                blockSize = 2;
//                blockShape = new bool[2, 2]
//                {
//                    { true, true },
//                    { true, true }
//                };
//                color = Color.White;
//                break;
//            default:
//                blockSize = 1;
//                color = new Color(102, 102, 102);
//                break;
//        }
//    }
//}

