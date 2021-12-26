using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

namespace IpGetterApi.Controllers
{
    //路由设置
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IpGetterController : ControllerBase
    {
        /// <summary>
        /// 发送IP地址
        /// </summary>        
        /// <param name="Password">密码</param>
        /// <returns>IP地址</returns>
        [HttpGet]
        public ActionResult<string> SendIp(string Password)
        {
            if (!System.IO.File.Exists(@".\Passwd.txt"))
            {
                System.IO.File.Create(@".\Passwd.txt").Close();
            }
            string correctPasswd = System.IO.File.ReadAllText(@".\Passwd.txt");
#pragma warning disable CS8602 // 解引用可能出现空引用。
            string ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#pragma warning restore CS8602 // 解引用可能出现空引用。
            if (Password != correctPasswd)
            {
                Console.WriteLine("\n\nUser from " + ip + " used your api.But he/she entered a wrong password.\n\nIpGetterApi by EachOther Tech.\n\n");
                return "403";
            }
            if (!System.IO.File.Exists(@".\saved.txt"))
            {
                System.IO.File.Create(@".\saved.txt").Close();
            }
            System.IO.File.WriteAllText(@".\saved.txt", ip);
            Console.WriteLine("\n\nUser from "+ip+" has uploaded his/her online IP.\n\nIpGetterApi by EachOther Tech.\n\n");
            return ip;
        }
        /// <summary>
        /// 获取已存储的远程IP地址
        /// </summary>
        /// <param name="Password">密码</param>
        /// <returns>远程IP地址</returns>
        [HttpGet]
        public ActionResult<string> GetRemoteIp(string Password)
        {
            if (!System.IO.File.Exists(@".\Passwd.txt"))
            {
                System.IO.File.Create(@".\Passwd.txt").Close();
            }
#pragma warning disable CS8602 // 解引用可能出现空引用。
            string ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#pragma warning restore CS8602 // 解引用可能出现空引用。
            string correctPasswd = System.IO.File.ReadAllText(@".\Passwd.txt");
            if (Password != correctPasswd)
            {
                Console.WriteLine("\n\nUser from " + ip + " used your api.But he/she entered a wrong password.\n\nIpGetterApi by EachOther Tech.\n\n");
                return "403";
            }
            string remoteIp = System.IO.File.ReadAllText(@".\saved.txt");
            Console.WriteLine("\n\nUser from " + ip + " has gotten your online-remote api.\n\nIpGetterApi by EachOther Tech.\n\n");
            return remoteIp;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Old">旧密码</param>
        /// <param name="New">新密码</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> ChangePassword(string Old,string New)
        {
            if (!System.IO.File.Exists(@".\Passwd.txt"))
            {
                System.IO.File.Create(@".\Passwd.txt").Close();
            }
#pragma warning disable CS8602 // 解引用可能出现空引用。
            string ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#pragma warning restore CS8602 // 解引用可能出现空引用。
            string correctPasswd = System.IO.File.ReadAllText(@".\Passwd.txt");
            if (Old != correctPasswd)
            {
                Console.WriteLine("\n\nUser from " + ip + " used your change-password api.But he/she entered a wrong password.\n\nIpGetterApi by EachOther Tech.\n\n");
                return "403";
            }
            System.IO.File.WriteAllText(@".\Passwd.txt", New);
            return "Success";
        }

    }
}