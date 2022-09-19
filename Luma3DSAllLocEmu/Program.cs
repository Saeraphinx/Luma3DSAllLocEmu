using System;
using System.Collections;

// ftp://10.0.0.34:5000/Nintendo%203DS/6f43b219b1247ad3ba3c340ea8935abd/390701234780ada7534c363400035344/title/00040000/
// ftp://10.0.0.34:5000/luma/titles/
namespace Luma3DSAllLocEmu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"";
            string path2 = @"";
            if (args.Length <= 1)
            {
                Console.WriteLine("3ds:");
                //path1 = Console.ReadLine();
                Console.WriteLine("LumaOut:");
                //path2 = Console.ReadLine();
            } else
            {
                path1 = args[0];
                path2 = args[0];
            }


            if (args.Length >= 3)
            {
                Console.WriteLine("Invalid Arguments. Read the usage for arguments.");
                Environment.Exit(-1);
            }

            Console.WriteLine("Reading Directories...");
            string[] n3dsTitles = Directory.GetDirectories(path1);
            string[] lumaTitles = Directory.GetDirectories(path2);
            Console.WriteLine("Found {0} installed titles.", n3dsTitles.Length);
            Console.WriteLine("Found {0} Luma titles.", lumaTitles.Length);

            Console.WriteLine("Loading installed TitleIDs...");
            ArrayList installedTitleIDs = new ArrayList();
            foreach (string title in n3dsTitles)
            {
                string titleID = "00040000" + Path.GetFileName(title);
                installedTitleIDs.Add(titleID.ToUpper());
            }
            Console.WriteLine("Loaded installed TitleIDs.");

            Console.WriteLine("Loading Luma3DS TitleIDs...");
            ArrayList LumaTitleIDs = new ArrayList();
            foreach (string title in lumaTitles)
            {
                string titleID = Path.GetFileName(title);
                LumaTitleIDs.Add(title);
            }
            Console.WriteLine("Loaded Luma3DS TitleIDs.");

            Console.WriteLine("Removing existing Luma3DS TitleIDs to prevent overwriting files.");
            int removedEnt = 0;
            foreach (string n3dsTitle in installedTitleIDs)
            {
                foreach (string lumaTitle in lumaTitles)
                {
                    if (lumaTitle.ToUpper() == n3dsTitle.ToUpper())
                    {
                        installedTitleIDs.Remove(n3dsTitle);
                        removedEnt++;
                    }
                }
            }
            Console.WriteLine("Removing {0} from list, {1} will have their entries to the Luma3DS title folder.", removedEnt, installedTitleIDs.Count);

            Console.WriteLine("Adding new titles...");
            int ltitlesadfded = 0;
            foreach (string titleID in installedTitleIDs)
            {
                string temp = Path.Combine(path2, titleID);
                Directory.CreateDirectory(temp);
                temp = Path.Combine(temp, "locale.txt");
                File.WriteAllText(temp, "USA EN");
                Console.WriteLine("Adding {0}", titleID);
                ltitlesadfded++;
            }
            Console.WriteLine("Added {0} TitleIDs.", ltitlesadfded);
        }
    }
}