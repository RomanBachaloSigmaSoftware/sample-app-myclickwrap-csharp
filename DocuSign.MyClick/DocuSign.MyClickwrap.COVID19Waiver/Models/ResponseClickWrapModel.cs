using DocuSign.MyClickwrap.COVID19Waiver.Domain;

namespace DocuSign.MyClickwrap.COVID19Waiver.Models
{
    public class ResponseClickWrapModel
    {
        public string DocuSignBaseUrl { get; set; }

        public ClickWrap ClickWrap { get; set; }

        public string AccountId { get; set; }

        public string UserId { get; set; }
    }
}