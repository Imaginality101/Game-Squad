using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
/*Workers: Kat, Tom
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
            playerRect = pRect;
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
            //FlagInteractables(objects.ToArray());

            base.Update(gameTime);
        }
        public void GetFrame()
        {
            // TODO: Kat - Method should figure out which frame rectangle is supposed to be active and pass it to an attribute bounds, which will need a property or accessor method
            
        }

        public void LoadFrames()
        {
            // TODO: Kat - This method will need to be set faceUp differently depending on individual images or spritesheets.
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
            Vector2 difference = PLayerOrigin - target.SpriteOrigin;
            return Math.Abs(difference.Length());
        }
        // TODO: Change the return type of FlagInteractables back to void
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
            else if (playerRect.Right > roomBounds.Right)
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
            if ((target != this && (GlobalBounds.Intersects(target.GlobalBounds))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Vector2 PLayerOrigin
        {
            get
            {
                return playerRect.Center.ToVector2();
            }
        }

        // Caleb - methods to block player from moving in the cardinal directions. Useful if collisions end faceUp not being handled by the player class
        public void BlockUp()
        {
            playerRect = new Rectangle(playerRect.X, playerRect.Y + 2, playerRect.Width, playerRect.Height);
        }
        public void BlockDown()
        {
            playerRect = new Rectangle(playerRect.X, playerRect.Y - 2, playerRect.Width, playerRect.Height);

        }
        public void BlockLeft()
        {
            //X += 10;
            playerRect = new Rectangle(playerRect.X + 2, playerRect.Y, playerRect.Width, playerRect.Height);
        }
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