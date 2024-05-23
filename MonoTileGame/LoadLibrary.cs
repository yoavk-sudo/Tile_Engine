using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTileGame
{
    public class LoadLibrary
    {
        private static LoadLibrary instance = null;
        public static LoadLibrary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoadLibrary();
                }
                return instance;
            }
        }

        private Dictionary<string, Texture2D> Data;

        private LoadLibrary()
        {
            Data = new Dictionary<string, Texture2D>
            {
                { "Missing_Texture", TileGame.Instance.Content.Load<Texture2D>("MissingTexture") }
            };
        }

        private void LoadData(string Key, string url)
        {
            if (!Data.ContainsKey(Key))
            {
                Data.Add(Key, TileGame.Instance.Content.Load<Texture2D>(url));
            }
        }

        public void LoadData(IRenderable LoadObject)
        {
            LoadData(LoadObject.Key, LoadObject.url);
        }

        public Texture2D GetTexture(IRenderable Subject) 
        {
            if (Data.ContainsKey(Subject.Key))
            {
                return Data[Subject.Key];
            }
            return Data["Missing_Texture"];
        }
    }
}
