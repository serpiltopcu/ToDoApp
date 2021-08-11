using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using Couchbase.N1QL;
using System;
using System.Linq;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Models.User;
using ToDoApp.Persistence.Data;

namespace ToDoApp.Application.Services
{
    public  class UserService :IUserService
    {
        private readonly IBucket _bucket;

        public UserService(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("Users", "478961");
        }

        public RegisterResponse Register(RegisterRequest arg)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                var request = new QueryRequest("select Users.* from Users where Email == '" + arg.Email + "'").UseStreaming(true);

                var userData = _bucket.Query<User>(request);

                if (userData.Rows!=null && userData.Rows.Count>0)
                {
                    response.Status = false;
                    response.Message = "This email is registered in the system!";
                }
                else
                {
                    string id = Guid.NewGuid().ToString();

                string str=  "insert into Users(KEY, VALUE) values('" +
                        id+
                        "',{ " +
                        "'Id':'"+id+"', " +
                        "'Name':'"+arg.Name+"', " +
                        "'Email':'" +arg.Email+"', " +
                        "'Password':'"+ Helper.GetMd5Hash(arg.Password) + "', " +
                        "'CreatedDate':'" + DateTime.Now.ToString("s") +"'"+
                        "})";

                    var query = _bucket.Query<User>(str);

                    response.Status = query.Success;
                    response.Id = id;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public LoginResponse Login(LoginRequest arg)
        {
            LoginResponse response = new LoginResponse();

            try
            {
                var userData = _bucket.Query<User>("select Users.* from default:Users where Email == '" + arg.Email + "'");


                if (userData.Rows.Count == 0)
                {
                    response.Status = false;
                    response.Message = "This email is not found!";
                }
                else if (userData.Rows[0].Password != Helper.GetMd5Hash(arg.Password))
                {
                    response.Status = false;
                    response.Message = "This password is wrong!";
                }
                else
                {
                    response.Status = true;
                    response.Id = userData.Rows[0].Id;
                    response.Name = userData.Rows[0].Name;
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public UserListResponse GetList()
        {
            UserListResponse response = new UserListResponse();

            var userData = _bucket.Query<User>("select Users.* from default:Users");

            if (userData.Count() > 0)
            {
                response.List = userData.Rows.Select(a => new TodoUserInfo()
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();
            }
            response.Status = true;
            return response;
        }
    }
}
