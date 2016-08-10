using System;
using System.Collections.Generic;
using System.Linq;
using LoremNET;

namespace BC.EQCS.UnitTests.Utils
{
    public static class RandomValueGenerators
    {
        private static readonly Dictionary<string, IValueGenerator> Generators;

        public interface IValueGenerator
        {
            string Key { get; }

            string Generate(string parameters);
        }

        public class ParagraphGenerator : IValueGenerator
        {

            private static readonly Dictionary<string, string> CachedParagraphs;

            static ParagraphGenerator()
            {
                CachedParagraphs = new Dictionary<string, string>();
            }

            public string Key
            {
                get { return "paragraph"; }
            }

            public string Generate(string parameters = null)
            {
                string paragraph = null, key = null;

                parameters = string.IsNullOrWhiteSpace(parameters) ? "5,10,5,10" : parameters;

                // split to 2 sections: word/sentence parameters and key for caching generated paragraphs
                // word/sentence parameters defaults to 5,10,5,10 if "parameters" is null 
                var paramSections = parameters.Split(';').ToArray();

                // when word/sentence is supplied then generate paragraph
                // and when key is supplied then store paragraph in cache
                if (!string.IsNullOrEmpty(paramSections[0]))
                {
                    var wordSentenceSection = paramSections[0].Split(',').Select(int.Parse).ToArray();

                    paragraph = Lorem.Paragraph(wordCountMin: wordSentenceSection[0],
                        wordCountMax: wordSentenceSection[1],
                        sentenceCountMin: wordSentenceSection[2],
                        sentenceCountMax: wordSentenceSection[3]);
                    
                    if (paramSections.Length > 1)
                    {
                        key = paramSections[1];

                        CachedParagraphs[key] = paragraph;
                    }

                    return paragraph;
                }

                // else when word/sentence is not supplied then expect a key
                // and fetch paragraph from cache
                if (paramSections.Length <= 1)
                {
                    throw new Exception("Random generated paragraph key was not suppled");
                };

                key = paramSections[1];

                if (!CachedParagraphs.ContainsKey(key))
                {
                    throw new Exception(string.Format("Random generated paragraph with key {0} does not exists", key));
                }

                paragraph = CachedParagraphs[key];

                return paragraph;
            }
        }

        static RandomValueGenerators()
        {
            Generators =
                new List<IValueGenerator>
                {
                    new ParagraphGenerator()
                }
                    .ToDictionary(generator => generator.Key, StringComparer.InvariantCultureIgnoreCase);
        }

        public static IValueGenerator GetByTypeName(string key)
        {
            return Generators[key];
        }
    }
}