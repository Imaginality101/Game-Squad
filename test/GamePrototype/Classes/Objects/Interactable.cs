using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Classes.Objects
{
    abstract class Interactable : GameObject
    {
        // TODO: This is an abstract class which will be inherited from by the Door and ClueObject classes
        // attributes - should have a Clue that the player needs to access it
        Clue keyRequired;

        public Interactable():base()
        {
            
        }

        public abstract void Interact(GameObject user); // Interaction, check if player has the requisite clue

        // property for required clue
        public Clue RequiredClue
        {
            get { return keyRequired; }

        }
    }
}
