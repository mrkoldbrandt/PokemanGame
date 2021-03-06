using EnumBuilder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PokeMan
{
    public class Player : Component // Static maybe
    {
        public SpriteAnimation[] Animations;
        private int animationIndex;
        private PlayerData data = new PlayerData();
        private KeyboardState lastState;
        private Rectangle rectangle;
        private uint frameCounter;
        private int baseAnimationIndex;
        private long animationLength;

        public PokeMan[] Party { get => data.Party; }

        public Vector2 Position
        {
            get => new Vector2(data.Position.x, data.Position.y);
            set
            {
                data.Position.x = (int)value.X;
                data.Position.y = (int)value.Y;
            }
        }

        
        public Player(int size)
        {
            rectangle = new Rectangle(PokeManGame.SceenSize.x / 2, PokeManGame.SceenSize.y / 2, size, size);
            data.Party = new PokeMan[6];
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animations[animationIndex], rectangle, Color.White);

            if (frameCounter++ >= animationLength)
                animationIndex = baseAnimationIndex;
        }

        public void LoadAssets(ContentManager contMan, string xmlPath)
        {
            //Loads animations sprites based off xml doc
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../Content/Xml/" + xmlPath);

            var node = doc.DocumentElement.SelectSingleNode("/Character/Animations");

            Animations = new SpriteAnimation[node.ChildNodes.Count];

            int i = 0;
            foreach (XmlNode n in node.SelectNodes("Animation"))
            {
                int j = 0;
                var t = new Texture2D[n.ChildNodes.Count];

                foreach (XmlNode m in n.SelectNodes("Frame"))
                {
                    t[j++] = contMan.Load<Texture2D>($"{n.Attributes["path"].Value}{m.InnerText}");
                }
                Animations[i++] = new SpriteAnimation(t, uint.Parse(n.Attributes["inverseSpeed"].Value));
            }
        }

        #region Animation

        
        public void PlaySingleAnimation(int index, bool restart = false)
        {
            frameCounter = 0;
            animationIndex = index;
            animationLength = Animations[animationIndex].Length * Animations[animationIndex].InverseSpeed;
            if (restart)
                Animations[index].Restart();
        }

        public void PlaySingleAnimation(PlayerAnimationEnums animation, bool restart = false)
        {
            PlaySingleAnimation((int)animation, restart);
        }

        public void ChangeAnimation(int index, bool restart = false)
        {
            baseAnimationIndex = index;
            if (restart)
                Animations[index].Restart();
        }

        public void ChangeAnimation(PlayerAnimationEnums animation, bool restart = false)
        {
            ChangeAnimation((int)animation, restart);
        }

        public void PlayAnimationFor(int index, int frames, bool restart = false)
        {
            frameCounter = 0;
            animationIndex = index;
            animationLength = frames;
            if (restart)
                Animations[index].Restart();
        }

        public void PlayAnimationFor(PlayerAnimationEnums animation, int frames, bool restart = false)
        {
            PlayAnimationFor((int)animation, frames, restart);
        }

        #endregion Animation

        /// <summary>
        /// Adds a pokeman to the party (a array)
        /// </summary>
        /// <param name="pokeMan"></param>
        /// <returns></returns>
        public bool AddPokeMan(PokeMan pokeMan)
        {
            for (int i = 0; i < data.Party.Length; i++)

                if (data.Party[i] == null)
                {
                    data.Party[i] = pokeMan;
                    return true;
                }
            return false;
        }

        public override void Update()
        {
        }
    }

    [Serializable]
    public class PlayerData
    {
        public (int x, int y) Position;
        public string Name;
        public PokeMan[] Party;
    }
}