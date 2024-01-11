
## 环境

  1。安装Openssl (Win32/Win64 OpenSSL Installer for Windows - Shining Light Productions (slproweb.com))  
  2。设置环境变量：  
  ![环境](.\png\01.png)
  ![环境](.\png\02.png)  
  
  配置文件修改：(`C:\Program Files\OpenSSL-Win64\bin\openssl.cfg`)  
  `dir = ./devCA  => dir = E:/localshare/share/MyProjects/source/devCA`  
  注意：dir在cfg内容中有两处出现。  
  （以避免因为文件系统访问权限受阻而无法作成文件）  
  另外、环境还需要做以下准备：  
  在ca证书作成目录内需要有newcerts文件夹和index.txt以及serial  
  newcerts文件夹（初始内容为空）：用于存放pem证书  
  index.txt文件（初始内容为空）：内容是subj内容  
  serial文件（初始内容是不超过20位的16进制序列）：由CA维护的为它所发的每个证书分配的一的序列号、用来追踪和撤销证书  
  
## 生成CA根证书

  生成根CA密钥 (使用`-des3`对密码123456进行加密并作成一个2024位长度的私钥文件）  
  `openssl genrsa -des3 -out devCA.key 2048`  
生成根证书并且自签名 (用自己的私钥和证书申请文件生成自己签名的证书、俗称自签名证书、这里可以理解为根证书）  
  `openssl req -new -x509 -days 365 -key devCA.key -out devCA.crt -subj "/C=CN/ST=shanghai/L=shanghai/O=Tonyzhangshi/CN=FilmHouse.com"`  
  
## 生成服务器秘钥和证书签名请求(CSR)

  `openssl genrsa -des3 -out server.key 2048`
  `openssl req -new -key server.key -out server.csr -subj "/C=CN/ST=shanghai/L=shanghai/O=Tonyzhangshi/CN=FilmHouse.com"`
  
## 使用自制的CA生成签名服务器证书

  `openssl x509 -req -days 3650 -in server.csr -CA devCA.crt -CAkey devCA.key -CAcreateserial -out server.crt`
  
## nginx ssl 部署

  将key、crt文件放置于nginx环境下的ssl目录内
  ![环境](.\png\06.png)  
  
## 开发环境（作为server端）CA证书安装

  ![环境](.\png\08.png)  
  
## nginx启动

  nginx配置验证（需要提供自签名证书密码）  
  `nginx -t`  
  ![环境](.\png\07.png)  
  
  启动 (每个执行过程中都需要提供自签名证书密码）
  `\tye-app\nginx_start.bat`  
  `\tye-app\nginx_reload.bat`  
  
## tye启动

  `\tye-app\tye_run.bat`  
  ![环境](.\png\09.png)  
  
## 需要注意

  因为服务器证书设定了验证密码、当nginx启动时需要提供密码、否则无法启动
  可以通过以下处理清除证书验证密码后再发布至ssl目录、以免Nginx无法启动网页
  `openssl rsa -in server.key -out server_nopass.key`
  (再将其以server.key为名进行同名覆盖后再启动nginx)
