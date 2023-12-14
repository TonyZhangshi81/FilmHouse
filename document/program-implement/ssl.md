
## 环境：
1.安装Openssl (Win32/Win64 OpenSSL Installer for Windows - Shining Light Productions (slproweb.com))  
2.设置环境变量：  
![环境](.\png\01.png)
![环境](.\png\02.png)  

配置文件修改：(`C:\Program Files\OpenSSL-Win64\bin\openssl.cfg`)  
`dir = ./demoCA  => dir = E:/localshare/share/MyProjects/source/demoCA`  
注意：dir在cfg内容中有两处出现。  
（以避免因为文件系统访问权限受阻而无法作成文件）  
另外，环境还需要做以下准备：  
在ca证书作成目录内需要有newcerts文件夹和index.txt以及serial  
newcerts文件夹(初始内容为空)：用于存放pem证书  
index.txt文件(初始内容为空)：内容是subj内容  
serial文件(初始内容是不超过20位的16进制序列)：由CA维护的为它所发的每个证书分配的一的序列号，用来追踪和撤销证书  
  

## 生成CA根证书
生成根CA密钥 (使用-des3 对密码123456进行加密并作成一个2024位长度的私钥文件)  
`openssl genrsa -des3 -out dev.key 2048`  
生成根证书并且自签名 (用自己的私钥和证书申请文件生成自己签名的证书，俗称自签名证书，这里可以理解为根证书)  
`openssl req -new -x509 -days 365 -key dev.key -out dev.crt -subj "/C=CN/ST=shanghai/L=shanghai/O=Home/CN=FilmHouse.com"`  

## 证书转换
将PEM转换为PKCS12 【使用私钥（key）和证书（crt），并将它们组合成一个PKCS12文件（pfx）】  
`openssl pkcs12 -inkey dev.key -in dev.crt -export -out dev.pfx`  

## nginx ssl 部署
将key、crt、pfx文件放置于nginx环境下的ssl目录内  
![环境](.\png\06.png)  

## nginx启动
nginx配置验证  
`nginx -t`  
![环境](.\png\07.png)  

启动  
`\tye-app\nginx_reload.bat`  
`\tye-app\nginx_start.bat`  

## tye启动
`\tye-app\tye_run.bat`  
