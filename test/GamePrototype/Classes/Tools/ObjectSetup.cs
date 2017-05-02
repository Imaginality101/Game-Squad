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

        //Texture2Ds                           
        private Texture2D bed;
        private Texture2D tv;
        private Texture2D sidetab1;
        private Texture2D sidetab2;
        private Texture2D book;
        private Texture2D dress;
        private Texture2D outdoor;
        private Texture2D bathdoor;
        private Texture2D closetdoor;
        private Texture2D stickynote;
        private Texture2D news1;
        private Texture2D lamp;
        private Texture2D crazyDiary;

        //Rectangles;
        private Rectangle bedRect;
        private Rectangle tvRect;
        private Rectangle sidetab1Rect;
        private Rectangle sidetab2Rect;
        private Rectangle bookRect;
        private Rectangle dressRect;
        private Rectangle outdoorRect;
        private Rectangle bathdoorRect;
        private Rectangle closetdoorRect;
        private Rectangle stickynoteRect;
        private Rectangle news1Rect;
        private Rectangle news2Rect;
        private Rectangle lampRect;
        private Rectangle crazyDiaryRect;
        
        
        // Texture for display above interactables
        public static Texture2D buttonPrompt;

        //fields to hold the constuctor stuff
        ContentManager content;
        SpriteBatch spriteBatch;
        GraphicsDevice graphics;

        Room doorDestination;
        Room doorDestination2;


        //draw origin
        Vector2 origin;

        //closet objects
        private Texture2D wardrobe;
        private Rectangle wardrobeRect;
        private Texture2D wardrobeopen;
        private Rectangle wardrobeopenRect;
        private Texture2D bathkey;
        private Rectangle bathkeyRect;
        private Texture2D lampfloor;
        private Rectangle lampfloorRect;
        private Texture2D cardbord;
        private Rectangle cardboardRect;
        private Texture2D mirror;
        private Rectangle mirrorRect;
        private Texture2D bedroondoorside;
        private Rectangle bedroomdoorsideRect;

        //Bathroom objects
        private Texture2D tub;
        private Rectangle tubRect;
        private Texture2D toilet;
        private Rectangle toiletRect;
        private Texture2D sink;
        private Rectangle sinkRect;
        private Texture2D wastebin;
        private Rectangle wastebinRect;
        private Texture2D medcab;
        private Rectangle medcabRect;
        private Texture2D bathtable;
        private Rectangle bathtableRect;
        private Texture2D bathtobeddoor;
        private Rectangle bathtobeddoorRect;

        // constructor to take content manager, spritebatch and a graphic device to handle local contect assignments
        public ObjectSetup(ContentManager ctm, SpriteBatch sbt, GraphicsDevice gd,Room doorDest,Room doorDest2)
        {
            content = ctm;
            spriteBatch = sbt;
            graphics = gd;
            origin = new Vector2(1728 / 2, 972 / 2);
            doorDestination = doorDest;
            doorDestination2 = doorDest2;
            buttonPrompt = content.Load<Texture2D>("ebut64x64");
        }
        //method to setup the objects int the bedroom it returns a list of gameobjects
        public List<GameObject> BedroomSetup()
        {
            // GameObject List for return
            List<GameObject> objs = new List<GameObject>();

            //texterure assignments                                                                     
            bed = content.Load<Texture2D>("bedFULL"); 
            tv = content.Load<Texture2D>("tvFULL"); 
            sidetab1 = content.Load<Texture2D>("sidetableFULL");
            sidetab2 = content.Load<Texture2D>("sidetableFULL"); 
            book = content.Load<Texture2D>("bookshelfFULL"); 
            dress = content.Load<Texture2D>("dresserFULL"); 
            outdoor = content.Load<Texture2D>("outdoorFULL"); 
            bathdoor = content.Load<Texture2D>("bathroomdoorFULL"); 
            closetdoor = content.Load<Texture2D>("closetdoorFULL");
            stickynote = content.Load<Texture2D>("stickynoteFull");
            news1 = content.Load<Texture2D>("NewspaperFULL"); 
            lamp = content.Load<Texture2D>("LampFULL");

            //Bounds assignments
            bedRect = new Rectangle((int)origin.X + 100, (int)origin.Y - 40, 518, 346);
            tvRect = new Rectangle((int)origin.X - 570, (int)origin.Y - 110, 172, 346);
            sidetab1Rect = new Rectangle((int)origin.X + 380, (int)origin.Y + 260, 172, 172);
            sidetab2Rect = new Rectangle((int)origin.X + 380, (int)origin.Y - 140, 172, 172);
            bookRect = new Rectangle((int)origin.X + 55, (int)origin.Y - 470, 346, 172);
            dressRect = new Rectangle((int)origin.X - 440, (int)origin.Y - 470, 346, 172);
            outdoorRect = new Rectangle((int)origin.X - 96, (int)origin.Y - 530, 172, 172);
            bathdoorRect = new Rectangle((int)origin.X + 370, (int)origin.Y - 530, 172, 172);
            closetdoorRect = new Rectangle((int)origin.X - 685, (int)origin.Y + 230, 172, 172);
            stickynoteRect = new Rectangle((int)origin.X - 490, (int)origin.Y + 130, 56, 56);
            news1Rect = new Rectangle((int)origin.X - 300, (int)origin.Y - 350, 72, 72);
            news2Rect = new Rectangle((int)origin.X + 440, (int)origin.Y - 120, 72, 72);
            lampRect = new Rectangle((int)origin.X - 440, (int)origin.Y - 475, 128, 128);

            //adding them all to the gameobjectlist
            // Caleb - adding a temporary name string to the constructor which is to demo interaction
            objs.Add(new GameObject(outdoor, outdoorRect, "End door"));
            objs.Add(new Door(bathdoor, bathdoorRect, CurrentRoom.Bathroom, doorDestination2,Clue.Clues["BathroomKey"]));
            objs.Add(new Door(closetdoor, closetdoorRect, CurrentRoom.Closet,doorDestination,Clue.Clues["ClosetKey"]));
            objs.Add(new GameObject(tv, tvRect, new Rectangle(0, 100, 172, 250)));
            objs.Add(new GameObject(sidetab2, sidetab2Rect, new Rectangle(5, 50, 172, 102)));
            objs.Add(new ClueObject(bed, bedRect, new Rectangle(0, 100, 512, 226), Clue.Clues["TenantDiary2"], false));//Clue.Clues["TenantDiary2"], "Bed"
            objs.Add(new ClueObject(sidetab1, sidetab1Rect, new Rectangle(0, 0, 172, 172), Clue.Clues["OldPhoto1"], false));
            objs.Add(new ClueObject(book, bookRect, Clue.Clues["TenantDiary1"], false, Clue.Clues["StickyNote"]));
            objs.Add(new ClueObject(news1, news1Rect, Clue.Clues["News1"], false, "News 1", true));
            objs.Add(new ClueObject(news1, news2Rect, Clue.Clues["News2"], false, "News 2", true));
            objs.Add(new ClueObject(dress, dressRect, Clue.Clues["ClosetKey"], false, Clue.Clues["TenantDiary2"]));
            objs.Add(new ClueObject(stickynote, stickynoteRect, Clue.Clues["StickyNote"], false, "Sticky Note",true));
            objs.Add(new Lamp(lamp, lampRect));
            objs.Add(new Door(closetdoor, new Rectangle((int)origin.X - 685, (int)origin.Y - 310, 172, 172), CurrentRoom.Closet, doorDestination));//Cheatdoor


            // Setting up interaction points, this is an example on how
            ((ClueObject)objs[5]).InteractionPoint = new Vector2(30, bedRect.Height / 2); // bed
            ((ClueObject)objs[10]).InteractionPoint = new Vector2(330, dressRect.Height / 2); // dresser
            ((Lamp)objs[12]).InteractionPoint = new Vector2(lampRect.Width/2,lampRect.Height); // Lamp
            ((Door)objs[1]).InteractionPoint = new Vector2((lampRect.Width / 2)+50, lampRect.Height); // Lamp


            return objs;
        }
        public List<GameObject> ClosetSetup()
        {
            //Declare what and where each item is 
            cardbord = content.Load<Texture2D>("boxesFULL");
            cardboardRect = new Rectangle((int)origin.X - 560, (int)origin.Y - 200, 172, 172);

            mirror = content.Load<Texture2D>("mirrorFULL");
            mirrorRect = new Rectangle((int)origin.X - 570, (int)origin.Y -26, 172, 172);

            bedroondoorside = content.Load<Texture2D>("bedroomdoorSideFULL");
            bedroomdoorsideRect = new Rectangle((int)origin.X + 495, (int)origin.Y + 50, 172, 172);

            lampfloor = content.Load<Texture2D>("lampFloor");
            lampfloorRect = new Rectangle((int)origin.X + 410, (int)origin.Y - 360, 172, 324);

            wardrobe = content.Load<Texture2D>("wardobeFULL");
            wardrobeRect = new Rectangle((int)origin.X +100, (int)origin.Y - 370, 346, 346);

            wardrobeopen = content.Load<Texture2D>("wardobeOpenFULL");
            wardrobeopenRect = new Rectangle((int)origin.X -320, (int)origin.Y - 370, 346, 346);

            bathkey = content.Load<Texture2D>("key1");
            bathkeyRect  = new Rectangle((int)origin.X - 170, (int)origin.Y - 110, 56, 56);


            List<GameObject> objs = new List<GameObject>();

            //Add items to the room here
            objs.Add(new ClueObject(cardbord, cardboardRect, new Rectangle(0, 0, 172, 172),Clue.Clues["News3"],false));
            objs.Add(new GameObject(mirror, mirrorRect, "Mirror"));
            objs.Add(new Door(bedroondoorside, bedroomdoorsideRect, CurrentRoom.Bedroom,doorDestination));
            objs.Add(new Lamp(lampfloor, lampfloorRect));
            objs.Add(new GameObject(wardrobeopen, wardrobeopenRect, "WardrobeOpen"));
            objs.Add(new ClueObject(wardrobe, wardrobeRect, new Rectangle(0, 0, 346, 346), Clue.Clues["TenantDiary3"], false));
            objs.Add(new ClueObject(bathkey, bathkeyRect, Clue.Clues["BathroomKey"], false, "Bathroom Key", true));

            //Interaction overrides
            ((Lamp)objs[3]).InteractionPoint = new Vector2(lampfloorRect.Width / 2, lampfloorRect.Height); // Lamp
            ((ClueObject)objs[5]).InteractionPoint = new Vector2(wardrobeRect.Width / 2, 3*(wardrobeRect.Height/4)); // wardrobe


            return objs;
        }
        public List<GameObject> BathroomSetup()
        {
            List<GameObject> objs = new List<GameObject>();
            //Declare what and where each item is 
            tub = content.Load<Texture2D>("tubberwareFULL");
            tubRect = new Rectangle((int)origin.X - 100, (int)origin.Y - 300, 346, 172);
            toilet = content.Load<Texture2D>("toiletFULL");
            toiletRect = new Rectangle((int)origin.X - 315, (int)origin.Y - 280, 172, 172);
            sink = content.Load<Texture2D>("sinkFULL");
            sinkRect = new Rectangle((int)origin.X - 315, (int)origin.Y - 0, 172, 172);
            wastebin = content.Load<Texture2D>("wastebinFULL");
            wastebinRect = new Rectangle((int)origin.X - 250, (int)origin.Y + 160, 64, 64);
            medcab = content.Load<Texture2D>("medicinecabFULL");
            medcabRect = new Rectangle((int)origin.X - 395, (int)origin.Y - 50, 172, 172);
            bathtable = content.Load<Texture2D>("bathtableFULL");
            bathtableRect = new Rectangle((int)origin.X + 130, (int)origin.Y - 100, 172, 346);
            bathtobeddoor = content.Load<Texture2D>("bathtobeddoorFULL");
            bathtobeddoorRect = new Rectangle((int)origin.X + 00, (int)origin.Y + 300, 172, 172);
            crazyDiary = content.Load<Texture2D>("Crazy1");
            crazyDiaryRect = new Rectangle((int)origin.X + 130 + 40, (int)origin.Y - 100 + 120, 72, 72);

            //Add items to the room here
            //objs.Add(new GameObject(tub, tubRect));
            objs.Add(new ClueObject(tub, tubRect, Clue.Clues["Bones"]));
            //objs.Add(new GameObject(toilet, toiletRect));
            objs.Add(new ClueObject(toilet, toiletRect, Clue.Clues["Pendant"]));
            objs.Add(new GameObject(sink, sinkRect, new Rectangle(0, 60, 172, 122)));
            objs.Add(new GameObject(wastebin, wastebinRect));
            //objs.Add(new GameObject(medcab, medcabRect,new Rectangle(0, 60, 60, 172)));
            objs.Add(new ClueObject(medcab, medcabRect, Clue.Clues["MedicineBottle"]));
            //objs.Add(new GameObject(bathtable, bathtableRect, new Rectangle(0, 100, 172, 250)));
            objs.Add(new ClueObject(bathtable, bathtableRect, Clue.Clues["News5"]));
            objs.Add(new Door(bathtobeddoor, bathtobeddoorRect,CurrentRoom.Bedroom, doorDestination, new Rectangle(0,60,172,60)));
            objs.Add(new ClueObject(crazyDiary, crazyDiaryRect, Clue.Clues["CrazyDiary3"], false, true));

            //Interaction overrides
            ((ClueObject)objs[4]).InteractionPoint = new Vector2(medcabRect.Width + sinkRect.Width / 4, medcabRect.Height / 2);
            ((ClueObject)objs[5]).InteractionPoint = new Vector2(0, bathtableRect.Height / 2);
            return objs;

        }
    }
}
