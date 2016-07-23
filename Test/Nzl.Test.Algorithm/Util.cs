namespace Nzl.Test.Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// The util class.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int[] GetRandomizedArray(int size)
        {
            int[] numbers = new int[size];
            Random ran = new Random();
            for (int i = 0; i < size; i++)
            {
                numbers[i] = ran.Next(0, size * 4);
            }

            return numbers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int[] GetAscArray(int size)
        {
            int[] numbers = new int[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = i;
            }

            return numbers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int[] GetDscArray(int size)
        {
            int[] numbers = new int[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = size - i;
            }

            return numbers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int[] GetRepArray(int size, int maxReps)
        {
            int[] numbers = new int[size];
            Random ran = new Random();
            for (int i = 0; i < size; i++)
            {
                int reps = ran.Next(1, maxReps);
                int num = ran.Next(0, size * 4);
                for (int j = 0; j < reps && i < size; j++)
                {
                    numbers[i++] = num;
                }
            }

            return numbers;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool CheckOrder<T>(T[] array, int size)
            where T : IComparable<T>
        {
            for (int i = 0; i < size - 2; i++)
            {
                if (array[i].CompareTo(array[i + 1]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool CheckOrder(List<object> list)
        {
            for (int i = 0; i < list.Count - 2; i++)
            {
                if ((int)list[i] > (int)list[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 根据枚举类型返回类型中的所有值，文本及描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns>返回三列数组，第0列为Description,第1列为Value，第2列为Text</returns>
        public static List<string[]> GetEnumInfor(Type type)
        {
            List<string[]> Strs = new List<string[]>();
            FieldInfo[] fields = type.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                string[] strEnum = new string[3];
                FieldInfo field = fields[i];
                //值列
                strEnum[1] = ((int)Enum.Parse(type, field.Name)).ToString();
                //文本列赋值
                strEnum[2] = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    strEnum[0] = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    strEnum[0] = da.Description;
                }

                Strs.Add(strEnum);
            }

            return Strs;
        }

        //// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        public static string GetEnumDescription(object enumSubitem)
        {
            enumSubitem = (Enum)enumSubitem;
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);

            if (fieldinfo != null)
            {

                Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (objs == null || objs.Length == 0)
                {
                    return strValue;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            else
            {
                return enumSubitem.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seq1"></param>
        /// <param name="seq2"></param>
        /// <returns></returns>
        public static bool IsContains<T>(T[] seq1, T[] seq2)
            where T : IComparable<T>
        {
            if (seq1 != null && seq2 != null)
            {
                int cusorSeq1 = 0;
                int cusorSeq2 = 0;
                while (true)
                {
                    if (cusorSeq1 >= seq1.Length || cusorSeq2 >= seq2.Length)
                    {
                        break;
                    }

                    if (seq1[cusorSeq1++].CompareTo(seq2[cusorSeq2]) == 0)
                    {
                        cusorSeq2++;
                    }
                }

                return cusorSeq2 >= seq2.Length;
            }

            return false;
        }
    }
}
