﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging;
using System.Drawing.Imaging;
using System.Threading;
using AutoItX3Lib;

namespace Bot
{
    class Program
    {
      

        static void Main(string[] args)
        {
            //Variables
            AutoItX3 auto = new AutoItX3();
            AutoIt AIT = new AutoIt();
           
            int Numero = 0;
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
            Bitmap BuscarPartida = new Bitmap("1_BuscarPartida.jpg");
            Bitmap PartidaEncontrada = new Bitmap("2_PartidaEncontrada.jpg");
            Bitmap PartidaIniciada = new Bitmap("3_PartidaIniciada.jpg");
            Bitmap Rendirse = new Bitmap("4_Rendirse.jpg");
            Bitmap ConfirmarRendirse = new Bitmap("5_ConfirmarRendirse.jpg");
            Bitmap VolverAJugar = new Bitmap("6_VolverAJugar.jpg");
            Bitmap ReintentandoSurrender = new Bitmap("7_ReintentandoSurrender.jpg");
            Bitmap Salir = new Bitmap("8_Salir.jpg");
            Bitmap ErrorConexion = new Bitmap("9_ErrorConexion.jpg");
            Bitmap Reconectar = new Bitmap("10_Reconectar.jpg");

            //Ciclo
            while (Numero == 0)
            {
                var fecha = DateTime.Now.ToString("hh:mm:ss");
                try
                {
                    Bitmap Captura = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
                    Graphics g = Graphics.FromImage(Captura);

                    g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                    //Captura.Save("captura.jpg");  // Guardamos la imagen

                    //Buscamos la partida
                    TemplateMatch[] matchings = tm.ProcessImage(BuscarPartida, Captura);
                    if (matchings[0].Similarity > 0.94f)
                    {
                        Console.WriteLine(fecha + " Buscando Partida");
                        Cursor.Position = new Point(584, 691);
                        auto.MouseClick("LEFT", 584, 691, 1, -1);
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        //Encontramos la Paritda y la aceptamos
                        matchings = tm.ProcessImage(PartidaEncontrada, Captura);
                        if (matchings[0].Similarity > 0.95f)
                        {
                            Console.WriteLine(fecha + " Partida Encontrada");
                            Cursor.Position = new Point(683, 560);
                            auto.MouseClick("LEFT", 683, 560, 1, -1);
                        }
                        else
                        {
                            //Detectamos que la partida fue iniciada con normalidad
                            matchings = tm.ProcessImage(PartidaIniciada, Captura);
                            if (matchings[0].Similarity > 0.91f)
                            {
                                
                                fecha = DateTime.Now.ToString("hh:mm:ss");
                                Console.WriteLine(fecha + " Partida Iniciada");
                                //Esperamos a la ruleta
                                Thread.Sleep(10000);
                                //Caminamos
                                fecha = DateTime.Now.ToString("hh:mm:ss");
                                Console.WriteLine(fecha + " Eligiendo campeon");
                                auto.MouseClick("RIGHT", 686, 222, 1, -1);
                                //Esperamos para comprar campeones
                                Thread.Sleep(60000);
                                //Compramos 2 Campeones
                                fecha = DateTime.Now.ToString("hh:mm:ss");
                                Console.WriteLine(fecha + " Comprando Campeones");
                                for (int i = 0; i < 6; i++)
                                {
                                    auto.MouseClick("LEFT", 434, 666, 1, -1);
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(1000);
                                for (int i = 0; i < 6; i++)
                                {
                                    auto.MouseClick("LEFT", 562, 666, 1, -1);
                                    Thread.Sleep(100);
                                }                               
                                Thread.Sleep(60000);

                                //Subimos de Nivel
                                for (int h=0; h<12; h++) {
                                //for (int h=0; h<14; h++) { 
                                fecha = DateTime.Now.ToString("hh:mm:ss");
                                Console.WriteLine(fecha + " Subiendo de Nivel, Iteracion:" + h);                               
                                for (int i = 0; i < 2; i++)
                                {
                                    auto.MouseClick("LEFT", 300, 700, 1, -1);
                                    Thread.Sleep(600);
                                }
                                //Compramos 4 Campeones mas
                                Console.WriteLine(fecha + " Comprando Campeones");
                                for (int i = 0; i < 6; i++)
                                {
                                    auto.MouseClick("LEFT", 434, 666, 1, -1);
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(1100);
                                for (int i = 0; i < 7; i++)
                                {
                                    auto.MouseClick("LEFT", 562, 666, 1, -1);
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(1100);
                                for (int i = 0; i < 7; i++)
                                {
                                    auto.MouseClick("LEFT", 696, 666, 1, -1);
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(1100);
                                for (int i = 0; i < 7; i++)
                                {
                                    auto.MouseClick("LEFT", 830, 666, 1, -1);
                                    Thread.Sleep(100);
                                }                               
                                Thread.Sleep(1000);
                                //Caminamos por el Mapa
                                Console.WriteLine(fecha + " Caminando");
                                auto.MouseClick("RIGHT", 864, 439, 1, -1);
                                Thread.Sleep(1000);
                                auto.MouseClick("RIGHT", 467, 274, 1, -1);
                                Thread.Sleep(1000);
                                auto.MouseClick("RIGHT", 897, 241, 1, -1);
                                fecha = DateTime.Now.ToString("hh:mm:ss");
                                //Esperar Minuto 10
                                fecha = DateTime.Now.ToString("hh:mm:ss");
                                Thread.Sleep(65000);
                                }
                                Console.WriteLine(fecha + " Intentando Tirar Surrender");
                                for (int i = 0; i < 10; i++)
                                {
                                  Cursor.Position = new Point(1185, 598);
                                  auto.MouseClick("LEFT", 1185, 598, 1, -1);
                                    Thread.Sleep(100);
                                }
                                for (int i = 0; i < 3; i++)
                                {
                                    auto.MouseClick("LEFT", 494, 685, 1, -1);
                                }
                                Thread.Sleep(1000);
                                Console.WriteLine(fecha + " Corfirmando Surrender ");
                                for (int i = 0; i < 10; i++)
                                {
                                    auto.MouseClick("LEFT", 617, 428, 1, -1);
                                    Thread.Sleep(100);
                                }
                                Thread.Sleep(5000);
                               // auto.MouseClick("LEFT", 617, 428, 1, -1);
                               Thread.Sleep(1000);
                              //  auto.MouseClick("LEFT", 617, 428, 1, -1);
                               // Thread.Sleep(6000);
                                auto.MouseClick("LEFT", 575, 691, 1, -1);
                            }
                            else
                            {
                                //Precionando Rendirse en el menu ESC
                                matchings = tm.ProcessImage(Rendirse, Captura);
                                if (matchings[0].Similarity > 0.95f)
                                {
                                    auto.MouseClick("LEFT", 494, 685, 1, -1);
                                    Console.WriteLine(fecha + " Corfirmando Surrender ");
                                    for (int i = 0; i < 10; i++)
                                    {
                                        auto.MouseClick("LEFT", 617, 428, 1, -1);
                                        Thread.Sleep(100);
                                    }
                                    Thread.Sleep(5000);
                                    auto.MouseClick("LEFT", 617, 428, 1, -1);
                                    Thread.Sleep(1000);
                                    auto.MouseClick("LEFT", 617, 428, 1, -1);
                                    Thread.Sleep(6000);
                                    auto.MouseClick("LEFT", 575, 691, 1, -1);
                                }
                                else
                                {
                                    //Confirmamos que queremos rendirnos
                                    matchings = tm.ProcessImage(ConfirmarRendirse, Captura);
                                    if (matchings[0].Similarity > 0.95f)
                                    {
                                        Console.WriteLine(fecha + " Corfirmando Surrender ");
                                        for (int i = 0; i < 10; i++)
                                        {
                                            auto.MouseClick("LEFT", 617, 428, 1, -1);
                                            Thread.Sleep(100);
                                        }
                                        Thread.Sleep(5000);
                                        auto.MouseClick("LEFT", 617, 428, 1, -1);
                                        Thread.Sleep(1000);
                                        auto.MouseClick("LEFT", 617, 428, 1, -1);
                                        Thread.Sleep(6000);
                                        auto.MouseClick("LEFT", 575, 691, 1, -1);
                                    }
                                    else
                                    {
                                        //Confirmamos que volveremos a jugar
                                        matchings = tm.ProcessImage(VolverAJugar, Captura);
                                        //if (matchings[0].Similarity > 0.94f)
                                        if (matchings[0].Similarity > 0.91f)
                                        {
                                            Thread.Sleep(1000);
                                            Console.WriteLine(fecha +" VolverAJugar ");
                                            //auto.MouseClick("LEFT", 567, 371, 1, -1);
                                            //Thread.Sleep(1000);
                                            Cursor.Position = new Point(575, 691);
                                            auto.MouseClick("LEFT", 575, 691, 1, -1);

                                        }
                                        else
                                        {
                                            //En caso de ser eliminados sin poder tirar surrender, precionar boton salir
                                            matchings = tm.ProcessImage(Salir, Captura);
                                            if (matchings[0].Similarity > 0.91f)
                                            {
                                                Console.WriteLine(fecha + " Salir");
                                                Cursor.Position = new Point(600, 371);
                                                auto.MouseClick("LEFT", 600, 371, 1, -1);
                                                Thread.Sleep(300);
                                                auto.MouseClick("LEFT", 600, 371, 1, -1);
                                                Thread.Sleep(300);
                                                auto.MouseClick("LEFT", 600, 371, 1, -1);
                                            }
                                            else
                                            {
                                                //En caso de error en el surrender reintentar hacerlo
                                                matchings = tm.ProcessImage(ReintentandoSurrender, Captura);
                                                if (matchings[0].Similarity > 0.98f)
                                                {
                                                    Console.WriteLine(fecha + " Reintentando Surrender");
                                                    for (int i = 0; i < 10; i++)
                                                    {
                                                        Cursor.Position = new Point(1185, 598);
                                                        auto.MouseClick("LEFT", 1185, 598, 1, -1);
                                                        Thread.Sleep(100);
                                                    }
                                                }
                                                else
                                                {
                                                    //En caso de desconexion reintentar conectar el juego
                                                    matchings = tm.ProcessImage(ErrorConexion, Captura);
                                                    if (matchings[0].Similarity > 0.96f)
                                                    {
                                                        Console.WriteLine(fecha + " Error de Conexion");                                                      
                                                            Cursor.Position = new Point(743, 444);
                                                            auto.MouseClick("LEFT", 743, 444, 1, -1);
                                                            Thread.Sleep(1000);
                                                            Console.WriteLine(fecha + " Reconectando");
                                                            auto.MouseClick("LEFT", 567, 371, 1, -1);
                                                            Thread.Sleep(100);
                                                    }
                                                    else {
                                                        //Confirmar la reconexion
                                                        matchings = tm.ProcessImage(Reconectar, Captura);
                                                        if (matchings[0].Similarity > 0.96f)
                                                        {
                                                            Thread.Sleep(1000);
                                                            Console.WriteLine(fecha + " Reconectando");
                                                           // auto.MouseClick("LEFT", 567, 371, 1, -1);
                                                            Thread.Sleep(1000);
                                                            Cursor.Position = new Point(575, 691);
                                                            auto.MouseClick("LEFT", 575, 691, 1, -1);
                                                        }
                                                        else {
                                                           
                                                                //Ninguna de las anteriores entonces .....
                                                            Console.WriteLine(fecha + " Esperando...");                                                           
                                                            matchings = null;
                                                            GC.Collect();
                                                            }                                                       
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
                catch (System.ComponentModel.Win32Exception exception)
                {
                    Console.WriteLine(fecha + " Hola Soy un error :'v o eso me decia mi mama :c");
                    Console.WriteLine(fecha + " Fuera bromas tu resolucion no es la correcta lee las instrucciones");
                }
                //Thread.Sleep(1500);
            }

        }
    }
}
