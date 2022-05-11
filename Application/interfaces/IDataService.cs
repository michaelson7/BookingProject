using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IDataService
    {
        Task<int> AccountTypeCreate(AccountTypeModel model);
        Task AccountTypeDelete(int Id);
        Task<AccountTypeModel> AccountTypeGet(int Id);
        Task<List<AccountTypeModel>> AccountTypeGetAll();
        Task AccountTypeUpdate(AccountTypeModel model);

        Task<int> BookingReviewsCreate(BookingReviewsModel model);
        Task BookingReviewsDelete(int Id);
        Task<BookingReviewsModel> BookingReviewsGet(int Id);
        Task<List<BookingReviewsModel>> BookingReviewsGetAll();
        Task<List<BookingReviewsModel>> BookingReviewsGetByServiceId(int Id);
        Task BookingReviewsUpdate(BookingReviewsModel model);

        Task<int> BookingsCreate(BookingsModel model);
        Task<bool> BookingsCheckIfFree(int serviceId, DateTime bookingDate);
        Task BookingsDelete(int Id);
        Task<BookingsModel> BookingsGet(int Id);
        Task<List<BookingsModel>> BookingsGetAll();
        Task<List<BookingsModel>> BookingsGetMine(int userId);
        Task BookingsUpdate(BookingsModel model);


        Task<int> ServicesCreate(ServicesModel model);
        Task ServicesDelete(int Id);
        Task<ServicesModel> ServicesGet(int Id);
        Task<List<ServicesModel>> ServicesGetAll();
        Task ServicesUpdate(ServicesModel model);


        Task<int> UserAccountCreate(UserAccountModel model);
        Task UserAccountDelete(int Id);
        Task<UserAccountModel> UserAccountGet(int Id);
        Task<List<UserAccountModel>> UserAccountGetAll();
        Task UserAccountUpdate(UserAccountModel model);

        Task UsersChangePassword(int Id, string newPassword);
        Task<int> UsersCreate(UsersModel model);
        Task UsersDelete(int Id);
        Task<UsersModel> UsersGet(int Id);
        Task<List<UsersModel>> UsersGetAll();
        Task<UsersModel> UsersLogin(string email, string password);
        Task UsersUpdate(UsersModel model);
    }
}
