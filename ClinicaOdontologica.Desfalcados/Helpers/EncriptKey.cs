using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ClinicaOdontologica.Desfalcados.Helpers
{
    public class EncriptKey
    {

        public static string AcertaSenha(string email, string _senha)
        {
            StringBuilder senha = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] entrada = Encoding.ASCII.GetBytes(email + "//" + _senha);
            byte[] hash = md5.ComputeHash(entrada);
            for (int i = 0; i < hash.Length; i++)
            {
                senha.Append(hash[i].ToString("X2"));
            }
            return senha.ToString();
        }


        public static string CriptoID(int? id)
        {

            var idchar = Convert.ToChar(id);
            StringBuilder senha = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] entrada = Encoding.ASCII.GetBytes(idchar + "/" + GerarHash());
            byte[] hash = md5.ComputeHash(entrada);

            for (int i = 0; i < hash.Length; i++)
            {
                senha.Append(hash[i].ToString("X2"));
            }
            return senha.ToString();
        }


        public static string GerarHash()
        {
            string caracteresPermitidos = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789@";
            char[] chars = new char[5];
            Random rd = new Random();
            for (int i = 0; i < 5; i++)
            {
                chars[i] = caracteresPermitidos[rd.Next(0, caracteresPermitidos.Length)];
            }

            string senha = new string(chars);

            return senha.ToString();
        }

    }
}