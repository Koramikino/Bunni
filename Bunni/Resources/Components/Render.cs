﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunni
{
    public class Render : Component
    {
        public Texture2D texture { get; set; }
        public Color color { get; set; }

        public Render(Entity _parent, Texture2D _texture) : base(_parent, ComponentType.Render)
        {
            texture = _texture;
            color = Color.White;
        }

        public PositionVector getPosition()
        {
            return parent.getProperty(PropertyType.PositionVector) as PositionVector;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, getPosition().Position, color);
        }

    }
}
