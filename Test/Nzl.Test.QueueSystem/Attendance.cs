using System;
using System.Collections.Generic;

namespace Nzl.Test.QueueSystem
{
    public class Attendance
    {
        public static Dictionary<DateTime, AttendaceStatus> GetAttendaceStatus(List<DateTime> attList, List<WorkPeroid> wpList)
        {
            Dictionary<DateTime, AttendaceStatus> dicAS = null;
            if (attList != null && wpList != null)
            {
                if (wpList.Count > 0)
                {
                    dicAS = new Dictionary<DateTime, AttendaceStatus>();
                    Dictionary<DateTime, List<DateTime>> dicWP = new Dictionary<DateTime, List<DateTime>>();
                    List<DateTime> dtMiddle = new List<DateTime>();
                    foreach (WorkPeroid wp in wpList)
                    {
                        dicWP.Add(wp.Start, new List<DateTime>());
                        dicWP.Add(wp.End, new List<DateTime>());
                    }

                    foreach (DateTime dt in attList)
                    {
                        //特殊处理，时间点正好处于两个工作时间段终止与开始的中间位置。
                        //后续进行处理。
                        bool isMiddle = false;
                        for (int i = 1; i < wpList.Count; i++)
                        {
                            if (GetTimeValue(dt) * 2 == GetTimeValue(wpList[i].Start) + GetTimeValue(wpList[i - 1].End))
                            {
                                dtMiddle.Add(dt);
                                isMiddle = true;
                                break;
                            }
                        }

                        //就近分配到时间点
                        if (isMiddle == false)
                        {
                            dicWP[FindNearest(dt, wpList)].Add(dt);
                        }
                    }

                    foreach (WorkPeroid wp in wpList)
                    {
                        //起始点判断
                        AttendaceStatus asTemp = AttendaceStatus.Absence;
                        if (dicWP[wp.Start].Count > 0)
                        {
                            bool isLate = true;
                            foreach (DateTime dt in dicWP[wp.Start])
                            {
                                if (GetTimeValue(dt) <= GetTimeValue(wp.Start))
                                {
                                    isLate = false;
                                    break;
                                }
                            }

                            asTemp = isLate ? AttendaceStatus.Late : AttendaceStatus.Normal;
                        }

                        dicAS.Add(wp.Start, asTemp);

                        //终止点判断
                        asTemp = AttendaceStatus.Absence;
                        if (dicWP[wp.End].Count > 0)
                        {
                            bool isLeaveEarly = true;
                            foreach (DateTime dt in dicWP[wp.End])
                            {
                                if (GetTimeValue(dt) >= GetTimeValue(wp.End))
                                {
                                    isLeaveEarly = false;
                                    break;
                                }
                            }

                            asTemp = isLeaveEarly ? AttendaceStatus.LeaveEarly : AttendaceStatus.Normal;
                        }

                        dicAS.Add(wp.End, asTemp);
                    }

                    ////特殊处理，时间点正好处于两个工作时间段终止与开始的中间位置。
                    for (int i = 1; i < wpList.Count; i++)
                    {
                        foreach (DateTime dt in dtMiddle)
                        {
                            if (GetTimeValue(dt) * 2 == GetTimeValue(wpList[i].Start) + GetTimeValue(wpList[i - 1].End))
                            {
                                AttendaceStatus fEndAS = dicAS[wpList[i-1].End];
                                AttendaceStatus sStartAS = dicAS[wpList[i].Start];
                                if (fEndAS == AttendaceStatus.Absence || sStartAS == AttendaceStatus.Absence)
                                {
                                    if (fEndAS == AttendaceStatus.Absence)
                                    {
                                        dicAS[wpList[i - 1].End] = AttendaceStatus.Normal;
                                    }
                                    else
                                    {
                                        dicAS[wpList[i].Start] = AttendaceStatus.Normal;
                                    }

                                    break;
                                }

                                if (fEndAS == AttendaceStatus.Normal || sStartAS == AttendaceStatus.Normal)
                                {
                                    dicAS[wpList[i - 1].End] = AttendaceStatus.Normal;
                                    dicAS[wpList[i].Start] = AttendaceStatus.Normal;
                                    break;
                                }

                                dicAS[wpList[i - 1].End] = AttendaceStatus.Normal;
                            }
                        }
                    }
                }
            }

            return dicAS;
        }

        /// <summary>
        /// Find nearest date-time.
        /// </summary>
        /// <param name="dt">The date time.</param>
        /// <param name="wpList">The work period list.</param>
        /// <returns>The nearest date time.</returns>
        private static DateTime FindNearest(DateTime dt, List<WorkPeroid> wpList)
        {
            int dtValue = GetTimeValue(dt);
            int dtMinDis = int.MaxValue;
            DateTime dtMin = DateTime.Now;
            foreach (WorkPeroid wp in wpList)
            {
                int dtDis = Math.Abs(GetTimeValue(wp.Start) - dtValue);
                if (dtDis < dtMinDis)
                {
                    dtMinDis = dtDis;
                    dtMin = wp.Start;
                }

                dtDis = Math.Abs(GetTimeValue(wp.End) - dtValue);
                if (dtDis < dtMinDis)
                {
                    dtMinDis = dtDis;
                    dtMin = wp.End;
                }
            }

            return dtMin;
        }

        /// <summary>
        /// Compare method of work period.
        /// </summary>
        /// <param name="l">The left value.</param>
        /// <param name="r">The right value.</param>
        /// <returns>The comparision result.</returns>
        private static int WorkPeroidSort(WorkPeroid l, WorkPeroid r)
        {
            return TimeSort(l.Start, r.Start);
        }

        /// <summary>
        /// Compare method of time.
        /// </summary>
        /// <param name="l">The left value.</param>
        /// <param name="r">The right value.</param>
        /// <returns>The comparision result.</returns>
        private static int TimeSort(DateTime l, DateTime r)
        {
            return (GetTimeValue(l)).CompareTo(GetTimeValue(r));
        }

        /// <summary>
        /// Compare method of date.
        /// </summary>
        /// <param name="l">The left value.</param>
        /// <param name="r">The right value.</param>
        /// <returns>The comparision result.</returns>
        private static int DateSort(DateTime l, DateTime r)
        {
            return (GetDateValue(l)).CompareTo(GetDateValue(r));
        }


        /// <summary>
        /// Get the date value in integer.
        /// </summary>
        /// <param name="dt">The date-time.</param>
        /// <returns>The value.</returns>
        private static int GetDateValue(DateTime dt)
        {
            return dt.Year * 10000 + dt.Month * 100 + dt.Day;
        }

        /// <summary>
        /// Get the time value in integer.
        /// </summary>
        /// <param name="dt">The date-time.</param>
        /// <returns>The value.</returns>
        private static int GetTimeValue(DateTime dt)
        {
            return dt.Hour * 3600 + dt.Minute * 60 + dt.Second;
        }
    }

    public enum AttendaceStatus
    {
        Unknown = 0,
        Normal = 1,
        Late = 2,
        LeaveEarly = 3,
        Absence = 4
    }

    public class WorkPeroid
    {
        public DateTime Start
        {
            get;
            set;
        }

        public DateTime End
        {
            get;
            set;
        }
    }
}
