using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data;
using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FilmHouse.Core.Utils;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using Microsoft.IdentityModel.Abstractions;

namespace FilmHouse.Tests;

public class Seed
{
    public static async Task SeedAsync(FilmHouseDbContext dbContext, ILogger logger)
    {
        try
        {
            var uuid = new RequestIdVO(Guid.NewGuid());
            var sysDate = new CreatedOnVO(System.DateTime.Now);

            await dbContext.Configuration.AddRangeAsync(GetInitConfigurationSettings(uuid, sysDate));
            await dbContext.CodeMast.AddRangeAsync(GetInitCodeMastSettings(uuid, sysDate));
            await dbContext.SaveChangesAsync();

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
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
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
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.HomeDiscoveryMaxPage, Value = new("6"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.MovieSummaryShort, Value = new("250"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.MovieCommentMax, Value = new("10"), CreatedOn = dateTime },
            new() { RequestId = uuid, Key = ConfigKeyVO.Keys.WorkItemCelebMax, Value = new("4"), CreatedOn = dateTime },
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

            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("001"), Name = new CodeValueVO("英语"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("002"), Name = new CodeValueVO("法语"), Order = new SortOrderVO(2), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Language, Code = new CodeKeyVO("003"), Name = new CodeValueVO("意大利语"), Order = new SortOrderVO(3), CreatedOn  = dateTime },

            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("001"), Name = new CodeValueVO("美国"), Order = new SortOrderVO(1), CreatedOn  = dateTime },
            new() { RequestId = uuid, Group = CodeGroupVO.Codes.Country, Code = new CodeKeyVO("002"), Name = new CodeValueVO("澳大利亚"), Order = new SortOrderVO(2), CreatedOn  = dateTime },

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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
                CreatedOn = dateTime
            },
       };

    public static UserIdVO TestUser_tonyzhangshi_UserId { get; set; } = new UserIdVO(Guid.Parse("00000000-0000-0000-0000-000000000001"));
    public static AccountNameVO TestUser_tonyzhangshi_Name { get; } = new AccountNameVO("tonyzhangshi");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    private static IEnumerable<UserAccountEntity> GetUserAccounts(RequestIdVO uuid, CreatedOnVO dateTime) =>
       new List<UserAccountEntity>
       {
            new(){ RequestId = uuid, UserId = TestUser_tonyzhangshi_UserId, Account = TestUser_tonyzhangshi_Name, PasswordHash = new(new PasswordHashVO("Tony19811031").ToHash(TestUser_tonyzhangshi_Name.AsPrimitive())), EmailAddress = new("tonyzhangshi@163.com"), Avatar = new("0ACFC82E7D5A41FC8AB8FD4EF603C858Tony.jpg"), Cover = new("Cover_1.jpg"), IsAdmin = new(false), LastLoginIp = new("201.182.1.23"), CreatedOn = dateTime },
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                UserId = TestUser_tonyzhangshi_UserId,
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
                    UserId = TestUser_tonyzhangshi_UserId,
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
                    UserId = TestUser_tonyzhangshi_UserId,
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
                    UserId = TestUser_tonyzhangshi_UserId,
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
                    UserId = TestUser_tonyzhangshi_UserId,
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
                    UserId = TestUser_tonyzhangshi_UserId,
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
                    UserId = TestUser_tonyzhangshi_UserId,
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
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集A"), UserId = TestUser_tonyzhangshi_UserId, Items = new($"{mvId01}, {mvId02}, {mvId03}"), Summary = new("全部都在这儿"), Cover = new("bc32641503490e83de10ef46cea98771.jpg"), AmountAttention = new(877), CreatedOn = dateTime },
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集A01"), UserId = TestUser_tonyzhangshi_UserId, Items = new($"{mvId02}, {mvId03}"), Summary = new("部分在这儿"), Cover = new("cf65ac138d14bd173acdf159a7cd811b.jpg"), AmountAttention = new(1002), CreatedOn = dateTime },
            new(){ RequestId = uuid, AlbumId = new(Guid.NewGuid()), Title = new("影集A02"), UserId = TestUser_tonyzhangshi_UserId, Summary = new("暂时没有收录内容"), Cover = new("Album_1.jpg"), AmountAttention = new(0), CreatedOn = dateTime },
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
    private static IEnumerable<AskEntity> GetAsks(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext)
    {
        var mvId01 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First();
        var mvId02 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First();
        var mvId03 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First();

        var test01 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test01")).Select(d => d.UserId).First();
        var test02 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test02")).Select(d => d.UserId).First();
        var test03 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test03")).Select(d => d.UserId).First();

        var list = new List<AskEntity>()
        {
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = TestUser_tonyzhangshi_UserId, MovieId = mvId01, RequestTime = new(System.DateTime.Now), RequestWith = new(12300), Note = new("tonyzhangshi请求-雷神4：爱与雷霆"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = TestUser_tonyzhangshi_UserId, MovieId = mvId02, RequestTime = new(System.DateTime.Now.AddHours(-1)), RequestWith = new(3455), Note = new("tonyzhangshi请求-剪刀手安德华"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },
            new(){ RequestId = uuid, AskId = new(Guid.NewGuid()), UserId = TestUser_tonyzhangshi_UserId, MovieId = mvId03, RequestTime = new(System.DateTime.Now.AddHours(-2)), RequestWith = new(1233), Note = new("tonyzhangshi请求-黑天鹅"), Status = new(false), IsEnabled = new(true), CreatedOn = new(System.DateTime.Now) },

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
    private static IEnumerable<MarkEntity> GetMarks(RequestIdVO uuid, CreatedOnVO dateTime, FilmHouseDbContext dbContext)
    {
        var mvId01 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("雷神4：爱与雷霆")).Select(d => d.MovieId).First().AsPrimitive();
        var mvId02 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("剪刀手安德华")).Select(d => d.MovieId).First().AsPrimitive();
        var mvId03 = dbContext.Movies.Where(d => d.Title == new MovieTitleVO("黑天鹅")).Select(d => d.MovieId).First().AsPrimitive();

        var test01 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test01")).Select(d => d.UserId).First();
        var test02 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test02")).Select(d => d.UserId).First();
        var test03 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test03")).Select(d => d.UserId).First();
        var test04 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test04")).Select(d => d.UserId).First();
        var test05 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test05")).Select(d => d.UserId).First();
        var test06 = dbContext.UserAccounts.Where(d => d.Account == new AccountNameVO("test06")).Select(d => d.UserId).First();

        var list = new List<MarkEntity>()
        {
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode4, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode5, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode6, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode7, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId01), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode1, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId02), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode2, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId02), CreatedOn = dateTime },
            new(){ RequestId = uuid, MarkId = new(Guid.NewGuid()), Type = MarkTypeVO.Codes.MarkTypeCode3, UserId = TestUser_tonyzhangshi_UserId, Target = new(mvId03), CreatedOn = dateTime },

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


}
