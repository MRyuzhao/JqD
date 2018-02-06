﻿using JqD.Common.Entities;
using JqD.Common.IRepository;
using JqD.Data;
using JqD.Data.Repository;

namespace JqD.Common.Repository
{
    public class SystemUserRepository : RepositoryBase<SystemUser>, ISystemUserRepository
    {
        private static readonly SqlStrings SystemUserSql = new SqlStrings
        {
            TableName = "SystemUser",
            //Add SQL 中 数据库表的 Status 列默认值=1 （正常）
            Add =
                @"INSERT INTO SystemUser(UserNo,LoginName,Password,LastLoginDate,CreateUser,CreateDate,EditUser,EditDate,IsLogin) 
                VALUES( @UserNo,@LoginName,@Password,@LastLoginDate,@CreateUser,@CreateDate,@EditUser,@EditDate,@IsLogin );",
            Update =
                @"UPDATE [SM_SystemUser] SET UserNo = @UserNo , LoginName = @LoginName,Password=@Password,LastLoginDate=@LastLoginDate,"
                + "CreateUser = @CreateUser , CreateDate = @CreateDate , EditUser = @EditUser , EditDate = @EditDate , IsLogin = @IsLogin WHERE Id=@Id ;",
            //Delete SQL 中更新数据库表的 Status 列=-1 （删除）
            Delete = @"UPDATE [SM_SystemUser] SET Status = -1 WHERE Id = @Id ;",
            QueryAll = @"SELECT * FROM [SM_SystemUser]; ",
            QueryOne = @"SELECT * FROM [SM_SystemUser] WHERE Id = @Id ;",
            QueryByPage =""
        };

        public SystemUserRepository(ISqlDatabaseProxy databaseProxy)
            : base(databaseProxy)
        {
            Sql = SystemUserSql;
        }

        protected sealed override SqlStrings Sql
        {
            get { return base.Sql; }
            set { base.Sql = value; }
        }
    }
}