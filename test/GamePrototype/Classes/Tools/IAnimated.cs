using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Workers: Tom
 * DisasterPiece Games
 * IAnimated interface for anything that animates, only applies to the player currently but if we implement other animated objects or make our stretch goal of getting a ghost that chases this'll apply to those too.
 * NOTE: At the time of turning in Milestone II the functions planned to be implemented through this interface are mostly present in the Player class' constructor and Draw methods. I'm not gonna micromanage someone else's code much
 * since the job gets done anyways, so this interface may go on the chopping block if its functions are handled in another way that works cleanly enough. - Tom
 */
namespace GamePrototype.Classes.Tools
{
    interface IAnimated
    {
        void GetFrame();
        void LoadFrames();
    }
}
