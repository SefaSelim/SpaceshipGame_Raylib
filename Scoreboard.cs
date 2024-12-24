using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SpaceshipGame
{
    public class Scoreboard
    {
        string inputText = "";
        public bool saveSuccessful = false;

        string filePath = "Scoreboard.txt";


        public void DrawScoreSavePanel()
        {

            int key = Raylib.GetCharPressed();
            while (key > 0)
            {
                if (key >= 32 && key <= 126)
                {
                    inputText += (char)key;
                }
                key = Raylib.GetCharPressed();
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && inputText.Length > 0)
            {
                inputText = inputText.Substring(0, inputText.Length - 1);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Enter) && !string.IsNullOrWhiteSpace(inputText))
            {
                try
                {
                    File.AppendAllText(filePath, inputText + Environment.NewLine + Game.Score.ToString() + Environment.NewLine);
                    saveSuccessful = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                    saveSuccessful = false;
                }
            }

            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.White);

            Raylib.DrawText("Enter your name and press ENTER:", 10, 10, 20, Color.DarkGray);
            Raylib.DrawText(inputText, 10, 40, 20, Color.Black);

            if (saveSuccessful)
            {
                Raylib.DrawText("Succecssfully Saved The Score", 10, 70, 20, Color.Green);
            }


            Raylib.EndDrawing();
        }

        public void DrawScoreboard ()
        {
            Raylib.BeginDrawing();


            Raylib.ClearBackground(Color.White);


            if (!File.Exists(filePath))
            {
                Console.WriteLine("Dosya bulunamadi");
                return;
            }

            try
            {
                string[] rows = File.ReadAllLines(filePath);

                List<(string Isim, int Skor)> players = new List<(string, int)>();

                for (int i = 0; i < rows.Length; i += 2)
                {
                    if (i + 1 < rows.Length)
                    {
                        string isim = rows[i];
                        if (int.TryParse(rows[i + 1], out int skor))
                        {
                            players.Add((isim, skor));
                        }
                    }
                }

                Raylib.DrawText("SCOREBOARD", 280, 50, 35, Color.Black);

                var orderedPlayers = players.OrderByDescending(o => o.Skor).Take(10);

                int sira = 1;
                foreach (var player in orderedPlayers)
                {
                    Raylib.DrawText(sira + "."+"  " + player.Isim + "  " + player.Skor, 280, 70 + sira * 30, 20, Color.Black);
                    sira++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

            Raylib.EndDrawing();
        }


    }
}
