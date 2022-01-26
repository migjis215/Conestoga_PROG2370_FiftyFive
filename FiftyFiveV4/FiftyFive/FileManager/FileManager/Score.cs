using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class Score
    {
        public static string recordPath = @"..\records.txt";
        //public static List<Score> scores;
        public static StreamReader reader;
        public static StreamWriter writer;
        public static string record = "";

        public string Name { get; set; }
        public int Blocks { get; set; }

        public static void ConfirmFile()
        {
            try
            {
                if (!File.Exists(recordPath))
                {
                    File.Create(recordPath).Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception confirming the file: " + ex.Message);
            }
        }

        public static List<Score> getRecords()
        {
            ConfirmFile();
            List<Score> scores = new List<Score>();

            try
            {
                using (reader = new StreamReader(recordPath))
                {
                    while (!reader.EndOfStream)
                    {
                        record = reader.ReadLine();
                        Score score = new Score()
                        {
                            Name = record.Split('\t')[0],
                            Blocks = int.Parse(record.Split('\t')[1])
                        };
                        scores.Add(score);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting records on file: " + ex.Message);
            }

            return scores;
            //return getRecords().OrderByDescending(s => s.Blocks).ToList();
        }

        public static void addRecord(List<Score> scores)
        {
            scores = scores.OrderByDescending(s => s.Blocks).ToList();

            try
            {
                using (writer = new StreamWriter(recordPath, false))
                {
                    for (int i = 0; i < scores.Count; i++)
                    {
                        writer.WriteLine(scores[i].Name + "\t" + scores[i].Blocks.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to add a new record: " + ex.Message);
            }
        }
    }
}
