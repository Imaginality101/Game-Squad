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

        public Interactable(Texture2D txtr, Rectangle psRct) : base(txtr, psRct)
        {
            flaggedForUse = false;
        }
        // Caleb - temporary constructor for interaction demo for milestone 2
        public Interactable(Texture2D txtr, Rectangle psRct, string nm) : base(txtr, psRct, nm)
        {
            flaggedForUse = false;
        }

        public Interactable(Texture2D txtr, Rectangle psRct, Boolean collision):base(txtr, psRct, collision)
        {
            flaggedForUse = false;
        }
        public Interactable(Texture2D txtr, Rectangle psRct, Rectangle clRct) : base(txtr, psRct, clRct)
        {
            flaggedForUse = false;
        }
        // Caleb - Another temporary constructor for interaction demo
        public Interactable(Texture2D txtr, Rectangle psRct, Boolean collision, string nm) : base(txtr, psRct, collision, nm)
        {
            flaggedForUse = false;
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
            if (Game1.easyMode)
            {
                if (flaggedForUse)
                {
                    if ( keyRequired == null || (keyRequired != null && Clue.Inventory.Contains(keyRequired)))
                    {
                        base.Draw(sprtBtch, Color.LightGreen);
                    }
                    else
                    {
                        base.Draw(sprtBtch, Color.Red);
                    }
                }
                else
                {
                    if (keyRequired == null || (keyRequired != null && Clue.Inventory.Contains(keyRequired)))
                    {
                        base.Draw(sprtBtch, Color.SkyBlue);
                    }
                    else
                    {
                        base.Draw(sprtBtch, Color.DarkRed);
                    }
                }
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
