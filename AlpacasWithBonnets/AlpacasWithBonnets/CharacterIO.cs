using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AlpacasWithBonnets
{
    class CharacterIO
    {
        // Could not find the testFile.Alpaca. Need to look into 
        //Zoe McHenry

        //Load
        //public Character LoadCharacter(string filePath)
        //{
        //    BinaryReader saveReader = new BinaryReader(File.OpenRead(filePath));
        //    int health = 1;
        //    int power = 0;
        //    try  // Having the reader in the try was not allowing the game to work
        //    {
        //        Stream saveFile = File.OpenRead(filePath); 
        //        saveReader = new BinaryReader(saveFile);
        //        health = saveReader.ReadInt32();
        //        power = saveReader.ReadInt32();
        //    }
        //    catch
        //    {
        //        Console.WriteLine("File read error.");
        //        Console.WriteLine("Press any key to close this window.");
        //    }
        //    finally
        //    {
        //        saveReader.Close();
        //    }
        //    //TODO:
        //    //Width and Height values are placeholders
        //    //X and Y are set at 0,0 - will need to change
        //    return new Character(0, 0, 100, 100, health, power);
        //}
    }
}
