namespace Nzl.Smth.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public static class MessageFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Message CreateMessage(string source, string detail)
        {
            return CreateMessage(DateTime.Now, source, detail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Message CreateMessage(DateTime datetime, string source, string detail)
        {
            return CreateMessage(MessageType.Information, datetime, source, detail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Message CreateMessage(MessageType type, DateTime datetime, string source, string detail)
        {
            Message msg = new Message();
            msg.Type = type;
            msg.DateTime = datetime;
            msg.Source = source;
            msg.Detail = detail;
            return msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Message CreateMessage(Exception e)
        {
            return e == null ? null : CreateMessage(MessageType.Exception, DateTime.Now, e.Source, GetExceptionMsg(e, e.ToString()));
        }

        /// <summary> 
        /// 生成自定义异常消息 
        /// </summary> 
        /// <param name="ex">异常对象</param> 
        /// <param name="backStr">备用异常消息：当ex为null时有效</param> 
        /// <returns>异常字符串文本</returns> 
        private static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************Exception****************************");
            sb.AppendLine("[Time]\t" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("[Exception Type]\t" + ex.GetType().Name);
                sb.AppendLine("[Exception Message]\t" + ex.Message);
                sb.AppendLine("[Stack information]\t" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("[Unhandled exception]\t" + backStr);
            }
            sb.AppendLine("*****************************************************************");
            return sb.ToString();
        }
    }
}
