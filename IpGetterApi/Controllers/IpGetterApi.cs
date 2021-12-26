using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

namespace IpGetterApi.Controllers
{
    //·������
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IpGetterController : ControllerBase
    {
        /// <summary>
        /// ����IP��ַ
        /// </summary>        
        /// <param name="Password">����</param>
        /// <returns>IP��ַ</returns>
        [HttpGet]
        public ActionResult<string> SendIp(string Password)
        {
            if (!System.IO.File.Exists(@".\Passwd.txt"))
            {
                System.IO.File.Create(@".\Passwd.txt").Close();
            }
            string correctPasswd = System.IO.File.ReadAllText(@".\Passwd.txt");
#pragma warning disable CS8602 // �����ÿ��ܳ��ֿ����á�
            string ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#pragma warning restore CS8602 // �����ÿ��ܳ��ֿ����á�
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
        /// ��ȡ�Ѵ洢��Զ��IP��ַ
        /// </summary>
        /// <param name="Password">����</param>
        /// <returns>Զ��IP��ַ</returns>
        [HttpGet]
        public ActionResult<string> GetRemoteIp(string Password)
        {
            if (!System.IO.File.Exists(@".\Passwd.txt"))
            {
                System.IO.File.Create(@".\Passwd.txt").Close();
            }
#pragma warning disable CS8602 // �����ÿ��ܳ��ֿ����á�
            string ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#pragma warning restore CS8602 // �����ÿ��ܳ��ֿ����á�
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
        /// �޸�����
        /// </summary>
        /// <param name="Old">������</param>
        /// <param name="New">������</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> ChangePassword(string Old,string New)
        {
            if (!System.IO.File.Exists(@".\Passwd.txt"))
            {
                System.IO.File.Create(@".\Passwd.txt").Close();
            }
#pragma warning disable CS8602 // �����ÿ��ܳ��ֿ����á�
            string ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
#pragma warning restore CS8602 // �����ÿ��ܳ��ֿ����á�
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