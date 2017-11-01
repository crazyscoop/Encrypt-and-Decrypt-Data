using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace IDXdecode
{
    public class Cryptography
    {
        public class Generic
        {
            public enum CryptMethod {ENCRYPT,DECRYPT}       //To Encrypt Or Decrypt 
            public enum CryptClass { AEC,DEC,RIJ,TDES}      //Different Cryptographic Algorithms
            public object Crypt(CryptMethod cryptmethod,CryptClass cryptclass,object input,string key,string IV)
            {

                SymmetricAlgorithm control;                  //Base class which all implementations of symmetric algorithms must inherit 

                switch (cryptclass)                          //Choosing CryptClass
                {
                    case CryptClass.AEC:
                        control = new AesManaged();
                        break;

                    case CryptClass.DEC:
                        control = new DESCryptoServiceProvider();
                        break;

                    case CryptClass.RIJ:
                        control = new RijndaelManaged();
                        break;

                    case CryptClass.TDES:
                        control = new TripleDESCryptoServiceProvider();
                        break;

                    default:
                        return false;
                        break;
                }

                control.Key = UTF8Encoding.ASCII.GetBytes(key);      //Converting Key(string) into Byte[] using UTF8 encoding   
                control.IV = UTF8Encoding.UTF8.GetBytes(IV);        //Converting Initializing Vector(string) into Byte[] using UTF8 encoding
                control.Padding = PaddingMode.PKCS7;                //PaddingMode For Cypher
                control.Mode = CipherMode.CBC;                      //Chiper Block Chaining

                ICryptoTransform cryptTransform = null;             //Defines the basic operations of cryptographic transformations

                if (cryptmethod == CryptMethod.ENCRYPT)
                {
                    cryptTransform = control.CreateEncryptor(control.Key,control.IV);
                }
                else if(cryptmethod == CryptMethod.DECRYPT)
                {
                    cryptTransform = control.CreateDecryptor(control.Key,control.IV);
                }

                byte[] resultarray;
                /*
                 * NOTE: 
                 * 1->While Encrypting Decrypting String data type special attention be paid.
                 * 2->While Encrypting string must be converted to bytes using 
                 * desired encoding techniques UTF8 or ASCII
                 * 3->While returning the Encrypted data in the form of string,
                 * use Convert.ToBase64String 
                 * 4->While Decrypting the base64 string must be converted to bytes using
                 * Convert.FromBase64String
                 * 5->Finally while returning the Decrypted Data, Convert the byte array into
                 * string using UnicodeEncoding.UTF8.ToString method
                 * 
                 */ 
                 

                if(input is string)
                { 
                    byte[] temp;
                    if (cryptmethod == CryptMethod.ENCRYPT)
                    {
                         temp = UTF8Encoding.UTF8.GetBytes((string)input);      //Convert into byte[] via UTF8 encoding
                    }
                    else
                    {
                         temp = Convert.FromBase64String((string)input);        //Convert into byte[] using FromBase64String since input is in base64 format
                    }
                    resultarray = cryptTransform.TransformFinalBlock(temp, 0, temp.Length);
                    control.Clear();
                    cryptTransform.Dispose();
                    if (cryptmethod == CryptMethod.ENCRYPT)
                    {
                        return Convert.ToBase64String(resultarray);            //Converting byte[] to base64
                    }
                    else
                    {
                        return UnicodeEncoding.UTF8.GetString(resultarray);     //Finally(Decryption) Converting byte[] into string 
                    }
                }
                else if(input is byte[])
                {
                    resultarray = cryptTransform.TransformFinalBlock((byte[])input, 0, ((byte[])input).Length);
                    control.Clear();
                    cryptTransform.Dispose();
                    return resultarray;
                }
                return false;
            }
        } 
    }
}
