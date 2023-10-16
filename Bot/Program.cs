using AForge.Imaging;
using AutoItX3Lib;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Bot
{
    class Program
    {

        static bool stopLoop = false;
        static int timeThread = 1000;
        static string status = "waiting";
        static string date = DateTime.Now.ToString("hh:mm:ss") + ": ";
        static AutoItX3 auto = new AutoItX3();

        static void Main(string[] args)
        {
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
            Bitmap BuscarPartida = new Bitmap("1_BuscarPartida.jpg");
            Bitmap PartidaEncontrada = new Bitmap("2_PartidaEncontrada.jpg");
            Bitmap PartidaIniciada = new Bitmap("3_PartidaIniciada.jpg");
            Bitmap ValidarPartidaIniciada = new Bitmap("11_ValidarPartidaIniciada.jpg");
            Bitmap Rendirse = new Bitmap("4_Rendirse.jpg");
            Bitmap ConfirmarRendirse = new Bitmap("5_ConfirmarRendirse.jpg");
            Bitmap VolverAJugar = new Bitmap("6_VolverAJugar.jpg");
            Bitmap VolverAJugar_Llave = new Bitmap("6_VolverAJugar_Llave.jpg");
            Bitmap ReintentandoSurrender = new Bitmap("7_ReintentandoSurrender.jpg");
            Bitmap Salir = new Bitmap("8_Salir.jpg");
            Bitmap ErrorConexion = new Bitmap("9_ErrorConexion.jpg");
            Bitmap Reconectar = new Bitmap("10_Reconectar.jpg");

            while (!stopLoop)
            {
                date = DateTime.Now.ToString("hh:mm:ss") + ": ";

                try
                {
                    CheckForKeyPress();
                    ForceStarGame();
                    
                    Bitmap Captura = captureScreen();
                    TemplateMatch[] matchings = tm.ProcessImage(BuscarPartida, Captura);

                    if (status == "waiting" && tm.ProcessImage(BuscarPartida, Captura)[0].Similarity > 0.94f)
                        searchGame();

                    if (tm.ProcessImage(PartidaEncontrada, Captura)[0].Similarity > 0.95f)
                        gameFound();

                    if (tm.ProcessImage(VolverAJugar_Llave, Captura)[0].Similarity > 0.95f)
                        playAgain();

                    if (tm.ProcessImage(VolverAJugar, Captura)[0].Similarity > 0.95f)
                        playAgain();

                    if (tm.ProcessImage(PartidaIniciada, Captura)[0].Similarity > 0.95f)
                        startGame();

                    if (tm.ProcessImage(ValidarPartidaIniciada, Captura)[0].Similarity > 0.93f)
                        startGame();

                    matchings = waiting( matchings);

                }
                catch (System.ComponentModel.Win32Exception exception)
                {
                    Console.WriteLine(date + " Wrong screen resolution");
                }
                Thread.Sleep(timeThread);
            }
            Console.WriteLine("Bucle detenido");
        }

        static void searchGame()
        {
            Console.WriteLine(date + "Buscando Partida");
            clickMouse("LEFT", 584, 691);
            timeThread = 3000;
        }

        static void gameFound()
        {
            Console.WriteLine(date + "Partida Encontrada");
            clickMouse("LEFT", 683, 560);
            timeThread = 0;
        }

        static void startGame()
        {
            Console.WriteLine(date + "Partida Iniciada");
            Thread.Sleep(10000);
            buyOnePiece(410);
            Thread.Sleep(60000);
            buyOnePiece(410);
            Thread.Sleep(60000);

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(date + "Iteracion: " + i);

                updateLevel();
                walkInBoard();
                buyMultiPieces();
                Thread.Sleep(60000);
            }

            surrenderGame();
        }

        static void updateLevel()
        {
            Console.WriteLine(date + "Subiendo Nivel");
            for (int i = 0; i < 2; i++)
            {
                clickMouse("LEFT", 265, 684);
                Thread.Sleep(500);
            }
        }

        static void walkInBoard()
        {
            Console.WriteLine(date + " Caminando");
            clickMouse("RIGHT", 864, 439);
            Thread.Sleep(2000);
            clickMouse("RIGHT", 467, 274);
            Thread.Sleep(2000);
            clickMouse("RIGHT", 897, 241);
        }

        static void buyOnePiece(int x)
        {
            Console.WriteLine(date + " Comprando Pieza");
            for (int i = 0; i < 2; i++)
            {
                clickMouse("LEFT", x, 700);
                Thread.Sleep(300);
            }
            Thread.Sleep(1000);
        }

        static void buyMultiPieces()
        {
            buyOnePiece(410);
            buyOnePiece(550);
            buyOnePiece(700);
            buyOnePiece(850);        
        }

        static void surrenderGame()
        {
            Console.WriteLine(date + "Surrender");

            clickMouse("LEFT", 1182, 12);
            Thread.Sleep(1000);

            clickMouse("LEFT", 550, 600);
            Thread.Sleep(1000);

            confirmSurrenderGame();
        }

        static void confirmSurrenderGame()
        {
            Console.WriteLine(date + "Confirmando Surrender");
            clickMouse("LEFT", 600, 350);
            Thread.Sleep(1000);
        }

        static void playAgain()
        {
            Console.WriteLine(date + "Jugar de Nuevo");
            clickMouse("LEFT",  580, 690);
        }


        static TemplateMatch[] waiting(TemplateMatch[] matchings)
        {
            Console.WriteLine(date + " Esperando...");
            matchings = null;
            GC.Collect();
            status = "waiting";
            return null;
        }

        static void clickMouse(string stringButton,  int x , int y)
        {
            Cursor.Position = new Point(x, y);
            auto.MouseClick(stringButton, x, y, 1, -1);
        }

        static Bitmap captureScreen()
        {
            Bitmap captura = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(captura))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            }
            return captura;
        }

        static void CheckForKeyPress()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.O)
                {
                    Console.WriteLine("Tecla 'O' detectada. Deteniendo el bucle.");
                    stopLoop = true;
                }
            }
        }

        async static void ForceStarGame()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.P)
                {
                    timeThread = 3000;
                    Console.WriteLine("Tecla 'P' detectada. Forzando Partida Iniciada");
                    startGame();
                }
            }
        }
    }
}
