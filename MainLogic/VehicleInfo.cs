using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLogic
{
    public class VehicleInfo
    {
        public string OKPOCode { get; set; }
        public string carrierName { get; set; }
        public string licStatus { get; set; }
        public string licIssueDate { get; set; }
        public string licStartDate { get; set; }
        public string licEndDate { get; set; }
        public string vhclNum { get; set; }
        public string vhclType { get; set; }
        public string vhclStatus { get; set; }
        public string vhclVendorID { get; set; }
        public string vhclModel { get; set; }
        public string vhclWt { get; set; }
        public string loadCap { get; set; }
        public string vchlManufYear { get; set; }
        public string vchlNumSeats { get; set; }
        public string vchlVIN { get; set; }
        public string certTypeID { get; set; }
        public string vhclSerie { get; set; }
        public string docNum { get; set; }
        public string certSeries { get; set; }
        public string certNum { get; set; }
        public string certDateFrom { get; set; }
        public string certDateTo { get; set; }
        public string taxMark { get; set; }
        public string taxType { get; set; }
        public string taxSeries { get; set; }


        public string GetSomething()
        {
            return OKPOCode;
        }
        private string checkFieldValue(string fieldName, string fieldValue)
        {
            if (fieldValue == null)
            {
                return "";
            }

            return fieldName + ": " + fieldValue + "\n";
        }

        public override string ToString()
        {
            return checkFieldValue("OKPO code", OKPOCode)
                + checkFieldValue("carrierName", carrierName)
                + checkFieldValue("licStatus", licStatus)
                + checkFieldValue("licIssueDate", licIssueDate)
                + checkFieldValue("licStartDate", licStartDate)
                + checkFieldValue("licEndDate", licEndDate)
                + checkFieldValue("vhclNum", vhclNum)
                + checkFieldValue("vhclType", vhclType)
                + checkFieldValue("vhclStatus", vhclStatus)
                + checkFieldValue("vhclVendorID", vhclVendorID)
                + checkFieldValue("licStartDate", licStartDate)
                + checkFieldValue("vhclVendorID", vhclVendorID)
                + checkFieldValue("vhclModel", vhclModel)
                + checkFieldValue("vhclWt", vhclWt)
                + checkFieldValue("loadCap", loadCap)
                + checkFieldValue("vchlManufYear", vchlManufYear)
                + checkFieldValue("vchlNumSeats", vchlNumSeats)
                + checkFieldValue("vchlVIN", vchlVIN)
                + checkFieldValue("certTypeID", certTypeID)
                + checkFieldValue("vhclSerie", vhclSerie)
                + checkFieldValue("docNum", docNum)
                + checkFieldValue("certSeries", certSeries)
                + checkFieldValue("certNum", certNum)
                + checkFieldValue("certDateFrom", certDateFrom)
                + checkFieldValue("certDateTo", certDateTo)
                + checkFieldValue("taxMark", taxMark)
                + checkFieldValue("taxType", taxType)
                + checkFieldValue("taxSeries", taxSeries);
        }
    }
}

/*
 * JSON EXAMPLE
 */

/*
{
"OKPOCode":"2352700257",
"carrierName":"КОВАЛЬ СЕРГІЙ ВІКТОРОВИЧ",
"licStatus":"Роздрукована",
"licIssueDate":null,
"licStartDate":"12.02.2016",
"licEndDate":null,
"vhclNum":"AA1020HH",
"vhclType":"Тягач",
"vhclStatus":"Звичайний",
"vhclVendorID":"VOLVO",
"vhclModel":"FH12.460",
"vhclWt":7961,
"loadCap":0,
"vchlManufYear":2005,
"vchlNumSeats":null,
"vchlVIN":"YV2A4CEA85B394358",
"certTypeID":"Свідоцтво про реєстрацію ТЗ",
"vhclSerie":"AAE",
"docNum":"041787",
"certSeries":null,
"certNum":null,
"certDateFrom":null,
"certDateTo":null,
"taxMark":null,
"taxType":null,
"taxSeries":null
}
*/
