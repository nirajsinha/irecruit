using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iRecruit.Extensions
{
    public class AntiXssSanitizer
    {
        public static void HtmlEncodeAllStringProperties<T>(T objectToSanitize) where T : class
        {
            //we CANNOT TRUST anything entered by the user, WE NEED TO ENCODE ALL STRING DATA using the Microsoft.Security.Application namespace
            //methods, to do this we find all the properties on this object that are strings and can be written to and we encode the values
 
            foreach (var x in (typeof(T)).GetProperties())
            {
                //check to see if the property has a Get on it (ie. it can be read)
                if (x.CanRead)
                {
                    var MatchingItemProperty = typeof(T).GetProperty(x.Name);
 
                    if (MatchingItemProperty != null && MatchingItemProperty.CanWrite)
                    {
                        //convert the input type into the value type your saving to
                        //in order to avoid runtime Type Mismatch errors
                        var ObjectPropertyValue = x.GetValue(objectToSanitize, null);
 
                        var p = typeof(T).GetProperty(MatchingItemProperty.Name);
                        var type = p.PropertyType;
 
                        if (type == typeof(String))
                        {
                            //use the Microsoft AntiXss library to HTML encode the string values
                            //var SanitizedObject = Microsoft.Security.Application.Encoder.HtmlEncode(ObjectPropertyValue.ToString());
                            var SanitizedObject = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(ObjectPropertyValue.ToString(),true);
                            //you have to convert it to the type of the property you are setting to avoid runtime errors
                            var ConvertedProperty = Convert.ChangeType(SanitizedObject, type);
                            MatchingItemProperty.SetValue(objectToSanitize, ConvertedProperty, null);
 
                        }
                    }
                }
            }
        }
        public static void HtmlEncode(string stringToSanitize)
        {
            var SanitizedObject = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(stringToSanitize, true);
        }
    }
    
}