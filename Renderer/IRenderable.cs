using System.Numerics;

namespace Renderer
{
    public interface IRenderable
    {
        public void Init(ISprite Sprite, Vector2 position2D);
        public IRenderable Clone();
        public void UpdatePosition(Vector2 newPosition);
        public void Tint(object Color);
        public void ResetTint();

    }

    public interface ISprite { }

    public interface IRenderableTile : IRenderable { }

    public interface IRenderableGamePiece : IRenderable { }

}