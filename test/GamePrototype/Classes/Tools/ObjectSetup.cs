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
        private Texture2D brokenFloor;

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
        private Rectangle brokenFloorRect;



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
        private Rectangle crazyDiary1Rect;
        private Texture2D tdiardy;
        private Rectangle tdiaryRect;
        private Texture2D recept;
        private Rectangle receptRect;
        private Texture2D jewelrybox;
        private Rectangle jewelryboxRect;
        private Texture2D ring;
        private Rectangle ringRect;
        private Texture2D oldPhoto;
        private Rectangle oldPhotoRect;


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
        private Texture2D crazyDiary;
        private Rectangle crazyDiary3Rect;
        private Texture2D knife;
        private Rectangle knifeRect;

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
            brokenFloor = content.Load<Texture2D>("floorboard");

            //Bounds assignments
            bedRect = new Rectangle((int)origin.X + 100, (int)origin.Y - 40, 518, 346);
            tvRect = new Rectangle((int)origin.X - 570, (int)origin.Y - 110, 172, 346);
            sidetab1Rect = new Rectangle((int)origin.X + 380, (int)origin.Y + 260, 172, 172);
            sidetab2Rect = new Rectangle((int)origin.X + 380, (int)origin.Y - 165, 172, 172);
            bookRect = new Rectangle((int)origin.X + 55, (int)origin.Y - 470, 346, 172);
            dressRect = new Rectangle((int)origin.X - 440, (int)origin.Y - 470, 346, 172);
            outdoorRect = new Rectangle((int)origin.X - 96, (int)origin.Y - 515, 172, 172);
            bathdoorRect = new Rectangle((int)origin.X + 370, (int)origin.Y - 530, 172, 172);
            closetdoorRect = new Rectangle((int)origin.X - 685, (int)origin.Y + 230, 172, 172);
            stickynoteRect = new Rectangle((int)origin.X - 490, (int)origin.Y + 130, 56, 56);
            news1Rect = new Rectangle((int)origin.X - 300, (int)origin.Y - 350, 72, 72);
            news2Rect = new Rectangle((int)origin.X + 430, (int)origin.Y - 150, 72, 72);
            lampRect = new Rectangle((int)origin.X - 440, (int)origin.Y - 475, 128, 128);
            //brokenWallRect = new Rectangle((int)origin.X - 512, (int)origin.Y - 547, 72, 72);
            brokenFloorRect = new Rectangle((int)origin.X -323, (int)origin.Y  +316, 172, 172);
            //adding them all to the gameobjectlist
            // Caleb - adding a temporary name string to the constructor which is to demo interaction
            //objs.Add(new GameObject(outdoor, outdoorRect, "End door"));
            objs.Add(new Door(outdoor, outdoorRect, CurrentRoom.WinRoom, new Room(new Texture2D(graphics, 1, 1), new Rectangle(((int)origin.X - (404)) + 150, ((int)origin.Y - (334)) + 0, 768 - 170, 768 - 80)), Clue.Clues["VeryCrazyDiary"]));
            objs.Add(new Door(bathdoor, bathdoorRect, CurrentRoom.Bathroom, doorDestination2,Clue.Clues["BathroomKey"]));
            objs.Add(new Door(closetdoor, closetdoorRect, CurrentRoom.Closet,doorDestination,Clue.Clues["ClosetKey"]));
            objs.Add(new GameObject(tv, tvRect, new Rectangle(0, 100, 172, 250)));
            objs.Add(new GameObject(sidetab2, sidetab2Rect, new Rectangle(5, 50, 172, 102)));
            objs.Add(new ClueObject(bed, bedRect, new Rectangle(0, 100, 512, 226), Clue.Clues["TenantDiary2"], false));//Clue.Clues["TenantDiary2"], "Bed"
            objs.Add(new ClueObject(sidetab1, sidetab1Rect, new Rectangle(0, 0, 172, 172), Clue.Clues["OldPhoto1"], false, .2f));
            objs.Add(new ClueObject(book, bookRect, Clue.Clues["TenantDiary1"], false, Clue.Clues["StickyNote"], .55f));
            objs.Add(new ClueObject(news1, news1Rect, Clue.Clues["News1"], false, "News 1", true, .95f));
            objs.Add(new ClueObject(news1, news2Rect, Clue.Clues["News2"], false, "News 2", true, .2f));
            objs.Add(new ClueObject(dress, dressRect, Clue.Clues["ClosetKey"], false, Clue.Clues["TenantDiary2"]));
            objs.Add(new ClueObject(stickynote, stickynoteRect, Clue.Clues["StickyNote"], false, "Sticky Note",true, .2f));
            objs.Add(new Lamp(lamp, lampRect, .55f));
            //objs.Add(new Door(closetdoor, new Rectangle((int)origin.X - 685, (int)origin.Y - 310, 172, 172), CurrentRoom.Closet, doorDestination));//Cheatdoor
            objs.Add(new ClueObject(brokenFloor, brokenFloorRect,Clue.Clues["VeryCrazyDiary"],false, false, Clue.Clues["CrazyDiary3"], .98f));

            // Setting up interaction points, this is an example on how
            ((ClueObject)objs[5]).InteractionPoint = new Vector2(30, bedRect.Height / 2); // bed
            ((ClueObject)objs[10]).InteractionPoint = new Vector2(3*dressRect.Width/4, dressRect.Height / 2); // dresser
            ((Lamp)objs[12]).InteractionPoint = new Vector2(lampRect.Width/2,lampRect.Height); // Lamp
            ((Door)objs[1]).InteractionPoint = new Vector2((lampRect.Width / 2)+50, lampRect.Height); // door
            ((ClueObject)objs[9]).InteractionPoint = new Vector2(-10, (news2Rect.Height)); // news1



            return objs;
        }
        public List<GameObject> ClosetSetup()
        {
            //Declare what and where each item is 
            cardbord = content.Load<Texture2D>("boxesFULL");
            cardboardRect = new Rectangle((int)origin.X - 560, (int)origin.Y - 200, 172, 172);

            mirror = content.Load<Texture2D>("mirrorFULL");
            mirrorRect = new Rectangle((int)origin.X - 570, (int)origin.Y -50, 172, 172);

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
            
            crazyDiary = content.Load<Texture2D>("Crazy1");
            crazyDiary1Rect = new Rectangle((int)origin.X + 80, (int)origin.Y + 20, 128, 128);

            tdiardy = content.Load<Texture2D>("Diary1");
            tdiaryRect = new Rectangle((int)origin.X + 180, (int)origin.Y - 120, 128, 128);

            recept = content.Load<Texture2D>("recept1");
            receptRect = new Rectangle((int)origin.X - 350, (int)origin.Y - 120, 50, 50);

            //
            jewelrybox = content.Load<Texture2D>("jewelryboxFULL");
            jewelryboxRect = new Rectangle((int)origin.X - 580, (int)origin.Y + 80, 172, 172);

            ring = content.Load<Texture2D>("Ring1");
            ringRect = new Rectangle((int)origin.X - 510, (int)origin.Y + 100, 50, 50);

            news1 = content.Load<Texture2D>("NewspaperFULL");
            news1Rect = new Rectangle((int)origin.X - 170, (int)origin.Y - 110, 72, 72);

            oldPhoto = content.Load<Texture2D>("Photo1");
            oldPhotoRect = new Rectangle(news1Rect.Location + new Point(110, 0), new Point(56, 56));
            List<GameObject> objs = new List<GameObject>();

            //Add items to the room here
            objs.Add(new ClueObject(cardbord, cardboardRect, new Rectangle(0, 0, 172, 172),Clue.Clues["News3"],false));
            //objs.Add(new GameObject(mirror, mirrorRect, "Mirror"));
            objs.Add(new ClueObject(mirror, mirrorRect, Clue.Clues["BathroomKey"], false, Clue.Clues["CrazyDiary2"], .8f));
            objs.Add(new Door(bedroondoorside, bedroomdoorsideRect, CurrentRoom.Bedroom, CurrentRoom.Closet, doorDestination));
            objs.Add(new Lamp(lampfloor, lampfloorRect, .55f));
            objs.Add(new ClueObject(tdiardy, tdiaryRect, Clue.Clues["TenantDiary3"], false, "Sticky Note", true, .98f));
            objs.Add(new GameObject(wardrobeopen, wardrobeopenRect, "WardrobeOpen"));
            objs.Add(new ClueObject(wardrobe, wardrobeRect, new Rectangle(0, 0, 346, 346), Clue.Clues["CrazyDiary2"], false, .55f));
            //objs.Add(new ClueObject(bathkey, bathkeyRect, Clue.Clues["BathroomKey"], false, "Bathroom Key", true));
            objs.Add(new ClueObject(crazyDiary, crazyDiary1Rect, Clue.Clues["CrazyDiary1"], false, "Sticky Note", true, .9f));
            objs.Add(new ClueObject(recept, receptRect, Clue.Clues["Receipt"], false, "Sticky Note", true, .55f));
            objs.Add(new GameObject(jewelrybox, jewelryboxRect, "JewelryBoxOpen"));
            objs.Add(new ClueObject(ring, ringRect, Clue.Clues["Ring"], false, "Sticky Note", true, .2f));
            objs.Add(new ClueObject(news1, news1Rect, Clue.Clues["News4"], true, .55f));
            objs.Add(new ClueObject(oldPhoto, oldPhotoRect, Clue.Clues["OldPhoto2"], true, .55f));



            //Interaction overrides
            ((Lamp)objs[3]).InteractionPoint = new Vector2(lampfloorRect.Width / 2, lampfloorRect.Height); // Lamp
            ((ClueObject)objs[6]).InteractionPoint = new Vector2(wardrobeRect.Width / 2, 3*(wardrobeRect.Height/4)); // wardrobe


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
            crazyDiary3Rect = new Rectangle(bathtableRect.Left, bathtableRect.Top + crazyDiary3Rect.Height + 50/ 2, 128, 128);
            knife = content.Load<Texture2D>("knife");
            knifeRect = new Rectangle(origin.ToPoint(), new Point(100, 100));
            //Add items to the room here
            //objs.Add(new GameObject(tub, tubRect));
            objs.Add(new ClueObject(tub, tubRect, Clue.Clues["Bones"]));
            //objs.Add(new GameObject(toilet, toiletRect));
            objs.Add(new ClueObject(toilet, toiletRect, Clue.Clues["News5"]));
            //objs.Add(new GameObject(sink, sinkRect, new Rectangle(0, 60, 172, 122)));
            objs.Add(new ClueObject(sink, sinkRect, new Rectangle(0, 60, 172, 122), Clue.Clues["Pendant"], false));
            //objs.Add(new GameObject(wastebin, wastebinRect));
            objs.Add(new ClueObject(wastebin, wastebinRect, Clue.Clues["OldPhoto3"]));
            //objs.Add(new GameObject(medcab, medcabRect,new Rectangle(0, 60, 60, 172)));
            objs.Add(new ClueObject(medcab, medcabRect, Clue.Clues["MedicineBottle"], .25f));
            objs.Add(new GameObject(bathtable, bathtableRect, new Rectangle(0, 105, 172, 220)));
            //objs.Add(new GameObject(bathtable, bathtableRect));
            objs.Add(new Door(bathtobeddoor, bathtobeddoorRect,CurrentRoom.Bedroom, CurrentRoom.Bathroom, doorDestination, new Rectangle(0,60,172,60)));
            objs.Add(new ClueObject(crazyDiary, crazyDiary3Rect, Clue.Clues["CrazyDiary3"], false, true, .8f));
            objs.Add(new ClueObject(knife, knifeRect, Clue.Clues["JaggedKnife"], false, true, .9f));
            //Interaction overrides
            ((ClueObject)objs[2]).InteractionPoint = new Vector2(sinkRect.Width, sinkRect.Height / 2);
            ((ClueObject)objs[4]).InteractionPoint = new Vector2(medcabRect.Width + sinkRect.Width / 4, medcabRect.Height / 2);
            //((ClueObject)objs[5]).InteractionPoint = new Vector2(0, bathtableRect.Height / 2);
            return objs;

        }
    }
}