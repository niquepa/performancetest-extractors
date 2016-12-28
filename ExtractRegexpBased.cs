using System;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


namespace ExtractRegexpBased
{
    //-------------------------------------------------------------------------  
    // This class creates a custom extraction rule named "Custom Extract Input"  
    // The user of the rule specifies the name of an input field, and the  
    // rule attempts to extract the value of that input field.  
    //-------------------------------------------------------------------------  
    public class ExtractRegexpBased : ExtractionRule
    {
        /// Specify a name for use in the user interface.  
        /// The user sees this name in the Add Extraction dialog box.  
        //---------------------------------------------------------------------  
        public override string RuleName
        {
            get { return "Extract string based on Regexp"; }
        }

        /// Specify a description for use in the user interface.  
        /// The user sees this description in the Add Extraction dialog box.  
        //---------------------------------------------------------------------  
        public override string RuleDescription
        {
            get { return "Extract string based on Regexp"; }
        }

        public string regexp { get; set; }
        public string text { get; set; }
        // The Extract method.  The parameter e contains the web performance test context.  
        //---------------------------------------------------------------------  

        public override void Extract(object sender, ExtractionEventArgs e)
        {

            if (e.WebTest.Context[text] == null)
            {
                e.Success = false;
                throw new WebTestException(string.Format("No header location available"));
            }
            if (regexp == null || regexp == "")    {
                e.Success = false;
                throw new WebTestException(string.Format("Regexp not defined"));
            }
            Regex r = new Regex(regexp);
            Match m = r.Match(e.WebTest.Context[text].ToString());
            //string vOrderId = m.Groups[0].ToString();
            e.WebTest.Context.Add(this.ContextParameterName, m.Groups[0].ToString());
            return;
        }
    }
}
