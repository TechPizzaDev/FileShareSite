using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FileShareSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
            builder.UseStartup<Startup>();

            return builder;
        }

        private static void ProcessHighlightJsFile(string path)
        {
            //string name = Path.GetFileNameWithoutExtension(path);
            //string safeName = name.Replace(" - ", "_");
            //var langRefs = new HashSet<string>();
            //
            //var builder = new StringBuilder();
            //using (var reader = new StreamReader(path))
            //{
            //    string line = reader.ReadLine();
            //    builder.AppendLine(line.Replace("module.exports = function", $"function {safeName}"));
            //
            //    while ((line = reader.ReadLine()) != null)
            //    {
            //        if (line.Contains("subLanguage"))
            //        {
            //            string trimmed = line.Trim();
            //            //Console.WriteLine(Path.GetFileName(path) + " (line): " + trimmed);
            //
            //            if(trimmed.Contains('['))
            //            {
            //                int start = trimmed.IndexOf('[') + 1;
            //                int end = trimmed.LastIndexOf(']');
            //                string arrayLine = trimmed.Remove(end).Substring(start);
            //                string[] items = arrayLine.Split(',');
            //
            //                for (int i = 0; i < items.Length; i++)
            //                {
            //                    items[i] = items[i].Trim('\'', '"', ' ');
            //
            //                    if (items[i] != "")
            //                        langRefs.Add(items[i]);
            //                }
            //            }
            //            else
            //            {
            //                int start = trimmed.IndexOf('\'') + 1;
            //                int end = trimmed.LastIndexOf('\'');
            //                if(start == 0)
            //                {
            //                    start = trimmed.IndexOf('"') + 1;
            //                    end = trimmed.LastIndexOf('"');
            //                }
            //
            //                string lang = trimmed.Remove(end).Substring(start);
            //                if (lang != "")
            //                    langRefs.Add(lang);
            //            }
            //        }
            //
            //        builder.AppendLine(line);
            //    }
            //}
            //
            //langRefs.Remove(name);
            //
            //if (langRefs.Count > 0)
            //{
            //    builder.AppendLine();
            //    builder.AppendLine($"const {safeName}_references = {{");
            //    foreach (var lang in langRefs)
            //        builder.AppendLine("  \"" + lang + "\",");
            //    builder.Append("};");
            //
            //    //Console.WriteLine(builder.ToString());
            //}
            //
            ////if(langRefs.Count > 0)
            ////    Console.WriteLine();
            //
            //File.WriteAllText(path, builder.ToString());
        }
    }
}
