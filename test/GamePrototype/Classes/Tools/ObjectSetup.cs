using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePrototype.Classes.Objects;

namespace GamePrototype.Classes.Tools
{
    static class ObjectSetup
    {
        // TODO: Declan - You can use this for initializing. I'll give some recommendations but largely leave decisions on methodology up to you.
        // Right now this is a static class (quick refresher, can't be instantiated), since we don't really need to initialize
        // it for functionality. What I'd recommend doing is setting up one method for each room we add 
        // (you could even do a property if you wanted, that'd be a really long property though)
        // and setting it up to return a list of GameObjects specifically tailored to that room's needs. Then just call that when instantiating
        // a room.
        // You could also set it up like TryParse, with a Room as a parameter with the keyword out in front.
        // Obviously you can also just get rid of the static identifier if you need to.

        // As an important other note, you'll need a way to get all the Texture2Ds you need for the objects. I'd recommend working with Kat,
        // get a dictionary up and running of Texture2D's with a string key


    }
}
