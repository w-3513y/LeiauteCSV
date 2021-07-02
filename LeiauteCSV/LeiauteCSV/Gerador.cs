using System;
using System.Collections.Generic;
using System.IO;

namespace LeiauteCSV
{
    static class Gerador
    {
        static public void Gerar()
        {

            string path = @"D:\Tecnologia\Cursos\C#\LeiauteCSV\Manual_Layout_MastersafDW.csv";
            string directoryName = Path.GetDirectoryName(path)+@"\";
            
            var leiautelist = Leiaute.ReadLine(path);
            var last = leiautelist.Count;
            var count = 0;

            List<string> fileclass = new();
            List<string> openedclass = new();

            string _class = "";            
            foreach (var y in leiautelist)
            {
                ++count;
                if ((_class != y.ClassName) || (count == last))
                {
                    
                    if (!(openedclass.IndexOf(y.ClassName) > -1) || (count == last))
                    {
                        if (!String.IsNullOrEmpty(_class))
                        {
                    
                            if (count == last)
                            {
                                //fileclass.Add("        " + y.JsonProperty);
                                if (!String.IsNullOrEmpty(y.SizeProperty))
                                {
                                    fileclass.Add("        " + y.SizeProperty);
                                }
                                fileclass.Add("        " + y.Field);
                            }
                            fileclass.Add("    }");
                            fileclass.Add("}");
                            if (File.Exists(directoryName + _class + ".cs"))
                            {
                                File.Delete(directoryName + _class + ".cs");
                            }
                            File.WriteAllLines(directoryName + _class + ".cs", fileclass);
                            fileclass.Clear();
                        }
                        fileclass.Add("using System.Collections.Generic;");
                       // fileclass.Add("using System.Text.Json.Serialization;");
                        fileclass.Add("");
                        fileclass.Add("IntegracaoCosmosMasterSAF.Domain.Entities");
                        fileclass.Add("{");
                        fileclass.Add(@"    public class " + y.ClassName);
                        fileclass.Add("    {");
                        //fileclass.Add("        " + y.JsonProperty);
                        if (!String.IsNullOrEmpty(y.SizeProperty))
                        {
                            fileclass.Add("        " + y.SizeProperty);
                        }

                        if (y.Required)
                        {
                            fileclass.Add("        [Required()]");
                        }
                        fileclass.Add("        " + y.Field);
                        openedclass.Add(y.ClassName);
                    }
                    _class = y.ClassName;
                }
                else
                {
                    //fileclass.Add("        " + y.JsonProperty);
                    if (!String.IsNullOrEmpty(y.SizeProperty))
                    {
                        fileclass.Add("        " + y.SizeProperty);
                    }                    
                    if (y.Required)
                    {
                        fileclass.Add("        [Required()]");
                    }
                    fileclass.Add("        " + y.Field);
                }
            }
            


        }
    }
}
