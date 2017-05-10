using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using GamePrototype.Classes.Tools; // added by kat for sound effect use
using GamePrototype.Classes;
using GamePrototype.Classes.Objects;

/*Workers: Kat, Tom, Caleb
* DisasterPiece Games
* Player Class
*/
namespace GamePrototype.Classes.Objects
{

    enum PlayerDir { FaceDown, WalkDown, FaceUp, WalkUp, FaceLeft, WalkLeft, FaceRight, WalkRight}

    public delegate void PopUpHandler(string message);

    class Player : GameObject, Tools.IAnimated, Tools.IControlled
    {
        public event PopUpHandler PopUp;

        KeyboardState kbState;
        KeyboardState prevKbState;
        Interactable flaggedInteractable;
        //private Rectangle[][] animFrames; // source rectangles to be used in drawing the player
        private Rectangle moveBounds;
        private Rectangle playerRect;
        private Rectangle hitBox;
        private Vector2 moveQueue;
        private PlayerDir playerDirection; // get direction the player is facing
        private List<Texture2D> walkRightSprites;
        private Texture2D faceRightSprite;
        private Texture2D faceUpSprite;
        private List<Texture2D> walkUpSprites;
        private Texture2D faceDownSprite;
        private List<Texture2D> walkDownSprites;
        private Texture2D promptTexture;
        private Texture2D rossHead;
        private Rectangle rossRect;
        private const int MOVE_SPEED = 4;

        // attributes for sounds - kat
        ContentManager content;
        GameSound footsteps;

        // variables for animation
        double timer = .1;
        int currentFrame = 0;

        // TODO: Player constructor, should take the same sort of information as well as potentially a Menu object. We'd feed the overall Game's Menu into that.
        public Player(GraphicsDevice graphics, ContentManager contentParam, Texture2D faceRight, List<Texture2D> walkRight, Texture2D faceUp, Texture2D faceDown, Rectangle bounds, List<Texture2D> walkUp, List<Texture2D> walkDown) : base()
        {
            moveQueue = Vector2.Zero; // initialize moveQueue to a zero vector
            playerDirection = PlayerDir.FaceDown; // start out facing downwards for now
            moveBounds = bounds;
            playerRect = new Rectangle(1728 / 2, 972 / 2 + 50, 96, 192); //<------------------------THIS IS WHERE THE PLAYER RECT SIZE IS-------------------------
            hitBox = new Rectangle(PlayerRect.X + 24, PlayerRect.Y + 144, 48, 48);
            faceRightSprite = faceRight;
            walkRightSprites = walkRight;
            walkUpSprites = walkUp;
            walkDownSprites = walkDown;
            faceUpSprite = faceUp;
            faceDownSprite = faceDown;
            content = contentParam;
            promptTexture = Tools.ObjectSetup.buttonPrompt;
            // footstep sound effect - kat
            rossHead = content.Load<Texture2D>("bobross");
            rossRect = new Rectangle(playerRect.X - 20, playerRect.Y - 5, 116, 116);
            footsteps = new GameSound("Footsteps", content);
        }
        // TODO: Update method override, should check player input and movement
        public void Update(GameTime gameTime, List<GameObject> objects)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            FlagInteractables(objects.ToArray());
            CheckInput(); // first get input to adjust movement queueing
            Move(gameTime);
            hitBox = new Rectangle(PlayerRect.X + 24, PlayerRect.Y + 144, 48, 48);
            CheckBounds(moveBounds);
            ChangeDirection();
            BlockCollisions(objects);
            if(Game1.bobRossMode)
            {
                rossRect = new Rectangle(playerRect.X - 10, playerRect.Y - 5, 126, 116);
            }

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
                footsteps.PlayAsMusic(.6f);
                moveQueue.Y -= MOVE_SPEED;
            }
            if (kbState.IsKeyDown(Keys.S))
            {
                footsteps.PlayAsMusic(.6f);
                moveQueue.Y += MOVE_SPEED;
            }
            if (kbState.IsKeyDown(Keys.A))
            {
                footsteps.PlayAsMusic(.6f);
                moveQueue.X -= MOVE_SPEED;
            }
            if (kbState.IsKeyDown(Keys.D))
            {
                footsteps.PlayAsMusic(.6f);
                moveQueue.X += MOVE_SPEED;
            }
            if (!kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyDown(Keys.W))
            {
                footsteps.EndMusic();
            }
            if (!kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyDown(Keys.S))
            {
                footsteps.EndMusic();
            }
            if (!kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyDown(Keys.A))
            {
                footsteps.EndMusic();
            }
            if (!kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyDown(Keys.D))
            {
                footsteps.EndMusic();
            }

            if (kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E) && flaggedInteractable != null)
            {
                flaggedInteractable.Interact(this);
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
            Vector2 difference;
            if (target is Interactable && ((Interactable)target).InteractionPoint.Length() > 0)
            {
                difference = PlayerOrigin - ((Interactable)target).GetGlobalInteractPoint();
            }
            else
            {
                difference = PlayerOrigin - target.SpriteOrigin;
            }
            return Math.Abs(difference.Length());
        }
        // TODO: Change the return type of FlagInteractables back to void
        // NOTE: I guess this works as a way of showing the methodology works for now, but I coded this method originally to register with the closest interactable
        // that it's usable so we can flag it with some visual marker like a button prompt. Absolutely works for the purposes of Milestone II's framework until we have this set up better,
        // but I'd like to migrate this to the original planned setup later on. Also, slightly unrelated and I'm not angry or anything but if 100% of my code for a method is commented out for a build
        // please give me a heads-up in the future. I like to be fully aware at all times what work I can take credit for. - Tom
        public void FlagInteractables(GameObject[] targets)
        {
            /*
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
            }*/
            Interactable closest = null; // use this temporary instance of the object to track which interactable in the room is closest to the player
            float minDistance = moveBounds.Right; // ideally nothing should be interactable beyond the width of how far the player can move so use this as the default

            if (flaggedInteractable != null)
            {
                flaggedInteractable.Usable = false;
                flaggedInteractable = null;
            }

            foreach (GameObject obj in targets)
            {
                if (obj is Interactable && obj.Enabled == true) // if the object is usable check its proximity
                {
                    if (minDistance >= CheckProximity(obj)) // looping through this will find which interactable is the closest to the player
                    {
                        if (!(obj is ClueObject) || (obj is ClueObject && !((ClueObject)obj).Found))
                            minDistance = CheckProximity(obj);
                        closest = (Interactable)obj;
                    }
                    else if (((Interactable)obj).Usable == true)
                    {
                        ((Interactable)obj).Usable = false;
                    }
                }
                if (closest != null && minDistance <= (playerRect.Width + 20)) // if something was found in a reasonable proximity
                {
                    flaggedInteractable = closest;
                    flaggedInteractable.Usable = true;
                }
            }
        }
        public void CheckBounds(Rectangle roomBounds)
        {
            // TODO: This method should check whether or not the player's collision rectangle is entirely inside
            // the accepted param bounds roomBounds. Correct any discrepancies.
            if (playerRect.Left < roomBounds.Left)
            {
                //X += (roomBounds.Left - GlobalBounds.Left + 5);
                KeepPlayerFromGoingLeft();
            }
            else if (playerRect.Right > (roomBounds.Right - playerRect.Width))
            {
                KeepPlayerFromGoingRight();
            }

            if (playerRect.Top < roomBounds.Top)
            {
                KeepPlayerFromGoingUp();
            }
            else if (playerRect.Bottom > roomBounds.Bottom)
            {
                KeepPlayerFromGoingDown();
            }

        }
        // Caleb - hopefully will block player and objects
        // Tom - Does it work? Yes. Can I cohesively explain my math here? Probably not.
        public void BlockCollisions(List<GameObject> objects)
        {
            foreach (GameObject go in objects)
            {
                if (go.GlobalBounds.Bottom > hitBox.Bottom && go.FixedDepth == false)
                {
                    go.Depth = .3f;
                }
                else if (go.FixedDepth == false)
                {
                    go.Depth = .7f;
                }
                Boolean collidingObj = isColliding(go);
                if (collidingObj)
                {
                    if (hitBox.Center.Y < go.GlobalBounds.Center.Y && (hitBox.Right - MOVE_SPEED > go.GlobalBounds.Left && hitBox.Left + MOVE_SPEED < go.GlobalBounds.Right))
                    {
                        KeepPlayerFromGoingDown();
                    }
                    if (hitBox.Center.Y > go.GlobalBounds.Center.Y && (hitBox.Right - MOVE_SPEED > go.GlobalBounds.Left && hitBox.Left + MOVE_SPEED < go.GlobalBounds.Right))
                    {
                        KeepPlayerFromGoingUp();
                    }
                    if (hitBox.Center.X > go.GlobalBounds.Center.X && (hitBox.Bottom - MOVE_SPEED > go.GlobalBounds.Top && hitBox.Top + MOVE_SPEED < go.GlobalBounds.Bottom))
                    {
                        KeepPlayerFromGoingLeft();
                    }
                    if (hitBox.Center.X < go.GlobalBounds.Center.X && (hitBox.Bottom - MOVE_SPEED > go.GlobalBounds.Top && hitBox.Top + MOVE_SPEED < go.GlobalBounds.Bottom))
                    {
                        KeepPlayerFromGoingRight();
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
                currentFrame = 0;
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

            if (kbState.IsKeyDown(Keys.S) && !prevKbState.IsKeyDown(Keys.S))
            {
                playerDirection = PlayerDir.FaceDown;
                currentFrame = 0;
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
                currentFrame = 0;
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

            if (kbState.IsKeyDown(Keys.A) && !prevKbState.IsKeyDown(Keys.A))
            {
                playerDirection = PlayerDir.FaceLeft;
                currentFrame = 0;
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
            if (playerDirection == PlayerDir.WalkUp && moveQueue == Vector2.Zero)
            {
                playerDirection = PlayerDir.FaceUp;
            }
            if (playerDirection == PlayerDir.WalkDown && moveQueue == Vector2.Zero)
            {
                playerDirection = PlayerDir.FaceDown;
            }
            if (playerDirection == PlayerDir.WalkRight && moveQueue == Vector2.Zero)
            {
                playerDirection = PlayerDir.FaceRight;
            }
            if (playerDirection == PlayerDir.WalkLeft && moveQueue == Vector2.Zero)
            {
                playerDirection = PlayerDir.FaceLeft;
            }
        }

        public Boolean isColliding(GameObject target)
        {
            if ((target != this && (hitBox.Intersects(target.GlobalBounds)) && target.Tangible))
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
                return hitBox.Center.ToVector2();
            }
        }

        public Rectangle PlayerRect
        {
            get
            {
                return playerRect;
            }
            set
            {
                playerRect = value;
            }
        }
        public override int X
        {
            get
            {
                return playerRect.X;
            }

            set
            {
                playerRect.X = value;
            }
        }
        public override int Y
        {
            get
            {
                return playerRect.Y;
            }

            set
            {
                playerRect.Y = value;
            }
        }
        // Caleb - methods to block player from moving in the cardinal directions. Useful if collisions end faceUp not being handled by the player class
        // moves player down
        public void KeepPlayerFromGoingUp()
        {
            playerRect = new Rectangle(playerRect.X, playerRect.Y + MOVE_SPEED, playerRect.Width, playerRect.Height);
        }
        // moves player up
        public void KeepPlayerFromGoingDown()
        {
            playerRect = new Rectangle(playerRect.X, playerRect.Y - MOVE_SPEED, playerRect.Width, playerRect.Height);

        }
        // moves player right
        public void KeepPlayerFromGoingLeft()
        {
            playerRect = new Rectangle(playerRect.X + MOVE_SPEED, playerRect.Y, playerRect.Width, playerRect.Height);
        }
        // moves player left
        public void KeepPlayerFromGoingRight()
        {
            playerRect = new Rectangle(playerRect.X - MOVE_SPEED, playerRect.Y, playerRect.Width, playerRect.Height);
        }

        public override void Draw(SpriteBatch sprtBtch)
        {
            //base.Draw(sprtBtch);
            if (playerDirection == PlayerDir.FaceUp)
            {
                sprtBtch.Draw(faceUpSprite, Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            }
            if (playerDirection == PlayerDir.WalkUp)
            {
                if (timer == 0 || timer < 0)
                {
                    currentFrame++;
                    timer = .1;
                }
                if (currentFrame >= walkUpSprites.Count)
                {
                    currentFrame = 0;
                }
                sprtBtch.Draw(walkUpSprites[currentFrame], Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            }
            if (playerDirection == PlayerDir.FaceLeft)
            {
                sprtBtch.Draw(faceRightSprite, Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, .5f);
            }
            if (playerDirection == PlayerDir.WalkLeft)
            {
                if (timer == 0 || timer < 0)
                {
                    currentFrame++;
                    timer = .1;
                }
                if (currentFrame >= walkRightSprites.Count)
                {
                    currentFrame = 0;
                }
                sprtBtch.Draw(walkRightSprites[currentFrame], Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, .5f);
            }
            if (playerDirection == PlayerDir.FaceDown)
            {
                sprtBtch.Draw(faceDownSprite, Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            }
            if (playerDirection == PlayerDir.WalkDown)
            {
                if (timer == 0 || timer < 0)
                {
                    currentFrame++;
                    timer = .1;
                }
                if (currentFrame >= walkDownSprites.Count)
                {
                    currentFrame = 0;
                }
                sprtBtch.Draw(walkDownSprites[currentFrame], Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            }
            if (playerDirection == PlayerDir.FaceRight)
            {
                sprtBtch.Draw(faceRightSprite, Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            }
            if (playerDirection == PlayerDir.WalkRight)
            {
                if (timer == 0 || timer < 0)
                {
                    currentFrame++;
                    timer = .1;
                }
                if (currentFrame >= walkRightSprites.Count)
                {
                    currentFrame = 0;
                }
                sprtBtch.Draw(walkRightSprites[currentFrame], Game1.FormatDraw(playerRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            }
            if (Game1.bobRossMode)
            {
                sprtBtch.Draw(rossHead, Game1.FormatDraw(rossRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .15f);
            }
            if (flaggedInteractable != null)
            {
                Vector2 basePoint = new Vector2(playerRect.X + (playerRect.Width / 2), playerRect.Y);
                sprtBtch.Draw(promptTexture, Game1.FormatDraw(new Rectangle((int)(basePoint.X - 16), (int)(basePoint.Y - 40), 32, 32)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .5f);
            }
        }
        public Rectangle MoveBounds { get { return moveBounds; } set { moveBounds = value; } }

        public void SendMessage(string str)
        {
            OnPopUp(str);
        }
        
        protected virtual void OnPopUp(String msg)
        {
            PopUp(msg);
        }

    }
}