# FilmHouse（概要）
![页面](./document/functional-design/png/FileHouse.png) 
（文字信息【略】）
  
# 执行准备：
1. 关于`ssl`的制作（请参见：https://github.com/TonyZhangshi81/FilmHouse/blob/main/document/program-implement/ssl.md）
2. 支持的数据库类型包括（`sqlserver 2016`、`mysql 8.0`、`postgresql 13.5` 以及以上版本）- 开发环境需安装以上任意一种数据库引擎
3. 安装在MongoDb设定连接（默认是`mongodb://127.0.0.1:27017`），创建`database`以及所属`user`
    - ```use FilmHouse_Mov_Server```
    - ```db.createUser({"user":[user], "pwd":[password], "roles":[{role:"dbAdminAnyDatabase", db:"admin"}]})```
4. 关于本地`hosts`修改（根据本地ip添加hostname为`filmhouse.com`）
5. 执行全体编译（https://github.com/TonyZhangshi81/FilmHouse/blob/main/src/Build.bat）
6. `nginx` 开启（https://github.com/TonyZhangshi81/FilmHouse/blob/main/src/tye-app/nginx_start.bat）
7. 站点服务开启（https://github.com/TonyZhangshi81/FilmHouse/blob/main/src/tye-app/tye_run.bat）
  
# 访问：
站点：https://filmhouse.com:7144/  
健康检查：https://filmhouse.com:7144/healthchecks-ui#/healthchecks  
WebApi：https://filmhouse.com:7144/Swagger/index.html  
