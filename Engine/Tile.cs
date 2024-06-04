using Renderer;
using System.Numerics;

namespace Tile_Engine
{
    public class Tile : IPosition
    {
        public Position Position { get; }
        public TileObject TileObject { get; set; } = null;
        public bool IsEmpty { get {  return TileObject == null; } }
        //public Actor Actor { get; set; }
        public IRenderable Texture { get; set; }
        private ISprite Sprite;

        public override string ToString()
        {
            //return TileObject.ToString() + ", " + Position.ToString();
            return Position.ToString();
        }

        public Tile(Position position, IRenderable texture, ISprite sprite)
        {
            Position = position;
            Texture = texture;
            Sprite = sprite;
        }

        public void InitTexture()
        {
            Texture.Init(Sprite);
        }

        public virtual void NewTileObject(TileObject tObject)
        {
            TileObject = tObject;
        }
    }
}
