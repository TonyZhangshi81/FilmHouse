# 1. 数据库设计

### 1-1. **[Album 影集](#jump_Album)**
### 1-2. **[Ask 资源请求](#jump_Ask)**
### 1-3. **[Celebrity 影人详细](#jump_Celebrity)**
### 1-4. **[Comment 评论](#jump_Comment)**
### 1-5. **[Configuration 配置设定](#jump_Configuration)**
### 1-6. **[Discovery 电影每日发现](#jump_Discovery)**
### 1-7. **[Country 制片国家/地区](#jump_Country)** <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
### 1-8. **[GenreMovie 电影类型](#jump_GenreMovie)** <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
### 1-9. **[Language 语言](#jump_Language)** <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
### 1-10. **[CodeMast CODE管理表](#jump_CodeMast)**
### 1-11. **[Mark 标记信息](#jump_Mark)**
### 1-12. **[Movie 电影](#jump_Movie)**
### 1-13. **[Resource 影片资源](#jump_Resource)**
### 1-14. **[UserAccount 账户信息](#jump_UserAccount)**
### 1-15. **[Notice 通知](#jump_Notice)**
### 1-16. **[Work 工作](#jump_Work)**

# 2. 业务表关系
### 2-1. **[ER图](#jump_ER)**

***（以下类型秒数是基于postgresql数据库）***
## 业务表结构

<a id="jump_Album"></a>
### [影集] Album
| 列名            |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                                          |
| :-------------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | ----------------------------------------------------------------- |
| RequestId       |   uuid    |   36 |      |      |    v |     |     |        | GUID                                                              |
| AlbumId         |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                                              |
| Title           |  varchar  |   50 |      |      |    v |     |     |        | 影集名称                                                          |
| UserId          |   uuid    |   36 |      |      |    v |     |   v |        | 登录者ID（与 UserAccount.UserId 有外键关系）                      |
| Items           |   text    |   -1 |      |      |      |     |     |        | 电影对象（以json格式保存的电影集合，与 Movie.MovieId 有外键关系） |
| Summary         |   text    |   -1 |      |      |      |     |     |        | 简介                                                              |
| Cover           |  varchar  |  100 |      |      |      |     |     |        | 封面（带扩展名）                                                  |
| AmountAttention |  numeric  |    4 |      |    0 |      |     |     | 0      | 关注数                                                            |
| CreatedOn       | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                                          |
| UpDatedOn       | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                                          |

- Item的数据规则
  - ```[{"Movie":"m0002","Note":"","Time":"2023/10/23 23:21:03","MovieInfo":null},{"Movie":"m0003","Note":"","Time":"2023/10/23 23:39:22","MovieInfo":null}]```


<a id="jump_Ask"></a>
### [资源请求] Ask
| 列名        |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                     |
| :---------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | -------------------------------------------- |
| RequestId   |   uuid    |   36 |      |      |    v |     |     |        | GUID                                         |
| AskId       |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                         |
| UserId      |   uuid    |   36 |      |      |    v |     |   v |        | 登录者ID（与 UserAccount.UserId 有外键关系） |
| MovieId     |   uuid    |   36 |      |      |    v |     |   v |        | 电影ID（与 Movie.MovieId 有外键关系）        |
| RequestTime | timestamp |    8 |    3 |   23 |      |     |     |        | 请求时间                                     |
| RequestWith |  numeric  |    4 |      |   10 |      |     |     | 0      | 被请求的数量                                 |
| Note        |  varchar  | 1000 |      |      |      |     |     |        | 备注                                         |
| Status      |   bool    |    1 |      |    1 |      |     |     | false  | 状态(1:已求到 0:未求到)                      |
| CreatedOn   | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                     |
| UpDatedOn   | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                     |


<a id="jump_Celebrity"></a>
### [影人详细] Celebrity
| 列名         |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                     |
| :----------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | -------------------------------------------- |
| RequestId    |   uuid    |   36 |      |      |    v |     |     |        | GUID                                         |
| CelebrityId  |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                         |
| Name         |  varchar  |   50 |      |      |    v |     |     |        | 明星姓名（中文名）                           |
| Aka          |  varchar  |  500 |      |      |      |     |     |        | 更多中文名（使用"/"分割）                    |
| NameEn       |  varchar  |   50 |      |      |      |     |     |        | 明星姓名（外文名）                           |
| AkaEn        |  varchar  |  500 |      |      |      |     |     |        | 更多外文名（使用"/"分割）                    |
| Gender       |   int2    |      |      |      |      |     |     |        | 性别（0:无 1:男 2:女）                       |
| Professions  |  varchar  |   50 |      |      |      |     |     |        | 职业（使用"/"分割）                          |
| Birthday     |   date    |      |      |      |      |     |     |        | 出生日期                                     |
| Deathday     |   date    |      |      |      |      |     |     |        | 生卒日期                                     |
| BornPlace    |  varchar  |  100 |      |      |      |     |     |        | 出生地                                       |
| Family       |  varchar  |  500 |      |      |      |     |     |        | 家庭成员（使用"/"分割）                      |
| Avatar       |  varchar  |  100 |      |      |      |     |     |        | 明星海报（带扩展名）                         |
| Works        |  varchar  | 1000 |      |      |      |     |     |        | 作品？                                       |
| DoubanID     |  varchar  |   10 |      |      |      |     |     |        | 豆瓣影人编号（可以导航至豆瓣板块）           |
| IMDb         |  varchar  |   10 |      |      |      |     |     |        | IMDb编号                                     |
| Summary      |   text    |   -1 |      |      |      |     |     |        | 影人简介                                     |
| UserId       |   uuid    |   36 |      |      |    v |     |   v |        | 登录者ID（与 UserAccount.UserId 有外键关系） |
| ReviewStatus |   int2    |    1 |      |    3 |      |     |     | 0      | 状态（0:默认值 1:评审不通过 2:通过）         |
| Note         |  varchar  | 1000 |      |      |      |     |     |        | 评审信息（0:内容有误 1:已经存在 other:其他） |
| CreatedOn    | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                     |
| UpDatedOn    | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                     |


<a id="jump_Comment"></a>
### [评论] Comment
| 列名        |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                 |
| :---------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | ---------------------------------------- |
| RequestId   |   uuid    |   36 |      |      |    v |     |     |        | GUID                                     |
| CommentId   |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                     |
| UserId      |   uuid    |   36 |      |      |    v |     |   v |        | 登录者ID（与 UserAccount.UserId 有外键关系） |
| MovieId     |   uuid    |   36 |      |      |    v |     |   v |        | 电影ID（与 Movie.MovieId 有外键关系）         |
| Content     |   text    |   -1 |      |      |      |     |     |        | 内容                                     |
| CommentTime | timestamp |    8 |    3 |   23 |      |     |     |        | 评论时间                                 |
| CreatedOn   | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                 |
| UpDatedOn   | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                 |


<a id="jump_Configuration"></a>
### [配置设定] Configuration
| 列名      |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述 |
| :-------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | -------- |
| RequestId |   uuid    |   36 |      |      |    v |     |     |        | GUID     |
| Key       |  varchar  |   50 |      |      |    v |   v |     |        | 主键     |
| Value     |  varchar  |   50 |      |      |      |     |     |        | 值       |
| CreatedOn | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间 |
| UpDatedOn | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间 |


<a id="jump_Discovery"></a>
### [电影每日发现] Discovery
| 列名        |   类型   | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                              |
| :---------- | :------: | ---: | ---: | ---: | ---: | --: | --: | :----- | ------------------------------------- |
| RequestId   |   uuid   |   36 |      |      |    v |     |     |        | GUID                                  |
| DiscoveryId |   uuid   |   36 |      |      |    v |   v |     |        | 主键                                  |
| MovieId     |   uuid   |   36 |      |      |    v |     |   v |        | 电影ID（与 Movie.MovieId 有外键关系） |
| Image       | varchar  |  100 |      |      |    v |     |     |        | 图片（带扩展名）                      |
| Flag        | numeric  |    3 |      |    0 |    v |     |     |        | 表示顺位                              |
| CreatedOn   | datetime |    8 |    3 |   23 |    v |     |     |        | 创建时间                              |
| UpDatedOn   | datetime |    8 |    3 |   23 |      |     |     |        | 修改时间                              |


<a id="jump_Movie"></a>
### [电影] Movie
| 列名         |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                                                 |
| :----------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | ------------------------------------------------------------------------ |
| RequestId    |   uuid    |   36 |      |      |    v |     |     |        | GUID                                                                     |
| MovieId      |   uuid    |   36 |      |      |    v |   v |     |        | 电影ID                                                                   |
| Title        |  varchar  |   50 |      |      |    v |     |     |        | 电影名称（中文描述）                                                     |
| TitleEn      |  varchar  |  100 |      |      |      |     |     |        | 电影名称（英文描述）                                                     |
| Aka          |  varchar  |  300 |      |      |      |     |     |        | 电影别名                                                                 |
| Directors    |  varchar  |  500 |      |      |      |     |     |        | 导演名列表                                                               |
| Writers      |  varchar  |  500 |      |      |      |     |     |        | 编剧名列表                                                               |
| Casts        |  varchar  |  500 |      |      |      |     |     |        | 演员名列表                                                               |
| DirectorsId  |  varchar  | 1000 |      |      |      |     |     |        | 导演ID列表                                                               |
| WritersId    |  varchar  | 1000 |      |      |      |     |     |        | 编剧ID列表                                                               |
| CastsId      |  varchar  | 1000 |      |      |      |     |     |        | 演员ID列表                                                               |
| Year         |  varchar  |   10 |      |      |      |     |     |        | 年代                                                                     |
| Pubdates     |  varchar  |  200 |      |      |      |     |     |        | 上映日期                                                                 |
| Durations    |  varchar  |   10 |      |      |      |     |     |        | 时长                                                                     |
| Genres       |  varchar  |  400 |      |      |      |     |     |        | 类型列表（必须使用"/"进行分割，与 GenreMovie.genre_Id 有外键关系）       |
| Languages    |  varchar  |  400 |      |      |      |     |     |        | 语言列表（必须使用"/"进行分割，与 Language.lang_Id 有外键关系）          |
| Countries    |  varchar  |  400 |      |      |      |     |     |        | 制片国家/地区列表（必须使用"/"进行分割，与 Country.genre_Id 有外键关系） |
| Rating       |  numeric  |    4 |    3 |    1 |      |     |     | 0.0    | 评分（0.0 ~ 10.0之间）                                                   |
| RatingCount  |   int4    |      |      |      |      |     |     | 0      | 评分人数                                                                 |
| DoubanID     |  varchar  |   10 |      |      |      |     |     |        | 豆瓣编号（可以导航至豆瓣板块）                                           |
| IMDb         |  varchar  |   10 |      |      |      |     |     |        | IMDb编号                                                                 |
| Summary      |   text    |   -1 |      |      |      |     |     |        | 电影简介                                                                 |
| Avatar       |  varchar  |  100 |      |      |      |     |     |        | 电影海报（带扩展名）                                                     |
| UserId       |   uuid    |   36 |      |      |      |     |   v |        | 登录者ID（与 UserAccount.UserId 有外键关系）                             |
| ReviewStatus |   int2    |    1 |      |      |      |     |     | 0      | 状态（0:默认值 1:评审不通过 2:通过）                                     |
| Note         |  varchar  | 1000 |      |      |      |     |     |        | 评审信息（0:内容有误 1:已经存在 other:其他）                             |
| PageViews    |  numeric  |   11 |   11 |    0 |      |     |     | 0      | 浏览量                                                                   |
| CreatedOn    | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                                                 |
| UpDatedOn    | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                                                 |

- 以下列举导演、编剧、演员导航与数据对应关系和使用规则  ***（必须使用"/"进行分割，与Celebrity.Id有外键关系）***
  - 多名演员列表导航
    - Casts = "约翰尼·德普 / 薇诺娜·瑞德 / 黛安·韦斯特" && CastsId = "001/005/006"

  - 导演列表导航（既是导演又是编剧的情况）
    - Directors = "蒂姆·波顿" && DirectorsId = "003" && WritersId = "003"

  - 编剧列表导航（其中有一位导演的情况）
    - Writers = "蒂姆·波顿 / 卡罗琳·汤普森" && DirectorsId = "003" && WritersId = "003/004"

- 以下是 Status 与 Note 的使用规则
  - 发布电影信息时（一般用户）：Status = 1 && Note = ""
  - 发布电影信息时（管理员）：Status = 2 && Note = ""
  - 评审电影信息时（通过）：Status = 2 && Note = ""
  - 评审电影信息时（不通过）：Status = 1 && Note = "（0:内容有误 1:已经存在 other:其他）"

- 电影最新与热门的数据规则
  - 最新：Status = 1（以 CreatedOn 降序取前20件数据）
  - 热门：Status = 2（以 PageViews 降序取前20件数据）
  

#### <span style="display:block;color:orangered;">(Country、GenreMovie、Language 这三个实体使用CodeMast进行管理)</span>

<a id="jump_Country"></a>
### [制片国家/地区] Country <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
| 列名         |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 描述 |
| :----------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| country_Id   | tinyint  |    1 |      |    3 |    v |   v |        | ID       |
| country_Name | nvarchar |   50 |      |      |    v |     |        | 国家名   |

<a id="jump_GenreMovie"></a>
### [电影类型] GenreMovie <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
| 列名       |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 描述 |
| :--------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| genre_Id   | tinyint  |    1 |      |    3 |    v |   v |        | ID       |
| genre_Name | nvarchar |   50 |      |      |    v |     |        | 电影类型 |

<a id="jump_Language"></a>
### [语言] Language <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
| 列名      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 描述 |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| lang_Id   | tinyint  |    1 |      |    3 |    v |   v |        | ID       |
| lang_Name | nvarchar |   50 |      |      |    v |     |        | 语言     |

<a id="jump_CodeMast"></a>
### [语言] CodeMast
| 列名      |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述 |
| :-------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | -------- |
| RequestId |   uuid    |   36 |      |      |    v |     |     |        | GUID     |
| Group     |  varchar  |   20 |      |    3 |    v |   v |     |        | 类别     |
| Code      |  varchar  |   20 |      |      |    v |     |     |        | KEY      |
| Name      |  varchar  |   50 |      |      |    v |     |     |        | 值       |
| Order     |  numeric  |    3 |      |      |    v |     |     |        | 排序顺   |
| CreatedOn | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间 |
| UpDatedOn | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间 |

- 以下是 Group 的分类规则
  - Country 代表国家/地区（001:剧情; 002:爱情; 003:奇幻; 004:惊悚...... ）
  - GenreMovie 电影类型（001:英语; 002:法语; 003:意大利语...... ）
  - Language 代表语言（001:美国; 002:澳大利亚......）

<a id="jump_UserAccount"></a>
### [账户信息] UserAccount
| 列名         |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | FK  | 默认值 | 描述               |
| :----------- | :------: | ---: | ---: | ---: | ---: | --: | :-- | ------ | ---------------------- |
| RequestId    |   uuid   |   36 |      |      |    v |     |     |        | GUID                   |
| UserId       |   uuid   |   50 |      |      |    v |   v |     |        | 主键                   |
| Account      | varchar  |   50 |      |      |    v |     |     |        | 用户名                 |
| Password     | varchar  |  200 |      |      |    v |     |     |        | 密码                   |
| EmailAddress | varchar  |  256 |      |      |    v |     |     |        | email                  |
| Avatar       | varchar  |  256 |      |      |      |     |     |        | 头像（带扩展名）       |
| Cover        | varchar  |  100 |      |      |      |     |     |        | 封面（带扩展名）       |
| IsAdmin      |   bool   |    1 |      |    1 |      |     |     | false  | 是否为管理员(1:管理员) |
| CreatedOn    | datetime |    8 |    3 |   23 |    v |     |     |        | 创建时间               |
| UpDatedOn    | datetime |    8 |    3 |   23 |      |     |     |        | 修改时间               |


<a id="jump_Resource"></a>
### [影片资源] Resource
| 列名         |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                      |
| :----------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | --------------------------------------------- |
| RequestId    |   uuid    |   36 |      |      |    v |     |     |        | GUID                                          |
| ResourceId   |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                          |
| Name         |  varchar  |   50 |      |      |    v |     |     |        | 名称                                          |
| Content      |  varchar  |  400 |      |      |    v |     |     |        | 链接内容                                      |
| Size         |  numeric  |   11 |      |    0 |      |     |     | 0      | 大小                                          |
| UserId       |   uuid    |   36 |      |      |      |     |   v |        | 登录者ID （与 UserAccount.UserId 有外键关系） |
| MovieId      |   uuid    |   36 |      |      |    v |     |   v |        | 电影ID（与 Movie.MovieId 有外键关系）         |
| FavorCount   |  numeric  |   11 |      |    0 |      |     |     | 0      | 名称                                          |
| Type         |   int2    |    1 |      |    3 |      |     |     | 0      | 类型（0:电驴链接 1:磁力链 other:bt种子）      |
| ReviewStatus |   int4    |    1 |      |    3 |      |     |     | 0      | 状态（0:默认值 1:评审不通过 2:通过）          |
| Note         |  varchar  | 1000 |      |      |      |     |     |        | 备注                                          |
| CreatedOn    | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                      |
| UpDatedOn    | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                      |


<a id="jump_Mark"></a>
### [标记信息] Mark
| 列名      |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                                                                       |
| :-------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | ---------------------------------------------------------------------------------------------- |
| RequestId |   uuid    |   36 |      |      |    v |     |     |        | GUID                                                                                           |
| MarkId    |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                                                                             |
| Type      |   int2    |    3 |      |    0 |    v |     |     | 0      | 标记类型（0默认值, 1想看电影，2看过电影，3喜欢电影，4收藏影人，5赞资源，6同求资源，7关注专辑） |
| UserId    |   uuid    |   36 |      |      |    v |     |   v |        | 登录者ID （与 UserAccount.UserId 有外键关系）                                                  |
| Target    |   uuid    |   36 |      |      |    v |     |     |        | 被标记目标id                                                                                   |
| CreatedOn | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                                                                       |
| UpDatedOn | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                                                                       |


<a id="jump_Notice"></a>
### [通知] Notice
| 列名       |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                      |
| :--------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | --------------------------------------------- |
| RequestId  |   uuid    |   36 |      |      |    v |     |     |        | GUID                                          |
| NoticeId   |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                            |
| Content    |   text    |   -1 |      |      |    v |     |     |        | 通知内容                                      |
| ResourceId |   uuid    |   36 |      |      |    v |     |   v |        | 资源ID（与 Resource.ResourceId 有外键关系）   |
| UserId     |   uuid    |   36 |      |      |    v |     |   v |        | 登录者ID （与 UserAccount.UserId 有外键关系） |
| Flag       |   int2    |    1 |      |    3 |      |     |     | 0      | 通知状态                                      |
| CreatedOn  | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                      |
| UpDatedOn  | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                      |


<a id="jump_Work"></a>
### [工作] Work
| 列名        |   类型    | 长度 | 标度 | 精度 | 非空 |  PK |  FK | 默认值 | 描述                                      |
| :---------- | :-------: | ---: | ---: | ---: | ---: | --: | --: | :----- | --------------------------------------------- |
| RequestId   |   uuid    |   36 |      |      |    v |     |     |        | GUID                                          |
| WorkId      |   uuid    |   36 |      |      |    v |   v |     |        | 主键                                          |
| MovieId     |   uuid    |   36 |      |      |    v |     |   v |        | 电影ID（与 Movie.MovieId 有外键关系）         |
| CelebrityId |   uuid    |   36 |      |      |    v |     |   v |        | 影人ID（与 Celebrity.CelebrityId 有外键关系） |
| Type        |   int2    |    1 |      |    3 |      |     |     | 0      | ？？？                                        |
| CreatedOn   | timestamp |    8 |    3 |   23 |    v |     |     |        | 创建时间                                      |
| UpDatedOn   | timestamp |    8 |    3 |   23 |      |     |     |        | 修改时间                                      |


<a id="jump_ER"></a>
## 业务表关系(ER图)
![ER图](FilmHouse-db-design.png)












