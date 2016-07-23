namespace Nzl.Hook
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public static class HookMessageExchanger
    {
        #region Variables.
        /// <summary>
        /// The message type storage length.
        /// </summary>
        private static int _msgTypeStorageLength = 8;

        /// <summary>
        /// The data storage length.
        /// </summary>
        private static int _msgLenStorageLength = 8;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static string GetString(SharedMemory sm, int start, int count)
        {
            if (sm != null)
            {
                char[] totalMsg = new char[count];
                if (sm.Read(ref totalMsg, start, count) == 0)
                {
                    return new string(totalMsg);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public static HookMessage Read(SharedMemory sm)
        {
            try
            {
                if (sm != null)
                {
                    string lensInfor = GetString(sm, 0, _msgTypeStorageLength + _msgLenStorageLength);
                    HookMessageType smmType = (HookMessageType)Convert.ToInt32(lensInfor.Substring(0, _msgTypeStorageLength).TrimEnd());
                    int msgLen = Convert.ToInt32(lensInfor.Substring(_msgTypeStorageLength).TrimEnd());
                    string smmMsg = GetString(sm, 0, _msgTypeStorageLength + _msgLenStorageLength + msgLen);
                    return new HookMessage(smmType, smmMsg.Substring(_msgTypeStorageLength + _msgLenStorageLength));
                }

                return null;
            }
            catch
            {
                return null;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static void Write(SharedMemory sm, HookMessage smm)
        {
            if (sm != null && smm != null && smm.Message != null)
            {
                string logInfor = ((int)smm.Type).ToString().PadRight(_msgTypeStorageLength)
                                + (smm.Message.Length).ToString().PadRight(_msgTypeStorageLength)
                                + smm.Message;
                sm.Write(logInfor.ToCharArray(), 0, logInfor.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infor"></param>
        /// <param name="messType"></param>
        /// <returns></returns>
        public static string Write(HookMessageType messType, string infor)
        {
            string result = string.Empty;
            result += ((int)messType).ToString().PadRight(_msgTypeStorageLength); 
            result += infor.Length.ToString().PadRight(_msgLenStorageLength);
            result += infor;
            return result;
        }
    }
}
