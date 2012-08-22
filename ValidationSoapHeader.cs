using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.Services.Protocols;

namespace SSA
{
    public class ValidationSoapHeader : SoapHeader
    {
        public string Directory, Password;
        private Dictionary<string, string> credentialDic=null;
         
        //Mudar para puxar de um banco
        public ValidationSoapHeader()
        {
            credentialDic = new Dictionary<string, string>();
            //Diretórios para desenvolvimento
            credentialDic.Add("adm", "teste");
        }

        public void Autenticate()
        {
            if(this == null)
                throw new Exception("Authentication Failed");
            
            if (this != null && credentialDic[Directory] == Password )
            {
                return;
            }
            else
            {
                throw new Exception("Authentication Failed");
            }
        }
    }
}
