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

        //Texture2Ds                           //Rectangles;
        Texture2D bed;                         Rectangle bedRect;
        Texture2D tv;                            Rectangle tvRect;
        Texture2D sidetab1;                  Rectangle sidetab1Rect;
        Texture2D sidetab2;                 Rectangle sidetab2Rect;
        Texture2D book;                       Rectangle bookRect;
        Texture2D dress;                      Rectangle dressRect;
        Texture2D outdoor;                   Rectangle outdoorRect;
        Texture2D bathdoor;                Rectangle bathdoorRect;
        Texture2D closetdoor;              Rectangle closetdoorRect;
        Texture2D stickynote;             Rectangle stickynoteRect;
        Texture2D news1;                    Rectangle news1Rect;
        //fields to hold the constuctor stuff
        ContentManager content;
        SpriteBatch spriteBatch;
        GraphicsDevice graphics;
        //draw origin
        Vector2 origin;

        // constructor to take content manager, spritebatch and a graphic device to handle local contect assignments
        public ObjectSetup(ContentManager ctm, SpriteBatch sbt, GraphicsDevice gd)
        {
            content = ctm;
            spriteBatch = sbt;
            graphics = gd;
            origin = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);

        }
        public List<GameObject> BedroomSetup()
        {
            // GameObject List for return
            List<GameObject> objs = new List<GameObject>();

            //texterure assignments                                                                      //Bounds assignments
            bed = content.Load<Texture2D>("bedFULL");                                       bedRect = new Rectangle((int)origin.X + 100, (int)origin.Y - 40, 518, 346);
            tv = content.Load<Texture2D>("tvFULL");                                             tvRect = new Rectangle((int)origin.X - 570, (int)origin.Y - 110, 172, 346);
            sidetab1 = content.Load<Texture2D>("sidetableFULL");                       sidetab1Rect = new Rectangle((int)origin.X + 380, (int)origin.Y + 260, 172, 172);
            sidetab2 = content.Load<Texture2D>("sidetableFULL");                       sidetab2Rect = new Rectangle((int)origin.X + 380, (int)origin.Y - 140, 172, 172);
            book = content.Load<Texture2D>("bookshelfFULL");                            bookRect = new Rectangle((int)origin.X + 55, (int)origin.Y - 470, 346, 172);
            dress = content.Load<Texture2D>("dresserFULL");                              dressRect = new Rectangle((int)origin.X - 440, (int)origin.Y - 470, 346, 172);
            outdoor = content.Load<Texture2D>("outdoorFULL");                          outdoorRect = new Rectangle((int)origin.X - 96, (int)origin.Y - 530, 172, 172);
            bathdoor = content.Load<Texture2D>("bathroomdoorFULL");              bathdoorRect = new Rectangle((int)origin.X + 350, (int)origin.Y - 530, 172, 172);
            closetdoor = content.Load<Texture2D>("closetdoorFULL");                 closetdoorRect = new Rectangle((int)origin.X - 685, (int)origin.Y + 230, 172, 172);
            stickynote = content.Load<Texture2D>("stickynoteFull");                    stickynoteRect = new Rectangle((int)origin.X - 490,(int)origin.Y +130, 56,56);
            news1 = content.Load<Texture2D>("NewspaperFULL");                        news1Rect = new Rectangle((int)origin.X - 300, (int)origin.Y - 350, 72, 72);

            //adding them all to the gameobjectlist
            // Caleb - adding a temporary name string to the constructor which is to demo interaction
            objs.Add(new GameObject(outdoor, outdoorRect, "End door"));
            objs.Add(new GameObject(bathdoor, bathdoorRect, "Bathroom door"));
            objs.Add(new GameObject(closetdoor, closetdoorRect, "Closet door"));
            objs.Add(new GameObject(tv, tvRect, "TV"));
            objs.Add(new ClueObject(sidetab2, sidetab2Rect, Clue.Clues["News2"], "Side table 2"));
            objs.Add(new ClueObject(bed, bedRect, Clue.Clues["TenantDiary2"], "Bed"));
            objs.Add(new ClueObject(sidetab1, sidetab1Rect, Clue.Clues["OldPhoto1"], "Side table 1"));
            objs.Add(new ClueObject(book, bookRect,Clue.Clues["TenantDiary1"], "Book"));
            objs.Add(new ClueObject(news1, news1Rect, Clue.Clues["News1"], false, "News 1"));
            objs.Add(new GameObject(dress, dressRect, "Dresser"));
            objs.Add(new ClueObject(stickynote, stickynoteRect, Clue.Clues["StickyNote"], false, "Sticky Note"));
            return objs;
        }
    }
}
