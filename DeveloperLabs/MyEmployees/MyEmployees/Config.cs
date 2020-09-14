using System;
using Newtonsoft.Json;


namespace ExportDataLibrary
{
    public partial class Config
    {
        [JsonProperty("isCheckForUpdatesEnabled")]
        public bool IsCheckForUpdatesEnabled { get; set; }

        [JsonProperty("about")]
        public About About { get; set; }
    }

    public partial class About
    {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("supportLink")]
        public string SupportLink { get; set; }

        [JsonProperty("supportMail")]
        public string SupportMail { get; set; }
    }
}
