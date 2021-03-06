﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JqD.Common.Command.SystemManage;
using JqD.Common.Entities;
using JqD.Common.ILogic;
using JqD.Common.IRepository;
using JqD.Common.Models;
using JqD.Data.CodeSection;
using JqD.Data.Logic;
using JqD.Entities.QueryModels;
using JqD.Infrustruct.Enums;
using log4net.Core;

namespace JqD.Common.Logic
{
    public class SystemUserLogic : LogicBase<SystemUser>, ISystemUserLogic
    {
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ICurrentTimeProvider _currentTimeProvider;

        public SystemUserLogic(ISystemUserRepository systemUserRepository,
            ICurrentTimeProvider currentTimeProvider)
            : base(systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
            _currentTimeProvider = currentTimeProvider;
        }

        public void Add(AddUserCommand user)
        {
            if (string.IsNullOrEmpty(user.LoginName) || string.IsNullOrEmpty(user.Password))
            {
                throw new LogException(LogicExceptionMessage.LoginNameOrPasswordIsNull);
            }
            if (!IsNumAndEnCh(user.LoginName)||!IsNumAndEnCh(user.Password))
            {
                throw new LogException(LogicExceptionMessage.LoginNameOrPasswordIsAlphabet);
            }
            if (!IsLengthMoreThanSix(user.LoginName) || !IsLengthMoreThanSix(user.Password))
            {
                throw new LogException(LogicExceptionMessage.LoginNameOrPasswordLengthMoreThanSix);
            }
            var equalName = _systemUserRepository.GetAll().FirstOrDefault(x=>x.LoginName== user.LoginName);
            if (equalName != null)
            {
                throw new LogException(LogicExceptionMessage.TheSameLoginName);
            }
            var info = new SystemUser
            {
                LoginName= user.LoginName,
                Password= user.Password,
                CreateUser= LoginUserSection.CurrentUser.LoginName,
                CreateDate= _currentTimeProvider.CurrentTime()
            };
            _systemUserRepository.Add(info);
        }

        public void Update(UpdateUserCommand user)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new LogException(LogicExceptionMessage.LoginNameOrPasswordIsNull);
            }
            if (!IsNumAndEnCh(user.Password))
            {
                throw new LogException(LogicExceptionMessage.LoginNameOrPasswordIsAlphabet);
            }
            if (!IsLengthMoreThanSix(user.Password))
            {
                throw new LogException(LogicExceptionMessage.LoginNameOrPasswordLengthMoreThanSix);
            }
            var info = _systemUserRepository.Get(user.Id);
            info.Password = user.Password;
            info.EditUser = LoginUserSection.CurrentUser.LoginName;
            info.EditDate = _currentTimeProvider.CurrentTime();
            _systemUserRepository.Update(info);
        }

        private static bool IsNumAndEnCh(string input)
        {
            const string pattern = @"^[A-Za-z0-9]+$";
            var regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        private static bool IsLengthMoreThanSix(string input)
        {
            return input.Length>=6;
        }

        public LoginUserInformation Login(string userName, string password)
        {
            SystemUser systemUser;
            try
            {
                var systemUserGroup = _systemUserRepository.GetAll();
                systemUser = systemUserGroup.Single(x=>x.LoginName== userName);
            }
            catch
            {
                throw new Exception("用户名不存在");
            }
            if (!PasswordHasher.ValidateHash(password, systemUser.Password))
            {
                throw new Exception("登陆密码错误");
            }
            systemUser.LastLoginDate =_currentTimeProvider.CurrentTime();
            systemUser.IsLogin = Enums.IsLogin.Logining;
            _systemUserRepository.Update(systemUser);
            var loginUserInfo = new LoginUserInformation
            {
                SystemUserId= systemUser.Id,
                LoginName= systemUser.LoginName
            };
            return loginUserInfo;
        }

        public void LogOut(string userNo)
        {
            var systemUser = _systemUserRepository.GetAll().Single(x => x.LoginName == userNo);
            systemUser.IsLogin = 0;
            _systemUserRepository.Update(systemUser);
        }

        public LoginUserInformation GetLoginUserInformationById(int sysUserId)
        {
            var systemUser = _systemUserRepository.Get(sysUserId);
            var loginUserInfo =new LoginUserInformation
            {
                LoginName = systemUser.LoginName,
                SystemUserId = systemUser.Id
            };
            return loginUserInfo;
        }

        public IEnumerable<SystemUser> QueryByPage(QuerySystemUserModel queryParams, out int totleCount)
        {
            //var querys = new Dictionary<string, object>
            //{
            //    {"DWBH",filter.SqlBuilder.Where}
            //};
            return _systemUserRepository.QueryByPage(queryParams.StartNo, queryParams.Page* queryParams.Rows, out totleCount);
        }
    }
}