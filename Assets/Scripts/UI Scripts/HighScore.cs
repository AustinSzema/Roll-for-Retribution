using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScore
{
   private string highScoreFile = "highScore.txt";
   public void WriteHighScore(int score)
   {
      int oldHighScore = 0;
      try
      {
         using (StreamReader reader = new StreamReader(highScoreFile))
         {
            string temp = reader.ReadLine();
            oldHighScore = int.Parse(temp);
            reader.Close();
         }

         if (score > oldHighScore)
         {
            using (StreamWriter writer = new StreamWriter(highScoreFile))
            {
               writer.Close();
               File.WriteAllText(highScoreFile, score.ToString());
            }
         }
      }
      catch (Exception exp)
      {
         Debug.Log(exp.Message);
         using (StreamWriter writer = new StreamWriter(highScoreFile))
         {
            writer.Write(score);
            writer.Close();
         }
      }
   }

   public int GetHighScore()
   {
       int highScore = 0;
       try
       {
          using (StreamReader reader = new StreamReader(highScoreFile))
          {
             string temp = reader.ReadLine();
             highScore = int.Parse(temp);
             reader.Close();
          }
       }
       catch (Exception e)
       {
          Debug.Log(e);
          throw new Exception("couldnt open file");
       }

       return highScore;
   }

}
