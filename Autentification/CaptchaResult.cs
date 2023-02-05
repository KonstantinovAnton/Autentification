using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autentification
{
    class CaptchaResult
    {
        public string captchaCode { get; set; }
        public byte[] captchaByteCode { get; set; }
        public string captchaString64 => Convert.ToBase64String(captchaByteCode);
        public DateTime Timestamp { get; set; }
    }
}
