using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using GamePrototype.Classes.Objects;
using Microsoft.Xna.Framework.Content;


namespace GamePrototype.Classes
{

    class Room
    {
        Texture2D roomBG; // Texture for the room itself, draw this before any of the objects in it
        private Rectangle roomBounds; // The size of the room, will be used for bounds checks
        private List<GameObject> objectsInRoom; // as requested, list format
        Vector2 origin; //added: Delcan //Use: using this to center the texture of the room. I dont want to use the viewport bounds because it would scale the texture based on the window size. Id like to have the bg relatively static and then have the furniture relative to this texture.
        GraphicsDevice graphics;//Added: Declan //Uses: allows referanced to the graphics bounds
        ContentManager content;//Added Delcan //Uses: Allows for the room clas to load the content locally so that the main Game1 doen't become overrun with texture loading
        // private Rectangle viewBounds; // Doesn't necessarily need to be used unless we go for resolution scalability

        // kat testing something
        Texture2D bed; Rectangle bedRect;
        Texture2D tv; Rectangle tvRect;
        Texture2D sidetab1; Rectangle sidetab1Rect;
        Texture2D sidetab2; Rectangle sidetab2Rect;
        Texture2D book; Rectangle bookRect;
        Texture2D dress; Rectangle dressRect;
        Texture2D outdoor; Rectangle outdoorRect;
        Texture2D bathdoor; Rectangle bathdoorRect;
        Texture2D closetdoor; Rectangle closetdoorRect;
        SpriteBatch spriteBatch;
        List<Texture2D> textureList;
        List<Rectangle> rectangleList;

        // TODO: Parameterized constructor, needs to take a collection of GameObjects as a param
        public Room(GraphicsDevice gd, ContentManager ctm)
        {
            content = ctm;
            graphics = gd;
            origin = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
            roomBG = content.Load<Texture2D>("backgroundFULL");
            roomBounds = new Rectangle((int)origin.X - (1382 / 2), (int)origin.Y - (972 / 2), 1382, 972);

            // kat moved from objectsetup
            bed = content.Load<Texture2D>("bedFULL"); bedRect = new Rectangle((int)origin.X + 100, (int)origin.Y - 40, 518, 346);
            tv = content.Load<Texture2D>("tvFULL"); tvRect = new Rectangle((int)origin.X - 570, (int)origin.Y - 110, 172, 346);
            sidetab1 = content.Load<Texture2D>("sidetableFULL"); sidetab1Rect = new Rectangle((int)origin.X + 380, (int)origin.Y + 260, 172, 172);
            sidetab2 = content.Load<Texture2D>("sidetableFULL"); sidetab2Rect = new Rectangle((int)origin.X + 380, (int)origin.Y - 140, 172, 172);
            book = content.Load<Texture2D>("bookshelfFULL"); bookRect = new Rectangle((int)origin.X + 55, (int)origin.Y - 470, 346, 172);
            dress = content.Load<Texture2D>("dresserFULL"); dressRect = new Rectangle((int)origin.X - 440, (int)origin.Y - 470, 346, 172);
            outdoor = content.Load<Texture2D>("outdoorFULL"); outdoorRect = new Rectangle((int)origin.X - 96, (int)origin.Y - 530, 172, 172);
            bathdoor = content.Load<Texture2D>("bathroomdoorFULL"); bathdoorRect = new Rectangle((int)origin.X + 350, (int)origin.Y - 530, 172, 172);
            closetdoor = content.Load<Texture2D>("closetdoorFULL"); closetdoorRect = new Rectangle((int)origin.X - 685, (int)origin.Y + 230, 172, 172);

            // more kat trying things
            textureList = new List<Texture2D>();
            rectangleList = new List<Rectangle>();
            textureList.Add(outdoor); rectangleList.Add(outdoorRect);
            textureList.Add(bathdoor); rectangleList.Add(bathdoorRect);
            textureList.Add(closetdoor); rectangleList.Add(closetdoorRect);
            textureList.Add(tv); rectangleList.Add(tvRect);
            textureList.Add(sidetab2); rectangleList.Add(sidetab2Rect);
            textureList.Add(bed); rectangleList.Add(bedRect);
            textureList.Add(sidetab1); rectangleList.Add(sidetab1Rect);
            textureList.Add(book); rectangleList.Add(bookRect);
            textureList.Add(dress); rectangleList.Add(dressRect);
        }

        // TODO: Initialize method, should be used to tell which objects in the room are in what state after they're all loaded in using the constructor
        public void Initialize()
        {
            
        }

        // TODO: Update function, should go through the array of GameObjects and call all their update functions
        public void Update(GameTime gameTime)
        {
            if (objectsInRoom != null)
            {
                foreach (GameObject obj in objectsInRoom)
                {
                    obj.Update(gameTime);
                }
            }
        }
        // TODO: Kat - Draw method, take a SpriteBatch param and draw any enabled objects in the room by calling their draw methods with it
        public void Draw(SpriteBatch sprtBtch)
        {
            // draws the room texture image/background
            sprtBtch.Draw(roomBG, roomBounds, Color.White);

            // foreach gameobject in the list of the objects in the room
            if (objectsInRoom != null)
            {
                // kat trying a different way
                for (int i = 0; i < objectsInRoom.Count; i++)
                {
                    objectsInRoom[i].Draw(sprtBtch);
                }
            }
        }

        // NOTE: Declan - This was moved from the constructor, so when you initialize the Room(s) make sure you remember
        // to then 
        public List<GameObject> Objects
        {
            get { return objectsInRoom; }
            set { objectsInRoom = value; }
        }
    }
}