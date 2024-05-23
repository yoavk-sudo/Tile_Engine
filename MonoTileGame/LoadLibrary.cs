using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Renderer;

namespace MonoTileGame
{
    public class LoadLibrary
    {
        private static LoadLibrary instance = null;

        private TileGame game;

        public TileGame Game { set { game = value; } }
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
                { "Missing_Texture", game.Content.Load<Texture2D>("MissingTexture") }
            };
        }

        private void LoadData(string Key, string url)
        {
            if (!Data.ContainsKey(Key))
            {
                Data.Add(Key, game.Content.Load<Texture2D>(url));
            }
        }

        public void LoadData(Sprite LoadObject)
        {
            LoadData(LoadObject.Key, LoadObject.url);
        }

        public void UpdateData(string Key, string url)
        {
            if (!Data.ContainsKey(Key))
            {
                LoadData(Key, url);
            }
            else
            {
                Data[Key] = game.Content.Load<Texture2D>(url);
            }
        }

        public Texture2D GetTexture(Sprite Subject) 
        {
            if (Data.ContainsKey(Subject.Key))
            {
                return Data[Subject.Key];
            }
            return Data["Missing_Texture"];
        }

        public Texture2D GetTexture(string Key)
        {
            if (Data.ContainsKey(Key))
            {
                return Data[Key];
            }
            return Data["Missing_Texture"];
        }
    }
}
