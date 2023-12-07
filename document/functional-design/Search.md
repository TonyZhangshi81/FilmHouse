# 1. 画面设计

### 1-1. **[概要](./Summary.md)**
### 1-2. **[画面原型](#jump_png)**
### 1-3. **[model元素](#jump_model)**
### 1-4. **[元素与数据源](#jump_logic)**

<a id="jump_png"></a>
## 首页
![首页](.\png\Search.png)

<a id="jump_model"></a>
## model元素&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;>**[POCO描述](../functional-design/POCO.md)**&emsp;>**[数据库设计](../db-design/tables.md)**
#### 搜索处理
| 参数名称      | 类型                    | 区域 | 描述 |
| :------------ | :---------------------- | :--- | :--- |
| SearchView    | SearchViewModel (class) |      |      |
| &emsp;Keyword | KeywordVO               | (2)  |      |
<br>
#### 搜索处理
| 参数名称                      | 类型                              | 区域 | 描述                 |
| :---------------------------- | :-------------------------------- | :--- | :------------------- |
| ClassifyView                  | ClassifyViewModel (class)         |      |                      |
| &emsp;ListGenre               | List&lt;SelectListItem>           | (4)  | 类型                 |
| &emsp;ListCountry             | List&lt;SelectListItem>           | (5)  | 国家/地区            |
| &emsp;ListYear                | List&lt;SelectListItem>           | (6)  | 年代                 |
| &emsp;ListMovies              | List&lt;MovieViewModel>           | (8)  |                      |
| &emsp;&emsp;Id                | MovieIdVO                         |      |                      |
| &emsp;&emsp;Title             | TitleVO                           |      |                      |
| &emsp;&emsp;TitleEn           | TitleEnVO                         |      |                      |
| &emsp;&emsp;Aka               | AkaVO                             |      |                      |
| &emsp;&emsp;Directors         | List&lt;LinkItem> (class)         |      |                      |
| &emsp;&emsp;Casts             | List&lt;LinkItem> (class)         |      |                      |
| &emsp;&emsp;Writers           | List&lt;LinkItem> (class)         |      |                      |
| &emsp;&emsp;Year              | YearVO                            |      |                      |
| &emsp;&emsp;Pubdates          | PubdatesVO                        |      |                      |
| &emsp;&emsp;Durations         | DurationsVO                       |      |                      |
| &emsp;&emsp;Genres            | GenresVO                          |      |                      |
| &emsp;&emsp;Languages         | LanguagesVO                       |      |                      |
| &emsp;&emsp;Countries         | CountriesVO                       |      |                      |
| &emsp;&emsp;Rating            | RatingVO                          |      |                      |
| &emsp;&emsp;RatingCount       | RatingCountVO                     |      |                      |
| &emsp;&emsp;DoubanID          | DoubanIDVO                        |      |                      |
| &emsp;&emsp;IMDbID            | IMDbIDVO                          |      |                      |
| &emsp;&emsp;Summary           | SummaryVO                         |      |                      |
| &emsp;&emsp;SummaryShort      | SummaryShortVO                    |      |                      |
| &emsp;&emsp;SummaryPara       | SummaryParaVO                     |      |                      |
| &emsp;&emsp;Avatar            | AvatarVO                          |      |                      |
| &emsp;&emsp;IsPlan            | bool                              |      | 想看                 |
| &emsp;&emsp;IsFinish          | bool                              |      | 看过                 |
| &emsp;&emsp;IsFavor           | bool                              |      | 喜欢                 |
| &emsp;&emsp;PlanCount         | PlanCountVO                       |      |                      |
| &emsp;&emsp;FinishCount       | FinishCountVO                     |      |                      |
| &emsp;&emsp;FavorCount        | FavorCountVO                      |      |                      |
| &emsp;&emsp;VisitCount        | VisitCountVO                      |      |                      |
| &emsp;&emsp;IsCreate          | bool                              |      | 当前用户是否是创建者 |
| &emsp;&emsp;Create            | UserIdVO                          |      |                      |
| &emsp;&emsp;Status            | ReviewStatusVO                    |      | 审核状态             |
| &emsp;&emsp;Note              | NoteVO                            |      | 审核备注             |
| &emsp;&emsp;CommentCount      | int                               |      |                      |
| &emsp;&emsp;Resources         | List&lt;ResViewModel> (class)     |      |                      |
| &emsp;&emsp;&emsp;Id          | ResourcesIdVO                     |      |                      |
| &emsp;&emsp;&emsp;Content     | ResourceContentVO                 |      |                      |
| &emsp;&emsp;&emsp;FileName    | FileNameVO                        |      |                      |
| &emsp;&emsp;&emsp;FileSize    | FileSizeVO                        |      |                      |
| &emsp;&emsp;&emsp;FavorCount  | FavorCountVO                      |      |                      |
| &emsp;&emsp;&emsp;ResType     | ResTypeVO                         |      |                      |
| &emsp;&emsp;&emsp;User        | UserIdVO                          |      |                      |
| &emsp;&emsp;&emsp;Account     | AccountIdVO                       |      |                      |
| &emsp;&emsp;&emsp;Movie       | MovieIdVO                         |      |                      |
| &emsp;&emsp;&emsp;MovieTitle  | MovieTitleVO                      |      |                      |
| &emsp;&emsp;&emsp;Status      | ReviewStatusVO                    |      |                      |
| &emsp;&emsp;&emsp;Note        | NoteVO                            |      |                      |
| &emsp;&emsp;MyComment         | CommentViewModel (class)          |      |                      |
| &emsp;&emsp;MyComment         | List&lt;CommentViewModel> (class) |      |                      |
| &emsp;&emsp;&emsp;Id          | CommentIdVO                       |      |                      |
| &emsp;&emsp;&emsp;Content     | CommentContentVO                  |      |                      |
| &emsp;&emsp;&emsp;UserId      | UserIdVO                          |      |                      |
| &emsp;&emsp;&emsp;UserAccount | UserAccountVO                     |      |                      |
| &emsp;&emsp;&emsp;UserAvatar  | UserAvatarVO                      |      |                      |
| &emsp;&emsp;&emsp;Movie       | MovieIdVO                         |      |                      |
| &emsp;&emsp;&emsp;Time        | TimeVO                            |      |                      |
| &emsp;&emsp;Albums            | List&lt;LinkItem> (class)         |      |                      |

<a id="jump_logic"></a>
## 元素与数据源
