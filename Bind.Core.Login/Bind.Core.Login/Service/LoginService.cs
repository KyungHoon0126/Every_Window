﻿using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TNetwork;
using TNetwork.Common;
using TNetwork.Data;

namespace BIND.Core.Login.Network
{
    public class LoginService
    {
        private NetworkManager networkManager = new NetworkManager();

        public string FILE_URL = "/upload/{0}";
        private readonly Encoding encoding = Encoding.UTF8;


        private const string LOGIN_URL = "/auth/login";
        private const string MEMBER_URL = "members/";
        private const string LOGOUT_URL = MEMBER_URL + "logout";
        private const string TOKEN_REFRESH_URL = "/token/refresh";

        public async Task<TResponse<TokenInfo>> Login(string id, string pw)
        {
            var jObj = new JObject();
            //jObj["device"] = "PC";
            jObj["email"] = id;
            jObj["pw"] = Sha512Hash(pw);
            //jObj["pw"] = pw;

            var resp = await networkManager.GetResponse<TokenInfo>(Options.loginUrl, Method.POST, jObj.ToString());

            // TODO :  학생 or 직장인인지 구분하는 Idx가 null 값일 때 임시로 -1 넣어둠. 고쳐야 함.
            if(resp.Data.Student_Idx == null)
            {
                resp.Data.Student_Idx = -1;
            }
            if(resp.Data.Worker_Idx == null)
            {
                resp.Data.Worker_Idx = -1;
            }
            

            if (resp.Data != null)
            {
                Options.tokenInfo.Token = resp.Data.Token;
                Options.tokenInfo.RefreshToken = resp.Data.RefreshToken;
                
                Options.tokenInfo.Student_Idx = resp.Data.Student_Idx;
                Options.tokenInfo.Worker_Idx = resp.Data.Worker_Idx;
            }
            return resp;
        }

        private string Sha512Hash(string str)
        {
            var sha512 = new SHA512CryptoServiceProvider();
            byte[] resultHash = sha512.ComputeHash(Encoding.Default.GetBytes(str));
            string transPwd = string.Empty;

            foreach (byte x in resultHash)
            {
                transPwd += $"{x:x2}";
            }

            return transPwd;
        }

        public async Task Logout()
        {
            var jObj = new JObject();
            jObj["device"] = "PC";

            var resp = await networkManager.GetResponse<TokenInfo>(Options.logoutUrl, Method.POST, jObj.ToString());
        }

        public void SettingHttpRequest(string serverUrl)
        {
            networkManager.SetHTTPRequestURL(serverUrl, LOGIN_URL, LOGOUT_URL, TOKEN_REFRESH_URL);
        }
    }
}
