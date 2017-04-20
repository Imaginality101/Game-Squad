using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using GamePrototype.Classes.Objects;
/*Workers: Caleb
 * DisasterPiece Games
 * SaveData Class
 */
namespace GamePrototype.Classes.Tools
{
    static class SaveData
    {
        // attributes
        //protected BinaryWriter bedRoomDataWriter;
        //protected BinaryReader bedRoomDataReader;
        //protected Stream bedRoomReadStream;
        //protected Stream bedRoomWriteStream;
        //protected const string BEDROOM_PATH = "SaveFile";
        private static StreamReader saveFileReader;
        private static StreamWriter saveFileWriter;
        private static string savePath = "SaveFile.txt";
        private static BinaryReader settingsReader;
        private static Stream settingsReadStream;
        private static string settingsPath = "Settings";
        // constructor
        /*public SaveData()
        {

        }*/
        // writes to the file
        /*public void WriteBedroom()
        {
            bedRoomWriteStream = File.Open(BEDROOM_PATH, FileMode.Create);
            bedRoomDataWriter = new BinaryWriter(bedRoomWriteStream);
            // string class name
            bedRoomDataWriter.Write("GameObject");
            // bool isActivated
            bedRoomDataWriter.Write(true);
            // name of texture to load
            bedRoomDataWriter.Write("BlueGuy");
            // vector components
            bedRoomDataWriter.Write(100);
            bedRoomDataWriter.Write(50);
            // end the file
            bedRoomDataWriter.Write("END");
            bedRoomDataWriter.Close();
            
        }*/

        // reads the data
        /*public List<GameObject> ReadBedroom()
        {
            // the list to return
            List<GameObject> result = new List<GameObject>();
            bedRoomReadStream = File.OpenRead(BEDROOM_PATH);
            bedRoomDataReader = new BinaryReader(bedRoomReadStream);
            // read the objects in a loop
            do
            {
                string className = bedRoomDataReader.ReadString();
                // if end of file, terminate method
                if (className == "END")
                {
                    bedRoomDataReader.Close();
                    return result;
                }
                // read data, pass it to a GameObject constructor, add the GameObject to the result list
                else if (className == "GameObject")
                {
                    bool isActivated;
                    string textureName;
                    int x;
                    int y;
                    isActivated = bedRoomDataReader.ReadBoolean();
                    textureName = bedRoomDataReader.ReadString();
                    x = bedRoomDataReader.ReadInt32();
                    y = bedRoomDataReader.ReadInt32();
                    result.Add(new GameObject(isActivated, textureName, new Point (x, y)));
                }
            } while (true);
        }*/

        public static List<object> ReadSettings()
        {
            List<object> result = new List<object>();
            try
            {
                settingsReadStream = File.OpenRead(settingsPath);
                settingsReader = new BinaryReader(settingsReadStream);
                // read until end of file
                try
                {
                    result.Add(settingsReader.ReadBoolean());
                    result.Add(settingsReader.ReadBoolean());
                    result.Add(settingsReader.ReadBoolean()); // Tom - Resolution settings, fullscreen boolean
                    if (!(Boolean)result[2]) // if windowed
                    {
                        result.Add(settingsReader.ReadInt32()); // width
                        result.Add(settingsReader.ReadInt32()); // height
                    }
                }
                catch (EndOfStreamException)
                {

                }
            }
            catch(FileNotFoundException e) // If the file isn't found just default the settings
            {
                result.Add(false);
                result.Add(false);
                result.Add(false);
                result.Add(1728);
                result.Add(972);
            }
                return result;
        }

        // saves the game by saving their inventory
        //            saveFileWriter = new StreamWriter(new FileStream("SaveFile", FileMode.Create));

        public static void Save()
        {
            //saveFileWriter = new StreamWriter(new FileStream("SaveFile", FileMode.Create));
            saveFileWriter = new StreamWriter(savePath);
            foreach(Clue c in Clue.Inventory)
            {
                string entry = c.Name + " ";
                saveFileWriter.Write(entry);
                saveFileWriter.Flush();
            }
            saveFileWriter.Close();
        }
        // returns the text in SaveFile
        public static string GetSaveFileData()
        {
            saveFileReader = new StreamReader(savePath);
            string line = "";
            string result = "";
            while ((line = saveFileReader.ReadLine()) != null)
            {
                result += line;
            }
            saveFileReader.Close();
            return result;
        }
        // clears the SaveFile file
        public static void Restart()
        {
            saveFileWriter = new StreamWriter(new FileStream("SaveFile", FileMode.Create));
            Clue.Inventory.Clear();
            saveFileWriter.Close();
        }
    }
}
