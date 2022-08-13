using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class ScoreRecord
{
    public static ScoreData[] highScores = new ScoreData[]{
        new ScoreData() { initials = new char[] {'T', 'O', 'B' }, totalSeconds = 300, highestLevel = 6},
        new ScoreData() { initials = new char[] {'C', 'A', 'T' }, totalSeconds = 300, highestLevel = 6},
        new ScoreData() { initials = new char[] {'G', 'U', 'B' }, totalSeconds = 300, highestLevel = 5},
        new ScoreData() { initials = new char[] {'Z', 'I', 'M' }, totalSeconds = 300, highestLevel = 5},
        new ScoreData() { initials = new char[] {'P', 'A', 'O' }, totalSeconds = 300, highestLevel = 4},
        new ScoreData() { initials = new char[] {'E', 'D', 'D' }, totalSeconds = 300, highestLevel = 4},
        new ScoreData() { initials = new char[] {'M', 'Y', 'L' }, totalSeconds = 300, highestLevel = 3},
        new ScoreData() { initials = new char[] {'W', 'E', 'S' }, totalSeconds = 300, highestLevel = 3},
        new ScoreData() { initials = new char[] {'K', 'I', 'X' }, totalSeconds = 300, highestLevel = 2},
        new ScoreData() { initials = new char[] {'Z', 'U', 'D' }, totalSeconds = 300, highestLevel = 2},
    };

    public static int totalSeconds = 0;

    public static void ResetHighScores()
    {
        highScores = new ScoreData[]{
            new ScoreData() { initials = new char[] {'T', 'O', 'B' }, totalSeconds = 300, highestLevel = 6},
            new ScoreData() { initials = new char[] {'C', 'A', 'T' }, totalSeconds = 300, highestLevel = 6},
            new ScoreData() { initials = new char[] {'G', 'U', 'B' }, totalSeconds = 300, highestLevel = 5},
            new ScoreData() { initials = new char[] {'Z', 'I', 'M' }, totalSeconds = 300, highestLevel = 5},
            new ScoreData() { initials = new char[] {'P', 'A', 'O' }, totalSeconds = 300, highestLevel = 4},
            new ScoreData() { initials = new char[] {'E', 'D', 'D' }, totalSeconds = 300, highestLevel = 4},
            new ScoreData() { initials = new char[] {'M', 'Y', 'L' }, totalSeconds = 300, highestLevel = 3},
            new ScoreData() { initials = new char[] {'W', 'E', 'S' }, totalSeconds = 300, highestLevel = 3},
            new ScoreData() { initials = new char[] {'K', 'I', 'X' }, totalSeconds = 300, highestLevel = 2},
            new ScoreData() { initials = new char[] {'Z', 'U', 'D' }, totalSeconds = 300, highestLevel = 2},
        };

        string path = Path.Combine(Application.persistentDataPath, "score.sav");
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, highScores);
        }
    }

    public static void UpdateHighScores(ScoreData newData)
    {

        ScoreData lastData = highScores[highScores.Length - 1];

        List<ScoreData> scoreList = new(highScores);

        scoreList.Add(newData);
        scoreList.Sort((a, b) => { 
            if (a.highestLevel != b.highestLevel) return b.highestLevel - a.highestLevel;
            return a.totalSeconds - b.totalSeconds;
        });
        scoreList.RemoveAt(scoreList.Count - 1);

        highScores = scoreList.ToArray();

        string path = Path.Combine(Application.persistentDataPath, "score.sav");
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, highScores);
        }
    }

    public static void LoadHighScores()
    {
        string path = Path.Combine(Application.persistentDataPath, "score.sav");
        if (File.Exists(path))
        {
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                highScores = (ScoreData[])formatter.Deserialize(stream);
            }
        }
    }

    public static int GetRankNumber(ScoreData data)
    {
        int rank = -1;
        for (int i = highScores.Length - 1; i >= 0; i--)
        {
            ScoreData lowData = highScores[i];
            if (data.highestLevel < lowData.highestLevel) break;
            if (data.highestLevel == lowData.highestLevel &&
                data.totalSeconds > lowData.totalSeconds) break;
            rank = i + 1;
        }
        return rank;
    }

    public static bool IsNewHighScore(ScoreData newData)
    {
        ScoreData lowData = highScores[^1];
        if (newData.highestLevel < lowData.highestLevel) return false;
        if (newData.highestLevel == lowData.highestLevel &&
            newData.totalSeconds > lowData.totalSeconds) return false;
        return true;
    }
}
