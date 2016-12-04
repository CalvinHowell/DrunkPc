using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Media;
//Application Name: Drunk Pc
//Description: Generate random mouse and keyboard movements and inputs to confuse users
//Threads
//System.Windows.Forms namespace and assembly
//Hidden application


namespace DrunkPc
{
    class Program
    {
        public static Random _random = new Random();
        public static int _startupDelaySec = 10, _totalDurationSec = 10;

        /// <summary>
        /// Entry point for prank app
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("DrunkPc Prank Application by: Cameron Howell");
            if(args.Length >= 2)
            {
                _startupDelaySec = Convert.ToInt32(args[0]);
                _totalDurationSec = Convert.ToInt32(args[1]);
            }

            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyThread = new Thread(new ThreadStart(DrunkKeyThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            DateTime future = DateTime.Now.AddSeconds(_startupDelaySec);
            Console.WriteLine("Waiting 10 seconds before starting threads");
            {
                while (future > DateTime.Now)
                {
                    Thread.Sleep(1000);
                }
            }

            drunkMouseThread.Start();
            drunkKeyThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSec);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Terminating all threads");
            drunkMouseThread.Abort();
            drunkKeyThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();
        }

            #region Thread functions
        public static void DrunkMouseThread()
        {
            int moveX, moveY;

            while (true)
            {
                if (_random.Next(100) > 50)
                {
                    moveX = _random.Next(10) - 10;
                    moveY = _random.Next(10) - 10;

                    Cursor.Position = new System.Drawing.Point(Cursor.Position.X - moveX, Cursor.Position.Y - moveY);

                    Thread.Sleep(5000);
                }
            }
        }

        public static void DrunkKeyThread()
        {
            while (true)
            {
                if (_random.Next(100) > 79)
                {

                    char key = (char)(_random.Next(25) + 65);

                    if (_random.Next(2) == 0) { key = Char.ToLower(key); }

                    SendKeys.SendWait(key.ToString());
                    Thread.Sleep(_random.Next(10000));
                }
            }
        }

        public static void DrunkSoundThread()
        {
            while (true)
            {
                if (_random.Next(100) > 79)
                {
                    switch(_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }
                    
                }
                Thread.Sleep(10000);
            }
        }

        public static void DrunkPopupThread()
        {
            while (true)
            { 
                if(_random.Next(100) > 79)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show("Internet explorer ahs stopped working", "Internet Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show("Your system is running low on resources", "Microsoft Windows", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                Thread.Sleep(10000);
            }
        }
        #endregion
    }
}