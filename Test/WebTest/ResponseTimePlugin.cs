using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.WebTesting;


namespace Microsoft.Practices.Liike.Test
{
    public class ResponsePlugin : WebTestPlugin
    {
        static ResponseRule rule = new ResponseRule();

        public override void PreRequest(object sender, PreRequestEventArgs e)
        {
            string stllimit = (string)e.WebTest.Context.Single(keyvp => keyvp.Key == "ThresholdLlmit").Value;
            if (e.WebTest.Context.Keys.Contains("ThresholdLlmit"))
                rule.ThresholdTime = (long.Parse((string)e.WebTest.Context.Single(keyvp => keyvp.Key == "ThresholdLlmit").Value));
            else
                rule.ThresholdTime = 4000;
                            
            if(rule.TotalResponseTime != 0) 
                rule.TotalResponseTime = 0;
            base.PreRequest(sender, e);
            e.Request.ValidateResponse += new EventHandler<ValidationEventArgs>(rule.Validate);
        }

        public override void PostRequest(object sender, PostRequestEventArgs e)
        {
            foreach (WebTestRequest depententRequest in e.Request.DependentRequests)
            {
                depententRequest.ValidateResponse += new EventHandler<ValidationEventArgs>(rule.Validate);
            }
        }

    }
}
