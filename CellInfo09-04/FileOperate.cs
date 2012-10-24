﻿using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace CellInfo
{
    class FileOperate
    {  
        // 文件写入操作
        public void WriteToFile(string path,string TextIn)
        {
            System.IO.File.WriteAllText(path, TextIn);
        }

        // 文件读取操作
        public string ReadFromFile(string path)
        {
            string Out = System.IO.File.ReadAllText(path);
            return Out;
        }

        public byte[] ReadByte(string path)
        {
            byte[] Out = new byte[400];
            FileStream FS = new FileStream(path,FileMode.Open);
            FS.Seek(177, SeekOrigin.Begin);
            FS.Read(Out,0,2);
            return Out;
        }

        public string[] DataPull(string InPut)
        {
            InPut = InPut.Trim();
            string[] str = System.Text.RegularExpressions.Regex.Split(InPut, @"\s+");            
            return str;
        }

        

       
    }
}