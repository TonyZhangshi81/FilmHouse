using System.Text;
using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FilmHouse.Data;

public class Seed
{
    public static async Task SeedAsync(FilmHouseDbContext dbContext, ILogger logger, int maxRetryAvailability, int retry = 0)
    {
        var retryForAvailability = retry;

        try
        {
            var uuid = new RequestIdVO(Guid.NewGuid());
            var sysDate = new CreatedOnVO(System.DateTime.Now);

            await dbContext.Configuration.AddRangeAsync(GetInitConfigurationSettings(uuid, sysDate));
            await dbContext.CodeMast.AddRangeAsync(GetInitCodeMastSettings(uuid, sysDate));
            await dbContext.SaveChangesAsync();


#if DEBUG

            // 用户
            await dbContext.UserAccounts.AddRangeAsync(GetUserAccounts(uuid, sysDate));
            await dbContext.SaveChangesAsync();

            // 影人信息
            await dbContext.Celebrities.AddRangeAsync(GetCelebrities(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 影片
            await dbContext.Movies.AddRangeAsync(GetMovies(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 每日发现
            await dbContext.Discoveries.AddRangeAsync(GetDiscoveries(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 影片资源
            await dbContext.Resources.AddRangeAsync(GetResources(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 评论
            await dbContext.Comments.AddRangeAsync(GetComments(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 影集
            await dbContext.Albums.AddRangeAsync(GetAlbums(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 标记
            await dbContext.Marks.AddRangeAsync(GetMarks(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 资源请求
            await dbContext.Asks.AddRangeAsync(GetAsks(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

            // 找回密码
            await dbContext.FindPwds.AddRangeAsync(GetFindPwds(uuid, sysDate, dbContext));
            await dbContext.SaveChangesAsync();

#endif

        }
        catch (Exception e)
        {
            if (retryForAvailability >= maxRetryAvailability)
            {
                throw;
            }

            retryForAvailability++;

            logger.LogError(e.Message);
            await SeedAsync(dbContext, logger, maxRetryAvailability, retryForAvailability);
            throw;
        }
    }

    /// <summary>
    /// 配置信息
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<ConfigurationEntity> GetInitConfigurationSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
        new List<ConfigurationEntity>
        {
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.Name, Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.SubTitle, Value = new("DEMO"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.Version, Value = new("0.2.0.0"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.WebpagesEnabled, Value = new("false"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.ClientValidationEnabled, Value = new("true"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.UnobtrusiveJavaScriptEnabled, Value = new("false"), CreatedOn = dateTime },
            // 首頁每日發現件數限制
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.HomeDiscoveryMaxPage, Value = new("6"), CreatedOn = dateTime },
            // 最新欄目顯示件數
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.HomeDiscoveryNewMovies, Value = new("20"), CreatedOn = dateTime },
            // 熱門欄目顯示件數
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.HomeDiscoveryMostMovies, Value = new("20"), CreatedOn = dateTime },
            // 评论缩略显示长度
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.MovieSummaryShort, Value = new("250"), CreatedOn = dateTime },
            // 影片页面上显示的最大评论件数
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.MovieCommentMax, Value = new("6"), CreatedOn = dateTime },
            // 电影卡片明星显示最大个数
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.WorkItemCelebMax, Value = new("4"), CreatedOn = dateTime },
            // 檢索頁單頁顯示的件數限制
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.MovieSearchMax, Value = new("12"), CreatedOn = dateTime },
        };

    /// <summary>
    /// 代码信息管理表
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<CodeMastEntity> GetInitCodeMastSettings(RequestIdVO uuid, CreatedOnVO dateTime) =>
        new List<CodeMastEntity>
        {
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("001"), Name = new CodeValueVO("剧情"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("002"), Name = new CodeValueVO("爱情"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("003"), Name = new CodeValueVO("奇幻"), Order = new SortOrderVO(3), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("004"), Name = new CodeValueVO("惊悚"), Order = new SortOrderVO(4), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("005"), Name = new CodeValueVO("喜剧"), Order = new SortOrderVO(5), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("006"), Name = new CodeValueVO("动作"), Order = new SortOrderVO(6), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("007"), Name = new CodeValueVO("科幻"), Order = new SortOrderVO(7), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("008"), Name = new CodeValueVO("冒险"), Order = new SortOrderVO(8), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("009"), Name = new CodeValueVO("悬疑"), Order = new SortOrderVO(9), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("010"), Name = new CodeValueVO("历史"), Order = new SortOrderVO(10), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("011"), Name = new CodeValueVO("犯罪"), Order = new SortOrderVO(11), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("012"), Name = new CodeValueVO("西部"), Order = new SortOrderVO(12), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.MovieGenre, Code = new CodeKeyVO("013"), Name = new CodeValueVO("歌舞"), Order = new SortOrderVO(13), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("001"), Name = new CodeValueVO("英语"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("002"), Name = new CodeValueVO("法语"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("003"), Name = new CodeValueVO("意大利语"), Order = new SortOrderVO(3), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("004"), Name = new CodeValueVO("拉丁语"), Order = new SortOrderVO(4), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("005"), Name = new CodeValueVO("欧塞奇语"), Order = new SortOrderVO(5), CreatedOn  = dateTime },


            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("001"), Name = new CodeValueVO("美国"), Order = new SortOrderVO(3), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("002"), Name = new CodeValueVO("澳大利亚"), Order = new SortOrderVO(6), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("003"), Name = new CodeValueVO("大陆"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("004"), Name = new CodeValueVO("香港"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("005"), Name = new CodeValueVO("日本"), Order = new SortOrderVO(4), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("006"), Name = new CodeValueVO("韩国"), Order = new SortOrderVO(5), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("007"), Name = new CodeValueVO("英国"), Order = new SortOrderVO(6), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("008"), Name = new CodeValueVO("加拿大"), Order = new SortOrderVO(7), CreatedOn  = dateTime },

        };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<CelebrityEntity> GetCelebrities(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext) =>
       new List<CelebrityEntity>
       {
            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("蒂姆·波顿"),
                Aka = new CelebrityAkaVO("蒂姆·伯顿 / 提姆·波顿 / 添·布顿"),
                NameEn = new CelebrityNameEnVO("Timothy William Burton"),
                AkaEn = new CelebrityAkaEnVO("Timothy William Burton (本名)"),
                Gender = GenderVO.Codes.GenderCode1,
                Professions = new ProfessionsVO("导演 / 制片人 / 编剧 / 演员 / 美术"),
                Birthday = new BirthdayVO(new DateOnly(1958, 8, 25)),
                BornPlace = new BornPlaceVO("美国,加利福尼亚州,伯班克"),
                Family = new FamiliesVO(" Helena Bonham Carter(前妻) / Lisa Marie(前妻) / Monica Bellucci(女友)"),
                Avatar = new StarAvatarVO("20231022132511.png"),
                DoubanID = new DoubanIDVO("1019002"),
                IMDbID = new IMDbIDVO("nm0000318"),
                Summary = new SummaryVO("　　美国电影导演，生于加利福尼亚州的伯班克。他在加州艺术学院学习时得到了迪士尼的奖学金，这是用来赞助给年轻动画人以帮助他们成就梦想的基金。由此他开始正式成为迪士尼的动画师，之后成为导演。蒂姆·伯顿热衷描绘错位，善于运用象征和隐喻的手法，常以黑色幽默，独特的视角而著称。其代表作有《蝙蝠侠》、《大鱼》、《剪刀手爱德华》等。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("卡罗琳·汤普森"),
                NameEn = new CelebrityNameEnVO("Caroline Thompson"),
                Gender = GenderVO.Codes.GenderCode2,
                Professions = new ProfessionsVO("编剧 / 导演 / 制片人 / 演员"),
                Birthday = new BirthdayVO(new DateOnly(1956, 4, 23)),
                BornPlace = new BornPlaceVO("美国,华盛顿哥伦比亚特区"),
                Avatar = new StarAvatarVO("20231022140533.png"),
                DoubanID = new DoubanIDVO("1045371"),
                IMDbID = new IMDbIDVO("nm0003031"),
                Summary = new SummaryVO("　　Caroline Thompson (born April 23, 1956) is an American novelist, screenwriter, film director, and producer. She wrote the screenplays for Tim Burton's films Edward Scissorhands, The Nightmare Before Christmas, and Corpse Bride. She co-wrote the story for Edward Scissorhands and recently co-adapted a new stage version of the film with director and choreographer Matthew Bourne. Thompson also adapted the screenplay for the film version of Wicked Lovely, a bestselling fantasy series, in 2011, but the production was put into turnaround."),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("娜塔莉·波特曼"),
                Aka = new CelebrityAkaVO("娜塔莉·波特曼"),
                NameEn = new CelebrityNameEnVO("Natalie Hershlag"),
                AkaEn = new CelebrityAkaEnVO("Natalie Hershlag (本名) / Nat (昵称)"),
                Gender = GenderVO.Codes.GenderCode2,
                Professions = new ProfessionsVO("演员 / 制片人 / 配音 / 导演 / 编剧"),
                Birthday = new BirthdayVO(new DateOnly(1981, 6, 9)),
                BornPlace = new BornPlaceVO("以色列,耶路撒冷"),
                Family = new FamiliesVO("本杰明·米派德(夫) / 阿夫纳·赫许勒(父) / 雪莉·史蒂文斯(母) / 阿列夫·波特曼·米派德(儿) / 阿玛莉亚·波特曼·米派德(女)"),
                Avatar = new StarAvatarVO("20231022124041.png"),
                DoubanID = new DoubanIDVO("1054454"),
                IMDbID = new IMDbIDVO("nm0000204"),
                Summary = new SummaryVO("　　娜塔丽·波特曼，美国著名女演员。因出演吕克·贝松导演的《杀手里昂》一鸣惊人，自此开始了一边读书一边拍戏的生涯。\r\n娜塔莉好像总能与好莱坞大腕们同台演出。接下来1995年她在麦克尔曼的《盗火线》中做了艾尔帕西诺的继女，1996年在《火星人攻击地球》中出演杰克尼科尔森的女儿。同年她又出现在伍迪·艾伦的音乐喜剧《人人都说我爱你》里。娜塔莉在剧中的轻松表演赢得了观众的喜爱。\r\n　　1996年娜塔莉在电影《美丽佳人》中的演出又一次获得好评如潮。在剧中她扮演的女孩叫马蒂，那可是个颇有心计的早熟女孩，一心一意想把心上人从他的未婚妻手中夺走。评论家对娜塔莉在剧中的演技评价甚高，有一点绝不可否认：她是整部剧中最出色的亮点。\r\n　　1997年娜塔莉作出一个惊人的决定：她竟然推掉《洛丽塔》和《罗密欧与朱莉叶》两部巨片，心甘情愿地登上舞台出演《安妮·弗兰克的日记》。这个举动曾让众人大惑不解，但是从《安妮》一剧每次结束后长达近一个小时的谢幕中，我们就明白她演的有多么出色。娜塔莉在舞台演了整整1年，直到1998年才离开《安妮》剧组。\r\n　　世纪末娜塔莉出演了她最为广泛议论的角色——在风靡全球的乔治卢卡斯的《星球大战前传：幽灵的威胁》中饰演高贵而美丽的阿米达拉女王。尽管毁誉皆有，这部影片创下了全球巨额票房却是一个不争的事实。而娜塔莉的美丽形象也如同当年的赫本世人皆知。\r\n　　从最初站在镁光灯下到今天，昔日的女孩如今已长大，纯真依旧，但是却多了份成熟女人的自信与风情。娜塔莉正用自己的实际行动向世界证明——她是好莱坞新一代演员中最具实力，最有前途的女星之一。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("约翰尼·德普"),
                Aka = new CelebrityAkaVO("尊尼狄普 / 强尼戴普 / 尊尼特普"),
                NameEn = new CelebrityNameEnVO("John Christopher Depp II (本名) / Colonel (昵称) / Mr. Stench (昵称)"),
                Gender = GenderVO.Codes.GenderCode1,
                Professions = new ProfessionsVO("演员 / 配音 / 制片人 / 导演 / 编剧"),
                Birthday = new BirthdayVO(new DateOnly(1963, 6, 9)),
                BornPlace = new BornPlaceVO("美国,肯塔基,欧文斯伯勒"),
                Family = new FamiliesVO("John Christopher Depp(父) / Betty Sue Palmer(母) / Amber Heard(前妻) / Christie Dembrowski(姐) / Lily-Rose Depp(女)"),
                Avatar = new StarAvatarVO("20231021184242.png"),
                DoubanID = new DoubanIDVO("1054456"),
                IMDbID = new IMDbIDVO("nm0000136"),
                Summary = new SummaryVO("　　约翰尼·德普，1963年6月出生于美国肯塔基州的一个中产家庭，在佛罗里达州长大。15岁时他的父母离异，这让他一度成为不良少年并染上毒瘾。在舅父的唱诗班他迷恋上了音乐，还曾经自组摇滚乐队。在1983年，为了成为摇滚歌手，他前往洛杉矶发展。同年，他与他的第一任妻子，化妆师Lori Anne Allison结婚，并通过她认识了尼古拉斯·凯奇。正是在凯奇的大力引荐下，约翰尼·德普开始踏入影视圈，走上星路。\r\n　　小时候他不喜欢念书，经常一个人关在房间里苦练吉他，高中辍学后，他前往洛杉矶寻求发展，希望成为摇滚歌星。但阴错阳差，一次偶然试镜却让他与表演结了缘。他领衔主演的电视剧《龙虎少年队》播出后，迅速成为美国青少年的偶像。1984年，约翰尼·德普进入电影圈，在恐怖片《猛鬼街》中饰演角色，随后又在鬼才导演蒂姆·波顿的《剪刀手爱德华》中扮演主角，并凭借此片首次获得金球奖提名。拍摄期间，他与片中扮演女主角的维诺娜·赖德陷入热恋并订了婚，但这场婚约仅维持了三年便宣告破裂。1994年，他与鬼才导演蒂姆·波顿二次合作了另类黑白片《艾德伍德》，他凭借精湛绝伦的演出第三次荣获金球奖最佳男主角奖提名。此后，德普比波顿的合作一直持续至今。但在此期间，德普也出演了一系列脍炙人口的影片、塑造了很多令人津津乐道的角色，其中以《加勒比海盗》系列中的“杰克船长”最为著名。\r\n　　除了演戏，约翰尼·德普在导演方面也显现出了才华。他于1997年自编自导自演了《勇气》(The Brave)一片，有大牌明星马龙·白兰度加盟提携，影片夺得了多项奖项，1997年的坎城影展并将它列入竞赛片,让他在好莱坞出尽了风头。不仅如此，2009年德普又将小说《朗姆酒日记》搬上了银幕，自导自演。\r\n　　德普除了别具特色的演技之外，引人注意的还有其恋爱史。与他谈过恋爱的女星名单可列出一长串，从薇诺娜·赖德到雪琳芬·珍妮佛葛蕾，再到骨感名模凯特·摩丝。性格刚烈的约翰尼·德普曾因与凯特·摩丝发生争吵，愤而捣毁了纽约一个每晚两千美元的饭店客房，并因此被捕入狱。1997年，约翰尼·德普爱上了法国女演员兼歌手凡妮莎·帕拉迪丝，2年后凡妮莎为他生下了女儿，取名百合玫瑰旋律德普(Lily-Rose Melody Depp)。此后，曾以颓废形象定格银幕的约翰尼·德普开始以慈父形象出现在各类报刊杂志，推着婴儿车，拿着尿布，经常出入各种社交派对，这与他以往的形象可谓天壤之别。\r\n　　自从20世纪80年代处就一直有纹身，在他身上共有12块纹身。他身上的纹身包括在他右前臂上纹的 他儿子的名字、在他左胸心脏位置文的他女儿的名字、在他右前臂纹的一个本土美国人的轮廓，他以次表达他对切罗基血统的敬意。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("薇诺娜·瑞德"),
                Aka = new CelebrityAkaVO("云露娜·维达 / 维诺娜·赖德 / 薇诺娜·劳拉·霍洛威兹(本名)"),
                NameEn = new CelebrityNameEnVO("Winona Laura Horowitz(本名) / Noni(昵称)"),
                AkaEn = new CelebrityAkaEnVO("Winona Laura Horowitz(本名) / Noni(昵称)"),
                Gender = GenderVO.Codes.GenderCode2,
                Professions = new ProfessionsVO("演员 / 配音 / 制片人 / 编剧"),
                Birthday = new BirthdayVO(new DateOnly(1958, 8, 25)),
                BornPlace = new BornPlaceVO("美国,明尼苏达,奥姆斯特德郡"),
                Family = new FamiliesVO("约翰尼·德普(前男友) / 马特·达蒙(前男友) / 欣蒂·霍洛威兹(母) / 麦克·霍洛威兹(父)"),
                Avatar = new StarAvatarVO("20231022141507.png"),
                DoubanID = new DoubanIDVO("1012519"),
                IMDbID = new IMDbIDVO("nm0000213"),
                Summary = new SummaryVO("　　薇诺娜·瑞德，美国女演员，曾在美国戏剧学院修演艺课。1985年，瑞德寄出了一个试演录像带，希望能在电影《Desert Blom》里串上一角，但是不获录用。尽管如此，一位叫戴维•塞尔哲的作家和导演对她印象深刻，并让她在《美国小子》里饰演主角的朋友。之后将姓氏改为“瑞德”，做为她在片尾工作人员名单出现的名字。1987年，薇诺娜出演了影片《少女离家时》，被洛杉矶时报评为“一个值得注意的处女作”。1988年在影片《阴间大法师》中饰演了一个穿着歌德式服装的颓丧少女，获得了绝大多数的正面评价。\r\n　　1989年，薇诺娜在黑色喜剧，《希德姐妹帮》中亮相。薇诺娜的经理人曾恳求她放弃演出，理由是此剧会“糟蹋了她的前途”。这部影片初次上映时在票房上遭遇挫折，但录像带在发行后却取得高销售量和出租量。尽管大家对影片中出现争议性的少年暴力事件反应冷淡，但薇诺娜的表现还是可圈可点。《华盛顿邮报》此评道“好莱坞最受曙目的天真无邪的少女……瑞德……让我们爱上她的少年凶犯角色：一个机灵,有趣， 带有一丝Bonnie Parker（美国二十世纪初声名狼藉的女罪犯）影子的女孩子。她是续《Gregory's Girl》内纯洁无辜的少女之后最可爱，最精选的年轻角色”。那年之后，她主演了《大火球》里Jerry Lee Lewis的十三岁新娘。\r\n　　1990年，薇诺娜在三部片子中演出，第一部是和她后来的男友，约翰尼·德普一起主演的《剪刀手爱德华》。之后，她去了意大利罗马拍弗朗西斯•科波拉的《教父III》。但她以“完全不能起床”为由辞演了此片，生病让瑞德取消了这计划。 薇诺娜的下一个角色出现在《怒海娇娃》（1990年）。薇诺娜凭片中Charlotte Flax一角，获得了金球奖最佳女配角提名。\r\n　　在演过多部少女片后，1991年接演吉姆·贾木许的《地球之夜》正式迈入成熟角色，而次年大导演科波拉的《惊情四百年》更将她推上票房明星的地位，自此成为好莱坞新一代的接班女星之一。\r\n　　长相甜美的赖德，一双水汪汪的大眼睛不知迷死多少影迷，帅哥约翰尼·德普、才子马特·达蒙都曾是她的裙下之臣。\r\n　　虽然被上天无限眷顾，现实中的薇诺娜却相当反叛，甚至因为盗窃而引起轩然大波，也因此严重影响了她近年在影坛的发展。这几年她重整旗鼓，以全新的心态和形象开展事业。2010年与波曼一起出演的影片《黑天鹅》，虽然在片中只是扮演配角，但薇诺娜的表现让喜爱她的影迷没有失望。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("黛安·韦斯特"),
                Aka = new CelebrityAkaVO("黛安娜·维斯特"),
                NameEn = new CelebrityNameEnVO("Diane Wiest"),
                Gender = GenderVO.Codes.GenderCode2,
                Professions = new ProfessionsVO("演员 / 配音 / 制片人 / 编剧"),
                Birthday = new BirthdayVO(new DateOnly(1948, 3, 28)),
                BornPlace = new BornPlaceVO("美国,密苏里州,堪萨斯城"),
                Avatar = new StarAvatarVO("20231022143552.png"),
                DoubanID = new DoubanIDVO("1002725"),
                IMDbID = new IMDbIDVO("nm0001848"),
                Summary = new SummaryVO("　　1948年生人。原来的志向是当芭蕾舞演员。后来80年跑去演电影，在《礼尚往来 It's My Turn》里打酱油。再后来又演了《追梦人生 Independence Day》、《浑身是劲 Footloose》、《坠入情网 Falling in Love》，一直到85年伍迪艾伦的《 The Purple Rose of Cairo开罗紫玫瑰》里一个大萧条时期的妓女角色，给人留下了深刻印象。伍迪艾伦第二年的电影《汉娜姐妹 Hannah and Her Sisters》又邀请她来演女配角，这次终于拿到了奥斯卡最佳女配角奖并且获得了金球奖提名。\r\n出名之后又拍了《粗野少年族 The Lost Boys》、伍迪艾伦的《情怀九月天 September》、《灯红酒绿 Bright Lights, Big City》等片，其中87年伍迪艾伦的《无线电时代 Radio Days》让她拿到了BAFTA奖提名。\r\n89年的《温馨家族 Parenthood》再度让她提名奥斯卡最佳女配角奖并且提名金球奖、提名美国喜剧奖。随后名导蒂姆波顿又来找他演了《剪刀手爱德华 Edward Scissorhands》的peg夫人，这一角色让他提名土星奖最佳女配角奖。\r\n随后，朱迪福斯特导演的《锦绣童年 Little Man Tate》、迈克尔里奇导演的《条子与妙家庭 Cops and Robbersons》和《球探 The Scout》都有她的角色，不过还是伍迪艾伦的作品最适合她呀！94年的电影《Bullets Over Broadway子弹横飞百老汇》让他第二次拿到了奥斯卡最佳女配角奖，也终于获得了金球奖，获得了美国喜剧奖，获得了独立精神奖，获得了屏幕演员工会奖的单人奖项，并且赢得了好几个影评人协会的奖项。96年客串了电视剧《通往埃文利之路Road to Avonlea》拿到了艾美奖最佳客串女演员奖，同年的电影《鸟笼 The Birdcage》让她第二次拿到了美国喜剧奖，又拿到了屏幕演员工会奖的一个集体奖项。\r\n98年又演了《马语者 The Horse Whisperer》和《超异能快感 Practical Magic》，后者获得了美国喜剧奖提名。99年《平凡的诺亚 The Simple Life of Noah Dearborn》又获艾美奖迷你剧/电视电影最佳女配角提名。\r\n00年左右基本都在电视行业，00年拍了魔幻题材迷你剧《第十王朝 The 10th Kingdom》再里面演Evil Queen；又去《法律与秩序》主剧演了四十八集的D.A. Nora Lewin，中间抽空演了西恩潘的《我是山姆 I Am Sam》。随后虽然演了几部大明星坐镇的电影《圣徒指南 A Guide to Recognizing Your Saints》《亲亲老爸 Dan in Real Life》等，但是除了04年的《暗水船灯 The Blackwater Lightship》拿到了金卫星奖以外，这6年基本沉寂了下来。\r\n直到08年《纽约提喻法 Synecdoche, New York》再获独立精神奖的集体奖项，而由她在电视剧《扪心问诊In Treatment》演的Dr. Gina Toll，08年获得艾美剧情类最佳女配角奖，09年获得艾美奖提名和金球奖提名卫星奖提名，到了2010年的第三季她不再出演，这部剧也就三季剧终了。\r\n她继续奋斗在电影行业，2010年和妮可基德曼演的《兔子洞 Rabbit Hole》又拿了卫星奖提名，11年演了喜剧《观鸟大年 The Big Year》、去年演了《蒂莫西的奇异生活 The Odd Life of Timothy Green》。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("达伦·阿伦诺夫斯基"),
                Aka = new CelebrityAkaVO("达伦·阿罗诺夫斯基 / 戴伦·艾洛诺夫斯基"),
                NameEn = new CelebrityNameEnVO("Darren Aronofsky"),
                Gender = GenderVO.Codes.GenderCode1,
                Professions = new ProfessionsVO("制片人 / 导演 / 编剧 / 演员 / 副导演"),
                Birthday = new BirthdayVO(new DateOnly(1969, 2, 12)),
                BornPlace = new BornPlaceVO("美国,纽约,布鲁克林"),
                Family = new FamiliesVO("蕾切尔·薇兹(前女友) / 詹妮弗·劳伦斯(前女友)"),
                Avatar = new StarAvatarVO("20231022155029.png"),
                DoubanID = new DoubanIDVO("1041000"),
                IMDbID = new IMDbIDVO("nm0004716"),
                Summary = new SummaryVO("　　1969年2月出生在纽约的布鲁克林，从小就喜欢古典电影，少年时期开始喜欢涂鸦。后来他进入哈佛大学学习电影，在学校的作品还屡次获奖。1996年2月，他开始筹备自己的长片处女作《圆周率》，两年后面世的这部电影一鸣惊人，先是获得圣丹斯电影节的导演奖，翌年又摘得独立精神奖的最佳编剧处女作奖。2000年，他再次自编自导了《梦之安魂曲》，也赢得了不错的反响。2005年，他还入围“最需要关注的好莱坞100人”。 导演作品2006年，阿罗诺夫斯基推出自己第三部导演作品《珍爱泉源》，这部影片的完成可谓历尽艰难：2002年初，阿罗诺夫斯基曾希望由布拉德·皮特和凯特·布兰切特主演本片，计划的投资预算也高达7500万美元。后来由于布拉德·皮特同阿罗诺夫斯基产生意见分歧而决定放弃，影片被搁置下来，而2004年影片开拍时，预算已经降到3500万，主演也变成了在当时比较“便宜”的休·杰克曼和蕾切尔·薇兹。不过对于拍惯独立影片的阿罗诺夫斯基来说，资金问题是难不倒他的，因为拍摄《圆周率》只用了6万，《梦之安魂曲》也不过400万。\r\n　　2008年，达伦的第四部作品《摔角王》捧得威尼斯金狮大奖。\r\n　　阿罗诺夫斯基的前妻就是主演了《珍爱泉源》的蕾切尔·薇兹，两人2006年5月31日成为父母，他们的儿子叫Henry Chance。不过两人已于2010年底分手。\r\n　　现已与福克斯公司签约两年,从《金刚狼2》起深度合作,令人期待.3月18日二十世纪福斯公司与达伦·阿罗诺夫斯基共同发表声明，宣布他将退出《金刚狼2》剧组，放弃执导该片。本片原计划这个月开机，2012年上映。\r\n　　达伦将担任2011年威尼斯国际电影节主竞赛单元评委会主席。达伦的五部长片中，三部的首映都放在了威尼斯电影节上，包括《珍爱源泉》、《摔角王》、《黑天鹅》。《黑天鹅》曾作为2010年威尼斯电影节开幕影片。\r\n　　达伦的新作《诺亚》被称作是他的野心之作，前不久刚刚在法国发布了他与漫画家Nico Henrichon合作的该片的迷你漫画第一辑。本片也将是达伦迄今最大规模的制作，据称投资将达到1.3亿元。对于这个宗教意味很重的故事，达伦并不想过于强调其宗教色彩：“我不认为这是个非常宗教的故事。我觉得这是一个伟大的寓言，来自各种宗教和精神实践。我认为这是一个从来没在银幕上表现过的伟大故事。”\r\n　　在《圣经》中，上帝要用洪水毁灭地上的人类，他选中了诺亚一家作为新人类的种子。诺亚一边赶造方舟，一边劝告世人悔改其行为。诺亚花了整整120年时间终于造成了一只庞大的方舟，并听从上帝的话，把全家八口搬了进去，各种飞禽走兽也一对对赶来，有条不紊地进入方舟。7天后，洪水自天而降，一连下了40个昼夜，人群和动植物全部陷入没顶之灾。几十天后，鸽子衔来了橄榄枝，表示大水已经消退，方舟上的人类和万物重新开始在地上繁衍。\r\n　　《诺亚》有望2012年春天开机，2014年上映。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("塔伊加·维迪提"),
                Aka = new CelebrityAkaVO("塔基·科昂 / 泰格·韦替替 / 泰卡·怀蒂蒂"),
                NameEn = new CelebrityNameEnVO("Taika Waititi"),
                Gender = GenderVO.Codes.GenderCode1,
                Professions = new ProfessionsVO("导演 / 演员 / 编剧 / 制片人 / 配音"),
                Birthday = new BirthdayVO(new DateOnly(1975, 8, 16)),
                BornPlace = new BornPlaceVO("新西兰"),
                Family = new FamiliesVO("Rita Ora(妻)"),
                Avatar = new StarAvatarVO("20231022162211.png"),
                DoubanID = new DoubanIDVO("1076354"),
                IMDbID = new IMDbIDVO("nm0169806"),
                Summary = new SummaryVO("塔伊加·维迪提（有时也采用科恩这个姓氏）是蒂-法脑-阿-阿帕努伊部落后裔，来自东海岸的劳库库利地区。他涉足艺术界多年，是一名视觉艺术家、演员、编剧和导演。泰卡的第一部短片《两车一夜》曾入围2005学院奖。他的另一部短片《塔玛图》是关于二战中在意大利战斗的一群毛利士兵的故事，赢得了许多国际奖，并使其有资格入围奥斯卡奖。他的首部故事片《鹰对鲨》以宣传片的形式出售给米拉马克斯并于2007年在国际上发行。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("安德雷斯·海因斯"),
                Gender = GenderVO.Codes.GenderCode1,
                Avatar = new StarAvatarVO("20231022155307.png"),
                DoubanID = new DoubanIDVO("1313056"),
                IMDbID = new IMDbIDVO("nm0374560"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("詹尼弗·凯廷·罗宾逊"),
                NameEn = new CelebrityNameEnVO("Jennifer Kaytin Robinson"),
                Gender = GenderVO.Codes.GenderCode2,
                Avatar = new StarAvatarVO("20231022162507.png"),
                DoubanID = new DoubanIDVO("1418806"),
                IMDbID = new IMDbIDVO("nm3066492"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("斯坦·李"),
                NameEn = new CelebrityNameEnVO("Stan Lee"),
                AkaEn = new CelebrityAkaEnVO("Stanley Martin Lieber (本名) / The Man (昵称)"),
                Gender = GenderVO.Codes.GenderCode1,
                Avatar = new StarAvatarVO("20231022162650.png"),
                DoubanID = new DoubanIDVO("1013888"),
                IMDbID = new IMDbIDVO("nm0498278"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("　　2008，两部美国大片——《钢铁侠》和《无敌浩克》在全世界刮起一股超级英雄狂潮，分别席卷全球票房达5.67亿美元和2.28亿美元。这两部大片的横空出世再次勾起了人们对漫画历史上那些超级英雄的记忆。蜘蛛侠、神奇四侠、X战警、夜魔侠……当然也包括钢铁侠和绿巨人，这一个个耳熟能详的名字都与一个人息息相关。他就是被尊为“漫画之王”、“蜘蛛侠之父”的斯坦·李。\r\n　　90岁的斯坦·李一手将惊奇漫画公司打造为全世界最为成功的动漫王国，神奇漫画总共创造了5000多个漫画角色，90%以上知名的角色都由斯坦·李参与创造。半个多世纪以来，美国年轻人都是读着斯坦·李的作品长大的，其漫画作品在全世界的销售量超过了20亿册，蜘蛛侠更是成为全世界最受欢迎的超级英雄。在美国，“神奇先生”斯坦·李已不仅仅是一个名人，而成为一个文化符号。如今，已届暮年的斯坦·李仍然“壮心不已”，前不久，他宣布将与迪士尼联手，推出三部新电影。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("杰森·亚伦"),
                Gender = GenderVO.Codes.GenderCode1,
                Avatar = new StarAvatarVO("20231022162908.png"),
                DoubanID = new DoubanIDVO("1474579"),
                IMDbID = new IMDbIDVO("nm3883355"),
                Summary = new SummaryVO("漫画家、漫威编剧。"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("马克·海曼"),
                Gender = GenderVO.Codes.GenderCode1,
                Avatar = new StarAvatarVO("20231022155755.png"),
                DoubanID = new DoubanIDVO("1313056"),
                IMDbID = new IMDbIDVO("nm2114730"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("约翰·J·麦克劳克林"),
                Gender = GenderVO.Codes.GenderCode1,
                Avatar = new StarAvatarVO("20231022161742.png"),
                DoubanID = new DoubanIDVO("1313057"),
                IMDbID = new IMDbIDVO("nm0572352"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("米拉·库尼斯"),
                Gender = GenderVO.Codes.GenderCode2,
                Avatar = new StarAvatarVO("20231022165234.jpg"),
                DoubanID = new DoubanIDVO("1003481"),
                IMDbID = new IMDbIDVO("nm0005109"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("克里斯·海姆斯沃斯"),
                Aka = new CelebrityAkaVO("基斯·咸士禾夫(港) / 克里斯·汉斯沃(台) / 锤哥(昵称) / 海总(昵称)"),
                NameEn = new CelebrityNameEnVO("克里斯·海姆斯沃斯"),
                Professions = new ProfessionsVO("演员 / 制片人 / 配音"),
                Gender = GenderVO.Codes.GenderCode2,
                BornPlace = new BornPlaceVO("澳大利亚,维多利亚,墨尔本"),
                Family = new FamiliesVO("利亚姆·海姆斯沃斯(弟) / 卢克·海姆斯沃斯(兄) / 埃尔莎·帕塔奇(妻)"),
                Avatar = new StarAvatarVO("20231022163325.png"),
                DoubanID = new DoubanIDVO("1021959"),
                IMDbID = new IMDbIDVO("nm1165110"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

            new(){
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("温子仁"),
                Aka = new CelebrityAkaVO("James Wan"),
                NameEn = new CelebrityNameEnVO("詹姆斯·温"),
                Professions = new ProfessionsVO("制片人 / 编剧 / 导演 / 演员 / 剪辑"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("马来西亚,古晋"),
                Family = new FamiliesVO("Ingrid Bisu(妻)"),
                Avatar = new StarAvatarVO("20240126152001.png"),
                DoubanID = new DoubanIDVO("1032122"),
                IMDbID = new IMDbIDVO("nm1490123"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("出生于马来西亚、成长于澳洲的华裔导演。早年在墨尔本上大学时主修的专业是中国民族划分。对于东方异域文化的了解对詹姆斯·温的导演风格也有着很大的影响，而造就他对于《电锯惊魂》和《死寂》(dead silence)之类血浆片独特口味的更重要的因素是他14岁时父亲过世的打击。说到温，就不得不提他的黄金搭档雷·沃纳尔。天才都不是一蹴而成，早在大学期间，俩人就已经有野心写出一部能打上时代烙印的恐怖片，同大多数老牌恐怖影迷一样，德州电锯杀人狂，沉默的羔羊等经典恐怖片给予了它们很大的灵感，终于在04年，温和沃纳尔共同撰写好了电锯惊魂的最初剧本，俩人勉强筹备好资金后拍摄了一部画面粗糙的小电影，随后递交给狮门影业，狮门看后当即决定采用，詹姆斯温亲自担任导演，至此，电锯惊魂---一个具有时代影响力的恐怖片系列诞生了，虽然在3部以后，温隐居到幕后工作，但依然掩盖不了‘电锯之父’这一美誉。随后俩人创作了《死寂》，虽然这部纯鬼片最终反响平平，但影片的‘温雷’风格仍然给影迷留下了极深的印象，2011年俩人再度推出低成本作品《阴儿房》，这部作品具有典型的复古杂糅风格，对当今的恐怖片具有一定颠覆色彩，而且最终口碑和票房也算是获得双丰收。詹姆斯·温无疑是未来最具潜力的恐怖片导演。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
            },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("大卫·莱斯利·约翰逊-麦戈德里克"),
                Aka = new CelebrityAkaVO("David Leslie Johnson-McGoldrick"),
                NameEn = new CelebrityNameEnVO("大卫·莱斯利·约翰逊 / 大卫·约翰逊"),
                Professions = new ProfessionsVO("编剧"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国"),
                Family = new FamiliesVO("Kimberly Lofstrom Johnson(前妻)"),
                Avatar = new StarAvatarVO("20240126152001.png"),
                DoubanID = new DoubanIDVO("1351120"),
                IMDbID = new IMDbIDVO("nm0424901"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("1993年，大卫·约翰森从俄亥俄州大学电影专业毕业，这时一部名叫《肖申克的救赎》的电影正在他得家乡曼斯菲尔德拍摄。约翰逊成功地得到了一份工作，后便被导演弗兰克·德拉邦特带到好莱坞，成为他的助理。五年时间内，约翰逊一直在写剧本，并且得到了德拉邦特的支持。2009年他第一个被拍成的剧本《孤儿》上映，这是一部关于领养儿童的恐怖影片。他也是09年《综艺》杂志选出的10位有前途的新人编剧之一。斯皮尔伯格的电影和斯蒂芬·金的书对约翰逊影响颇深，“他们代表了我的两类热情：冒险大戏和恐怖小说”。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("杰森·莫玛"),
                Aka = new CelebrityAkaVO("Jason Momoa"),
                NameEn = new CelebrityNameEnVO("约瑟夫·杰森·纳马卡埃哈·莫玛"),
                Professions = new ProfessionsVO("演员 / 编剧 / 导演 / 配音 / 制片人"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,夏威夷,檀香山"),
                Family = new FamiliesVO("Lisa Bonet(前妻) / Lola Iolani Momoa(女) / Nakoa-Wolf Manakauapo Namakaeha Momoa(子) / Eiza González(女友)"),
                Avatar = new StarAvatarVO("20240126153240.png"),
                DoubanID = new DoubanIDVO("1022614"),
                IMDbID = new IMDbIDVO("nm0597388"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("杰森·莫玛（Jason Momoa），美国演员，1979年08月01日出生于美国夏威夷，曾主演《北海迷情》(North Shore)和《星际之门亚特兰蒂斯》(Stargate Atlantis)。在HBO电视台播出的魔幻巨制《冰与火之歌：权力的游戏》中，出演Khal Drogo一角。在《蛮王柯南》中扮演主角柯南，出道至今塑造了众多的硬汉形象。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("帕特里克·威尔森"),
                Aka = new CelebrityAkaVO("Patrick Wilson"),
                AkaEn = new CelebrityAkaEnVO("Patrick Joseph Wilson (本名)"),
                Professions = new ProfessionsVO("演员 / 配音 / 导演 / 制片人"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,弗吉尼亚,诺福克"),
                Family = new FamiliesVO("Dagmara Dominczyk(妻) / Kalin Patrick (长子) / Kassian McCarrel(次子)"),
                Avatar = new StarAvatarVO("20240126153738.png"),
                DoubanID = new DoubanIDVO("1006919"),
                IMDbID = new IMDbIDVO("nm0933940"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("帕特里克的父亲约翰是名电视主持人，妈妈玛丽是个专业歌手同时还会教授学生如何发声，父母的遗传基因这让他在戏剧表演方面更多了一份天赋。成长在这样一个文艺家庭，也让帕特里克从小就对登上舞台充满梦想。可以说，帕特里克·威尔森是才华型的演员，他毕业于世界顶尖的内基·梅隆私立大学的戏剧系，并且取得了美术学士学位，值得一提的是，曾因《钢琴课》一片荣获奥斯卡最佳女主角殊荣的霍莉·亨特也毕业于此，良好的学业经历为帕特里克登上表演舞台打下了坚实的基础，科班毕业也让他在接下来的表演岁月里表现得游刃有余。大学毕业后，帕特里克·威尔森就开始在百老汇演出，与其他表演者命运不同的是，帕特里克超然的自信和俊美的外形获得大部分剧组的青睐，再加上良好的艺术修为，使得帕特里克一开始便得到男主角的头衔，而真正让他成功地走到戏剧巅峰的就是音乐剧《俄克拉荷马》与根据同名电影改编的音乐剧《光猪六壮士》，这两出音乐剧让帕特里克两度获得托尼奖最佳音乐剧男主角的提名。在《俄克拉荷马》中，帕特里克扮演的库利是个乐观、幽默、热情、以捉弄女主角为乐的年轻牛仔，虽然四肢发达且头脑却像孩子般顽皮。以帕特里克的柔弱书生气能够体现出那种让人信服的粗犷也着实不容易。他在演出中刚出场时哼唱的那句“Oh，What a Beautiful Mornin”完全得益于他那天生的好嗓音，听起来也格外让人赏心悦目；而他在《光猪六壮士》中的那幕经典的性感舞蹈，也曾经令台下的女观众和男同志们尖叫到疯狂。正是在舞台上的顺风顺水让帕特里克受到了好莱坞的关注，冥冥中，他的表演舞台注定将变得更加宽广。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("叶海亚·阿卜杜勒-迈丁"),
                Aka = new CelebrityAkaVO("Yahya Abdul-Mateen II"),
                NameEn = new CelebrityNameEnVO("叶海亚·阿卜杜勒-迈丁二世"),
                Professions = new ProfessionsVO("演员"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,路易斯安那,新奥尔良"),
                Family = new FamiliesVO(""),
                Avatar = new StarAvatarVO("20240126154154.png"),
                DoubanID = new DoubanIDVO("1374708"),
                IMDbID = new IMDbIDVO("nm5584344"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO(""),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("安珀·赫德"),
                Aka = new CelebrityAkaVO("Amber Laura Heard(本名)"),
                NameEn = new CelebrityNameEnVO("安柏·希尔德, 安布尔·赫德, 安柏·赫德, 艾梅柏·希尔德"),
                Professions = new ProfessionsVO("演员"),
                Gender = GenderVO.Codes.GenderCode2,
                BornPlace = new BornPlaceVO("美国,得克萨斯,奥斯汀"),
                Family = new FamiliesVO("Johnny Depp(前夫) / Whitney Heard(妹)/Elon Musk(前男友)"),
                Avatar = new StarAvatarVO("20240126154435.png"),
                DoubanID = new DoubanIDVO("1044702"),
                IMDbID = new IMDbIDVO("nm1720028"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("安珀·赫德生于1986年4月22日美国德克萨斯州奥斯汀市，家中有两个姐妹，父亲是一名成功的商人。脸蛋与身材俱佳的艾梅柏初入好莱坞时，只在一些电视节目和小制作的电影里扮演一些青春性感的配角，她主演过小成本恐怖片《爱你至死不渝》，还参演过《棕榈泉秘事》、《美好的普通人》等影视剧。2008年，她参与了贾德·阿帕图的《菠萝快车》等片，渐渐被人认识。美好的未来正在向她招手。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("妮可·基德曼"),
                Aka = new CelebrityAkaVO("Nicole Kidman"),
                NameEn = new CelebrityNameEnVO("妮歌洁曼(港) / 妮科尔基曼 / 妮可基嫚(台)"),
                AkaEn = new CelebrityAkaEnVO("Nicole Mary Kidman (本名) / Nic (昵称)"),
                Professions = new ProfessionsVO("演员 / 制片人 / 配音 / 编剧"),
                Gender = GenderVO.Codes.GenderCode2,
                BornPlace = new BornPlaceVO("美国,夏威夷,火奴鲁鲁"),
                Family = new FamiliesVO("Keith Urban (丈夫) / Sunday Rose Kidman Urban (女儿) / Faith Margaret Kidman Urban (女儿) / Tom Cruise(前夫)"),
                Avatar = new StarAvatarVO("20240126162400.png"),
                DoubanID = new DoubanIDVO("1054442"),
                IMDbID = new IMDbIDVO("nm0000173"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("妮可·基德曼（Nicole Kidman），1967年6月20日出生于美国夏威夷，澳大利亚女演员、制片人。4岁时随家人迁往澳大利亚生活。她在16岁那年辍学，专心投入演艺事业。2003年因影片《时时刻刻》获得第75届奥斯卡影后。\r\n1983年，她第一次登上了银幕，在《丛林圣诞节》一片中出演一个配角，迈出了影视表演的第一步。1985年，她因出演了电视连续剧《越南》而荣获澳大利亚电影学院最佳女演员奖。凭借1989年惊悚片《飞越地平线》中的出色表演，她获得了去美国发展的机会。\r\n1989年，她有幸与巨星汤姆·克鲁斯合作《雷霆壮志》，之后她与克鲁斯热恋并结婚，使她一夜成名。又在1992年与当时的丈夫汤姆·克鲁斯联手出演了《大地雄心》。\r\n随后，在1995年的卖座巨片《永远的蝙蝠侠》中，基德曼饰演了一个美丽的心理医生，再次获得成功。这一年她被美国《人物》杂志评选为世界50位美人之一。同年，她又出演了《不惜一切》，凭借此片获得第53届美国电影电视金球奖电影类-音乐喜剧类最佳女主角奖、第1届美国广播影评人协会奖最佳女主角、收获了第49届英国电影学院奖最佳女主角的提名，但是没有被奥斯卡提名。\r\n1998年在英国全裸主演舞台剧《蓝房间》，轰动一时。1999年主演的惊悚悬疑电影《大开眼戒》上映。2001年2月5日，妮可·基德曼和汤姆·克鲁斯宣布离婚。2006年6月25日，妮可·基德曼和澳洲乡村歌手凯斯·厄本在澳大利亚悉尼举行婚礼。\r\n2001年12月14日，妮可·基德曼主演了歌舞片《红磨坊》，扮演巴黎红灯区的舞娘“莎婷”。她凭借这部影片收获第一个奥斯卡影后提名，也第二次获得金球奖音喜影后。同年，她主演的恐怖片《小岛惊魂》上映。2002年，她在《时时刻刻》一片中饰演英国作家弗吉尼亚·伍尔芙，这部影片让她收获了奥斯卡影后、金球奖影后以及柏林电影节影后在内的多个最佳女主角奖。\r\n而妮可·基德曼并没有停下自己追求伟大的进程。2017年，她被授予戛纳电影节70周年特别奖。同年，妮可基德曼凭借美剧《大小谎言》横扫当年颁奖季。包括第69届美国电视艾美奖限定剧最佳女演员、艾美奖最佳限定剧（制作人身份）、第24届演员工会奖限定剧最佳女演员、第75届美国电影电视金球奖迷你剧最佳女演员和第23届评论家选择奖限定剧最佳女主角。基德曼在银幕上充分证明了她不仅仅只是拥有美貌的女人，她的演技同样是一流的。\r\n2022年11月，美国电影学会（AFI）宣布，授予妮可·基德曼美国电影业最高荣誉之一——第49届美国电影学会终身成就奖，以表彰其对于影视行业的杰出贡献。妮可·基德曼是该奖项的第49位获奖者，也是第1位得奖的澳洲人。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime

           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("马丁·斯科塞斯"),
                Aka = new CelebrityAkaVO("Martin Scorsese"),
                NameEn = new CelebrityNameEnVO("马丁·斯科西斯 / 马田史高西斯 / 马丁史柯席斯 / 马丁·斯科塞西"),
                AkaEn = new CelebrityAkaEnVO("Martin Marcantonio Luciano Scorsese (本名) / Marty (昵称)"),
                Professions = new ProfessionsVO("制片人 / 演员 / 导演 / 编剧 / 配音"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,纽约,皇后区"),
                Family = new FamiliesVO("Keith Urban (丈夫) / Sunday Rose Kidman Urban (女儿) / Faith Margaret Kidman Urban (女儿) / Tom Cruise(前夫)"),
                Avatar = new StarAvatarVO("20240126163022.png"),
                DoubanID = new DoubanIDVO("1054425"),
                IMDbID = new IMDbIDVO("nm0000217"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("马丁·斯科西斯的双亲是意大利西西里岛人，移民至纽约后，在皇后区生下马丁。斯科塞斯从小在纽约的小意大利（Little Italy）区成长，影响到他未来的创作生涯。 斯科塞斯念布朗克斯区的 Cardinal Hays School 中学，然后在1960年进入知名的纽约大学念电影系。1966年斯科塞斯拿到电影硕士，在1968年到1970年之间斯科塞斯留在纽约大学电影系任教。在这段期间，斯科塞斯曾经在1969年担任传奇演唱会伍兹塔克（Woodstock）的纪录片助理导演。\r\n1974年弗朗西斯·科波拉介绍斯科塞斯进入华纳兄弟片厂，斯科塞斯得以执导他的第一部好莱坞剧情片《大篷車博莎》（Boxcar Bertha）。\r\n1976年斯科塞斯以《出租车司机》夺下当年戛纳电影节金棕榈奖，男演员罗伯特·德尼罗的一句台词“在跟我说话吗？你在跟我说话吗？你真的在跟我说话吗？”（You talkin' to me? You talkin' to me? You talkin' to me ?）成为知名经典台词。\r\n1980年斯科塞斯再找来男演员劳伯·狄尼洛增肥演出拳击名将 Jack La Motta的故事《愤怒的公牛》，为劳伯·狄尼洛拿下一座1981年美国奥斯卡最佳男主角。\r\n1988年斯科塞斯改编希腊作家尼可斯·卡山札基（Nikos Kazantzakis）的小说《基督的最后诱惑》成同名电影，影片引起轩然大波，全世界基督教团体扬言抵制。位在法国巴黎的圣米歇（Saint-Michel）戏院因上映《基督的最后诱惑》遭人割破银幕并纵火。\r\n1990年参演日本导演黑泽明的《梦》，饰演荷兰画家梵高。\r\n1997年美国电影学会颁AFI终身成就奖给斯科塞斯。\r\n1998年斯科塞斯担任戛纳电影节评审团主席。当年虽将金棕榈奖颁给希腊导演泰奥·安哲罗普洛斯的《永远的一天》，但最惊人的影像莫过于当评审团大奖颁给意大利导演罗伯托·贝尼尼的《美丽人生》时，罗伯托·贝尼尼先是在舞台上向斯科塞斯下跪，然后再抱起斯科塞斯转一圈的动人画面。\r\n2006年的影片《无间行者》乃翻拍自香港《无间道》系列之第一部，并令其得到第79届奥斯卡金像奖最佳导演奖。\r\n2010年1月18日，马丁·斯科塞斯荣获第67届美国金球奖终身成就奖。\r\n马丁·斯科西斯在进行剧情长片的同时，也拍了几部跟意大利移民、意大利新写实主义、音乐（爵士乐、蓝调）的纪录片。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("埃里克·罗思"),
                Aka = new CelebrityAkaVO("Eric Roth"),
                NameEn = new CelebrityNameEnVO("艾瑞克·罗斯"),
                AkaEn = new CelebrityAkaEnVO("Yukon Eric (昵称)"),
                Professions = new ProfessionsVO("编剧 / 演员 / 制片人"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,纽约"),
                Family = new FamiliesVO("Keith Urban (丈夫) / Sunday Rose Kidman Urban (女儿) / Faith Margaret Kidman Urban (女儿) / Tom Cruise(前夫)"),
                Avatar = new StarAvatarVO("20240126163301.png"),
                DoubanID = new DoubanIDVO("1000393"),
                IMDbID = new IMDbIDVO("nm0744839"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("编剧、制片人，多次入围奥斯卡最佳改编剧本奖，好莱坞历史上最伟大的编剧之一，曾多次获得奥斯卡最佳改编剧本奖提名。罗斯是加利福尼亚大学圣塔芭芭拉分校1996年届学生。1973年入学加州大学洛杉矶分校戏剧影视学院。他为人熟知的作品包括《阿甘正传》《惊爆内幕》《慕尼黑》《本杰明·巴顿奇事》《一个明星的诞生》《沙丘》《花月杀手》等。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("莱昂纳多·迪卡普里奥"),
                Aka = new CelebrityAkaVO("Leonardo DiCaprio"),
                NameEn = new CelebrityNameEnVO("李奥纳多·迪卡普里奥 / 里安纳度·迪卡比奥 / 小李(昵称) / 小李子(昵称)"),
                AkaEn = new CelebrityAkaEnVO("Leonardo Wilhelm DiCaprio (本名) / Lenny D (昵称) / Leo (昵称)"),
                Professions = new ProfessionsVO("演员 / 制片人 / 配音 / 编剧"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,加利福尼亚,洛杉矶"),
                Family = new FamiliesVO("Gisele Bündchen(前女友) / Bar Refaeli(前女友) / Erin Heatherton(前女友) / Kelly Rohrbach(前女友) / Gigi Hadid(前女友) / Vittoria Ceretti(女友)"),
                Avatar = new StarAvatarVO("20240126163553.png"),
                DoubanID = new DoubanIDVO("1000393"),
                IMDbID = new IMDbIDVO("nm0000138"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("莱昂纳多•迪卡普里奥（Leonardo DiCaprio），1974年11月11日出生于美国加利福尼亚州洛杉矶，好莱坞电影男演员。 1974年，一位叫艾莫琳的德国孕妇站在莱昂纳多•达•芬奇的画展上久久不能自己，当她沉浸在达芬奇的艺术魅力之中时，腹中的宝贝突然有了类似的悸动，她即刻领会了这股神奇力量，于是将后来诞下的这名男婴取名为莱昂纳多•威尔海姆•迪卡普里奥，仿佛她已经知道这孩子将来注定要从事艺术工作。 莱昂纳多的母亲艾莫琳是个有俄罗斯血统的德国人，他的父亲是出生于纽约的德国和意大利混血儿。莱昂纳多一出生，他的父母便已离异。他一直和母亲住在一起，周末则和父亲一起度过。即使离异后，莱昂纳多的父亲和母亲也一如既往地保持着亲密状态。 5岁时，莱昂纳多迎来他的电视银幕处女秀在教育片《游戏屋》中扮演一个角色。14岁时，他出现在他的第一部商业广告之中，那是为火柴盒汽车所做的广告。此后，他成了包括燕麦片、玩具乃至泡泡糖在内的所有儿童商品都做了广告代言人。15岁时，他参加了日间肥皂剧《圣芭芭拉》的演出，扮演一个酗酒少年。之后，他又在剧集《父母之道》中他再次挑战“问题少年”角色。 1990年，16岁的他参加了著名美剧《成长的烦恼第七季》的拍摄，在片中扮演一个无家可归的少年。同年，科幻片《魔精3》成为他的第一部电影。 1992年，主演了首部剧情长片《男孩的生活》，和罗伯特•德尼罗搭档，在片中饰演一位受继父虐待、内心无比压抑、痛苦的少年形象。同年，他还出演了《不一样的天空》中的弱智儿亚尼。他纯朴自然的表演，为初涉影坛的他赢得了第66届奥斯卡金像奖提名和第51届美国电影金球奖电影剧情类最佳男配角提名，而那时他才19岁。 1994年，莱昂纳多与莎朗•斯通等合作出演西部片《致命的快感》。同年，他又在《篮球日记》中塑造了一个个性鲜明的吸毒作家形象。之后，拍摄了电影《全蚀狂爱》，饰演同性恋诗人兰波。 1995年，在影片《马文的房间》中，他饰演了一个烧毁自家房子的叛逆小子，和黛安•基顿和梅丽尔•斯特里普搭戏。同年，在巴兹•鲁赫曼指导的现代激情版《罗密欧与朱丽叶》中，他塑造了一个崭新的“罗密欧”的形象。他的表演被评论界称为具有“毁灭性”，融狂躁不安、反叛精神和多愁伤感于一体，莱昂纳多因此片荣膺第47届柏林国际电影节最佳男主角奖。 1996年，主演詹姆斯•卡梅隆执导的史诗巨作《泰坦尼克号》。该片获11项奥斯卡大奖，全球狂揽18亿美元票房，打破了美国和世界各地的票房记录。莱昂纳多因在影片中的完美表演而成了“世纪末的票房炸弹”，像旋风般席卷了全球，年轻英俊、充满朝气。他凭此片获得第55届美国电影金球奖电影剧情类最佳男主角提名。 1997年，在电影《铁面人》中扮演路易十四以及他的孪生弟弟菲利普双重角色。 1998年，在伍迪•艾伦导演的《名人百态》里客串了一个角色。 1999年，莱昂纳多在其新片《海滩》中饰演一个酷爱畅游网络世界寻找刺激的美国年轻人，并决定到泰国寻找在网路上得知的人间天堂----海滩。 2000年，首次与马丁•西科塞斯导演合作影片《纽约黑帮》。 2002年，莱昂纳多出演了斯蒂文•斯皮尔伯格导演的《逍遥法外》，票房极好，他的表现广获好评，更提名第60届金球奖电影剧情类最佳男主角。 2003年，与马丁•西科塞斯合作电影《飞行家》，为莱昂迎来第一个金球影帝（第62届金球奖电影剧情类最佳男主角）和第77届奥斯卡金像奖最佳男主角提名。 2005年，拍摄电影《无间行者》，与马丁•西科塞斯第三次合作。拍摄电影《血钻》。莱昂纳多凭借这两部影片获得第64届金球奖电影剧情类最佳男主角双提名，他也是金球历史上首个获得最佳男主角双提名的演员，并以《血钻》获得第79届奥斯卡金像奖最佳男主角提名。 2007年，先后主演了电影《革命之路》和《谎言之躯》。在《革命之路》中，他和《泰坦尼克号》里的拍档凯特•温斯莱特再度携手，提名第66届金球奖电影剧情类最佳男主角。同年上映的环境记录片《第11小时》是一部莱昂独立制作的电影，除了导演是另请的之外几乎全程包办。 2008年，加盟的影片《禁闭岛》是莱昂和马丁•西科塞斯的第四次合作。 2009年，参演克里斯托弗•诺兰执导的《盗梦空间》，本片带观众游走于梦境与现实之间，被定义为''发生在意识结构内的当代动作科幻片”。 2010年，莱昂纳多担任旁白的纪录片《哈勃望远镜3D》上映。 2011年，拍摄传记片《胡佛》，由克林特•伊斯特伍德执导，获第69届金球奖电影剧情类最佳男主角提名。主演电影《了不起的盖茨比》，与导演巴兹•鲁赫曼第二次合作。同年，福布斯发布好莱坞最具票房号召力明星排名，莱昂纳多凭借7000万美元的收入荣登榜首 。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("罗伯特·德尼罗"),
                Aka = new CelebrityAkaVO("Robert De Niro"),
                NameEn = new CelebrityNameEnVO("罗拔·迪尼路 / 劳勃·狄尼洛)"),
                AkaEn = new CelebrityAkaEnVO("Robert Mario De Niro Jr.(本名) / Bobby D(昵称) / Bobby Milk / Robert Mitchum / Bob(昵称)"),
                Professions = new ProfessionsVO("演员 / 制片人 / 配音 / 编剧 / 导演"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,纽约"),
                Family = new FamiliesVO("Grace Hightower（妻） / Diahnne Abbott（前妻） / Drena De Niro（女）"),
                Avatar = new StarAvatarVO("20240126163833.png"),
                DoubanID = new DoubanIDVO("1054445"),
                IMDbID = new IMDbIDVO("nm0000134"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("　　奥斯卡影帝罗伯特·德尼罗塑造过的所有角色中，最著名的当属《教父2》中年轻的唐·科莱昂和《愤怒的公牛》中的拳击手杰克·拉莫塔，他最经典的一句台词则是《出租汽车司机》中“你在跟我讲话？”凭借马丁·斯科塞斯1973年影片《穷街陋巷》一举成名的德尼罗，既可以扮演心狠手辣的黑帮分子，又能在《午夜狂奔》、《摇尾狗》、《老大靠边闪》和《拜见岳父岳母》等喜剧中尽现搞笑天份。\r\n　　1943年8月，罗伯特·德尼罗出生在纽约一个艺术家家庭，两岁时父母离异，他跟着母亲生活在格林威治村。16岁高中毕业后他跟着路瑟·詹姆斯（Luther James）和李·斯特拉斯伯格（Lee Strasberg）学习表演，参演了几部外百老汇剧，1965年在法国影片《Trois chambres à Manhattan》中饰演一个小角色，60年代后期他在《帅气逃兵》、《嗨，妈妈》、《婚礼舞会》等影片中担任主演。进入70年代，德尼罗的事业开始绽放异彩，出演了《血腥妈妈》、《我的子弹会转弯》和《天生大赢家》等影片后，与马丁·斯科塞斯首度合作的《穷街陋巷》为他赢得了纽约影评人协会奖和美国国家影评人协会奖，此后他又凭借《战鼓轻悄》获得奥斯卡提名，1974年的《教父2》让他如愿捧得奥斯卡影帝桂冠，两年后的《出租汽车司机》更是好评如潮，获得多项最佳男演员奖。此后他的主要作品有《愤怒的公牛》、《不可触犯》、《午夜狂奔》、《我们不是天使》、《盗亦有道》、《疯狗马子》等。1993年他首执导棒，自导自演了《布鲁克斯故事》。2003年罗伯特·德尼罗被美国电影学会授予终生成就奖，以表彰他对电影事业的杰出贡献。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("莉莉·格莱斯顿"),
                Aka = new CelebrityAkaVO("Lily Gladstone"),
                NameEn = new CelebrityNameEnVO("莉莉·格莱德斯通"),
                AkaEn = new CelebrityAkaEnVO(""),
                Professions = new ProfessionsVO("演员 / 编剧"),
                Gender = GenderVO.Codes.GenderCode2,
                BornPlace = new BornPlaceVO("美国,蒙大纳,卡利斯佩尔"),
                Family = new FamiliesVO(""),
                Avatar = new StarAvatarVO("20240126164111.png"),
                DoubanID = new DoubanIDVO("1354255"),
                IMDbID = new IMDbIDVO("nm4291409"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("Lily Gladstone was raised on the Blackfeet Reservation in Northwestern Montana.\r\nOf mixed heritage, Lily’s tribal affiliations include Kainai, Amskapi Piikani and Nimi'ipuu First Nations. In 2008, she graduated with high honors from the University of Montana with a BFA in Acting, and a minor in Native American studies. Film credits include Kelly Reichardt's Certain Women, Alex and Andrew Smith’s Winter in the Blood, Arnaud Desplechin’s Jimmy P, and Sarah Adina Smith’s Buster’s Mal Heart. She has thrice toured nationally with The Montana Repertory Theater.\r\nIn addition to garnering multiple film and theater credits, she has facilitated countless expressive arts workshops with various social justice and human rights advocacy groups - including National Indigenous Women’s Resource Center, Living Voices, Red Eagle Soaring, Longhouse Media, Yellow Bird Inc., The Roxy Film Academy and Conscious Alliance. Her emphasis is youth outreach and education."),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("保罗·金"),
                Aka = new CelebrityAkaVO("Paul King"),
                NameEn = new CelebrityNameEnVO(""),
                AkaEn = new CelebrityAkaEnVO("Paul Thomas King"),
                Professions = new ProfessionsVO("导演 / 编剧 / 演员 / 制片人 / 副导演"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("英国"),
                Family = new FamiliesVO(""),
                Avatar = new StarAvatarVO("20240126165342.png"),
                DoubanID = new DoubanIDVO("1313689"),
                IMDbID = new IMDbIDVO("nm1653753"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("Paul King is a writer and director. He works in television, film and theatre, and specialises in comedy.\r\n\r\nHe graduated from St Catharine's College, Cambridge University with first-class honors in English in 1999. There he met Richard Ayoade, Matthew Holness and Alice Lowe, and went on to direct them at the Edinburgh Festival in \"Garth Marenghi's FrightKnight\" (nominated for the Perrier Award in 2000), and \"Netherhead\" (Perrier Award winner 2001). King worked as Associate Director on the subsequent TV transfer, \"Garth Marenghi's Darkplace\", a six-part series for Channel 4. In 2002, King garnered another Perrier Award nomination for directing Noel Fielding's Edinburgh Festival show, \"Voodoo Hedgehog\"."),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("西蒙·法纳比"),
                Aka = new CelebrityAkaVO("Simon Farnaby"),
                NameEn = new CelebrityNameEnVO(""),
                AkaEn = new CelebrityAkaEnVO(""),
                Professions = new ProfessionsVO("演员 / 编剧 / 配音"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("英国,杜伦,达林顿"),
                Family = new FamiliesVO(""),
                Avatar = new StarAvatarVO("20240126165527.png"),
                DoubanID = new DoubanIDVO("1327901"),
                IMDbID = new IMDbIDVO("nm1375030"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("http://www.curtisbrown.co.uk/simon-farnaby/\r\n\r\n\r\nEarly Life\r\nBorn in Wath-upon-Dearne, South Yorkshire, Farnaby trained at the Webber Douglas Academy of Dramatic Art, National Youth Theatre, and Trinity College, Dublin. He was a finalist for the Carlton Hobbes Radio Award and co-devised The Blue Diamond of Azcabar for The Changeling Theatre Company. Simon has played many roles like various characters in Horrible Histories and 'James Blond' in M.I. High. He is also known as 'Farno'."),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("提莫西·查拉梅"),
                Aka = new CelebrityAkaVO("Timothée Chalamet"),
                NameEn = new CelebrityNameEnVO("蒂莫西·查拉梅 / 蒂莫西·夏拉梅 / 提莫西·查拉梅 / 堤摩西·柴勒梅德 / 蒂莫西·沙勒梅 / 甜茶(昵称) / 莎乐美(昵称) / 小美(昵称)"),
                AkaEn = new CelebrityAkaEnVO("Timo"),
                Professions = new ProfessionsVO("演员 / 配音 / 导演 / 制片人"),
                Gender = GenderVO.Codes.GenderCode1,
                BornPlace = new BornPlaceVO("美国,纽约州,纽约"),
                Family = new FamiliesVO("Marc Chalamet (父) / Nicole Flender (母) / Pauline Chalamet (姐) / Lourdes Leon (前女友) / Lily-Rose Depp (前女友) / Eiza González(前女友) / Kylie Jenner(女友)"),
                Avatar = new StarAvatarVO("20240126165846.png"),
                DoubanID = new DoubanIDVO("1325862"),
                IMDbID = new IMDbIDVO("nm3154303"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("蒂莫西·柴勒梅德出生于美国纽约，他的父亲是法国人，母亲是美国人（一半俄罗斯犹太血统，一半奥地利犹太血统），叔叔Rodman Flender是一位电视剧导演，阿姨Amy Lippman是一位编剧，曾参与执笔《性爱大师第一季》，外祖父Harold Flender也是一位编剧。\r\n 　　蒂莫西·柴勒梅德曾在哥伦比亚大学就读，为了追求自己的演艺生涯转学去了纽约大学。由于出演了美剧《国土安全》第二季中副总统的儿子Finn Walden而为人熟悉，之后又参演过贾森·雷特曼的《男人女人和孩子》，以及诺兰的《星际穿越》。2017年的《请以你的名字呼唤我》中的出色表演让他成为颁奖季最佳男主角的大热门。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },

           new()
           {
                RequestId = uuid,
                CelebrityId = new CelebrityIdVO(Guid.NewGuid()),
                Name = new CelebrityNameVO("奥利维娅·科尔曼"),
                Aka = new CelebrityAkaVO("Olivia Colman"),
                NameEn = new CelebrityNameEnVO("奥莉薇娅·柯尔曼"),
                AkaEn = new CelebrityAkaEnVO("Sarah Caroline Olivia Colman(本名) / Collie (昵称)"),
                Professions = new ProfessionsVO("演员 / 配音 / 制片人 / 编剧"),
                Gender = GenderVO.Codes.GenderCode2,
                BornPlace = new BornPlaceVO("英国,英格兰,诺维奇"),
                Family = new FamiliesVO("Ed Sinclair(夫)"),
                Avatar = new StarAvatarVO("20240126170109.png"),
                DoubanID = new DoubanIDVO("1004900"),
                IMDbID = new IMDbIDVO("nm1469236"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode0,
                Summary = new SummaryVO("莎拉·卡萝琳·奥利维娅·科尔曼，CBE是一名英国女演员。知名作品包括《王冠》、《被告人》、《宠儿》、《二零一二》、《伦敦生活》 、《夜班经理》和《小镇疑云》。凭她在电视剧的演出，她斩获三座英国电视学院奖和一座金球奖。 科尔曼亦出演过的多部电影，如《沉睡的暴龙》、《铁娘子》、《单身动物园》。"),
                UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                CreatedOn = dateTime
           },
       };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<UserAccountEntity> GetUserAccounts(RequestIdVO uuid, CreatedOnVO dateTime) =>
       new List<UserAccountEntity>
       {
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("tonyzhangshi"), PasswordHash = new(new PasswordHashVO("Tony19811031").ToHash("tonyzhangshi")), EmailAddress = new("tonyzhangshi@163.com"), Avatar = new("0ACFC82E7D5A41FC8AB8FD4EF603C858Tony.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), LastLoginIp = new("201.182.1.23"), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test01"), PasswordHash = new(new PasswordHashVO("111111").ToHash("test01")), EmailAddress = new("test01@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test02"), PasswordHash = new(new PasswordHashVO("222222").ToHash("test02")), EmailAddress = new("test02@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(true), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test03"), PasswordHash = new(new PasswordHashVO("333333").ToHash("test03")), EmailAddress = new("test03@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test04"), PasswordHash = new(new PasswordHashVO("444444").ToHash("test04")), EmailAddress = new("test04@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test05"), PasswordHash = new(new PasswordHashVO("555555").ToHash("test05")), EmailAddress = new("test05@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test06"), PasswordHash = new(new PasswordHashVO("666666").ToHash("test06")), EmailAddress = new("test06@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test07"), PasswordHash = new(new PasswordHashVO("777777").ToHash("test07")), EmailAddress = new("test07@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
            new(){ RequestId = uuid, UserId = new(Guid.NewGuid()), Account = new("test08"), PasswordHash = new(new PasswordHashVO("888888").ToHash("test08")), EmailAddress = new("test08@163.com"), Avatar = new("User_1.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), CreatedOn = dateTime },
       };

    private static string GetWritersId(FilmHouseDbContext dbContext, string names)
    {
        if (string.IsNullOrEmpty(names))
        {
            return string.Empty;
        }

        var ids = new StringBuilder();
        var items = names.Split('/');
        foreach (var item in items)
        {
            var id = dbContext.Celebrities.AsNoTracking().Where(d => d.Name == new CelebrityNameVO(item.Trim())).FirstOrDefault();
            if (id != null)
            {
                ids.Append($"{id.CelebrityId} / ");
            }
        }
        ids.Length -= 3;
        return ids.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<MovieEntity> GetMovies(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext)
    {
        var list = new List<MovieEntity>()
        {
            new MovieEntity() {
                RequestId = uuid,
                MovieId = new MovieIdVO(Guid.NewGuid()),
                Title = new MovieTitleVO("剪刀手安德华"),
                TitleEn = new MovieTitleEnVO("Edward Scissorhands"),
                Aka = new MovieAkaVO("幻海奇缘(港)"),
                Directors = new DirectorNamesVO("蒂姆·波顿"),
                Writers = new WritersNamesVO("蒂姆·波顿 / 卡罗琳·汤普森"),
                Casts = new CastsNamesVO("约翰尼·德普 / 薇诺娜·瑞德 / 黛安·韦斯特"),
                DirectorsId = new DirectorsIdVO(GetWritersId(dbContext, "蒂姆·波顿")),
                WritersId = new WritersIdVO(GetWritersId(dbContext, "蒂姆·波顿 / 卡罗琳·汤普森")),
                CastsId = new CastsIdVO(GetWritersId(dbContext, "约翰尼·德普 / 薇诺娜·瑞德 / 黛安·韦斯特")),
                Year = new YearVO("1990"),
                Pubdates = new PubdatesVO("2023-10-21(中国大陆) / 2023-05-20(戛纳电影节) / 2023-10-11(平遥国际电影展)"),
                Durations = new DurationsVO("105分钟"),
                Genres = new GenresVO("001/002/003"),
                Languages = new LanguagesVO("001"),
                Countries = new CountriesVO("001"),
                Rating = new RatingVO(8.6m),
                RatingCount = new RatingCountVO(234409),
                DoubanID = new DoubanIDVO("1292370"),
                IMDb = new IMDbIDVO("tt0099487"),
                Summary = new SummaryVO("　　爱德华（约翰尼•戴普 饰）是一个机器人，他拥有人的心智，却有一双剪刀手，孤独地生活在古堡里，闯入古堡的化妆品推销员佩格把他带回家，让他走进了人类的世界。单纯的爱德华爱上了佩格的女儿金（薇诺娜•瑞德 饰），金也慢慢的被爱德华的善良所吸引。\r\n　　但是，一连串的意外事情让周围的人邻居对爱德华的态度从喜欢变成无法接受，爱德华痛苦地发现，他总是好心办坏事，连自己的爱人都不能拥抱，或许，他注定就不属于这个世界。"),
                Avatar = new MovieAvatarVO("p480956937.jpg"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode2,
                PageViews = new PageViewsVO(98),
                UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).First().UserId,
                CreatedOn = dateTime
            },

            new MovieEntity() {
                RequestId = uuid,
                MovieId = new MovieIdVO(Guid.NewGuid()),
                Title = new MovieTitleVO("黑天鹅"),
                TitleEn = new MovieTitleEnVO("Black Swan"),
                Aka = new MovieAkaVO("夺命黑天鹅 / 霸王别鹅(豆友译名)"),
                Directors = new DirectorNamesVO("达伦·阿伦诺夫斯基"),
                Writers = new WritersNamesVO("安德雷斯·海因斯 / 马克·海曼 / 约翰·J·麦克劳克林"),
                Casts = new CastsNamesVO("娜塔莉·波特曼 / 米拉·库尼斯 / 文森特·卡塞尔 / 芭芭拉·赫希 / 薇诺娜·瑞德 / 本杰明·米派德 / 克塞尼亚·索罗 / 克里斯汀娜·安娜波 / 詹妮特·蒙哥马利 / 塞巴斯蒂安·斯坦 / 托比·海明威 / 塞尔吉奥·托拉多 / 马克·马戈利斯 / 蒂娜·斯隆 / 亚伯拉罕·阿罗诺夫斯基 / 夏洛特·阿罗诺夫斯基 / 玛西娅·让·库尔茨 / 肖恩·奥哈根 / 克里斯托弗·加廷 / 黛博拉·奥夫纳 / 斯坦利·B·赫尔曼 / 库尔特·弗勒曼 / 帕特里克·赫辛格 / 莎拉·海伊"),
                DirectorsId = new DirectorsIdVO(GetWritersId(dbContext, "达伦·阿伦诺夫斯基")),
                WritersId = new WritersIdVO(GetWritersId(dbContext, "安德雷斯·海因斯 / 马克·海曼 / 约翰·J·麦克劳克林")),
                CastsId = new CastsIdVO(GetWritersId(dbContext, "娜塔莉·波特曼 / 米拉·库尼斯 / 文森特·卡塞尔 / 芭芭拉·赫希 / 薇诺娜·瑞德 / 本杰明·米派德 / 克塞尼亚·索罗 / 克里斯汀娜·安娜波 / 詹妮特·蒙哥马利 / 塞巴斯蒂安·斯坦 / 托比·海明威 / 塞尔吉奥·托拉多 / 马克·马戈利斯 / 蒂娜·斯隆 / 亚伯拉罕·阿罗诺夫斯基 / 夏洛特·阿罗诺夫斯基 / 玛西娅·让·库尔茨 / 肖恩·奥哈根 / 克里斯托弗·加廷 / 黛博拉·奥夫纳 / 斯坦利·B·赫尔曼 / 库尔特·弗勒曼 / 帕特里克·赫辛格 / 莎拉·海伊")),
                Year = new YearVO("2010"),
                Pubdates = new PubdatesVO("2010-09-01(威尼斯电影节) / 2010-12-17(美国)"),
                Durations = new DurationsVO("108分钟"),
                Genres = new GenresVO("001/012"),
                Languages = new LanguagesVO("001/002/003"),
                Countries = new CountriesVO("001"),
                Rating = new RatingVO(8.6m),
                RatingCount = new RatingCountVO(3456),
                DoubanID = new DoubanIDVO("1978709"),
                IMDb = new IMDbIDVO("tt0947798"),
                Summary = new SummaryVO("　　纽约剧团要重排《天鹅湖》，因前领舞Beth（薇诺娜•赖德 Winona Ryder 饰）离去，总监Thomas（文森特•卡索尔 Vincent Cassel. 饰）决定海选新领舞，且要求领舞要分饰黑天鹅与白天鹅。Nina（娜塔莉•波特曼Natalie Portman 饰）自幼练习芭蕾舞，在母亲的细心关照下，技艺出众。这次，她希望可以脱颖而出。然而，在竞争中，她发现心机颇重的Lily（米拉•库妮丝 Mila Kunis 饰）是自己的强劲对手。在选拔中，她的白天鹅表演的无可挑剔，但是黑天鹅不及Lily。她感到身心俱疲，回家还发现了背部的红斑与脚伤。她一个人找到总监，希望争取一下。总监趁机亲吻她，却被她强硬拒绝。结果，总监居然选了她。队友怀疑她靠色相上位。在酒会上，Beth甚至当众发泄。这种压力外加伤病，一直影响着她的发挥。总监启发她要释放激情，表现出黑天鹅的诱惑。在强大的心理暗示中，她似乎也滑向了黑天鹅的角色……"),
                Avatar = new MovieAvatarVO("p719282906.jpg"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode2,
                PageViews = new PageViewsVO(105),
                UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).First().UserId,
                CreatedOn = dateTime },

            new MovieEntity() {
                RequestId = uuid,
                MovieId = new MovieIdVO(Guid.NewGuid()),
                Title = new MovieTitleVO("雷神4：爱与雷霆"),
                TitleEn = new MovieTitleEnVO("Thor: Love and Thunder"),
                Aka = new MovieAkaVO("雷神4 / 雷神奇侠4：爱与雷霆(港) / 雷神索尔：爱与雷霆(台)"),
                Directors = new DirectorNamesVO("塔伊加·维迪提"),
                Writers = new WritersNamesVO("塔伊加·维迪提 / 詹尼弗·凯廷·罗宾逊 / 斯坦·李 / 杰森·亚伦"),
                Casts = new CastsNamesVO("克里斯·海姆斯沃斯 / 娜塔莉·波特曼 / 克里斯蒂安·贝尔 / 泰莎·汤普森 / 塔伊加·维迪提 / 罗素·克劳 / 杰米·亚历山大 / 伊德里斯·艾尔巴 / 克里斯·帕拉特 / 戴夫·巴蒂斯塔 / 凯伦·吉兰 / 庞·克莱门捷夫 / 肖恩·古恩 / 范·迪塞尔 / 布莱德利·库珀 / 卡尔利·里斯 / 凯特·戴琳斯 / 博特·高登斯汀 / 史蒂芬·库里 / 斯特兰·斯卡斯加德 / 鲍比·霍兰德·汉顿 / 卢克·海姆斯沃斯 / 马特·达蒙 / 戴利·皮尔森 / 山姆·尼尔 / 梅丽莎·麦卡西 / 基隆·L·戴尔 / 尹迪亚·萝丝·海姆斯沃斯 / 特里斯坦·海姆斯沃斯 / 萨莎·海姆斯沃斯 / 埃尔莎·帕塔奇 / 齐亚·凯利 / 罗桑杰拉·法萨诺 / 本·法尔科内 / 西蒙·拉塞尔·比尔 / 乔尼·布鲁 / 布鲁克·斯塔奇威尔 / 埃莉扎·德苏扎 / 卡恩·古尔杜尔 / 普里西拉·道伊西 / 斯蒂芬·亨特 / 印第安娜·伊万斯 / 艾娃·卡约菲丽斯 / 本·辛克莱尔"),
                DirectorsId = new DirectorsIdVO(GetWritersId(dbContext, "塔伊加·维迪提")),
                WritersId = new WritersIdVO(GetWritersId(dbContext, "塔伊加·维迪提 / 詹尼弗·凯廷·罗宾逊 / 斯坦·李 / 杰森·亚伦")),
                CastsId = new CastsIdVO(GetWritersId(dbContext, "克里斯·海姆斯沃斯 / 娜塔莉·波特曼 / 克里斯蒂安·贝尔 / 泰莎·汤普森 / 塔伊加·维迪提 / 罗素·克劳 / 杰米·亚历山大 / 伊德里斯·艾尔巴 / 克里斯·帕拉特 / 戴夫·巴蒂斯塔 / 凯伦·吉兰 / 庞·克莱门捷夫 / 肖恩·古恩 / 范·迪塞尔 / 布莱德利·库珀 / 卡尔利·里斯 / 凯特·戴琳斯 / 博特·高登斯汀 / 史蒂芬·库里 / 斯特兰·斯卡斯加德 / 鲍比·霍兰德·汉顿 / 卢克·海姆斯沃斯 / 马特·达蒙 / 戴利·皮尔森 / 山姆·尼尔 / 梅丽莎·麦卡西 / 基隆·L·戴尔 / 尹迪亚·萝丝·海姆斯沃斯 / 特里斯坦·海姆斯沃斯 / 萨莎·海姆斯沃斯 / 埃尔莎·帕塔奇 / 齐亚·凯利 / 罗桑杰拉·法萨诺 / 本·法尔科内 / 西蒙·拉塞尔·比尔 / 乔尼·布鲁 / 布鲁克·斯塔奇威尔 / 埃莉扎·德苏扎 / 卡恩·古尔杜尔 / 普里西拉·道伊西 / 斯蒂芬·亨特 / 印第安娜·伊万斯 / 艾娃·卡约菲丽斯 / 本·辛克莱尔")),
                Year = new YearVO("2022"),
                Pubdates = new PubdatesVO("2022-07-06(中国香港) / 2022-07-08(美国) / 2022-09-08(美国网络)"),
                Durations = new DurationsVO("119分钟"),
                Genres = new GenresVO("002/013"),
                Languages = new LanguagesVO("001"),
                Countries = new CountriesVO("001/002"),
                Rating = new RatingVO(5.2m),
                RatingCount = new RatingCountVO(143589),
                DoubanID = new DoubanIDVO("34477861"),
                IMDb = new IMDbIDVO("tt10648342"),
                Summary = new SummaryVO("　　曾经的信仰崩塌，内心笃定的爱慕与崇拜转而化作足以吞噬一切的无边黑暗。虔诚的格尔（克里斯蒂安·贝尔 Christian Bale 饰）化身屠神者，将杀戮之手伸向了宇宙中每一个至高无上的神祇。雷神索尔（克里斯·海姆斯沃斯 Chris Hemsworth 饰）的平静生活终于被格尔所扰动，他和女武神（泰莎·汤普森 Tessa Thompson 饰）联手与新的对手展开对抗。而在此过程中，索尔重逢八年未见的前女友简·福斯特（娜塔莉·波特曼 Natalie Portman 饰）。此时的她身患绝症，不久于人世，却借助雷神之锤的力量恢复健康，甚至还成为了实力强悍的女雷神。\r\n　　获得与失去，爱慕与憎恶，信仰与虚无，矛盾的情愫在宇宙的角落中萌生……"),
                Avatar = new MovieAvatarVO("20231022161924.png"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode2,
                PageViews = new PageViewsVO(123),
                UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test01")).First().UserId,
                CreatedOn = dateTime },

            new MovieEntity()
            {
                RequestId = uuid,
                MovieId = new MovieIdVO(Guid.NewGuid()),
                Title = new MovieTitleVO("海王2：失落的王国"),
                TitleEn = new MovieTitleEnVO("Aquaman and the Lost Kingdom"),
                Aka = new MovieAkaVO("海王与失落的王国 / 水行侠 失落王国(台) / 水行侠与失落王国(港) / 潜水侠2 / 水人2 / Aquaman 2"),
                Directors = new DirectorNamesVO("温子仁"),
                Writers = new WritersNamesVO("大卫·莱斯利·约翰逊-麦戈德里克"),
                Casts = new CastsNamesVO("杰森·莫玛 / 帕特里克·威尔森 / 叶海亚·阿卜杜勒-迈丁 / 安珀·赫德 / 妮可·基德曼"),
                DirectorsId = new DirectorsIdVO(GetWritersId(dbContext, "温子仁")),
                WritersId = new WritersIdVO(GetWritersId(dbContext, "大卫·莱斯利·约翰逊-麦戈德里克")),
                CastsId = new CastsIdVO(GetWritersId(dbContext, "杰森·莫玛 / 帕特里克·威尔森 / 叶海亚·阿卜杜勒-迈丁 / 安珀·赫德 / 妮可·基德曼")),
                Year = new YearVO("2023"),
                Pubdates = new PubdatesVO("2023-12-20(中国大陆) / 2023-12-22(美国)"),
                Durations = new DurationsVO("124分钟"),
                Genres = new GenresVO("001/002/003"),
                Languages = new LanguagesVO("001"),
                Countries = new CountriesVO("001"),
                Rating = new RatingVO(6.5m),
                RatingCount = new RatingCountVO(122156),
                DoubanID = new DoubanIDVO("30444942"),
                IMDb = new IMDbIDVO("tt9663764"),
                Summary = new SummaryVO("　　在上一次试图击败海王（杰森·莫玛 Jason Momoa 饰）未果后，黑蝠鲼（叶海亚·阿卜杜勒-迈丁 Yahya Abdul-Mateen II 饰）依然不甘放弃为父报仇，誓要消灭海王。这一次，他找到了传说中的黑暗三叉戟，释放出古老的邪恶力量，比以往更来势汹汹。为了与之抗衡，海王向被囚禁狱中的弟弟奥姆（也是前亚特兰蒂斯国王，帕特里克·威尔森 Patrick Wilson 饰）求助，组成了出乎意料的联盟。他俩必须抛弃前仇旧怨，携手并肩作战，才能从即将到来的灾难中保卫王国，拯救家人，拯救世界。"),
                Avatar = new MovieAvatarVO("20240126155159.png"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode1,
                PageViews = new PageViewsVO(98),
                UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).First().UserId,
                CreatedOn = dateTime
            },

            new MovieEntity()
            {
                RequestId = uuid,
                MovieId = new MovieIdVO(Guid.NewGuid()),
                Title = new MovieTitleVO("花月杀手"),
                TitleEn = new MovieTitleEnVO("Killers of the Flower Moon"),
                Aka = new MovieAkaVO("花开之月的杀手"),
                Directors = new DirectorNamesVO("马丁·斯科塞斯"),
                Writers = new WritersNamesVO("埃里克·罗思 / 马丁·斯科塞斯"),
                Casts = new CastsNamesVO("莱昂纳多·迪卡普里奥 / 罗伯特·德尼罗 / 莉莉·格莱斯顿 / 杰西·普莱蒙 / 坦图·卡丁诺"),
                DirectorsId = new DirectorsIdVO(GetWritersId(dbContext, "马丁·斯科塞斯")),
                WritersId = new WritersIdVO(GetWritersId(dbContext, "埃里克·罗思 / 马丁·斯科塞斯")),
                CastsId = new CastsIdVO(GetWritersId(dbContext, "莱昂纳多·迪卡普里奥 / 罗伯特·德尼罗 / 莉莉·格莱斯顿 / 杰西·普莱蒙 / 坦图·卡丁诺")),
                Year = new YearVO("2023"),
                Pubdates = new PubdatesVO("2023-05-20(戛纳国际电影节) / 2023-10-20(美国)"),
                Durations = new DurationsVO("206分钟"),
                Genres = new GenresVO("001/002/009/010/011/012"),
                Languages = new LanguagesVO("001"),
                Countries = new CountriesVO("001/002/004/005"),
                Rating = new RatingVO(7.3m),
                RatingCount = new RatingCountVO(52164),
                DoubanID = new DoubanIDVO("26745332"),
                IMDb = new IMDbIDVO("tt5537002"),
                Summary = new SummaryVO("　　印第安人的一支欧塞奇族，从祖先的土地上被驱逐到了美国俄克拉何马州一块贫瘠之地，谁曾想这里居然发现了石油。欧塞奇人借此一跃成为时尚新贵的同时，也给自身带来了杀机。在此之后，许多白人涌入这片流淌着黑金的土地，不择手段攫取利益。一战退伍兵欧内斯特（莱昂纳多·迪卡普里奥 Leonardo DiCaprio 饰）为了生计辗转来此，投奔已经贵为名流舅舅威廉·黑尔（罗伯特·德尼罗 Robert De Niro 饰）。威廉和原住民关系密切，黑白通吃，然而和善的外表下包藏着贪婪的蛇蝎之心。他暗示欧内斯特追求富有的印第安女子莫莉（莉莉·格莱斯顿 Lily Gladstone 饰），而真正觊觎的则是她们家族所拥有的财富。短短数年间，数十名印第安人意外身亡。君子无罪，怀璧其罪……\r\n　　本片根据大卫·格兰的畅销著作改编。"),
                Avatar = new MovieAvatarVO("20240126165024.png"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode1,
                PageViews = new PageViewsVO(63),
                UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).First().UserId,
                CreatedOn = dateTime
            },

            new MovieEntity()
            {
                RequestId = uuid,
                MovieId = new MovieIdVO(Guid.NewGuid()),
                Title = new MovieTitleVO("旺卡"),
                TitleEn = new MovieTitleEnVO("Wonka"),
                Aka = new MovieAkaVO("查理和巧克力工厂前传"),
                Directors = new DirectorNamesVO("保罗·金"),
                Writers = new WritersNamesVO("西蒙·法纳比 / 保罗·金"),
                Casts = new CastsNamesVO("提莫西·查拉梅 / 奥利维娅·科尔曼 / 休·格兰特 / 卡拉·莱恩 / 科甘-迈克尔·凯"),
                DirectorsId = new DirectorsIdVO(GetWritersId(dbContext, "保罗·金")),
                WritersId = new WritersIdVO(GetWritersId(dbContext, "西蒙·法纳比 / 保罗·金")),
                CastsId = new CastsIdVO(GetWritersId(dbContext, "提莫西·查拉梅 / 奥利维娅·科尔曼 / 休·格兰特 / 卡拉·莱恩 / 科甘-迈克尔·凯")),
                Year = new YearVO("2023"),
                Pubdates = new PubdatesVO("2023-12-08(中国大陆) / 2023-12-06(中国台湾) / 2023-12-15(美国)"),
                Durations = new DurationsVO("116分钟"),
                Genres = new GenresVO("005/013/003/008"),
                Languages = new LanguagesVO("001"),
                Countries = new CountriesVO("001/007/008"),
                Rating = new RatingVO(7.5m),
                RatingCount = new RatingCountVO(81199),
                DoubanID = new DoubanIDVO("26897888"),
                IMDb = new IMDbIDVO("tt6166392"),
                Summary = new SummaryVO("　　影片聚焦经典之作《查理和巧克力工厂》的主人公威利·旺卡（提莫西·查拉梅 饰），这位年轻且天赋异禀的发明家、魔术师、巧克力制作师，是如何经历奇幻冒险，成长为观众挚爱的巧克力工厂掌门人的故事。"),
                Avatar = new MovieAvatarVO("20240126170837.png"),
                ReviewStatus = ReviewStatusVO.Codes.ReviewStatusCode1,
                PageViews = new PageViewsVO(33),
                UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).First().UserId,
                CreatedOn = dateTime
            },
        };

        return list;
    }

    /// <summary>
    /// 影片资源
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static IEnumerable<ResourceEntity> GetResources(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext) =>
       new List<ResourceEntity>
       {
                new ResourceEntity(){
                    RequestId = uuid,
                    ResourceId = new ResourceIdVO(Guid.NewGuid()),
                    Name = new ResourceNameVO("[电影天堂www.dygod.net].剪刀手爱德华.[中英双字.1024分辨率]"),
                    Content = new ResourceContentVO("magnet:?xt=urn:btih:5e3e7fd6735c56ea1b936699ba7b6e549737a155蓝光国英双语中英双字"),
                    Size = new ResourceSizeVO(345678),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First(),
                    FavorCount = new FavorCountVO(120),
                    Type = new ResourceTypeVO(ResourceTypeVO.Codes.TypeCode0),
                    ReviewStatus = new ReviewStatusVO(ReviewStatusVO.Codes.ReviewStatusCode2),
                    Note = new NoteVO("非常好！！"),
                    CreatedOn = dateTime
                },
                new ResourceEntity(){
                    RequestId = uuid,
                    ResourceId = new ResourceIdVO(Guid.NewGuid()),
                    Name = new ResourceNameVO("剪刀手爱德华.Edward.Scissorhands.1990.BD720P.英语中字.mp4"),
                    Content = new ResourceContentVO("magnet:?xt=urn:btih:FEF4ED7CECD353E862C246FE798C8073CFBD81B6"),
                    Size = new ResourceSizeVO(33345678),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First(),
                    FavorCount = new FavorCountVO(120),
                    Type = new ResourceTypeVO(ResourceTypeVO.Codes.TypeCode0),
                    ReviewStatus = new ReviewStatusVO(ReviewStatusVO.Codes.ReviewStatusCode2),
                    Note = new NoteVO("非常好！！"),
                    CreatedOn = dateTime
                },
                new ResourceEntity(){
                    RequestId = uuid,
                    ResourceId = new ResourceIdVO(Guid.NewGuid()),
                    Name = new ResourceNameVO("剪刀手爱德华.Edward.Scissorhands.1990.BD1080P.国英双语中字.mp4"),
                    Content = new ResourceContentVO("magnet:?xt=urn:btih:986768CC6AA56154C4A1948327C22175E2741380"),
                    Size = new ResourceSizeVO(13345008),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First(),
                    FavorCount = new FavorCountVO(120),
                    Type = new ResourceTypeVO(ResourceTypeVO.Codes.TypeCode0),
                    ReviewStatus = new ReviewStatusVO(ReviewStatusVO.Codes.ReviewStatusCode2),
                    Note = new NoteVO("非常好！！"),
                    CreatedOn = dateTime
                },
                new ResourceEntity(){
                    RequestId = uuid,
                    ResourceId = new ResourceIdVO(Guid.NewGuid()),
                    Name = new ResourceNameVO("黑天鹅BD高清"),
                    Content = new ResourceContentVO("magnet:?xt=urn:btih:F1782861B0BFB4D91C04447CA2B1D15524366E45"),
                    Size = new ResourceSizeVO(12005678),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First(),
                    FavorCount = new FavorCountVO(231),
                    Type = new ResourceTypeVO(ResourceTypeVO.Codes.TypeCode0),
                    ReviewStatus = new ReviewStatusVO(ReviewStatusVO.Codes.ReviewStatusCode2),
                    Note = new NoteVO("非常好！！"),
                    CreatedOn = dateTime
                },
                new ResourceEntity(){
                    RequestId = uuid,
                    ResourceId = new ResourceIdVO(Guid.NewGuid()),
                    Name = new ResourceNameVO("雷神4：爱与雷霆-2022_HD中英双字"),
                    Content = new ResourceContentVO("magnet:?xt=urn:btih:e64aceecde28df2d027905c11c633320cebeef9e&amp;dn=[电影天堂www.dytt89.com]雷神4：爱与雷霆-2022_HD中英双字.mp4"),
                    Size = new ResourceSizeVO(12005678),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    FavorCount = new FavorCountVO(231),
                    Type = new ResourceTypeVO(ResourceTypeVO.Codes.TypeCode0),
                    ReviewStatus = new ReviewStatusVO(ReviewStatusVO.Codes.ReviewStatusCode2),
                    Note = new NoteVO("非常好！！"),
                    CreatedOn = dateTime
                },
       };

    /// <summary>
    /// 影片评论
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static IEnumerable<CommentEntity> GetComments(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext) =>
       new List<CommentEntity>
       {
           new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("tonyzhangshi"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("再多一个人扎纸解释虫洞我就当场尬死"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-1)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test01"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("第一部：《爸爸，再爱我一次》 第二部：《哥哥，再爱我一次》 第三部：《姐姐，再爱我一次》 第四部：《女友，再爱我一次》"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-2)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test02"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("第一，我不叫喂，我叫雷神雨荨！"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-3)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test03"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("蝙蝠侠因演得过于认真显得格格不入"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-4)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test04"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("宇宙许愿池是一次性吗为啥只能一个人许一个愿望呢？"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-5)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test05"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("彻底儿童片化了，好一坨五颜六色的欢乐大便哈哈。算上导演五个奥斯卡得主陪锤哥玩，马特•达蒙的客串自带喜感，罗素•克劳的宙斯小碎步才真是绝了~"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-6)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test06"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("差，真的差，請蝙蝠俠過來讓雷神揍是很爽，但你漫威一輩子都拍不出《黑暗騎士》，這就是區別。"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-7)),
                    CreatedOn = dateTime
                },

                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test01"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("除了JohnnyDepp 还能有谁能有那样的眼神？"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-1)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test02"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("爱德华的眼睛让人心疼"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-2)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test03"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("就让我相信全世界的雪都是爱德华下的吧。"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-3)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test04"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("如果我没有刀，我就不能保护你。如果我有刀，我就不能拥抱你。 这个城市看不到雪，我为你降一场雪，每一片雪花落地那都是在说，我爱你。 我现在已经是个老妇人了。我只愿他记得我当初的样子。"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-4)),
                    CreatedOn = dateTime
                },

                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test01"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("“我感受到了，感到了……完美。”一段用走火入魔来成就的完美，亦真亦假、亦实亦幻。影片从始至终都弥漫着黑暗的色调，很惊悚，很压抑，无论配乐还是摄影都极其吸引观众。娜塔莉·波特曼颠覆以往，奉献了一场精彩的演出。★★★★★"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-1)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test02"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("好看到呛着人，波特曼就是一直被过于标致的容貌给拖累的公主，这是要飞起来了，看到干巴成那样五官依然标致的前公主薇诺娜赖德又出来跑龙套好心酸"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-2)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test03"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("其实没想象中精彩，精神分裂的表现力度不太够，手法也比较陈旧，差出fight club几个档次，大半夜的愣是没让我有一丁点紧张或局促的感官反应。反倒是配乐和Natalie的精彩表演收服了我，整部电影在交响乐的烘托下活像一出慷慨悲壮的天鹅舞剧，Natalie美翻了"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-3)),
                    CreatedOn = dateTime
                },
                new CommentEntity(){
                    RequestId = uuid,
                    CommentId = new CommentIdVO(Guid.NewGuid()),
                    UserId = dbContext.UserAccounts.Where(d => d.Account.Equals(new AccountNameVO("test04"))).Select(d => d.UserId).First(),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First(),
                    Content = new ContentVO("镜子用多了，想不精神分裂都难。故事基本就是天鹅湖的变奏，与《摔跤手》一样的手持摄影，却少了摔跤手的写实风，多了些超现实的感觉与舞台味。女主、摄影、配乐、女配都很有实力。波特曼，好运！"),
                    CommentTime = new CommentTimeVO(dateTime.AddHours(-4)),
                    CreatedOn = dateTime
                },

       };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static IEnumerable<AlbumEntity> GetAlbums(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext)
    {
        var mvId01 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First().AsPrimitive().ToString();
        var mvId02 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First().AsPrimitive().ToString();
        var mvId03 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First().AsPrimitive().ToString();

        var list = new List<AlbumEntity>()
        {
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集A"), UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).Select(d => d.UserId).First(), Items = new($"{mvId01}, {mvId02}, {mvId03}"), Summary = new("全部都在这儿"), Cover = new("bc32641503490e83de10ef46cea98771.jpg"), AmountAttention = new(877), CreatedOn = dateTime },
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集A01"), UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).Select(d => d.UserId).First(), Items = new($"{mvId02}, {mvId03}"), Summary = new("部分在这儿"), Cover = new("cf65ac138d14bd173acdf159a7cd811b.jpg"), AmountAttention = new(1002), CreatedOn = dateTime },
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集A02"), UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).Select(d => d.UserId).First(), Summary = new("暂时没有收录内容"), Cover = new("Album_1.jpg"), AmountAttention = new(0), CreatedOn = dateTime },
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集B"), UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test01")).Select(d => d.UserId).First(), Items = new($"{mvId01}, {mvId02}"), Summary = new("商业片A"), Cover = new("Album_1.jpg"), AmountAttention = new(11), CreatedOn = dateTime },
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集C"), UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test02")).Select(d => d.UserId).First(), Items = new($"{mvId03}"), Summary = new("商业片B"), Cover = new("Album_1.jpg"), AmountAttention = new(22), CreatedOn = dateTime },
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集D"), UserId = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test03")).Select(d => d.UserId).First(), Summary = new("商业片"), Cover = new("Album_1.jpg"), AmountAttention = new(1), CreatedOn = dateTime },
        };
        return list;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static IEnumerable<MarkEntity> GetMarks(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext)
    {
        var mvId01 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First().AsPrimitive();
        var mvId02 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First().AsPrimitive();
        var mvId03 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First().AsPrimitive();

        var celebId01 = dbContext.Celebrities.Where(d => d.Name == new CelebrityNameVO("娜塔莉·波特曼")).Select(d => d.CelebrityId).First().AsPrimitive();
        var celebId02 = dbContext.Celebrities.Where(d => d.Name == new CelebrityNameVO("约翰尼·德普")).Select(d => d.CelebrityId).First().AsPrimitive();

        var tonyzhangshi = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).Select(d => d.UserId).First();
        var test01 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test01")).Select(d => d.UserId).First();
        var test02 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test02")).Select(d => d.UserId).First();
        var test03 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test03")).Select(d => d.UserId).First();
        var test04 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test04")).Select(d => d.UserId).First();
        var test05 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test05")).Select(d => d.UserId).First();
        var test06 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test06")).Select(d => d.UserId).First();

        var albumId01 = dbContext.Albums.Where(d => d.Title == new AlbumTitleVO("影集A")).Select(d => d.AlbumId).First().AsPrimitive();
        var albumId02 = dbContext.Albums.Where(d => d.Title == new AlbumTitleVO("影集B")).Select(d => d.AlbumId).First().AsPrimitive();
        var albumId03 = dbContext.Albums.Where(d => d.Title == new AlbumTitleVO("影集C")).Select(d => d.AlbumId).First().AsPrimitive();

        var list = new List<MarkEntity>()
        {
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = tonyzhangshi, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = tonyzhangshi, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = tonyzhangshi, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode5, UserId = tonyzhangshi, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode6, UserId = tonyzhangshi, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = tonyzhangshi, Target = new(mvId02), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = tonyzhangshi, Target = new(mvId02), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = tonyzhangshi, Target = new(mvId03), CreatedOn = dateTime },

            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = test01, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = test02, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = test03, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = test04, Target = new(mvId01), CreatedOn = dateTime },

            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = test01, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = test02, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = test03, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = test04, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = test05, Target = new(mvId01), CreatedOn = dateTime },

            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = test01, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = test02, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = test03, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = test04, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = test05, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = test06, Target = new(mvId01), CreatedOn = dateTime },

            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = tonyzhangshi, Target = new(albumId02), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test01, Target = new(albumId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test02, Target = new(albumId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test03, Target = new(albumId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test04, Target = new(albumId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test05, Target = new(albumId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test06, Target = new(albumId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = tonyzhangshi, Target = new(albumId03), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test05, Target = new(albumId03), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = test06, Target = new(albumId03), CreatedOn = dateTime },

            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode4, UserId = tonyzhangshi, Target = new(celebId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode4, UserId = tonyzhangshi, Target = new(celebId02), CreatedOn = dateTime },

        };
        return list;
    }

    /// <summary>
    /// 每日发现
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static IEnumerable<DiscoveryEntity> GetDiscoveries(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext) =>
       new List<DiscoveryEntity>
       {
                new DiscoveryEntity(){
                    RequestId = uuid,
                    DiscoveryId = new DiscoveryIdVO(Guid.NewGuid()),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).First().MovieId,
                    Avatar = new DiscoveryAvatarVO("p2305422832.jpg"),
                    Order = new SortOrderVO(1),
                    CreatedOn = dateTime
                },
                new DiscoveryEntity(){
                    RequestId = uuid,
                    DiscoveryId = new DiscoveryIdVO(Guid.NewGuid()),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).First().MovieId,
                    Avatar = new DiscoveryAvatarVO("p2220223845.jpg"),
                    Order = new SortOrderVO(2),
                    CreatedOn = dateTime },

                new DiscoveryEntity(){
                    RequestId = uuid,
                    DiscoveryId = new DiscoveryIdVO(Guid.NewGuid()),
                    MovieId = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).First().MovieId,
                    Avatar = new DiscoveryAvatarVO("p2181503009.jpg"),
                    Order = new SortOrderVO(3),
                    CreatedOn = dateTime },
       };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static IEnumerable<AskEntity> GetAsks(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext)
    {
        var mvId01 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First();
        var mvId02 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First();
        var mvId03 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First();

        var tonyzhangshi = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).Select(d => d.UserId).First();
        var test01 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test01")).Select(d => d.UserId).First();
        var test02 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test02")).Select(d => d.UserId).First();
        var test03 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test03")).Select(d => d.UserId).First();

        var list = new List<AskEntity>()
        {
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = tonyzhangshi, MovieId = mvId01, RequestTime = new(System.DateTime.Now), RequestWith = new(12300), Note = new("tonyzhangshi请求-雷神4：爱与雷霆"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = tonyzhangshi, MovieId = mvId02, RequestTime = new(System.DateTime.Now.AddHours(-1)), RequestWith = new(3455), Note = new("tonyzhangshi请求-剪刀手安德华"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = tonyzhangshi, MovieId = mvId03, RequestTime = new(System.DateTime.Now.AddHours(-2)), RequestWith = new(1233), Note = new("tonyzhangshi请求-黑天鹅"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = test01, MovieId = mvId01, RequestTime = new(System.DateTime.Now.AddHours(-3)), RequestWith = new(6667), Note = new("test01请求-雷神4：爱与雷霆"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = test01, MovieId = mvId02, RequestTime = new(System.DateTime.Now.AddHours(-4)), RequestWith = new(345), Note = new("test01请求-剪刀手安德华"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = test02, MovieId = mvId03, RequestTime = new(System.DateTime.Now.AddHours(-5)), RequestWith = new(222), Note = new("test02请求-黑天鹅"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = test03, MovieId = mvId03, RequestTime = new(System.DateTime.Now.AddHours(-6)), RequestWith = new(111), Note = new("test03请求-黑天鹅"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
        };
        return list;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static IEnumerable<FindPasswordEntity> GetFindPwds(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext)
    {
        var tonyzhangshi = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("tonyzhangshi")).Select(d => d.UserId).First();

        var list = new List<FindPasswordEntity>()
        {
            new(){ RequestId = uuid, FindId = new(Guid.NewGuid()), UserId = tonyzhangshi, Token = new("abc1234567"), EmailAddress = new("tonyfindpsw01@gamil.com"), ExpiryTime = new(System.DateTime.Now.AddMinutes(10)), IsEnabled = new(false), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, FindId = new(Guid.NewGuid()), UserId = tonyzhangshi, Token = new("abc7654321"), EmailAddress = new("tonyfindpsw02@gamil.com"), ExpiryTime = new(System.DateTime.Now.AddMinutes(20)), IsEnabled = new(false), CreatedOn = new(System.DateTime.Now) },
        };
        return list;
    }

}