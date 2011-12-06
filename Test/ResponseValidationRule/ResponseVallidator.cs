using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace Microsoft.Practices.Liike.Test
{
    //-------------------------------------------------------------------------
    // This class creates a custom validation rule named "Custom Validate Tag"
    // The custom validation rule is used to check that an HTML tag with a 
    // particular name is found one or more times in the HTML response.
    // The user of the rule can specify the HTML tag to look for, and the 
    // number of times that it must appear in the response.
    //-------------------------------------------------------------------------
    public class ResponseRule : ValidationRule
    {
        /// Specify a name for use in the user interface.
        /// The user sees this name in the Add Validation dialog box.
        //---------------------------------------------------------------------
        public override string RuleName
        {
            get { return "Total Response Time Rule"; }
        }

        /// Specify a description for use in the user interface.
        /// The user sees this description in the Add Validation dialog box.
        //---------------------------------------------------------------------
        public override string RuleDescription
        {
            get { return "Validates a response Time from a web test request aggregating dependent requests."; }
        }

        // This is the time that takes to get the request plus dependent requests.
         private long TotalResPonseTimeValue;
         public long TotalResponseTime
         {
             get { return TotalResPonseTimeValue; }
             set { TotalResPonseTimeValue = value; }
         }

         private long ThresholdTimeValue;
         public long ThresholdTime
         {
             get { return ThresholdTimeValue; }
             set { ThresholdTimeValue = value; }
         }

    
        public override void Validate(object sender, ValidationEventArgs e)
        {
           
            TotalResPonseTimeValue = TotalResPonseTimeValue + e.Response.Statistics.MillisecondsToLastByte;
         
            if (TotalResPonseTimeValue > ThresholdTimeValue)
            {
                e.IsValid = false;
             //   e.Message = String.Format("Total Request Time {0:00000} Exceeded Threshold {1:00000) ", TotalResPonseTimeValue, ThresholdTimeValue);
                e.Message = "Total Request Time " + TotalResPonseTimeValue + "Exceeded Threshold "+ ThresholdTimeValue;

            }

           
        }
    }
}
