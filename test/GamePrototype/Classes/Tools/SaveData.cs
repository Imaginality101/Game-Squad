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

namespace GamePrototype.Classes.Tools
{
    class SaveData
    {
        // attributes
        protected BinaryWriter bedRoomDataWriter;
        protected BinaryReader bedRoomDataReader;
        protected Stream bedRoomReadStream;
        protected Stream bedRoomWriteStream;
        protected const string BEDROOM_PATH = "SaveFile";
        protected BinaryReader settingsReader;
        protected Stream settingsReadStream;
        protected const string SETTINGS_PATH = "Settings";
        // constructor
        public SaveData()
        {

        }
        // writes to the file
        public void WriteBedroom()
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
            
        }

        // reads the data
        public List<GameObject> ReadBedroom()
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
        }

        public List<object> ReadSettings()
        {
            List<object> result = new List<object>();
            settingsReadStream = File.OpenRead(SETTINGS_PATH);
            settingsReader = new BinaryReader(settingsReadStream);
            // read until end of file
            try
            {
                result.Add(settingsReader.ReadBoolean());
            }
            catch (EndOfStreamException)
            {

            }
            return result;
        }
    }
}
