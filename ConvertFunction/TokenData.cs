using System;

namespace ConvertFunction
{
    /* 
        * This class don't use for Table. It will use only for AuthToken Data as Object
        iat = The time the JWT was issued. Can be used to determine the age of the JWT
        jti = Unique identifier for the JWT. Can be used to prevent the JWT from being replayed. This is helpful for a one time use token.
        sub = The subject of the token 
        */
    public class TokenData
    {
        public string Sub = "";  //Required Field, Used for core JWT
        public string Jti = ""; //Required Field, Used for core JWT
        public string Iat = ""; //Required Field, Used for core JWT
        public string UserID = "";
        public string Userlevelid = "";
        public string IPAddress = "";
        public string Url = "";
        public DateTime TicketExpireDate = DateTime.UtcNow;
    }
}