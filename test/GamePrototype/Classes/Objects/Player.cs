using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
/*Workers: Kat, Tom, Caleb
 * DisasterPiece Games
 * Player Class
 */
namespace GamePrototype.Classes.Objects
{
    enum PlayerDir { FaceDown, WalkDown, FaceUp, WalkUp, FaceLeft, WalkLeft, FaceRight, WalkRight}
    class Player : GameObject, Tools.IAnimated, Tools.IControlled
    {
        KeyboardState kbState;
        KeyboardState prevKbState;
        GameObject flaggedInteractable;
        //private Rectangle[][] animFrames; // source rectangles to be used in drawing the player
        private Rectangle moveBounds;
        private Rectangle playerRect;
        private Vector2 moveQueue;
        private PlayerDir playerDirection; // get direction the player is facing
        private List<Texture2D> walkRightSprites;
        private Texture2D faceRightSprite;
        private Texture2D faceUpSprite;
        private Texture2D faceDownSprite;
        // variables for animation
        double timer = .1;
        int currentFrame = 0;

        // TODO: Player constructor, should take the same sort of information as well as potentially a Menu object. We'd feed the overall Game's Menu into that.
        public Player(Texture2D faceRight, List<Texture2D> walkRight, Texture2D faceUp, Texture2D faceDown, Rectangle bounds, Rectangle pRect):base()
        {
            moveQueue = Vector2.Zero; // initialize moveQueue to a zero vector
            playerDirection = PlayerDir.FaceDown; // start out facing downwards for now
            moveBounds = bounds;
            playerRect = pRect; //<------------------------------------------------------------------------THIS IS WHERE THE PLAYER RECT SIZE IS-------------------------
            faceRightSprite = faceRight;
            walkRightSprites = walkRight;
            faceUpSprite = faceUp;
            faceDownSprite = faceDown;
        }
        // TODO: Update method override, should check player input and movement
        public void Update(GameTime gameTime, List<GameObject> objects)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            CheckInput(); // first get input to adjust movement queueing
            Move(gameTime);
            CheckBounds(moveBounds);
            ChangeDirection();
            BlockCollisions(objects);
            //FlagInteractables(objects.ToArray());

            base.Update(gameTime);
        }
        public void GetFrame()
        {
            // TODO: Kat - Method should figure out which frame rectangle is supposed to be active and pass it to an attribute bounds, which will need a property or accessor method
            // NOTE: As of milestone 2, both this and the LoadFrames method have their basic function fulfilled in other ways. Leaving them here in case we want to clean those methods up a bit, but the intended
            // function of the methods themselves is implemented.
        }

        public void LoadFrames()
        {
            // TODO: Kat - This method will need to be set up differently depending on individual images or spritesheets.
            // If we use sprite sheets we need it to take values for the number of animation sets, the number of frames in each set, and the pixel dimensions of each frame being pulled.
            // With that said, because of how this stuff works (to my knowledge) we don't need to pull the image file itself as part of this. We can just use the player's sprite itself for references.
            // This method shouldn't actually need to touch the Texture2D, we're just using it to set faceUp rects which will be used as sources in Draw() method.
        }

        public void CheckInput()
        {

            // TODO: This method should be called by the update function, this is where keyboard state checking should go.

            moveQueue = Vector2.Zero; // reset moveQueue every update

            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.W))
            {
                moveQueue.Y -= 2;
            }
            if (kbState.IsKeyDown(Keys.S))
            {
                moveQueue.Y += 2;
            }
            if (kbState.IsKeyDown(Keys.A))
            {
                moveQueue.X -= 2;
            }
            if (kbState.IsKeyDown(Keys.D))
            {
                moveQueue.X += 2;
            }
            prevKbState = kbState;
        }

        public void Move(GameTime gameTime)
        {
            playerRect.X += (int)(moveQueue.X); //* (gameTime.ElapsedGameTime.Seconds);
            playerRect.Y += (int)(moveQueue.Y); //* (gameTime.ElapsedGameTime.Seconds);
            //Console.WriteLine(moveQueue.ToString());
            CheckBounds(moveBounds);
        }
        public float CheckProximity(GameObject target)
        {
            // This method will return the length of a vector between the two objects' global origin coordinates
            Vector2 difference = PlayerOrigin - target.SpriteOrigin;
            return Math.Abs(difference.Length());
        }
        // TODO: Change the return type of FlagInteractables back to void
        // NOTE: I guess this works as a way of showing the methodology works for now, but I coded this method originally to register with the closest interactable
        // that it's usable so we can flag it with some visual marker like a button prompt. Absolutely works for the purposes of Milestone II's framework until we have this set up better,
        // but I'd like to migrate this to the original planned setup later on. Also, slightly unrelated and I'm not angry or anything but if 100% of my code for a method is commented out for a build
        // please give me a heads-up in the future. I like to be fully aware at all times what work I can take credit for. - Tom
        public string FlagInteractables(GameObject[] targets)
        {
            GameObject closestGameObj = new GameObject();
            float closestDistance = float.MaxValue;
            foreach (GameObject go in targets)
            {
                //if (!(go is ClueObject))
                //{
                    if ((CheckProximity(go) < closestDistance) && (CheckProximity(go) < 200))
                    {
                        closestDistance = CheckProximity(go);
                        closestGameObj = go;
                    }
                //}
            }
            if (closestDistance == float.MaxValue)
            {
                return "";
            }
            else
            {
                return "You are interacting with: " + closestGameObj.Name;
            }
            /*Interactable closest = null; // use this temporary instance of the object to track which interactable in the room is closest to the player
            float minDistance = moveBounds.Right; // ideally nothing should be interactable beyond the width of how far the player can move so use this as the default
            foreach(GameObject obj in targets)
            {
                if (obj is Interactable) // if the object is usable check its proximity
                {
                    if (minDistance >= CheckProximity(obj)) // looping through this will find which interactable is the closest to the player
                    {
                        minDistance = CheckProximity(obj);
                        closest = (Interactable)obj;
                    }
                    else if (((Interactable)obj).Usable == true)
                    {
                        ((Interactable)obj).Usable = false;
                        flaggedInteractable = null;
                    }
                }
                if (closest != null && minDistance <= ((Sprite.Height / 2) + 20)) // if something was found in a reasonable proximity
                {
                    closest.Usable = true;
                    flaggedInteractable = closest;
                }
            }*/
        }
        public void CheckBounds(Rectangle roomBounds)
        {
            // TODO: This method should check whether or not the player's collision rectangle is entirely inside
            // the accepted param bounds roomBounds. Correct any discrepancies.
            if (playerRect.Left < roomBounds.Left)
            {
                //X += (roomBounds.Left - GlobalBounds.Left + 5);
                BlockLeft();
            }
            else if (playerRect.Right > (roomBounds.Right - playerRect.Width))
            {
                BlockRight();
            }
            
            if (playerRect.Top < roomBounds.Top)
            {
                BlockUp();
            }
            else if (playerRect.Bottom > roomBounds.Bottom)
            {
                BlockDown();
            }

        }
        // Caleb - hopefully will block player and objects
        public void BlockCollisions(List<GameObject> objects)
        {
            foreach (GameObject go in objects)
            {
                Boolean collidingObj = isColliding(go);
                if (collidingObj)
                {
                    if (playerRect.Center.Y < go.GlobalBounds.Center.Y && (playerRect.Right - 2 > go.GlobalBounds.Left && playerRect.Left + 2 < go.GlobalBounds.Right))
                    {
                        BlockDown();
                    }
                    if (playerRect.Center.Y > go.GlobalBounds.Center.Y && (playerRect.Right - 2 > go.GlobalBounds.Left && playerRect.Left + 2 < go.GlobalBounds.Right))
                    {
                        BlockUp();
                    }
                    if (playerRect.Center.X > go.GlobalBounds.Center.X && (playerRect.Bottom > go.GlobalBounds.Top && playerRect.Top < go.GlobalBounds.Bottom))
                    {
                        BlockLeft();
                    }
                    if (playerRect.Center.X < go.GlobalBounds.Center.X && (playerRect.Bottom > go.GlobalBounds.Top && playerRect.Top < go.GlobalBounds.Bottom))
                    {
                        BlockRight();
                    }
                }
            }
        }

        // Caleb - changes the PlayerDir enum based on input
        private void ChangeDirection()
        {
            if (kbState.IsKeyDown(Keys.W) && !prevKbState.IsKeyDown(Keys.W))
            {
                playerDirection = PlayerDir.FaceUp;
            }
            if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyDown(Keys.W))
            {
                playerDirection = PlayerDir.WalkUp;
                //moveBounds = new Rectangle(moveBounds.X, moveBounds.Y - 2, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyDown(Keys.W))
            {
                playerDirection = PlayerDir.FaceUp;
            }
            if (kbState.IsKeyDown(Keys.A) && !prevKbState.IsKeyDown(Keys.A))
            {
                playerDirection = PlayerDir.FaceLeft;
            }
            if (kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyDown(Keys.A))
            {
                playerDirection = PlayerDir.WalkLeft;
                //moveBounds = new Rectangle(moveBounds.X - 2, moveBounds.Y, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyDown(Keys.A))
            {
                playerDirection = PlayerDir.FaceLeft;
            }
            if (kbState.IsKeyDown(Keys.S) && !prevKbState.IsKeyDown(Keys.S))
            {
                playerDirection = PlayerDir.FaceDown;
            }
            if (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyDown(Keys.S))
            {
                playerDirection = PlayerDir.WalkDown;
                //moveBounds = new Rectangle(moveBounds.X, moveBounds.Y + 2, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyDown(Keys.S))
            {
                playerDirection = PlayerDir.FaceDown;
            }
            if (kbState.IsKeyDown(Keys.D) && !prevKbState.IsKeyDown(Keys.D))
            {
                playerDirection = PlayerDir.FaceRight;
            }
            if (kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyDown(Keys.D))
            {
                playerDirection = PlayerDir.WalkRight;
                //moveBounds = new Rectangle(moveBounds.X + 2, moveBounds.Y, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyDown(Keys.D))
            {
                playerDirection = PlayerDir.FaceRight;
            }
            if (playerDirection == PlayerDir.WalkRight && kbState.IsKeyUp(Keys.D))
            {
                playerDirection = PlayerDir.FaceRight;
            }
            if (playerDirection == PlayerDir.WalkLeft && kbState.IsKeyUp(Keys.A))
            {
                playerDirection = PlayerDir.FaceLeft;
            }
        }

        public Boolean isColliding(GameObject target)
        {
            if ((target != this && (playerRect.Intersects(target.GlobalBounds))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2 PlayerOrigin
        {
            get
            {
                return playerRect.Center.ToVector2();
            }
        }

        public Rectangle PlayerRect
        {
            get
            {
                return playerRect;
            }
        }

        // Caleb - methods to block player from moving in the cardinal directions. Useful if collisions end faceUp not being handled by the player class
        // moves player down
        public void BlockUp()
        {
            playerRect = new Rectangle(playerRect.X, playerRect.Y + 2, playerRect.Width, playerRect.Height);
        }
        // moves player up
        public void BlockDown()
        {
            playerRect = new Rectangle(playerRect.X, playerRect.Y - 2, playerRect.Width, playerRect.Height);

        }
        // moves player right
        public void BlockLeft()
        {
            playerRect = new Rectangle(playerRect.X + 2, playerRect.Y, playerRect.Width, playerRect.Height);
        }
        // moves player left
        public void BlockRight()
        {
            playerRect = new Rectangle(playerRect.X - 2, playerRect.Y, playerRect.Width, playerRect.Height);
        }

        public override void Draw(SpriteBatch sprtBtch)
        {
            //base.Draw(sprtBtch);
            if (playerDirection == PlayerDir.FaceUp)
            {
                sprtBtch.Draw(faceUpSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.WalkUp)
            {
                sprtBtch.Draw(faceUpSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.FaceLeft)
            {
                sprtBtch.Draw(faceRightSprite, playerRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
            if (playerDirection == PlayerDir.WalkLeft)
            {
                if (timer == 0 || timer < 0)
                {
                    currentFrame++;
                    timer = .1;
                    if (currentFrame >= walkRightSprites.Count)
                    {
                        currentFrame = 0;
                    }
                }
                sprtBtch.Draw(walkRightSprites[currentFrame], playerRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
            if (playerDirection == PlayerDir.FaceDown)
            {
                sprtBtch.Draw(faceDownSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.WalkDown)
            {
                sprtBtch.Draw(faceDownSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.FaceRight)
            {
                sprtBtch.Draw(faceRightSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.WalkRight)
            {
                if (timer == 0 || timer < 0)
                {
                    currentFrame++;
                    timer = .1;
                    if (currentFrame >= walkRightSprites.Count)
                    {
                        currentFrame = 0;
                    }
                }
                sprtBtch.Draw(walkRightSprites[currentFrame], playerRect, Color.White);
            }
        }

    }
}