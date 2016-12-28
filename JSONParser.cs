using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System.Globalization;
using System.Web.Script.Serialization;

namespace JSONParser
{
    public class JSONParser : ExtractionRule
    {
        public override string RuleName
        {
            get { return "Parse JSON response"; }
        }

        public override string RuleDescription
        {
            get { return "Parse JSON response, if 'position' is defined returns only that element"; }
        }

        public override void Extract(object sender, ExtractionEventArgs e)
        {
            if (e.Response.BodyString == null)
            {
                e.Success = false;
                throw new WebTestException(string.Format("No JSON to Parse"));
            }

            var jsonSerialization = new JavaScriptSerializer();

            IDictionary<string, dynamic> dictObj = jsonSerialization.Deserialize<Dictionary<string, dynamic>>(e.Response.BodyString);
            e.WebTest.Context.Add(this.ContextParameterName, dictObj);
            return;
        }
    }
}
