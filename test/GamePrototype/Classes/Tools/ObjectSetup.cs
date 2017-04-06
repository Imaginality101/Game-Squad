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
/*Workers: Declan, Tom, Caleb
 * DisasterPiece Games
 * ObjectSetup Class
 */
namespace GamePrototype.Classes.Tools
{
    class ObjectSetup
    {
        // TODO: Declan - You can use this for initializing game objects

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
        //method to setup the objects int the bedroom it returns a list of gameobjects
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
            objs.Add(new GameObject(tv, tvRect,new Rectangle(0,100,172,250)));
            objs.Add(new ClueObject(sidetab2, sidetab2Rect, new Rectangle(0, 70, 172, 172),Clue.Clues["News2"],false));
            objs.Add(new ClueObject(bed, bedRect,new Rectangle(0,100,512,226), Clue.Clues["TenantDiary2"],false));//Clue.Clues["TenantDiary2"], "Bed"
            objs.Add(new ClueObject(sidetab1, sidetab1Rect, Clue.Clues["OldPhoto1"], "Side table 1",false));
            objs.Add(new ClueObject(book, bookRect,Clue.Clues["TenantDiary1"], "Book",false, Clue.Clues["StickyNote"]));
            objs.Add(new ClueObject(news1, news1Rect, Clue.Clues["News1"], false, "News 1",true));
            objs.Add(new GameObject(dress, dressRect, "Dresser"));
            objs.Add(new ClueObject(stickynote, stickynoteRect, Clue.Clues["StickyNote"], false, "Sticky Note",true));

            // Setting up interaction points, this is an example on how
            ((ClueObject)objs[5]).InteractionPoint = new Vector2(30, bedRect.Height / 2); // bed
            return objs;
        }
    }
}
