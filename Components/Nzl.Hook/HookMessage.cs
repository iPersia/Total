namespace Nzl.Hook
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class HookMessage
    {
        private HookMessageType _type = HookMessageType.Others;

        private string _msg;
        
        public HookMessage(HookMessageType type, string msg)
        {
            this._type = type;
            this._msg = msg;
        }

        public HookMessageType Type
        {
            get
            {
                return this._type;
            }
        }

        public string Message
        {
            get
            {
                return this._msg;
            }
        }
    }
}
