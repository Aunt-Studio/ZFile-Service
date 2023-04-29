
<div align="center"><image width="140em" src="src/Title.png" /></div>
<h1 align="center">ZFile Service</h1>
<h3 align="center">Windows下使ZFile以服务运行</h3>
<h4 align="center">


</h3>

---

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

## 安装

### 作为服务安装

运行程序目录下的'install.bat'文件。

或

在安装目录打开CMD，运行

```shell
ZFile.exe -i
```

### 直接运行

双击ZFile.exe即可直接运行ZFile。

---

## Special Thanks
[![ZFile](src/zfile-horizontal.abd5aec9.jpg)](https://www.zfile.vip/)

---

[![ChatGPT](src/ChatGPT_logo.png)](https://chat.openai.com/)
