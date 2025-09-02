using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using System.IO;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConvertFunction
{
    public static class HttpGlobalFunction
    {

        private static System.Configuration.AppSettingsReader  confReader = new System.Configuration.AppSettingsReader();
      
        public static string _SERVICEROOT_ENDPOINT = confReader.GetValue("ServiceURL", typeof(string)).ToString();
        public static string _ACCESS_TOKEN = "";
        

        /// <summary>
        /// To Access API for POST Method
        /// </summary>
        /// <param name="ControllerName">The controller name you want to access</param>
        /// <param name="ActionName">The Action name you want to call</param>
        /// <param name="PostDataObject">Data for POST Request, have to be in JObject</param>
        /// <param name="AllowToken">true to authorize, false to not</param>
        /// <param name="Headers">Your Additional Headers</param>
        /// <returns>Return JSON data string if success, else return false</returns>
        public static async Task<string> HttpPostApi(string ControllerName, string ActionName, JObject PostDataObject = null, bool AllowToken = false, Dictionary<string, string> Headers = null)
        {
            string ControllerEndpoint = "api/" + ControllerName + "/" + ActionName;
            string ResultContent = "";
            HttpResponseMessage result;
            AllowToken = false;
            try
            {
                using (HttpClient ApiClient = new HttpClient())
                {
                    //ConfigureAwait(false);
                    ApiClient.BaseAddress = new Uri(_SERVICEROOT_ENDPOINT);

                    //#Headers Start
                    
        

                   // ApiClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                   ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (AllowToken)
                      
                        ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ACCESS_TOKEN);
                    if (Headers != null)
                    {
                        foreach (var header in Headers)
                        {
                            ApiClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    //#Headers End
                    if (PostDataObject != null)
                    {
                        StringContent stringContent = new StringContent(PostDataObject.ToString(), UnicodeEncoding.UTF8, "application/json");
                        result = await ApiClient.PostAsync(ControllerEndpoint, stringContent).ConfigureAwait(false);

                    }
                    else
                    {
                        result = await ApiClient.PostAsync(ControllerEndpoint, null).ConfigureAwait(false);
                    }

                    //#doesn't work with PostAsJsonAsync
                    //HttpResponseMessage response = await ApiClient.PostAsJsonAsync(ControllerEndpoint, PostDataObject);

                    //#validate statuscode have to 200, else exception
                    if (result.IsSuccessStatusCode)
                    {
                        HttpHeaders headers = result.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("newToken", out values))
                        {
                            string _NEWTOKEN = values.First();
                            if (_NEWTOKEN != "")
                                _ACCESS_TOKEN = _NEWTOKEN;
                        }

                        ResultContent = await result.Content.ReadAsStringAsync();
                    }
                    else
                        ResultContent = "false";
                    //ResultContent = "ERROR: " + result.StatusCode;
                }

                return ResultContent;
            }
            catch (WebException wex)
            {
                //if (wex.Response != null)
                //    return "ERROR: " + new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                //else
                //    return "ERROR: " + wex.Message.ToString();
                return "false";
            }
        }

        /// <summary>
        /// To Access API for GET Method
        /// </summary>
        /// <param name="ControllerName">The controller name you want to access</param>
        /// <param name="ActionName">The Action name you want to call</param>
        /// <param name="AllowToken">true to authorize, false to not</param>
        /// <param name="Headers">Your Additional Headers</param>
        /// <returns></returns>
        public static async Task<string> HttpGetApi(string ControllerName, string ActionName, bool AllowToken = false, Dictionary<string, string> Headers = null)
        {
            string ControllerEndpoint = "/mobile/" + ControllerName + "/" + ActionName + "/";
            string ResultContent = "";
            HttpResponseMessage result;
            try
            {
                using (HttpClient ApiClient = new HttpClient())
                {
                    ApiClient.BaseAddress = new Uri(_SERVICEROOT_ENDPOINT);

                    if (AllowToken)
                        ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ACCESS_TOKEN);

                    if (Headers != null)
                    {
                        foreach (var header in Headers)
                        {
                            ApiClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    result = await ApiClient.GetAsync(ControllerEndpoint);

                    //#validate statuscode have to 200, else exception
                    if (result.IsSuccessStatusCode)
                        ResultContent = await result.Content.ReadAsStringAsync();
                    else
                        ResultContent = "ERROR: " + result.StatusCode + " " + await result.Content.ReadAsStringAsync();
                }

                return "SUCCESS: " + ResultContent;
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                    return "ERROR: " + new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                else
                    return "ERROR: " + wex.Message.ToString();
            }
        }
        
        public static async Task<Boolean> HttpPostLogin(JObject PostDataObject)
        {
            string ControllerEndpoint = "api/token";
            string ResultContent = "";
            Boolean ReturnBol = false;
            HttpResponseMessage result;
            using (HttpClient ApiClient = new HttpClient())
            {
                //ConfigureAwait(false);
                ApiClient.BaseAddress = new Uri(_SERVICEROOT_ENDPOINT);

                //#Headers Start
                ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));


                //#Headers End
                if (PostDataObject != null)
                {
                    StringContent stringContent = new StringContent(PostDataObject.ToString(), UnicodeEncoding.UTF8, "application/json");
                    result = await ApiClient.PostAsync(ControllerEndpoint, stringContent).ConfigureAwait(false);
                    //List<KeyValuePair<string, string>> nvc = GlobalFunction.ConvertJObjectToList(PostDataObject);

                   // var req = new HttpRequestMessage(HttpMethod.Post, ControllerEndpoint) { Content = new FormUrlEncodedContent(nvc) };
                   

                    //result = ApiClient.SendAsync(req).Result;
                       
                }
                else
                {
                    throw new Exception("Invalid Parameter");
                }

                if (result.IsSuccessStatusCode)
                {
                    ResultContent = await result.Content.ReadAsStringAsync();
                    JObject jobj = GlobalFunction.ConvertStringToJsonObject(ResultContent);
                    if (jobj["access_token"].ToString() != "")
                    {
                        _ACCESS_TOKEN = jobj["access_token"].ToString();
                        ReturnBol = true;
                    }
                    else
                    {
                        throw new Exception(ResultContent);
                    }
                           
                }
                else
                {
                    throw new Exception("Login request fail.");
                    
                }
                    
            }

            return ReturnBol;
            
            
        }
        
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////
        /// </summary>

        public static async Task<System.Drawing.Image> GetPhoto(int PlayerID)
        {
            JObject obj = new JObject();
            obj.Add("PlayerID", PlayerID);
            string Result = await HttpPostApi("Player", "GetPhoto", obj, true);
            System.Drawing.Image bmpReturn = null;
            return bmpReturn;
        }

        public static dynamic employeePhoto(string functionname,  string filename ,int CompanyID, int CurrentCustomerID)
        {

            System.Configuration.AppSettingsReader confReader = new System.Configuration.AppSettingsReader();
            string URL = confReader.GetValue("ServicePhotoURL", typeof(string)).ToString();
            URL = URL + functionname + "/" + filename + "/" + "jpg";
            string method = "POST";


            dynamic  strResponse ;
            strResponse = WPhotoRequest(URL, method,"", CompanyID.ToString(), CurrentCustomerID.ToString());


            return strResponse;
        }
      

        public static System.Drawing.Image WPhotoRequest(string URL, string method, string postData ,string companyID, string customerID)
        {
            string responseData = "";
            System.Drawing.Image bmpReturn = null;
            try
            {
                System.Net.CookieContainer cookieJar = new System.Net.CookieContainer();
                System.Net.HttpWebRequest hwrequest =
                  (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
                hwrequest.CookieContainer = cookieJar;
                hwrequest.Accept = "*/*";
                hwrequest.AllowAutoRedirect = true;
                hwrequest.UserAgent = "http_requester/0.1";
                hwrequest.Timeout = 60000;
                hwrequest.Method = method;

                hwrequest.Timeout = 95 * 95 * 100000;
                hwrequest.ServicePoint.ConnectionLimit = 100;
                hwrequest.ReadWriteTimeout = 95 * 95 * 100000;
                hwrequest.ContinueTimeout = 95 * 95 * 100000;
                hwrequest.KeepAlive = true;

                //hwrequest.Headers.Add("Authorization", "Bearer  N5XIuzClcNuwrdsuqdt6mWqKlk/k8A0y2GK/QCn9eux/RtXEpXV/VBc328JVYUHSkFYSXPIwdckYJAofRjG7r/R00P1QMDj81NZDqfSlegCYO1duw5whK3FcXH+MahbnmD9WhkAuxQs=");
                //hwrequest.Headers.Add("myOrigin", "globalemsweb");
                hwrequest.Headers.Add("companyID", companyID);
                hwrequest.Headers.Add("customerID", customerID);
                if (hwrequest.Method == "POST")
                {
                    hwrequest.ContentType = "application/x-www-form-urlencoded";
                    // Use UTF8Encoding instead of ASCIIEncoding for XML requests:
                    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                    byte[] postByteArray = encoding.GetBytes(postData);
                    hwrequest.ContentLength = postByteArray.Length;
                    System.IO.Stream postStream = hwrequest.GetRequestStream();

                    postStream.Write(postByteArray, 0, postByteArray.Length);
                    postStream.Close();
                }
              
                using (System.Net.HttpWebResponse hwresponse = (System.Net.HttpWebResponse)hwrequest.GetResponse())
                {
                    if (hwresponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        System.IO.Stream responseStream = hwresponse.GetResponseStream();
                        //System.IO.StreamReader myStreamReader =
                        //   new System.IO.StreamReader(responseStream);
                        bmpReturn = System.Drawing.Image.FromStream(responseStream);
                        //responseData = myStreamReader.ReadToEnd();
                    }
                    hwresponse.Close();
                }

            }
            catch (Exception e)
            {
                responseData = "An error occurred: " + e.Message;
            }
            return bmpReturn;// responseData;
        }
        /// <summary>
        /// To Access API for POST Method
        /// </summary>
        /// <param name="ControllerName">The controller name you want to access</param>
        /// <param name="ActionName">The Action name you want to call</param>
        /// <param name="PostDataObject">Data for POST Request, have to be in JObject</param>
        /// <param name="AllowToken">true to authorize, false to not</param>
        /// <param name="Headers">Your Additional Headers</param>
        /// <returns>Return JSON data string if success, else return false</returns>
        public static async Task<string> HttpPostApiForm(string ControllerName, string ActionName, JObject PostDataObject = null, bool AllowToken = false, Dictionary<string, string> Headers = null)
        {
            string ControllerEndpoint = "api/" + ControllerName + "/" + ActionName;
            string ResultContent = "";
            HttpResponseMessage result;
            AllowToken = false;
            try
            {
                using (HttpClient ApiClient = new HttpClient())
                {
                    //ConfigureAwait(false);
                    ApiClient.BaseAddress = new Uri(_SERVICEROOT_ENDPOINT);

                    //#Headers Start
                    //ApiClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));



                    if (AllowToken)
                        ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ACCESS_TOKEN);

                    if (Headers != null)
                    {
                        foreach (var header in Headers)
                        {
                            ApiClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    //#Headers End
                    if (PostDataObject != null)
                    {
                        //MultipartFormDataContent stringContent = new MultipartFormDataContent();
                        StringContent stringContent = new StringContent(PostDataObject.ToString(), UnicodeEncoding.UTF8, "application/json");
                        // StringContent stringContent = new StringContent(PostDataObject.ToString(), UnicodeEncoding.UTF8, "multipart/form-data");
                        //MultipartFormDataContent formContent = new MultipartFormDataContent();
                        //formContent.Add(stringContent, "formDataObj");
                        //stringContent.Add(new HttpContent(PostDataObject.ToString()), "formDataObj");
                        //result = await ApiClient.PostAsync(ControllerEndpoint, formContent).ConfigureAwait(false);
                        result = await ApiClient.PostAsync(ControllerEndpoint, stringContent).ConfigureAwait(false);
                    }
                    else
                    {
                        result = await ApiClient.PostAsync(ControllerEndpoint, null).ConfigureAwait(false);
                    }

                    //#doesn't work with PostAsJsonAsync
                    //HttpResponseMessage response = await ApiClient.PostAsJsonAsync(ControllerEndpoint, PostDataObject);

                    //#validate statuscode have to 200, else exception
                    if (result.IsSuccessStatusCode)
                    {
                        HttpHeaders headers = result.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("newToken", out values))
                        {
                            string _NEWTOKEN = values.First();
                            if (_NEWTOKEN != "")
                                _ACCESS_TOKEN = _NEWTOKEN;
                        }

                        ResultContent = await result.Content.ReadAsStringAsync();
                    }
                    else
                        ResultContent = "false";
                    //ResultContent = "ERROR: " + result.StatusCode;
                }

                return ResultContent;
            }
            catch (WebException wex)
            {
                //if (wex.Response != null)
                //    return "ERROR: " + new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                //else
                //    return "ERROR: " + wex.Message.ToString();
                return "false";
            }
        }
        public static string WRequest(string URL, string method, string postData)
        {
            string responseData = "";
            try
            {

                System.Net.CookieContainer cookieJar = new System.Net.CookieContainer();
                System.Net.HttpWebRequest hwrequest =
                  (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
                hwrequest.CookieContainer = cookieJar;
                //     hwrequest.Accept = "*/*";
                hwrequest.AllowAutoRedirect = true;
                hwrequest.Timeout = 300000;
                hwrequest.Method = method;
                hwrequest.Timeout = 95 * 95 * 100000;

                hwrequest.KeepAlive = true;
                hwrequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

                if (hwrequest.Method == "POST")
                {
                    hwrequest.ContentType = "application/x-www-form-urlencoded";
                    hwrequest.Headers.Add("Content-Encoding", "utf-8");

                    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                    byte[] postByteArray = encoding.GetBytes(postData);
                    hwrequest.ContentLength = postByteArray.Length;
                    System.IO.Stream postStream = hwrequest.GetRequestStream();

                    postStream.Write(postByteArray, 0, postByteArray.Length);
                    postStream.Close();
                }
                System.Net.HttpWebResponse hwresponse = (System.Net.HttpWebResponse)hwrequest.GetResponse();
                if (hwresponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.Stream responseStream = hwresponse.GetResponseStream();
                    System.IO.StreamReader myStreamReader = new System.IO.StreamReader(responseStream);
                    responseData = myStreamReader.ReadToEnd();
                }
                hwresponse.Close();
            }
            catch (Exception e)
            {

                responseData = "An error occurred: " + e.Message;
            }
            return responseData;
        }






    }
}
