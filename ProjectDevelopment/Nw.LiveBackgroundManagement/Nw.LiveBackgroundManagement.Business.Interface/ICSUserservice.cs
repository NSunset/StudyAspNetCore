using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.Query;
using Nw.LiveBackgroundManagement.Models.ViewModel;

namespace Nw.LiveBackgroundManagement.Business.Interface
{
    public interface ICSUserservice : IBaseService
    {
        #region 后台用户使用
        /// <summary>
        /// 获取主播的列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CSUser> QueyrCSUserList();

        /// <summary>
        /// 审批列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<CSUserApplyViewModel> ApprovalList(int userid);


        /// <summary>
        /// 审批用户
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="ApprovalMsg"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ApprovalUser(int userid, ApprovalStatusEnum ApprovalMsg, string message);


        /// <summary>
        ///  分页查询前台用户，判断用户是否需要审批成为主播
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userType"></param>
        /// <param name="url"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageResult<UserViewModel> GetPageReceptionUserList(string searchString, string userType, string url, int pageIndex, int pageSize);
         
        /// <summary>
        /// 分页查询前台用户，主播用户
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="url"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageResult<UserViewModel> GetPageReceptionAnchorUserList(string searchString, string url, int pageIndex, int pageSize);

        /// <summary>
        /// 冻结
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool FreezeUser(int UserId);

        /// <summary>
        /// 解冻用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool ThawUser(int UserId);

        #endregion


        #region 前台用户使用

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public ApiResult Register(RegisterModel registerModel);

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        ApiResult SendVerifyCode(string phone);

        /// <summary>
        /// 发送验证码前验证
        /// </summary>
        /// <param name="phone"></param>
        ApiResult CheckPhoneNumberBeforeSend(string phone);

        /// <summary>
        /// 前台用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="cSUser"></param>
        /// <returns></returns>
        public bool Login(string userName, string passWord, out CSUser cSUser);

        /// <summary>
        /// 前台用户详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApiResult CurrentUserDetail(int userId);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        public ApiResult UpdateUserDetail(CSUserViewModel userViewModel);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        public ApiResult UpdateCurrentPassword(UpdatePass userViewModel);

        /// <summary>
        /// 申请成为主播
        /// </summary>
        /// <returns></returns>
        public ApiResult ApplyToAnchor(CSUserApplyViewModel userApply);

        /// <summary>
        /// 当前用户的审批详情
        /// </summary>
        /// <param name="pageQuery"></param>
        /// <returns></returns>
        public ApiResult CSUserApplyPage(ApplyPageQuery pageQuery);

        #endregion

        #region 评论分页
        public ApiResult GetCurrentComment(CommentPageQuery queryWhere);

        #endregion

        #region 作品
        public ApiResult GetCurrentWorkPage(WorkPageQuery queryWhere);
        #endregion

        /// <summary>
        /// 我的关注 和历史记录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ApiResult QueyBrowsePage(BrowsePageQuery query);

        /// <summary>
        /// 获取首页展示的评论和回复详情
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public ApiResult GetCommentReplyByWorkId(int workId);

        /// <summary>
        /// 保存弹幕
        /// </summary>
        /// <param name="AddContent"></param>
        /// <returns></returns>
        public ApiResult AddBulletChat(CSCommentViewModel comment);

        /// <summary>
        ///  获取弹幕
        /// </summary>
        /// <param name="anchorId"></param>
        /// <param name="cSWorksId"></param>
        /// <param name="touristGuid"></param>
        /// <returns></returns>
        public ApiResult GetBulletChat(int anchorId, int cSWorksId, long bulletChatTime);

        /// <summary>
        /// 获取主播信息
        /// </summary>
        /// <param name="anchorId"></param>
        /// <returns></returns>
        public ApiResult GetAnchorById(int anchorId);


        /// <summary>
        /// 获取当前用户的积分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApiResult GetCurretnIntegral(int userId);

        /// <summary>
        /// 充值积分，发起微信支付，生成微信支付链接
        /// </summary>
        /// <param name="cSIntegral"></param>
        /// <returns></returns>
        public ApiResult RechargePoints(CSIntegralRechargeDetailViewModel cSIntegral);

        /// <summary>
        /// 微信充值积分记录
        /// </summary>
        /// <param name="pageQuery"></param>
        /// <returns></returns>
        public ApiResult GetRechargePointsPage(RechargePointsPageQuery pageQuery);


        /// <summary>
        /// 数据同步
        /// </summary>
        /// <returns></returns>
        public void SynchronizeFieldData();
    }
}
