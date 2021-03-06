using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace PokeMan
{
    public class Button : Component
    {
        private Texture2D _texture;
        private SpriteFont _font;

        private MouseState _previousMouse;
        private MouseState _currentMouse;

        private bool _isHovering;

        public event EventHandler Click;

        public Color PenColor { get; set; }
        public Color SpriteColor { get; set; }
        public Color HoverColor { get; set; }
        public string Text { get; set; }

        public Rectangle Rectangle;

        // Constructor for button

        public Button(Texture2D texture, SpriteFont font, Rectangle rectangle, string text = "", Color? penColour = null, Color? spriteColor = null, Color? hoverColor = null)
        {
            Text = text;
            _texture = texture;
            _font = font;
            PenColor = penColour ?? Color.Black;
            SpriteColor = spriteColor ?? Color.White;
            HoverColor = hoverColor ?? Color.Gray;

            Rectangle = rectangle;
        }

        public Button(
            Texture2D texture, SpriteFont font, Point? position = null, int width = 100, int height = 50, string text = "", Color? penColour = null, Color? spriteColor = null, Color? hoverColor = null)
            : this(texture, font, new Rectangle(position ?? Point.Zero, new Point(width, height)), text, penColour, spriteColor, hoverColor) { }


        public override void Draw(SpriteBatch spriteBatch)
        {
            // Resets from hover colour
            var colour = SpriteColor;
            // changes colour when mouse is hovering
            if (_isHovering)
                colour = HoverColor;

            // draws the button outline
            spriteBatch.Draw(_texture, Rectangle, colour);

            // Draws the buttons label and sets it in the middle of the button, if the string is not empty or null
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            //defines the mouse size
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            // continusly resets hovering to false
            _isHovering = false;

            // if mouse is hovering over button
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                //  Executes the buttons code when pressed and checks for click)
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());

                    //same as this:
                    //if (Click != null)
                    //{
                    //    Click(this, new EventArgs());
                    //}
                }
            }
        }
    }
}