﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilmHouse.Localization {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FilmHouse.Localization.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 Api Response Errors 的本地化字符串。
        /// </summary>
        public static string ApiResponseErrors {
            get {
                return ResourceManager.GetString("ApiResponseErrors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Api Response Time 的本地化字符串。
        /// </summary>
        public static string ApiResponseTime {
            get {
                return ResourceManager.GetString("ApiResponseTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 CommentId 的本地化字符串。
        /// </summary>
        public static string CommentId {
            get {
                return ResourceManager.GetString("CommentId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ConfirmPassword 的本地化字符串。
        /// </summary>
        public static string ConfirmPassword {
            get {
                return ResourceManager.GetString("ConfirmPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 EMail 的本地化字符串。
        /// </summary>
        public static string EMail {
            get {
                return ResourceManager.GetString("EMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 New Password 的本地化字符串。
        /// </summary>
        public static string NewPassword {
            get {
                return ResourceManager.GetString("NewPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Password 的本地化字符串。
        /// </summary>
        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 request Id 的本地化字符串。
        /// </summary>
        public static string RequestId {
            get {
                return ResourceManager.GetString("RequestId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 HttpStatus 的本地化字符串。
        /// </summary>
        public static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 UserName 的本地化字符串。
        /// </summary>
        public static string UserName {
            get {
                return ResourceManager.GetString("UserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The password does not match the confirmation password. 的本地化字符串。
        /// </summary>
        public static string ValidationConfirmPassword {
            get {
                return ResourceManager.GetString("ValidationConfirmPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Please enter the correct email address. 的本地化字符串。
        /// </summary>
        public static string ValidationEMail {
            get {
                return ResourceManager.GetString("ValidationEMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The password must contain at least 8 characters and digits. 的本地化字符串。
        /// </summary>
        public static string ValidationPassword {
            get {
                return ResourceManager.GetString("ValidationPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {0} is required. 的本地化字符串。
        /// </summary>
        public static string ValidationRequired {
            get {
                return ResourceManager.GetString("ValidationRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 {0} must be less than {1} characters. 的本地化字符串。
        /// </summary>
        public static string ValidationStringLength {
            get {
                return ResourceManager.GetString("ValidationStringLength", resourceCulture);
            }
        }
    }
}
