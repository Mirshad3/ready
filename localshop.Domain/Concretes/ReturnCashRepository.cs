using AutoMapper;
using localshop.Core.Common;
using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using localshop.Domain.Entities;
using MassTransit;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Concretes
{
    public class ReturnCashRepository : IReturnCashRepository
    {
        private IMapper _mapper;
        private ApplicationDbContext _context;

        public ReturnCashRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IList<ReturnCashDTO> ReturnCashs
        {
            get
            {
                return _context.ReturnCashes.AsEnumerable().Select(o => _mapper.Map<ReturnCash, ReturnCashDTO>(o)).ToList();
            }
        }

        public IList<ReturnCashDTO> GetReturnCashs(string userId)
        {
            var ReturnCashs = _context.ReturnCashes.Where(o => o.UserId == userId).AsEnumerable()
                .Select(o => _mapper.Map<ReturnCash, ReturnCashDTO>(o)).ToList();
            return ReturnCashs;
        }
        public IList<ReturnCashDTO> GetReturnCashsByOwner(string ownerId)
        {
            var ReturnCashDetails = _context.ReturnCashes.Where(od => od.Product.UserId == ownerId);
            var ReturnCashs = new List<ReturnCashDTO>();
            foreach (var ReturnCashDetail in ReturnCashDetails)
            {
                ReturnCashs = _context.ReturnCashes.Where(o => o.Id == ReturnCashDetail.Id).AsEnumerable()
                  .Select(o => _mapper.Map<ReturnCash, ReturnCashDTO>(o)).ToList();
            }
            
            return ReturnCashs;
        }
         
        
        public ReturnCashDTO FindById(string orderId)
        {
            var ReturnCash = _context.ReturnCashes.FirstOrDefault(o => o.OrderId == orderId);
            if (ReturnCash == null)
            {
                return null;
            }
            return _mapper.Map<ReturnCash, ReturnCashDTO>(ReturnCash);
        }

        
        public string GetReturnCashStatus(string ReturnCashStatusId)
        {
            var ReturnCashStatus = _context.ReturnCashes.FirstOrDefault(os => os.Id == ReturnCashStatusId);
            if (ReturnCashStatus == null)
            {
                return null;
            }

            return ReturnCashStatus.Status;
        }

        public string GetPaymentMethod(string paymentMethodId)
        {
            var paymentMethod = _context.PaymentMethods.FirstOrDefault(pm => pm.Id == paymentMethodId);
            if (paymentMethod == null)
            {
                return null;
            }

            return paymentMethod.Name;
        }

       

        // For DTO
        public string AddPaymentMethod(ReturnCashDTO ReturnCashDTO, string paymentMethod)
        {
            var method = _context.PaymentMethods.FirstOrDefault(pm => pm.Name == paymentMethod);
            if (paymentMethod == null)
            {
                return null;
            }

             
            return method.Id;
        }

        // For DTO
        public string UpdateStatus(ReturnCashDTO ReturnCashDTO, string statusName)
        {
            var status = _context.ReturnCashes.FirstOrDefault(os => os.Status == statusName);
            if (status == null)
            {
                return null;
            }

            ReturnCashDTO.Status = status.Id;
            return status.Id;
        }

        public string AddPaymentMethod(string ReturnCashId, string paymentMethod)
        {
            var ReturnCash = _context.ReturnCashes.FirstOrDefault(o => o.Id == ReturnCashId);
            if (ReturnCash == null)
            {
                return null;
            }

            var method = _context.PaymentMethods.FirstOrDefault(pm => pm.Name == paymentMethod);
            if (paymentMethod == null)
            {
                return null;
            }

             
            _context.SaveChanges();

            return method.Id;
        }

        public string UpdateStatus(string orderId, string statusName)
        {
            var ReturnCash = _context.ReturnCashes.FirstOrDefault(o => o.OrderId == orderId);
            if (ReturnCash == null)
            {
                return null;
            }
            ReturnCash.Status = statusName;
 
             _context.SaveChanges();

            return ReturnCash.Status;
        }

        public ReturnCashDTO Save(ReturnCashDTO ReturnCashDTO)
        {
            ReturnCashDTO.Id = "#" + string.Join("", NewId.Next().ToString("D").ToUpperInvariant().Split('-'));
            ReturnCashDTO.ReturnDate = DateTime.Now;
            ReturnCashDTO.Status = "Pending";
            //ReturnCashDTO.ProductId = _context.Products.FirstOrDefault(p => p.Id == ReturnCashDTO.ProductId).MetaTitle;
          
            var ReturnCash = _mapper.Map<ReturnCashDTO, ReturnCash>(ReturnCashDTO);

            var checkavailable = _context.ReturnCashes.Where(m => m.OrderId == ReturnCashDTO.OrderId).Count();
            if(checkavailable != 0)
            {
                return ReturnCashDTO;
            }
            try
            {
                _context.ReturnCashes.Add(ReturnCash);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return ReturnCashDTO;
        }
        public string ReturnCashHistory(int waybillid)
        {
            var values = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://application.koombiyodelivery.lk/api/ReturnCashhistory/users");
            httpWebRequest.ReadWriteTimeout = 100000; //this can cause issues which is why we are manually setting this
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Basic ThisShouldbeBase64String"); // "Basic 4dfsdfsfs4sf5ssfsdfs=="
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                // we want to remove new line characters otherwise it will return an error 
                values = "apikey=KwyHFZKKZeqrWyDyEhqr&waybillid=" + waybillid;
                streamWriter.Write(values);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse resp = (HttpWebResponse)httpWebRequest.GetResponse();
                string respStr = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Response : " + respStr); // if you want see the output
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //dynamic jsonObject = serializer.Deserialize<dynamic>(respStr);
                //dynamic x = jsonObject["ReturnCash_history"];
                //var valu = jsonObject["ReturnCash_history"][0]["status_id"];

                return respStr;

            }
            catch (Exception ex)
            {
                throw ex;
                //process exception here   
            }

        }
    }
}
