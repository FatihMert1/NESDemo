using NilveraDemo.Models;
using static NilveraDemo.Consts.Const;

namespace NilveraDemo.DTOs.Remote.Requests  
{
    public class NESInvoiceSendRequest{

        public NESInvoice NESInvoice { get; set; }
        public InvoiceProfile InvoiceProfile { get; set; }
        public string CustomerRegisterNumber { get; set; }
        public bool IsDirectSend { get; set; }
        
    }
}