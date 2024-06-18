using Renderer;
using System.Numerics;

namespace Tile_Engine
{
    public class Tile : IPosition, IDestroyable
    {
        private ISprite _sprite;
        public Position Position { get; }
        public TileObject TileObject { get; set; } = null;
        public bool IsEmpty { get {  return TileObject == null; } }
        //public Actor Actor { get; set; }
        public IRenderable Texture { get; set; }

        public override string ToString()
        {
            //return TileObject.ToString() + ", " + Position.ToString();
            return Position.ToString();
        }

        public Tile(Position position, ISprite sprite)
        {
            Position = position;
            _sprite = sprite;
        }

        public void InitTexture(IRenderable Renderer)
        {
            Texture = Renderer;
            Texture.Init(_sprite);
        }

        public virtual void NewTileObject(TileObject tObject)
        {
            TileObject = tObject;
        }

        public void Destroy()
        {
            Texture = null;
            _sprite = null;
        }

        public void DestroyChildren()
        {
            TileObject.Destroy();
        }
    }
}
