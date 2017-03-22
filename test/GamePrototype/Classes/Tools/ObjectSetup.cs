using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePrototype.Classes.Objects;
using GamePrototype.Classes;
using GamePrototype.Classes.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GamePrototype.Classes.Tools
{
    class ObjectSetup
    {
        // TODO: Declan - You can use this for initializing. I'll give some recommendations but largely leave decisions on methodology up to you.
        // Right now this is a static class (quick refresher, can't be instantiated), since we don't really need to initialize
        // it for functionality. What I'd recommend doing is setting up one method for each room we add 
        // (you could even do a property if you wanted, that'd be a really long property though)
        // and setting it up to return a list of GameObjects specifically tailored to that room's needs. Then just call that when instantiating
        // a room.
        // You could also set it up like TryParse, with a Room as a parameter with the keyword out in front.
        // Obviously you can also just get rid of the static identifier if you need to.

        // As an important other note, you'll need a way to get all the Texture2Ds you need for the objects. I'd recommend working with Kat,
        // get a dictionary up and running of Texture2D's with a string key
        Texture2D bed;                         Rectangle bedRect;
        Texture2D tv;                            Rectangle tvRect;
        Texture2D sidetab1;                  Rectangle sidetab1Rect;
        Texture2D sidetab2;                 Rectangle sidetab2Rect;
        Texture2D book;                       Rectangle bookRect;
        Texture2D dress;                      Rectangle dressRect;
        Texture2D outdoor;                   Rectangle outdoorRect;
        Texture2D bathdoor;                Rectangle bathdoorRect;
        Texture2D closetdoor;              Rectangle closterdoorRect;
        //Texture2D background;             Rectangle backgroundRect;

        ContentManager content;
        SpriteBatch spriteBatch;
        GraphicsDevice graphics;
        Vector2 origin;
        Dictionary<Texture2D, Rectangle> furnitureDict;
        // Texture2D bed, Texture2D tv, Texture2D sidetab1, Texture2D sidetab2, Texture2D book, Texture2D dress, Texture2D outdoor, Texture2D bathdoor, Texture2D closetdoor
        public ObjectSetup(ContentManager ctm, SpriteBatch sbt, GraphicsDevice gd)
        {
            content = ctm;
            spriteBatch = sbt;
            graphics = gd;
            origin = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
            furnitureDict = new Dictionary<Texture2D, Rectangle>();

        }
        public List<GameObject> BedroomSetup()
        {
            // GameObject List for return
            List<GameObject> objs = new List<GameObject>();

            bed = content.Load<Texture2D>("bedFULL");                                       bedRect = new Rectangle((int)origin.X + 100, (int)origin.Y - 40, 518, 346);
            tv = content.Load<Texture2D>("tvFULL");                                             tvRect = new Rectangle((int)origin.X - 570, (int)origin.Y - 110, 172, 346);
            sidetab1 = content.Load<Texture2D>("sidetableFULL");                       sidetab1Rect = new Rectangle((int)origin.X + 380, (int)origin.Y + 260, 172, 172);
            sidetab2 = content.Load<Texture2D>("sidetableFULL");                       sidetab2Rect = new Rectangle((int)origin.X + 380, (int)origin.Y - 140, 172, 172);
            book = content.Load<Texture2D>("bookshelfFULL");                            bookRect = new Rectangle((int)origin.X + 55, (int)origin.Y - 470, 346, 172);
            dress = content.Load<Texture2D>("dresserFULL");                              dressRect = new Rectangle((int)origin.X - 440, (int)origin.Y - 470, 346, 172);
            outdoor = content.Load<Texture2D>("outdoorFULL");                          outdoorRect = new Rectangle((int)origin.X - 96, (int)origin.Y - 530, 172, 172);
            bathdoor = content.Load<Texture2D>("bathroomdoorFULL");              bathdoorRect = new Rectangle((int)origin.X + 350, (int)origin.Y - 530, 172, 172);
            closetdoor = content.Load<Texture2D>("closetdoorFULL");                 closterdoorRect = new Rectangle((int)origin.X - 685, (int)origin.Y + 230, 172, 172);
            //background = content.Load<Texture2D>("backgroundFULL");               backgroundRect = new Rectangle((int)origin.X - (1382 / 2), (int)origin.Y - (972 / 2), 1382, 972);

            objs.Add(new GameObject(bed, bedRect));

            return objs;
        }
        public void DrawBedroom()
        {
            foreach (KeyValuePair<Texture2D,Rectangle> furn in furnitureDict)
            {
                spriteBatch.Draw(furn.Key, furn.Value, Color.White);
            }
            /*
            spriteBatch.Draw(sidetab2, sidetab2Rect, Color.White);
            spriteBatch.Draw(bed, bedRect, Color.White);
            spriteBatch.Draw(dress, dressRect, Color.White);
            spriteBatch.Draw(outdoor, outdoorRect, Color.White);
            spriteBatch.Draw(bathdoor, bathdoorRect, Color.White);
            spriteBatch.Draw(book, bookRect, Color.White);
            spriteBatch.Draw(tv, tvRect, new Color(255, 255, 255, 255));
            spriteBatch.Draw(sidetab1, sidetab1Rect, Color.White);
            spriteBatch.Draw(closetdoor, closterdoorRect, Color.White);
            */
        }

    }
}
