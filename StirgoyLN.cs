using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Zeno
{
    [XmlRoot("feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class Feed
    {
        [XmlElement("entry")]
        public List<Entry> Entries { get; set; }
    }

    public class Entry
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public Link Link { get; set; }

        [XmlElement("updated")]
        public string Updated { get; set; }

        [XmlElement("published")]
        public string Published { get; set; }

        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("summary")]
        public string Summary { get; set; }

        [XmlElement("category")]
        public Category Category { get; set; }

        [XmlElement("content")]
        public string Content { get; set; }
    }


    public class Category
    {
        [XmlAttribute("term")]
        public string Term { get; set; }
    }

    public class Link
    {
        [XmlAttribute("href")]
        public string Href { get; set; }
    }

    internal partial class Program
    {



        public async void StirgoyLN()
        {

            string url = "https://eu.finalfantasyxiv.com/lodestone/news/news.xml";

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    string xmlContent = await client.GetStringAsync(url);
                    XmlSerializer serializer = new XmlSerializer(typeof(Feed));
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, "http://www.w3.org/2005/Atom");

                    Feed feed;
                    using (StringReader reader = new StringReader(xmlContent))
                    {
                        feed = (Feed)serializer.Deserialize(reader);
                    }

                    int max = 9999;
                    int cont = 0;
                    Print("--------------------------------------------------------------------------------");
                    foreach (var entry in feed.Entries)
                    {
                        string id = ParseId(entry.Id); //id
                        string text = ParseTxt(entry.Title);//with title is enough
                        string title = entry.Title;
                        string link = entry.Link.Href;
                        string con = "";
                        string[] both;

                        // Print(entry.Category.Term);
                        switch (entry.Category.Term)
                        {
                            case "Maintenance":

                                if (IsSheudledMaintenance(entry.Content))
                                {
                                    string t1, t2;
                                    string t = ParseMaintenanceSheudles(entry.Content);
                                    con = ParseTxt(entry.Content);//only for sheudled maintenance
                                    if (t.Contains("|"))
                                    {
                                        both = t.Split('|');
                                        t1 = both[0];
                                        t2 = both[1];
                                        DateTime s = DateTime.Parse(t1);
                                        Print("From " + s.ToShortDateString() + " " + s.ToShortTimeString());
                                        DateTime e = DateTime.Parse(t2);
                                        Print("To " + e.ToShortDateString() + " " + e.ToShortTimeString());
                                    
                                    }
                                    else
                                    {
                                        //both = new string[] {t, "" };
                                        t1 = t;
                                        t2 = "";
                                        DateTime s = DateTime.Parse(t1);
                                        Print("From " + s.ToShortDateString() + " " + s.ToShortTimeString());

                                    }
                                    

                                    
                                }
                                else
                                {
                                    
                                }
                                break;

                            case "Notices":
                            case "Status":                                
                            case "Updates":
                                //Print("--------------------------------------------------------------------------------");
                                //Print($"FFXIV {entry.Category.Term} - {title}");
                                //Print(link);
                                break;



                            default:
                                Print(entry.Category.Term);
                                break;
                        }


                        //debug
                        cont++;
                        if (cont >= max)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        }

        string GetLocalTime(string entryData)
        {
            DateTime fe = DateTime.Parse(entryData);
            DateTime r = fe.ToUniversalTime().ToLocalTime();
            return r.ToString("yyyy/MM/dd HH:mm");
        }


        string ParseTxt(string data)
        {
            string r = "";
            bool work = true;

            string wdata = data.Replace("<br>", NL);

            foreach (char item in wdata)
            {
                if (item == '<') { work = false; }
                if (work) { r += item; }
                if (item == '>') { work = true; }
            }
            return r;
        }

        string ParseId(string iddata)
        {
            string r = iddata.Split('/').Last();
            return r;
        }

        string ParseMaintenanceSheudles(string content)
        {
            string r = content;
            string sep = "[Date & Time]";
            string[] partes = Regex.Split(r, Regex.Escape(sep));
            r = partes[1];
            string[] times = null, from_arr = null;
            string date = "", from = "", to = "";

            if (r.Contains("from"))
            {
                Print("Maint sheudle");
                int start = r.IndexOf("<br>");
                int stop = r.IndexOf("(GMT)");
                r = r.Substring(start, stop);
                //r = r.Replace(" ", "");
                r = r.Replace("<br>", "");
                r = r.Replace("\n", "").Replace("\r", "");
                times = Regex.Split(r, "from");
                from_arr = times[0].Split(' ');//0=month 1=day 2year 3 hour
                date = from_arr[2] + "-" + from_arr[0].Replace('.', '-') + from_arr[1].Replace(",", "");
                from = date + " " + from_arr[3] + ":00Z";
                //to = date + " " + times[1].Replace(" ", "") + ":00Z";
                r = from;
            }
            else
            {
                Print("Maint know sheudle");
                int start = r.IndexOf("<br>");
                int stop = r.IndexOf("(GMT)");
                r = r.Substring(start, stop);
                //r = r.Replace(" ", "");
                r = r.Replace("<br>", "");
                r = r.Replace("\n", "").Replace("\r", "");
                times = Regex.Split(r, "to");
                from_arr = times[0].Split(' ');//0=month 1=day 2year 3 hour
                date = from_arr[2] + "-" + from_arr[0].Replace('.', '-') + from_arr[1].Replace(",", "");
                from = date + " " + from_arr[3] + ":00Z";
                to = date + " " + times[1].Replace(" ", "") + ":00Z";
                r = from + "|" + to;
            }




            return r;
        }

        bool IsSheudledMaintenance(string content)
        {
            return (content.Contains("[Date & Time]") && content.Contains(""));

        }



    }

}

