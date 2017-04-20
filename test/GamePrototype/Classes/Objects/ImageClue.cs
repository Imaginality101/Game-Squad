using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using GamePrototype;

namespace GamePrototype.Classes
{
    class ImageClue : Clue
    {
        // attributes
        Texture2D fullImage;
        // constructors
        // default
        public ImageClue (string nm, string text) : base(nm, text)
        {
            fullImage = null;
        }
        public ImageClue(string nm, string text, Texture2D img) : base(nm, text)
        {
            fullImage = img;
        }
        // property
        public Texture2D FullImage
        {
            get
            {
                return fullImage;
            }
            set
            {
                FullImage = value;
            }
        }
    }
}
