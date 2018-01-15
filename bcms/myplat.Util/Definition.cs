using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    //系统常量定义类
    public class Definition
    {
        #region  ---------------常量字符串---------------

        //public static string Wap_Cookie = "wap_mil_cookie";
        //public static string Admin_Cookie = "admin_mil_cookie";

        public static string AnonymousPrefix = "游客_";

        public static int Expire_Minute_Cache_User = 300;//用户对象缓存有效期 (单位/分钟)
        public static int Expire_Minute_Cache_Article = 1200;//文章对象缓存有效期 (单位/分钟)
        public static int Expire_Minute_Cache_Content_List = 30;//频道内容列表缓存有效期 (单位/分钟)
        public static int Expire_Minute_Cache_MobileCode = 30;//手机验证码缓存有效期 (单位/分钟)
        public static int Expire_Minute_Cache_Post = 600;//帖子对象缓存有效期 (单位/分钟)
        public static int Expire_Minute_Cache_User_RelevantContent = 300;//用户定制数据缓存有效期 (单位/分钟)

        public static int PageSize_Content_Recommend = 20;//推荐内容分页数量
        public static int PageSize_Content_Discover = 6;//发现新内容分页数量
        public static int PageSize_HotCmt = 5;//文章热门评论分页数量
        public static int PageSize_Cmt = 20;//文章最新评论分页数量
        public static int PageSize_Collection = 20;//用户收藏分页数量
        public static int PageSize_Attention = 20;//用户关注分页数量
        public static int PageSize_UserMsg = 10;//用户消息分页数量
        public static int PageSize_UserDynamic = 20;//用户个人动态分页数量
        public static int PageSize_DynamicCmt = 20;//动态评论分页数量
        public static int PageSize_UserOpt = 20;//用户个人操作分页数量
        public static int PageSize_SearchTag = 20;//用户TAG词搜索结果分页数量
        public static int Count_Article_Num = 999;//限定文章数量
        public static int Cache_Content_Num = 50;//数据区间的缓存内容数量

        public static int Img_Avatar_Width = 200;//用户头像尺寸: 宽
        public static int Img_Avatar_Heigh = 200;//用户头像尺寸：高

        #endregion

        #region  ---------------枚举类型---------------
        //注册平台
        public static int Reg_Robot = 0;      //内置平台
        public static int Reg_Wap = 1;        //WAP站
        public static int Reg_Android = 2;    //安卓
        public static int Reg_Ios = 3;        //IOS
        public static int Reg_Pad = 4;        //Pad平板
        public static int Reg_Pc = 5;         //PC端
        public static int Reg_Tiexue = 8;     //来至铁血军事APP

        //UserMark：用户标记
        public static int UserMark_Robot = 1;     //机器人
        public static int UserMark_People = 2;    //普通用户
        public static int UserMark_Media = 3;     //自媒体
        public static int UserMark_manager = 4;   //管理员
        //用户状态
        public static int UserStatus_Normal = 1;     //正常
        public static int UserStatus_Forbidden = 2;  //禁言
        public static int UserStatus_Termination = 3;//封号
        //用户令牌生成方式
        public static int TokenType_Anonymous = 0;  //自动生成(未登录)
        public static int TokenType_Login = 1;      //登录生成(已登录)
        //第三方平台 
        public const int Platform_Weixin = 1;  //微信平台
        public const int Platform_Qq = 2;      //QQ平台
        public const int Platform_Weibo = 3;   //微博平台
        //绑定类型
        public const int Binding_Mobile = 1;  //绑定手机
        public const int Binding_Weixin = 2;  //绑定微信
        public const int Binding_Qq = 3;      //绑定QQ
        public const int Binding_Weibo = 4;   //绑定微博
        //用户关系
        public static int Relationship_Stranger = 0;          //陌生人
        public static int Relationship_A_Attention_B = 1;     //A单向关注B
        public static int Relationship_B_Attention_A = 2;     //B单向关注A
        public static int Relationship_A_And_B = 3;           //AB相互关注
        //Content内容类型
        
        public static int ContentType_Topic = 21;   //话题类型
        public static int ContentType_Post = 22;    //帖子类型
        public static int ContentType_BaiKe = 31;   //知识百科
        public static int ContentType_Special = 41; //专题类型
        public static int ContentType_News = 11;    //新闻资讯类型
        public static int ContentType_Article = 12; //文章类型
        public static int ContentType_Video = 13;   //视频类型
        public static int ContentType_Pic = 14;     //图片类型
        public static int ContentType_Subject = 10; //专题类型
        public static int ContentType_Ad_100 = 100;     //URL跳转内嵌页面广告
        public static int ContentType_Ad_101 = 101;     //平台软文显示广告
        public static int ContentType_Ad_102 = 102;     //点击直接下载广告
        /// <summary>
        /// 投票类型
        /// </summary>
        public static int ContentType_Vote = 51; //投票类型



        //内容浏览方式
        public static int ViewType_Local = 1;      //本地内容浏览
        public static int ViewType_Browser = 2;    //内嵌网页浏览
        //数据源标识
        public static int SourceType_Post = 1;      //帖子
        public static int SourceType_News = 2;      //资讯
        public static int SourceType_Video = 3;     //视频
        public static int SourceType_Subject = 4;     //专题
        public static int SourceType_Vote = 5; //投票类型
        public static int SourceType_Pic = 6; //图片类型
        //标识
        public static int Mark_Normal = 0;           //正常 
        public static int Mark_Recommend = 1;        //推荐
        public static int Mark_Hot = 2;              //热门   依据评论数指标
        public static int Mark_Essence = 3;          //精华   依据点赞数指标
        public static int Mark_Top = 4;              //置顶
        public static int Mark_Topic = 5;            //话题（专题）
        public static int Mark_BaiKe = 6;            //知识百科
        public static int Mark_Picture = 7;          //酷图
        public static int Mark_Video = 8;            //视频
        public static int Mark_Extension = 100;      //推广
        //状态
        public static int Status_Normal = 1;         //正常
        public static int Status_Deleted = 2;        //已删除
        public static int Status_Audited = 3;        //被审核
        public static int Status_Similarity = 4;        //相似文章
        #region  考试状态
        /// <summary>
        /// 未考试
        /// </summary>
        public static int ExamStatus_None = 0;         //未考试
        /// <summary>
        /// 已考试
        /// </summary>
        public static int ExamStatus_Normal = 1;         //已考试
        /// <summary>
        /// 考试失败
        /// </summary>
        public static int ExamStatus_Error = 2;        //考试失败
        #endregion
        #region 查重状态
        /// <summary>
        /// 未查重
        /// </summary>
        public static int CheckStatus_None = 0;         //
        /// <summary>
        /// 已查重
        /// </summary>
        public static int CheckStatus_Normal = 1;         //
        #endregion

        //文章评论类型
        public const int Publish_Art_Cmt = 1;        //评论文章  
        public const int Publish_Cmt_Reply = 2;      //回复评论
        //频道类型 1:推荐频道 2:订阅自媒体 3精选频道 4:热门频道 11:资讯频道 12:视频频道 13:图片频道  21:推荐版块 22:热门版块 23:精华版块 24:帖子内容版块
        public const int Chan_Type_Channel_Recommend = 1;         //系统推荐频道
        public const int Chan_Type_Channel_Subscribe = 2;         //订阅自媒体频道
        public const int Chan_Type_Channel_Essence = 3;           //精选频道
        public const int Chan_Type_Channel_Hot = 4;               //热门频道
        public const int Chan_Type_Channel_News = 11;             //资讯内容频道
        public const int Chan_Type_Channel_Video = 12;            //视频频道
        public const int Chan_Type_Channel_Pic = 13;              //图片频道
        public const int Chan_Type_Board_Recommend = 21;          //推荐版块
        public const int Chan_Type_Board_Hot = 22;                //热门版块
        public const int Chan_Type_Board_Essence = 23;            //精华版块
        public const int Chan_Type_Channel_Post = 24;             //帖子内容版块
        //频道类别
        public const int Chan_Category_Board = 1;        //版块
        public const int Chan_Category_Channel = 2;      //频道
        
        //消息状态
        public static int MsgStatus_New = 1;        //未读
        public static int MsgStatus_Read = 2;       //已读
        public static int MsgStatus_Deleted = 3;    //已删除
        //消息类型
        public static int MsgType_Private = 1;      //个人消息
        public static int MsgType_Notice = 2;       //系统通知
        //消息目标对象类型
        public static int MsgTarget_Article = 1;      //文章
        public static int MsgTarget_Article_Cmt = 2;  //文章的评论
        public static int MsgTarget_Dynamic = 3;      //动态
        public static int MsgTarget_Dynamic_Cmt = 4;      //动态的评论
        //消息动作类型
        public static int MsgAction_Cmt_Article = 11;       //Who评论了我的文章
        public static int MsgAction_Ding_Article = 12;      //Who顶了我的文章
        public static int MsgAction_Cai_Article = 13;       //Who踩了我的文章
        public static int MsgAction_Share_Article = 14;     //Who转发了我的文章
        public static int MsgAction_Collect_Article = 15;   //Who收藏了我的文章

        public static int MsgAction_Reply_Comment = 21;     //Who回复了我对文章评论
        public static int MsgAction_Ding_Comment = 22;      //Who顶了我对文章的评论

        public static int MsgAction_Cmt_Dynamic = 31;       //Who评论了我的动态
        public static int MsgAction_Ding_Dynamic = 32;      //Who顶了我的动态
        public static int MsgAction_Share_Dynamic = 33;     //Who转发了我的动态
        public static int MsgAction_Reply_DynamicCmt = 41;  //Who回复了我对动态的评论
        public static int MsgAction_Ding_DynamicCmt = 42;   //Who顶了我对动态的评论
        public static int MsgAction_Attention_Me = 51;      //Who关注了我
        public static int MsgAction_Call_Me = 61;           //Who艾特了我 @
        //动态状态
        public static int DynamicStatus_New = 1;        //未读
        public static int DynamicStatus_Read = 2;       //已读
        public static int DynamicStatus_Deleted = 3;    //已删除
        //动态目标类型
        public static int DynamicTarget_Article = 1;        //文章
        public static int DynamicTarget_Article_Cmt = 2;    //文章的评论
        public static int DynamicTarget_User = 3;           //用户
        public static int DynamicTarget_Channel = 4;        //频道
        public static int DynamicTarget_ShuoShuo = 5;       //说说
        public static int DynamicTarget_Topic = 6;          //话题
        public static int DynamicTarget_Topic_Cmt = 7;      //话题的评论
        //动态动作类型
        public static int DynamicAction_Publish_Article = 11;           //11：发表文章
        public static int DynamicAction_Publish_Article_Cmt = 12;       //12：评论文章
        public static int DynamicAction_Ding_Article = 13;              //13：顶文章
        public static int DynamicAction_Cai_Article = 14;               //14：踩文章
        public static int DynamicAction_Collect_Article = 15;           //15：收藏文章
        public static int DynamicAction_Share_Article = 16;             //16：转发文章
        public static int DynamicAction_Ding_Article_Cmt = 21;          //21：顶评论
        public static int DynamicAction_Reply_Article_Cmt = 22;         //22：回复评论
        public static int DynamicAction_Attention_User = 31;            //31：关注用户
        public static int DynamicAction_Subscribe_Channel = 41;         //41：订阅频道
        public static int DynamicAction_Publish_Shuoshuo = 51;          //51：用户发表说说
        public static int DynamicAction_Publish_Topic = 61;             //61：发表话题
        public static int DynamicAction_Publish_Topic_Cmt = 62;         //62：发表话题评论
        //动态 评论类型
        public const int Publish_Dynamic_Cmt = 1;        //发表动态的评论
        public const int Publish_DynamicCmt_Reply = 2;   //回复动态的评论
        //用户操作类型
        public const int OptType_Ding = 1;      //顶
        public const int OptType_Cai = 2;       //踩
        //用户操作目标类型
        public static int OptTargetType_Article = 1;        //针对文章
        public static int OptTargetType_Article_Cmt = 2;    //针对文章的评论
        public static int OptTargetType_Dynamic = 3;        //针对用户动态
        public static int OptTargetType_Dynamic_Cmt = 4;    //针对用户动态的评论
        public static int OptTargetType_SpecialEvt = 5;     //针对专题
        public static int OptTargetType_SpecialEvt_Cmt = 6; //针对专题的评论
        

        //条目对象类型：1：词条  2：内容  3：频道  4：话题
        public const int Item_Type_Tag = 1;        //Tag词条
        public const int Item_Type_Art = 2;        //文章
        public const int Item_Type_Channel = 3;    //频道
        public const int Item_Type_Topic = 4;      //话题

        //条目聚合 对象类型：1：词条  2：文章  3：频道  4：话题  10-19内容排行榜(预留)
        public const int ItemList_Type_Tag = 1;        //Tag词条
        public const int ItemList_Type_Art = 2;        //文章
        public const int ItemList_Type_Channel = 3;    //频道
        public const int ItemList_Type_Topic = 4;      //话题
        public const int ItemList_Type_Rank = 10;      //排行
        public const int ItemList_Type_Hot_Week = 11;  //一周热榜
        #endregion

        #region  ---------------提示信息定义---------------
        public static string Msg_Success = "操作成功!";
        public static string Msg_IdentifyingCode = "验证码已发送,请稍后...";
        //成功提示
        public static string Msg_User_Reg_Success = "注册成功！";
        public static string Msg_User_Login_Success = "登录成功！";
        public static string Msg_User_Logout_Success = "退出成功！";
        public static string Msg_User_ResetPwd_Success = "重置成功！";
        public static string Msg_User_UpdateNickName_Success = "成功修改昵称！";
        public static string Msg_User_UpdateSignature_Success = "成功修改签名！";
        public static string Msg_User_UpdateHeaderIcon_Success = "成功修改头像！";
        public static string Msg_User_Binding_Success = "绑定成功！";
        //业务提示
        public static string Msg_Content_List_Discover = "头条引擎发现了{0}篇文章";
        public static string Msg_Content_List_More = "头条引擎推荐了{0}篇文章";//为您推荐了{0}条内容
        public static string Msg_Content_List_Empty = "";
        
        //错误提示
        public static string Err_Msg_Fail = "{Msg:'操作失败,请重试!'}";
        public static string Err_User_NotFind = "{Msg:'用户不存在!'}";
        public static string Err_User_Exist = "{Msg:'用户已存在!'}";
        public static string Err_Create_User_Fail = "{Msg:'创建用户失败,请重试...'}";
        public static string Err_Mobile_NotReg = "{Msg:'手机号没有注册!'}";
        public static string Err_Mobile_Exist = "{Msg:'手机号已被注册!'}";
        public static string Err_NickName_Length = "{Msg:'昵称字数请在2-20个字符以内,请重新输入...'}";
        public static string Err_NickName_Illegal = "{Msg:'昵称包含特殊字符,请重新输入...'}";
        public static string Err_NickName_Exist = "{Msg:'昵称已存在,请重新输入...'}";
        public static string Err_Signature_Length = "{Msg:'签名长度请在2-100个字以内,请重新输入...'}";
        public static string Err_Mobile_Input_Err = "{Msg:'请输入正确的手机号...'}";
        public static string Err_Pwd_Error = "{Msg:'密码有误,请重新输入...'}";
        public static string Err_Art_NotFind = "{Msg:'文章不存在!'}";
        public static string Err_Cmt_NotFind = "{Msg:'评论不存在!'}";
        public static string Err_Post_NotFind = "{Msg:'帖子内容不存在!'}";
        public static string Err_PostCmt_NotFind = "{Msg:'帖子评论不存在!'}";
        public static string Err_DelCmt_NotPermission = "{Msg:'没有权限删除评论!'}";
        public static string Err_Channel_NotFind = "{Msg:'频道不存在!'}";
        public static string Err_Content_NotFind = "{Msg:'内容不存在!'}";
        public static string Err_Dynamic_NotFind = "{Msg:'用户动态不存在!'}";
        public static string Err_DynamicCmt_NotFind = "{Msg:'用户动态评论不存在!'}";
        public static string Err_SpecialEvt_NotFind = "{Msg:'专题内容不存在!'}";
        public static string Err_SpecialEvt_Cmt_NotFind = "{Msg:'专题评论不存在!'}";

        public static string Tip_Art_Title_No_Empty = "{Msg:'文章标题不能为空!'}";
        public static string Tip_Art_Content_No_Empty = "{Msg:'文章正文不能为空!'}";
        public static string Tip_Art_Content_Too_Short = "{Msg:'文章正文太短!'}";

        public static string Tip_MobileCode_NoPermission = "{Msg:'权限校验失败,请重试...'}";
        public static string Tip_MobileCode_UserLimit = "{Msg:'该IP/手机号超出当日发送数量限制'}";
        public static string Tip_MobileCode_GroupLimit = "{Msg:'该分组已超出当日发送总量限制'}";
        public static string Tip_MobileCode_InsertFailed = "{Msg:'操作失败'}";
        public static string Tip_Gen_MobileCode_Failed = "{Msg:'发送验证码失败,请稍后再试...'}";
        public static string Tip_MobileCode_Err = "{Msg:'验证失败,请重新输入...'}";

        //广告相关
        public static string Ad_Msg_Empty = "数据为空";
        public static string Ad_Msg_Normal = "数据正常";
        public static string Ad_Msg_Error = "数据请求失败";
        public static string Ad_Msg_Para_Error = "数据请求参数错误";
        #endregion

        #region  ---------------基础数据函数---------------
        /// <summary>
        /// 获取举报原因  根据ID
        /// </summary>
        /// <param name="model">文章实体类</param>
        /// <returns></returns>
        public static string GetAccusationCause(int CauseId)
        {
            switch (CauseId)
            {
                case 10:
                    return "淫秽色情";
                case 11:
                    return "营销垃圾广告";
                case 12:
                    return "恶意攻击辱骂";
                case 19:
                    return "我有话要说";
                case 20:
                    return "内容过时重复";
                case 21:
                    return "垃圾广告，色情";
                case 22:
                    return "内容质量垃圾";
                case 29:
                    return "给我们留言";
                default:
                    return "举报原因说明";
            }
        }
        #endregion


        #region  ---------------数据库字段枚举---------------
        //文章主表全部字段
        public static string DB_Fields_TTArticle_All = "Id,Title,Intro,CoverImgs,ImgShowType,ContentType,ViewType,PublisherId,PublisherName,PublisherIcon,ViewCount,DingCount,CaiCount,CommentCount,ShareCount,CollectionCount,ShowTime,CreateTime,Mark,Status,SourceType,Weight,OriginalId,OriginalTitle,OriginalIntro,OriginalUrl,OriginalViewCount,OriginalDingCount,OriginalCaiCount,OriginalCommentCount,OriginalTags,OriginalArticleTime,FromId,FromName,PlatformId,PlatformName,Tags,MultiTags,FondTags,RelationTags,ContentMarkTags,RelationalShowTags,RelationChannels,RelationArticles,RelationSorts,SolrStatus,SpecificChannelId,RelationBoards,RelationPosts,VideoDuration,VideoSrc,ContentLen";
        //文章主表文章详情字段
        public static string DB_Fields_TTArticle_Detailed = "Id,Title,Intro,CoverImgs,ImgShowType,ContentType,PublisherId,PublisherName,PublisherIcon,ViewCount,DingCount,CaiCount,CommentCount,ShareCount,CollectionCount,ShowTime,CreateTime,OriginalArticleTime,Mark,Status,SourceType,OriginalUrl,OriginalViewCount,OriginalDingCount,OriginalCaiCount,OriginalCommentCount,FromId,FromName,PlatformId,PlatformName,Tags,ContentMarkTags,RelationalShowTags,RelationChannels,RelationArticles,SpecificChannelId,RelationBoards,RelationPosts,VideoDuration,VideoSrc,ContentLen,MultiTags,FondTags";
        //没用的的字段： ViewType,Weight,OriginalId,OriginalTitle,OriginalIntro,OriginalTags,RelationTags,RelationSorts,SolrStatus,OriginalContent

        //文章列表字段枚举
        public static string DB_Fields_TTArticle_List = "Id,Title,Intro,CoverImgs,ImgShowType,ContentType,ViewType,PublisherId,PublisherName,PublisherIcon,ViewCount,DingCount,CaiCount,CommentCount,ShareCount,CollectionCount,ShowTime,CreateTime,Mark,Status,SourceType,Weight,OriginalUrl,OriginalViewCount,OriginalDingCount,OriginalCaiCount,OriginalCommentCount,FromId,FromName,PlatformId,PlatformName,VideoDuration,VideoSrc,ContentLen";
        //用户通知字段枚举
        public static string DB_Fields_TTUserNotice_List = "Id,AppId,UserToken,DeviceToken,Switch,LastCloseTime,CreateTime,CloseCount,Status,Mark";
        public static string DB_Fields_TTArticleJoinTTMsgPush = "[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].Id  as  Id ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].Type  as  Type ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].Status  as  Status ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].InfoType  as  InfoType ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].InfoId  as  InfoId ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].Des  as  Des ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].Title  as  Title ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue]. ntro  as  Intro ,[TiexueMobileTT_Manage].[dbo].[TTMsgPushQueue].CreateTime  as  CreateTime ,[Status_Ios],[OrderNo],[SetTime],[SendTime],[SendTime_Ios],[OverdueTime],[OpType],[Params],[PushTargetType],[PushTargetUnLive],[PushTargetDeviceToken],[RemarkInfo],SpecilMark";

        #endregion

    }
}
