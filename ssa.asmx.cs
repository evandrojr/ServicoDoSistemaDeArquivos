using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Web.Services.Protocols;
using System.Text;

namespace SSA
{
    /// <summary>
    /// Summary description for SSA
    /// </summary>
    [WebService(Namespace = "http://serpro/webservice/ssa")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SSA : System.Web.Services.WebService
    {
        public ValidationSoapHeader Authentication;

        string ROOT = @"C:\ftp\ssa\";

        [SoapHeader("Authentication")]
        [WebMethod]
        public string[] DirList(string dir)
        {
            Authentication.Autenticate();
            string[] diretorios = Directory.GetDirectories(ROOT + dir);
            return diretorios;
        }

        [SoapHeader("Authentication")]
        [WebMethod]
        public string[] FileList(string dir)
        {
            Authentication.Autenticate();
            string[] arquivos = Directory.GetFiles(ROOT + dir);
            return arquivos;
        }

        [SoapHeader("Authentication")]
        [WebMethod]
        public string Md5(string input)
        {
            Authentication.Autenticate();
            // Primeiro passo, calcular o MD5 hash a partir da string
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            BinaryReader binReader = new BinaryReader(File.Open(ROOT + input, FileMode.Open, FileAccess.Read));
            binReader.BaseStream.Position = 0;
            byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
            binReader.Close();

            byte[] hash = md5.ComputeHash(binFile);

            // Segundo passo, converter o array de bytes em uma string haxadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        [SoapHeader("Authentication")]
        [WebMethod]
        public byte[] FileGet(string filename)
        {
            Authentication.Autenticate();
            //BinaryReader binReader = new BinaryReader(File.Open(Server.MapPath(filename), FileMode.Open, FileAccess.Read));
            BinaryReader binReader = new BinaryReader(File.Open(ROOT + filename, FileMode.Open, FileAccess.Read));
            binReader.BaseStream.Position = 0;
            byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
            binReader.Close();
            return binFile;
        }

        [SoapHeader("Authentication")]
        [WebMethod]
        public void FilePut(byte[] buffer, string filename)
        {
            Authentication.Autenticate();
            BinaryWriter binWriter = new
            BinaryWriter(File.Open((ROOT + filename), FileMode.CreateNew, FileAccess.ReadWrite));
            binWriter.Write(buffer);
            binWriter.Close();
        }

        [WebMethod]
        public string hi5()
        {
            return "hi 5!";
        }




    }
}
