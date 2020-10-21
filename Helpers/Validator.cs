using System.Collections.Generic;
using NilveraDemo.Models;

namespace NilveraDemo.Helpers{

    public class Validator{

        public static KeyValuePair<bool,string> IsValidNESInvoice(NESInvoice nesInvoice){
            KeyValuePair<bool,string> pair; 
            if(nesInvoice.CompanyInfo == null ){
                pair = FillToPairs(false,"CompanyInfo");
            } else if(nesInvoice.CustomerInfo == null){
                pair = FillToPairs(false,"CustomerInfo");
            }else if(nesInvoice.InvoiceInfo == null){
                pair = FillToPairs(false,"InvoiceInfo");
            }else if(nesInvoice.InvoiceLines == null ){
                pair = FillToPairs(false,"InvoiceLines");
            }else{
                pair = FillToPairs(true,"");
            }
            return pair;
        }

        private static KeyValuePair<bool,string> FillToPairs(bool key, string property){
            return new KeyValuePair<bool,string>(key,$"{property} Can Not Be Null");
        }
    }
}