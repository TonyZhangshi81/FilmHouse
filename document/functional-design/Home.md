# 1. 画面设计

## 1-1. **[概要](./Summary.md)**

## 1-2. **[画面原型](#jump_png)**

## 1-3. **[model元素](#jump_model)**

## 1-4. **[元素与数据源](#jump_logic)**

<a id="jump_png"></a>

## 首页

![首页](.\png\Home.png)

<a id="jump_model"></a>

## model元素&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;>**[POCO描述](../functional-design/POCO.md)**&emsp;>**[数据库设计](../db-design/tables.md)**

### 显示处理

| 参数名称              | 类型                                | 区域 | 描述         |
| :-------------------- | :---------------------------------- | :--- | :----------- |
| Discovery             | HomeDiscViewModel (class)           |      |              |
| &emsp;Image           | ImageVO                             | (4)  |              |
| &emsp;Movie           | MovieViewModel (class)              | (5)  |              |
| &emsp;&emsp;Id        | MovieIdVO                           |      |              |
| &emsp;&emsp;Title     | TitleVO                             |      |              |
| &emsp;&emsp;DoubanID  | DoubanIDVO                          |      |              |
| &emsp;&emsp;Rating    | RatingVO                            |      |              |
| &emsp;&emsp;Summary   | SummaryVO                           |      |              |
| &emsp;&emsp;Directors | DirectorsVO                         |      |              |
| &emsp;&emsp;Writers   | WritersVO                           |      |              |
| &emsp;&emsp;Casts     | CastsVO                             |      |              |
| &emsp;&emsp;IsPlan    | bool                                |      | 想看         |
| &emsp;&emsp;IsFinish  | bool                                |      | 看过         |
| &emsp;&emsp;IsFavor   | bool                                |      | 喜欢         |
| &emsp;Offset          | int                                 |      | 当前翻页页码 |
| &emsp;Pre             | int                                 |      | 上一个页码   |
| &emsp;Post            | int                                 |      | 下一个页码   |
| News                  | List&lt;MovieListViewModel> (class) | (6)  | 最新栏目     |
| &emsp;Title           | TitleVO                             |      |              |
| &emsp;Year            | YearVO                              |      |              |
| &emsp;Id              | MovieIdVO                           |      |              |
| Mosts                 | List&lt;MovieListViewModel> (class) | (7)  | 热门栏目     |
| &emsp;Title           | TitleVO                             |      |              |
| &emsp;Year            | YearVO                              |      |              |
| &emsp;Id              | MovieIdVO                           |      |              |

<a id="jump_logic"></a>

## 元素与数据源
