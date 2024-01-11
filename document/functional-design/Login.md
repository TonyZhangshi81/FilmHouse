# 1. 画面设计

## 1-1. **[概要](./Summary.md)**

## 1-2. **[画面原型](#jump_png)**

## 1-3. **[model元素](#jump_model)**

## 1-4. **[元素与数据源](#jump_logic)**

<a id="jump_png"></a>

## 首页

![首页](.\png\Login.png)

<a id="jump_model"></a>

## model元素&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;>**[POCO描述](../functional-design/POCO.md)**&emsp;>**[数据库设计](../db-design/tables.md)**

### 登陆处理

| 参数名称       | 类型                   | 区域 | 验证                           | 描述                                    |
| :------------- | :--------------------- | :--- | :----------------------------- | :-------------------------------------- |
| LoginView      | LoginViewModel (class) |      |                                |                                         |
| &emsp;Account  | AccountVO              | (4)  | Required<BR>StringLength(20)   | 必须<BR>最大长度20                      |
| &emsp;Password | PasswordVO             | (5)  | Regular<BR>RegularExpression() | 必须<BR>必须包括字符和数字且长度不小于6 |

<a id="jump_logic"></a>

## 元素与数据源
