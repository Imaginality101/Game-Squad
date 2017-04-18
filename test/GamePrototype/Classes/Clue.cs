﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using GamePrototype.Classes.Menu;
/*Workers: Caleb, Tom, Declan
* DisasterPiece Games
* Clue Class
*/
namespace GamePrototype.Classes
{
    class Clue
    {
        // TODO: This class should have a texture, which will be an image file of the clue information.
        // Should also have a string property for flavor/pickup text, and potentially a thumbnail image.
        Texture2D clueImg; // the clue itself to be displayed on screen
        string flavorText;
        string name;
        // might be deprecated
        bool playerHas;
        private static List<Clue> inventory = new List<Clue>();

        // default constructor
        public Clue()
        {
            playerHas = false;
        }
        // constructor with flavorText
        public Clue(string nm, string text)
        {
            playerHas = false;
            flavorText = text;
            name = nm;
        }

        //Declan property to get if the play has something
        public bool PlayerHas {  get { return playerHas; } set { playerHas = value; } }

        // Caleb accessor property for dictionary, call this in setups
        public static Dictionary<string, Clue> Clues { get { return clues; } }

        public static List<Clue> Inventory
        {
            get
            {
                return inventory;
            }
        }

        // kat
        public List<Clue> MenuInventory
        {
            get
            {
                return inventory;
            }
        }

        public Texture2D ClueImage
        {
            get
            {
                return clueImg;
            }

            set
            {
                clueImg = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        // TODO: Caleb - Clue Dictionary should go here, it will be static
        private static Dictionary<string, Clue> clues = new Dictionary<string, Clue>
        {
            {"News1", new Clue("News1","Details revealed about the disappearance of the wealthy widow Olivia Afton.")},
            {"News2", new Clue("News2", "Another body was pulled from the Humerus River with lacerations. The press begins to call the string a deaths a serial event, branding the killer \"The Funnybone Killer\"") },
            {"News3", new Clue("News3", "Reports of a missing girl have began to surface after deaths of Mr and Mrs. Edward Afton. The question on everyone's minds is: Where is Elizabeth Afton? ") },
            {"News4", new Clue("News4", "Reports that Afton Manor has been acquired by the state due to no live heirs") },
            {"News5", new Clue("News5", "A bed of remains have been found near the river yet again, this time with an unidentified number of victims. The repeated" /*TODO: Finish sentence */ ) },
            {"Bathroom Key", new Clue("Bathroom Key", "It's a key to the bathroom") },
            {"Closet Key", new Clue("Closet Key", "It's a key to the closet") },
            {"OldPhoto1", new Clue("OldPhoto1", "It's a photo of the house, when it was constructed") },
            {"OldPhoto2", new Clue("OldPhoto2", "It's a photo of Liz, and her parents Olivia and Edward") },
            {"OldPhoto3", new Clue("OldPhoto3", "It's a photo of the cutlery rack in the kitchen, it has a spot missing.") },
            {"NewPhoto", new Clue("NewPhoto", "It's a stained polaroid of Olivia, unconscious on the floor.") },
            {"TenantDiary1", new Clue("TenantDiary1", "Introduction to Deborah and Katie, the tenants that moved in after the house was taken over.") },
            {"TenantDiary2", new Clue("TenantDiary2", "Deb confesses to the struggles of raising a moody teen singlehandedly. She even has to keep a hidden key to the closet for when Katie locks herself in.") },
            {"TenantDiary3", new Clue("TenantDiary3", "Deb is scared. She found the lock to the house broken last night and the locksmith is booked out for another 3 days. She hopes that the chair under the handle tactic will work but still is anxious.") },
            // TODO: Caleb - I'm confused about the lamp/lampshade; is one of them a clue, or does it just lead to the blacklight?
            {"CrazyDiary1", new Clue("CrazyDiary1", "An unnerving account of how a crazy person disposed of a dead body") },
            {"CrazyDiary2", new Clue("CrazyDiary2", "An account of how to get blood out of clothing before a funeral") },
            {"CrazyDiary3", new Clue("CrazyDiary3", "The tale of how a little girl tortured and killed her neglectful parents") },
            {"Receipt", new Clue("Receipt", "Receipt from the local general store for bleach, hydrogen peroxide, gardening tools and other various food items. Payed for by card") },
            {"Ring", new Clue("Ring", "A ring that use to belong to Olivia Afton") },
            {"Pendant", new Clue("Pendant", "It's a locket. It won't seem to open.") },
            {"Bones", new Clue("Bones", "Unidentified bones found in the wastebin in the bathroom") },
            {"JaggedKnife", new Clue("JaggedKnife", "Stained knife found in the bathtub. It's seems like a modified kitchen knife") },
            {"SpaCoupon", new Clue("SpaCoupon", "It's an expired spa coupon... too bad.") },
            {"MedicineBottle", new Clue("MedicineBottle", "It's a prescription for something called ”ARIPIPRAZOLE” the rest of the sicker is ripped.") },
            {"StickyNote", new Clue("StickyNote", "It has some phone numbers on it. Some are scribbled out.") }
        };

        public override string ToString()
        {
            return flavorText;
        }

        public static void PrintInventory()
        {
            string result = "";
            foreach (Clue c in inventory)
            {
                result += c.name + ": " + c.flavorText + '\n';
            }
            Console.WriteLine(result); 
        }

        public static void LoadContent(Texture2D news, Texture2D bathroomKey, Texture2D closetKey, Texture2D oldPhoto, Texture2D newPhoto, Texture2D tenantDir, Texture2D crazyDir, Texture2D receipt, Texture2D ring, Texture2D pendant, Texture2D bones, Texture2D jaggedKnife, Texture2D spaCoupon, Texture2D medicineBottle, Texture2D stickyNote)
        {
            clues["News1"].ClueImage = news;
            clues["News2"].ClueImage = news;
            clues["News3"].ClueImage = news;
            clues["News4"].ClueImage = news;
            clues["News5"].ClueImage = news;
            clues["Bathroom Key"].ClueImage = bathroomKey;
            clues["Closet Key"].ClueImage = closetKey;
            clues["OldPhoto1"].ClueImage = oldPhoto;
            clues["OldPhoto2"].ClueImage = oldPhoto;
            clues["OldPhoto3"].ClueImage = oldPhoto;
            clues["NewPhoto"].ClueImage = newPhoto;
            clues["TenantDiary1"].ClueImage = tenantDir;
            clues["TenantDiary2"].ClueImage = tenantDir;
            clues["TenantDiary3"].ClueImage = tenantDir;
            clues["CrazyDiary1"].ClueImage = crazyDir;
            clues["CrazyDiary2"].ClueImage = crazyDir;
            clues["CrazyDiary3"].ClueImage = crazyDir;
            clues["Receipt"].ClueImage = receipt;
            clues["Ring"].ClueImage = ring;
            clues["Pendant"].ClueImage = pendant;
            clues["Bones"].ClueImage = bones;
            clues["JaggedKnife"].ClueImage = jaggedKnife;
            clues["SpaCoupon"].ClueImage = spaCoupon;
            clues["MedicineBottle"].ClueImage = medicineBottle;
            clues["StickyNote"].ClueImage = stickyNote;
        }

    }
}