using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeMan
{
    internal interface IDisplayable
    {
        public void Draw(SpriteBatch spriteBatch);
    }
}