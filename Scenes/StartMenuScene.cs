using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace PokeMan
{
    public class StartMenuScene : Scene
    {
        private List<Component> _components;

        private Texture2D background;
        private Texture2D buttonTexture;
        private SpriteFont font;

        public StartMenuScene()
        {
            background = Content.Load<Texture2D>("Assets/StartMenu/StartMenuBG");
            font = PokeManGame.Font;
            buttonTexture = PokeManGame.ButtonTexture;

            var startGameButton = new Button(buttonTexture, font, text: "Start Game", position: new Point(100, 800), width: 250);
            startGameButton.Click += StartGameButton_Click;

            _components = new List<Component>()
            {
                startGameButton
            };
        }

        /// <summary>
        /// Starts the game by removing the top layer of stack (this)
        /// (button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, Vector2.Zero, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);

            spriteBatch.DrawString(font, "Welcome to the world of PokeMan!", new Vector2(50, 50), Color.White);

            foreach (var component in _components)
            {
                component.Draw(spriteBatch);
            }
        }

        


        public override void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }
    }
}