using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Nzl.Test.QueueSystem
{
    class Program
    {
        static readonly QueueSystem _sys = new QueueSystem();
        static readonly Random _random = new Random();
        static readonly int _interval = 50;
        static int _counter = 0;
        static readonly object _lockerForCounter = new object();

        static int _addCounter = 0;
        static int _breakCounter = 0;
        static int _admitCounter = 0;

        static void Main(string[] args)
        {

            TestAttendance();
            System.Console.ReadLine();
            return;

            Thread threadAdd = new Thread(Add);
            Thread threadBreak = new Thread(Break);
            Thread threadAdmit = new Thread(Admit);

            threadAdd.Start();
            threadBreak.Start();
            threadAdmit.Start();

            System.Console.ReadLine();
        }

        static void TestAttendance()
        {
            List<WorkPeroid> wpList = new List<WorkPeroid>();
            {
                WorkPeroid wp = new WorkPeroid();
                wp.Start = new DateTime(2000, 1, 1, 8, 0, 0);//08:00:00
                wp.End = new DateTime(2000, 1, 1, 12, 0, 0);//12:00:00
                wpList.Add(wp);

                wp = new WorkPeroid();
                wp.Start = new DateTime(2000, 1, 1, 14, 00, 0);//13:30:00
                wp.End = new DateTime(2000, 1, 1, 17, 0, 0);//17:00:00
                wpList.Add(wp);

                wp = new WorkPeroid();
                wp.Start = new DateTime(2000, 1, 1, 19, 0, 0);//19:00:00
                wp.End = new DateTime(2000, 1, 1, 21, 0, 0);//21:00:00
                wpList.Add(wp);
            }

            {
                foreach (WorkPeroid wp in wpList)
                {
                    System.Console.Write(wp.Start.ToString("HH:mm:ss "));
                    System.Console.Write(wp.End.ToString("HH:mm:ss "));
                }

                System.Console.WriteLine();
                List<DateTime> dtList = new List<DateTime>();
                foreach (WorkPeroid wp in wpList)
                {
                    //dtList.Add(wp.Start);
                    //dtList.Add(wp.End);
                }

                dtList.Add(new DateTime(2000, 1, 1, 7, 0, 0));
                dtList.Add(new DateTime(2000, 1, 1, 11, 59, 0));
                dtList.Add(new DateTime(2000, 1, 1, 13, 0, 0));
                dtList.Add(new DateTime(2000, 1, 1, 14, 01, 0));
                dtList.Add(new DateTime(2000, 1, 1, 17, 00, 0));
                dtList.Add(new DateTime(2000, 1, 1, 18, 00, 0));
                dtList.Add(new DateTime(2000, 1, 1, 20, 0, 0));
                dtList.Add(new DateTime(2000, 1, 1, 22, 0, 0));

                Dictionary<DateTime, AttendaceStatus> dicAS = Attendance.GetAttendaceStatus(dtList, wpList);
                foreach (DateTime dt in dtList)
                {
                    System.Console.Write(dt.ToString("HH:mm:ss "));
                }

                System.Console.WriteLine();

                foreach (KeyValuePair<DateTime, AttendaceStatus> kvp in dicAS)
                {
                    System.Console.WriteLine(kvp.Key.ToString("HH:mm:ss") + " - " + kvp.Value.ToString());
                }
            }
        }

        static bool Action(string actionFlag)
        {
            switch (actionFlag)
            {
                case "Add":
                    {
                        return _random.Next(0, 99) < 50;
                    }
                case "Break":
                    {
                        return _random.Next(0, 99) < 25;
                    }
                case "Admit":
                    {
                        return _random.Next(0, 99) < 50;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        static void PrintInfor(string action, Candidate candi)
        {
            lock (_sys)
            {
                System.Console.WriteLine("-*--*--*--*--*--*--*--*--*--*-");
                System.Console.WriteLine(_sys.ToString());
                System.Console.WriteLine(action);
                if (candi != null)
                {
                    System.Console.WriteLine("\tId:\t" + candi.Id + "\tIndex:\t" + candi.Index);
                }

                System.Console.WriteLine("add:" + _addCounter + " break:" + _breakCounter + " admit:" + _admitCounter);
                System.Console.WriteLine(" -> add - break - admit:" + (_addCounter - _breakCounter - _admitCounter).ToString());
            }
        }

        static int CreateNewCandidateNumber()
        {
            lock (_lockerForCounter)
            {
                _counter = _counter + 1;
                return _counter;
            }
        }

        static void Add()
        {
            while (true)
            {
                if (Action("Add"))
                {
                    Candidate candi = _sys.AddCandidate(CreateNewCandidateNumber().ToString());
                    if (candi != null)
                    {
                        PrintInfor("Add", candi);
                        _addCounter++;
                    }
                }

                System.Threading.Thread.Sleep(_interval);
            }
        }

        static void Break()
        {
            while (true)
            {
                if (Action("Break"))
                {
                    if (_sys.QueueSize > 1)
                    {
                        int index = _random.Next(_sys.QueueSize);
                        if (index > 0)
                        {
                            Candidate candi = _sys.BreakCandidate(index);
                            if (candi != null)
                            {
                                PrintInfor("Break", candi);
                                _breakCounter++;
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(_interval);
            }
        }

        static void Admit()
        {
            while (true)
            {
                if (Action("Admit"))
                {
                    Candidate candi = _sys.AdmitCandidate();
                    if (candi != null)
                    {
                        PrintInfor("Admit", candi);
                        _admitCounter++;
                    }
                }

                System.Threading.Thread.Sleep(_interval);
            }

        }
    }
}
