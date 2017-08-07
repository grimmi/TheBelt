using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheBelt;

namespace XmlToJsonAdapter
{
    public class XmlToJsonAdapter : BaseAdapter
    {
        public override ResultType ResultType => ResultType.File;

        [Input(false, "xml", "the input xml file")]
        public string XmlFile { get; set; }
        
        [Input(true, "json", "the json file")]
        [Output("json", "resulting json file")]
        public string JsonFile { get; set; }

        public override Task<string> GetResult()
        {
            if(!Finished)
            {
                throw new InvalidOperationException("operation is not finished!");
            }
            return Task.FromResult(JsonFile);
        }

        public override Task Start()
        {
            return Task.Run(() =>
            {
                var jsonResult = JsonConvert.SerializeXNode(XDocument.Load(XmlFile));
                if(string.IsNullOrWhiteSpace(JsonFile))
                {
                    JsonFile = Path.GetTempFileName();
                }
                File.WriteAllText(JsonFile, jsonResult);
                Finished = true;
            });
        }
    }
}
