namespace Nzl.Web.Util
{    
    using System;
    using System.Net;
    using System.Management;
    using System.Runtime.InteropServices;

    public static class IPConfig
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        //获取本机的IP

        public static string getLocalIP()
        {
            string strHostName = Dns.GetHostName(); //得到本机的主机名

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP

            string strAddr = ipEntry.AddressList[0].ToString();
            return (strAddr);
        }
        //获取本机的MAC

        public static string getLocalMac()
        {
            string mac = null;
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return (mac);
        }

        //获取远程主机IP

        public static string[] getRemoteIP(string RemoteHostName)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(RemoteHostName);
            IPAddress[] IpAddr = ipEntry.AddressList;
            string[] strAddr = new string[IpAddr.Length];
            for (int i = 0; i < IpAddr.Length; i++)
            {
                strAddr[i] = IpAddr[i].ToString();
            }
            return (strAddr);
        }
        //获取远程主机MAC

        public static string getRemoteMac(string localIP, string remoteIP)
        {
            Int32 ldest = inet_addr(remoteIP); //目的ip

            Int32 lhost = inet_addr(localIP); //本地ip


            try
            {
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                return Convert.ToString(macinfo, 16);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error:{0}", err.Message);
            }
            return 0.ToString();
        }
    }
}