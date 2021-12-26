using System;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;

namespace RemoteAddressDwonloader
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IpGetter v.1.0.0.0\nBy EachOther Tech");
            bool restart = false;
            if (!File.Exists(@".\time.txt"))
            {
                File.Create(@".\time.txt").Close();
                restart = true;
            }
            if (!File.Exists(@".\serverIp.txt"))
            {
                File.Create(@".\serverIp.txt").Close();
                restart = true;
            }
            if (!File.Exists(@".\Password.txt"))
            {
                File.Create(@".\Password.txt").Close();
                restart = true;
            }
            if (restart)
            {
                Console.WriteLine("Please set the opreations in txt files.Exiting now.Press any key to close.");
                Console.ReadKey();
            }
            else
            {
                string Time = File.ReadAllText(@".\time.txt");
                int timeMs = Convert.ToInt32(Time);
                string Server = File.ReadAllText(@".\serverIp.txt");
                string Passwd = File.ReadAllText(@".\Password.txt");
                Console.WriteLine(Server + "/api/ipgetter/getremoteip?password=" + Passwd);
                string ip = Get(Server + "/api/ipgetter/getremoteip?password=" + Passwd);
                if (ip == "403")
                {
                    Console.WriteLine("Failed");
                    return;
                }
                Console.WriteLine(ip + " is downloaded from the server");
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Enabled = true;
                timer.Interval = timeMs; //执行间隔时间,单位为毫秒; 这里实际间隔为10分钟  
                timer.Start();
                timer.Elapsed += new System.Timers.ElapsedEventHandler(Send);
                Console.ReadKey();
            }
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void Send(object source, ElapsedEventArgs e)
        {
            string Time = File.ReadAllText(@".\time.txt");
            int timeMs = Convert.ToInt32(Time);
            string Server = File.ReadAllText(@".\serverIp.txt");
            string Passwd = File.ReadAllText(@".\Password.txt");
            string ip = Get(Server + "/api/ipgetter/getremoteip?password=" + Passwd);
            if (ip == "403")
            {
                Console.WriteLine("Failed");
                return;
            }
            Console.WriteLine(ip + " is downloaded from the server");
        }
        /// <summary>
        /// 指定Url地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求链接地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }
    }
}