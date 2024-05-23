namespace Renderer
{
    public interface IRenderable
    {
        public abstract string url { get; set; }
        public abstract string Key { get; set; }


    }

    public interface IRenderableTile : IRenderable { }

    public interface IRenderableGamePiece : IRenderable { }

    public interface IRenderableHighlight : IRenderable { }

}