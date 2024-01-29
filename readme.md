IpGetterApi-一款自动上传/获取ip的工具

# 配置方法
1.首先在ip固定的服务器上安装Asp.NET Core 6.0.
2.运行服务端(IpGetterApi),命令行"dotnet IpGetterApi.dll --urls https://*:5000".
3.运行发送端(IpAutoSenter),电脑需要安装.NET 6.0,双击运行exe.
4.编辑发送端目录下的Server文件,格式:"https://0.0.0.0:5000".
5.编辑发送端目录下的Password和Time文件,Time是发送间隔,单位毫秒,格式:"6000",一般填10000到600000均可.
6.重新运行发送端,出现"x.x.x.x is sent to the server",服务端出现提示即配置成功.
7.使用接收端重复3到6步.