using Application.interfaces;
using Domain.models;
using Infrustructure.StoredProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrustructure.DataAccess
{
    public class DataService : IDataService
    {
        private readonly IDataAccess _db;
        private const string connectionStringName = "SqlDb";
        public StoredProceduresPaths _sp = new StoredProceduresPaths();

        public DataService(IDataAccess db)
        {
            _db = db;
        }

        //AccountType//
        public async Task<int> AccountTypeCreate(AccountTypeModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.AccountTypeCreate,
                                             new
                                             {
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task AccountTypeUpdate(AccountTypeModel model)
        {
            await _db.SaveDataAsync(_sp.AccountTypeUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Title = model.Title
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task AccountTypeDelete(int Id)
        {
            await _db.SaveDataAsync(_sp.AccountTypeDelete,
                                              new
                                              {
                                                  Id = Id
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task<AccountTypeModel> AccountTypeGet(int Id)
        {
            var list = await _db.LoadDataAsync<AccountTypeModel, dynamic>(_sp.AccountTypeGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);

            return list.FirstOrDefault();
        }

        public async Task<List<AccountTypeModel>> AccountTypeGetAll()
        {
            var output = await _db.LoadDataAsync<AccountTypeModel, dynamic>(_sp.AccountTypeGetAll,
                                                   new
                                                   { },
                                                   connectionStringName,
                                                   true);


            return output;
        }


        //USERS//
        public async Task<int> UsersCreate(UsersModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.UsersCreate,
                                             new
                                             {
                                                 Names = model.Names,
                                                 Email = model.Email,
                                                 Password = model.Password,
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task UsersUpdate(UsersModel model)
        {
            await _db.SaveDataAsync(_sp.UsersUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Names = model.Names,
                                                 Email = model.Email,
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task UsersDelete(int Id)
        {
            await _db.SaveDataAsync(_sp.UsersDelete,
                                              new
                                              {
                                                  Id = Id
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task<UsersModel> UsersGet(int Id)
        {
            UsersModel output = new UsersModel();
            var list = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            output = list.FirstOrDefault();
            return output;
        }

        public async Task<List<UsersModel>> UsersGetAll()
        {
            List<UsersModel> output = new List<UsersModel>();
            output = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            return output;
        }

        public async Task UsersChangePassword(int Id, string newPassword)
        {
            await _db.SaveDataAsync(_sp.UsersChangePassword,
                                            new
                                            {
                                                Id = Id,
                                                Password = newPassword
                                            },
                                            connectionStringName,
                                            true);
        }

        public async Task<UsersModel> UsersLogin(string email, string password)
        {
            UsersModel output = new UsersModel();
            var list = await _db.LoadDataAsync<UsersModel, dynamic>(_sp.UsersLogin,
                                                 new
                                                 {
                                                     Email = email,
                                                     Password = password
                                                 },
                                                 connectionStringName,
                                                 true);
            output = list.FirstOrDefault();
            return output;
        }


        //Services
        public async Task<int> ServicesCreate(ServicesModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.ServicesCreate,
                                             new
                                             {
                                                 Title = model.Title,
                                                 Cost = model.Cost,
                                                 ImagePath = model.ImagePath,
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task ServicesUpdate(ServicesModel model)
        {
            await _db.SaveDataAsync(_sp.ServicesUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 Title = model.Title,
                                                 Cost = model.Cost,
                                                 ImagePath = model.ImagePath,
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task ServicesDelete(int Id)
        {
            await _db.SaveDataAsync(_sp.ServicesDelete,
                                              new
                                              {
                                                  Id = Id
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task<ServicesModel> ServicesGet(int Id)
        {
            ServicesModel output = new ServicesModel();
            var list = await _db.LoadDataAsync<ServicesModel, dynamic>(_sp.ServicesGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);
            output = list.FirstOrDefault();
            if (output != null)
            {
                output.bookingReviewsModel = await BookingReviewsGetByServiceId(output.Id);
            }
            return output;
        }

        public async Task<List<ServicesModel>> ServicesGetAll()
        {
            List<ServicesModel> output = new List<ServicesModel>();
            output = await _db.LoadDataAsync<ServicesModel, dynamic>(_sp.ServicesGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);


            if (output != null)
            {
                foreach (var data in output)
                {
                    data.bookingReviewsModel = await BookingReviewsGetByServiceId(data.Id);
                }
            }
            return output;
        }


        //Bookings
        public async Task<int> BookingsCreate(BookingsModel model)
        {
            int output = 0;

            output = await _db.SaveDataAsync(_sp.BookingsCreate,
                                             new
                                             {
                                                 UserId = model.UserId,
                                                 ServiceId = model.ServiceId,
                                                 BookingDate = model.BookingDate,
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task<bool> BookingsCheckIfFree(int serviceId, DateTime bookingDate)
        {
            var list = await _db.LoadDataAsync<BookingsModel, dynamic>(_sp.BookingsCheckIfFree,
                                              new
                                              {
                                                  serviceId = serviceId,
                                                  bookingDate = bookingDate

                                              },
                                              connectionStringName,
                                              true);

            if (list.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task BookingsUpdate(BookingsModel model)
        {
            await _db.SaveDataAsync(_sp.BookingsUpdate,
                                             new
                                             {
                                                 UserId = model.UserId,
                                                 Id = model.Id,
                                                 ServiceId = model.ServiceId,
                                                 BookingDate = model.BookingDate,
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task BookingsDelete(int Id)
        {
            await _db.SaveDataAsync(_sp.BookingsDelete,
                                              new
                                              {
                                                  Id = Id
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task<BookingsModel> BookingsGet(int Id)
        {
            BookingsModel output = new BookingsModel();
            var list = await _db.LoadDataAsync<BookingsModel, dynamic>(_sp.BookingsGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);

            output = list.FirstOrDefault();
            if (output != null)
            {
                output.UsersModel = await UsersGet(output.UserId);
                output.ServiceModel = await ServicesGet(output.ServiceId);
            }
            return output;
        }

        public async Task<List<BookingsModel>> BookingsGetAll()
        {
            List<BookingsModel> output = new List<BookingsModel>();
            output = await _db.LoadDataAsync<BookingsModel, dynamic>(_sp.BookingsGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            if (output != null)
            {
                foreach (var data in output)
                {
                    data.UsersModel = await UsersGet(data.UserId);
                    data.ServiceModel = await ServicesGet(data.ServiceId);
                }
            }
            return output;
        }

        public async Task<List<BookingsModel>> BookingsGetMine(int userId)
        {
            List<BookingsModel> output = new List<BookingsModel>();
            output = await _db.LoadDataAsync<BookingsModel, dynamic>(_sp.BookingsGetMine,
                                                 new
                                                 { userId = userId },
                                                 connectionStringName,
                                                 true);
            if (output != null)
            {
                foreach (var data in output)
                {
                    data.UsersModel = await UsersGet(data.UserId);
                    data.ServiceModel = await ServicesGet(data.ServiceId);
                }
            }
            return output;
        }


        //UserAccount
        public async Task<int> UserAccountCreate(UserAccountModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.UserAccountsCreate,
                                             new
                                             {
                                                 UserId = model.AccountId,
                                                 ServiceId = model.Userid,
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task UserAccountUpdate(UserAccountModel model)
        {
            await _db.SaveDataAsync(_sp.UserAccountsUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 UserId = model.AccountId,
                                                 ServiceId = model.Userid,
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task UserAccountDelete(int Id)
        {
            await _db.SaveDataAsync(_sp.UserAccountsDelete,
                                              new
                                              {
                                                  Id = Id
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task<UserAccountModel> UserAccountGet(int Id)
        {
            UserAccountModel output = new UserAccountModel();
            var list = await _db.LoadDataAsync<UserAccountModel, dynamic>(_sp.UserAccountsGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);

            output = list.FirstOrDefault();
            if (output != null)
            {
                output.UsersModel = await UsersGet(output.Userid);
                output.accountTypeModel = await AccountTypeGet(output.AccountId);
            }
            return output;
        }

        public async Task<List<UserAccountModel>> UserAccountGetAll()
        {
            List<UserAccountModel> output = new List<UserAccountModel>();
            output = await _db.LoadDataAsync<UserAccountModel, dynamic>(_sp.UserAccountsGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            if (output != null)
            {
                foreach (var data in output)
                {
                    data.UsersModel = await UsersGet(data.Userid);
                    data.accountTypeModel = await AccountTypeGet(data.AccountId);
                }
            }
            return output;
        }


        //Bookings
        public async Task<int> BookingReviewsCreate(BookingReviewsModel model)
        {
            int output = 0;
            output = await _db.SaveDataAsync(_sp.BookingReviewsCreate,
                                             new
                                             {
                                                 UserId = model.UserId,
                                                 Reviews = model.Reviews,
                                                 ServiceId = model.ServiceId,
                                             },
                                             connectionStringName,
                                             true);
            return output;
        }

        public async Task BookingReviewsUpdate(BookingReviewsModel model)
        {
            await _db.SaveDataAsync(_sp.BookingReviewsUpdate,
                                             new
                                             {
                                                 Id = model.Id,
                                                 UserId = model.UserId,
                                                 Reviews = model.Reviews,
                                             },
                                             connectionStringName,
                                             true);
        }

        public async Task BookingReviewsDelete(int Id)
        {
            await _db.SaveDataAsync(_sp.BookingReviewsDelete,
                                              new
                                              {
                                                  Id = Id
                                              },
                                              connectionStringName,
                                              true);
        }

        public async Task<BookingReviewsModel> BookingReviewsGet(int Id)
        {
            BookingReviewsModel output = new BookingReviewsModel();
            var list = await _db.LoadDataAsync<BookingReviewsModel, dynamic>(_sp.BookingReviewsGet,
                                                 new
                                                 { Id = Id },
                                                 connectionStringName,
                                                 true);

            output = list.FirstOrDefault();
            if (output != null)
            {
                output.usersModel = await UsersGet(output.UserId);
                //output.BookingsModel = await BookingsGet(output.BookingId);
            }
            return output;
        }

        public async Task<List<BookingReviewsModel>> BookingReviewsGetAll()
        {
            List<BookingReviewsModel> output = new List<BookingReviewsModel>();
            output = await _db.LoadDataAsync<BookingReviewsModel, dynamic>(_sp.BookingReviewsGetAll,
                                                 new
                                                 { },
                                                 connectionStringName,
                                                 true);
            if (output != null)
            {
                foreach (var data in output)
                {
                    data.usersModel = await UsersGet(data.UserId);
                    //data.BookingsModel = await BookingsGet(data.BookingId);
                }
            }
            return output;
        }
        public async Task<List<BookingReviewsModel>> BookingReviewsGetByServiceId(int Id)
        {
            List<BookingReviewsModel> output = new List<BookingReviewsModel>();
            output = await _db.LoadDataAsync<BookingReviewsModel, dynamic>(_sp.BookingReviewsGetByServiceId,
                                                 new
                                                 { serviceId = Id },
                                                 connectionStringName,
                                                 true);
            if (output != null)
            {
                foreach (var data in output)
                {
                    data.usersModel = await UsersGet(data.UserId);
                    //data.ServicesModel = await ServicesGet(data.BookingId);
                }
            }
            return output;
        }
    }
}
