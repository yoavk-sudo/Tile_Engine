using System.Numerics;

namespace Renderer
{
    public interface IRenderable
    {
        public void Init(ISprite Sprite);
        public IRenderable Clone();
        public void UpdateSprite(ISprite Sprite);
        public void Tint(object Color);
        public void ResetTint();

    }

    public interface ISprite { }

}