using Bunni.Resources.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunni.Resources.Modules
{
    public class Button : Entity
    {
        /*
         * TODO: document, possible refactor
         */ 
        private Action onClick;
        private Action onHover;
        private bool Clicked = false;

        public void OnClick(Action callback)
        {
            onClick = callback;
        }

        public void OnHover(Action callback)
        {
            onHover = callback;
        }

        public override void Update(GameTime gameTime, Scene scene)
        {
            Vector2 mousePos = Camera.GetMouseWorldPosition();
            MouseState mouseState = Mouse.GetState();
            Render rendComp = GetComponent<Render>();
            Render.TransformComponent pos = rendComp.Transform;
            if (!(pos.X > mousePos.X
                    || pos.X + rendComp.Texture.Width < mousePos.X
                    || pos.Y > mousePos.Y
                    || pos.Y + rendComp.Texture.Height < mousePos.Y
                    ))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (!Clicked)
                    {
                        onClick?.Invoke();
                        Clicked = true;
                    }
                }
                else
                {
                    Clicked = false;
                }
            }
            else
            {
                onHover?.Invoke();
            }
        }
    }
}
