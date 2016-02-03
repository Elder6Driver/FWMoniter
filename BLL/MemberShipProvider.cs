using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWSync.Model;

namespace FWSync.BLL
{
    public class MemberShipProvider
        :System.Web.Security.MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser CreateUser(
            string username, 
            string password, 
            string email, 
            string passwordQuestion, 
            string passwordAnswer, 
            bool isApproved, 
            object providerUserKey, 
            out System.Web.Security.MembershipCreateStatus status)
        {

            if (string.IsNullOrEmpty(username))
            {
                status = System.Web.Security.MembershipCreateStatus.InvalidUserName;
                return null;
            }


            FWSync.IDAL.IUser dal = FWSync.DALFactory.DataAccess.CreateUser();

            bool isexist = dal.ValidateUserExist(username);

            if (isexist)
            {
                status = System.Web.Security.MembershipCreateStatus.DuplicateUserName;
                return null;
            }



            UserInfo us = new UserInfo();

            us.UserName = username;

            //这里要用到md5加密
            us.PassWord = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password,"MD5");
            //us.PassWord = password;

            int userid = dal.InsertUser(us);

            status = System.Web.Security.MembershipCreateStatus.Success;

            System.Web.Security.MembershipUser user
                = new System.Web.Security.MembershipUser(
                    "MyMemberShip",
                    username,
                    userid,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    true,
                    false,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now
                    );

            return user;

        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            FWSync.IDAL.IUser dal = FWSync.DALFactory.DataAccess.CreateUser();
            string md5pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            //由于数据库里面的用户名密码也是MD5加密过了，因此这里验证的时候传值应该也是传输加密的值

            return dal.ValidateUser(username, md5pwd);
            
        }
    }
}
