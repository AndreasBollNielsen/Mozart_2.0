using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;

namespace Mozart_2._0
{
    class Program
    {
        public static string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static string MinuetFiles = Path.Combine(projectFolder, @"Wave\Minutt\M");
        public static string TrioFiles = Path.Combine(projectFolder, @"Wave\Trio\T");

        public static string[,] minutpath = new string[11, 16];
        public static string[,] triopath = new string[6, 16];

        public static int Dice1 = 0;
        public static int Dice2 = 0;
        static void Main(string[] args)
        {
            //init arrays
            int[,] minuet = new int[,]
            {
            { 96, 22, 141, 41, 105, 122, 11, 30, 70, 121, 26, 9, 112, 49, 109, 14},
            { 32, 6, 128, 63, 146, 46, 134, 81, 117, 39, 126, 56, 174, 18, 116, 83},
            { 69, 95, 158, 13, 153, 55, 110, 24, 66, 139, 15, 132, 73, 58, 145, 79},
            { 40, 17, 113, 85, 161, 2, 159, 100, 90, 176, 7, 34, 67, 160, 52, 170},
            { 148, 74, 163, 45, 80, 97, 36, 107, 25, 143, 64, 125, 76, 136, 1, 93},
            { 104, 157, 27, 167, 154, 68, 118, 91, 168, 71, 150, 29, 101, 162, 23, 151},
            { 152, 60, 171, 53, 99, 133, 21, 127, 16, 155, 57, 175, 43, 168, 89, 172},
            { 119, 84, 114, 50, 140, 86, 169, 94, 120, 88, 48, 166, 51, 115, 72, 111},
            { 98, 142, 41, 156, 75, 129, 62, 123, 65, 77, 19, 82, 137, 38, 149, 8},
            { 3, 87, 165, 61, 135, 47, 147, 33, 102, 4, 31, 164, 144, 59, 173, 78},
            { 54, 130, 10, 103, 28, 37, 106, 5, 35, 20, 108, 92, 12, 124, 44, 131}
            };
            int[,] trio = new int[,]
                {
            { 72, 6, 59, 25, 81, 41, 89, 13, 36, 5, 46, 79, 30, 95, 19, 66},
            { 56, 82, 42, 74, 14, 7, 26, 71, 76, 20, 64, 84, 8, 35, 47, 88},
            { 75, 39, 54, 1, 65, 43, 15, 80, 9, 34, 93, 48, 69, 58, 90, 21},
            { 40, 73, 16, 68, 29, 55, 2, 61, 22, 67, 49, 77, 57, 87, 33, 10},
            { 83, 3, 28, 53, 37, 17, 44, 70, 63, 85, 32, 96, 12, 23, 50, 91},
            { 18, 45, 62, 38, 4, 27, 52, 94, 11, 92, 24, 86, 51, 60, 78, 31}
                };
            string[] notes = new string[16];

            //get paths to wav files
            minutpath = MusicPath(minuet, MinuetFiles);
            triopath = MusicPath(trio, TrioFiles);

            
            //generate music notes from dices
            notes = outputNotes(minutpath,false);

            //play music minuet
            PlayMusic(notes);

            //generate music notes trio
            notes = outputNotes(triopath, true);

            //play music trio
            PlayMusic(notes);

            Console.ReadKey();
        }

        /// <summary>
        /// Output string array of paths to the wav files
        /// </summary>
        /// <param name="Musicpaths"> path to the wav file</param>
        /// <param name="UseRow"> specify row or cells to be used </param>
        /// <returns></returns>
        public static string[] outputNotes(string[,] Musicpaths, bool UseRow)
        {
            string[] output = new string[16];

            for (int i = 0; i < 16; i++)
            {
                if (!UseRow)
                {
                    RollDices(2);
                    output[i] = Musicpaths[Dice1, i];
                }
                else
                {
                    output[i] = Musicpaths[Dice1,Dice2];
                }

            }

            return output;
        }

        /// <summary>
        /// Generating random values for dice 1 & dice 2
        /// </summary>
        /// <param name="NumbDices"> specify the number of dices in use </param>
        public static void RollDices(int NumbDices)
        {
            Random randdice = new Random();
            if (NumbDices > 1)
            {
                Dice1 = randdice.Next(0, 6);
                Dice2 = randdice.Next(0, 6);
            }
            else
            {
                Dice1 = randdice.Next(0, 6);
            }
        }

        /// <summary>
        /// Output paths to each specific wav file
        /// </summary>
        /// <param name="NoteSheet"> 2D array of ints </param>
        /// <param name="Folderpath"> string path to the Solution folder of the wav files</param>
        /// <returns></returns>
        public static string[,] MusicPath(int[,] NoteSheet, string Folderpath)
        {
            string[,] temp = new string[NoteSheet.GetLength(0), NoteSheet.GetLength(1)];
            for (int index = 0; index < NoteSheet.GetLength(0); index++)
            {
                for (int i = 0; i < NoteSheet.GetLength(1); i++)
                {

                    temp[index, i] = Folderpath + NoteSheet[index, i] + ".wav";
                }
            }

            return temp;
        }

        /// <summary>
        /// plays the wav files from a list of strings
        /// </summary>
        /// <param name="Notes"></param>
        public static void PlayMusic(string[] Notes)
        {
            SoundPlayer player = new SoundPlayer();
            foreach (string music in Notes)
            {
                player.SoundLocation = music;
                player.Load();
                player.PlaySync();
                
            }
        }
    }
}
