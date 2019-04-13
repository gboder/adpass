using System;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Authentication;
using Moq;

namespace AdTools
{
    public interface IAdConnector
    {
        bool CanAuthenticate(string userName, string password);
        void ChangePassword(string username, string oldPassword, string newPassword);
    }

    public class AdConnectorFactory
    {
        public static AdConnectorFactory Instance => instanceLazy.Value;
        private static readonly Lazy<AdConnectorFactory> instanceLazy = new Lazy<AdConnectorFactory>();

        public IAdConnector Build()
        {
            //return BuildMock();
            return BuildInstance();
        }

        private IAdConnector BuildInstance()
        {
            return new AdConnector();
        }

        private IAdConnector BuildMock()
        {
            var moq = new Mock<IAdConnector>();

            moq.Setup(m => m.CanAuthenticate(It.IsAny<string>(), It.Is<string>(s => s == "123456789"))).Returns(true);
            moq.Setup(m => m.CanAuthenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            return moq.Object;
        }
    }

    public class AdConnector : IAdConnector
    {
        public bool CanAuthenticate(string userName, string password)
        {
            try
            {
                using (var ctx = new PrincipalContext(ContextType.Domain))
                {
                    return ctx.ValidateCredentials(userName, password);
                }
            }
            catch (Exception e)
            {
                // TODO Logging
                return false;
            }
        }

        public void ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                using (var ctx = new PrincipalContext(ContextType.Domain))
                {
                    using (var user = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, username))
                    {
                        user?.ChangePassword(oldPassword, newPassword);
                    }
                }
            }
            catch (Exception e)
            {
                // TODO Logging
				throw;
            }
        }
    }
}
