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
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using System.Collections;
using System.Data.SqlClient;

namespace ConvertFunction
{
    public static class GlobalFunction
    {
        public static Claim[] GetClaims(TokenData obj)
        {
            var claims = new Claim[]
            {
                new Claim("UserID",obj.UserID),
                new Claim("Userlevelid", obj.Userlevelid),
                new Claim(JwtRegisteredClaimNames.Sub, obj.Sub),
                new Claim(JwtRegisteredClaimNames.Jti, obj.Jti),
                new Claim(JwtRegisteredClaimNames.Iat, obj.Iat, ClaimValueTypes.Integer64)
            };
            return claims;
        }

        public static TokenData GetTokenData(JwtSecurityToken tokenS)
        {
            var obj = new TokenData();
            try
            {
                obj.UserID = tokenS.Claims.First(claim => claim.Type == "UserID").Value;
                obj.Userlevelid = tokenS.Claims.First(claim => claim.Type == "Userlevelid").Value;
                obj.Sub = tokenS.Claims.First(claim => claim.Type == "sub").Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return obj;
        }

        public static string DecryptToken(string cipherString, bool useHashing, string key)
        {
            try
            {
                byte[] keyArray = new byte[24];
                //get the byte code of the string

                byte[] toEncryptArray = Convert.FromBase64String(cipherString);
                //string key = "JFTUFICUCQ4GS3MI6RBM39SD";
                byte[] newkeyArray = new byte[24];
                if (useHashing)
                {
                    //if hashing was used get the hash code with regards to your key
                    var hashmd5 = System.Security.Cryptography.MD5.Create();
                    keyArray = hashmd5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(key));
                    keyArray.CopyTo(newkeyArray, 0);
                    hashmd5.Dispose();
                }
                else
                {
                    keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(key);
                    keyArray.CopyTo(newkeyArray, 0);
                }

                var tdes = System.Security.Cryptography.TripleDES.Create();
                tdes.Key = newkeyArray;
                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)

                tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                System.Security.Cryptography.ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                                     toEncryptArray, 0, toEncryptArray.Length);

                //Release resources held by TripleDes Encryptor                
                tdes.Dispose();
                //return the Clear decrypted TEXT
                string olddata = System.Text.UTF8Encoding.UTF8.GetString(resultArray);
                return olddata;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        public static string EncryptToken(string toEncrypt, bool useHashing, string key)
        {
            try
            {
                byte[] keyArray = new byte[24];
                //get the byte code of the string

                byte[] toEncryptArray = System.Text.UTF8Encoding.UTF8.GetBytes(toEncrypt);
                //string key = "JFTUFICUCQ4GS3MI6RBM39SD";
                byte[] newkeyArray = new byte[24];
                if (useHashing)
                {
                    //if hashing was used get the hash code with regards to your key
                    var hashmd5 = System.Security.Cryptography.MD5.Create();
                    keyArray = hashmd5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(key));
                    keyArray.CopyTo(newkeyArray, 0);
                    hashmd5.Dispose();
                }
                else
                {
                    keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(key);
                    keyArray.CopyTo(newkeyArray, 0);
                }

                var tdes = System.Security.Cryptography.TripleDES.Create();
                tdes.Key = newkeyArray;
                //mode of operation. there are other 4 modes. 
                //We choose ECB(Electronic code Book)

                tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                System.Security.Cryptography.ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                                     toEncryptArray, 0, toEncryptArray.Length);

                //Release resources held by TripleDes Encryptor                
                tdes.Dispose();
                //return the Clear decrypted TEXT
                string encrypteddata = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                return encrypteddata;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        public static dynamic DataTableToJson(DataTable dt)
        {
            JArray rows = new JArray();
            JObject row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new JObject();
                foreach (DataColumn col in dt.Columns)
                {
                    string val = dr[col].ToString();
                    row.Add(col.ColumnName, (!String.IsNullOrEmpty(val)) ? JToken.FromObject(dr[col]) : null);

                }
                rows.Add(row);
            }

            return rows;
        }
        public static dynamic ConvertDataSetToArrayList(DataSet ds1)
        {
            ArrayList alst = new ArrayList();
            JArray rows = new JArray();
            foreach (DataTable tbl in ds1.Tables)
            {
                rows.Add(DataTableToJson(tbl));
            }
            return rows;
        }

        public static string ConvertJObjectToQueryString(JObject jObj)
        {
            if (jObj == null)
                throw new ArgumentNullException("request");

            var query = String.Join("&",
                            jObj.Children().Cast<JProperty>()
                            .Select(jp => jp.Name + "=" + (jp.Value.ToString())));
            return query;

        }

        public static List<KeyValuePair<string, string>> ConvertJObjectToList(JObject jObj)
        {
            if (jObj == null)
                throw new ArgumentNullException("request");
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, JToken> property in jObj)
            {
                result.Add(new KeyValuePair<string, string>(property.Key, property.Value.ToString()));
            }
            
            return result;


        }


        public static JObject ConvertStringToJsonObject(string str)
        {
            try
            {
                return (JObject)JsonConvert.DeserializeObject(str);
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public static JArray ConvertStringToJsonArray(string str)
        {
            try
            {
                return (JArray)JsonConvert.DeserializeObject(str);
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public static DataTable ConvertJSONToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            //strip out bad characters
            string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

            //hold column names
            List<string> dtColumns = new List<string>();

            //get columns
            foreach (string jp in jsonParts)
            {
                //only loop  once to get column names
             string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
               // string[] propData =Regex.Split(jp.Replace("{", "").Replace("}", ""),"");

                foreach (string rowData in propData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        if (idx == -1)
                        {
                            break;
                        }                          

                        string n = rowData.Substring(0, idx - 1);
                        string v = rowData.Substring(idx + 1);
                        if (!dtColumns.Contains(n))
                        {
                            dtColumns.Add(n.Replace("\"", ""));
                        }
                    }
                    catch
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                    }

                }
                break; // TODO: might not be correct. Was : Exit For
            }

            //build dt
            foreach (string c in dtColumns)
            {
                dt.Columns.Add(c);
            }
            //get table data
            
            
            foreach (string jp in jsonParts)
            {
                string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in propData)
                {
                    try
                    {

                        int idx = rowData.IndexOf(":");
                        if (idx == -1)
                        {
                            break;
                        }

                        string n = rowData.Substring(0, idx - 1).Replace("\"", "");                                           
                        string v = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[n] = v;
                    }
                    catch
                    {
                        continue;
                    }

                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public static DataTable ConvertToDataTable(dynamic queryResult)
        {
            try
            {
                DataTable dtResult = new DataTable();
                Object objDynamicList = queryResult[0];

                string[] propertyNames = objDynamicList.GetType().GetProperties().Select(p => p.Name).ToArray();
                foreach (var prop in propertyNames)
                {
                    dtResult.Columns.Add(prop);
                }
                for (int i = 0; i < queryResult.Count; i++)
                {
                    DataRow drResult = dtResult.NewRow();
                    for (int k = 0; k < propertyNames.Length; k++)
                    {
                        drResult[propertyNames[k]] = queryResult[i].GetType().GetProperty(propertyNames[k]).GetValue(queryResult[i], null);
                    }
                    dtResult.Rows.Add(drResult);
                }
                return dtResult;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }
        


    }
}