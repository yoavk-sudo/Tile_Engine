﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Engine
{
    public class Actor : IDestroyable
    {
        private static string _name;

        public string Name { get; } = _name;
        public List<TileObject> TileObjects { get; private set; } = new List<TileObject>();

        public Actor(string actorName)
        {
            _name = actorName;
        }

        public void AddObjects(List<TileObject> objects) => TileObjects.AddRange(objects);
        public void RemoveObject(TileObject tileObject) => TileObjects.Remove(tileObject);
        public bool TryRemoveAtIndex(int index)
        {
            try
            {
                TileObjects.RemoveAt(index);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public void Destroy()
        {
            _name = null;
            TileObjects = null;
        }

        public void DestroyChildren()
        {
            foreach (TileObject tileObject in TileObjects)
            {
                tileObject.Destroy();
            }
        }
    }
}
