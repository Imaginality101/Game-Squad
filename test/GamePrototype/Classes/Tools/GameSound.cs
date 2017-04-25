using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
/*Workers: Declan 
 * DisasterPiece Games
 * GameSound Class
 */
namespace GamePrototype.Classes.Tools
{
    //TODO: This class handles the sound effect building and the backround music for the game
    class GameSound
    {
        //Attributes
        SoundEffect singleEffect;
        SoundEffectInstance singleEffectInst;
        bool isPlayed = false;

        //Proporties
        public bool IsPlayed { get { return isPlayed; } set { isPlayed = value; } }
        //Constructor
        public GameSound(string filename, ContentManager content)
        {
            
            singleEffect = content.Load<SoundEffect>(filename);
            singleEffectInst = singleEffect.CreateInstance();

        }



        //PlayAsSoundEffect Method - Takes a float volume as a parameter
        public void PlayAsSoundEffect(float volume0to1f)
        {
            singleEffectInst = singleEffect.CreateInstance();
            singleEffectInst.Play();
            singleEffectInst.Volume = volume0to1f;
        }

        //PlayAsMusic Method - Takes a float volume as a parameter
        public void PlayAsMusic(float volume0to1f)
        {
            if (singleEffectInst.State == SoundState.Stopped)
            {
                singleEffectInst.Play();
                singleEffectInst.Volume = volume0to1f;
            }
        }
        //PlayIntro Method - Takes a float volume as a parameter
        public void PlayIntro(float volume0to1f)
        {
            if (isPlayed == false)
            {
                singleEffectInst.Play();
                singleEffectInst.Volume = volume0to1f;
                if (singleEffectInst.State == SoundState.Stopped)
                {
                    isPlayed = true;
                }
            }
        }
        //EndMusic Method - stops the music
        public void EndMusic()
        {
            singleEffectInst.Stop();
        }
    } 
}
