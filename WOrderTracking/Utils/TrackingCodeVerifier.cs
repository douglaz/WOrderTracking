using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WOrderTracking.Utils
{
    public static class TrackingCodeValidator
    {
        private static readonly string TrackingCodeRegexPattern = @"^[a-zA-Z]{2}[\d]{9}[a-zA-Z]{2}$";
        private static readonly Regex TrackingCodeRegexVerifier = new Regex(TrackingCodeRegexPattern);

        public static bool ValidateTrackingCode(string trackingCode)
        {
            return TrackingCodeRegexVerifier.IsMatch(trackingCode);
        }
    }
}
