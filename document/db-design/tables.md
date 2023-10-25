# 数据库设计

### 1. **[tbl_Album 影集](#jump_tbl_Album)**
### 2. **[tbl_Ask 资源请求](#jump_tbl_Ask)**
### 3. **[tbl_Celebrity 影人详细](#jump_tbl_Celebrity)**
### 4. **[tbl_Comment 评论](#jump_tbl_Comment)**
### 5. **[tbl_Country 制片国家/地区](#jump_tbl_Country)**
### 6. **[tbl_Discovery 电影每日发现](#jump_tbl_Discovery)**
### 7. **[tbl_GenreMovie 电影类型](#jump_tbl_GenreMovie)**
### 8. **[tbl_Language 语言](#jump_tbl_Language)**
### 9. **[tbl_Mark 标记信息](#jump_tbl_Mark)**
### 10. **[tbl_Movie 电影](#jump_tbl_Movie)**
### 11. **[tbl_Resource 影片资源](#jump_tbl_Resource)**
### 12. **[tbl_UserAccount 账户信息](#jump_tbl_UserAccount)**
### 13. **[tbl_Notice 通知](#jump_tbl_Notice)**
### 14. **[tbl_Work 工作](#jump_tbl_Work)**


## 业务表结构

<a id="jump_tbl_Album"></a>
### [影集] tbl_Album
| 列明            |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                                               |
| :-------------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :--------------------------------------------------------------------- |
| album_Id        | varchar  |   50 |      |      |    v |   v |        | 主键                                                                   |
| album_Title     | nvarchar |   50 |      |      |    v |     |        | 影集名称                                                               |
| album_User      | varchar  |   50 |      |      |    v |     |        | 登录者ID（与 tbl_UserAccount.user_Id 有外键关系）                      |
| album_Item      | nvarchar |   -1 |      |      |      |     |        | 电影对象（以json格式保存的电影集合，与 tbl_Movie.movie_Id 有外键关系） |
| album_Summary   | nvarchar |   -1 |      |      |      |     |        | 简介                                                                   |
| album_Time      | datetime |    8 |    3 |   23 |      |     |        | 创建时间                                                               |
| album_AlterTime | datetime |    8 |    3 |   23 |      |     |        | 修改时间                                                               |
| album_Cover     | nvarchar |  100 |      |      |      |     |        | 封面（带扩展名）                                                       |
| album_Visit     |   int    |    4 |      |   10 |      |     |        | 关注数                                                                 |

- album_Item的数据规则
  - ```[{"Movie":"m0002","Note":"","Time":"2023/10/23 23:21:03","MovieInfo":null},{"Movie":"m0003","Note":"","Time":"2023/10/23 23:39:22","MovieInfo":null}]```


<a id="jump_tbl_Ask"></a>
### [资源请求] tbl_Ask
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                          |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------------ |
| ask_Id    | varchar  |   50 |      |      |    v |   v |        | 主键                                              |
| ask_User  | varchar  |   50 |      |      |    v |     |        | 登录者ID（与 tbl_UserAccount.user_Id 有外键关系） |
| ask_Movie | varchar  |   50 |      |      |    v |     |        | 电影ID（与 tbl_Movie.movie_Id 有外键关系）        |
| ask_Time  | datetime |    8 |    3 |   23 |      |     |        | 请求时间                                          |
| ask_With  |   int    |    4 |      |   10 |      |     |        | 被请求的数量                                      |
| ask_Note  | nvarchar |   -1 |      |      |      |     |        | 备注                                              |
| ask_State |   bit    |    1 |      |    1 |      |     |        | 状态(1:已求到 0:未求到)                           |


<a id="jump_tbl_Celebrity"></a>
### [影人详细] tbl_Celebrity
| 列明            |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                          |
| :-------------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------------ |
| celeb_Id        | varchar  |   50 |      |      |    v |   v |        | 主键                                              |
| celeb_Name      | nvarchar |   50 |      |      |    v |     |        | 明星姓名（中文名）                                |
| celeb_Aka       | nvarchar |   -1 |      |      |      |     |        | 更多中文名（使用"/"分割）                         |
| celeb_NameEn    | nvarchar |   50 |      |      |      |     |        | 明星姓名（外文名）                                |
| celeb_AkaEn     | nvarchar |   -1 |      |      |      |     |        | 更多外文名（使用"/"分割）                         |
| celeb_Gender    | nvarchar |   10 |      |      |      |     |        | 性别（0:无 1:男 2:女）                            |
| celeb_Pro       | nvarchar |   -1 |      |      |      |     |        | 职业（使用"/"分割）                               |
| celeb_Birthday  | nvarchar |   50 |      |      |      |     |        | 出生日期                                          |
| celeb_Deathday  | nvarchar |   50 |      |      |      |     |        | 生卒日期                                          |
| celeb_BornPlace | nvarchar |   50 |      |      |      |     |        | 出生地                                            |
| celeb_Family    | nvarchar |   -1 |      |      |      |     |        | 家庭成员（使用"/"分割）                           |
| celeb_Avatar    | nvarchar |   50 |      |      |      |     |        | 明星海报（带扩展名）                              |
| celeb_Works     | nvarchar |   -1 |      |      |      |     |        | 作品？                                            |
| celeb_DoubanID  | nvarchar |   50 |      |      |      |     |        | 豆瓣影人编号（可以导航至豆瓣板块）                |
| celeb_IMDb      | nvarchar |   50 |      |      |      |     |        | IMDb编号                                          |
| celeb_Summary   | nvarchar |   -1 |      |      |      |     |        | 影人简介                                          |
| celeb_Create    | varchar  |   50 |      |      |      |     |        | 登录者ID（与 tbl_UserAccount.user_Id 有外键关系） |
| celeb_Status    | tinyint  |    1 |      |    3 |      |     |        | 状态（0:默认值 1:评审不通过 2:通过）              |
| celeb_Note      | nvarchar |  100 |      |      |      |     |        | 评审信息（0:内容有误 1:已经存在 other:其他）      |
| celeb_Time      | datetime |    8 |    3 |   23 |      |     |        | 创建时间                                          |
| celeb_AlterTime | datetime |    8 |    3 |   23 |      |     |        | 更新时间                                          |


<a id="jump_tbl_Comment"></a>
### [评论] tbl_Comment
| 列明        |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                          |
| :---------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------------ |
| cmt_Id      | varchar  |   50 |      |      |    v |   v |        | 主键                                              |
| cmt_Content | nvarchar |   -1 |      |      |    v |     |        | 内容                                              |
| cmt_User    | varchar  |   50 |      |      |      |     |        | 登录者ID（与 tbl_UserAccount.user_Id 有外键关系） |
| cmt_Movie   | varchar  |   50 |      |      |    v |     |        | 电影ID（与 tbl_Movie.movie_Id 有外键关系）        |
| cmt_Time    | datetime |    8 |    3 |   23 |      |     |        | 评论时间                                          |


<a id="jump_tbl_Country"></a>
### [制片国家/地区] tbl_Country
| 列明         |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 自增列 | 默认值 | 业务描述 |
| :----------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | -----: | :------- |
| country_Id   | tinyint  |    1 |      |    3 |    v |   v |      v |        | ID       |
| country_Name | nvarchar |   50 |      |      |    v |     |        |        | 国家名   |


<a id="jump_tbl_Discovery"></a>
### [电影每日发现] tbl_Discovery
| 列明       |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                   |
| :--------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :----------------------------------------- |
| disc_Id    | varchar  |   50 |      |      |    v |   v |        | 主键                                       |
| disc_Movie | varchar  |   50 |      |      |    v |     |        | 电影ID（与 tbl_Movie.movie_Id 有外键关系） |
| disc_Image | varchar  |  100 |      |      |    v |     |        | 图片（带扩展名）                           |
| disc_Flag  |   int    |    4 |      |   10 |    v |     |        | 表示顺位                                   |
| disc_Time  | datetime |    8 |    3 |   23 |      |     |        | 发布时间                                   |


<a id="jump_tbl_Movie"></a>
### [电影] tbl_Movie
| 列明              |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                                                     |
| :---------------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :--------------------------------------------------------------------------- |
| movie_Id          | varchar  |   50 |      |      |    v |   v |        | 电影ID                                                                       |
| movie_Title       | nvarchar |  100 |      |      |    v |     |        | 电影名称（中文描述）                                                         |
| movie_TitleEn     | nvarchar |  100 |      |      |      |     |        | 电影名称（英文描述）                                                         |
| movie_Aka         | nvarchar |   -1 |      |      |      |     |        | 电影别名                                                                     |
| movie_Directors   | nvarchar |   -1 |      |      |      |     |        | 导演名列表                                                                   |
| movie_Writers     | nvarchar |   -1 |      |      |      |     |        | 编剧名列表                                                                   |
| movie_Casts       | nvarchar |   -1 |      |      |      |     |        | 演员名列表                                                                   |
| movie_DirectorsId | nvarchar |   -1 |      |      |      |     |        | 导演ID列表                                                                   |
| movie_WritersId   | nvarchar |   -1 |      |      |      |     |        | 编剧ID列表                                                                   |
| movie_CastsId     | nvarchar |   -1 |      |      |      |     |        | 演员ID列表                                                                   |
| movie_Year        | varchar  |   50 |      |      |      |     |        | 年代                                                                         |
| movie_Pubdates    | nvarchar |  200 |      |      |      |     |        | 上映日期                                                                     |
| movie_Durations   | nvarchar |  200 |      |      |      |     |        | 时长                                                                         |
| movie_Genres      | varchar  |   50 |      |      |      |     |        | 类型列表（必须使用"/"进行分割，与 tbl_GenreMovie.genre_Id 有外键关系）       |
| movie_Languages   | varchar  |   50 |      |      |      |     |        | 语言列表（必须使用"/"进行分割，与 tbl_Language.lang_Id 有外键关系）          |
| movie_Countries   | varchar  |   50 |      |      |      |     |        | 制片国家/地区列表（必须使用"/"进行分割，与 tbl_Country.genre_Id 有外键关系） |
| movie_Rating      | varchar  |   50 |      |      |      |     |        | 评分（0.0 ~ 10.0之间）                                                       |
| movie_RatingCount | varchar  |   50 |      |      |      |     |        | 评分人数                                                                     |
| movie_DoubanID    | nvarchar |   50 |      |      |      |     |        | 豆瓣编号（可以导航至豆瓣板块）                                               |
| movie_IMDb        | nvarchar |   50 |      |      |      |     |        | IMDb编号                                                                     |
| movie_Summary     | nvarchar |   -1 |      |      |      |     |        | 电影简介                                                                     |
| movie_Avatar      | nvarchar |   50 |      |      |      |     |        | 电影海报（带扩展名）                                                         |
| movie_Create      | varchar  |   50 |      |      |      |     |        | 登录者ID（与 tbl_UserAccount.user_Id 有外键关系）                            |
| movie_Status      | tinyint  |    1 |      |      |      |     |        | 状态（0:默认值 1:评审不通过 2:通过）                                         |
| movie_Note        | nvarchar |  100 |      |      |      |     |        | 评审信息（0:内容有误 1:已经存在 other:其他）                                 |
| movie_Time        | datetime |    8 |    3 |   23 |      |     |        | 创建时间                                                                     |
| movie_AlterTime   | datetime |    8 |    3 |   23 |      |     |        | 更新时间                                                                     |
| movie_VisitCount  |   int    |    4 |      |   10 |      |     |        | 浏览量                                                                       |

- 以下列举导演、编剧、演员导航与数据对应关系和使用规则  ***（必须使用"/"进行分割，与tbl_Celebrity.ask_Id有外键关系）***
  - 多名演员列表导航
    - movie_Casts = "约翰尼·德普 / 薇诺娜·瑞德 / 黛安·韦斯特" && movie_CastsId = "001/005/006"

  - 导演列表导航（既是导演又是编剧的情况）
    - movie_Directors = "蒂姆·波顿" && movie_DirectorsId = "003" && movie_WritersId = "003"

  - 编剧列表导航（其中有一位导演的情况）
    - movie_Writers = "蒂姆·波顿 / 卡罗琳·汤普森" && movie_DirectorsId = "003" && movie_WritersId = "003/004"

- 以下是 movie_Status 与 movie_Note 的使用规则
  - 发布电影信息时（一般用户）：movie_Status = 1 && movie_Note = ""
  - 发布电影信息时（管理员）：movie_Status = 2 && movie_Note = ""
  - 评审电影信息时（通过）：movie_Status = 2 && movie_Note = ""
  - 评审电影信息时（不通过）：movie_Status = 1 && movie_Note = "（0:内容有误 1:已经存在 other:其他）"

- 电影最新与热门的数据规则
  - 最新：movie_Status = 1（以 movie_Time 降序取前20件数据）
  - 热门：movie_Status = 2（以 movie_VisitCount 降序取前20件数据）


<a id="jump_tbl_GenreMovie"></a>
### [电影类型] tbl_GenreMovie
| 列明       |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 自增列 | 默认值 | 业务描述 |
| :--------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | -----: | :------- |
| genre_Id   | tinyint  |    1 |      |    3 |    v |   v |      v |        | ID       |
| genre_Name | nvarchar |   50 |      |      |    v |     |        |        | 电影类型 |


<a id="jump_tbl_Language"></a>
### [语言] tbl_Language
| 列明      |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 自增列 | 默认值 | 业务描述 |
| :-------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | -----: | :------- |
| lang_Id   | tinyint  |    1 |      |    3 |    v |   v |      v |        | ID       |
| lang_Name | nvarchar |   50 |      |      |    v |     |        |        | 语言     |


<a id="jump_tbl_UserAccount"></a>
### [账户信息] tbl_UserAccount
| 列明              |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述 |
| :---------------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------- |
| user_Id           | varchar  |   50 |      |      |    v |   v |        |          |  ID
| user_Account      | nvarchar |   50 |      |      |    v |     |        |          |  用户名
| user_Password     | varchar  |  200 |      |      |    v |     |        |          |  密码
| user_EmailAddress | nvarchar |   50 |      |      |      |     |        |          |  email
| user_Avatar       | nvarchar |  100 |      |      |      |     |        |          |  头像（带扩展名）
| user_Cover        | nvarchar |  100 |      |      |      |     |        |          |  封面（带扩展名）
| user_CreateTime   | datetime |    8 |    3 |   23 |      |     |        |          |  创建时间
| user_AlterTime    | datetime |    8 |    3 |   23 |      |     |        |          |  修改时间
| user_IsAdmin      |   bit    |    1 |      |    1 |      |     |        |          |  是否为管理员(1:管理员)


<a id="jump_tbl_Resource"></a>
### [影片资源] tbl_Resource
| 列明           |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                           |
| :------------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------------- |
| res_Id         | varchar  |   50 |      |      |    v |   v |        | ID                                                 |
| res_Name       | nvarchar |   -1 |      |      |      |     |        | 名称                                               |
| res_Content    | nvarchar |   -1 |      |      |    v |     |        | 链接内容                                           |
| res_Size       |   real   |    4 |      |   24 |      |     |        | 大小                                               |
| res_User       | varchar  |   50 |      |      |      |     |        | 登录者ID （与 tbl_UserAccount.user_Id 有外键关系） |
| res_Movie      | varchar  |   50 |      |      |    v |     |        | 电影ID（与 tbl_Movie.movie_Id 有外键关系）         |
| res_Time       | datetime |    8 |   -1 |   23 |      |     |        | 创建时间                                           |
| res_FavorCount |   int    |    4 |      |   10 |      |     |        | 名称                                               |
| res_Type       | tinyint  |    1 |      |    3 |      |     |        | 类型（0:电驴链接 1:磁力链 other:bt种子）           |
| res_Status     | tinyint  |    1 |      |    3 |      |     |        | 状态（0:默认值 1:评审不通过 2:通过）               |
| res_Note       | nvarchar |  100 |      |      |      |     |        | 备注                                               |


<a id="jump_tbl_Mark"></a>
### [标记信息] tbl_Mark
| 列明        |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                                                              |
| :---------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------------------------------------------------ |
| mark_Id     | varchar  |   50 |      |      |    v |   v |        | ID                                                                                    |
| mark_Type   | tinyint  |    1 |      |    3 |    v |     |        | 标记类型（1想看电影，2看过电影，3喜欢电影，4收藏影人，5赞资源，6同求资源，7关注专辑） |
| mark_User   | varchar  |   50 |      |      |    v |     |        | 登录者ID （与 tbl_UserAccount.user_Id 有外键关系）                                    |
| mark_Target | varchar  |   50 |      |      |    v |     |        | 被标记目标id                                                                          |
| mark_Time   | datetime |    8 |    3 |   23 |      |     |        | 创建时间                                                                              |


<a id="jump_tbl_Notice"></a>
### [通知] tbl_Notice
| 列明           |   类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                           |
| :------------- | :------: | ---: | ---: | ---: | ---: | --: | -----: | :------------------------------------------------- |
| notice_Id      | varchar  |   50 |      |      |    v |   v |        | ID                                                 |
| notice_Content | nvarchar |   -1 |      |      |    v |     |        | 通知内容                                           |
| notice_Res     | varchar  |   50 |      |      |    v |     |        | ？？？                                             |
| notice_User    | varchar  |   50 |      |      |    v |     |        | 登录者ID （与 tbl_UserAccount.user_Id 有外键关系） |
| notice_Time    | datetime |    8 |    3 |   23 |      |     |        | 创建时间                                           |
| notice_Flag    | TinyInt  |    1 |      |    3 |      |     |        | 通知状态                                           |


<a id="jump_tbl_Work"></a>
### [工作] tbl_Work
| 列明       |  类型   | 长度 | 标度 | 精度 | 非空 |  PK | 默认值 | 业务描述                                       |
| :--------- | :-----: | ---: | ---: | ---: | ---: | --: | -----: | :--------------------------------------------- |
| work_Id    | varchar |   50 |      |      |    v |   v |        | ID                                             |
| work_Movie | varchar |   50 |      |      |    v |     |        | 电影ID（与 tbl_Movie.movie_Id 有外键关系）     |
| work_Celeb | varchar |   50 |      |      |    v |     |        | 影人ID（与 tbl_Celebrity.celeb_Id 有外键关系） |
| work_Type  | tinyint |    1 |      |    3 |      |     |        | ？？？                                         |


## 业务表关系













