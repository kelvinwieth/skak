using System.Text;

var path = "~/test.txt";

using var file = File.Create(path);
byte[] info = new UTF8Encoding(true).GetBytes("hello");
file.Write(info, 0, info.Length);
