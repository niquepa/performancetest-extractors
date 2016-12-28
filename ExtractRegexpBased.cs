using System;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace ExtractRegexpBased
{

    public class ExtractRegexpBased : ExtractionRule
    {

        public override string RuleName
        {
            get { return "Extract string based on Regexp"; }
        }
        
        public override string RuleDescription
        {
            get { return "Extract string based on Regexp"; }
        }

        public string regexp { get; set; }
        public string text { get; set; }

        public override void Extract(object sender, ExtractionEventArgs e)
        {

            if (e.WebTest.Context[text] == null)
            {
                e.Success = false;
                throw new WebTestException(string.Format("No text available"));
            }
            if (regexp == null || regexp == "")    {
                e.Success = false;
                throw new WebTestException(string.Format("Regexp not defined"));
            }
            Regex r = new Regex(regexp);
            Match m = r.Match(e.WebTest.Context[text].ToString());
            e.WebTest.Context.Add(this.ContextParameterName, m.Groups[0].ToString());
            return;
        }
    }
}
