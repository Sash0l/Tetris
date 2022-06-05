using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris.Controllers
{
    public static class Record
    {
        public static string recordPath = @"Records.txt";
        public static void SaveRecords(string playerName)
        {
            List<string> recordsArray = new List<string>(LoadRecords());
            bool isNameExistsInRecords = false;
            for (int i = 0; i < recordsArray.Count; i++)
            {
                if (playerName == recordsArray[i].Split('|')[0])
                {
                    isNameExistsInRecords = true;
                    recordsArray.RemoveAt(i);
                    recordsArray.Add(playerName + "|" + DrawControler.score);
                }
            }
            if (!isNameExistsInRecords)
                recordsArray.Add(playerName + "|" + DrawControler.score);
            File.WriteAllLines(recordPath, recordsArray);
        }

        public static IEnumerable<string> LoadRecords()
        {
            if (File.Exists(recordPath))
            {
                string[] recordsArray = File.ReadAllLines(recordPath);
                return recordsArray;
            }
            else
            {
                File.Create(recordPath).Dispose();
                return new string[] { };
            }
        }

        public static void ShowRecords(Label Label4)
        {
            List<string> recordsArray = new List<string>(LoadRecords());

            for (int i = 0; i < recordsArray.Count - 1; ++i)

            { //Сортировка рекордов по очкам
                string str = recordsArray[i];
                string strNext = recordsArray[i + 1];
                string[] args = str.Split('|');
                string[] argsNext = strNext.Split('|');

                if (int.Parse(args[1]) < int.Parse(argsNext[1]))
                {
                    var temp = recordsArray[i];
                    recordsArray[i] = recordsArray[i + 1];
                    recordsArray[i + 1] = temp;
                    i = -1;
                }

            }
            Label4.Text = "Таблица рейтинга";
            foreach ( var str in recordsArray)
            {
                string[] args = str.Split('|');
                    Label4.Text += "\n";
                Label4.Text += "Игрок " + args[0] + " : " + args[1];

            }
            
        }
    }
}

