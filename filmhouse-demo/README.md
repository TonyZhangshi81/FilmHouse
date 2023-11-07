# 资源文件管理

以下目录结构
```test
📁css
  📄custom.css
  📄site.css
📁fonts
  📄Weston Free.otf
📁js
  📁Home
    📄Index.js
    📄XXXXXXX.js
  📁XXXXX
    📄XXXXXXX.js
📁lib
  📁backstrech
    📄jquery.backstrech.min.js (ver2.0.4)
  📁bootstrap
    📁css
      📄bootstrap.css (ver3.3.6)
    📁fonts
      📁glyphicons
    📁js
      📄bootstrap.js (jquery 版本要求大于1.9且小于3.0)
  📁font-awesome
    📁css
      📄font-awesome.css (ver4.7.0)
    📁fonts
  📁jquery
    📄jquery.js (ver2.2.4)
  📁jquery-validation
    📁dist
      📄additional-methods.js
      📄jquery.validate.js (ver1.19.5)
  📁jquery-validation-unobtrusive
    📄jquery.validate.unobtrusive.js (ver4.0.0)
  📁modernizr
    📄modernizr.js (ver2.8.3)
  📁respond
    📄respond.js (ver1.2.0)
  📁smoothscroll
    📄jquery.smooth-scroll.js (ver1.4.10)
  📁thumbnailscroller
    📁css
      📄jquery.mThumbnailScroller.css
    📁js
      📄jquery.mThumbnailScroller.js (ver2.0.2)

```

注意：
由于`bootstarp`的`navbar`运用在不同版本之间存在差异，暂时先使用比较熟悉的低版本布局。由于低版本`bootstarp`对应依赖低版本的`jquery`，所以`bootstarp`和`jquery`暂时都使用低端版本`lib`对应
