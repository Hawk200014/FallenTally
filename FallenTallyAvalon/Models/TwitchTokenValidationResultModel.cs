using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallenTallyAvalon.Models
{
    public class TwitchTokenValidationResultModel
    {
        public string? client_id { get; set; }
        public string? login { get; set; }
        public string[]? scopes { get; set; }
        public string? user_id { get; set; }
        public int? expires_in { get; set; }
        public int? status { get; set; }
        public string? message { get; set; }
    }
}
