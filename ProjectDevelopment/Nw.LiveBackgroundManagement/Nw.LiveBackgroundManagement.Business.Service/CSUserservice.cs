using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.Common.EncryptHelper;
using Nw.LiveBackgroundManagement.Models.Query;
using Nw.LiveBackgroundManagement.Common.CacheHelper;
using Nw.LiveBackgroundManagement.Common.WechatPayCore;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using RedisHelper.Service;

namespace Nw.LiveBackgroundManagement.Business.Services
{
    public class CSUserservice : BaseService, ICSUserservice
    {
        private readonly RedisStringService _redisString;
        private readonly RedisZSetService _redisZSetService;
        private readonly IMapper _iMapper;
        private readonly PayHelper _PayHelper = null;

        public CSUserservice(
            AuthorityDbContext context,
            IMapper mapper,
            RedisStringService redisStringService,
            PayHelper payHelper,
            RedisZSetService redisZSetService
            ) : base(context)
        {
            _iMapper = mapper;
            _redisString = redisStringService;
            this._PayHelper = payHelper;
            _redisZSetService = redisZSetService;
        }


        #region 后台用户使用


        /// <summary>
        /// 获取前提用户提交的审批申请
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<CSUserApplyViewModel> ApprovalList(int userid)
        {
            List<CSUserApply> CSUserApplylist = base.Query<CSUserApply>(u => u.CSUserId == userid)
               .OrderByDescending(u => u.LastModifyTime)
               .ToList();
            return _iMapper.Map<List<CSUserApply>, List<CSUserApplyViewModel>>(CSUserApplylist);
        }

        public bool ApprovalUser(int userid, ApprovalStatusEnum state, string message)
        {
            CSUser user = _Context.Find<CSUser>(userid);
            user.UserType = (int)UserTypeEnum.Anchor;
            user.ApplysState = (int)state;
            CSUserApply cSUserApply = _Context.Set<CSUserApply>().OrderByDescending(u => u.CreateTime).FirstOrDefault(c => c.State == (int)ApprovalStatusEnum.UnderApproval);
            cSUserApply.State = (int)(state);
            cSUserApply.ApprovalMsg = message;
            return _Context.SaveChanges() > 0;
        }

        public bool FreezeUser(int UserId)
        {
            CSUser user = _Context.Find<CSUser>(UserId);
            user.Status = (int)UserStatusEnum.Frozen;
            _Context.Update<CSUser>(user);
            return _Context.SaveChanges() > 0;
        }

        public PageResult<UserViewModel> GetPageReceptionAnchorUserList(string searchString, string url, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 所有前台用户的分页列表
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userType"></param>
        /// <param name="url"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual PageResult<UserViewModel> GetPageReceptionUserList(string searchString, string userType, string url, int pageIndex, int pageSize)
        {
            Expression<Func<CSUser, bool>> expressionWhere = u => true;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                expressionWhere = expressionWhere.And(s => s.Name.Contains(searchString) || s.Phone.Contains(searchString) || s.Mobile.Contains(searchString) || s.Email.Contains(searchString) || s.QQ.ToString().Contains(searchString) || s.WeChat.Contains(searchString));
            }
            if (userType != "0" && userType != null)
            {
                expressionWhere = expressionWhere.And<CSUser>(u => u.UserType == (Convert.ToInt32(userType)));
            }
            Expression<Func<CSUser, DateTime>> expressionOrdery = c => c.CreateTime;
            PageResult<CSUser> pageResult = base.QueryPage(expressionWhere, pageSize, pageIndex, expressionOrdery, false);

            PageResult<UserViewModel> page = new()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                TotalCount = pageResult.TotalCount,
                DataList = _iMapper.Map<List<CSUser>, List<UserViewModel>>(pageResult.DataList)
            };
            return page;
        }

        public IEnumerable<CSUser> QueyrCSUserList()
        {
            throw new NotImplementedException();
        }

        public bool ThawUser(int UserId)
        {
            CSUser user = _Context.Find<CSUser>(UserId);
            user.Status = (int)UserStatusEnum.Normal;
            _Context.Update<CSUser>(user);
            return _Context.SaveChanges() > 0;
        }

        #endregion



        #region 前台用户使用

        /// <summary>
        /// 查询审批历史记录
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ApiResult CSUserApplyPage(ApplyPageQuery pageQuery)
        {
            Expression<Func<CSUserApply, bool>> expressionWhere = c => c.CSUserId == pageQuery.CurrentUserId;
            if (!string.IsNullOrWhiteSpace(pageQuery.StringWord))
            {
                Expression<Func<CSUserApply, bool>> expressionWhere1 = c => c.ApprovalMsg.Contains(pageQuery.StringWord.Trim());
                expressionWhere.And<CSUserApply>(expressionWhere1);
            }
            Expression<Func<CSUserApply, DateTime>> expressionOrder = c => c.CreateTime;
            PageResult<CSUserApply> pageResult = base.QueryPage<CSUserApply, DateTime>(expressionWhere, pageQuery.PageSize, pageQuery.PageIndex, expressionOrder, false);
            PageResult<CSUserApplyViewModel> result = new PageResult<CSUserApplyViewModel>()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                TotalCount = pageResult.TotalCount,
                DataList = _iMapper.Map<List<CSUserApply>,
                List<CSUserApplyViewModel>>(pageResult.DataList)
            };
            return new ApiResult() { Data = result };
        }

        /// <summary>
        /// 申请成为主播
        /// </summary>
        /// <returns></returns>
        public ApiResult ApplyToAnchor(CSUserApplyViewModel userApply)
        {
            CSUserApply apply = new CSUserApply()
            {
                CSUserId = userApply.CSUserId,
                CreateTime = DateTime.Now,
                ApprovalMsg = userApply.ApprovalMsg,
                Remark = userApply.Remark,
                LastModifyId = userApply.CSUserId,
                State = (int)ApprovalStatusEnum.UnderApproval
            };
            this._Context.Add(apply);
            CSRoom cSRoom = new CSRoom()
            {
                UserId = userApply.CSUserId,
                RoomName = userApply.RoomName,
                Description = userApply.Description,
                CSRoomTypeId = userApply.CSRoomTypeId,
                CreateTime = DateTime.Now,
                CreateId = userApply.CSUserId
            };
            this._Context.Add(cSRoom);
            CSUser cSUser = base.Find<CSUser>(userApply.CSUserId);
            cSUser.ApplysState = (int)ApprovalStatusEnum.UnderApproval;   //用户信息的审批状态字段：冗余的；完全是为了前端判断的时候，能够直接基于用户判断
            bool bReult = _Context.SaveChanges() > 0;
            if (bReult)
            {
                return new ApiResult();
            }
            else
            {
                return new ApiResult()
                {
                    ErrorMessage = "审批发送失败了"
                };
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        public ApiResult UpdateUserDetail(CSUserViewModel userViewModel)
        {
            CSUser cSUser = _Context.Find<CSUser>(userViewModel.Id);
            cSUser.Name = userViewModel.Name;
            cSUser.Phone = userViewModel.Phone;
            cSUser.WeChat = userViewModel.WeChat;
            cSUser.Email = userViewModel.Email;
            cSUser.Address = userViewModel.Address;
            cSUser.QQ = userViewModel.QQ;
            cSUser.Sex = userViewModel.Sex;
            cSUser.ImgUrl = userViewModel.ImgUrl;
            Commit();
            return new ApiResult();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        public ApiResult UpdateCurrentPassword(UpdatePass updatePass)
        {
            if (!string.Equals(updatePass.NewPass, updatePass.NewPass1))
            {
                return new ApiResult() { ErrorMessage = "两次输入的新密码不相同" };
            }
            string encryptOldPass = MD5Encrypt.Encrypt(RegisterModel.PwdAddsalt(updatePass.OldPass));
            CSUser cSUser = _Context.Set<CSUser>().FirstOrDefault(cs => cs.Id == updatePass.UserId && cs.Password == encryptOldPass);
            if (cSUser == null)
            {
                return new ApiResult() { ErrorMessage = "原密码输入不正确" };
            }
            cSUser.Password = MD5Encrypt.Encrypt(RegisterModel.PwdAddsalt(updatePass.NewPass));
            Commit();
            return new ApiResult();
        }

        /// <summary>
        /// 前台用户详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApiResult CurrentUserDetail(int userId)
        {
            CSUser cSUser = Find<CSUser>(userId);
            return new ApiResult()
            {
                Data = _iMapper.Map<CSUser, CSUserViewModel>(cSUser)
            };
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="cSUserViewModel"></param>
        /// <returns></returns>
        public ApiResult Register(RegisterModel registerModel)
        {
            var smsVerificationCode = _redisString.Get(registerModel.GetUserVerifyVodeKey);

            if (string.IsNullOrWhiteSpace(smsVerificationCode) || string.Equals(smsVerificationCode.Replace("\"", ""), registerModel.SmsVerificationCode, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                return new ApiResult()
                {
                    ErrorMessage = ""
                };
            }

            CSUser cSUser = new CSUser();
            cSUser.CreateTime = DateTime.Now;
            cSUser.LastModifyTime = DateTime.Now;
            cSUser.UserType = (int)UserTypeEnum.OrdinaryUsers;
            cSUser.Status = (int)UserStatusEnum.Normal;
            cSUser.ApplysState = (int)ApprovalStatusEnum.NoApproval;
            cSUser.Password = MD5Encrypt.Encrypt(RegisterModel.PwdAddsalt(registerModel.Password));
            cSUser.Name = registerModel.Name;
            cSUser.Mobile = registerModel.Mobile;
            cSUser.Phone = registerModel.Mobile;

            CSUser user = base.Insert<CSUser>(cSUser);
            return new ApiResult()
            {
                Data = user
            };
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ApiResult SendVerifyCode(string phone)
        {
            Random random = new Random();
            string code = random.Next(100000, 999999).ToString();// 生成随机6位数字验证码

            _redisString.Set(RegisterModel.GetVerifyVodeKey(phone), code, TimeSpan.FromMinutes(5));// 把验证码存储到redis中  5分钟有效,有则覆盖

            _redisString.Set(RegisterModel.GetVerifyVodeKey($"{phone}1m1t"), code, TimeSpan.FromMinutes(1));//一分钟只能发一次 

            var sendResult = SMSTool.SendValidateCode(phone, code);// 调用发送短信的方法
            ApiResult apiResult = new ApiResult();
            if (sendResult.Item1 == false)
            {
                apiResult.ErrorMessage = sendResult.Item3;
            }
            return apiResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ApiResult CheckPhoneNumberBeforeSend(string phone)
        {
            ApiResult apiResult = new ApiResult();
            var list = this._Context.Set<CSUser>().Where(u => u.Phone.Equals(phone)).ToList();
            if (list.Count > 0)
            {
                apiResult.ErrorMessage = "手机号码重复";
                return apiResult;
            }
            if (!string.IsNullOrWhiteSpace(
                _redisString.Get(
                    RegisterModel.GetVerifyVodeKey($"{phone}1m1t")
                    )
                ))
            {

                apiResult.ErrorMessage = "1分钟内只能发一次验证码";
                return apiResult;
            }
            return apiResult;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool Login(string userName, string passWord, out CSUser cSUser)
        {
            var pass = MD5Encrypt.Encrypt(RegisterModel.PwdAddsalt(passWord));
            CSUser user = _Context.Set<CSUser>().FirstOrDefault(c => c.Name.Equals(userName) && c.Password.Equals(pass));
            if (user == null)
            {
                cSUser = null;
                return false;
            }
            cSUser = user;
            return true;
        }


        #endregion

        #region 评论管理
        public ApiResult GetCurrentComment(CommentPageQuery queryWhere)
        {
            ApiResult apiResult = new ApiResult();
            Expression<Func<CSComment, bool>> expressionWhere = c => c.CSCommentType == queryWhere.CommentType && c.ToUserId == queryWhere.UserId;

            Expression<Func<CSComment, DateTime>> expressionOrder = c => c.CreateTime;

            PageResult<CSComment> pageResult = base.QueryPage<CSComment, DateTime>(expressionWhere, queryWhere.PageSize, queryWhere.PageIndex, expressionOrder, false);

            PageResult<CSCommentViewModel> result = new PageResult<CSCommentViewModel>()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                TotalCount = pageResult.TotalCount,
                DataList = _iMapper.Map<List<CSComment>,
                List<CSCommentViewModel>>(pageResult.DataList)
            };
            return new ApiResult() { Data = result };
        }

        /// <summary>
        /// 获取当前主播的所有作品
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        public ApiResult GetCurrentWorkPage(WorkPageQuery queryWhere)
        {
            Expression<Func<CSWorks, bool>> expressionWhere = c => c.WorkType == queryWhere.WorkType && c.UserId == queryWhere.UserId;

            Expression<Func<CSWorks, DateTime>> expressionOrder = c => c.CreateTime;


            PageResult<CSWorks> pageResult = base.QueryPage<CSWorks, DateTime>(expressionWhere, queryWhere.PageSize, queryWhere.PageIndex, expressionOrder, false);

            List<CSWorkViewModel> list = new List<CSWorkViewModel>();
            foreach (var data in pageResult.DataList)
            {
                CSWorkViewModel cSWorkView = _iMapper.Map<CSWorks, CSWorkViewModel>(data);
                cSWorkView.CSCommentCount = data.CSComment.Count(c => c.CSCommentType == 1);
                cSWorkView.BulletChatCount = data.CSComment.Count(c => c.CSCommentType == 2);
                list.Add(cSWorkView);
            }

            //List<CSWorkViewModel> list = _iMapper.Map<List<CSWorks>,
            //    List<CSWorkViewModel>>(pageResult.DataList);

            PageResult<CSWorkViewModel> result = new PageResult<CSWorkViewModel>()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                TotalCount = pageResult.TotalCount,
                DataList = list
            };
            return new ApiResult() { Data = result };
        }
        #endregion

        /// <summary>
        /// 我的关注 和历史记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApiResult QueyBrowsePage(BrowsePageQuery query)
        {
            ApiResult apiResult = new ApiResult();

            Expression<Func<CSFollow, bool>> expressionWhere = c => c.UserId == query.UserId && c.CollAttentionType == (int)query.CollAttentionType;

            Expression<Func<CSFollow, DateTime>> expressionOrder = c => c.CreateTime;

            PageResult<CSFollow> pageResult = base.QueryPage<CSFollow, DateTime>(expressionWhere, query.PageSize, query.PageIndex, expressionOrder, false);

            var anchorIds = pageResult.DataList.Select(c => c.AnchorId);

            IQueryable<CSUser> userQuery = _Context.Set<CSUser>().Where(c => c.UserType == (int)(UserTypeEnum.Anchor) && anchorIds.Contains(c.Id));

            IQueryable<CSRoom> roomQuery = _Context.Set<CSRoom>().Where(c => anchorIds.Contains(c.UserId));


            List<CSFollowViewModel> resultlist = new List<CSFollowViewModel>();
            foreach (var csuser in pageResult.DataList)
            {
                var anchor = userQuery.FirstOrDefault(c => c.Id == csuser.AnchorId);
                var room = roomQuery.FirstOrDefault(c => c.UserId == csuser.AnchorId);
                var liveState = _redisString.Get(CacheKeyConstant.GetLiveStateConstant(csuser.AnchorId));
                CSFollowViewModel cSFollow = new CSFollowViewModel()
                {
                    AnchorId = csuser.AnchorId,
                    CreatorId = csuser.CreatorId,
                    CreateTime = csuser.CreateTime,
                    LastModifyTime = csuser.LastModifyTime,
                    UserId = csuser.UserId,
                    AnchorName = anchor?.Name,
                    ImgUrl = room?.RoomImgUrl,
                    RoomName = room?.RoomName,
                    LiveState = !string.IsNullOrWhiteSpace(liveState) ? (int)LiveStateEnum.Live : (int)LiveStateEnum.LiveOver
                };
                resultlist.Add(cSFollow);
            }
            PageResult<CSFollowViewModel> pageData = new PageResult<CSFollowViewModel>()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                TotalCount = pageResult.TotalCount,
                DataList = resultlist
            };
            apiResult.Data = pageData;
            return apiResult;
        }

        /// <summary>
        /// 获取某一个主播作品下的各种品论信息和回复
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public ApiResult GetCommentReplyByWorkId(int workId)
        {
            var csCommentlist = _Context.Set<CSComment>().Where(c => c.CSWorksId == workId && c.CSCommentType == 1).OrderBy(x => x.CreateTime).ToList();

            List<int> comentIds = csCommentlist.Select(c => c.Id).ToList();

            var childData = _Context.Set<CSCommentReply>().Where(c => comentIds.Contains(c.CommentId)).ToList();

            ///有多少栋楼评论；  包含了每一栋楼的第一条评论数据
            var csCommentQuery = csCommentlist.Select(c => new CSCommentQueryViewModel()
            {
                CommentId = c.Id,
                ImgUrl = c.ImgUrl,
                CommentDate = c.CreateTime.ToString(),
                CommentName = c.ToUserName,
                Content = c.Text,
                ToUserId = c.FromUserId
            }).ToList();

            RecursionCommentReply(csCommentQuery, childData);

            return new ApiResult() { Data = csCommentQuery };
        }

        private void RecursionCommentReply(
            List<CSCommentQueryViewModel> csCommentQuery,
            List<CSCommentReply> childData
            )
        {
            foreach (var cSComment in csCommentQuery)
            {
                cSComment.ChildData = childData.Where(c =>
                                            c.CommentId == cSComment.CommentId &&
                                            c.FromUserId == cSComment.ToUserId
                                        )
                                        .OrderBy(x => x.CreateTime)
                                        .Select(c => new CSCommentQueryViewModel()
                                        {
                                            CommentId = c.CommentId,
                                            ImgUrl = c.ImgUrl,
                                            CommentDate = c.CreateTime.ToString(),
                                            CommentName = c.ToUserName,
                                            RepliesName = c.FromUserName,
                                            Content = c.Text,
                                            ToUserId = c.FromUserId
                                        })
                                        .ToList();
                cSComment.RepliesCount = cSComment.ChildData.Count.ToString();

                RecursionCommentReply(cSComment.ChildData, childData);
            }
        }

        /// <summary>
        /// 添加弹幕
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public ApiResult AddBulletChat(CSCommentViewModel comment)
        {
            //_RedisHashService.SetEntryInHash(CacheKeyConstant.GetBulletChatKeyContant(comment.ToUserId, comment.LiveWorksId), $"{Guid.NewGuid()}_{comment.FromUserName}", comment.Text);

            string key = CacheKeyConstant.GetBulletChatKeyContant(comment.ToUserId, comment.LiveWorksId);
            string bulletChat = $"{comment.FromUserName}:{comment.Text}";
            _redisZSetService.SortedSetAdd(key, bulletChat, comment.BulletChatTime);

            ApiResult apiResult = new ApiResult();
            CSComment addComment = _iMapper.Map<CSCommentViewModel, CSComment>(comment);
            addComment.CreateTime = DateTime.Now;
            addComment.CSCommentType = 2;
            addComment.LastModifyTime = DateTime.Now;
            _Context.Set<CSComment>().Add(addComment);
            int flg = _Context.SaveChanges();
            if (flg <= 0)
            {
                apiResult.ErrorMessage = "失败";
            }
            return apiResult;
        }

        /// <summary>
        /// 获取弹幕
        /// </summary>
        /// <param name="anchorId"></param>
        /// <param name="cSWorksId"></param>
        /// <param name="bulletChatTime"></param>
        /// <returns></returns>
        public ApiResult GetBulletChat(int anchorId, int cSWorksId, long bulletChatTime)
        {
            ApiResult apiResult = new ApiResult();
            {
                string key = CacheKeyConstant.GetBulletChatKeyContant(anchorId, cSWorksId);
                //List<string> bulletChatList = _RedisZSetService.GetRangeFromSortedSetByHighestScore(key, bulletChatTime + 5, bulletChatTime - 5);
                {
                    var dic = _redisZSetService.SortedSetRangeByRankWithScores(key, bulletChatTime + 5, bulletChatTime - 5);

                    apiResult.Data = dic.Select(c => c.Element).ToList();
                }

            }
            return apiResult;
        }


        /// <summary>
        /// 获取主播信息
        /// </summary>
        /// <param name="anchorId"></param>
        /// <returns></returns>
        public ApiResult GetAnchorById(int anchorId)
        {
            //先去看看Redis中的记录---当前主播是不是正在直播直播
            string strLiveWorks = _redisString.Get($"Live_works_{anchorId}");

            ApiResult apiResult = new ApiResult();
            CSUser cSUser = _Context.Set<CSUser>().FirstOrDefault(c => c.UserType == (int)UserTypeEnum.Anchor && c.Id == anchorId);
            if (cSUser != null)
            {
                CSUserViewModel cSUserView = _iMapper.Map<CSUser, CSUserViewModel>(cSUser);
                if (strLiveWorks == null)
                {
                    strLiveWorks = "1";//这里是模拟的假数据
                }
                cSUserView.LiveWorksId = strLiveWorks;
                apiResult.Data = cSUserView;

            }
            else
            {
                apiResult.ErrorMessage = "找不到这个主播用户";
            }
            return apiResult;
        }



        #region 积分
        /// <summary>
        /// 积分充值
        /// </summary>
        /// <param name="cSIntegral"></param>
        /// <returns></returns>
        public ApiResult RechargePoints(CSIntegralRechargeDetailViewModel cSIntegral)
        {
            CSIntegralRechargeDetail cSIntegralRecharge = _iMapper.Map<CSIntegralRechargeDetailViewModel, CSIntegralRechargeDetail>(cSIntegral);
            cSIntegralRecharge.CreateTime = DateTime.Now;
            cSIntegralRecharge.LastModifyTime = DateTime.Now;
            cSIntegralRecharge.LastModifierId = cSIntegral.UserId;
            cSIntegralRecharge.CreatorId = cSIntegral.UserId;

            ///自动生成一个订单号
            string orderNum = $"ZhaoXi{DateTime.Now.ToString("yyyyMMddHHmmssfffff")}";
            cSIntegralRecharge.OrderNum = orderNum;
            _Context.Set<CSIntegralRechargeDetail>().Add(cSIntegralRecharge);
            string payUrl = string.Empty;
            if (_Context.SaveChanges() > 0)
            {

                //从这里开始就是准备付钱：
                //生成支付链接 
                string redisKey = CacheKeyConstant.GetPayUrlRedisKeyPrefix(orderNum);
                payUrl = this._redisString.Get(redisKey);
                if (string.IsNullOrWhiteSpace(payUrl))
                {
                    payUrl = _PayHelper.CreatePayUrl(orderNum, "朝夕直播平台积分充值", cSIntegral.Amount.ToString(), null);
                    _redisString.Set(redisKey, payUrl);
                }
                return new ApiResult()
                {
                    Data = new
                    {
                        redisKey,
                        orderNum
                    }
                };
            }
            else
            {
                return new ApiResult() { ErrorMessage = "充值失败" };
            }

        }


        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="pageQuery"></param>
        /// <returns></returns>
        public ApiResult GetRechargePointsPage(RechargePointsPageQuery pageQuery)
        {
            ApiResult apiResult = new ApiResult();
            Expression<Func<CSIntegralRechargeDetail, bool>> expressionWhere = c => c.UserId == pageQuery.UserId;

            Expression<Func<CSIntegralRechargeDetail, DateTime>> expressionOrder = c => c.CreateTime;

            PageResult<CSIntegralRechargeDetail> pageResult = base.QueryPage<CSIntegralRechargeDetail, DateTime>(expressionWhere, pageQuery.PageSize, pageQuery.PageIndex, expressionOrder, false);

            PageResult<CSIntegralRechargeDetailViewModel> result = new PageResult<CSIntegralRechargeDetailViewModel>()
            {
                PageIndex = pageResult.PageIndex,
                PageSize = pageResult.PageSize,
                TotalCount = pageResult.TotalCount,
                DataList = _iMapper.Map<List<CSIntegralRechargeDetail>,
                List<CSIntegralRechargeDetailViewModel>>(pageResult.DataList)
            };
            return new ApiResult() { Data = result };
        }



        /// <summary>
        /// 获取当前用户的积分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApiResult GetCurretnIntegral(int userId)
        {
            CSIntegralRecharge cSIntegral = _Context.Set<CSIntegralRecharge>().FirstOrDefault(c => c.UserId == userId);
            CSIntegralRechargeViewModel result = _iMapper.Map<CSIntegralRecharge, CSIntegralRechargeViewModel>(cSIntegral);
            return new ApiResult() { Data = result };
        }


        #endregion


        #region 定时任务处理

        public void SynchronizeFieldData()
        {
            List<CSComment> cSCommentList = this.Query<CSComment>(c => c.CSCommentType == (int)CSCommentTypeEnum.Comment && string.IsNullOrWhiteSpace(c.ImgUrl)).ToList();

            if (cSCommentList == null || cSCommentList.Count == 0)
            {
                return;
            }

            List<int> userIds = cSCommentList.Select(c => c.FromUserId).ToList();
            var userlist = _Context.Set<CSUser>().Where(c => userIds.Contains(c.Id)).Select(c => new
            {
                UserId = c.Id,
                ImgUrl = c.ImgUrl
            });
            foreach (var item in cSCommentList)
            {
                item.ImgUrl = userlist.FirstOrDefault(c => c.UserId == item.FromUserId).ImgUrl;
            }
            _Context.SaveChanges();
        }
        #endregion
    }
}
