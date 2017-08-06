using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheBelt
{
    public class DownloadAdapter : BaseAdapter
    {
        [Input(false, "url to download")]
        public string Url { get; set; }

        [Input(true, "username (optional)")]
        public string User { get; set; }
        [Input(true, "password (optional)")]
        public string Password { get; set; }

        [Output("directory that contains download result")]
        public string Output { get; private set; }

        private HttpClientHandler handler;
        private HttpClient client;

        private Task<HttpResponseMessage> getTask;

        public override bool Finished => getTask.IsCompleted;

        public DownloadAdapter()
        {
            handler = new HttpClientHandler();
            client = new HttpClient(handler);
        }

        public override Task Start()
        {
            if(string.IsNullOrWhiteSpace(Url))
            {
                throw new InvalidOperationException("you need to set the url first!");
            }

            handler.Credentials = new NetworkCredential(User, Password);
            getTask = client.GetAsync(Url);
            return getTask;
        }

        public override async Task<string> GetResult()
        {
            if(!string.IsNullOrWhiteSpace(Output))
            {
                return Output;
            }
            if (!Finished)
            {
                throw new InvalidOperationException("the operation is not finished yet!");
            }

            var response = await getTask.Result.Content.ReadAsByteArrayAsync();
            var tmpPath = Path.GetTempFileName();
            File.WriteAllBytes(tmpPath, response);
            Output = tmpPath;
            return tmpPath;
        }
    }

    public class InputAttribute : Attribute
    {
        public bool IsOptional { get; }
        public string Description { get; }

        public InputAttribute(bool optional, string description = "")
        {
            IsOptional = optional;
            Description = description;
        }
    }

    public class OutputAttribute : Attribute
    {
        public string Description { get; }
        public OutputAttribute(string description)
        {
            Description = description;
        }
    }
}
