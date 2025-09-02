using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using System.Collections;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace ConvertFunction
{
    public class MMTMCardService
    {
        public static String _CertificateVerificationCode = "YCDCMARKET";
        public static String _CertificatePublicKey = "";
        public static String _ErrorMessage = "";
        private String _LoginName;
        private String _LoginPwd;
        private String _APIURL;
        public Boolean IsLogin = false;
        public void New()
        {

        }
        public DataTable GetMarketList()
        {
            DataTable dtDriver = new DataTable();
            String strResponse = "";
            JObject jObj = new JObject();
            jObj.Add("loginname", _LoginName);
            jObj.Add("loginpwd", _LoginPwd);
            String jString = ConvertFunction.GlobalFunction.ConvertJObjectToQueryString(jObj);
            strResponse = HttpGlobalFunction.WRequest(_APIURL + "/?action=getmarketlist", "POST", jString);
            // dt_Employee = Await WebService.Fingerprint.GetPlayerInfoByEnrollNumber(EnrollNumber)

            dtDriver = ConvertFunction.GlobalFunction.ConvertJSONToDataTable(strResponse);

            return dtDriver;

        }


    }
    

}