# Encrypt-and-Decrypt-Data
Symmetric Encryption and Decryption of data

## How To Use
1. Create a new C# project.
2. Add Cyrptography class to your project.
3. Create an instance of Cryptograpghy Class.
```
        Cryptography.Generic crypt = new Cryptography.Generic(); 
```
4. Create a key and a Initializing Vector(IV).
```
        string key = "DotCode..DotCode";                            
        string IV = ".DotCodeDotCode.";
```
5. Encrypt your text.
```
        string encrytString = (string)crypt.Crypt(Cryptography.Generic.CryptMethod.ENCRYPT, Cryptography.Generic.CryptClass.AEC, text, key,IV);
```
6. Decrypt your text.
```
        string decryptString = (string)crypt.Crypt(Cryptography.Generic.CryptMethod.DECRYPT, Cryptography.Generic.CryptClass.AEC, encrytString, key,IV);
```

