
<div align="center"><image width="500em" src="src/Title.png" /></div>
<h1 align="center">ZFile Service</h1>
<h3 align="center">Windows下使ZFile以服务运行</h3>
<h4 align="center"> <a href=https://github.com/yangnuozhen/ZFile-Service/wiki>Wiki</a> | 
<a href=https://www.zfile.vip/>了解ZFile</a>


</h3>

---

## 项目介绍

ZFile Service 是一个能让基于Java的在线文件管理器[ZFile](https://www.zfile.vip/)以Windows 服务方式运行的程序。

其可以让ZFile一键运行，亦可以一键将ZFile以服务方式安装，轻松在Windows Service管理控制台控制您的ZFile。同时，软件默认安装服务时要求系统启动时自动开启服务，实现轻松配置开机自启，您也可以自行设置。

## 下载

[下载最新的Release](https://github.com/yangnuozhen/ZFile-Service/releases)

请仔细阅读Release Notes。

## 配置

打开ZFile-Service目录中的ZFile.cfg文件，你应该可以看到下列示例内容(若无请务必自行创建):

```conf
ZFilePath="C:\Users\Administrator\source\repos\ZFile\ZFile\bin\Release\zfile-release.jar"
JDK8Path="C:\Program Files\Java\jdk1.8.0_351\bin\java.exe"
ZFilePort=1234
```

以下是配置文件对照:
```conf
ZFilePath="[ZFile的Jar文件绝对目录]"
JDK8Path="[JDK8的java.exe文件绝对目录]"
ZFilePort=[ZFile运行端口]
```
>⚠注意**引号**，以及**绝对目录**。

**以上所有配置项均为必填项。**

## 安装 | 运行

### 作为服务安装

运行程序目录下的`install.bat`文件。

或

在安装目录打开CMD，运行

```shell
ZFile.exe -i
```

### 直接运行

双击ZFile.exe即可直接运行ZFile。

---
## TODO List

- [x] 将ZFile作为Windows服务运行
- [x] 双击ZFile.exe即可直接运行
- [x] 若作为控制台程序运行，将ZFile回显实时输出到控制台
- [ ] 自动输出日志到文件

## License | 许可证

[MIT © 2023-Aunt Studio](https://github.com/yangnuozhen/ZFile-Service/blob/master/LICENSE)
本仓库所有源代码和发行包遵循MIT协议。有关更多信息，请查看[LICENSE](https://github.com/yangnuozhen/ZFile-Service/blob/master/LICENSE)

---

## Special Thanks
[![ZFile](src/zfile-horizontal.abd5aec9.jpg)](https://www.zfile.vip/)

---

[![ChatGPT](src/ChatGPT_logo.png)](https://chat.openai.com/)
