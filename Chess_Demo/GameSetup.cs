using Microsoft.Xna.Framework;
using MonoTileGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tile_Engine;

namespace Chess_Demo
{
    internal class GameSetup
    {
        static Sprite sprite = new("a", "ball");
        private const int PAWN_AMOUNT = 8;
        static TileMap map = new(8, 8, sprite);
        //SpriteLoader spriteManager = new();
        //TileGame game = new TileGame(map);
        TileGame game = new TileGame(map);
        SpriteLoader spriteManager = new();
        public void CreateChessPieces()
        {

            Sprite wpSprite = spriteManager.WhiteUnits[0];
            Sprite bpSprite = spriteManager.BlackUnits[0];
            Sprite wqSprite = spriteManager.WhiteUnits[4];
            Sprite bqSprite = spriteManager.BlackUnits[4];
            Sprite wkSprite = spriteManager.WhiteUnits[5];
            Sprite bkSprite = spriteManager.BlackUnits[5];
            Sprite wrSprite = spriteManager.WhiteUnits[3];
            Sprite brSprite = spriteManager.BlackUnits[3];
            Sprite wbsSprite = spriteManager.WhiteUnits[1];
            Sprite bbsSprite = spriteManager.BlackUnits[1];
            Sprite wknSprite = spriteManager.WhiteUnits[2];
            Sprite bknSprite = spriteManager.BlackUnits[2];

            Actor white = new("White");
            Actor black = new("Black");
            //pawnObject.Destroy();

            WhitePawn p = new();
            TileObject pawnObject = p.CreateTileObject(map.GetMap()[0, 0], white, wpSprite);
            pawnObject.InitTexture(new TileRenderer());
            for (int i = 0; i < PAWN_AMOUNT; i++)
            {
                TileObject cTO = pawnObject.CloneTO(map.GetMap()[i, 6]);
                cTO.InitTexture(new TileRenderer());
            }
            BlackPawn bp = new();
            TileObject blackPawnObject = bp.CreateTileObject(map.GetMap()[0, 0], black, bpSprite);
            blackPawnObject.InitTexture(new TileRenderer());
            for (int i = 0; i < PAWN_AMOUNT; i++)
            {
                TileObject cTO = blackPawnObject.CloneTO(map.GetMap()[i, 1]);
                cTO.InitTexture(new TileRenderer());
            }
            pawnObject.Destroy();
            blackPawnObject.Destroy();
            Queen q = new();
            TileObject bQueen = q.CreateTileObject(map.GetMap()[3, 0], black, bqSprite);
            TileObject wQueen = q.CreateTileObject(map.GetMap()[3, 7], white, wqSprite);
            wQueen.InitTexture(new TileRenderer());
            bQueen.InitTexture(new TileRenderer());
            King k = new();
            TileObject bKing = k.CreateTileObject(map.GetMap()[4, 0], black, bkSprite);
            TileObject wKing = k.CreateTileObject(map.GetMap()[4, 7], white, wkSprite);
            bKing.InitTexture(new TileRenderer());
            wKing.InitTexture(new TileRenderer());
            Bishop b = new();
            TileObject bBishop = b.CreateTileObject(map.GetMap()[5, 0], black, bbsSprite);
            bBishop.CloneTO(map.GetMap()[2, 0]).InitTexture(new TileRenderer());
            TileObject wBishop = k.CreateTileObject(map.GetMap()[5, 7], white, wbsSprite);
            wBishop.CloneTO(map.GetMap()[2, 7]).InitTexture(new TileRenderer());
            bBishop.InitTexture(new TileRenderer());
            wBishop.InitTexture(new TileRenderer());
            Knight kn = new();
            TileObject bKnight = b.CreateTileObject(map.GetMap()[1, 0], black, bknSprite);
            bKnight.CloneTO(map.GetMap()[6, 0]).InitTexture(new TileRenderer());
            TileObject wKnight = k.CreateTileObject(map.GetMap()[1, 7], white, wknSprite);
            wKnight.CloneTO(map.GetMap()[6, 7]).InitTexture(new TileRenderer());
            bBishop.InitTexture(new TileRenderer());
            wBishop.InitTexture(new TileRenderer());
            Rook r = new();
            TileObject bRook = r.CreateTileObject(map.GetMap()[7, 0], black, brSprite);
            bRook.CloneTO(map.GetMap()[0, 0]).InitTexture(new TileRenderer());
            TileObject wRook = k.CreateTileObject(map.GetMap()[7, 7], white, wrSprite);
            wRook.CloneTO(map.GetMap()[0, 7]).InitTexture(new TileRenderer());
            bBishop.InitTexture(new TileRenderer());
            wBishop.InitTexture(new TileRenderer());
            Console.WriteLine(wRook.Owner.Name);//spriteManager.SetMapBackground(map);
            //game.MousePressedOnTile += Tint;
            //game.Run();
        }

        public void RunGame()
        {
            spriteManager.SetMapBackground(map);
            game.MousePressedOnTile += Tint;
            game.Run();
        }

        public static void Tint(Tile tile)
        {
            tile.Texture.Tint(new Object());
        }
    }
}
