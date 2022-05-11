using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.StoredProcedure
{
    public class StoredProceduresPaths
    {
        public static string baseValue = "dbo.";

        //users
        public string UsersCreate = baseValue + "UsersCreate";
        public string UsersUpdate = baseValue + "UsersUpdate";
        public string UsersDelete = baseValue + "UsersDelete";
        public string UsersGet = baseValue + "UsersGet";
        public string UsersGetAll = baseValue + "UsersGetAll";
        public string UsersLogin = baseValue + "UsersLogin";
        public string UsersGetRole = baseValue + "UsersGetRole";
        public string UsersChangePassword = baseValue + "UsersChangePassword";

        //AccountType
        public string AccountTypeCreate = baseValue + "AccountTypeCreate";
        public string AccountTypeUpdate = baseValue + "AccountTypeUpdate";
        public string AccountTypeDelete = baseValue + "AccountTypeDelete";
        public string AccountTypeGet = baseValue + "AccountTypeGet";
        public string AccountTypeGetAll = baseValue + "AccountTypeGetAll";

        //UserAccounts
        public string UserAccountsCreate = baseValue + "UserAccountsCreate";
        public string UserAccountsUpdate = baseValue + "UserAccountsUpdate";
        public string UserAccountsDelete = baseValue + "UserAccountsDelete";
        public string UserAccountsGet = baseValue + "UserAccountsGet";
        public string UserAccountsGetAll = baseValue + "UserAccountsGetAll";

        ///Services
        public string ServicesCreate = baseValue + "ServicesCreate";
        public string ServicesUpdate = baseValue + "ServicesUpdate";
        public string ServicesDelete = baseValue + "ServicesDelete";
        public string ServicesGet = baseValue + "ServicesGet";
        public string ServicesGetAll = baseValue + "ServicesGetAll";
        public string ServicesGetAllServicesGetAll = baseValue + "ServicesGetAll";

        //Bookings
        public string BookingsCreate = baseValue + "BookingsCreate";
        public string BookingsUpdate = baseValue + "BookingsUpdate";
        public string BookingsDelete = baseValue + "BookingsDelete";
        public string BookingsGet = baseValue + "BookingsGet";
        public string BookingsGetAll = baseValue + "BookingsGetAll";
        public string BookingsCheckIfFree = baseValue + "BookingsCheckIfFree";
        public string BookingsGetMine = baseValue + "BookingsGetMine";

        //BookingReviews
        public string BookingReviewsGetByServiceId = baseValue + "BookingReviewsGetByServiceId";
        public string BookingReviewsCreate = baseValue + "BookingReviewsCreate";
        public string BookingReviewsUpdate = baseValue + "BookingReviewsUpdate";
        public string BookingReviewsDelete = baseValue + "BookingReviewsDelete";
        public string BookingReviewsGet = baseValue + "BookingReviewsGet";
        public string BookingReviewsGetAll = baseValue + "BookingReviewsGetAll";
    }
}
