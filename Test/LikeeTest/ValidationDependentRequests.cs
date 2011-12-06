using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace LikeeTest
{
    public class PageSize : WebTest
    {

        public PageSize()
        {
            this.PreAuthenticate = true;
        }

        public int DependentResponsesBytes { get; set; }

        public long ResponseBytesLength { get; set; }
        public long ResponseBytesLengthMaximum { get; set; }

        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            if ((this.Context.ValidationLevel >= Microsoft.VisualStudio.TestTools.WebTesting.ValidationLevel.Low))
            {
                ValidationResponseRuleNumberOfBytes validationRule3 = new ValidationResponseRuleNumberOfBytes();
                this.ValidateResponseOnPageComplete += new EventHandler<ValidationEventArgs>(validationRule3.Validate);
            }

            WebTestRequest request1 = new WebTestRequest("http://artequalswork.com/");
            request1.ThinkTime = 2;
            DependentResponsesBytes = 0;
            ResponseBytesLengthMaximum = 300000;
            request1.PostRequest += new EventHandler<PostRequestEventArgs>(SetValidationEventsForDependentResponses);
            yield return request1;

            //request1 = new WebTestRequest("http://localhost/welcome.aspx");
            //request1.ThinkTime = 2;
            //DependentResponsesBytes = 0;
            //ResponseBytesLengthMaximum = 400000;
            //request1.PostRequest += new EventHandler<PostRequestEventArgs>(SetValidationEventsForDependentResponses);
            //yield return request1;

            request1 = null;
        }

        void SetValidationEventsForDependentResponses(object sender, PostRequestEventArgs e)
        {
            foreach (WebTestRequest depententRequest in e.Request.DependentRequests)
            {
                depententRequest.ValidateResponse += new EventHandler<ValidationEventArgs>(depententRequest_ValidateResponse);
            }
        }

        void depententRequest_ValidateResponse(object sender, ValidationEventArgs e)
        {
            DependentResponsesBytes += e.Response.BodyBytes.Length;
        }
    }

    public class ValidationResponseRuleNumberOfBytes : ValidationRule
    {

        public override string RuleName
        {
            get { return "Validate Page Size"; }
        }

        public override string RuleDescription
        {
            get { return string.Format("Validates that the page is less then some number of bytes set in WebTest."); }
        }

        public override void Validate(object sender, ValidationEventArgs e)
        {
            if (e.Response.HtmlDocument != null)
            {
                int totalBytes = e.Response.BodyBytes.Length + ((PageSize)e.WebTest).DependentResponsesBytes;
                e.Message = string.Format("Page length is {0}", totalBytes);
                if (totalBytes < ((PageSize)e.WebTest).ResponseBytesLengthMaximum)
                {
                    e.IsValid = true;
                    e.Message = "Passed";
                }
                else
                {
                    e.IsValid = false;
                    e.Message = "Failed";
                }
                e.Message += string.Format(". Page length is {0} bytes. Maximum is {1}", totalBytes, ((PageSize)e.WebTest).ResponseBytesLengthMaximum);
            }
        }
    } 

}
