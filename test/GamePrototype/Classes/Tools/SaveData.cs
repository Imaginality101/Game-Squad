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
        public void Write()
        {
            bedRoomWriteStream = File.OpenWrite(BEDROOM_PATH);
            bedRoomDataWriter = new BinaryWriter(bedRoomWriteStream);
            /* class name
            bedRoomDataWriter.Write("Interactable");
            // bool isActivated
            bedRoomDataWriter.Write(false);
            // name of texture to load
            bedRoomDataWriter.Write("BlueGuy");
            // vector components
            bedRoomDataWriter.Write(0.0);
            bedRoomDataWriter.Write(20.0);
            // start again
            // class name
            bedRoomDataWriter.Write("Interactable");
            // bool isActivated
            bedRoomDataWriter.Write(false);
            // name of texture to load
            bedRoomDataWriter.Write("GreenGuy");
            // vector components
            bedRoomDataWriter.Write(400.0);
            bedRoomDataWriter.Write(20.0);
            // end the file
            bedRoomDataWriter.Write("END");
            bedRoomDataWriter.Close();
            */
        }

        // reads the data
        public List<GameObject> Read()
        {
            // the list to return
            List<GameObject> result = new List<GameObject>();
            bedRoomReadStream = File.OpenRead(BEDROOM_PATH);
            bedRoomDataReader = new BinaryReader(bedRoomReadStream);
            // read the objects in a loop
            do
            {
                string className = bedRoomDataReader.ReadString();
                if (className == "END")
                {
                    bedRoomDataReader.Close();
                    return result;
                }
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
            catch (EndOfStreamException e)
            {

            }
            return result;
        }
    }
}
