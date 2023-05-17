using System;
using System.Linq;
using WebApi.Alpari.Models.Context;
using WebApi.Alpari.Models.Entities;

namespace WebApi.Alpari.Models.Services
{
    public class UserRepository
    {
        private readonly DataBaseContext _context;
        public UserRepository(DataBaseContext context)
        {
            this._context = context;
        }

        public User GetUser(Guid id)
        {

            var user = _context.Users.SingleOrDefault(p=> p.Id == id);
            return user;
        }

        public bool ValidateUser(string username,string Password)
        {
            var user = _context.Users.FirstOrDefault();
            return user != null? true : false;
        }

        public string GetCode(string phoneNumber)
        {
            Random rand = new Random();
            string code = rand.Next(1000,9999).ToString();
            SmsCode smsCode = new SmsCode()
            {
                Code = code,
                ExpireDateTime = DateTime.Now.AddMinutes(1),
                PhoneNumber = phoneNumber,
                RequestCount = 0,
                Used = false
            };
            _context.Add(smsCode);
            _context.SaveChanges();

            return code;
        }


        //public LoginDto Login(string phonenumber,string Code)
        //{
        //    var smsCode = _context.SmsCode.Where(p => p.PhoneNumber == phonenumber && p.Code == Code).FirstOrDefault();
        //    if (smsCode != null)
        //    {
        //        return new LoginDto
        //        {
        //            Message = "Code is not correct",
        //            IsSuccess = false,
        //            user = null
        //        };
        //    }

        //    else
        //    {
        //        smsCode.RequestCount++;
        //        if(smsCode.Used == true)
        //        {
        //            return new LoginDto
        //            {
        //                Message = "Code is not correct",
        //                IsSuccess = false,
        //                user = null
        //            };
        //        }

        //    }
        //}

    }


    public class LoginDto
    {
        public string  Message { get; set; }
        public bool IsSuccess { get; set; }
        public User user { get; set; }
    }
}
