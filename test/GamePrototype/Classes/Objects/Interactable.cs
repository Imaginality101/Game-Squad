using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
/*Workers: Caleb, Tom
 * DisasterPiece Games
 * Interactable Class
 */
namespace GamePrototype.Classes.Objects
{
    abstract class Interactable : GameObject
    {
        
        // TODO: This is an abstract class which will be inherited from by the Door and ClueObject classes
        // attributes - should have a Clue that the player needs to access it
        private Clue keyRequired;
        private Boolean flaggedForUse;
        Vector2 interactionPoint;
        Texture2D promptTexture;

        public Interactable(Texture2D txtr, Rectangle psRct) : base(txtr, psRct)
        {
            flaggedForUse = false;
            promptTexture = Tools.ObjectSetup.buttonPrompt;
        }
        // Caleb - temporary constructor for interaction demo for milestone 2
        public Interactable(Texture2D txtr, Rectangle psRct, string nm) : base(txtr, psRct, nm)
        {
            flaggedForUse = false;
            promptTexture = Tools.ObjectSetup.buttonPrompt;
        }

        public Interactable(Texture2D txtr, Rectangle psRct, Boolean collision):base(txtr, psRct, collision)
        {
            flaggedForUse = false;
            promptTexture = Tools.ObjectSetup.buttonPrompt;
        }
        public Interactable(Texture2D txtr, Rectangle psRct, Rectangle clRct) : base(txtr, psRct, clRct)
        {
            flaggedForUse = false;
            promptTexture = Tools.ObjectSetup.buttonPrompt;
        }
        // Caleb - Another temporary constructor for interaction demo
        public Interactable(Texture2D txtr, Rectangle psRct, Boolean collision, string nm) : base(txtr, psRct, collision, nm)
        {
            flaggedForUse = false;
            promptTexture = Tools.ObjectSetup.buttonPrompt;
        }

        public Interactable()
        {
        }

        public abstract void Interact(Player user); // Interaction, check if player has the requisite clue

        // property for required clue
        public Clue RequiredClue
        {
            get { return keyRequired; }
            set { keyRequired = value; }
        }

        // property for flagged
        public Boolean Usable
        {
            get { return flaggedForUse; }
            set { flaggedForUse = value; }
        }

        public override void Draw(SpriteBatch sprtBtch)
        {
            if (flaggedForUse)
            {
                base.Draw(sprtBtch, Color.LightGreen);
                Vector2 basePoint;
                if (interactionPoint == null)
                {
                    basePoint = base.SpriteOrigin;
                }
                else
                {
                    basePoint = GetGlobalInteractPoint();
                }
                sprtBtch.Draw(promptTexture, Game1.FormatDraw(new Rectangle((int)basePoint.X, (int)basePoint.Y - 30, 32, 32)), Color.White);
            }
            else
            {
                base.Draw(sprtBtch);
            }
        }

        // property for point of interaction
        public Vector2 InteractionPoint
        {
            get { return interactionPoint; }
            set { interactionPoint = value; }
        }
        public Vector2 GetGlobalInteractPoint()
        {
            Vector2 location = GlobalBounds.Location.ToVector2() + interactionPoint;
            return location;
        }
    }
}
