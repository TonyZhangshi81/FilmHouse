# 数据库设计

### 1. **[Album 影集](#jump_Album)**
### 2. **[Ask 资源请求](#jump_Ask)**
### 3. **[Celebrity 影人详细](#jump_Celebrity)**
### 4. **[Comment 评论](#jump_Comment)**
### 5. **[Discovery 电影每日发现](#jump_Discovery)**
### 6. **[Country 制片国家/地区](#jump_Country)** <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
### 7. **[GenreMovie 电影类型](#jump_GenreMovie)** <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
### 8. **[Language 语言](#jump_Language)** <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
### 9. **[CodeMast CODE管理表](#jump_CodeMast)**
### 10. **[Mark 标记信息](#jump_Mark)**
### 11. **[Movie 电影](#jump_Movie)**
### 12. **[Resource 影片资源](#jump_Resource)**
### 13. **[UserAccount 账户信息](#jump_UserAccount)**
### 14. **[Notice 通知](#jump_Notice)**
### 15. **[Work 工作](#jump_Work)**


## 业务表结构

<a id="jump_Album"></a>
### [影集] Album
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                                     |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :----------------------------------------------------------- |
| RequestId | varchar  |   36 |      |      |    v |     |        | GUID                                                         |
| AlbumId   | varchar  |   50 |      |      |    v |   v |        | 主键                                                         |
| Title     | nvarchar |   50 |      |      |    v |     |        | 影集名称                                                     |
| UserId    | varchar  |   50 |      |      |    v |     |        | 登录者ID（与 UserAccount.Id 有外键关系）                     |
| Item      | nvarchar |   -1 |      |      |      |     |        | 电影对象（以json格式保存的电影集合，与 Movie.Id 有外键关系） |
| Summary   | nvarchar |   -1 |      |      |      |     |        | 简介                                                         |
| Cover     | nvarchar |  100 |      |      |      |     |        | 封面（带扩展名）                                             |
| Visit     |   int    |    4 |      |   10 |      |     |        | 关注数                                                       |
| CreatedOn | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                                     |
| UpDatedOn | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                                     |

- Item的数据规则
  - ```[{"Movie":"m0002","Note":"","Time":"2023/10/23 23:21:03","MovieInfo":null},{"Movie":"m0003","Note":"","Time":"2023/10/23 23:39:22","MovieInfo":null}]```


<a id="jump_Ask"></a>
### [资源请求] Ask
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                 |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :--------------------------------------- |
| RequestId | varchar  |   36 |      |      |    v |     |        | GUID                                     |
| AskId     | varchar  |   50 |      |      |    v |   v |        | 主键                                     |
| UserId    | varchar  |   50 |      |      |    v |     |        | 登录者ID（与 UserAccount.Id 有外键关系） |
| MovieId   | varchar  |   50 |      |      |    v |     |        | 电影ID（与 Movie.Id 有外键关系）         |
| Time      | datetime |    8 |    3 |   23 |      |     |        | 请求时间                                 |
| With      |   int    |    4 |      |   10 |      |     |        | 被请求的数量                             |
| Note      | nvarchar |   -1 |      |      |      |     |        | 备注                                     |
| Status    |   bit    |    1 |      |    1 |      |     |      0 | 状态(1:已求到 0:未求到)                  |
| CreatedOn | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                 |
| UpDatedOn | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                 |


<a id="jump_Celebrity"></a>
### [影人详细] Celebrity
| 列明        |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                     |
| :---------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------- |
| RequestId   | varchar  |   36 |      |      |    v |     |        | GUID                                         |
| CelebrityId | varchar  |   50 |      |      |    v |   v |        | 主键                                         |
| Name        | nvarchar |   50 |      |      |    v |     |        | 明星姓名（中文名）                           |
| Aka         | nvarchar |   -1 |      |      |      |     |        | 更多中文名（使用"/"分割）                    |
| NameEn      | nvarchar |   50 |      |      |      |     |        | 明星姓名（外文名）                           |
| AkaEn       | nvarchar |   -1 |      |      |      |     |        | 更多外文名（使用"/"分割）                    |
| Gender      | nvarchar |   10 |      |      |      |     |        | 性别（0:无 1:男 2:女）                       |
| Pro         | nvarchar |   -1 |      |      |      |     |        | 职业（使用"/"分割）                          |
| Birthday    | nvarchar |   50 |      |      |      |     |        | 出生日期                                     |
| Deathday    | nvarchar |   50 |      |      |      |     |        | 生卒日期                                     |
| BornPlace   | nvarchar |   50 |      |      |      |     |        | 出生地                                       |
| Family      | nvarchar |   -1 |      |      |      |     |        | 家庭成员（使用"/"分割）                      |
| Avatar      | nvarchar |   50 |      |      |      |     |        | 明星海报（带扩展名）                         |
| Works       | nvarchar |   -1 |      |      |      |     |        | 作品？                                       |
| DoubanID    | nvarchar |   50 |      |      |      |     |        | 豆瓣影人编号（可以导航至豆瓣板块）           |
| IMDb        | nvarchar |   50 |      |      |      |     |        | IMDb编号                                     |
| Summary     | nvarchar |   -1 |      |      |      |     |        | 影人简介                                     |
| Create      | varchar  |   50 |      |      |      |     |        | 登录者ID（与 UserAccount.Id 有外键关系）     |
| Status      | tinyint  |    1 |      |    3 |      |     |      0 | 状态（0:默认值 1:评审不通过 2:通过）         |
| Note        | nvarchar |  100 |      |      |      |     |        | 评审信息（0:内容有误 1:已经存在 other:其他） |
| CreatedOn   | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                     |
| UpDatedOn   | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                     |


<a id="jump_Comment"></a>
### [评论] Comment
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                 |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :--------------------------------------- |
| RequestId | varchar  |   36 |      |      |    v |     |        | GUID                                     |
| CommentId | varchar  |   50 |      |      |    v |   v |        | 主键                                     |
| Content   | nvarchar |   -1 |      |      |    v |     |        | 内容                                     |
| UserId    | varchar  |   50 |      |      |      |     |        | 登录者ID（与 UserAccount.Id 有外键关系） |
| MovieId   | varchar  |   50 |      |      |    v |     |        | 电影ID（与 Movie.Id 有外键关系）         |
| Time      | datetime |    8 |    3 |   23 |      |     |        | 评论时间                                 |
| CreatedOn | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                 |
| UpDatedOn | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                 |


<a id="jump_Discovery"></a>
### [电影每日发现] Discovery
| 列明        |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                         |
| :---------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------- |
| RequestId   | varchar  |   36 |      |      |    v |     |        | GUID                             |
| DiscoveryId | varchar  |   50 |      |      |    v |   v |        | 主键                             |
| MovieId     | varchar  |   50 |      |      |    v |     |        | 电影ID（与 Movie.Id 有外键关系） |
| Image       | varchar  |  100 |      |      |    v |     |        | 图片（带扩展名）                 |
| Flag        |   int    |    4 |      |   10 |    v |     |        | 表示顺位                         |
| Time        | datetime |    8 |    3 |   23 |      |     |        | 发布时间                         |
| CreatedOn   | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                         |
| UpDatedOn   | datetime |    8 |    3 |   23 |      |     |        | 修改时间                         |


<a id="jump_Movie"></a>
### [电影] Movie
| 列明        |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                                                 |
| :---------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :----------------------------------------------------------------------- |
| RequestId   | varchar  |   36 |      |      |    v |     |        | GUID                                                                     |
| MovieId     | varchar  |   50 |      |      |    v |   v |        | 电影ID                                                                   |
| Title       | nvarchar |  100 |      |      |    v |     |        | 电影名称（中文描述）                                                     |
| TitleEn     | nvarchar |  100 |      |      |      |     |        | 电影名称（英文描述）                                                     |
| Aka         | nvarchar |   -1 |      |      |      |     |        | 电影别名                                                                 |
| Directors   | nvarchar |   -1 |      |      |      |     |        | 导演名列表                                                               |
| Writers     | nvarchar |   -1 |      |      |      |     |        | 编剧名列表                                                               |
| Casts       | nvarchar |   -1 |      |      |      |     |        | 演员名列表                                                               |
| DirectorsId | nvarchar |   -1 |      |      |      |     |        | 导演ID列表                                                               |
| WritersId   | nvarchar |   -1 |      |      |      |     |        | 编剧ID列表                                                               |
| CastsId     | nvarchar |   -1 |      |      |      |     |        | 演员ID列表                                                               |
| Year        | varchar  |   50 |      |      |      |     |        | 年代                                                                     |
| Pubdates    | nvarchar |  200 |      |      |      |     |        | 上映日期                                                                 |
| Durations   | nvarchar |  200 |      |      |      |     |        | 时长                                                                     |
| Genres      | varchar  |   50 |      |      |      |     |        | 类型列表（必须使用"/"进行分割，与 GenreMovie.genre_Id 有外键关系）       |
| Languages   | varchar  |   50 |      |      |      |     |        | 语言列表（必须使用"/"进行分割，与 Language.lang_Id 有外键关系）          |
| Countries   | varchar  |   50 |      |      |      |     |        | 制片国家/地区列表（必须使用"/"进行分割，与 Country.genre_Id 有外键关系） |
| Rating      | varchar  |   50 |      |      |      |     |    0.0 | 评分（0.0 ~ 10.0之间）                                                   |
| RatingCount | varchar  |   50 |      |      |      |     |      0 | 评分人数                                                                 |
| DoubanID    | nvarchar |   50 |      |      |      |     |        | 豆瓣编号（可以导航至豆瓣板块）                                           |
| IMDb        | nvarchar |   50 |      |      |      |     |        | IMDb编号                                                                 |
| Summary     | nvarchar |   -1 |      |      |      |     |        | 电影简介                                                                 |
| Avatar      | nvarchar |   50 |      |      |      |     |        | 电影海报（带扩展名）                                                     |
| Create      | varchar  |   50 |      |      |      |     |        | 登录者ID（与 UserAccount.Id 有外键关系）                                 |
| Status      | tinyint  |    1 |      |      |      |     |      0 | 状态（0:默认值 1:评审不通过 2:通过）                                     |
| Note        | nvarchar |  100 |      |      |      |     |        | 评审信息（0:内容有误 1:已经存在 other:其他）                             |
| VisitCount  |   int    |    4 |      |   10 |      |     |        | 浏览量                                                                   |
| CreatedOn   | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                                                 |
| UpDatedOn   | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                                                 |

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
  - 最新：Status = 1（以 Time 降序取前20件数据）
  - 热门：Status = 2（以 VisitCount 降序取前20件数据）
  

#### <span style="display:block;color:orangered;">(Country、GenreMovie、Language 这三个实体使用CodeMast进行管理)</span>

<a id="jump_Country"></a>
### [制片国家/地区] Country <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
| 列明         |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述 |
| :----------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| country_Id   | tinyint  |    1 |      |    3 |    v |   v |        | ID       |
| country_Name | nvarchar |   50 |      |      |    v |     |        | 国家名   |

<a id="jump_GenreMovie"></a>
### [电影类型] GenreMovie <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
| 列明       |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述 |
| :--------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| genre_Id   | tinyint  |    1 |      |    3 |    v |   v |        | ID       |
| genre_Name | nvarchar |   50 |      |      |    v |     |        | 电影类型 |

<a id="jump_Language"></a>
### [语言] Language <span style="display:block;color:orangered;">(已经废弃 -> CODE管理表)</span>
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述 |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| lang_Id   | tinyint  |    1 |      |    3 |    v |   v |        | ID       |
| lang_Name | nvarchar |   50 |      |      |    v |     |        | 语言     |

<a id="jump_CodeMast"></a>
### [语言] CodeMast
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述 |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| RequestId | varchar  |   36 |      |      |    v |     |        | GUID     |
| Type      | varchar  |    1 |      |    3 |    v |   v |        | 类别     |
| CodeId    | varchar  |   50 |      |      |    v |     |        | KEY      |
| CodeValue | varchar  |   50 |      |      |    v |     |        | VALUE    |
| CreatedOn | datetime |    8 |    3 |   23 |    v |     |        | 创建时间 |
| UpDatedOn | datetime |    8 |    3 |   23 |      |     |        | 修改时间 |

- 以下是 Type 的分类规则
  - Country 代表国家/地区（001:剧情; 002:爱情; 003:奇幻; 004:惊悚...... ）
  - GenreMovie 电影类型（001:英语; 002:法语; 003:意大利语...... ）
  - Language 代表语言（001:美国; 002:澳大利亚......）

<a id="jump_UserAccount"></a>
### [账户信息] UserAccount
| 列明         |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述               |
| :----------- | :------: | ---: | ---: | ---: | ---: | --: | :----- | ---------------------- |
| RequestId    | varchar  |   36 |      |      |    v |     |        | GUID                   |
| UserId       | varchar  |   50 |      |      |    v |   v |        | ID                     |
| Account      | nvarchar |   50 |      |      |    v |     |        | 用户名                 |
| Password     | varchar  |  200 |      |      |    v |     |        | 密码                   |
| EmailAddress | nvarchar |   50 |      |      |      |     |        | email                  |
| Avatar       | nvarchar |  100 |      |      |      |     |        | 头像（带扩展名）       |
| Cover        | nvarchar |  100 |      |      |      |     |        | 封面（带扩展名）       |
| IsAdmin      |   bit    |    1 |      |    1 |      |     |        | 是否为管理员(1:管理员) |
| CreatedOn    | datetime |    8 |    3 |   23 |    v |     |        | 创建时间               |
| UpDatedOn    | datetime |    8 |    3 |   23 |      |     |        | 修改时间               |


<a id="jump_Resource"></a>
### [影片资源] Resource
| 列明       |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                  |
| :--------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :---------------------------------------- |
| RequestId  | varchar  |   36 |      |      |    v |     |        | GUID                                      |
| ResourceId | varchar  |   50 |      |      |    v |   v |        | ID                                        |
| Name       | nvarchar |   -1 |      |      |      |     |        | 名称                                      |
| Content    | nvarchar |   -1 |      |      |    v |     |        | 链接内容                                  |
| Size       |   real   |    4 |      |   24 |      |     |      0 | 大小                                      |
| UserId     | varchar  |   50 |      |      |      |     |        | 登录者ID （与 UserAccount.Id 有外键关系） |
| Movie      | varchar  |   50 |      |      |    v |     |        | 电影ID（与 Movie.Id 有外键关系）          |
| Time       | datetime |    8 |   -1 |   23 |      |     |        | 创建时间                                  |
| FavorCount |   int    |    4 |      |   10 |      |     |        | 名称                                      |
| Type       | tinyint  |    1 |      |    3 |      |     |        | 类型（0:电驴链接 1:磁力链 other:bt种子）  |
| Status     | tinyint  |    1 |      |    3 |      |     |      0 | 状态（0:默认值 1:评审不通过 2:通过）      |
| Note       | nvarchar |  100 |      |      |      |     |        | 备注                                      |
| CreatedOn  | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                  |
| UpDatedOn  | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                  |


<a id="jump_Mark"></a>
### [标记信息] Mark
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                                                              |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------------------------------------------------ |
| RequestId | varchar  |   36 |      |      |    v |     |        | GUID                                                                                  |
| MarkId    | varchar  |   50 |      |      |    v |   v |        | ID                                                                                    |
| Type      | tinyint  |    1 |      |    3 |    v |     |        | 标记类型（1想看电影，2看过电影，3喜欢电影，4收藏影人，5赞资源，6同求资源，7关注专辑） |
| UserId    | varchar  |   50 |      |      |    v |     |        | 登录者ID （与 UserAccount.Id 有外键关系）                                             |
| Target    | varchar  |   50 |      |      |    v |     |        | 被标记目标id                                                                          |
| Time      | datetime |    8 |    3 |   23 |      |     |        | 创建时间                                                                              |
| CreatedOn | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                                                              |
| UpDatedOn | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                                                              |


<a id="jump_Notice"></a>
### [通知] Notice
| 列明       |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                  |
| :--------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :---------------------------------------- |
| RequestId  | varchar  |   36 |      |      |    v |     |        | GUID                                      |
| NoticeId   | varchar  |   50 |      |      |    v |   v |        | ID                                        |
| Content    | nvarchar |   -1 |      |      |    v |     |        | 通知内容                                  |
| ResourceId | varchar  |   50 |      |      |    v |     |        | ？？？                                    |
| UserId     | varchar  |   50 |      |      |    v |     |        | 登录者ID （与 UserAccount.Id 有外键关系） |
| Time       | datetime |    8 |    3 |   23 |      |     |        | 创建时间                                  |
| Flag       | TinyInt  |    1 |      |    3 |      |     |        | 通知状态                                  |
| CreatedOn  | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                                  |
| UpDatedOn  | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                  |


<a id="jump_Work"></a>
### [工作] Work
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                             |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :----------------------------------- |
| RequestId | varchar  |   36 |      |      |    v |     |        | GUID                                 |
| WorkId    | varchar  |   50 |      |      |    v |   v |        | ID                                   |
| Movie     | varchar  |   50 |      |      |    v |     |        | 电影ID（与 Movie.Id 有外键关系）     |
| Celeb     | varchar  |   50 |      |      |    v |     |        | 影人ID（与 Celebrity.Id 有外键关系） |
| Type      | tinyint  |    1 |      |    3 |      |     |        | ？？？                               |
| CreatedOn | datetime |    8 |    3 |   23 |    v |     |        | 创建时间                             |
| UpDatedOn | datetime |    8 |    3 |   23 |      |     |        | 修改时间                             |


## 业务表关系













