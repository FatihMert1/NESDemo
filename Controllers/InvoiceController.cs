using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NilveraDemo.Db;
using NilveraDemo.DTOs.Local.Responses;
using NilveraDemo.DTOs.Remote.Requests;
using NilveraDemo.DTOs.Remote.Responses;
using NilveraDemo.Helpers;
using NilveraDemo.Models;

namespace NilveraDemo.Controllers
{
    [ApiController]
    [Route("invoice")]
    public class InvoiceController : ControllerBase{

        private readonly IConfiguration _configuration;
        public InvoiceController(IConfiguration configuration){
            _configuration = configuration;
        }

        [HttpPost("insert")]
        public async Task<ApiResponse<NESInvoice>> Insert(NESInvoice nesInvoice) {

            var isValidNesVoice = Validator.IsValidNESInvoice(nesInvoice);

            if(!isValidNesVoice.Key)
                return ResponseHelper.CreateApiResponse<NESInvoice>(null,isValidNesVoice.Value,400,true);

                var connector = new DbConnector(_configuration.GetConnectionString("NilveraDb"));
                
                await connector.Insert<PartyInfo>("insert into PartyInfo(register_number,name,address,district,city,country) values(@RegisterNumber,"
                + "@Name,@Address,@District,@City,@Country)", new[] { nesInvoice.CustomerInfo });
                
                await connector.Insert<PartyInfo>("insert into PartyInfo(register_number,name,address,district,city,country) values(@RegisterNumber,"
                + "@Name,@Address,@District,@City,@Country)", new[] { nesInvoice.CompanyInfo });

                await connector.Insert<InvoiceInfo>("insert into InvoiceInfo(uuid,invoice_seri_or_number) values(@UUID,@InvoiceSerieOrNumber)",
                        new []{nesInvoice.InvoiceInfo});
                
                if(nesInvoice.ExportCustomerInfo != null)
                    await connector.Insert<ExportCustomerInfo>("insert into ExportCustomerInfo(party_name,person_name) values(@PartyName,@PersonName)",
                        new []{nesInvoice.ExportCustomerInfo});

                await connector.Insert<object>("insert into NESInvoice(id,invoice_info_id,ise_archive_invoice,customer_info_id,company_info_id) values(@Id,@InvoiceInfoId," +
                    "@IseArchive,@CustomerInfoId,@CompanyInfoId)",new []{
                        new {Id=Guid.NewGuid().ToString(), InvoiceInfoId=nesInvoice.InvoiceInfo.UUID, IseArchive=nesInvoice.ISEArchiveInvoice,
                            CustomerInfoId=nesInvoice.CustomerInfo.RegisterNumber, CompanyInfoId=nesInvoice.CompanyInfo.RegisterNumber}
                });

            return ResponseHelper.CreateApiResponse<NESInvoice>(default(NESInvoice),"Success",201,false);
        }

        [HttpPost("send")]
        public async Task<ApiResponse<SendInvoiceResponse>> Send(NESInvoiceSendRequest request){
            string auth=GetHeaderTitle("authorization");
            if(string.IsNullOrEmpty(auth))
                return ResponseHelper.CreateApiResponse<SendInvoiceResponse>(null,"Authorization Header Must Have",400,true);
            
            var headers = new Dictionary<string, string>();
            headers.Add("content-type","application/json");
            headers.Add("authorization",auth);

            var parameters = ClientHelper.AddParameter(request);

            var remoteRequestModel = new RemoteApiRequest{ Method = RestSharp.Method.POST, Route= "invoicegeneral/sendNESInvoice", Headers=headers, Parameters=parameters };

            return await ClientHelper.SendRequest<SendInvoiceResponse>(remoteRequestModel);
        }

        [HttpGet("consume/xml/{uuid}")]
        public async Task<ApiResponse<string>> ConsumeXml(string uuid){
            string auth = GetHeaderTitle("authorization");
            if(string.IsNullOrEmpty(auth))
                return ResponseHelper.CreateApiResponse<string>("","Authorization Denied!",400,true);
            
            var headers = new Dictionary<string, string>();
            headers.Add("content-type","application/json");
            headers.Add("authorization",$"bearer {auth}");

            var remoteRequestModel = new RemoteApiRequest{Method=RestSharp.Method.GET,Route=$"invoicegeneral/ublXmlContent/{uuid}", Headers=headers,Parameters=null};
            var response =  await ClientHelper.SendRequestForContent<string>(remoteRequestModel);
            return response;
        }

        private string GetHeaderTitle(string headerName){
            StringValues val;
            HttpContext.Request.Headers.TryGetValue(headerName,out val);
            return val;
        }
    }
}
