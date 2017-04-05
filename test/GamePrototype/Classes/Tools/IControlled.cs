using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
/*Workers: Tom
 * DisasterPiece Games
 * IControlled interface for anything which checks user input. Not quite as redundant right now as IAnimated since the player class actively uses it, but a lot of stuff is still in Game1 such as controls for the Menu.
 * If Menu stays in a situation where it doesn't use the interface in any meaningful way I'll collapse this into the Player class since it'd just be pointless. - Tom
 */
namespace GamePrototype.Classes.Tools
{
    interface IControlled
    {
        void CheckInput();
    }
}