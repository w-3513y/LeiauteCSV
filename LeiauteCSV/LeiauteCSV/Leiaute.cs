using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeiauteCSV
{
    class Leiaute
    {
        public string ClassName { get; set; }
      //  public string JsonProperty { get; set; }
        public string SizeProperty { get; set; }
        public bool Required { get; set; }
        public string Field { get; set; }       
        public static string ReturnType(string type, string size, string field)
        {
            return "string";
      /*      if (type == "A")
            {
                return "string";
            }
            else if (type == "N")
            {
                _ = int.TryParse(size, out int _size);
                if(_size > 0)
                {
                    if (field.StartsWith("DATA"))
                    {
                        return "DateTime";
                    }
                    else
                    {
                        return "int";
                    }                    
                }
                else
                {
                    return "double";
                }
            }
            else
            {
                return "";
            }*/
        }
        public static string ReturnSize(string size)
        {
       //     if (type == "A"){
                if (size.Length > 2)
                {
                    return $"[StringLength({int.Parse(size.Substring(0, 3))})]";
                }
                else if (size.Length > 0)
                {
                    return $"[StringLength({int.Parse(size)})]";
                }
                else
                {
                    return "";
                }

    //        }
    /*        else
            {
                return "";
            }*/
            
        }
        public static bool IsRequired(string required)
        {
            return required == "(*)";
        }
        public static List<Leiaute> ReadLine(string path)
        {
            File.ReadAllLines(path);
            return File.ReadAllLines(path)
                          .Select(a => a.Split(';'))
                        //  .Where(b => b[0].StartsWith("SAFX"))
                          .Select(c => new Leiaute()
                          {
                              ClassName = c[0],
                      //        JsonProperty = $"[JsonPropertyName(\"{c[4].Replace(" ","")}\")]",
                              SizeProperty =  ReturnSize(c[5]),
                              Required = IsRequired(c[1]),
                              Field = $"public {ReturnType(c[6], c[5], c[4].Trim())} {c[4].Replace(" ", "")}"+" { get; set; }"

                          })                          
                          .ToList();
        }
    }
}
