using System.Collections.Generic;
using HtmlAgilityPack;


namespace Company.Function.DigestFetcher
{
    public class DigestFetcher
    {
        private const string BASE_URL = "https://csharpdigest.net/digests";
        public List<Digest> GetDigestLinks(string digestNumber)
        {
            List<string> htmlElements = new List<string>
            {
                "/html/body/div/div/div[1]/p[1]/a",
                "/html/body/div/div/div[2]/p[1]/a",
                "/html/body/div/div/div[3]/p[1]/a",
                "/html/body/div/div/div[4]/p[1]/a",
                "/html/body/div/div/div[5]/p[1]/a"
            };
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument document = htmlWeb.Load($"{BASE_URL + "/" + digestNumber}");
            List<Digest> digests = new List<Digest>();
            foreach(var element in htmlElements)
            {
                var node = document.DocumentNode.SelectNodes(element);
                digests.Add(new Digest {Title =  node[0].InnerText, Link = node[0].Attributes[1].Value});
            }

            return digests ?? new List<Digest>();
        }
    }

    public class Digest
    {
        private string title;
        private string link;
        public string Title
        {
            get => title;
            set => title = value;
        }
        public string Link
        {
            get => link;
            set => link = value;
        }
    }

}


