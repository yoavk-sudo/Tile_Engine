using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Renderer;
using Microsoft.Xna.Framework;

namespace MonoTileGame
{
    public class TileRenderer : IRenderable
    {
        public Color tint = Color.White;
        public Sprite sprite;

        public TileRenderer() { }

        public TileRenderer(Sprite Sprite) { sprite = Sprite; }

        public IRenderable Clone()
        {
            throw new NotImplementedException();
        }

        public void Init(ISprite Sprite)
        {
            sprite = (Sprite)Sprite;
            LoadLibrary.Instance.LoadData(sprite);
        }

        public void UpdateSprite(ISprite Sprite)
        {
            sprite = (Sprite)Sprite;
        }

        public void ResetTint()
        {
            tint = Color.White;
        }

        public void Tint(object Color)
        {
            try { tint = (Color)Color; }
            catch { throw new InvalidCastException("Color Input must be an Xna Color"); }
        }

 
    }

    public class Sprite : ISprite
    {
        public string Key;
        public string url;
        public Sprite(string key, string Url)
        {
            Key = key;
            url = Url;
        }


    }



}
