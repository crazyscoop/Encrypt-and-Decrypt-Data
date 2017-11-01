using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using IDXdecode;        

class IDX
{
    static void Main()
    {
        Cryptography.Generic crypt = new Cryptography.Generic();    //instance of the CrptoGraphy Class we created
        string text = "My Name Is Himanshu...DotCode";
        string key = "DotCode..DotCode";                            //Private Key
        string IV = ".DotCodeDotCode.";                             //Must change IV every time

        /*
         * For Files
         */
        byte[] allBytes = File.ReadAllBytes("C:/Users/AMAZING/Desktop/q.txt");
        byte[] encryptBytes = (byte[])crypt.Crypt(Cryptography.Generic.CryptMethod.ENCRYPT, Cryptography.Generic.CryptClass.AEC, allBytes, key,IV);
        File.WriteAllBytes("C:/Users/AMAZING/Desktop/q1.txt", encryptBytes);
        byte[] decryptBytes = (byte[])crypt.Crypt(Cryptography.Generic.CryptMethod.DECRYPT, Cryptography.Generic.CryptClass.AEC, encryptBytes, key,IV);
        File.WriteAllBytes("C:/Users/AMAZING/Desktop/q2.txt", decryptBytes);

        /*
         * For String
         */ 
        string encrytString = (string)crypt.Crypt(Cryptography.Generic.CryptMethod.ENCRYPT, Cryptography.Generic.CryptClass.AEC, text, key,IV);
        string decryptString = (string)crypt.Crypt(Cryptography.Generic.CryptMethod.DECRYPT, Cryptography.Generic.CryptClass.AEC, encrytString, key,IV);
        Console.WriteLine(decryptString);
        Console.Read(); 
    }
}
